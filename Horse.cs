using Photon;
using System;
using UnityEngine;
using CLEARSKIES;

public class Horse : MONO
{
    private float awayTimer;
    private TITAN_CONTROLLER controller;
    public GameObject dust;
    public GameObject myHero;
    private Vector3 setPoint;
    private float speed = 45f;
    private string State = "idle";
    private float timeElapsed;
    private float _idleTime = 0f; //aottg2


    public Animation baseA;
    public Rigidbody baseR;
    public Transform baseT;
    public GameObject baseG;
    public Transform baseGT;
    public PhotonView basePV;
    public HERO hero;
    public Transform heroT;
    public Animation heroA;
    public Rigidbody heroR;
    public ParticleSystem dustPS;


    public Horse() : base(SPECIES.Horse)
    {
    }


    private void Awake()
    {
        this.baseA = base.animation;
        this.baseT = base.transform;
        this.baseR = base.rigidbody;
        this.baseG = base.gameObject;
        this.basePV = base.photonView;
        this.baseGT = this.baseG.transform;
        this.dustPS = this.dust.GetComponent<ParticleSystem>();
    }



    private void crossFade(string aniName, float time)
    {
        this.baseA.CrossFade(aniName, time);
        if (PhotonNetwork.connected && this.basePV.isMine)
        {
            object[] parameters = new object[] { aniName, time };
            this.basePV.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    private void followed()
    {
        if (this.myHero != null)
        {
            this.State = "follow";
            this.setPoint = (this.myHero.transform.position + (Vector3.right * UnityEngine.Random.Range(-6, 6))) + (Vector3.forward * UnityEngine.Random.Range(-6, 6));
            this.setPoint.y = this.getHeight(this.setPoint + ((Vector3) (Vector3.up * 5f)));
            this.awayTimer = 0f;
        }
    }

    private float getHeight(Vector3 pt)
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(pt, -Vector3.up, out raycastHit, 1000f, Layer.Ground.value))
        {
            return raycastHit.point.y;
        }
        return 0f;
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(this.baseGT.position + Vector3.up * 0.1f, -Vector3.up, 0.3f, Layer.GroundEnemy.value);
    }

    private void FixedUpdate()
    {
        if ((this.myHero == null) && this.basePV.isMine)
        {
            PhotonNetwork.Destroy(this.baseG);
        }
        if (this.State == "mounted")
        {
            if (this.myHero == null)
            {
                this.unmounted();
                return;
            }
            try
            {
                this.heroT.position = this.baseT.position + Vector3.up * 1.68f;
                this.heroT.rotation = this.baseT.rotation;
                this.heroR.velocity = this.baseR.velocity;
            }
            catch (NullReferenceException) { }
            this.myHero.transform.position = this.baseT.position + ((Vector3)(Vector3.up * 1.68f));

            this.myHero.transform.rotation = this.baseT.rotation;
            this.myHero.rigidbody.velocity = this.baseR.velocity;


            if (this.controller.targetDirection != -874f)
            {
                this.baseGT.rotation = Quaternion.Lerp(this.baseGT.rotation, Quaternion.Euler(0f, this.controller.targetDirection, 0f), (100f * Time.deltaTime) / (this.baseR.velocity.magnitude + 20f));
                if (this.controller.isWALKDown)
                {
                    this.baseR.AddForce((Vector3)((this.baseT.forward * this.speed) * 0.6f), ForceMode.Acceleration);
                    if (this.baseR.velocity.magnitude >= (this.speed * 0.6f))
                    {
                        this.baseR.AddForce((Vector3)((-this.speed * 0.6f) * this.baseR.velocity.normalized), ForceMode.Acceleration);
                    }
                }
                else
                {
                    this.baseR.AddForce((Vector3)(this.baseT.forward * this.speed), ForceMode.Acceleration);
                    if (this.baseR.velocity.magnitude >= this.speed)
                    {
                        this.baseR.AddForce((Vector3)(-this.speed * this.baseR.velocity.normalized), ForceMode.Acceleration);
                    }
                }

                if (this.baseR.velocity.magnitude > 8f)
                {
                    if (!this.baseA.IsPlaying("horse_Run"))
                    {
                        this.crossFade("horse_Run", 0.1f);
                    }
                    if (!this.heroA.IsPlaying("horse_Run"))
                    {
                        this.hero.crossFade("horse_run", 0.1f);
                    }
                    if (!this.dustPS.enableEmission)
                    {
                        this.dustPS.enableEmission = true;
                        object[] parameters = new object[] { true };
                        this.basePV.RPC("setDust", PhotonTargets.Others, parameters);
                    }
                }
                else
                {
                    if (!this.baseA.IsPlaying("horse_WALK"))
                    {
                        this.crossFade("horse_WALK", 0.1f);
                    }
                    if (!this.heroA.IsPlaying("horse_idle"))
                    {
                        this.hero.crossFade("horse_idle", 0.1f);
                    }
                    if (this.dustPS.enableEmission)
                    {
                        this.dustPS.enableEmission = false;
                        object[] objArray2 = new object[] { false };
                        this.basePV.RPC("setDust", PhotonTargets.Others, objArray2);
                    }
                }

            }
            else
            {
                this.toIdleAnimation();

                if (this.baseR.velocity.magnitude > 15f)
                {
                    if (!this.heroA.IsPlaying("horse_run"))
                    {
                        this.hero.crossFade("horse_run", 0.1f);
                    }

                }
                else if (!this.heroA.IsPlaying("horse_idle"))
                {
                    this.hero.crossFade("horse_idle", 0.1f);
                }
            }

            if ((this.controller.isAttackDown || this.controller.isAttackIIDown) && this.IsGrounded())
            {
                this.baseR.AddForce((Vector3)(Vector3.up * 25f), ForceMode.VelocityChange);
            }

        }
        else if (this.State == "follow")
        {
            if (this.myHero == null)
            {
                this.unmounted();
                return;
            }
            if (this.baseR.velocity.magnitude > 8f)
            {
                if (!this.baseA.IsPlaying("horse_run"))
                {
                    this.crossFade("horse_Run", 0.1f);
                }
                if (!this.dustPS.enableEmission)
                {
                    this.dustPS.enableEmission = true;
                    object[] objArray3 = new object[] { true };
                    this.basePV.RPC("setDust", PhotonTargets.Others, objArray3);
                }
            }
            else
            {
                if (!this.baseA.IsPlaying("horse_WALK"))
                {
                    this.crossFade("horse_WALK", 0.1f);
                }
                if (this.dustPS.enableEmission)
                {
                    this.dustPS.enableEmission = false;
                    object[] objArray4 = new object[] { false };
                    this.basePV.RPC("setDust", PhotonTargets.Others, objArray4);
                }
            }
            float num = -Mathf.DeltaAngle(FengMath.getHorizontalAngle(this.baseT.position, this.setPoint), this.baseGT.rotation.eulerAngles.y - 90f);
            this.baseGT.rotation = Quaternion.Lerp(this.baseGT.rotation, Quaternion.Euler(0f, this.baseGT.rotation.eulerAngles.y + num, 0f), (200f * Time.deltaTime) / (this.baseR.velocity.magnitude + 20f));
            if (Vector3.Distance(this.setPoint, this.baseT.position) < 20f)
            {
                this.baseR.AddForce((Vector3)((this.baseT.forward * this.speed) * 0.7f), ForceMode.Acceleration);
                if (this.baseR.velocity.magnitude >= this.speed)
                {
                    this.baseR.AddForce((Vector3)((-this.speed * 0.7f) * this.baseR.velocity.normalized), ForceMode.Acceleration);
                }
            }
            else
            {
                this.baseR.AddForce((Vector3)(this.baseT.forward * this.speed), ForceMode.Acceleration);
                if (this.baseR.velocity.magnitude >= this.speed)
                {
                    this.baseR.AddForce((Vector3)(-this.speed * this.baseR.velocity.normalized), ForceMode.Acceleration);
                }
            }
            this.timeElapsed += Time.deltaTime;
            if (this.timeElapsed > 0.6f)
            {
                this.timeElapsed = 0f;
                if (Vector3.Distance(this.myHero.transform.position, this.setPoint) > 20f)
                {
                    this.followed();
                }
            }
            if (Vector3.Distance(this.myHero.transform.position, this.baseT.position) < 5f)
            {
                this.unmounted();
            }
            if (Vector3.Distance(this.setPoint, this.baseT.position) < 5f)
            {
                this.unmounted();
            }
            this.awayTimer += Time.deltaTime;
            if (this.awayTimer > 6f)
            {
                this.awayTimer = 0f;
                LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("Ground");
                if (Physics.Linecast(this.baseT.position + Vector3.up, this.myHero.transform.position + Vector3.up, mask2.value))
                {
                    this.baseT.position = new Vector3(this.myHero.transform.position.x, this.getHeight(this.myHero.transform.position + ((Vector3)(Vector3.up * 5f))), this.myHero.transform.position.z);
                }
            }
        }
        else if (this.State == "idle")
        {
            this.toIdleAnimation();
            if ((this.myHero != null) && (Vector3.Distance(this.myHero.transform.position, this.baseT.position) > 20f))
            {
                this.followed();
            }
        }
        this.baseR.AddForce(new Vector3(0f, -50f * this.baseR.mass, 0f));
    }

    public void mounted()
    {
        this.State = "mounted";
        this.baseG.GetComponent<TITAN_CONTROLLER>().enabled = true;
    }

    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        this.baseA.CrossFade(aniName, time);
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        this.baseA.Play(aniName);
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        this.baseA.Play(aniName);
        this.baseA[aniName].normalizedTime = normalizedTime;
    }

    public void playAnimation(string aniName)
    {
        this.baseA.Play(aniName);
        if (PhotonNetwork.connected && this.basePV.isMine)
        {
            object[] parameters = new object[] { aniName };
            this.basePV.RPC("netPlayAnimation", PhotonTargets.Others, parameters);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        this.baseA.Play(aniName);
        this.baseA[aniName].normalizedTime = normalizedTime;
        if (PhotonNetwork.connected && this.basePV.isMine)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            this.basePV.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void setDust(bool enable)
    {
        if (this.dustPS.enableEmission)
        {
            this.dustPS.enableEmission = enable;
        }
    }

    private void Start()
    {
        this.controller = base.GetComponent<TITAN_CONTROLLER>();
        foreach (HERO hero in FengGameManagerMKII.heroes)
        {
            if (hero.photonView.owner == this.basePV.owner)
            {
                hero.horse = this;
                break;
            }
        }
    }

    private void toIdleAnimation()
    {
        if (this.baseR.velocity.magnitude > 0.1f)
        {
            if (this.baseR.velocity.magnitude > 15f)
            {
                if (!this.baseA.IsPlaying("horse_Run"))
                {
                    this.crossFade("horse_Run", 0.1f);
                }
                if (!this.dustPS.enableEmission)
                {
                    this.dustPS.enableEmission = true;
                    object[] parameters = new object[] { true };
                    this.basePV.RPC("setDust", PhotonTargets.Others, parameters);
                }
            }
            else
            {
                if (!this.baseA.IsPlaying("horse_WALK"))
                {
                    this.crossFade("horse_WALK", 0.1f);
                }
                if (this.dustPS.enableEmission)
                {
                    this.dustPS.enableEmission = false;
                    object[] objArray2 = new object[] { false };
                    this.basePV.RPC("setDust", PhotonTargets.Others, objArray2);
                }
            }
        }//aottg2
        else 
        {
            if (_idleTime <= 0f)
            {
                if (base.animation.IsPlaying("horse_idle0"))
                {
                    //this.crossFade("horse_idle1", 0.1f);
                    float num = UnityEngine.Random.Range(0f, 1f);
                    if (num < 0.33f)
                        this.crossFade("horse_idle1", 0.1f);
                    else if (num < 0.66f)
                        this.crossFade("horse_idle2", 0.1f);
                    else
                        this.crossFade("horse_idle3", 0.1f);
                    _idleTime = 1f;
                }
                else
                {
                    this.crossFade("horse_idle0", 0.1f);
                    _idleTime = UnityEngine.Random.Range(1f, 4f);
                }
            }
            if (this.dustPS.enableEmission)
            {
                this.dustPS.enableEmission = false;
                object[] objArray3 = new object[] { false };
                this.basePV.RPC("setDust", PhotonTargets.Others, objArray3);
            }
            // this.baseR.AddForce(-this.baseR.velocity, ForceMode.VelocityChange);
            _idleTime -= Time.deltaTime;
        }
    }

    public void unmounted()
    {
        this.State = "idle";
        this.baseG.GetComponent<TITAN_CONTROLLER>().enabled = false;
    }
}

