using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using CLEARSKIES;
using Utility;

public class Bomb : Photon.MonoBehaviour
{


    public static int bombId = 0;

    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool Disabled;
    public float SmoothingDelay = 10f;
    public float BombRadius;
    private TITAN _collidedTitan;
    private SphereCollider _sphereCollider;
    private List<GameObject> _hideUponDestroy;
    private ParticleSystem _trail;
    private ParticleSystem _flame;
    private float _DisabledTrailFadeMultiplier = 0.6f;
    private bool _receivedNonZeroVelocity;
    private bool _ownerIsUpdated;
    public float bombStickTime = 0f;
    public HERO myHero;
    private float _minimumLuminance = 0.4f;

    #region old variables
    public GameObject myExplosion;

    private Transform baseT;
    private Rigidbody baseR;
    private PhotonView basePV;
    private bool isMine
    {
        get
        {
            if (this.basePV == null)
            {
                this.basePV = base.photonView;
                //this.basePV = base.GetComponent<PhotonView>(); //wutahell
            }
            return this.basePV != null && this.basePV.isMine;
        }
    }
    #endregion
    public bool Stuck = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isMine && !this.Disabled)
        {
            if (HERO.StickyBombEnabled) //sticky
            {
                this.Stuck = true;
                base.rigidbody.isKinematic = true;
                // base.gameObject.GetComponent<HERO>().cachedSprites["skill_cd_bottom"].color = Color.red;
            }
            else if (((int)FengGameManagerMKII.settings[291]) == 0) //detonate
            {
                this.Explode(this.BombRadius);
            }
        }
    }

    public void Awake()
    {
        baseT = base.transform;
        baseR = base.rigidbody;
        basePV = base.photonView;

        if (basePV != null)
        {
            basePV.observed = this;
            this.correctPlayerPos = baseT.position;
            this.correctPlayerRot = Quaternion.identity; //base.transform.rotation;
            PhotonPlayer owner = base.photonView.owner;
            this._trail = baseT.Find("Trail").GetComponent<ParticleSystem>();
            this._flame = baseT.Find("Flame").GetComponent<ParticleSystem>();

            // Set collider and reduce it by half.
            this._sphereCollider = base.GetComponent<SphereCollider>();
            this._sphereCollider.radius /= 2;

            this._hideUponDestroy = new List<GameObject>();
            this._hideUponDestroy.Add(baseT.Find("Flame").gameObject);
            this._hideUponDestroy.Add(baseT.Find("ThunderSpearModel").gameObject);
            if (GameSettings.ShowBombColor > 0)
            {
                Color bombColor = BombUtil.GetBombColor(owner, 1f);
                this._trail.startColor = bombColor;
            }
            if (base.photonView.isMine)
            {
                base.photonView.RPC("IsUpdatedRPC", PhotonTargets.All, new object[0]);
                //Events.EventManager.CallEvent("bomb_shot");
            }
            else
            {
                // change bomb color here
                if (ThymesUtils.RelativeLuminance(this._trail.startColor) < _minimumLuminance)
                {
                    this._trail.startColor = Color.white;
                }

            }
        }
    }

    [RPC]
    private void IsUpdatedRPC(PhotonMessageInfo info)
    {
        if (info.sender != base.photonView.owner)
        {
            return;
        }
        this._ownerIsUpdated = true;
    }


    public void DestroySelf()
    {
        if (isMine && !this.Disabled)
        {
            base.photonView.RPC("DisableRPC", PhotonTargets.All, new object[0]);
            base.StartCoroutine(this.WaitAndFinishDestroyCoroutine(1.5f));
        }
    }



    public void Explode(float radius)
    {
        if (!this.Disabled)
        {
            try
            {
                base.rigidbody.isKinematic = false;
                this.Stuck = false;
                PhotonNetwork.Instantiate("RCAsset/BombExplodeMain", baseT.position, Quaternion.Euler(0f, 0f, 0f), 0);
                this.KillPlayersInRadius(radius);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            this.DestroySelf();
            if (HERO.StickyBombEnabled)
            {
                HERO.StickyBombEnabled = false;
            }
            
        }
    }

    public static bool PointNotOccluded(Vector3 pointa, Vector3 pointb)
    {
        RaycastHit depthCheck;
        Vector3 directionBetween = pointa - pointb;
        directionBetween = directionBetween.normalized;

        float distance = Vector3.Distance(pointb, pointa);

        if (Physics.Raycast(pointb, directionBetween, out depthCheck, distance + 0.05f, Layer.GroundEnemy.value))
        {
            if (depthCheck.point != pointa)
            {
                // log name and tag of what was hit
                UnityEngine.Debug.Log($"Hit missed (occluded by {depthCheck.collider.name} with tag {depthCheck.collider.tag})");
            }
            return depthCheck.point != pointa;
        }
        return false;
    }

    public bool PointNotOccluded2(Vector3 bombPosition, Vector3 playerPosition)
    {
        // Check that we have a clear shot to the target.
        Vector3 angle = (playerPosition - bombPosition).normalized;
        float dist = Vector3.Distance(playerPosition, bombPosition);
        RaycastHit hit;

        // Only need to check if the distance is 0 since raycast requires that. Also a 0 dist bomb is a good bomb.
        if (dist != 0)
        {
            if (Physics.Raycast(bombPosition, angle, out hit, dist, Layer.GroundEnemy.value))
            {
                // log name and tag of what was hit
                UnityEngine.Debug.Log($"Hit missed (occluded by {hit.collider.name} with tag {hit.collider.tag})");
                return false;
            }
        }
        return true;
    }

    private void KillPlayersInRadius(float radius)
    {
        foreach (HERO hero in FengGameManagerMKII.heroes)
        {
            GameObject gameObject = hero.gameObject;
            if (Vector3.Distance(gameObject.transform.position, baseT.position) < radius && !gameObject.GetPhotonView().isMine && !hero.bombImmune)
            {
                PhotonPlayer owner = hero.basePV.owner;
                if (owner.ID == PhotonNetwork.player.ID) continue;

                if (GameSettings.teamMode > 0 && PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null && owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    int num = RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]);
                    int num2 = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
                    if (num == 0 || num != num2)
                    {
                        this.KillPlayer(hero);
                        
                    }
                }
                else
                {
                    this.KillPlayer(hero); //creates exception here when killing twice
                }
            }
        }
    }

    //killplayerinradius -> killplayer
    private void KillPlayer(HERO hero)
    {
        //HERO local = HERO.FindMyHero(PhotonNetwork.player.ID);

        //if (/*when carried, cant kill person who carries*/local.PersonWhoCarries != hero.transform.root.GetComponent<HERO>() && local.PersonBeingCarried != hero.transform.root.GetComponent<HERO>())
        {
            //if (Vector3.Distance(hero.transform.position, local.transform.position) < 3) return;
            hero.markDie(); 
            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) + " " });
            FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
        }
    }

    private void SetDisabledTrailFade()
    {
        int particleCount = this._trail.particleCount;
        float startLifetime = this._trail.startLifetime * this._DisabledTrailFadeMultiplier;
        ParticleSystem.Particle[] array = new ParticleSystem.Particle[particleCount];
        this._trail.GetParticles(array);
        for (int i = 0; i < particleCount; i++)
        {
            ParticleSystem.Particle[] array2 = array;
            int num = i;
            array2[num].lifetime = array2[num].lifetime * this._DisabledTrailFadeMultiplier;
            array[i].startLifetime = startLifetime;
        }
        this._trail.SetParticles(array, particleCount);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(baseT.position);
            stream.SendNext(baseT.rotation);
            stream.SendNext(baseR.velocity);
            return;
        }
        this.correctPlayerPos = (Vector3)stream.ReceiveNext();
        this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        this.correctPlayerVelocity = (Vector3)stream.ReceiveNext();


        //float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
        //this.correctPlayerPos += rigidbody.velocity * lag;
    }

    //private void FixedUpdate()
    //{
    //    if (!this.Disabled && isMine)
    //    {
    //        this.CheckCollide();
    //    }
    //}

    [RPC]
    public void DisableRPC(PhotonMessageInfo info = null)
    {
        if (this.Disabled) return;

        if (info != null && info.sender != base.photonView.owner) return;

        foreach (GameObject gameObject in this._hideUponDestroy)
        {
            gameObject.SetActive(false);
        }
        this._sphereCollider.enabled = false;
        this.SetDisabledTrailFade();
        base.rigidbody.velocity = Vector3.zero;
        this.Disabled = true;
    }

    //private void CheckCollide()
    //{
    //    LayerMask mask = 1 << LayerMask.NameToLayer("PlayerAttackBox") | 1 << LayerMask.NameToLayer("PlayerHitBox");
    //    foreach (Collider collider in Physics.OverlapSphere(base.transform.position + this._sphereCollider.center, this._sphereCollider.radius, mask))
    //    {
    //        if (collider.name.Contains("PlayerDetectorRC"))
    //        {
    //            TITAN component = collider.transform.root.gameObject.GetComponent<TITAN>();
    //            if (component != null)
    //            {
    //                if (this._collidedTitan == null)
    //                {
    //                    this._collidedTitan = component;
    //                    this._collidedTitan.isThunderSpear = true;
    //                }
    //                else if (this._collidedTitan != component)
    //                {
    //                    this._collidedTitan.isThunderSpear = false;
    //                    this._collidedTitan = component;
    //                    this._collidedTitan.isThunderSpear = true;
    //                }
    //            }
    //        }
    //        //shouldnt collide on people
    //        //else if (collider.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox") && !collider.transform.root.gameObject.GetComponent<HERO>().photonView.isMine)
    //        //{
    //        //    this.Explode(this.BombRadius);
    //        //}
    //    }
    //}


    public void Update()
    {

        if (!isMine)
        {
            if (this.baseT == null) this.baseT = base.transform;
            if (this.baseR == null) this.baseR = base.rigidbody;

            if (this.baseT != null)
            {
                baseT.position = Vector3.Lerp(baseT.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
                baseT.rotation = Quaternion.Lerp(baseT.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            }
            if (this.baseR != null) baseR.velocity = this.correctPlayerVelocity;

            if (base.rigidbody.velocity != Vector3.zero)
            {
                this._receivedNonZeroVelocity = true;
                return;
            }
            if (!this._ownerIsUpdated && this._receivedNonZeroVelocity && !Disabled)
            {
                Disabled = true;
                foreach (GameObject gameObject in this._hideUponDestroy)
                    gameObject.SetActive(false);

            }
        }

    }

    private IEnumerator WaitAndFinishDestroyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (this._collidedTitan != null)
        {
            this._collidedTitan.isThunderSpear = false;
        }
        PhotonNetwork.Destroy(base.gameObject);
        yield break;
    }

}

