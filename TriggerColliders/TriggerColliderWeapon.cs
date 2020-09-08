using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class TriggerColliderWeapon : MonoBehaviour
{
    public bool active_me;
    public ArrayList currentHits = new ArrayList();
    public ArrayList currentHitsII = new ArrayList();
    public AudioSource meatDie;
    public int myTeam = 1;
    public float scoreMulti = 1f;


    private GameObject heroG;
    private HERO hero;
    private Transform baseT;
    private PhotonView basePV;
    private IN_GAME_MAIN_CAMERA mainCamera;



    private bool checkIfBehind(GameObject titan)
    {
        Transform transform = titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
        return Vector3.Angle(-transform.forward, this.baseT.position - transform.position) < 70f;
    }

    public void clearHits()
    {
        this.currentHitsII = new ArrayList();
        this.currentHits = new ArrayList();
    }

    private void napeMeat(Vector3 vkill, Transform titan)
    {
        TITAN component = titan.root.GetComponent<TITAN>();
        if (component == null)
        {
            return;
        }
        Transform transform = component.neck;
        if (transform == null)
            transform = titan.root.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
        GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("titanNapeMeat"), transform.position, transform.rotation);
        gameObject.transform.localScale = titan.localScale;
        Rigidbody rigidbody = gameObject.rigidbody;
        rigidbody.AddForce((Vector3)(vkill.normalized * 15f), ForceMode.Impulse);
        rigidbody.AddForce((Vector3)(-titan.forward * 10f), ForceMode.Impulse);
        rigidbody.AddTorque(new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100)), ForceMode.Impulse);
    }

    private int Damage(Component component)
    {
        return Mathf.Max(10, (int)((this.hero.rigidbody.velocity - component.rigidbody.velocity).magnitude * 10f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (this.active_me)
        {
            GameObject otherG = other.gameObject;
            Transform otherT = other.transform;
            if (!this.currentHitsII.Contains(otherG))
            {
                this.currentHitsII.Add(otherG);
                mainCamera.startShake(0.1f, 0.1f, 0.95f);
                if (otherT.root.gameObject.CompareTag("titan"))
                {
                    this.hero.slashHit.Play();
                    if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                    {
                        PhotonNetwork.Instantiate("hitMeat", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0, null);
                    }
                    else
                    {
                        ((GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("hitMeat"))).transform.position = baseT.position;
                    }
                    this.hero.useBlade(0);
                }
            }
            string tag;
            if ((tag = otherG.tag) != null)
            {
                HitBox component = otherG.GetComponent<HitBox>();
                Transform componentT, componentTroot;
                GameObject componentGO;
                MONO monosingletone;
                switch (tag)
                {
                    case "playerHitbox":
                        if (LevelInfo.getInfo(FengGameManagerMKII.level).pvp)
                        {
                            float b = 1f - (Vector3.Distance(otherT.position, this.baseT.position) * 0.05f);
                            b = Mathf.Min(1f, b);
                            if ((componentTroot = (componentT = component.transform).root) != null)
                            {
                                HERO Hero = componentTroot.GetComponent<HERO>();
                                if (Hero != null && Hero.myTeam != this.myTeam && !Hero.isInvincible() && !Hero.isGrabbed)
                                {
                                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                    {
                                        Hero.die((componentTroot.position - this.baseT.position).normalized * b * 1000f + Vector3.up * 50f, false);
                                        return;
                                    }
                                    if (!Hero.HasDied())
                                    {
                                        Hero.markDie();
                                        object[] parameters = new object[]
                                        {
                                                (componentTroot.position - baseT.position).normalized * b * 1000f + Vector3.up * 50f,
                                                false,
                                                basePV.viewID,
                                                basePV.owner.customProperties[PhotonPlayerProperty.name],
                                                false
                                        };
                                        Hero.photonView.RPC("netDie", PhotonTargets.All, parameters);
                                        PhotonNetwork.Instantiate("redCross1", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
                                        PhotonNetwork.Instantiate("redCross1", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
                                    }
                                }
                            }
                        }
                        break;
                    case "titanneck":
                        if ((componentGO = (componentTroot = (componentT = component.transform).root).gameObject) != null)
                        {
                            if (this.checkIfBehind(componentGO) && !this.currentHits.Contains(component))
                            {
                                component.hitPosition = (Vector3)((this.baseT.position + componentT.position) * 0.5f);
                                this.currentHits.Add(component);
                                this.meatDie.Play();
                                monosingletone = componentTroot.GetComponent<MONO>();
                                SPECIES specie = monosingletone.species;
                                switch (specie)
                                {
                                    case SPECIES.Titan:
                                        TITAN titan = monosingletone as TITAN;
                                        if (titan != null && !titan.hasDie)
                                        {
                                            int num2 = this.Damage(componentTroot);
                                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                            {
                                                if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
                                                {
                                                    mainCamera.startSnapShot2(componentT.position, num2, componentGO, 0.02f);
                                                }
                                                titan.die();
                                                this.napeMeat(this.hero.rigidbody.velocity, componentTroot);
                                                FengGameManagerMKII.instance.netShowDamage(num2);
                                                FengGameManagerMKII.instance.playerKillInfoSingleUpdate(num2);
                                            }
                                            else
                                            {
                                                if (!PhotonNetwork.isMasterClient)
                                                {
                                                    if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
                                                    {
                                                        mainCamera.startSnapShot2(componentT.position, num2, componentGO, 0.02f);
                                                        titan.asClientLookTarget = false;
                                                    }
                                                    object[] objArray2 = new object[] { basePV.viewID, num2 };
                                                    titan.photonView.RPC("titanGetHit", titan.photonView.owner, objArray2);
                                                }
                                                else
                                                {
                                                    if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
                                                        mainCamera.startSnapShot2(componentT.position, num2, componentGO, 0.02f);
                                                    titan.titanGetHit(this.basePV.viewID, num2);
                                                }
                                            }
                                        }
                                        break;
                                    case SPECIES.Hero | SPECIES.Titan:
                                        break;
                                    case SPECIES.FemaleTitan:
                                        FEMALE_TITAN female_titan = monosingletone as FEMALE_TITAN;
                                        this.hero.useBlade(0x7fffffff);
                                        if (female_titan != null && !female_titan.hasDie)
                                        {
                                            int num2 = this.Damage(componentTroot);
                                            if (!PhotonNetwork.isMasterClient)
                                            {
                                                object[] objArray3 = new object[] { this.basePV.viewID, num2 };
                                                female_titan.photonView.RPC("titanGetHit", female_titan.photonView.owner, objArray3);
                                            }
                                            else
                                            {
                                                if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
                                                    mainCamera.startSnapShot2(componentT.position, num2, null, 0.02f);
                                                female_titan.titanGetHit(this.basePV.viewID, num2);
                                            }
                                        }
                                        break;
                                    default:
                                        if (monosingletone.species == SPECIES.ColossalTitan)
                                        {
                                            COLOSSAL_TITAN colossal_titan = monosingletone as COLOSSAL_TITAN;
                                            this.hero.useBlade(0x7fffffff);
                                            if (!colossal_titan.hasDie)
                                            {
                                                int num2 = this.Damage(componentTroot);
                                                if (!PhotonNetwork.isMasterClient)
                                                {
                                                    object[] objArray4 = new object[] { this.basePV.viewID, num2 };
                                                    colossal_titan.photonView.RPC("titanGetHit", colossal_titan.photonView.owner, objArray4);
                                                }
                                                else

                                                    if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
                                                    mainCamera.startSnapShot2(componentT.position, num2, null, 0.02f);
                                                colossal_titan.titanGetHit(this.basePV.viewID, num2);
                                            }
                                        }
                                        break;
                                }
                                this.showCriticalHitFX();
                            }
                        }
                        break;
                    case "titaneye":
                        if (!this.currentHits.Contains(otherG))
                        {
                            this.currentHits.Add(otherG);
                            componentT = otherG.transform;
                            if (componentT != null)
                            {
                                monosingletone = componentT.root.GetComponent<MONO>();
                                if (monosingletone != null)
                                {
                                    switch (monosingletone.species)
                                    {
                                        case SPECIES.Titan:
                                            TITAN titan = monosingletone as TITAN;
                                            if (titan.abnormalType != AbnormalType.TYPE_CRAWLER && !titan.hasDie)
                                            {
                                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                    titan.hitEye();
                                                else if (!PhotonNetwork.isMasterClient)
                                                {
                                                    object[] objArray6 = new object[] { this.basePV.viewID };
                                                    titan.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray6);
                                                }
                                                else
                                                    titan.hitEyeRPC(this.baseT.root.gameObject.GetPhotonView().viewID);
                                                this.showCriticalHitFX();
                                            }
                                            break;
                                        case SPECIES.FemaleTitan:
                                            FEMALE_TITAN female_titan = monosingletone as FEMALE_TITAN;
                                            if (!female_titan.hasDie)
                                            {
                                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                    female_titan.hitEye();
                                                else if (!PhotonNetwork.isMasterClient)
                                                {
                                                    object[] objArray5 = new object[] { this.basePV.viewID };
                                                    female_titan.photonView.RPC("hitEyeRPC", PhotonTargets.MasterClient, objArray5);
                                                }
                                                else
                                                    female_titan.hitEyeRPC(this.basePV.viewID);
                                                this.showCriticalHitFX();
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case "titanankle":
                        if (!this.currentHits.Contains(otherG))
                        {
                            this.currentHits.Add(otherG);
                            componentT = otherG.transform;
                            if (componentT != null)
                            {
                                monosingletone = componentT.root.GetComponent<MONO>();
                                switch (monosingletone.species)
                                {
                                    case SPECIES.Titan:
                                        TITAN titan = monosingletone as TITAN;
                                        if (titan.abnormalType != AbnormalType.TYPE_CRAWLER && !titan.hasDie)
                                        {
                                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                titan.hitAnkle();
                                            else if (!PhotonNetwork.isMasterClient)
                                            {
                                                object[] objArray6 = new object[] { this.basePV.viewID };
                                            }
                                            else
                                                titan.hitAnkle();
                                            this.showCriticalHitFX();
                                        }
                                        break;
                                    case SPECIES.FemaleTitan:
                                        FEMALE_TITAN female_titan = monosingletone as FEMALE_TITAN;
                                        if (!female_titan.hasDie)
                                        {
                                            int num2 = this.Damage(female_titan);
                                            if (otherG.name == "ankleR")
                                            {
                                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                {
                                                    female_titan.hitAnkleR(num2);
                                                    FengGameManagerMKII.instance.netShowDamage(num2);
                                                }
                                                else if (!PhotonNetwork.isMasterClient)
                                                {
                                                    object[] objArray5 = new object[] { this.basePV.viewID };
                                                    female_titan.photonView.RPC("hitAnkleRRPC", PhotonTargets.MasterClient, objArray5);
                                                }
                                                else
                                                    female_titan.hitAnkleRRPC(this.basePV.viewID, num2);
                                            }
                                            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                            {
                                                female_titan.hitAnkleL(num2);
                                                FengGameManagerMKII.instance.netShowDamage(num2);
                                            }
                                            else if (!PhotonNetwork.isMasterClient)
                                            {
                                                object[] objArray5 = new object[] { this.basePV.viewID };
                                                female_titan.photonView.RPC("hitAnkleLRPC", PhotonTargets.MasterClient, objArray5);
                                            }
                                            else
                                                female_titan.hitAnkleLRPC(this.basePV.viewID, num2);
                                            this.showCriticalHitFX();
                                        }
                                        break;
                                }
                            }

                        }
                        break;

                    default:
                        return;
                }
            }
        }
    }

    private void showCriticalHitFX()
    {
        GameObject obj2;
        mainCamera.startShake(0.2f, 0.3f, 0.95f);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            obj2 = PhotonNetwork.Instantiate("redCross1", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
            obj2 = PhotonNetwork.Instantiate("redCross1", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0);
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("redCross1"));
        }
        obj2.transform.position = this.baseT.position;
        obj2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Start()
    {
        this.hero = (this.heroG = (this.baseT = base.transform).root.gameObject).GetComponent<HERO>();
        this.basePV = this.baseT.root.GetComponent<PhotonView>();
        this.mainCamera = IN_GAME_MAIN_CAMERA.mainCamera;
    }
}

