using ExitGames.Client.Photon;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Xft;
using BRM;
using CLEARSKIES;


public class HERO : MONO
{
    private HERO_STATE _state;
    private bool almostSingleHook;
    private string attackAnimation;
    private int attackLoop;
    private bool attackMove;
    private bool attackReleased;
    public AudioSource audio_ally;
    public AudioSource audio_hitwall;
    private GameObject badGuy;
    public float bombCD;
    public bool bombImmune;
    public float bombRadius;
    public float bombSpeed;
    public float bombTime;
    public float bombTimeMax;
    private float buffTime;
    public GameObject bulletLeft;
    private int bulletMAX = 7;
    public GameObject bulletRight;
    private bool buttonAttackRelease;
    public Dictionary<string, UISprite> cachedSprites;
    public float CameraMultiplier;
    public bool canJump = true;
    public GameObject checkBoxLeft;
    public GameObject checkBoxRight;


    public string currentAnimation;
    private int currentBladeNum = 5;
    private float currentBladeSta = 100f;
    private BUFF currentBuff;
    //public Camera currentCamera;
    private float currentGas = 100f;
    public float currentSpeed;
    public Vector3 currentV;
    private bool dashD;
    private Vector3 dashDirection;
    private bool dashL;
    private bool dashR;
    private float dashTime;
    private bool dashU;
    private Vector3 dashV;
    public bool detonate;
    private float dTapTime = -1f;
    private bool EHold;
    private GameObject eren_titan;
    private int escapeTimes = 1;
    private float facingDirection;
    private float flare1CD;
    private float flare2CD;
    private float flare3CD;
    private float flareTotalCD = 30f;
    private Transform forearmL;
    private Transform forearmR;
    private float gravity = 20f;
    private bool grounded;
    private GameObject gunDummy;
    private Vector3 gunTarget;
    private Transform handL;
    private Transform handR;
    private bool hasDied;
    public bool hasspawn;
    private bool hookBySomeOne = true;

    private bool hookSomeOne;
    private GameObject hookTarget;
    private float invincible = 3f;
    public bool isCannon;
    private bool isLaunchLeft;
    private bool isLaunchRight;
    private bool isLeftHandHooked;
    private bool isMounted;
    public bool isPhotonCamera;
    private bool isRightHandHooked;
    public float jumpHeight = 2f;
    private bool justGrounded;
    public UILabel LabelDistance;
    public Transform lastHook;
    private float launchElapsedTimeL;
    private float launchElapsedTimeR;
    private Vector3 launchForce;
    private Vector3 launchPointLeft;
    private Vector3 launchPointRight;
    private bool leanLeft;
    private bool leftArmAim;

    private int leftBulletLeft = 7;
    private bool leftGunHasBullet = true;
    private float lTapTime = -1f;
    //public GameObject maincamera;
    public float maxVelocityChange = 10f;
    public AudioSource meatDie;
    public Bomb myBomb;
    public GameObject myCannon;
    public Transform myCannonBase;
    public Transform myCannonPlayer;
    public CannonPropRegion myCannonRegion;
    public GROUP myGroup;
    private GameObject myHorse;
    public GameObject myNetWorkName;
    public float myScale = 1f;
    public int myTeam = 1;
    public List<TITAN> myTitans;
    private bool needLean;
    private Quaternion oldHeadRotation;
    private float originVM;
    private bool QHold;
    private string reloadAnimation = string.Empty;
    private bool rightArmAim;

    private int rightBulletLeft = 7;
    private bool rightGunHasBullet = true;
    public AudioSource rope;
    private float rTapTime = -1f;
    public HERO_SETUP setup;
    private GameObject skillCD;
    public float skillCDDuration;
    public float skillCDLast;
    public float skillCDLastCannon;
    private string skillId;
    public string skillIDHUD;
    public AudioSource slash;
    public AudioSource slashHit;
    private ParticleSystem smoke_3dmg;
    private ParticleSystem sparks;
    public float speed = 10f;
    public GameObject speedFX;
    public GameObject speedFX1;
    private ParticleSystem speedFXPS;
    private string standAnimation = "stand";
    private Quaternion targetHeadRotation;
    private Quaternion targetRotation;
    public Vector3 targetV;
    private bool throwedBlades;
    public bool titanForm;
    private GameObject titanWhoGrabMe;
    private int titanWhoGrabMeID;
    private int totalBladeNum = 5;
    public float totalBladeSta = 100f;
    public float totalGas = 100f;
    private Transform upperarmL;
    private Transform upperarmR;
    private float useGasSpeed = 0.2f;
    public bool useGun;
    private float uTapTime = -1f;
    private bool wallJump;
    private float wallRunTime;


    //public Transform head;
    private UISprite flare1;
    private UISprite flare2;
    private UISprite flare3;
    private Transform myNetWorkNameT;
    public XWeaponTrail rightbladetrail;
    public XWeaponTrail rightbladetrail2;
    public XWeaponTrail leftbladetrail;
    public XWeaponTrail leftbladetrail2;
    public GameObject hookRefL1;
    public GameObject hookRefL2;
    public GameObject hookRefR1;
    public GameObject hookRefR2;
    public SmoothSyncMovement sync;
    private GameObject cross1;
    private GameObject cross2;
    private Transform crossT1;
    private Transform crossT2;
    public GameObject crossL1;
    public GameObject crossL2;
    public GameObject crossR1;
    public GameObject crossR2;
    private Transform crossL1T;
    private Transform crossL2T;
    private Transform crossR1T;
    private Transform crossR2T;
    private Transform bulletLT;
    private Transform bulletRT;
    private Bullet LBullet;
    private Bullet RBullet;
    private List<Bullet> DashRampage = new List<Bullet>();
    private Transform labelT;
    public Transform hookRefL1T;
    public Transform hookRefL2T;
    public Transform hookRefR1T;
    public Transform hookRefR2T;
    //private UISprite flare1;
    //private UISprite flare2;
    //private UISprite flare3;
    private TriggerColliderWeapon triggerLeft;
    private TriggerColliderWeapon triggerRight;
    public CapsuleCollider capsule;
    public Horse horse;


    public Dictionary<string, Bullet> hook = new Dictionary<string, Bullet>
    {
        {
            "left",
            null
        },
        {
            "right",
            null
        }
    };

    public Bullet currentHook
    {
        get
        {
            if (this.hook.ContainsKey("left") && this.hook["left"] != null && this.hook["left"].isHooked())
            {
                return this.hook["left"];
            }
            if (this.hook.ContainsKey("right") && this.hook["right"] != null && this.hook["right"].isHooked())
            {
                return this.hook["right"];
            }
            return null;
        }
    }
    

    private bool netPauseStopped;

    private HERO() : base(SPECIES.Hero)
    {
    }

    private void applyForceToBody(GameObject GO, Vector3 v)
    {
        Rigidbody rigidbody = GO.rigidbody;
        rigidbody.AddForce(v);
        rigidbody.AddTorque(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
    }
    private static void applyForceToBody(Component GO, Vector3 v)
    {
        Rigidbody rigidbody = GO.rigidbody;
        rigidbody.AddForce(v);
        rigidbody.AddTorque(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
    }

    public void attackAccordingToMouse()
    {
        if (Input.mousePosition.x < (Screen.width * 0.5))
        {
            this.attackAnimation = "attack2";
        }
        else
        {
            this.attackAnimation = "attack1";
        }
    }

    public void attackAccordingToTarget(Transform a)
    {
        Vector3 vector = a.position - baseT.position;
        float current = -Mathf.Atan2(vector.z, vector.x) * 57.29578f;
        float f = -Mathf.DeltaAngle(current, baseT.rotation.eulerAngles.y - 90f);
        if (((Mathf.Abs(f) < 90f) && (vector.magnitude < 6f)) && ((a.position.y <= (baseT.position.y + 2f)) && (a.position.y >= (baseT.position.y - 5f))))
        {
            this.attackAnimation = "attack4";
        }
        else if (f > 0f)
        {
            this.attackAnimation = "attack1";
        }
        else
        {
            this.attackAnimation = "attack2";
        }
    }

    private void Awake()
    {
        this.cache();
        this.setup = baseG.GetComponent<HERO_SETUP>();
        baseR.freezeRotation = true;
        baseR.useGravity = false;
        this.handL = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
        this.handR = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
        this.forearmL = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L");
        this.forearmR = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        this.upperarmL = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L");
        this.upperarmR = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
    }

    public void backToHuman()
    {
        sync.disabled = false;
        baseR.velocity = Vector3.zero;
        this.titanForm = false;
        this.ungrabbed();
        this.falseAttack();
        this.skillCDDuration = this.skillCDLast;
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(baseG, true, false);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            basePV.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
        }
    }

    [RPC]
    private void backToHumanRPC()
    {
        this.titanForm = false;
        this.eren_titan = null;
        sync.disabled = false;
    }

    [RPC]
    public void badGuyReleaseMe()
    {
        this.hookBySomeOne = false;
        this.badGuy = null;
    }

    //[RPC]
    //public void blowAway(Vector3 force)
    //{
    //    if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
    //    {
    //        baseR.AddForce(force, ForceMode.Impulse);
    //        baseT.LookAt(baseT.position);
    //    }
    //}
    [RPC]
    public void blowAway(Vector3 force, PhotonMessageInfo info = null)
    {
        if (info != null && (info.sender == PhotonNetwork.player || info.sender.isMasterClient) || Vector3.Distance(force, new Vector3(0f, 0f, 0f)) <= 18f && Vector3.Distance(force, new Vector3(0f, 0f, 0f)) >= -18f)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE || photonView.isMine)
            {
                rigidbody.AddForce(force, ForceMode.Impulse);
                transform.LookAt(transform.position);
            }
        }
    }

    private void bodyLean()
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
        {
            float z = 0f;
            this.needLean = false;
            if ((!this.useGun && (this.state == HERO_STATE.Attack)) && ((this.attackAnimation != "attack3_1") && (this.attackAnimation != "attack3_2")))
            {
                this.targetRotation = Quaternion.Euler(-(Mathf.Atan2(this.baseR.velocity.y, Mathf.Sqrt(this.baseR.velocity.x * this.baseR.velocity.x + this.baseR.velocity.z * this.baseR.velocity.z)) * 57.29578f) * (1f - Vector3.Angle(this.baseR.velocity, this.baseT.forward) / 90f), this.facingDirection, 0f);
                if ((this.isLeftHandHooked && (this.bulletLeft != null)) || (this.isRightHandHooked && (this.bulletRight != null)))
                {
                    baseT.rotation = this.targetRotation;
                }
            }
            else
            {
                if ((this.isLeftHandHooked && (this.bulletLeft != null)) && (this.isRightHandHooked && (this.bulletRight != null)))
                {
                    if (this.almostSingleHook)
                    {
                        this.needLean = true;
                        z = this.getLeanAngle(this.bulletRT.position, true);
                    }
                }
                else if (this.isLeftHandHooked && (this.bulletLeft != null))
                {
                    this.needLean = true;
                    z = this.getLeanAngle(this.bulletLT.position, true);
                }
                else if (this.isRightHandHooked && (this.bulletRight != null))
                {
                    this.needLean = true;
                    z = this.getLeanAngle(this.bulletRT.position, false);
                }
                if (this.needLean)
                {
                    float a = 0f;
                    if (!this.useGun && (this.state != HERO_STATE.Attack))
                    {
                        a = this.currentSpeed * 0.1f;
                        a = Mathf.Min(a, 20f);
                    }
                    this.targetRotation = Quaternion.Euler(-a, this.facingDirection, z);
                }
                else if (this.state != HERO_STATE.Attack)
                {
                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                }
            }
        }
    }

    public void bombInit()
    {
        this.skillIDHUD = this.skillId;
        this.skillCDDuration = this.skillCDLast;
        if (GameSettings.bombMode == 1)
        {
            int num = (int)FengGameManagerMKII.settings[250];
            int num2 = (int)FengGameManagerMKII.settings[0xfb];
            int num3 = (int)FengGameManagerMKII.settings[0xfc];
            int num4 = (int)FengGameManagerMKII.settings[0xfd];
            if ((num < 0) || (num > 10))
            {
                num = 5;
                FengGameManagerMKII.settings[250] = 5;
            }
            if ((num2 < 0) || (num2 > 10))
            {
                num2 = 5;
                FengGameManagerMKII.settings[0xfb] = 5;
            }
            if ((num3 < 0) || (num3 > 10))
            {
                num3 = 5;
                FengGameManagerMKII.settings[0xfc] = 5;
            }
            if ((num4 < 0) || (num4 > 10))
            {
                num4 = 5;
                FengGameManagerMKII.settings[0xfd] = 5;
            }
            if ((((num + num2) + num3) + num4) > 20)
            {
                num = 5;
                num2 = 5;
                num3 = 5;
                num4 = 5;
                FengGameManagerMKII.settings[250] = 5;
                FengGameManagerMKII.settings[0xfb] = 5;
                FengGameManagerMKII.settings[0xfc] = 5;
                FengGameManagerMKII.settings[0xfd] = 5;
            }
            this.bombTimeMax = ((num2 * 60f) + 200f) / ((num3 * 60f) + 200f);
            this.bombRadius = (num * 4f) + 20f;
            this.bombCD = (num4 * -0.4f) + 5f;
            this.bombSpeed = (num3 * 60f) + 200f;
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            //if (FengGameManagerMKII.RandomizeBombColor)
            //{
            //    System.Random rnd = new System.Random();
            //ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            //propertiesToSet.Add(PhotonPlayerProperty.RCBombR, (float)rnd.Next(0, 1) / 100f);
            //propertiesToSet.Add(PhotonPlayerProperty.RCBombG, (float)rnd.Next(0, 1) / 100f);
            //propertiesToSet.Add(PhotonPlayerProperty.RCBombB, (float)rnd.Next(0, 1) / 100f);
            //propertiesToSet.Add(PhotonPlayerProperty.RCBombA, (float)rnd.Next(0, 1) / 100f);
            //PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            //}
            //else
            //{
            propertiesToSet.Add(PhotonPlayerProperty.RCBombR, (float)FengGameManagerMKII.settings[0xf6]);
            propertiesToSet.Add(PhotonPlayerProperty.RCBombG, (float)FengGameManagerMKII.settings[0xf7]);
            propertiesToSet.Add(PhotonPlayerProperty.RCBombB, (float)FengGameManagerMKII.settings[0xf8]);
            propertiesToSet.Add(PhotonPlayerProperty.RCBombA, (float)FengGameManagerMKII.settings[0xf9]);
            //}


            propertiesToSet.Add(PhotonPlayerProperty.RCBombRadius, this.bombRadius);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            this.skillId = "bomb";
            this.skillIDHUD = "armin";
            this.skillCDLast = this.bombCD;
            this.skillCDDuration = 10f;
            if (FengGameManagerMKII.instance.roundTime > 10f)
            {
                this.skillCDDuration = 5f;
            }
        }
    }

    //private void breakApart(Vector3 v, bool isBite)
    //{
    //    GameObject obj6;
    //    GameObject obj7;
    //    GameObject obj8;
    //    GameObject obj9;
    //    GameObject obj10;
    //    GameObject obj2 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
    //    obj2.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
    //    obj2.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_R);
    //    if (!isBite)
    //    {
    //        GameObject gO = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
    //        GameObject obj4 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
    //        GameObject obj5 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
    //        gO.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
    //        obj4.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
    //        obj5.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
    //        gO.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.UPPER);
    //        obj4.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.LOWER);
    //        obj5.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_L);
    //        this.applyForceToBody(gO, v);
    //        this.applyForceToBody(obj4, v);
    //        this.applyForceToBody(obj5, v);
    //        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
    //        {
    //            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(gO, false, false);
    //        }
    //    }
    //    else if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
    //    {
    //        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(obj2, false, false);
    //    }
    //    this.applyForceToBody(obj2, v);
    //    Transform transform = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L").transform;
    //    Transform transform2 = baseT.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R").transform;
    //    if (this.useGun)
    //    {
    //        obj6 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
    //        obj7 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_r"), transform2.position, transform2.rotation);
    //        obj8 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_3dmg_2"), baseT.position, baseT.rotation);
    //        obj9 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_mag_l"), baseT.position, baseT.rotation);
    //        obj10 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_mag_r"), baseT.position, baseT.rotation);
    //    }
    //    else
    //    {
    //        obj6 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
    //        obj7 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
    //        obj8 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_3dmg"), baseT.position, baseT.rotation);
    //        obj9 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_3dmg_gas_l"), baseT.position, baseT.rotation);
    //        obj10 = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_3dmg_gas_r"), baseT.position, baseT.rotation);
    //    }
    //    obj6.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
    //    obj7.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
    //    obj8.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
    //    obj9.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
    //    obj10.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
    //    this.applyForceToBody(obj6, v);
    //    this.applyForceToBody(obj7, v);
    //    this.applyForceToBody(obj8, v);
    //    this.applyForceToBody(obj9, v);
    //    this.applyForceToBody(obj10, v);
    //}

    private void breakApart(Vector3 v, bool isBite)
    {
        //if (this.isBait)
        //{
        //    return;
        //}
        //GameObject obj6;
        //GameObject obj7;
        //GameObject obj8;
        //GameObject obj9;
        //GameObject obj10;
        base.RecheckTransforms();

        GameObject go = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
        go.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
        go.gameObject.GetComponent<HERO_SETUP>().isDeadBody = true;
        go.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_R);
        if (!isBite)
        {
            GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
            GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
            GameObject gameObject4 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/AOTTG_HERO_body"), baseT.position, baseT.rotation);
            gameObject2.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            gameObject3.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            gameObject4.gameObject.GetComponent<HERO_SETUP>().myCostume = this.setup.myCostume;
            gameObject2.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.UPPER);
            gameObject3.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.LOWER);
            gameObject4.GetComponent<HERO_DEAD_BODY_SETUP>().init(this.currentAnimation, baseA[this.currentAnimation].normalizedTime, BODY_PARTS.ARM_L);
            this.applyForceToBody(gameObject2, v);
            this.applyForceToBody(gameObject3, v);
            this.applyForceToBody(gameObject4, v);
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
            {
                IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(gameObject2, false, false);
            }
        }
        else if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
        {
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(go, false, false);
        }
        this.applyForceToBody(go, v);
        GameObject gameObject5;
        GameObject gameObject6;
        GameObject gameObject7;
        GameObject gameObject8;
        GameObject gameObject9;
        if (this.useGun)
        {
            gameObject5 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_gun_l"), hand_L.position, hand_L.rotation);
            gameObject6 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_gun_r"), hand_R.position, hand_R.rotation);
            gameObject7 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_3dmg_2"), baseT.position, baseT.rotation);
            gameObject8 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_gun_mag_l"), baseT.position, baseT.rotation);
            gameObject9 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_gun_mag_r"), baseT.position, baseT.rotation);
        }
        else
        {
            gameObject5 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_blade_l"), hand_L.position, hand_L.rotation);
            gameObject6 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_blade_r"), hand_R.position, hand_R.rotation);
            gameObject7 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_3dmg"), baseT.position, baseT.rotation);
            gameObject8 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_3dmg_gas_l"), baseT.position, baseT.rotation);
            gameObject9 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load("Character_parts/character_3dmg_gas_r"), baseT.position, baseT.rotation);
        }
        gameObject5.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        gameObject6.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        gameObject7.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        gameObject8.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        gameObject9.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        this.applyForceToBody(gameObject5, v);
        this.applyForceToBody(gameObject6, v);
        this.applyForceToBody(gameObject7, v);
        this.applyForceToBody(gameObject8, v);
        this.applyForceToBody(gameObject9, v);

    }

    private void bufferUpdate()
    {
        if (this.buffTime > 0f)
        {
            this.buffTime -= Time.deltaTime;
            if (this.buffTime <= 0f)
            {
                this.buffTime = 0f;
                if ((this.currentBuff == BUFF.SpeedUp) && baseA.IsPlaying("run_sasha"))
                {
                    this.crossFade("run", 0.1f);
                }
                this.currentBuff = BUFF.NoBuff;
            }
        }
    }

    public void cache()
    {
        base.CacheComponnents();
        base.CacheTransforms();
        //baseT = baseT;
        //baseR = baseR;
        this.sync = base.GetComponent<SmoothSyncMovement>();
        this.capsule = base.GetComponent<CapsuleCollider>();
        this.hookRefL1T = this.hookRefL1.transform;
        this.hookRefL2T = this.hookRefL2.transform;
        this.hookRefR1T = this.hookRefR1.transform;
        this.hookRefR2T = this.hookRefR2.transform;
        //this.maincamera = CacheGameObject.Find("MainCamera");
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
        {
            //baseA= baseA;
            this.cross1 = CacheGameObject.Find("cross1");
            this.cross2 = CacheGameObject.Find("cross2");
            this.crossT1 = CacheGameObject.Find<Transform>("cross1");
            this.crossT2 = CacheGameObject.Find<Transform>("cross2");
            this.crossL1 = CacheGameObject.Find("crossL1");
            this.crossL2 = CacheGameObject.Find("crossL2");
            this.crossR1 = CacheGameObject.Find("crossR1");
            this.crossR2 = CacheGameObject.Find("crossR2");
            //this.bladeCL = CacheGameObject.Find<UISprite>("bladeCL");
            //this.bladeCR = CacheGameObject.Find<UISprite>("bladeCR");
            //this.bulletL = CacheGameObject.Find<UISprite>("bulletL");
            //this.bulletR = CacheGameObject.Find<UISprite>("bulletR");
            this.crossL1T = this.crossL1.transform;
            this.crossL2T = this.crossL2.transform;
            this.crossR1T = this.crossR1.transform;
            this.crossR2T = this.crossR2.transform;
            this.LabelDistance = CacheGameObject.Find<UILabel>("LabelDistance");
            this.labelT = this.LabelDistance.transform;
            this.flare1 = CacheGameObject.Find<UISprite>("UIflare1");
            this.flare2 = CacheGameObject.Find<UISprite>("UIflare2");
            this.flare3 = CacheGameObject.Find<UISprite>("UIflare3");
            this.cachedSprites = new Dictionary<string, UISprite>();
            foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if ((obj2.GetComponent<UISprite>() != null) && obj2.activeInHierarchy)
                {
                    string name = obj2.name;
                    if (!(((!name.Contains("blade") && !name.Contains("bullet")) && ((!name.Contains("gas") && !name.Contains("flare")) && !name.Contains("skill_cd"))) || this.cachedSprites.ContainsKey(name)))
                    {
                        this.cachedSprites.Add(name, obj2.GetComponent<UISprite>());
                    }
                }
            }
        }
    }

    private void calcFlareCD()
    {
        if (this.flare1CD > 0f)
        {
            this.flare1CD -= Time.deltaTime;
            if (this.flare1CD < 0f)
            {
                this.flare1CD = 0f;
            }
        }
        if (this.flare2CD > 0f)
        {
            this.flare2CD -= Time.deltaTime;
            if (this.flare2CD < 0f)
            {
                this.flare2CD = 0f;
            }
        }
        if (this.flare3CD > 0f)
        {
            this.flare3CD -= Time.deltaTime;
            if (this.flare3CD < 0f)
            {
                this.flare3CD = 0f;
            }
        }
    }

    private void calcSkillCD()
    {
        if (this.skillCDDuration > 0f)
        {
            this.skillCDDuration -= Time.deltaTime;
            if (this.skillCDDuration < 0f)
            {
                this.skillCDDuration = 0f;
            }
        }
    }

    private float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt((2f * this.jumpHeight) * this.gravity);
    }

    private void changeBlade()
    {
        if ((!this.useGun || this.grounded) || (LevelInfo.getInfo(FengGameManagerMKII.level).type != GAMEMODE.PVP_AHSS))
        {
            this.state = HERO_STATE.ChangeBlade;
            this.throwedBlades = false;
            if (this.useGun)
            {
                if (!this.leftGunHasBullet && !this.rightGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both_air";
                    }
                }
                else if (!this.leftGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_l";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_l_air";
                    }
                }
                else if (!this.rightGunHasBullet)
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_r";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_r_air";
                    }
                }
                else
                {
                    if (this.grounded)
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both";
                    }
                    else
                    {
                        this.reloadAnimation = "AHSS_gun_reload_both_air";
                    }
                    this.leftGunHasBullet = this.rightGunHasBullet = false;
                }
                this.crossFade(this.reloadAnimation, 0.05f);
            }
            else
            {
                if (!this.grounded)
                {
                    this.reloadAnimation = "changeBlade_air";
                }
                else
                {
                    this.reloadAnimation = "changeBlade";
                }
                this.crossFade(this.reloadAnimation, 0.1f);
            }
        }
    }

    private void checkDashDoubleTap()
    {
        if (this.uTapTime >= 0f)
        {
            this.uTapTime += Time.deltaTime;
            if (this.uTapTime > 0.2f)
            {
                this.uTapTime = -1f;
            }
        }
        if (this.dTapTime >= 0f)
        {
            this.dTapTime += Time.deltaTime;
            if (this.dTapTime > 0.2f)
            {
                this.dTapTime = -1f;
            }
        }
        if (this.lTapTime >= 0f)
        {
            this.lTapTime += Time.deltaTime;
            if (this.lTapTime > 0.2f)
            {
                this.lTapTime = -1f;
            }
        }
        if (this.rTapTime >= 0f)
        {
            this.rTapTime += Time.deltaTime;
            if (this.rTapTime > 0.2f)
            {
                this.rTapTime = -1f;
            }
        }
        if (FengCustomInputs.Inputs.isInputDown[InputCode.up])
        {
            if (this.uTapTime == -1f)
            {
                this.uTapTime = 0f;
            }
            if (this.uTapTime != 0f)
            {
                this.dashU = true;
            }
        }
        if (FengCustomInputs.Inputs.isInputDown[InputCode.down])
        {
            if (this.dTapTime == -1f)
            {
                this.dTapTime = 0f;
            }
            if (this.dTapTime != 0f)
            {
                this.dashD = true;
            }
        }
        if (FengCustomInputs.Inputs.isInputDown[InputCode.left])
        {
            if (this.lTapTime == -1f)
            {
                this.lTapTime = 0f;
            }
            if (this.lTapTime != 0f)
            {
                this.dashL = true;
            }
        }
        if (FengCustomInputs.Inputs.isInputDown[InputCode.right])
        {
            if (this.rTapTime == -1f)
            {
                this.rTapTime = 0f;
            }
            if (this.rTapTime != 0f)
            {
                this.dashR = true;
            }
        }
    }

    private void checkDashRebind()
    {
        if (FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.dash))
        {
            if (FengCustomInputs.Inputs.isInput[InputCode.up])
            {
                this.dashU = true;
            }
            else if (FengCustomInputs.Inputs.isInput[InputCode.down])
            {
                this.dashD = true;
            }
            else if (FengCustomInputs.Inputs.isInput[InputCode.left])
            {
                this.dashL = true;
            }
            else if (FengCustomInputs.Inputs.isInput[InputCode.right])
            {
                this.dashR = true;
            }
        }
    }

    public void checkTitan()
    {
        int count;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = ((int)1) << LayerMask.NameToLayer("PlayerAttackBox");
        LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("Ground");
        LayerMask mask3 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        LayerMask mask4 = (mask | mask2) | mask3;
        RaycastHit[] hitArray = Physics.RaycastAll(ray, 180f, mask4.value);
        List<RaycastHit> list = new List<RaycastHit>();
        List<TITAN> list2 = new List<TITAN>();
        for (count = 0; count < hitArray.Length; count++)
        {
            RaycastHit item = hitArray[count];
            list.Add(item);
        }
        list.Sort((x, y) => x.distance.CompareTo(y.distance));
        float num2 = 180f;
        for (count = 0; count < list.Count; count++)
        {
            RaycastHit hit2 = list[count];
            GameObject gameObject = hit2.collider.gameObject;
            if (gameObject.layer == 0x10)
            {
                if (gameObject.name.Contains("PlayerDetectorRC") && ((hit2 = list[count]).distance < num2))
                {
                    num2 -= 60f;
                    if (num2 <= 60f)
                    {
                        count = list.Count;
                    }
                    TITAN component = gameObject.transform.root.gameObject.GetComponent<TITAN>();
                    if (component != null)
                    {
                        list2.Add(component);
                    }
                }
            }
            else
            {
                count = list.Count;
            }
        }
        for (count = 0; count < this.myTitans.Count; count++)
        {
            TITAN titan2 = this.myTitans[count];
            if (!list2.Contains(titan2))
            {
                titan2.isLook = false;
            }
        }
        for (count = 0; count < list2.Count; count++)
        {
            TITAN titan3 = list2[count];
            titan3.isLook = true;
        }
        this.myTitans = list2;
    }

    public void ClearPopup()
    {
        FengGameManagerMKII.instance.ShowHUDInfoCenter(string.Empty);
    }

    public void continueAnimation()
    {
        if (!this.netPauseStopped) return;
        this.netPauseStopped = false;
        foreach (object obj in this.baseA)
        {
            AnimationState current = (AnimationState)obj;
            if (current.speed == 1f)
            {
                return;
            }
            current.speed = 1f;
        }
        this.customAnimationSpeed();
        this.playAnimation(this.currentPlayingClipName());
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && this.basePV.isMine)
        {
            this.basePV.RPC("netContinueAnimation", PhotonTargets.Others, new object[0]);
        }
    }
    


    public void crossFade(string aniName, float time)
    {
        this.currentAnimation = aniName;
        baseA.CrossFade(aniName, time);
        if (PhotonNetwork.connected && basePV.isMine)
        {
            object[] parameters = new object[] { aniName, time };
            basePV.RPC("netCrossFade", PhotonTargets.Others, parameters);
        }
    }

    public string currentPlayingClipName()
    {
        foreach (AnimationState animationState in baseA)
        {
            if (baseA.IsPlaying(animationState.name))
            {
                return animationState.name;
            }
        }
        return string.Empty;
    }


    private void customAnimationSpeed()
    {
        baseA["attack5"].speed = 1.85f;
        baseA["changeBlade"].speed = 1.2f;
        baseA["air_release"].speed = 0.6f;
        baseA["changeBlade_air"].speed = 0.8f;
        baseA["AHSS_gun_reload_both"].speed = 0.38f;
        baseA["AHSS_gun_reload_both_air"].speed = 0.5f;
        baseA["AHSS_gun_reload_l"].speed = 0.4f;
        baseA["AHSS_gun_reload_l_air"].speed = 0.5f;
        baseA["AHSS_gun_reload_r"].speed = 0.4f;
        baseA["AHSS_gun_reload_r_air"].speed = 0.5f;
    }

    private void dash(float horizontal, float vertical)
    {
        if (((this.dashTime <= 0f) && (this.currentGas > 0f)) && !this.isMounted)
        {
            this.useGas(this.totalGas * 0.04f);
            this.facingDirection = this.getGlobalFacingDirection(horizontal, vertical);
            this.dashV = this.getGlobaleFacingVector3(this.facingDirection);
            this.originVM = this.currentSpeed;
            Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
            baseR.rotation = quaternion;
            this.targetRotation = quaternion;
            UnityEngine.Object.Instantiate(CacheResources.Load("FX/boost_smoke"), baseT.position, baseT.rotation);
          //  PhotonNetwork.Instantiate("FX/boost_smoke", base.transform.position, base.transform.rotation, 0);
            this.dashTime = 0.5f;
            this.crossFade("dash", 0.1f);
            baseA["dash"].time = 0.1f;
            this.state = HERO_STATE.AirDodge;
            this.falseAttack();
            baseR.AddForce((Vector3)(this.dashV * 40f), ForceMode.VelocityChange);
        }
    }

    public void die(Vector3 v, bool isBite)
    {
        if (this.invincible <= 0f)
        {
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.bulletLeft != null)
            {
                this.LBullet.removeMe();
            }
            if (this.bulletRight != null)
            {
                this.RBullet.removeMe();
            }
            this.meatDie.Play();
            if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine) && !this.useGun)
            {
                this.leftbladetrail.Deactivate();
                this.rightbladetrail.Deactivate();
                this.leftbladetrail2.Deactivate();
                this.rightbladetrail2.Deactivate();
            }
            this.breakApart(v, isBite);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            FengGameManagerMKII.instance.gameLose2();
            this.falseAttack();
            this.hasDied = true;
            Transform transform = baseT.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
            if (PlayerPrefs.HasKey("EnableSS") && (PlayerPrefs.GetInt("EnableSS") == 1))
            {
                IN_GAME_MAIN_CAMERA.mainCamera.startSnapShot2(baseT.position, 0, null, 0.02f);
            }
            UnityEngine.Object.Destroy(baseG);
        }
    }

    public void die2(Transform tf)
    {
        if (this.invincible <= 0f)
        {
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.bulletLeft != null)
            {
                this.LBullet.removeMe();
            }
            if (this.bulletRight != null)
            {
                this.RBullet.removeMe();
            }
            Transform transform = baseT.Find("audio_die");
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
            this.meatDie.Play();
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            FengGameManagerMKII.instance.gameLose2();
            this.falseAttack();
            this.hasDied = true;
            GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("hitMeat2"));
            obj2.transform.position = baseT.position;
            UnityEngine.Object.Destroy(baseG);
        }
    }

    
    private void dodge2(bool offTheWall = false)
    {
        if (((!FengGameManagerMKII.inputRC.isInputHorse(InputCodeRC.horseMount) || (this.myHorse == null)) || this.isMounted) || (Vector3.Distance(this.myHorse.transform.position, baseT.position) >= 15f))
        {
            this.state = HERO_STATE.GroundDodge;
            if (!offTheWall)
            {
                float num;
                float num2;
                if (FengCustomInputs.Inputs.isInput[InputCode.up])
                {
                    num = 1f;
                }
                else if (FengCustomInputs.Inputs.isInput[InputCode.down])
                {
                    num = -1f;
                }
                else
                {
                    num = 0f;
                }
                if (FengCustomInputs.Inputs.isInput[InputCode.left])
                {
                    num2 = -1f;
                }
                else if (FengCustomInputs.Inputs.isInput[InputCode.right])
                {
                    num2 = 1f;
                }
                else
                {
                    num2 = 0f;
                }
                float num3 = this.getGlobalFacingDirection(num2, num);
                if ((num2 != 0f) || (num != 0f))
                {
                    this.facingDirection = num3 + 180f;
                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                }
                this.crossFade("dodge", 0.1f);
            }
            else
            {
                this.playAnimation("dodge");
                this.playAnimationAt("dodge", 0.2f);
            }
            this.sparks.enableEmission = false;
        }
    }

    private void erenTransform()
    {
        this.skillCDDuration = this.skillCDLast;
        if (this.bulletLeft != null)
        {
            this.LBullet.removeMe();
        }
        if (this.bulletRight != null)
        {
            this.RBullet.removeMe();
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            this.eren_titan = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("TITAN_EREN"), baseT.position, baseT.rotation);
        }
        else
        {
            this.eren_titan = PhotonNetwork.Instantiate("TITAN_EREN", baseT.position, baseT.rotation, 0);
        }
        this.eren_titan.GetComponent<TITAN_EREN>().realBody = baseG;
        IN_GAME_MAIN_CAMERA.mainCamera.flashBlind();
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(this.eren_titan, true, false);
        this.eren_titan.GetComponent<TITAN_EREN>().born();
        this.eren_titan.rigidbody.velocity = baseR.velocity;
        baseR.velocity = Vector3.zero;
        baseT.position = this.eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
        this.titanForm = true;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            object[] parameters = new object[] { this.eren_titan.GetPhotonView().viewID };
            basePV.RPC("whoIsMyErenTitan", PhotonTargets.Others, parameters);
        }
        if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && basePV.isMine)
        {
            object[] objArray2 = new object[] { false };
            basePV.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray2);
        }
        this.smoke_3dmg.enableEmission = false;
    }

    private void escapeFromGrab()
    {
    }

    public void falseAttack()
    {
        this.attackMove = false;
        if (this.useGun)
        {
            if (!this.attackReleased)
            {
                this.continueAnimation();
                this.attackReleased = true;
            }
        }
        else
        {
            if (base.isLocal)
            {
                this.triggerLeft.active_me = false;
                this.triggerRight.active_me = false;
                this.triggerLeft.clearHits();
                this.triggerRight.clearHits();
                this.leftbladetrail.StopSmoothly(0.2f);
                this.rightbladetrail.StopSmoothly(0.2f);
                this.leftbladetrail2.StopSmoothly(0.2f);
                this.rightbladetrail2.StopSmoothly(0.2f);
            }
            this.attackLoop = 0;
            if (!this.attackReleased)
            {
                this.continueAnimation();
                this.attackReleased = true;
            }
        }
    }

    public void fillGas()
    {
        this.currentGas = this.totalGas;
    }

    private GameObject findNearestTitan()
    {
        GameObject[] objArray = GameObject.FindGameObjectsWithTag("titan");
        GameObject obj2 = null;
        float positiveInfinity = float.PositiveInfinity;
        Vector3 position = baseT.position;
        foreach (GameObject obj3 in objArray)
        {
            Vector3 vector2 = obj3.transform.position - position;
            float sqrMagnitude = vector2.sqrMagnitude;
            if (sqrMagnitude < positiveInfinity)
            {
                obj2 = obj3;
                positiveInfinity = sqrMagnitude;
            }
        }
        return obj2;
    }

    private void FixedUpdate()
    {
        //GameObject dmgsmoke = new GameObject();
        if ((!this.titanForm && !this.isCannon) && (!IN_GAME_MAIN_CAMERA.isPausing || (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)))
        {
            this.currentSpeed = baseR.velocity.magnitude;
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
            {
                if (!((baseA.IsPlaying("attack3_2") || baseA.IsPlaying("attack5")) || baseA.IsPlaying("special_petra")))
                {
                    baseR.rotation = Quaternion.Lerp(baseG.transform.rotation, this.targetRotation, Time.deltaTime * 6f);
                }
                if (this.state == HERO_STATE.Grab)
                {
                    baseR.AddForce(-baseR.velocity, ForceMode.VelocityChange);
                }
                else
                {
                    if (this.IsGrounded())
                    {
                        if (!this.grounded)
                        {
                            this.justGrounded = true;
                        }
                        this.grounded = true;
                    }
                    else
                    {
                        this.grounded = false;
                    }
                    if (this.hookSomeOne)
                    {
                        if (this.hookTarget != null)
                        {
                            Vector3 vector2 = this.hookTarget.transform.position - baseT.position;
                            float magnitude = vector2.magnitude;
                            if (magnitude > 2f)
                            {
                                baseR.AddForce((Vector3)(((vector2.normalized * Mathf.Pow(magnitude, 0.15f)) * 30f) - (baseR.velocity * 0.95f)), ForceMode.VelocityChange);
                            }
                        }
                        else
                        {
                            this.hookSomeOne = false;
                        }
                    }
                    else if (this.hookBySomeOne && (this.badGuy != null))
                    {
                        if (this.badGuy != null)
                        {
                            Vector3 vector3 = this.badGuy.transform.position - baseT.position;
                            float f = vector3.magnitude;
                            if (f > 5f)
                            {
                                baseR.AddForce((Vector3)((vector3.normalized * Mathf.Pow(f, 0.15f)) * 0.2f), ForceMode.Impulse);
                            }
                        }
                        else
                        {
                            this.hookBySomeOne = false;
                        }
                    }
                    float x = 0f;
                    float z = 0f;
                    if (!IN_GAME_MAIN_CAMERA.isTyping)
                    {
                        if (FengCustomInputs.Inputs.isInput[InputCode.up])
                        {
                            z = 1f;
                        }
                        else if (FengCustomInputs.Inputs.isInput[InputCode.down])
                        {
                            z = -1f;
                        }
                        else
                        {
                            z = 0f;
                        }
                        if (FengCustomInputs.Inputs.isInput[InputCode.left])
                        {
                            x = -1f;
                        }
                        else if (FengCustomInputs.Inputs.isInput[InputCode.right])
                        {
                            x = 1f;
                        }
                        else
                        {
                            x = 0f;
                        }
                    }
                    bool flag2 = false;
                    bool flag3 = false;
                    bool flag4 = false;
                    this.isLeftHandHooked = false;
                    this.isRightHandHooked = false;
                    if (this.isLaunchLeft)
                    {
                        if ((this.bulletLeft != null) && this.LBullet.isHooked())
                        {
                            this.isLeftHandHooked = true;
                            Vector3 to = this.bulletLT.position - baseT.position;
                            to.Normalize();
                            to = (Vector3)(to * 10f);
                            if (!this.isLaunchRight)
                            {
                                to = (Vector3)(to * 2f);
                            }
                            if ((Vector3.Angle(baseR.velocity, to) > 90f) && FengCustomInputs.Inputs.isInput[InputCode.jump])
                            {
                                flag3 = true;
                                flag2 = true;
                            }
                            if (!flag3)
                            {
                                baseR.AddForce(to);
                                if (Vector3.Angle(baseR.velocity, to) > 90f)
                                {
                                    baseR.AddForce((Vector3)(-baseR.velocity * 2f), ForceMode.Acceleration);
                                }
                            }
                        }
                        this.launchElapsedTimeL += Time.deltaTime;
                        if (this.QHold && (this.currentGas > 0f))
                        {
                            this.useGas(this.useGasSpeed * Time.deltaTime);
                        }
                        else if (this.launchElapsedTimeL > 0.3f)
                        {
                            this.isLaunchLeft = false;
                            if (this.bulletLeft != null)
                            {
                                this.LBullet.disable();
                                this.releaseIfIHookSb();
                                this.bulletLeft = null;
                                this.bulletLT = null;
                                this.LBullet = null;
                                flag3 = false;
                            }
                        }
                    }
                    if (this.isLaunchRight)
                    {
                        if ((this.bulletRight != null) && this.RBullet.isHooked())
                        {
                            this.isRightHandHooked = true;
                            Vector3 vector5 = this.bulletRT.position - baseT.position;
                            vector5.Normalize();
                            vector5 = (Vector3)(vector5 * 10f);
                            if (!this.isLaunchLeft)
                            {
                                vector5 = (Vector3)(vector5 * 2f);
                            }
                            if ((Vector3.Angle(baseR.velocity, vector5) > 90f) && FengCustomInputs.Inputs.isInput[InputCode.jump])
                            {
                                flag4 = true;
                                flag2 = true;
                            }
                            if (!flag4)
                            {
                                baseR.AddForce(vector5);
                                if (Vector3.Angle(baseR.velocity, vector5) > 90f)
                                {
                                    baseR.AddForce((Vector3)(-baseR.velocity * 2f), ForceMode.Acceleration);
                                }
                            }
                        }
                        this.launchElapsedTimeR += Time.deltaTime;
                        if (this.EHold && (this.currentGas > 0f))
                        {
                            this.useGas(this.useGasSpeed * Time.deltaTime);
                        }
                        else if (this.launchElapsedTimeR > 0.3f)
                        {
                            this.isLaunchRight = false;
                            if (this.bulletRight != null)
                            {
                                this.RBullet.disable();
                                this.releaseIfIHookSb();
                                this.bulletRight = null;
                                this.bulletRT = null;
                                this.RBullet = null;
                                flag4 = false;
                            }
                        }
                    }
                    if (this.grounded)
                    {
                        Vector3 vector7;
                        Vector3 zero = Vector3.zero;
                        if (this.state == HERO_STATE.Attack)
                        {
                            if (this.attackAnimation == "attack5")
                            {
                                if ((baseA[this.attackAnimation].normalizedTime > 0.4f) && (baseA[this.attackAnimation].normalizedTime < 0.61f))
                                {
                                    baseR.AddForce((Vector3)(baseG.transform.forward * 200f));
                                }
                            }
                            else if (this.attackAnimation == "special_petra")
                            {
                                if ((baseA[this.attackAnimation].normalizedTime > 0.35f) && (baseA[this.attackAnimation].normalizedTime < 0.48f))
                                {
                                    baseR.AddForce((Vector3)(baseG.transform.forward * 200f));
                                }
                            }
                            else if (baseA.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                            else if (baseA.IsPlaying("attack1") || baseA.IsPlaying("attack2"))
                            {
                                baseR.AddForce((Vector3)(baseG.transform.forward * 200f));
                            }
                            if (baseA.IsPlaying("attack3_2"))
                            {
                                zero = Vector3.zero;
                            }
                        }
                        if (this.justGrounded)
                        {
                            if ((this.state != HERO_STATE.Attack) || (((this.attackAnimation != "attack3_1") && (this.attackAnimation != "attack5")) && (this.attackAnimation != "special_petra")))
                            {
                                if ((((this.state != HERO_STATE.Attack) && (x == 0f)) && ((z == 0f) && (this.bulletLeft == null))) && ((this.bulletRight == null) && (this.state != HERO_STATE.FillGas)))
                                {
                                    this.state = HERO_STATE.Land;
                                    this.crossFade("dash_land", 0.01f);
                                }
                                else
                                {
                                    this.buttonAttackRelease = true;
                                    if (((this.state != HERO_STATE.Attack) && (((baseR.velocity.x * baseR.velocity.x) + (baseR.velocity.z * baseR.velocity.z)) > ((this.speed * this.speed) * 1.5f))) && (this.state != HERO_STATE.FillGas))
                                    {
                                        this.state = HERO_STATE.Slide;
                                        this.crossFade("slide", 0.05f);
                                        this.facingDirection = Mathf.Atan2(baseR.velocity.x, baseR.velocity.z) * 57.29578f;
                                        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                        this.sparks.enableEmission = true;
                                    }
                                }
                            }
                            this.justGrounded = false;
                            zero = baseR.velocity;
                        }
                        if (((this.state == HERO_STATE.Attack) && (this.attackAnimation == "attack3_1")) && (baseA[this.attackAnimation].normalizedTime >= 1f))
                        {
                            this.playAnimation("attack3_2");
                            this.resetAnimationSpeed();
                            vector7 = Vector3.zero;
                            baseR.velocity = vector7;
                            zero = vector7;
                            IN_GAME_MAIN_CAMERA.mainCamera.startShake(0.2f, 0.3f, 0.95f);
                        }
                        if (this.state == HERO_STATE.GroundDodge)
                        {
                            if ((baseA["dodge"].normalizedTime >= 0.2f) && (baseA["dodge"].normalizedTime < 0.8f))
                            {
                                zero = (Vector3)((-baseT.forward * 2.4f) * this.speed);
                            }
                            if (baseA["dodge"].normalizedTime > 0.8f)
                            {
                                zero = (Vector3)(baseR.velocity * 0.9f);
                            }
                        }
                        else if (this.state == HERO_STATE.Idle)
                        {
                            Vector3 vector8 = new Vector3(x, 0f, z);
                            float resultAngle = this.getGlobalFacingDirection(x, z);
                            zero = this.getGlobaleFacingVector3(resultAngle);
                            float num6 = (vector8.magnitude <= 0.95f) ? ((vector8.magnitude >= 0.25f) ? vector8.magnitude : 0f) : 1f;
                            zero = (Vector3)(zero * num6);
                            zero = (Vector3)(zero * this.speed);
                            if ((this.buffTime > 0f) && (this.currentBuff == BUFF.SpeedUp))
                            {
                                zero = (Vector3)(zero * 4f);
                            }
                            if ((x != 0f) || (z != 0f))
                            {
                                if (((!baseA.IsPlaying("run") && !baseA.IsPlaying("jump")) && !baseA.IsPlaying("run_sasha")) && (!baseA.IsPlaying("horse_geton") || (baseA["horse_geton"].normalizedTime >= 0.5f)))
                                {
                                    if ((this.buffTime > 0f) && (this.currentBuff == BUFF.SpeedUp))
                                    {
                                        this.crossFade("run_sasha", 0.1f);
                                    }
                                    else
                                    {
                                        this.crossFade("run", 0.1f);
                                    }
                                }
                            }
                            else
                            {
                                if (!(((baseA.IsPlaying(this.standAnimation) || (this.state == HERO_STATE.Land)) || (baseA.IsPlaying("jump") || baseA.IsPlaying("horse_geton"))) || baseA.IsPlaying("grabbed")))
                                {
                                    this.crossFade(this.standAnimation, 0.1f);
                                    zero = (Vector3)(zero * 0f);
                                }
                                resultAngle = -874f;
                            }
                            if (resultAngle != -874f)
                            {
                                this.facingDirection = resultAngle;
                                this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                            }
                        }
                        else if (this.state == HERO_STATE.Land)
                        {
                            zero = (Vector3)(baseR.velocity * 0.96f);
                        }
                        else if (this.state == HERO_STATE.Slide)
                        {
                            zero = (Vector3)(baseR.velocity * 0.99f);
                            if (this.currentSpeed < (this.speed * 1.2f))
                            {
                                this.idle();
                                this.sparks.enableEmission = false;
                            }
                        }
                        Vector3 velocity = baseR.velocity;
                        Vector3 force = zero - velocity;
                        force.x = Mathf.Clamp(force.x, -this.maxVelocityChange, this.maxVelocityChange);
                        force.z = Mathf.Clamp(force.z, -this.maxVelocityChange, this.maxVelocityChange);
                        force.y = 0f;
                        if (baseA.IsPlaying("jump") && (baseA["jump"].normalizedTime > 0.18f))
                        {
                            force.y += 8f;
                        }
                        if ((baseA.IsPlaying("horse_geton") && (baseA["horse_geton"].normalizedTime > 0.18f)) && (baseA["horse_geton"].normalizedTime < 1f))
                        {
                            float num7 = 6f;
                            force = -baseR.velocity;
                            force.y = num7;
                            float num8 = Vector3.Distance(this.myHorse.transform.position, baseT.position);
                            float num9 = ((0.6f * this.gravity) * num8) / (2f * num7);
                            vector7 = this.myHorse.transform.position - baseT.position; //
                            force += (Vector3)(num9 * vector7.normalized);
                        }
                        if (!((this.state == HERO_STATE.Attack) && this.useGun))
                        {
                            baseR.AddForce(force, ForceMode.VelocityChange);
                            baseR.rotation = Quaternion.Lerp(baseG.transform.rotation, Quaternion.Euler(0f, this.facingDirection, 0f), Time.deltaTime * 10f);
                        }
                    }
                    else
                    {
                        if (this.sparks.enableEmission)
                        {
                            this.sparks.enableEmission = false;
                        }
                        if (((this.myHorse != null) && (baseA.IsPlaying("horse_geton") || baseA.IsPlaying("air_fall"))) && ((baseR.velocity.y < 0f) && (Vector3.Distance(this.myHorse.transform.position + ((Vector3)(Vector3.up * 1.65f)), baseT.position) < 0.5f)))
                        {
                            baseT.position = this.myHorse.transform.position + ((Vector3)(Vector3.up * 1.65f));
                            baseT.rotation = this.myHorse.transform.rotation;
                            this.isMounted = true;
                            this.crossFade("horse_idle", 0.1f);
                            this.myHorse.GetComponent<Horse>().mounted();
                        }
                        if ((((((this.state == HERO_STATE.Idle) && !baseA.IsPlaying("dash")) && (!baseA.IsPlaying("wallrun") && !baseA.IsPlaying("toRoof"))) && ((!baseA.IsPlaying("horse_geton") && !baseA.IsPlaying("horse_getoff")) && (!baseA.IsPlaying("air_release") && !this.isMounted))) && ((!baseA.IsPlaying("air_hook_l_just") || (baseA["air_hook_l_just"].normalizedTime >= 1f)) && (!baseA.IsPlaying("air_hook_r_just") || (baseA["air_hook_r_just"].normalizedTime >= 1f)))) || (baseA["dash"].normalizedTime >= 0.99f))
                        {
                            if (((!this.isLeftHandHooked && !this.isRightHandHooked) && ((baseA.IsPlaying("air_hook_l") || baseA.IsPlaying("air_hook_r")) || baseA.IsPlaying("air_hook"))) && (baseR.velocity.y > 20f))
                            {
                                baseA.CrossFade("air_release");
                            }
                            else
                            {
                                bool flag5 = (Mathf.Abs(baseR.velocity.x) + Mathf.Abs(baseR.velocity.z)) > 25f;
                                bool flag6 = baseR.velocity.y < 0f;
                                if (!flag5)
                                {
                                    if (flag6)
                                    {
                                        if (!baseA.IsPlaying("air_fall"))
                                        {
                                            this.crossFade("air_fall", 0.2f);
                                        }
                                    }
                                    else if (!baseA.IsPlaying("air_rise"))
                                    {
                                        this.crossFade("air_rise", 0.2f);
                                    }
                                }
                                else if (!this.isLeftHandHooked && !this.isRightHandHooked)
                                {
                                    float current = -Mathf.Atan2(baseR.velocity.z, baseR.velocity.x) * 57.29578f;
                                    float num11 = -Mathf.DeltaAngle(current, baseT.rotation.eulerAngles.y - 90f);
                                    if (Mathf.Abs(num11) < 45f)
                                    {
                                        if (!baseA.IsPlaying("air2"))
                                        {
                                            this.crossFade("air2", 0.2f);
                                        }
                                    }
                                    else if ((num11 < 135f) && (num11 > 0f))
                                    {
                                        if (!baseA.IsPlaying("air2_right"))
                                        {
                                            this.crossFade("air2_right", 0.2f);
                                        }
                                    }
                                    else if ((num11 > -135f) && (num11 < 0f))
                                    {
                                        if (!baseA.IsPlaying("air2_left"))
                                        {
                                            this.crossFade("air2_left", 0.2f);
                                        }
                                    }
                                    else if (!baseA.IsPlaying("air2_backward"))
                                    {
                                        this.crossFade("air2_backward", 0.2f);
                                    }
                                }
                                else if (this.useGun)
                                {
                                    if (!this.isRightHandHooked)
                                    {
                                        if (!baseA.IsPlaying("AHSS_hook_forward_l"))
                                        {
                                            this.crossFade("AHSS_hook_forward_l", 0.1f);
                                        }
                                    }
                                    else if (!this.isLeftHandHooked)
                                    {
                                        if (!baseA.IsPlaying("AHSS_hook_forward_r"))
                                        {
                                            this.crossFade("AHSS_hook_forward_r", 0.1f);
                                        }
                                    }
                                    else if (!baseA.IsPlaying("AHSS_hook_forward_both"))
                                    {
                                        this.crossFade("AHSS_hook_forward_both", 0.1f);
                                    }
                                }
                                else if (!this.isRightHandHooked)
                                {
                                    if (!baseA.IsPlaying("air_hook_l"))
                                    {
                                        this.crossFade("air_hook_l", 0.1f);
                                    }
                                }
                                else if (!this.isLeftHandHooked)
                                {
                                    if (!baseA.IsPlaying("air_hook_r"))
                                    {
                                        this.crossFade("air_hook_r", 0.1f);
                                    }
                                }
                                else if (!baseA.IsPlaying("air_hook"))
                                {
                                    this.crossFade("air_hook", 0.1f);
                                }
                            }
                        }
                        if (((this.state == HERO_STATE.Idle) && baseA.IsPlaying("air_release")) && (baseA["air_release"].normalizedTime >= 1f))
                        {
                            this.crossFade("air_rise", 0.2f);
                        }
                        if (baseA.IsPlaying("horse_getoff") && (baseA["horse_getoff"].normalizedTime >= 1f))
                        {
                            this.crossFade("air_rise", 0.2f);
                        }
                        if (baseA.IsPlaying("toRoof"))
                        {
                            if (baseA["toRoof"].normalizedTime < 0.22f)
                            {
                                baseR.velocity = Vector3.zero;
                                baseR.AddForce(new Vector3(0f, this.gravity * baseR.mass, 0f));
                            }
                            else
                            {
                                if (!this.wallJump)
                                {
                                    this.wallJump = true;
                                    baseR.AddForce((Vector3)(Vector3.up * 8f), ForceMode.Impulse);
                                }
                                baseR.AddForce((Vector3)(baseT.forward * 0.05f), ForceMode.Impulse);
                            }
                            if (baseA["toRoof"].normalizedTime >= 1f)
                            {
                                this.playAnimation("air_rise");
                            }
                        }
                        else if (!(((((this.state != HERO_STATE.Idle) || !this.isPressDirectionTowardsHero(x, z)) ||
                            (FengCustomInputs.Inputs.isInput[InputCode.jump] || FengCustomInputs.Inputs.isInput[InputCode.leftRope])) ||
                            ((FengCustomInputs.Inputs.isInput[InputCode.rightRope] || FengCustomInputs.Inputs.isInput[InputCode.bothRope]) ||
                            (!this.IsFrontGrounded() || baseA.IsPlaying("wallrun")))) ||
                            baseA.IsPlaying("dodge")))
                        {
                            this.crossFade("wallrun", 0.1f);
                            this.wallRunTime = 0f;
                        }
                        else if (baseA.IsPlaying("wallrun"))
                        {
                            baseR.AddForce(((Vector3)(Vector3.up * this.speed)) - baseR.velocity, ForceMode.VelocityChange);
                            this.wallRunTime += Time.deltaTime;
                            if ((this.wallRunTime > 1f) || ((z == 0f) && (x == 0f)))
                            {
                                baseR.AddForce((Vector3)((-baseT.forward * this.speed) * 0.75f), ForceMode.Impulse);
                                this.dodge2(true);
                            }
                            else if (!this.IsUpFrontGrounded())
                            {
                                this.wallJump = false;
                                this.crossFade("toRoof", 0.1f);
                            }
                            else if (!this.IsFrontGrounded())
                            {
                                this.crossFade("air_fall", 0.1f);
                            }
                        }
                        else if ((!baseA.IsPlaying("attack5") && !baseA.IsPlaying("special_petra")) && (!baseA.IsPlaying("dash") && !baseA.IsPlaying("jump")))
                        {
                            Vector3 vector11 = new Vector3(x, 0f, z);
                            float num12 = this.getGlobalFacingDirection(x, z);
                            Vector3 vector12 = this.getGlobaleFacingVector3(num12);
                            float num13 = (vector11.magnitude <= 0.95f) ? ((vector11.magnitude >= 0.25f) ? vector11.magnitude : 0f) : 1f;
                            vector12 = (Vector3)(vector12 * num13);
                            vector12 = (Vector3)(vector12 * ((((float)this.setup.myCostume.stat.ACL) / 10f) * 2f));
                            if ((x == 0f) && (z == 0f))
                            {
                                if (this.state == HERO_STATE.Attack)
                                {
                                    vector12 = (Vector3)(vector12 * 0f);
                                }
                                num12 = -874f;
                            }
                            if (num12 != -874f)
                            {
                                this.facingDirection = num12;
                                this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                            }
                            if (((!flag3 && !flag4) && (!this.isMounted && FengCustomInputs.Inputs.isInput[InputCode.jump])) && (this.currentGas > 0f))
                            {
                                if ((x != 0f) || (z != 0f))
                                {
                                    baseR.AddForce(vector12, ForceMode.Acceleration);
                                }
                                else
                                {
                                    baseR.AddForce((Vector3)(baseT.forward * vector12.magnitude), ForceMode.Acceleration);
                                }
                                flag2 = true;
                            }
                        }
                        if ((baseA.IsPlaying("air_fall") && (this.currentSpeed < 0.2f)) && this.IsFrontGrounded())
                        {
                            this.crossFade("onWall", 0.3f);
                        }
                    }
                    if (flag3 && flag4)
                    {
                        float num14 = this.currentSpeed + 0.1f;
                        baseR.AddForce(-baseR.velocity, ForceMode.VelocityChange);
                        Vector3 vector13 = ((Vector3)((this.bulletRT.position + this.bulletLT.position) * 0.5f)) - baseT.position;
                        float num15 = 0f;
                        if ((((int)FengGameManagerMKII.settings[0x61]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num15 = -1f;
                        }
                        else if ((((int)FengGameManagerMKII.settings[0x74]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num15 = 1f;
                        }
                        else
                        {
                            num15 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num15 = Mathf.Clamp(num15, -0.8f, 0.8f);
                        float num16 = 1f + num15;
                        Vector3 vector14 = Vector3.RotateTowards(vector13, baseR.velocity, 1.53938f * num16, 1.53938f * num16);
                        vector14.Normalize();
                        baseR.velocity = (Vector3)(vector14 * num14);
                    }
                    else if (flag3)
                    {
                        float num17 = this.currentSpeed + 0.1f;
                        baseR.AddForce(-baseR.velocity, ForceMode.VelocityChange);
                        Vector3 vector15 = this.bulletLT.position - baseT.position;
                        float num18 = 0f;
                        if ((((int)FengGameManagerMKII.settings[0x61]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num18 = -1f;
                        }
                        else if ((((int)FengGameManagerMKII.settings[0x74]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num18 = 1f;
                        }
                        else
                        {
                            num18 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num18 = Mathf.Clamp(num18, -0.8f, 0.8f);
                        float num19 = 1f + num18;
                        Vector3 vector16 = Vector3.RotateTowards(vector15, baseR.velocity, 1.53938f * num19, 1.53938f * num19);
                        vector16.Normalize();
                        baseR.velocity = (Vector3)(vector16 * num17);
                    }
                    else if (flag4)
                    {
                        float num20 = this.currentSpeed + 0.1f;
                        baseR.AddForce(-baseR.velocity, ForceMode.VelocityChange);
                        Vector3 vector17 = this.bulletRT.position - baseT.position;
                        float num21 = 0f;
                        if ((((int)FengGameManagerMKII.settings[0x61]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelin))
                        {
                            num21 = -1f;
                        }
                        else if ((((int)FengGameManagerMKII.settings[0x74]) == 1) && FengGameManagerMKII.inputRC.isInputHuman(InputCodeRC.reelout))
                        {
                            num21 = 1f;
                        }
                        else
                        {
                            num21 = Input.GetAxis("Mouse ScrollWheel") * 5555f;
                        }
                        num21 = Mathf.Clamp(num21, -0.8f, 0.8f);
                        float num22 = 1f + num21;
                        Vector3 vector18 = Vector3.RotateTowards(vector17, baseR.velocity, 1.53938f * num22, 1.53938f * num22);
                        vector18.Normalize();
                        baseR.velocity = (Vector3)(vector18 * num20);
                    }
                    if (((this.state == HERO_STATE.Attack) && ((this.attackAnimation == "attack5") || (this.attackAnimation == "special_petra"))) && ((baseA[this.attackAnimation].normalizedTime > 0.4f) && !this.attackMove))
                    {
                        this.attackMove = true;
                        if (this.launchPointRight.magnitude > 0f)
                        {
                            Vector3 vector19 = this.launchPointRight - baseT.position;
                            vector19.Normalize();
                            vector19 = (Vector3)(vector19 * 13f);
                            baseR.AddForce(vector19, ForceMode.Impulse);
                        }
                        if ((this.attackAnimation == "special_petra") && (this.launchPointLeft.magnitude > 0f))
                        {
                            Vector3 vector20 = this.launchPointLeft - baseT.position;
                            vector20.Normalize();
                            vector20 = (Vector3)(vector20 * 13f);
                            baseR.AddForce(vector20, ForceMode.Impulse);
                            if (this.bulletRight != null)
                            {
                                this.RBullet.disable();
                                this.releaseIfIHookSb();
                            }
                            if (this.bulletLeft != null)
                            {
                                this.LBullet.disable();
                                this.releaseIfIHookSb();
                            }
                        }
                        baseR.AddForce((Vector3)(Vector3.up * 2f), ForceMode.Impulse);
                    }
                    bool flag7 = false;
                    if ((this.bulletLeft != null) || (this.bulletRight != null))
                    {
                        if (((this.bulletLeft != null) && (this.bulletLT.position.y > baseT.position.y)) && (this.isLaunchLeft && this.LBullet.isHooked()))
                        {
                            flag7 = true;
                        }
                        if (((this.bulletRight != null) && (this.bulletRT.position.y > baseT.position.y)) && (this.isLaunchRight && this.RBullet.isHooked()))
                        {
                            flag7 = true;
                        }
                    }
                    if (flag7)
                    {
                        baseR.AddForce(new Vector3(0f, -10f * baseR.mass, 0f));
                    }
                    else
                    {
                        baseR.AddForce(new Vector3(0f, -this.gravity * baseR.mass, 0f));
                    }
                    if (this.currentSpeed > 10f)
                    {
                        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Mathf.Min((float)100f, (float)(this.currentSpeed + 40f)), 0.1f);
                    }
                    else
                    {
                        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50f, 0.1f);
                    }
                    if (flag2)
                    {
                        this.useGas(this.useGasSpeed * Time.deltaTime);
                        if ((!this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && basePV.isMine)
                        {
                            object[] parameters = new object[] { true };
                            basePV.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
                        }
                        this.smoke_3dmg.enableEmission = true;
                    }
                    else
                    {
                        if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && basePV.isMine)
                        {
                            object[] objArray3 = new object[] { false };
                            basePV.RPC("net3DMGSMOKE", PhotonTargets.Others, objArray3);
                        }
                        this.smoke_3dmg.enableEmission = false;
                    }
                    if (this.currentSpeed > 80f)
                    {
                        if (!this.speedFXPS.enableEmission)
                        {
                            this.speedFXPS.enableEmission = true;
                        }
                        this.speedFXPS.startSpeed = this.currentSpeed;
                        this.speedFX.transform.LookAt(baseT.position + baseR.velocity);
                    }
                    else if (this.speedFXPS.enableEmission)
                    {
                        this.speedFXPS.enableEmission = false;
                    }
                }
            }
        }
    }

    public string getDebugInfo()
    {
        string str = "\n";
        str = "Left:" + this.isLeftHandHooked + " ";
        if (this.isLeftHandHooked && (this.bulletLeft != null))
        {
            Vector3 vector = this.bulletLT.position - baseT.position;
            str = str + ((int)(Mathf.Atan2(vector.x, vector.z) * 57.29578f));
        }
        string str2 = str;
        object[] objArray1 = new object[] { str2, "\nRight:", this.isRightHandHooked, " " };
        str = string.Concat(objArray1);
        if (this.isRightHandHooked && (this.bulletRight != null))
        {
            Vector3 vector2 = this.bulletRT.position - baseT.position;
            str = str + ((int)(Mathf.Atan2(vector2.x, vector2.z) * 57.29578f));
        }
        str = (((str + "\nfacingDirection:" + ((int)this.facingDirection)) + "\nActual facingDirection:" + ((int)baseT.rotation.eulerAngles.y)) + "\nState:" + this.state.ToString()) + "\n\n\n\n\n";
        if (this.state == HERO_STATE.Attack)
        {
            this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
        }
        return str;
    }

    private Vector3 getGlobaleFacingVector3(float resultAngle)
    {
        float num = -resultAngle + 90f;
        float x = Mathf.Cos(num * 0.01745329f);
        return new Vector3(x, 0f, Mathf.Sin(num * 0.01745329f));
    }

    private Vector3 getGlobaleFacingVector3(float horizontal, float vertical)
    {
        float num = -this.getGlobalFacingDirection(horizontal, vertical) + 90f;
        float x = Mathf.Cos(num * 0.01745329f);
        return new Vector3(x, 0f, Mathf.Sin(num * 0.01745329f));
    }

    private float getGlobalFacingDirection(float horizontal, float vertical)
    {
        if ((vertical == 0f) && (horizontal == 0f))
        {
            return baseT.rotation.eulerAngles.y;
        }
        float y = IN_GAME_MAIN_CAMERA.mainT.rotation.eulerAngles.y;
        float num2 = Mathf.Atan2(vertical, horizontal) * 57.29578f;
        num2 = -num2 + 90f;
        return (y + num2);
    }

    private float getLeanAngle(Vector3 p, bool left)
    {
        if (!this.useGun && (this.state == HERO_STATE.Attack))
        {
            return 0f;
        }
        float num = p.y - baseT.position.y;
        float num2 = Vector3.Distance(p, baseT.position);
        float a = Mathf.Acos(num / num2) * 57.29578f;
        a *= 0.1f;
        a *= 1f + Mathf.Pow(baseR.velocity.magnitude, 0.2f);
        Vector3 vector3 = p - baseT.position;
        float current = Mathf.Atan2(vector3.x, vector3.z) * 57.29578f;
        float target = Mathf.Atan2(baseR.velocity.x, baseR.velocity.z) * 57.29578f;
        float num6 = Mathf.DeltaAngle(current, target);
        a += Mathf.Abs((float)(num6 * 0.5f));
        if (this.state != HERO_STATE.Attack)
        {
            a = Mathf.Min(a, 80f);
        }
        if (num6 > 0f)
        {
            this.leanLeft = true;
        }
        else
        {
            this.leanLeft = false;
        }
        if (this.useGun)
        {
            return (a * ((num6 >= 0f) ? ((float)1) : ((float)(-1))));
        }
        float num7 = 0f;
        if ((left && (num6 < 0f)) || (!left && (num6 > 0f)))
        {
            num7 = 0.1f;
        }
        else
        {
            num7 = 0.5f;
        }
        return (a * ((num6 >= 0f) ? num7 : -num7));
    }

    private void getOffHorse()
    {
        this.playAnimation("horse_getoff");
        baseR.AddForce((Vector3)(((Vector3.up * 10f) - (baseT.forward * 2f)) - (baseT.right * 1f)), ForceMode.VelocityChange);
        this.unmounted();
    }

    private void getOnHorse()
    {
        this.playAnimation("horse_geton");
        this.facingDirection = this.myHorse.transform.rotation.eulerAngles.y;
        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
    }

    public void getSupply()
    {
        if (((baseA.IsPlaying(this.standAnimation) || baseA.IsPlaying("run")) || baseA.IsPlaying("run_sasha")) && (((this.currentBladeSta != this.totalBladeSta) || (this.currentBladeNum != this.totalBladeNum)) || (((this.currentGas != this.totalGas) || (this.leftBulletLeft != this.bulletMAX)) || (this.rightBulletLeft != this.bulletMAX))))
        {
            this.state = HERO_STATE.FillGas;
            this.crossFade("supply", 0.1f);
        }
    }

    public void grabbed(GameObject titan, bool leftHand)
    {
        if (this.isMounted)
        {
            this.unmounted();
        }
        this.state = HERO_STATE.Grab;
        base.GetComponent<CapsuleCollider>().isTrigger = true;
        this.falseAttack();
        this.titanWhoGrabMe = titan;
        if (this.titanForm && (this.eren_titan != null))
        {
            this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
        }
        if (!this.useGun && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine))
        {
            this.leftbladetrail.Deactivate();
            this.rightbladetrail.Deactivate();
            this.leftbladetrail2.Deactivate();
            this.rightbladetrail2.Deactivate();
        }
        this.smoke_3dmg.enableEmission = false;
        this.sparks.enableEmission = false;
    }

    public bool HasDied()
    {
        return (this.hasDied || this.isInvincible());
    }

    private void headMovement()
    {
        //Transform transform = baseT.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
        //Transform transform2 = baseT.Find("Amarture/Controller_Body/hip/spine/chest/neck");
        float x = Mathf.Sqrt(((this.gunTarget.x - baseT.position.x) * (this.gunTarget.x - baseT.position.x)) + ((this.gunTarget.z - baseT.position.z) * (this.gunTarget.z - baseT.position.z)));
        this.targetHeadRotation = base.head.rotation;
        Vector3 vector5 = this.gunTarget - baseT.position;
        float current = -Mathf.Atan2(vector5.z, vector5.x) * 57.29578f;
        float num3 = -Mathf.DeltaAngle(current, baseT.rotation.eulerAngles.y - 90f);
        num3 = Mathf.Clamp(num3, -40f, 40f);
        float y = base.neck.position.y - this.gunTarget.y;
        float num5 = Mathf.Atan2(y, x) * 57.29578f;
        num5 = Mathf.Clamp(num5, -40f, 30f);
        this.targetHeadRotation = Quaternion.Euler(base.head.rotation.eulerAngles.x + num5, base.head.rotation.eulerAngles.y + num3, base.head.rotation.eulerAngles.z);
        this.oldHeadRotation = Quaternion.Lerp(this.oldHeadRotation, this.targetHeadRotation, Time.deltaTime * 60f);
        base.head.rotation = this.oldHeadRotation;
    }

    public void hookedByHuman(int hooker, Vector3 hookPosition)
    {
        object[] parameters = new object[] { hooker, hookPosition };
        basePV.RPC("RPCHookedByHuman", basePV.owner, parameters);
    }

    [RPC]
    public void hookFail()
    {
        this.hookTarget = null;
        this.hookSomeOne = false;
    }

    public void hookToHuman(GameObject target, Vector3 hookPosition, bool dash = false)
    {
        this.releaseIfIHookSb();
        this.hookTarget = target;
        this.hookSomeOne = true;
        HERO hero = target.GetComponent<HERO>();
        if (hero != null)
        {
            hero.hookedByHuman(basePV.viewID, hookPosition);
         }
        this.launchForce = hookPosition - baseT.position;
        float num = Mathf.Pow(this.launchForce.magnitude, 0.1f);
        if (this.grounded)
        {
            baseR.AddForce((Vector3)(Vector3.up * Mathf.Min((float)(this.launchForce.magnitude * 0.2f), (float)10f)), ForceMode.Impulse);
        }
        baseR.AddForce((Vector3)((this.launchForce * num) * 0.1f), ForceMode.Impulse);
    }

    private void idle()
    {
        if (this.state == HERO_STATE.Attack)
        {
            this.falseAttack();
        }
        this.state = HERO_STATE.Idle;
        this.crossFade(this.standAnimation, 0.1f);
    }

    private bool IsFrontGrounded()
    {
        //LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        //LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        //LayerMask mask3 = mask2 | mask;
        //return Physics.Raycast(baseG.transform.position + ((Vector3)(baseG.transform.up * 1f)), baseG.transform.forward, (float)1f, mask3.value);
        return Physics.Raycast(this.baseT.position + this.baseT.up * 1f, this.baseT.forward, 1f, Layer.GroundEnemy.value);
    }

    public bool IsGrounded()
    {
        //LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        //LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        //LayerMask mask3 = mask2 | mask;
        //return Physics.Raycast(baseG.transform.position + ((Vector3)(Vector3.up * 0.1f)), -Vector3.up, (float)0.3f, mask3.value);
        return Physics.Raycast(this.baseT.position + Vector3.up * 0.1f, -Vector3.up, 0.3f, Layer.GroundEnemy.value);
    }

    public bool isInvincible()
    {
        return (this.invincible > 0f);
    }

    private bool isPressDirectionTowardsHero(float h, float v)
    {
        if ((h == 0f) && (v == 0f))
        {
            return false;
        }
        return (Mathf.Abs(Mathf.DeltaAngle(this.getGlobalFacingDirection(h, v), baseT.rotation.eulerAngles.y)) < 45f);
    }

    private bool IsUpFrontGrounded()
    {
        //LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
        //LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
        //LayerMask mask3 = mask2 | mask;
        //return Physics.Raycast(baseG.transform.position + ((Vector3)(baseG.transform.up * 3f)), baseG.transform.forward, (float)1.2f, mask3.value);
        return Physics.Raycast(this.baseT.position + this.baseT.up * 3f, this.baseT.forward, 1.2f, Layer.GroundEnemy.value);
    }

    [RPC]
    private void killObject()
    {
        UnityEngine.Object.Destroy(base.gameObject);
    }

    //public void lateUpdate()
    //{
    //    if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && (this.myNetWorkName != null))
    //    {
    //        if (this.titanForm && (this.eren_titan != null))
    //        {
    //            this.myNetWorkName.transform.localPosition = (Vector3) ((Vector3.up * Screen.height) * 2f);
    //        }
    //        Vector3 start = new Vector3(baseT.position.x, baseT.position.y + 2f, baseT.position.z);
    //        GameObject obj2 = BRM.CacheGameObject.Find("MainCamera");
    //        LayerMask mask = ((int) 1) << LayerMask.NameToLayer("Ground");
    //        LayerMask mask2 = ((int) 1) << LayerMask.NameToLayer("EnemyBox");
    //        LayerMask mask3 = mask2 | mask;
    //        if ((Vector3.Angle(obj2.transform.forward, start - obj2.transform.position) > 90f) || Physics.Linecast(start, obj2.transform.position, (int) mask3))
    //        {
    //            this.myNetWorkName.transform.localPosition = (Vector3) ((Vector3.up * Screen.height) * 2f);
    //        }
    //        else
    //        {
    //            Vector2 vector5 = BRM.CacheGameObject.Find("MainCamera").GetComponent<Camera>().WorldToScreenPoint(start);
    //            this.myNetWorkName.transform.localPosition = new Vector3((float) ((int) (vector5.x - (Screen.width * 0.5f))), (float) ((int) (vector5.y - (Screen.height * 0.5f))), 0f);
    //        }
    //    }
    //    if (!this.titanForm)
    //    {
    //        if ((IN_GAME_MAIN_CAMERA.cameraTilt == 1) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine))
    //        {
    //            Quaternion quaternion3;
    //            Vector3 zero = Vector3.zero;
    //            Vector3 position = Vector3.zero;
    //            if ((this.isLaunchLeft && (this.bulletLeft != null)) && this.LBullet.isHooked())
    //            {
    //                zero = this.bulletLT.position;
    //            }
    //            if ((this.isLaunchRight && (this.bulletRight != null)) && this.RBullet.isHooked())
    //            {
    //                position = this.bulletRT.position;
    //            }
    //            Vector3 vector8 = Vector3.zero;
    //            if ((zero.magnitude != 0f) && (position.magnitude == 0f))
    //            {
    //                vector8 = zero;
    //            }
    //            else if ((zero.magnitude == 0f) && (position.magnitude != 0f))
    //            {
    //                vector8 = position;
    //            }
    //            else if ((zero.magnitude != 0f) && (position.magnitude != 0f))
    //            {
    //                vector8 = (Vector3) ((zero + position) * 0.5f);
    //            }
    //            Vector3 from = Vector3.Project(vector8 - baseT.position, BRM.CacheGameObject.Find("MainCamera").transform.up);
    //            Vector3 vector10 = Vector3.Project(vector8 - baseT.position, BRM.CacheGameObject.Find("MainCamera").transform.right);
    //            if (vector8.magnitude > 0f)
    //            {
    //                Vector3 to = from + vector10;
    //                float num = Vector3.Angle(vector8 - baseT.position, baseR.velocity) * 0.005f;
    //                Vector3 vector14 = BRM.CacheGameObject.Find("MainCamera").transform.right + vector10.normalized;
    //                quaternion3 = Quaternion.Euler(BRM.CacheGameObject.Find("MainCamera").transform.rotation.eulerAngles.x, BRM.CacheGameObject.Find("MainCamera").transform.rotation.eulerAngles.y, (vector14.magnitude >= 1f) ? (-Vector3.Angle(from, to) * num) : (Vector3.Angle(from, to) * num));
    //            }
    //            else
    //            {
    //                quaternion3 = Quaternion.Euler(BRM.CacheGameObject.Find("MainCamera").transform.rotation.eulerAngles.x, BRM.CacheGameObject.Find("MainCamera").transform.rotation.eulerAngles.y, 0f);
    //            }
    //            BRM.CacheGameObject.Find("MainCamera").transform.rotation = Quaternion.Lerp(BRM.CacheGameObject.Find("MainCamera").transform.rotation, quaternion3, Time.deltaTime * 2f);
    //        }
    //        if ((this.state == HERO_STATE.Grab) && (this.titanWhoGrabMe != null))
    //        {
    //            if (this.titanWhoGrabMe.GetComponent<TITAN>() != null)
    //            {
    //                baseT.position = this.titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.position;
    //                baseT.rotation = this.titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.rotation;
    //            }
    //            else if (this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>() != null)
    //            {
    //                baseT.position = this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.position;
    //                baseT.rotation = this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.rotation;
    //            }
    //        }
    //        if (this.useGun)
    //        {
    //            if (this.leftArmAim || this.rightArmAim)
    //            {
    //                Vector3 vector17 = this.gunTarget - baseT.position;
    //                float current = -Mathf.Atan2(vector17.z, vector17.x) * 57.29578f;
    //                float num3 = -Mathf.DeltaAngle(current, baseT.rotation.eulerAngles.y - 90f);
    //                this.headMovement();
    //                if ((!this.isLeftHandHooked && this.leftArmAim) && ((num3 < 40f) && (num3 > -90f)))
    //                {
    //                    this.leftArmAimTo(this.gunTarget);
    //                }
    //                if ((!this.isRightHandHooked && this.rightArmAim) && ((num3 > -40f) && (num3 < 90f)))
    //                {
    //                    this.rightArmAimTo(this.gunTarget);
    //                }
    //            }
    //            else if (!this.grounded)
    //            {
    //                this.handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
    //                this.handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
    //            }
    //            if (this.isLeftHandHooked && (this.bulletLeft != null))
    //            {
    //                this.leftArmAimTo(this.bulletLT.position);
    //            }
    //            if (this.isRightHandHooked && (this.bulletRight != null))
    //            {
    //                this.rightArmAimTo(this.bulletRT.position);
    //            }
    //        }
    //        this.setHookedPplDirection();
    //        this.bodyLean();
    //        if ((!baseA.IsPlaying("attack3_2") && !baseA.IsPlaying("attack5")) && !baseA.IsPlaying("special_petra"))
    //        {
    //            baseR.rotation = Quaternion.Lerp(baseG.transform.rotation, this.targetRotation, Time.deltaTime * 6f);
    //        }
    //    }
    //}

    public void lateUpdate()
    {
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && (this.myNetWorkNameT != null))
        {
            if (this.titanForm && (this.eren_titan != null))
            {
                this.myNetWorkNameT.localPosition = (Vector3)((Vector3.up * Screen.height) * 2f);
            }
            Vector3 start = new Vector3(baseT.position.x, baseT.position.y + 2f, baseT.position.z);
            LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if ((Vector3.Angle(IN_GAME_MAIN_CAMERA.mainT.forward, start - IN_GAME_MAIN_CAMERA.mainT.position) > 90f) || Physics.Linecast(start, IN_GAME_MAIN_CAMERA.mainT.position, (int)mask3))
            {
                this.myNetWorkNameT.localPosition = (Vector3)((Vector3.up * Screen.height) * 2f);
            }
            else
            {
                Vector2 vector2 = Camera.main.WorldToScreenPoint(start);
                this.myNetWorkNameT.localPosition = new Vector3((float)((int)(vector2.x - (Screen.width * 0.5f))), (float)((int)(vector2.y - (Screen.height * 0.5f))), 0f);
            }
        }
        if (!this.titanForm && !this.isCannon)
        {
            if ((IN_GAME_MAIN_CAMERA.cameraTilt == 1) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine))
            {
                Quaternion quaternion2;
                Vector3 zero = Vector3.zero;
                Vector3 position = Vector3.zero;
                if ((this.isLaunchLeft && (this.bulletLeft != null)) && this.LBullet.isHooked())
                {
                    zero = this.bulletLT.position;
                }
                if ((this.isLaunchRight && (this.bulletRight != null)) && this.RBullet.isHooked())
                {
                    position = this.bulletRT.position;
                }
                Vector3 vector5 = Vector3.zero;
                if ((zero.magnitude != 0f) && (position.magnitude == 0f))
                {
                    vector5 = zero;
                }
                else if ((zero.magnitude == 0f) && (position.magnitude != 0f))
                {
                    vector5 = position;
                }
                else if ((zero.magnitude != 0f) && (position.magnitude != 0f))
                {
                    vector5 = (Vector3)((zero + position) * 0.5f);
                }
                Vector3 from = Vector3.Project(vector5 - baseT.position, IN_GAME_MAIN_CAMERA.mainT.up);
                Vector3 vector7 = Vector3.Project(vector5 - baseT.position, IN_GAME_MAIN_CAMERA.mainT.right);
                if (vector5.magnitude > 0f)
                {
                    Vector3 to = from + vector7;
                    float num = Vector3.Angle(vector5 - baseT.position, baseR.velocity) * 0.005f;
                    Vector3 vector9 = IN_GAME_MAIN_CAMERA.mainT.right + vector7.normalized;
                    quaternion2 = Quaternion.Euler(IN_GAME_MAIN_CAMERA.mainT.rotation.eulerAngles.x, IN_GAME_MAIN_CAMERA.mainT.rotation.eulerAngles.y, (vector9.magnitude >= 1f) ? (-Vector3.Angle(from, to) * num) : (Vector3.Angle(from, to) * num));
                }
                else
                {
                    quaternion2 = Quaternion.Euler(IN_GAME_MAIN_CAMERA.mainT.rotation.eulerAngles.x, IN_GAME_MAIN_CAMERA.mainT.rotation.eulerAngles.y, 0f);
                }
                IN_GAME_MAIN_CAMERA.mainT.rotation = Quaternion.Lerp(IN_GAME_MAIN_CAMERA.mainT.rotation, quaternion2, Time.deltaTime * 2f);
            }
            if ((this.state == HERO_STATE.Grab) && (this.titanWhoGrabMe != null))
            {
                if (this.titanWhoGrabMe.GetComponent<TITAN>() != null)
                {
                    baseT.position = this.titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.position;
                    baseT.rotation = this.titanWhoGrabMe.GetComponent<TITAN>().grabTF.transform.rotation;
                }
                else if (this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>() != null)
                {
                    baseT.position = this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.position;
                    baseT.rotation = this.titanWhoGrabMe.GetComponent<FEMALE_TITAN>().grabTF.transform.rotation;
                }
            }
            if (this.useGun)
            {
                if (this.leftArmAim || this.rightArmAim)
                {
                    Vector3 vector10 = this.gunTarget - baseT.position;
                    float current = -Mathf.Atan2(vector10.z, vector10.x) * 57.29578f;
                    float num3 = -Mathf.DeltaAngle(current, baseT.rotation.eulerAngles.y - 90f);
                    this.headMovement();
                    if ((!this.isLeftHandHooked && this.leftArmAim) && ((num3 < 40f) && (num3 > -90f)))
                    {
                        this.leftArmAimTo(this.gunTarget);
                    }
                    if ((!this.isRightHandHooked && this.rightArmAim) && ((num3 > -40f) && (num3 < 90f)))
                    {
                        this.rightArmAimTo(this.gunTarget);
                    }
                }
                else if (!this.grounded)
                {
                    this.handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
                    this.handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                }
                if (this.isLeftHandHooked && (this.bulletLeft != null))
                {
                    this.leftArmAimTo(this.bulletLT.position);
                }
                if (this.isRightHandHooked && (this.bulletRight != null))
                {
                    this.rightArmAimTo(this.bulletRT.position);
                }
            }
            this.setHookedPplDirection();
            this.bodyLean();
        }
    }

    public void launch(Vector3 des, bool left = true, bool leviMode = false)
    {
        if (this.isMounted)
        {
            this.unmounted();
        }
        if (this.state != HERO_STATE.Attack)
        {
            this.idle();
        }
        Vector3 vector = des - baseT.position;
        if (left)
        {
            this.launchPointLeft = des;
        }
        else
        {
            this.launchPointRight = des;
        }
        vector.Normalize();
        vector = (Vector3)(vector * 20f);
        if (((this.bulletLeft != null) && (this.bulletRight != null)) && (this.LBullet.isHooked() && this.RBullet.isHooked()))
        {
            vector = (Vector3)(vector * 0.8f);
        }
        if (baseA.IsPlaying("attack5") || baseA.IsPlaying("special_petra"))
        {
            leviMode = true;
        }
        else
        {
            leviMode = false;
        }
        if (!leviMode)
        {
            this.falseAttack();
            this.idle();
            if (this.useGun)
            {
                this.crossFade("AHSS_hook_forward_both", 0.1f);
            }
            else if (left && !this.isRightHandHooked)
            {
                this.crossFade("air_hook_l_just", 0.1f);
            }
            else if (!left && !this.isLeftHandHooked)
            {
                this.crossFade("air_hook_r_just", 0.1f);
            }
            else
            {
                this.crossFade("dash", 0.1f);
                baseA["dash"].time = 0f;
            }
        }
        if (left)
        {
            this.isLaunchLeft = true;
        }
        if (!left)
        {
            this.isLaunchRight = true;
        }
        this.launchForce = vector;
        if (!leviMode)
        {
            if (vector.y < 30f)
            {
                this.launchForce += (Vector3)(Vector3.up * (30f - vector.y));
            }
            if (des.y >= baseT.position.y)
            {
                this.launchForce += (Vector3)((Vector3.up * (des.y - baseT.position.y)) * 10f);
            }
            baseR.AddForce(this.launchForce);
        }
        this.facingDirection = Mathf.Atan2(this.launchForce.x, this.launchForce.z) * 57.29578f;
        Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
        baseT.rotation = quaternion;
        baseR.rotation = quaternion;
        targetRotation = quaternion;
        if (left)
        {
            this.launchElapsedTimeL = 0f;
        }
        else
        {
            this.launchElapsedTimeR = 0f;
        }
        if (leviMode)
        {
            this.launchElapsedTimeR = -100f;
        }
        if (baseA.IsPlaying("special_petra"))
        {
            this.launchElapsedTimeR = -100f;
            this.launchElapsedTimeL = -100f;
            if (this.bulletRight != null)
            {
                this.RBullet.disable();
                this.releaseIfIHookSb();
            }
            if (this.bulletLeft != null)
            {
                this.LBullet.disable();
                this.releaseIfIHookSb();
            }
        }
        this.sparks.enableEmission = false;
    }

   
    private void launchLeftRope(RaycastHit hit, bool single, int mode)
    {
        if (this.currentGas != 0f)
        {
            this.useGas(0f);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.LBullet = (this.bulletLeft = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("hook"))).GetComponent<Bullet>();
                this.bulletLT = this.bulletLeft.transform;
            }
            else if (basePV.isMine)
            {
                this.LBullet = (this.bulletLeft = PhotonNetwork.Instantiate("hook", baseT.position, baseT.rotation, 0)).GetComponent<Bullet>();
                this.bulletLT = this.bulletLeft.transform;
            }
            this.bulletLT.position = (!this.useGun ? this.hookRefL1T : this.hookRefL2T).position;
            float num = !single ? ((hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f)) : 0f;
            Vector3 vector = (hit.point - ((Vector3)(baseT.right * num))) - this.bulletLT.position;
            vector.Normalize();
            LBullet.launch((Vector3)(vector * 3f), baseR.velocity, !this.useGun ? "hookRefL1" : "hookRefL2", true, baseG, mode == 1);
            this.launchPointLeft = Vector3.zero;
        }
    }

    private void launchRightRope(RaycastHit hit, bool single, int mode)
    {
        if (this.currentGas != 0f)
        {
            this.useGas(0f);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.RBullet = (this.bulletRight = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("hook"))).GetComponent<Bullet>();
                this.bulletRT = this.bulletRight.transform;
            }
            else if (basePV.isMine)
            {
                this.RBullet = (this.bulletRight = PhotonNetwork.Instantiate("hook", baseT.position, baseT.rotation, 0)).GetComponent<Bullet>();
                this.bulletRT = this.bulletRight.transform;
            }
            this.bulletRT.position = (!this.useGun ? this.hookRefR1T : this.hookRefR2T).position;
            //Bullet component = tthis.RBullet;
            float num = !single ? ((hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f)) : 0f;
            Vector3 vector = (hit.point + ((Vector3)(baseT.right * num))) - this.bulletRT.position;
            vector.Normalize();
            this.RBullet.launch((Vector3)(vector * 3f), baseR.velocity, !this.useGun ? "hookRefR1" : "hookRefR2", false, baseG, mode == 1);
            this.launchPointRight = Vector3.zero;
        }
    }

    private void leftArmAimTo(Vector3 target)
    {
        float y = target.x - this.upperarmL.transform.position.x;
        float num2 = target.y - this.upperarmL.transform.position.y;
        float x = target.z - this.upperarmL.transform.position.z;
        float num4 = Mathf.Sqrt((y * y) + (x * x));
        this.handL.localRotation = Quaternion.Euler(90f, 0f, 0f);
        this.forearmL.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        this.upperarmL.rotation = Quaternion.Euler(0f, 90f + (Mathf.Atan2(y, x) * 57.29578f), -Mathf.Atan2(num2, num4) * 57.29578f);
    }



    private void loadskin(int horse, string url)
    {
        if (this.hasspawn)
        {
            bool mipmap = true;
            bool iteratorVariable1 = false;
            if (((int)FengGameManagerMKII.settings[0x3f]) == 1)
            {
                mipmap = false;
            }
            string[] iteratorVariable2 = url.Split(new char[] { ',' });
            bool iteratorVariable3 = false;
            if (((int)FengGameManagerMKII.settings[15]) == 0)
            {
                iteratorVariable3 = true;
            }
            bool iteratorVariable4 = false;
            if (LevelInfo.getInfo(FengGameManagerMKII.level).horse || (GameSettings.horseMode == 1))
            {
                iteratorVariable4 = true;
            }
            bool iteratorVariable5 = false;
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || this.photonView.isMine)
            {
                iteratorVariable5 = true;
            }
            if (this.setup.part_hair_1 != null)
            {
                Renderer renderer = this.setup.part_hair_1.renderer;
                if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        WWW link = new WWW(iteratorVariable2[1]);
                        //yield return link;
                        Texture2D iteratorVariable8 = RCextensions.loadimage(link, mipmap, 0x30d40);
                        link.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                        {
                            iteratorVariable1 = true;
                            if (this.setup.myCostume.hairInfo.id >= 0)
                            {
                                renderer.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                            }
                            renderer.material.mainTexture = iteratorVariable8;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], renderer.material);
                            renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                        else
                        {
                            renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                    }
                    else
                    {
                        renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else if (iteratorVariable2[1].ToLower() == "transparent")
                {
                    renderer.enabled = false;
                }
            }
            if (this.setup.part_cape != null)
            {
                Renderer iteratorVariable9 = this.setup.part_cape.renderer;
                if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        WWW iteratorVariable10 = new WWW(iteratorVariable2[7]);
                        //yield return iteratorVariable10;
                        Texture2D iteratorVariable11 = RCextensions.loadimage(iteratorVariable10, mipmap, 0x30d40);
                        iteratorVariable10.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable9.material.mainTexture = iteratorVariable11;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable9.material);
                            iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                        else
                        {
                            iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                    }
                    else
                    {
                        iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else if (iteratorVariable2[7].ToLower() == "transparent")
                {
                    iteratorVariable9.enabled = false;
                }
            }
            if (this.setup.part_chest_3 != null)
            {
                Renderer iteratorVariable12 = this.setup.part_chest_3.renderer;
                if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        WWW iteratorVariable13 = new WWW(iteratorVariable2[6]);
                        //yield return iteratorVariable13;
                        Texture2D iteratorVariable14 = RCextensions.loadimage(iteratorVariable13, mipmap, 0x7a120);
                        iteratorVariable13.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable12.material.mainTexture = iteratorVariable14;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable12.material);
                            iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                        else
                        {
                            iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                    }
                    else
                    {
                        iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else if (iteratorVariable2[6].ToLower() == "transparent")
                {
                    iteratorVariable12.enabled = false;
                }
            }
            foreach (Renderer iteratorVariable15 in this.GetComponentsInChildren<Renderer>())
            {
                if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[1]))
                {
                    if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                        {
                            WWW iteratorVariable16 = new WWW(iteratorVariable2[1]);
                            //yield return iteratorVariable16;
                            Texture2D iteratorVariable17 = RCextensions.loadimage(iteratorVariable16, mipmap, 0x30d40);
                            iteratorVariable16.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                            {
                                iteratorVariable1 = true;
                                if (this.setup.myCostume.hairInfo.id >= 0)
                                {
                                    iteratorVariable15.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                                }
                                iteratorVariable15.material.mainTexture = iteratorVariable17;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                    }
                    else if (iteratorVariable2[1].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[2]))
                {
                    if ((iteratorVariable2[2].EndsWith(".jpg") || iteratorVariable2[2].EndsWith(".png")) || iteratorVariable2[2].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                        {
                            WWW iteratorVariable18 = new WWW(iteratorVariable2[2]);
                            //yield return iteratorVariable18;
                            Texture2D iteratorVariable19 = RCextensions.loadimage(iteratorVariable18, mipmap, 0x30d40);
                            iteratorVariable18.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable19;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[2], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                        }
                    }
                    else if (iteratorVariable2[2].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[3]))
                {
                    if ((iteratorVariable2[3].EndsWith(".jpg") || iteratorVariable2[3].EndsWith(".png")) || iteratorVariable2[3].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                        {
                            WWW iteratorVariable20 = new WWW(iteratorVariable2[3]);
                            //yield return iteratorVariable20;
                            Texture2D iteratorVariable21 = RCextensions.loadimage(iteratorVariable20, mipmap, 0x30d40);
                            iteratorVariable20.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable21;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[3], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                        }
                    }
                    else if (iteratorVariable2[3].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[4]))
                {
                    if ((iteratorVariable2[4].EndsWith(".jpg") || iteratorVariable2[4].EndsWith(".png")) || iteratorVariable2[4].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                        {
                            WWW iteratorVariable22 = new WWW(iteratorVariable2[4]);
                            //yield return iteratorVariable22;
                            Texture2D iteratorVariable23 = RCextensions.loadimage(iteratorVariable22, mipmap, 0x30d40);
                            iteratorVariable22.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                                iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                                iteratorVariable15.material.mainTexture = iteratorVariable23;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[4], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                        }
                    }
                    else if (iteratorVariable2[4].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[5]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[6])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[10]))
                {
                    if ((iteratorVariable2[5].EndsWith(".jpg") || iteratorVariable2[5].EndsWith(".png")) || iteratorVariable2[5].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                        {
                            WWW iteratorVariable24 = new WWW(iteratorVariable2[5]);
                            //yield return iteratorVariable24;
                            Texture2D iteratorVariable25 = RCextensions.loadimage(iteratorVariable24, mipmap, 0x30d40);
                            iteratorVariable24.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable25;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[5], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                        }
                    }
                    else if (iteratorVariable2[5].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (((iteratorVariable15.name.Contains(FengGameManagerMKII.s[7]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[8])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[9])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x18]))
                {
                    if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                        {
                            WWW iteratorVariable26 = new WWW(iteratorVariable2[6]);
                            //yield return iteratorVariable26;
                            Texture2D iteratorVariable27 = RCextensions.loadimage(iteratorVariable26, mipmap, 0x7a120);
                            iteratorVariable26.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable27;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                    }
                    else if (iteratorVariable2[6].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[12]))
                {
                    if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                        {
                            WWW iteratorVariable28 = new WWW(iteratorVariable2[7]);
                            //yield return iteratorVariable28;
                            Texture2D iteratorVariable29 = RCextensions.loadimage(iteratorVariable28, mipmap, 0x30d40);
                            iteratorVariable28.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable29;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                    }
                    else if (iteratorVariable2[7].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[15]) || ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[13]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a])) && !iteratorVariable15.name.Contains("_r")))
                {
                    if ((iteratorVariable2[8].EndsWith(".jpg") || iteratorVariable2[8].EndsWith(".png")) || iteratorVariable2[8].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                        {
                            WWW iteratorVariable30 = new WWW(iteratorVariable2[8]);
                            //yield return iteratorVariable30;
                            Texture2D iteratorVariable31 = RCextensions.loadimage(iteratorVariable30, mipmap, 0x7a120);
                            iteratorVariable30.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable31;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[8], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                        }
                    }
                    else if (iteratorVariable2[8].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x10])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a]) && iteratorVariable15.name.Contains("_r")))
                {
                    if ((iteratorVariable2[9].EndsWith(".jpg") || iteratorVariable2[9].EndsWith(".png")) || iteratorVariable2[9].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                        {
                            WWW iteratorVariable32 = new WWW(iteratorVariable2[9]);
                            //yield return iteratorVariable32;
                            Texture2D iteratorVariable33 = RCextensions.loadimage(iteratorVariable32, mipmap, 0x7a120);
                            iteratorVariable32.Dispose();
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable33;
                                FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[9], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                        }
                    }
                    else if (iteratorVariable2[9].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if ((iteratorVariable15.name == FengGameManagerMKII.s[0x12]) && iteratorVariable3)
                {
                    if ((iteratorVariable2[10].EndsWith(".jpg") || iteratorVariable2[10].EndsWith(".png")) || iteratorVariable2[10].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                        {
                            WWW iteratorVariable34 = new WWW(iteratorVariable2[10]);
                            //yield return iteratorVariable34;
                            Texture2D iteratorVariable35 = RCextensions.loadimage(iteratorVariable34, mipmap, 0x30d40);
                            iteratorVariable34.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable35;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[10], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                        }
                    }
                    else if (iteratorVariable2[10].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
                else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x19]))
                {
                    if ((iteratorVariable2[11].EndsWith(".jpg") || iteratorVariable2[11].EndsWith(".png")) || iteratorVariable2[11].EndsWith(".jpeg"))
                    {
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                        {
                            WWW iteratorVariable36 = new WWW(iteratorVariable2[11]);
                            //yield return iteratorVariable36;
                            Texture2D iteratorVariable37 = RCextensions.loadimage(iteratorVariable36, mipmap, 0x30d40);
                            iteratorVariable36.Dispose();
                            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                            {
                                iteratorVariable1 = true;
                                iteratorVariable15.material.mainTexture = iteratorVariable37;
                                FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[11], iteratorVariable15.material);
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                            }
                            else
                            {
                                iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                            }
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                        }
                    }
                    else if (iteratorVariable2[11].ToLower() == "transparent")
                    {
                        iteratorVariable15.enabled = false;
                    }
                }
            }
            if (iteratorVariable4 && (horse >= 0))
            {
                GameObject gameObject = PhotonView.Find(horse).gameObject;
                if (gameObject != null)
                {
                    foreach (Renderer iteratorVariable39 in gameObject.GetComponentsInChildren<Renderer>())
                    {
                        if (iteratorVariable39.name.Contains(FengGameManagerMKII.s[0x13]))
                        {
                            if ((iteratorVariable2[0].EndsWith(".jpg") || iteratorVariable2[0].EndsWith(".png")) || iteratorVariable2[0].EndsWith(".jpeg"))
                            {
                                if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                                {
                                    WWW iteratorVariable40 = new WWW(iteratorVariable2[0]);
                                    //yield return iteratorVariable40;
                                    Texture2D iteratorVariable41 = RCextensions.loadimage(iteratorVariable40, mipmap, 0x7a120);
                                    iteratorVariable40.Dispose();
                                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable39.material.mainTexture = iteratorVariable41;
                                        FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[0], iteratorVariable39.material);
                                        iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                    }
                                    else
                                    {
                                        iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                    }
                                }
                                else
                                {
                                    iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                }
                            }
                            else if (iteratorVariable2[0].ToLower() == "transparent")
                            {
                                iteratorVariable39.enabled = false;
                            }
                        }
                    }
                }
            }
            if (iteratorVariable5 && ((iteratorVariable2[12].EndsWith(".jpg") || iteratorVariable2[12].EndsWith(".png")) || iteratorVariable2[12].EndsWith(".jpeg")))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
                {
                    WWW iteratorVariable42 = new WWW(iteratorVariable2[12]);
                    //yield return iteratorVariable42;
                    Texture2D iteratorVariable43 = RCextensions.loadimage(iteratorVariable42, mipmap, 0x30d40);
                    iteratorVariable42.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
                    {
                        iteratorVariable1 = true;
                        this.leftbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                        this.rightbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[12], this.leftbladetrail.MyMaterial);
                        this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.leftbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                        this.rightbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                    }
                    else
                    {
                        this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                        this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    }
                }
                else
                {
                    this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                }
            }
            if (iteratorVariable1)
            {
                FengGameManagerMKII.instance.unloadAssets();
            }
        }
    }


    public void loadskin()
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
        {
            if (((int)FengGameManagerMKII.settings[0x5d]) == 1)
            {
                foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
                {
                    if (renderer.name.Contains("speed"))
                    {
                        renderer.enabled = false;
                    }
                }
            }
            if (((int)FengGameManagerMKII.settings[0]) == 1)
            {
                int index = 14;
                int num3 = 4;
                int num4 = 5;
                int num5 = 6;
                int num6 = 7;
                int num7 = 8;
                int num8 = 9;
                int num9 = 10;
                int num10 = 11;
                int num11 = 12;
                int num12 = 13;
                int num13 = 3;
                int num14 = 0x5e;
                if (((int)FengGameManagerMKII.settings[0x85]) == 1)
                {
                    num13 = 0x86;
                    num3 = 0x87;
                    num4 = 0x88;
                    num5 = 0x89;
                    num6 = 0x8a;
                    num7 = 0x8b;
                    num8 = 140;
                    num9 = 0x8d;
                    num10 = 0x8e;
                    num11 = 0x8f;
                    num12 = 0x90;
                    index = 0x91;
                    num14 = 0x92;
                }
                else if (((int)FengGameManagerMKII.settings[0x85]) == 2)
                {
                    num13 = 0x93;
                    num3 = 0x94;
                    num4 = 0x95;
                    num5 = 150;
                    num6 = 0x97;
                    num7 = 0x98;
                    num8 = 0x99;
                    num9 = 0x9a;
                    num10 = 0x9b;
                    num11 = 0x9c;
                    num12 = 0x9d;
                    index = 0x9e;
                    num14 = 0x9f;
                }
                string str = (string)FengGameManagerMKII.settings[index];
                string str2 = (string)FengGameManagerMKII.settings[num3];
                string str3 = (string)FengGameManagerMKII.settings[num4];
                string str4 = (string)FengGameManagerMKII.settings[num5];
                string str5 = (string)FengGameManagerMKII.settings[num6];
                string str6 = (string)FengGameManagerMKII.settings[num7];
                string str7 = (string)FengGameManagerMKII.settings[num8];
                string str8 = (string)FengGameManagerMKII.settings[num9];
                string str9 = (string)FengGameManagerMKII.settings[num10];
                string str10 = (string)FengGameManagerMKII.settings[num11];
                string str11 = (string)FengGameManagerMKII.settings[num12];
                string str12 = (string)FengGameManagerMKII.settings[num13];
                string str13 = (string)FengGameManagerMKII.settings[num14];
                string url = str12 + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + "," + str7 + "," + str8 + "," + str9 + "," + str10 + "," + str11 + "," + str + "," + str13;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    //base.StartCoroutine(this.loadskinE(-1, url));
                    AsyncHelper.BeginInBackground(new Action<int, string>((int horse, string URL) =>
                    {
                        loadskin(horse, URL);
                    }), -1, url);
                }
                else
                {
                    int viewID = -1;
                    if (this.myHorse != null)
                    {
                        viewID = this.myHorse.GetPhotonView().viewID;
                    }
                    basePV.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { viewID, url });
                }
            }
        }
    }

    public IEnumerator loadskinE(int horse, string url)
    {
        while (!this.hasspawn)
        {
            yield return null;
        }
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int)FengGameManagerMKII.settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        string[] iteratorVariable2 = url.Split(new char[] { ',' });
        bool iteratorVariable3 = false;
        if (((int)FengGameManagerMKII.settings[15]) == 0)
        {
            iteratorVariable3 = true;
        }
        bool iteratorVariable4 = false;
        if (LevelInfo.getInfo(FengGameManagerMKII.level).horse || (GameSettings.horseMode == 1))
        {
            iteratorVariable4 = true;
        }
        bool iteratorVariable5 = false;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || this.photonView.isMine)
        {
            iteratorVariable5 = true;
        }
        if (this.setup.part_hair_1 != null)
        {
            Renderer renderer = this.setup.part_hair_1.renderer;
            if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                {
                    WWW link = new WWW(iteratorVariable2[1]);
                    yield return link;
                    Texture2D iteratorVariable8 = RCextensions.loadimage(link, mipmap, 0x30d40);
                    link.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        iteratorVariable1 = true;
                        if (this.setup.myCostume.hairInfo.id >= 0)
                        {
                            renderer.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                        }
                        renderer.material.mainTexture = iteratorVariable8;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], renderer.material);
                        renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                    else
                    {
                        renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else
                {
                    renderer.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                }
            }
            else if (iteratorVariable2[1].ToLower() == "transparent")
            {
                renderer.enabled = false;
            }
        }
        if (this.setup.part_cape != null)
        {
            Renderer iteratorVariable9 = this.setup.part_cape.renderer;
            if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                {
                    WWW iteratorVariable10 = new WWW(iteratorVariable2[7]);
                    yield return iteratorVariable10;
                    Texture2D iteratorVariable11 = RCextensions.loadimage(iteratorVariable10, mipmap, 0x30d40);
                    iteratorVariable10.Dispose();
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable9.material.mainTexture = iteratorVariable11;
                        FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable9.material);
                        iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                    else
                    {
                        iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else
                {
                    iteratorVariable9.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                }
            }
            else if (iteratorVariable2[7].ToLower() == "transparent")
            {
                iteratorVariable9.enabled = false;
            }
        }
        if (this.setup.part_chest_3 != null)
        {
            Renderer iteratorVariable12 = this.setup.part_chest_3.renderer;
            if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
            {
                if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                {
                    WWW iteratorVariable13 = new WWW(iteratorVariable2[6]);
                    yield return iteratorVariable13;
                    Texture2D iteratorVariable14 = RCextensions.loadimage(iteratorVariable13, mipmap, 0x7a120);
                    iteratorVariable13.Dispose();
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        iteratorVariable1 = true;
                        iteratorVariable12.material.mainTexture = iteratorVariable14;
                        FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable12.material);
                        iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                    else
                    {
                        iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else
                {
                    iteratorVariable12.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                }
            }
            else if (iteratorVariable2[6].ToLower() == "transparent")
            {
                iteratorVariable12.enabled = false;
            }
        }
        foreach (Renderer iteratorVariable15 in this.GetComponentsInChildren<Renderer>())
        {
            if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[1]))
            {
                if ((iteratorVariable2[1].EndsWith(".jpg") || iteratorVariable2[1].EndsWith(".png")) || iteratorVariable2[1].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                    {
                        WWW iteratorVariable16 = new WWW(iteratorVariable2[1]);
                        yield return iteratorVariable16;
                        Texture2D iteratorVariable17 = RCextensions.loadimage(iteratorVariable16, mipmap, 0x30d40);
                        iteratorVariable16.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[1]))
                        {
                            iteratorVariable1 = true;
                            if (this.setup.myCostume.hairInfo.id >= 0)
                            {
                                iteratorVariable15.material = CharacterMaterials.materials[this.setup.myCostume.hairInfo.texture];
                            }
                            iteratorVariable15.material.mainTexture = iteratorVariable17;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[1], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[1]];
                    }
                }
                else if (iteratorVariable2[1].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[2]))
            {
                if ((iteratorVariable2[2].EndsWith(".jpg") || iteratorVariable2[2].EndsWith(".png")) || iteratorVariable2[2].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                    {
                        WWW iteratorVariable18 = new WWW(iteratorVariable2[2]);
                        yield return iteratorVariable18;
                        Texture2D iteratorVariable19 = RCextensions.loadimage(iteratorVariable18, mipmap, 0x30d40);
                        iteratorVariable18.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[2]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable19;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[2], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[2]];
                    }
                }
                else if (iteratorVariable2[2].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[3]))
            {
                if ((iteratorVariable2[3].EndsWith(".jpg") || iteratorVariable2[3].EndsWith(".png")) || iteratorVariable2[3].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                    {
                        WWW iteratorVariable20 = new WWW(iteratorVariable2[3]);
                        yield return iteratorVariable20;
                        Texture2D iteratorVariable21 = RCextensions.loadimage(iteratorVariable20, mipmap, 0x30d40);
                        iteratorVariable20.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[3]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable21;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[3], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[3]];
                    }
                }
                else if (iteratorVariable2[3].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[4]))
            {
                if ((iteratorVariable2[4].EndsWith(".jpg") || iteratorVariable2[4].EndsWith(".png")) || iteratorVariable2[4].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                    {
                        WWW iteratorVariable22 = new WWW(iteratorVariable2[4]);
                        yield return iteratorVariable22;
                        Texture2D iteratorVariable23 = RCextensions.loadimage(iteratorVariable22, mipmap, 0x30d40);
                        iteratorVariable22.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[4]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTextureScale = (Vector2)(iteratorVariable15.material.mainTextureScale * 8f);
                            iteratorVariable15.material.mainTextureOffset = new Vector2(0f, 0f);
                            iteratorVariable15.material.mainTexture = iteratorVariable23;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[4], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[4]];
                    }
                }
                else if (iteratorVariable2[4].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[5]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[6])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[10]))
            {
                if ((iteratorVariable2[5].EndsWith(".jpg") || iteratorVariable2[5].EndsWith(".png")) || iteratorVariable2[5].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                    {
                        WWW iteratorVariable24 = new WWW(iteratorVariable2[5]);
                        yield return iteratorVariable24;
                        Texture2D iteratorVariable25 = RCextensions.loadimage(iteratorVariable24, mipmap, 0x30d40);
                        iteratorVariable24.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[5]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable25;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[5], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[5]];
                    }
                }
                else if (iteratorVariable2[5].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (((iteratorVariable15.name.Contains(FengGameManagerMKII.s[7]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[8])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[9])) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x18]))
            {
                if ((iteratorVariable2[6].EndsWith(".jpg") || iteratorVariable2[6].EndsWith(".png")) || iteratorVariable2[6].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                    {
                        WWW iteratorVariable26 = new WWW(iteratorVariable2[6]);
                        yield return iteratorVariable26;
                        Texture2D iteratorVariable27 = RCextensions.loadimage(iteratorVariable26, mipmap, 0x7a120);
                        iteratorVariable26.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[6]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable27;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[6], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[6]];
                    }
                }
                else if (iteratorVariable2[6].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[12]))
            {
                if ((iteratorVariable2[7].EndsWith(".jpg") || iteratorVariable2[7].EndsWith(".png")) || iteratorVariable2[7].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                    {
                        WWW iteratorVariable28 = new WWW(iteratorVariable2[7]);
                        yield return iteratorVariable28;
                        Texture2D iteratorVariable29 = RCextensions.loadimage(iteratorVariable28, mipmap, 0x30d40);
                        iteratorVariable28.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[7]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable29;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[7], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[7]];
                    }
                }
                else if (iteratorVariable2[7].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[15]) || ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[13]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a])) && !iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[8].EndsWith(".jpg") || iteratorVariable2[8].EndsWith(".png")) || iteratorVariable2[8].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                    {
                        WWW iteratorVariable30 = new WWW(iteratorVariable2[8]);
                        yield return iteratorVariable30;
                        Texture2D iteratorVariable31 = RCextensions.loadimage(iteratorVariable30, mipmap, 0x7a120);
                        iteratorVariable30.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[8]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable31;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[8], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[8]];
                    }
                }
                else if (iteratorVariable2[8].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x11]) || iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x10])) || (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x1a]) && iteratorVariable15.name.Contains("_r")))
            {
                if ((iteratorVariable2[9].EndsWith(".jpg") || iteratorVariable2[9].EndsWith(".png")) || iteratorVariable2[9].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                    {
                        WWW iteratorVariable32 = new WWW(iteratorVariable2[9]);
                        yield return iteratorVariable32;
                        Texture2D iteratorVariable33 = RCextensions.loadimage(iteratorVariable32, mipmap, 0x7a120);
                        iteratorVariable32.Dispose();
                        if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[9]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable33;
                            FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[9], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[9]];
                    }
                }
                else if (iteratorVariable2[9].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if ((iteratorVariable15.name == FengGameManagerMKII.s[0x12]) && iteratorVariable3)
            {
                if ((iteratorVariable2[10].EndsWith(".jpg") || iteratorVariable2[10].EndsWith(".png")) || iteratorVariable2[10].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                    {
                        WWW iteratorVariable34 = new WWW(iteratorVariable2[10]);
                        yield return iteratorVariable34;
                        Texture2D iteratorVariable35 = RCextensions.loadimage(iteratorVariable34, mipmap, 0x30d40);
                        iteratorVariable34.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[10]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable35;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[10], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[10]];
                    }
                }
                else if (iteratorVariable2[10].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
            else if (iteratorVariable15.name.Contains(FengGameManagerMKII.s[0x19]))
            {
                if ((iteratorVariable2[11].EndsWith(".jpg") || iteratorVariable2[11].EndsWith(".png")) || iteratorVariable2[11].EndsWith(".jpeg"))
                {
                    if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                    {
                        WWW iteratorVariable36 = new WWW(iteratorVariable2[11]);
                        yield return iteratorVariable36;
                        Texture2D iteratorVariable37 = RCextensions.loadimage(iteratorVariable36, mipmap, 0x30d40);
                        iteratorVariable36.Dispose();
                        if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[11]))
                        {
                            iteratorVariable1 = true;
                            iteratorVariable15.material.mainTexture = iteratorVariable37;
                            FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[11], iteratorVariable15.material);
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                        }
                        else
                        {
                            iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                        }
                    }
                    else
                    {
                        iteratorVariable15.material = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[11]];
                    }
                }
                else if (iteratorVariable2[11].ToLower() == "transparent")
                {
                    iteratorVariable15.enabled = false;
                }
            }
        }
        if (iteratorVariable4 && (horse >= 0))
        {
            GameObject gameObject = PhotonView.Find(horse).gameObject;
            if (gameObject != null)
            {
                foreach (Renderer iteratorVariable39 in gameObject.GetComponentsInChildren<Renderer>())
                {
                    if (iteratorVariable39.name.Contains(FengGameManagerMKII.s[0x13]))
                    {
                        if ((iteratorVariable2[0].EndsWith(".jpg") || iteratorVariable2[0].EndsWith(".png")) || iteratorVariable2[0].EndsWith(".jpeg"))
                        {
                            if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                            {
                                WWW iteratorVariable40 = new WWW(iteratorVariable2[0]);
                                yield return iteratorVariable40;
                                Texture2D iteratorVariable41 = RCextensions.loadimage(iteratorVariable40, mipmap, 0x7a120);
                                iteratorVariable40.Dispose();
                                if (!FengGameManagerMKII.linkHash[1].ContainsKey(iteratorVariable2[0]))
                                {
                                    iteratorVariable1 = true;
                                    iteratorVariable39.material.mainTexture = iteratorVariable41;
                                    FengGameManagerMKII.linkHash[1].Add(iteratorVariable2[0], iteratorVariable39.material);
                                    iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                }
                                else
                                {
                                    iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                                }
                            }
                            else
                            {
                                iteratorVariable39.material = (Material)FengGameManagerMKII.linkHash[1][iteratorVariable2[0]];
                            }
                        }
                        else if (iteratorVariable2[0].ToLower() == "transparent")
                        {
                            iteratorVariable39.enabled = false;
                        }
                    }
                }
            }
        }
        if (iteratorVariable5 && ((iteratorVariable2[12].EndsWith(".jpg") || iteratorVariable2[12].EndsWith(".png")) || iteratorVariable2[12].EndsWith(".jpeg")))
        {
            if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
            {
                WWW iteratorVariable42 = new WWW(iteratorVariable2[12]);
                yield return iteratorVariable42;
                Texture2D iteratorVariable43 = RCextensions.loadimage(iteratorVariable42, mipmap, 0x30d40);
                iteratorVariable42.Dispose();
                if (!FengGameManagerMKII.linkHash[0].ContainsKey(iteratorVariable2[12]))
                {
                    iteratorVariable1 = true;
                    this.leftbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                    this.rightbladetrail.MyMaterial.mainTexture = iteratorVariable43;
                    FengGameManagerMKII.linkHash[0].Add(iteratorVariable2[12], this.leftbladetrail.MyMaterial);
                    this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.leftbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                    this.rightbladetrail2.MyMaterial = this.leftbladetrail.MyMaterial;
                }
                else
                {
                    this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                    this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                }
            }
            else
            {
                this.leftbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.rightbladetrail2.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.leftbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
                this.rightbladetrail.MyMaterial = (Material)FengGameManagerMKII.linkHash[0][iteratorVariable2[12]];
            }
        }
        if (iteratorVariable1)
        {
            FengGameManagerMKII.instance.unloadAssets();
        }
    }

    [RPC]
    public void loadskinRPC(int horse, string url, PhotonMessageInfo sender)
    {
        if (((int)FengGameManagerMKII.settings[0]) == 1)
        {
            base.StartCoroutine(this.loadskinE(horse, url));
            //AsyncHelper.BeginInBackground(new Action<int, string>((int HORSE, string URL) =>
            //{
            //    loadskin(HORSE, URL);
            //}), horse, url);


            //AsyncHelper.BeginInBackground(new Action (() =>
            //{
            //    using (System.IO.StreamWriter write = new System.IO.StreamWriter(Application.dataPath + "/MAMAKAKPIZDETSKINY.txt", false, System.Text.Encoding.Default))
            //    {
            //        string text = string.Format("Name{0}, Skin URLs:{1}", new object[] {
            //            sender.sender.uiname,
            //            url
            //        });
            //        write.WriteLine(text);
            //    }
            //}));
        }
    }

    public void markDie()
    {
        this.hasDied = true;
        this.state = HERO_STATE.Die;
    }

    [RPC]
    public void moveToRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            baseT.position = new Vector3(posX, posY, posZ);
        }
    }

    [RPC]
    private void net3DMGSMOKE(bool ifON)
    {
        if (this.smoke_3dmg != null)
        {
            this.smoke_3dmg.enableEmission = ifON;
        }
    }

    [RPC]
    private void netContinueAnimation()
    {
        foreach (AnimationState animationState in baseA)
        {
            if (animationState.speed == 1f)
            {
                return;
            }
            animationState.speed = 1f;
        }
        this.playAnimation(this.currentPlayingClipName());
    }


    [RPC]
    private void netCrossFade(string aniName, float time)
    {
        this.currentAnimation = aniName;
        if (baseA != null)
        {
            baseA.CrossFade(aniName, time);
        }
    }

    [RPC]
    public void netDie(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true, PhotonMessageInfo info = null)
    {
        if ((basePV.isMine && (info != null)) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT))
        {
            if (FengGameManagerMKII.ignoreList.Contains(info.sender.ID))
            {
                basePV.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
                return;
            }
            if (!info.sender.isLocal && !info.sender.isMasterClient)
            {
                if ((info.sender.customProperties[PhotonPlayerProperty.name] == null) || (info.sender.customProperties[PhotonPlayerProperty.isTitan] == null))
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
                else if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + " (possibly valid).</color>");
                        return;
                    }
                    else
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                        return;
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
                else if (PhotonView.Find(viewID).owner.ID != info.sender.ID)
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
            }
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, killByTitan);
            int iD = basePV.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
        if (basePV.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }
        if (this.bulletLeft != null)
        {
            this.LBullet.removeMe();
        }
        if (this.bulletRight != null)
        {
            this.RBullet.removeMe();
        }
        this.meatDie.Play();
        if (!(this.useGun || ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !basePV.isMine)))
        {
            this.leftbladetrail.Deactivate();
            this.rightbladetrail.Deactivate();
            this.leftbladetrail2.Deactivate();
            this.rightbladetrail2.Deactivate();
        }
        this.falseAttack();
        this.breakApart(v, isBite);
        if (basePV.isMine)
        {
            IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(false);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.hasDied = true;
        Transform transform = baseT.Find("audio_die");
        if (transform != null)
        {
            transform.parent = null;
            transform.GetComponent<AudioSource>().Play();
        }
        sync.disabled = true;
        if (basePV.isMine)
        {
            PhotonNetwork.RemoveRPCs(basePV);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            propertiesToSet.Add(PhotonPlayerProperty.deaths, RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths]) + 1);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            object[] parameters = new object[] { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.PView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                PhotonView view2 = PhotonView.Find(viewID);
                if (view2 != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(killByTitan, "[FFC000][" + info.sender.ID.ToString() + "][FFFFFF]" + RCextensions.returnStringFromObject(view2.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view2.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view2.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(!(titanName == string.Empty), "[FFC000][" + info.sender.ID.ToString() + "][FFFFFF]" + titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }
        }
        if (basePV.isMine)
        {
            PhotonNetwork.Destroy(basePV);
        }
    }

    private void diefromRPC(int viewID = -1, string titanName = "", PhotonMessageInfo info = null)
    {
        GameObject obj2;
        if (basePV.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            PhotonNetwork.RemoveRPCs(basePV);
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }

        this.meatDie.Play();
        if (this.bulletLeft != null)
        {
            this.LBullet.removeMe();
        }
        if (this.bulletRight != null)
        {
            this.RBullet.removeMe();
        }
        Transform transform = baseT.Find("audio_die");
        transform.parent = null;
        transform.GetComponent<AudioSource>().Play();
        if (basePV.isMine)
        {
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
            IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.falseAttack();
        this.hasDied = true;
        sync.disabled = true;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && basePV.isMine)
        {
            PhotonNetwork.RemoveRPCs(basePV);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            propertiesToSet.Add(PhotonPlayerProperty.deaths, ((int)PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths]) + 1);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            if (viewID != -1)
            {
                PhotonView view2 = PhotonView.Find(viewID);
                if (view2 != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(true, "[FFC000][" + info.sender.ID.ToString() + "][FFFFFF]" + RCextensions.returnStringFromObject(view2.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view2.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view2.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(true, "[FFC000][" + info.sender.ID.ToString() + "][FFFFFF]" + titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }
            object[] parameters = new object[] { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.PView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && basePV.isMine)
        {
            PhotonNetwork.Instantiate("hitMeat2", this.baseT.position, Quaternion.Euler(270f, 0f, 0f), 0, null);
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("hitMeat2"));
        }      
        if (basePV.isMine)
        {
            PhotonNetwork.Destroy(basePV);
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, true);
            int iD = basePV.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }

    [RPC]
    private void netDie2(int viewID = -1, string titanName = "", PhotonMessageInfo info = null)
    {
       
        if ((basePV.isMine && (info != null)) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.BOSS_FIGHT_CT))
        {
            if (FengGameManagerMKII.ignoreList.Contains(info.sender.ID))
            {
                basePV.RPC("backToHumanRPC", PhotonTargets.Others, new object[0]);
                return;
            }
            if (!info.sender.isLocal && !info.sender.isMasterClient)
            {
                if ((info.sender.customProperties[PhotonPlayerProperty.name] == null) || (info.sender.customProperties[PhotonPlayerProperty.isTitan] == null))
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
                else if (viewID < 0)
                {
                    if (titanName == "")
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + " (possibly valid).</color>");
                        return;
                    }
                    else if ((GameSettings.bombMode == 0) && (GameSettings.deadlyCannons == 0))
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                        return;
                    }
                }
                else if (PhotonView.Find(viewID) == null)
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
                else if (PhotonView.Find(viewID).owner.ID != info.sender.ID)
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Unusual Kill from ID " + info.sender.ID.ToString() + "</color>");
                    return;
                }
            }
        }
        diefromRPC(viewID, titanName, info);
    }

    public void netDieLocal(Vector3 v, bool isBite, int viewID = -1, string titanName = "", bool killByTitan = true)
    {
        if (basePV.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            if (this.titanForm && (this.eren_titan != null))
            {
                this.eren_titan.GetComponent<TITAN_EREN>().lifeTime = 0.1f;
            }
            if (this.myBomb != null)
            {
                this.myBomb.destroyMe();
            }
            if (this.myCannon != null)
            {
                PhotonNetwork.Destroy(this.myCannon);
            }
            if (this.skillCD != null)
            {
                this.skillCD.transform.localPosition = vector;
            }
        }
        if (this.bulletLeft != null)
        {
            this.LBullet.removeMe();
        }
        if (this.bulletRight != null)
        {
            this.RBullet.removeMe();
        }
        this.meatDie.Play();
        if (!(this.useGun || ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !basePV.isMine)))
        {
            this.leftbladetrail.Deactivate();
            this.rightbladetrail.Deactivate();
            this.leftbladetrail2.Deactivate();
            this.rightbladetrail2.Deactivate();
        }
        this.falseAttack();
        this.breakApart(v, isBite);
        if (basePV.isMine)
        {
            IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(false);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            FengGameManagerMKII.instance.myRespawnTime = 0f;
        }
        this.hasDied = true;
        Transform transform = baseT.Find("audio_die");
        transform.parent = null;
        transform.GetComponent<AudioSource>().Play();
        sync.disabled = true;
        if (basePV.isMine)
        {
            PhotonNetwork.RemoveRPCs(basePV);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.dead, true);
            propertiesToSet.Add(PhotonPlayerProperty.deaths, RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.deaths]) + 1);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            object[] parameters = new object[] { !(titanName == string.Empty) ? 1 : 0 };
            FengGameManagerMKII.instance.photonView.RPC("someOneIsDead", PhotonTargets.MasterClient, parameters);
            if (viewID != -1)
            {
                PhotonView view = PhotonView.Find(viewID);
                if (view != null)
                {
                    FengGameManagerMKII.instance.sendKillInfo(killByTitan, RCextensions.returnStringFromObject(view.owner.customProperties[PhotonPlayerProperty.name]), false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
                    propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.kills, RCextensions.returnIntFromObject(view.owner.customProperties[PhotonPlayerProperty.kills]) + 1);
                    view.owner.SetCustomProperties(propertiesToSet);
                }
            }
            else
            {
                FengGameManagerMKII.instance.sendKillInfo(!(titanName == string.Empty), titanName, false, RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]), 0);
            }
        }
        if (basePV.isMine)
        {
            PhotonNetwork.Destroy(basePV);
        }
        if (PhotonNetwork.isMasterClient)
        {
            this.onDeathEvent(viewID, killByTitan);
            int iD = basePV.owner.ID;
            if (FengGameManagerMKII.heroHash.ContainsKey(iD))
            {
                FengGameManagerMKII.heroHash.Remove(iD);
            }
        }
    }

    [RPC]
    private void netGrabbed(int id, bool leftHand, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient || info.sender.isTitan)
        {
            this.titanWhoGrabMeID = id;
            this.grabbed(PhotonView.Find(id).gameObject, leftHand);
        }
    }

    [RPC]
    private void netlaughAttack()
    {
        foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if (((Vector3.Distance(obj2.transform.position, baseT.position) < 50f) && (Vector3.Angle(obj2.transform.forward, baseT.position - obj2.transform.position) < 90f)) && (obj2.GetComponent<TITAN>() != null))
            {
                obj2.GetComponent<TITAN>().beLaughAttacked();
            }
        }
    }

    [RPC]
    private void netPauseAnimation()
    {
        foreach (AnimationState animationState in baseA)
        {
            animationState.speed = 0f;
        }
    }

    [RPC]
    private void netPlayAnimation(string aniName)
    {
        this.currentAnimation = aniName;
        if (baseA != null)
        {
            baseA.Play(aniName);
        }
    }

    [RPC]
    private void netPlayAnimationAt(string aniName, float normalizedTime)
    {
        this.currentAnimation = aniName;
        if (baseA != null)
        {
            baseA.Play(aniName);
            baseA[aniName].normalizedTime = normalizedTime;
        }
    }

    [RPC]
    private void netSetIsGrabbedFalse()
    {
        this.state = HERO_STATE.Idle;
    }

    [RPC]
    private void netTauntAttack(float tauntTime, float distance = 100f)
    {
        foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if ((Vector3.Distance(obj2.transform.position, baseT.position) < distance) && (obj2.GetComponent<TITAN>() != null))
            {
                obj2.GetComponent<TITAN>().beTauntedBy(baseG, tauntTime);
            }
        }
    }

    [RPC]
    private void netUngrabbed()
    {
        this.ungrabbed();
        this.netPlayAnimation(this.standAnimation);
        this.falseAttack();
    }

    

    public void onDeathEvent(int viewID, bool isTitan)
    {
        RCEvent event2;
        string[] strArray;
        if (isTitan)
        {
            if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByTitan"))
            {
                event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByTitan"];
                strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByTitan"];
                if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
                {
                    FengGameManagerMKII.playerVariables[strArray[0]] = basePV.owner;
                }
                else
                {
                    FengGameManagerMKII.playerVariables.Add(strArray[0], basePV.owner);
                }
                if (FengGameManagerMKII.titanVariables.ContainsKey(strArray[1]))
                {
                    FengGameManagerMKII.titanVariables[strArray[1]] = PhotonView.Find(viewID).gameObject.GetComponent<TITAN>();
                }
                else
                {
                    FengGameManagerMKII.titanVariables.Add(strArray[1], PhotonView.Find(viewID).gameObject.GetComponent<TITAN>());
                }
                event2.checkEvent();
            }
        }
        else if (FengGameManagerMKII.RCEvents.ContainsKey("OnPlayerDieByPlayer"))
        {
            event2 = (RCEvent)FengGameManagerMKII.RCEvents["OnPlayerDieByPlayer"];
            strArray = (string[])FengGameManagerMKII.RCVariableNames["OnPlayerDieByPlayer"];
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[0]))
            {
                FengGameManagerMKII.playerVariables[strArray[0]] = basePV.owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[0], basePV.owner);
            }
            if (FengGameManagerMKII.playerVariables.ContainsKey(strArray[1]))
            {
                FengGameManagerMKII.playerVariables[strArray[1]] = PhotonView.Find(viewID).owner;
            }
            else
            {
                FengGameManagerMKII.playerVariables.Add(strArray[1], PhotonView.Find(viewID).owner);
            }
            event2.checkEvent();
        }
    }

    //private void OnDisable()
    //{

    //}

    private void OnDestroy()
    {
        if (this.myNetWorkName != null)
        {
            UnityEngine.Object.Destroy(this.myNetWorkName);
        }
        if (this.gunDummy != null)
        {
            UnityEngine.Object.Destroy(this.gunDummy);
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            this.releaseIfIHookSb();
        }
        if (FengGameManagerMKII.instance != null)
        {
            FengGameManagerMKII.instance.removeHero(this, baseG);
        }
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && basePV.isMine)
        {
            Vector3 vector = (Vector3)(Vector3.up * 5000f);
            this.cross1.transform.localPosition = vector;
            this.cross2.transform.localPosition = vector;
            this.crossL1.transform.localPosition = vector;
            this.crossL2.transform.localPosition = vector;
            this.crossR1.transform.localPosition = vector;
            this.crossR2.transform.localPosition = vector;
            this.LabelDistance.transform.localPosition = vector;
        }
        if (this.setup.part_cape != null)
        {
            ClothFactory.DisposeObject(this.setup.part_cape);
        }
        if (this.setup.part_hair_1 != null)
        {
            ClothFactory.DisposeObject(this.setup.part_hair_1);
        }
        if (this.setup.part_hair_2 != null)
        {
            ClothFactory.DisposeObject(this.setup.part_hair_2);
        }
    }

    public void pauseAnimation()
    {
        if (this.netPauseStopped) return;
        foreach (object obj in this.baseA)
        {
            ((AnimationState)obj).speed = 0f;
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && this.basePV.isMine)
        {
            this.basePV.RPC("netPauseAnimation", PhotonTargets.Others, new object[0]);
        }
        this.netPauseStopped = true;
    }


    public void playAnimation(string aniName)
    {
        if (this.baseA == null || this.baseA[aniName] == null)
        {
            return;
        }
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && !this.basePV.isMine))
        {
            this.currentAnimation = aniName;
        }
        baseA.Play(aniName);
        if (PhotonNetwork.connected && basePV.isMine)
        {
            basePV.RPC("netPlayAnimation", PhotonTargets.Others, aniName);
        }
    }

    private void playAnimationAt(string aniName, float normalizedTime)
    {
        if (this.baseA == null || this.baseA[aniName] == null)
        {
            return;
        }
        baseA.Play(aniName);
        baseA[aniName].normalizedTime = normalizedTime;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && this.basePV != null && !this.basePV.isMine))
        {
            this.currentAnimation = aniName;
        }    
        if (PhotonNetwork.connected && basePV.isMine)
        {
            object[] parameters = new object[] { aniName, normalizedTime };
            basePV.RPC("netPlayAnimationAt", PhotonTargets.Others, parameters);
        }
    }

    private void releaseIfIHookSb()
    {
        if (this.hookSomeOne && (this.hookTarget != null))
        {
            this.hookTarget.GetPhotonView().RPC("badGuyReleaseMe", this.hookTarget.GetPhotonView().owner, new object[0]);
            this.hookTarget = null;
            this.hookSomeOne = false;
        }
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if ((FengGameManagerMKII.skyMaterial != null) && (Camera.main.GetComponent<Skybox>().material != FengGameManagerMKII.skyMaterial))
        {
            Camera.main.GetComponent<Skybox>().material = FengGameManagerMKII.skyMaterial;
        }
    }

    // HERO
    public void resetAnimationSpeed()
    {
        foreach (AnimationState animationState in baseA)
        {
            animationState.speed = 1f;
        }
        this.customAnimationSpeed();
    }


    [RPC]
    public void ReturnFromCannon(PhotonMessageInfo info)
    {
        if (info.sender == basePV.owner)
        {
            this.isCannon = false;
            sync.disabled = false;
        }
    }

    private void rightArmAimTo(Vector3 target)
    {
        float y = target.x - this.upperarmR.transform.position.x;
        float num2 = target.y - this.upperarmR.transform.position.y;
        float x = target.z - this.upperarmR.transform.position.z;
        float num4 = Mathf.Sqrt((y * y) + (x * x));
        this.handR.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        this.forearmR.localRotation = Quaternion.Euler(90f, 0f, 0f);
        this.upperarmR.rotation = Quaternion.Euler(180f, 90f + (Mathf.Atan2(y, x) * 57.29578f), Mathf.Atan2(num2, num4) * 57.29578f);
    }

    [RPC]
    private void RPCHookedByHuman(int hooker, Vector3 hookPosition)
    {
        this.hookBySomeOne = true;
        this.badGuy = PhotonView.Find(hooker).gameObject;
        if (Vector3.Distance(hookPosition, baseT.position) < 15f)
        {
            this.launchForce = PhotonView.Find(hooker).gameObject.transform.position - baseT.position;
            baseR.AddForce((Vector3)(-baseR.velocity * 0.9f), ForceMode.VelocityChange);
            float num = Mathf.Pow(this.launchForce.magnitude, 0.1f);
            if (this.grounded)
            {
                baseR.AddForce((Vector3)(Vector3.up * Mathf.Min((float)(this.launchForce.magnitude * 0.2f), (float)10f)), ForceMode.Impulse);
            }
            baseR.AddForce((Vector3)((this.launchForce * num) * 0.1f), ForceMode.Impulse);
            if (this.state != HERO_STATE.Grab)
            {
                this.dashTime = 1f;
                this.crossFade("dash", 0.05f);
                baseA["dash"].time = 0.1f;
                this.state = HERO_STATE.AirDodge;
                this.falseAttack();
                this.facingDirection = Mathf.Atan2(this.launchForce.x, this.launchForce.z) * 57.29578f;
                Quaternion quaternion = Quaternion.Euler(0f, this.facingDirection, 0f);
                baseG.transform.rotation = quaternion;
                baseR.rotation = quaternion;
                this.targetRotation = quaternion;
            }
        }
        else
        {
            this.hookBySomeOne = false;
            this.badGuy = null;
            PhotonView.Find(hooker).RPC("hookFail", PhotonView.Find(hooker).owner, new object[0]);
        }
    }

    private void salute()
    {
        this.state = HERO_STATE.Salute;
        this.crossFade("salute", 0.1f);
    }

    private void setHookedPplDirection()
    {
        this.almostSingleHook = false;
        if (this.isRightHandHooked && this.isLeftHandHooked)
        {
            if ((this.bulletLeft != null) && (this.bulletRight != null))
            {
                Vector3 normal = this.bulletLT.position - this.bulletRT.position;
                if (normal.sqrMagnitude < 4f)
                {
                    Vector3 vector2 = ((Vector3)((this.bulletLT.position + this.bulletRT.position) * 0.5f)) - baseT.position;
                    this.facingDirection = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
                    if (this.useGun && (this.state != HERO_STATE.Attack))
                    {
                        float current = -Mathf.Atan2(baseR.velocity.z, baseR.velocity.x) * 57.29578f;
                        float target = -Mathf.Atan2(vector2.z, vector2.x) * 57.29578f;
                        float num3 = -Mathf.DeltaAngle(current, target);
                        this.facingDirection += num3;
                    }
                    this.almostSingleHook = true;
                }
                else
                {
                    Vector3 to = baseT.position - this.bulletLT.position;
                    Vector3 vector6 = baseT.position - this.bulletRT.position;
                    Vector3 vector7 = (Vector3)((this.bulletLT.position + this.bulletRT.position) * 0.5f);
                    Vector3 from = baseT.position - vector7;
                    if ((Vector3.Angle(from, to) < 30f) && (Vector3.Angle(from, vector6) < 30f))
                    {
                        this.almostSingleHook = true;
                        Vector3 vector9 = vector7 - baseT.position;
                        this.facingDirection = Mathf.Atan2(vector9.x, vector9.z) * 57.29578f;
                    }
                    else
                    {
                        this.almostSingleHook = false;
                        Vector3 forward = baseT.forward;
                        Vector3.OrthoNormalize(ref normal, ref forward);
                        this.facingDirection = Mathf.Atan2(forward.x, forward.z) * 57.29578f;
                        float num4 = Mathf.Atan2(to.x, to.z) * 57.29578f;
                        if (Mathf.DeltaAngle(num4, this.facingDirection) > 0f)
                        {
                            this.facingDirection += 180f;
                        }
                    }
                }
            }
        }
        else
        {
            this.almostSingleHook = true;
            Vector3 zero = Vector3.zero;
            if (this.isRightHandHooked && (this.bulletRight != null))
            {
                zero = this.bulletRT.position - baseT.position;
            }
            else if (this.isLeftHandHooked && (this.bulletLeft != null))
            {
                zero = this.bulletLT.position - baseT.position;
            }
            else
            {
                return;
            }
            this.facingDirection = Mathf.Atan2(zero.x, zero.z) * 57.29578f;
            if (this.state != HERO_STATE.Attack)
            {
                float num6 = -Mathf.Atan2(baseR.velocity.z, baseR.velocity.x) * 57.29578f;
                float num7 = -Mathf.Atan2(zero.z, zero.x) * 57.29578f;
                float num8 = -Mathf.DeltaAngle(num6, num7);
                if (this.useGun)
                {
                    this.facingDirection += num8;
                }
                else
                {
                    float num9 = 0f;
                    if ((this.isLeftHandHooked && (num8 < 0f)) || (this.isRightHandHooked && (num8 > 0f)))
                    {
                        num9 = -0.1f;
                    }
                    else
                    {
                        num9 = 0.1f;
                    }
                    this.facingDirection += num8 * num9;
                }
            }
        }
    }

    [RPC]
    public void SetMyCannon(int viewID, PhotonMessageInfo info)
    {
        if (info.sender == basePV.owner)
        {
            PhotonView view = PhotonView.Find(viewID);
            if (view != null)
            {
                this.myCannon = view.gameObject;
                if (this.myCannon != null)
                {
                    this.myCannonBase = this.myCannon.transform;
                    this.myCannonPlayer = this.myCannonBase.Find("PlayerPoint");
                    this.isCannon = true;
                }
            }
        }
    }

    [RPC]
    public void SetMyPhotonCamera(float offset, PhotonMessageInfo info)
    {
        if (basePV.owner == info.sender)
        {
            this.CameraMultiplier = offset;
            sync.PhotonCamera = true;
            this.isPhotonCamera = true;
        }
    }

    [RPC]
    private void setMyTeam(int val)
    {
        this.myTeam = val;
        if (this.checkBoxLeft != null && this.triggerLeft != null)
        {
            this.triggerLeft.myTeam = val;
        }
        if (this.checkBoxRight != null && this.triggerRight != null)
        {
            this.triggerRight.myTeam = val;
        }
        if ((val > 1 && IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
        {
            object[] objArray;
            if (GameSettings.friendlyMode > 0)
            {
                if (val != 1)
                {
                    objArray = new object[] { 1 };
                    basePV.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if (GameSettings.pvpMode == 1)
            {
                int num = 0;
                if (basePV.owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    num = RCextensions.returnIntFromObject(basePV.owner.customProperties[PhotonPlayerProperty.RCteam]);
                }
                if (val != num)
                {
                    objArray = new object[] { num };
                    basePV.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
                }
            }
            else if ((GameSettings.pvpMode == 2) && (val != basePV.owner.ID))
            {
                objArray = new object[] { basePV.owner.ID };
                basePV.RPC("setMyTeam", PhotonTargets.AllBuffered, objArray);
            }
        }
    }
    

    public void setSkillHUDPosition2()
    {
        this.skillCD = BRM.CacheGameObject.Find("skill_cd_" + this.skillIDHUD);
        if (this.skillCD != null)
        {
            this.skillCD.transform.localPosition = BRM.CacheGameObject.Find("skill_cd_bottom").transform.localPosition;
        }
        if (this.useGun && (GameSettings.bombMode == 0))
        {
            this.skillCD.transform.localPosition = (Vector3)(Vector3.up * 5000f);
        }
    }
    
    public void setStat2()
    {
        this.skillCDLast = 1.5f;
        this.skillId = this.setup.myCostume.stat.skillId;
        if (this.skillId == "levi")
        {
            this.skillCDLast = 3.5f;
        }
        this.customAnimationSpeed();
        if (this.skillId == "armin")
        {
            this.skillCDLast = 5f;
        }
        if (this.skillId == "marco")
        {
            this.skillCDLast = 10f;
        }
        if (this.skillId == "jean")
        {
            this.skillCDLast = 0.001f;
        }
        if (this.skillId == "eren")
        {
            this.skillCDLast = 120f;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if ((LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan || (LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.RACING)) || ((LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.PVP_CAPTURE) || (LevelInfo.getInfo(FengGameManagerMKII.level).type == GAMEMODE.TROST)))
                {
                    this.skillId = "petra";
                    this.skillCDLast = 1f;
                }
                else
                {
                    int num = 0;
                    foreach (PhotonPlayer player in PhotonNetwork.playerList)
                    {
                        if ((RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1) && (RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.character]).ToUpper() == "EREN"))
                        {
                            num++;
                        }
                    }
                    if (num > 1)
                    {
                        this.skillId = "petra";
                        this.skillCDLast = 1f;
                    }
                }
            }
        }
        if (this.skillId == "sasha")
        {
            this.skillCDLast = 20f;
        }
        if (this.skillId == "petra")
        {
            this.skillCDLast = 3.5f;
        }
        this.bombInit();
        this.speed = ((float)this.setup.myCostume.stat.SPD) / 10f;
        this.totalGas = this.currentGas = this.setup.myCostume.stat.GAS;
        this.totalBladeSta = this.currentBladeSta = this.setup.myCostume.stat.BLA;
        baseR.mass = 0.5f - ((this.setup.myCostume.stat.ACL - 100) * 0.001f);
        BRM.CacheGameObject.Find("skill_cd_bottom").transform.localPosition = new Vector3(0f, (-Screen.height * 0.5f) + 5f, 0f);
        this.skillCD = BRM.CacheGameObject.Find("skill_cd_" + this.skillIDHUD);
        this.skillCD.transform.localPosition = BRM.CacheGameObject.Find("skill_cd_bottom").transform.localPosition;
        BRM.CacheGameObject.Find("GasUI").transform.localPosition = BRM.CacheGameObject.Find("skill_cd_bottom").transform.localPosition;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
        {
            BRM.CacheGameObject.Find<UISprite>("bulletL").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL1").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR1").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL2").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR2").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL3").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR3").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL4").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR4").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL5").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR5").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL6").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR6").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletL7").enabled = false;
            BRM.CacheGameObject.Find<UISprite>("bulletR7").enabled = false;
        }
        if (this.setup.myCostume.uniform_type == UNIFORM_TYPE.CasualAHSS)
        {
            this.standAnimation = "AHSS_stand_gun";
            this.useGun = true;
            this.gunDummy = new GameObject();
            this.gunDummy.name = "gunDummy";
            this.gunDummy.transform.position = baseT.position;
            this.gunDummy.transform.rotation = baseT.rotation;
            this.myGroup = GROUP.A;
            this.setTeam2(2);
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
            {
                BRM.CacheGameObject.Find<UISprite>("bladeCL").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladeCR").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladel1").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("blader1").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladel2").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("blader2").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladel3").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("blader3").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladel4").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("blader4").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bladel5").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("blader5").enabled = false;
                BRM.CacheGameObject.Find<UISprite>("bulletL").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL1").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR1").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL2").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR2").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL3").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR3").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL4").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR4").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL5").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR5").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL6").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR6").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletL7").enabled = true;
                BRM.CacheGameObject.Find<UISprite>("bulletR7").enabled = true;
                if (this.skillId != "bomb")
                {
                    this.skillCD.transform.localPosition = (Vector3)(Vector3.up * 5000f);
                }
            }
        }
        else if (this.setup.myCostume.sex == SEX.FEMALE)
        {
            this.standAnimation = "stand";
            this.setTeam2(1);
        }
        else
        {
            this.standAnimation = "stand_levi";
            this.setTeam2(1);
        }
    }

 

    public void setTeam2(int team)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && basePV.isMine)
        {
            object[] parameters = new object[] { team };
            basePV.RPC("setMyTeam", PhotonTargets.AllBuffered, parameters);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.team, team);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else
        {
            this.setMyTeam(team);
        }
    }

    public void shootFlare(int type)
    {
        bool flag = false;
        if ((type == 1) && (this.flare1CD == 0f))
        {
            this.flare1CD = this.flareTotalCD;
            flag = true;
        }
        if ((type == 2) && (this.flare2CD == 0f))
        {
            this.flare2CD = this.flareTotalCD;
            flag = true;
        }
        if ((type == 3) && (this.flare3CD == 0f))
        {
            this.flare3CD = this.flareTotalCD;
            flag = true;
        }
        if (flag)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("FX/flareBullet" + type), baseT.position, baseT.rotation);
                obj2.GetComponent<FlareMovement>().dontShowHint();
                UnityEngine.Object.Destroy(obj2, 25f);
            }
            else
            {
                PhotonNetwork.Instantiate("FX/flareBullet" + type, baseT.position, baseT.rotation, 0).GetComponent<FlareMovement>().dontShowHint();
            }
        }
    }
    
    private void showAimUI2()
    {
        Vector3 vector;
        if (Screen.showCursor)
        {
            //GameObject obj2 = this.cross1;
            //GameObject obj3 = this.cross2;
            //GameObject obj4 = this.crossL1;
            //GameObject obj5 = this.crossL2;
            //GameObject obj6 = this.crossR1;
            //GameObject obj7 = this.crossR2;
            //GameObject labelDistance = this.LabelDistance;
            vector = (Vector3)(Vector3.up * 10000f);
            crossT2.localPosition = vector;
            crossT1.localPosition = vector;
            crossR2T.localPosition = vector;
            crossR1T.localPosition = vector;
            crossL2T.localPosition = vector;
            crossL1T.localPosition = vector;
            labelT.localPosition = vector;
        }
        else
        {
            RaycastHit hit;
            this.checkTitan();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
            LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
            LayerMask mask3 = mask2 | mask;
            if (Physics.Raycast(ray, out hit, 1E+07f, mask3.value))
            {
                RaycastHit hit2;
                //GameObject obj9 = this.cross1;
                //GameObject obj10 = this.cross2;
                crossT1.localPosition = Input.mousePosition;
                //Transform transform = obj9.transform;
                crossT1.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                crossT2.localPosition = crossT1.localPosition;
                vector = hit.point - baseT.position;
                float magnitude = vector.magnitude;
                //GameObject obj11 = this.LabelDistance;
                string str = (magnitude <= 1000f) ? ((int)magnitude).ToString() : "???";
                if (((int)FengGameManagerMKII.settings[0xbd]) == 1)
                {
                    str = str + "\n" + this.currentSpeed.ToString("F1") + " u/s";
                }
                else if (((int)FengGameManagerMKII.settings[0xbd]) == 2)
                {
                    str = str + "\n" + ((this.currentSpeed / 100f)).ToString("F1") + "K";
                }
                LabelDistance.text = str;
                if (magnitude > 120f)
                {
                    //Transform transform11 = obj9.transform;
                    crossT1.localPosition += (Vector3)(Vector3.up * 10000f);
                    labelT.localPosition = crossT2.localPosition;
                }
                else
                {
                    //Transform transform12 = obj10.transform;
                    crossT2.localPosition += (Vector3)(Vector3.up * 10000f);
                    labelT.localPosition = crossT1.transform.localPosition;
                }
                //Transform transform13 = obj11.transform;
                labelT.localPosition -= new Vector3(0f, 15f, 0f);
                Vector3 vector2 = new Vector3(0f, 0.4f, 0f);
                vector2 -= (Vector3)(baseT.right * 0.3f);
                Vector3 vector3 = new Vector3(0f, 0.4f, 0f);
                vector3 += (Vector3)(baseT.right * 0.3f);
                float num4 = (hit.distance <= 50f) ? (hit.distance * 0.05f) : (hit.distance * 0.3f);
                Vector3 vector4 = (hit.point - ((Vector3)(baseT.right * num4))) - (baseT.position + vector2);
                Vector3 vector5 = (hit.point + ((Vector3)(baseT.right * num4))) - (baseT.position + vector3);
                vector4.Normalize();
                vector5.Normalize();
                vector4 = (Vector3)(vector4 * 1000000f);
                vector5 = (Vector3)(vector5 * 1000000f);
                if (Physics.Linecast(baseT.position + vector2, (baseT.position + vector2) + vector4, out hit2, mask3.value))
                {
                    //GameObject obj12 = this.crossL1;
                    crossL1T.localPosition = Camera.main.WorldToScreenPoint(hit2.point);
                    //Transform transform14 = obj12.transform;
                    crossL1T.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                    crossL1T.localRotation = Quaternion.Euler(0f, 0f, (Mathf.Atan2(crossL1T.localPosition.y - (Input.mousePosition.y - (Screen.height * 0.5f)), crossL1T.localPosition.x - (Input.mousePosition.x - (Screen.width * 0.5f))) * 57.29578f) + 180f);
                    //GameObject obj13 = this.crossL2;
                    crossL2T.localPosition = crossL1T.localPosition;
                    crossL2T.localRotation = crossL1T.localRotation;
                    if (hit2.distance > 120f)
                    {
                        //Transform transform15 = obj12.transform;
                        crossL1T.localPosition += (Vector3)(Vector3.up * 10000f);
                    }
                    else
                    {
                        //Transform transform16 = obj13.transform;
                        crossL2T.localPosition += (Vector3)(Vector3.up * 10000f);
                    }
                }
                if (Physics.Linecast(baseT.position + vector3, (baseT.position + vector3) + vector5, out hit2, mask3.value))
                {
                    //GameObject obj14 = this.crossR1;
                    crossR1T.localPosition = Camera.main.WorldToScreenPoint(hit2.point);
                    //Transform transform17 = obj14.transform;
                    crossR1T.localPosition -= new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                    crossR1T.localRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(crossR1T.localPosition.y - (Input.mousePosition.y - (Screen.height * 0.5f)), crossR1T.localPosition.x - (Input.mousePosition.x - (Screen.width * 0.5f))) * 57.29578f);
                    //GameObject obj15 = this.crossR2;
                    crossR2T.localPosition = crossR1T.localPosition;
                    crossR2T.localRotation = crossR1T.localRotation;
                    if (hit2.distance > 120f)
                    {
                        //Transform transform18 = obj14.transform;
                        crossR1T.localPosition += (Vector3)(Vector3.up * 10000f);
                    }
                    else
                    {
                        //Transform transform19 = obj15.transform;
                        crossR2T.localPosition += (Vector3)(Vector3.up * 10000f);
                    }
                }
            }
        }
    }
    

    private void showFlareCD2()
    {
        if (this.cachedSprites["UIflare1"] != null)
        {
            this.cachedSprites["UIflare1"].fillAmount = (this.flareTotalCD - this.flare1CD) / this.flareTotalCD;
            this.cachedSprites["UIflare2"].fillAmount = (this.flareTotalCD - this.flare2CD) / this.flareTotalCD;
            this.cachedSprites["UIflare3"].fillAmount = (this.flareTotalCD - this.flare3CD) / this.flareTotalCD;
        }
    }
    
    private void showGas2()
    {
        float num = this.currentGas / this.totalGas;
        float num2 = this.currentBladeSta / this.totalBladeSta;
        this.cachedSprites["gasL1"].fillAmount = this.currentGas / this.totalGas;
        this.cachedSprites["gasR1"].fillAmount = this.currentGas / this.totalGas;
        if (!this.useGun)
        {
            this.cachedSprites["bladeCL"].fillAmount = this.currentBladeSta / this.totalBladeSta;
            this.cachedSprites["bladeCR"].fillAmount = this.currentBladeSta / this.totalBladeSta;
            if (num <= 0f)
            {
                this.cachedSprites["gasL"].color = Color.red;
                this.cachedSprites["gasR"].color = Color.red;
            }
            else if (num < 0.3f)
            {
                this.cachedSprites["gasL"].color = Color.yellow;
                this.cachedSprites["gasR"].color = Color.yellow;
            }
            else
            {
                this.cachedSprites["gasL"].color = Color.white;
                this.cachedSprites["gasR"].color = Color.white;
            }
            if (num2 <= 0f)
            {
                this.cachedSprites["bladel1"].color = Color.red;
                this.cachedSprites["blader1"].color = Color.red;
            }
            else if (num2 < 0.3f)
            {
                this.cachedSprites["bladel1"].color = Color.yellow;
                this.cachedSprites["blader1"].color = Color.yellow;
            }
            else
            {
                this.cachedSprites["bladel1"].color = Color.white;
                this.cachedSprites["blader1"].color = Color.white;
            }
            if (this.currentBladeNum <= 4)
            {
                this.cachedSprites["bladel5"].enabled = false;
                this.cachedSprites["blader5"].enabled = false;
            }
            else
            {
                this.cachedSprites["bladel5"].enabled = true;
                this.cachedSprites["blader5"].enabled = true;
            }
            if (this.currentBladeNum <= 3)
            {
                this.cachedSprites["bladel4"].enabled = false;
                this.cachedSprites["blader4"].enabled = false;
            }
            else
            {
                this.cachedSprites["bladel4"].enabled = true;
                this.cachedSprites["blader4"].enabled = true;
            }
            if (this.currentBladeNum <= 2)
            {
                this.cachedSprites["bladel3"].enabled = false;
                this.cachedSprites["blader3"].enabled = false;
            }
            else
            {
                this.cachedSprites["bladel3"].enabled = true;
                this.cachedSprites["blader3"].enabled = true;
            }
            if (this.currentBladeNum <= 1)
            {
                this.cachedSprites["bladel2"].enabled = false;
                this.cachedSprites["blader2"].enabled = false;
            }
            else
            {
                this.cachedSprites["bladel2"].enabled = true;
                this.cachedSprites["blader2"].enabled = true;
            }
            if (this.currentBladeNum <= 0)
            {
                this.cachedSprites["bladel1"].enabled = false;
                this.cachedSprites["blader1"].enabled = false;
            }
            else
            {
                this.cachedSprites["bladel1"].enabled = true;
                this.cachedSprites["blader1"].enabled = true;
            }
        }
        else
        {
            if (this.leftGunHasBullet)
            {
                this.cachedSprites["bulletL"].enabled = true;
            }
            else
            {
                this.cachedSprites["bulletL"].enabled = false;
            }
            if (this.rightGunHasBullet)
            {
                this.cachedSprites["bulletR"].enabled = true;
            }
            else
            {
                this.cachedSprites["bulletR"].enabled = false;
            }
        }
    }

    [RPC]
    private void showHitDamage()
    {
        //GameObject target = BRM.CacheGameObject.Find("LabelScore");
        if (FengGameManagerMKII.LabelScore != null)
        {
            this.speed = Mathf.Max(10f, this.speed);
            FengGameManagerMKII.LabelScoreUI.text = this.speed.ToString();
            FengGameManagerMKII.LabelScoreT.localScale = Vector3.zero;
            this.speed = (int)(this.speed * 0.1f);
            this.speed = Mathf.Clamp(this.speed, 40f, 150f);
            iTween.Stop(FengGameManagerMKII.LabelScore);
            object[] args = new object[] { "x", this.speed, "y", this.speed, "z", this.speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(FengGameManagerMKII.LabelScore, iTween.Hash(args));
            object[] objArray2 = new object[] { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(FengGameManagerMKII.LabelScore, iTween.Hash(objArray2));
        }
    }

    private void showSkillCD()
    {
        if (this.skillCD != null)
        {
            this.skillCD.GetComponent<UISprite>().fillAmount = (this.skillCDLast - this.skillCDDuration) / this.skillCDLast;
        }
    }

    [RPC]
    public void SpawnCannonRPC(string settings, PhotonMessageInfo info)
    {
        if ((info.sender.isMasterClient && basePV.isMine) && (this.myCannon == null))
        {
            if ((this.myHorse != null) && this.isMounted)
            {
                this.getOffHorse();
            }
            this.idle();
            if (this.bulletLeft != null)
            {
                this.LBullet.removeMe();
            }
            if (this.bulletRight != null)
            {
                this.RBullet.removeMe();
            }
            if ((this.smoke_3dmg.enableEmission && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)) && basePV.isMine)
            {
                object[] parameters = new object[] { false };
                basePV.RPC("net3DMGSMOKE", PhotonTargets.Others, parameters);
            }
            this.smoke_3dmg.enableEmission = false;
            baseR.velocity = Vector3.zero;
            string[] strArray = settings.Split(new char[] { ',' });
            if (strArray.Length > 15)
            {
                this.myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])), 0);
            }
            else
            {
                this.myCannon = PhotonNetwork.Instantiate("RCAsset/" + strArray[1], new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0);
            }
            this.myCannonBase = this.myCannon.transform;
            this.myCannonPlayer = this.myCannon.transform.Find("PlayerPoint");
            this.isCannon = true;
            this.myCannon.GetComponent<Cannon>().myHero = this;
            this.myCannonRegion = null;
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(this.myCannon.transform.Find("Barrel").Find("FiringPoint").gameObject, true, false);
            Camera.main.fieldOfView = 55f;
            basePV.RPC("SetMyCannon", PhotonTargets.OthersBuffered, new object[] { this.myCannon.GetPhotonView().viewID });
            this.skillCDLastCannon = this.skillCDLast;
            this.skillCDLast = 3.5f;
            this.skillCDDuration = 3.5f;
        }
    }

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && this.basePV == null)
        {
            this.cache();
        }
        FengGameManagerMKII.instance.addHero(this, this.baseG);
        if (((LevelInfo.getInfo(FengGameManagerMKII.level).horse || (GameSettings.horseMode == 1)) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)) && basePV.isMine)
        {
            this.myHorse = PhotonNetwork.Instantiate("horse", baseT.position + ((Vector3)(Vector3.up * 5f)), baseT.rotation, 0);
            this.myHorse.GetComponent<Horse>().myHero = baseG;
            this.myHorse.GetComponent<TITAN_CONTROLLER>().isHorse = true;
        }
        //float num2 = RCextensions.returnFloatFromObject(basePV.owner.customProperties[PhotonPlayerProperty.RCBombR]);
        //float num3 = RCextensions.returnFloatFromObject(basePV.owner.customProperties[PhotonPlayerProperty.RCBombG]);
        //float num4 = RCextensions.returnFloatFromObject(basePV.owner.customProperties[PhotonPlayerProperty.RCBombB]);
        this.sparks = baseT.Find("slideSparks").GetComponent<ParticleSystem>();
        this.smoke_3dmg = baseT.Find("3dmg_smoke").GetComponent<ParticleSystem>();
        //this.smoke_3dmg.startColor = new Color(0f, 0f, 0f, 1f);
        baseT.localScale = new Vector3(this.myScale, this.myScale, this.myScale);
        this.facingDirection = baseT.rotation.eulerAngles.y;
        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
        this.smoke_3dmg.enableEmission = false;
        this.sparks.enableEmission = false;
        this.speedFXPS = this.speedFX1.GetComponent<ParticleSystem>();
        this.speedFXPS.enableEmission = false;
        this.hookRefL1T = this.hookRefL1.transform;
        this.hookRefL2T = this.hookRefL2.transform;
        this.hookRefR1T = this.hookRefR1.transform;
        this.hookRefR2T = this.hookRefR2.transform;
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.MULTIPLAYER)
        {
            if (Minimap.instance != null)
            {
                Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.green, false, true, Minimap.IconStyle.CIRCLE);
            }
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                int iD = basePV.owner.ID;
                if (FengGameManagerMKII.heroHash.ContainsKey(iD))
                {
                    FengGameManagerMKII.heroHash[iD] = this;
                }
                else
                {
                    FengGameManagerMKII.heroHash.Add(iD, this);
                }
            }
            GameObject obj2 = BRM.CacheGameObject.Find("UI_IN_GAME");
            this.myNetWorkName = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("UI/LabelNameOverHead"));
            this.myNetWorkName.name = "LabelNameOverHead";
            this.myNetWorkNameT = this.myNetWorkName.transform;
            this.myNetWorkNameT.parent = obj2.GetComponent<UIReferArray>().panels[0].transform;
            this.myNetWorkNameT.localScale = new Vector3(14f, 14f, 14f);
            UILabel component = this.myNetWorkName.GetComponent<UILabel>();
            component.text = string.Empty;
            if (basePV.isMine)
            {
                if (Minimap.instance != null)
                {
                    Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.green, false, true, Minimap.IconStyle.CIRCLE);
                }
                sync.PhotonCamera = true;
                basePV.RPC("SetMyPhotonCamera", PhotonTargets.OthersBuffered, new object[] { PlayerPrefs.GetFloat("cameraDistance") + 0.3f });
                for (int i = 0; i < 10; i++)
                {
                    DashRampage.Add(new Bullet());
                }
            }
            else
            {
                bool flag2 = false;
                if (basePV.owner.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    switch (RCextensions.returnIntFromObject(basePV.owner.customProperties[PhotonPlayerProperty.RCteam]))
                    {
                        case 1:
                            flag2 = true;
                            if (Minimap.instance != null)
                            {
                                Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.cyan, false, true, Minimap.IconStyle.CIRCLE);
                            }
                            break;

                        case 2:
                            flag2 = true;
                            if (Minimap.instance != null)
                            {
                                Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.magenta, false, true, Minimap.IconStyle.CIRCLE);
                            }
                            break;
                    }
                }
                if (RCextensions.returnIntFromObject(basePV.owner.customProperties[PhotonPlayerProperty.team]) == 2)
                {
                    component.text = "[FF0000]AHSS\n[FFFFFF]";
                    if (!flag2 && (Minimap.instance != null))
                    {
                        Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.red, false, true, Minimap.IconStyle.CIRCLE);
                    }
                }
                else if (!flag2 && (Minimap.instance != null))
                {
                    Minimap.instance.TrackGameObjectOnMinimap(baseG, Color.blue, false, true, Minimap.IconStyle.CIRCLE);
                }
            }
            string guildname = this.basePV.owner.guildname;
            string name = this.basePV.owner.uiname;
            if (guildname != string.Empty)
            {
                UILabel uilabel = component;
                string text = uilabel.text;
                uilabel.text = string.Concat(new string[]
                {
                text,
                "[FFFF00]",
                guildname.Length > 100 ? guildname.Substring(0, 20) : guildname,
                "\n[FFFFFF]",
                name
                });
            }
            else
            {
                UILabel uilabel2 = component;
                uilabel2.text += name.Length > 100 ? name.Substring(0, 20) : name;
            }
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && !basePV.isMine)
        {
            baseG.layer = LayerMask.NameToLayer("NetworkObject");
            if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
            {
                Transform obj3 = ((GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("flashlight"))).transform;
                obj3.parent = baseT;
                obj3.position = baseT.position + Vector3.up;
                obj3.rotation = Quaternion.Euler(353f, 0f, 0f);
            }
            this.setup.init();
            this.setup.myCostume = new HeroCostume();
            this.setup.myCostume = CostumeConeveter.PhotonDataToHeroCostume2(basePV.owner);
            this.setup.setCharacterComponent();
            UnityEngine.Object.Destroy(this.checkBoxLeft);
            UnityEngine.Object.Destroy(this.checkBoxRight);
            UnityEngine.Object.Destroy(this.leftbladetrail);
            UnityEngine.Object.Destroy(this.rightbladetrail);
            UnityEngine.Object.Destroy(this.leftbladetrail2);
            UnityEngine.Object.Destroy(this.rightbladetrail2);
            this.hasspawn = true;
        }
        else
        {
            this.triggerLeft = this.checkBoxLeft.GetComponent<TriggerColliderWeapon>();
            this.triggerRight = this.checkBoxRight.GetComponent<TriggerColliderWeapon>();
            this.loadskin();
            this.hasspawn = true;
            base.StartCoroutine(this.reloadSky());
        }
        this.bombImmune = false;
        if (GameSettings.bombMode == 1)
        {
            this.bombImmune = true;
            base.StartCoroutine(this.stopImmunity());
        }
    }

    public IEnumerator stopImmunity()
    {
        yield return new WaitForSeconds(5f);
        this.bombImmune = false;
    }
    

    private void suicide2()
    {
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            this.netDieLocal((Vector3)(baseR.velocity * 50f), false, -1, string.Empty, true);
            FengGameManagerMKII.instance.needChooseSide = true;
            FengGameManagerMKII.instance.justSuicide = true;
        }
    }

    private void throwBlades()
    {
        Transform transform = this.setup.part_blade_l.transform;
        Transform transform2 = this.setup.part_blade_r.transform;
        GameObject obj2 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_blade_l"), transform.position, transform.rotation);
        GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_blade_r"), transform2.position, transform2.rotation);
        obj2.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        obj3.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
        Vector3 force = (baseT.forward + ((Vector3)(baseT.up * 2f))) - baseT.right;
        obj2.rigidbody.AddForce(force, ForceMode.Impulse);
        Vector3 vector2 = (baseT.forward + ((Vector3)(baseT.up * 2f))) + baseT.right;
        obj3.rigidbody.AddForce(vector2, ForceMode.Impulse);
        Vector3 torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
        torque.Normalize();
        obj2.rigidbody.AddTorque(torque);
        torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
        torque.Normalize();
        obj3.rigidbody.AddTorque(torque);
        this.setup.part_blade_l.SetActive(false);
        this.setup.part_blade_r.SetActive(false);
        this.currentBladeNum--;
        if (this.currentBladeNum == 0)
        {
            this.currentBladeSta = 0f;
        }
        if (this.state == HERO_STATE.Attack)
        {
            this.falseAttack();
        }
    }

    public void ungrabbed()
    {
        this.facingDirection = 0f;
        this.targetRotation = Quaternion.Euler(0f, 0f, 0f);
        baseT.parent = null;
        base.GetComponent<CapsuleCollider>().isTrigger = false;
        this.state = HERO_STATE.Idle;
    }

    private void unmounted()
    {
        this.myHorse.GetComponent<Horse>().unmounted();
        this.isMounted = false;
    }
    
    public void update()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing)
        {
            if (this.invincible > 0f)
            {
                this.invincible -= Time.deltaTime;
            }
            if (!this.hasDied)
            {
                if (this.titanForm && (this.eren_titan != null))
                {
                    baseT.position = this.eren_titan.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position;
                    sync.disabled = true;
                }
                else if (this.isCannon && (this.myCannon != null))
                {
                    this.updateCannon();
                    sync.disabled = true;
                }
                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
                {
                    if (this.myCannonRegion != null)
                    {
                        FengGameManagerMKII.instance.ShowHUDInfoCenter("Press 'Cannon Mount' key to use Cannon.");
                        if (FengGameManagerMKII.inputRC.isInputCannonDown(InputCodeRC.cannonMount))
                        {
                            this.myCannonRegion.photonView.RPC("RequestControlRPC", PhotonTargets.MasterClient, new object[] { basePV.viewID });
                        }
                    }
                    if ((this.state == HERO_STATE.Grab) && !this.useGun)
                    {
                        if (this.skillId == "jean")
                        {
                            if (((this.state != HERO_STATE.Attack) && (FengCustomInputs.Inputs.isInputDown[InputCode.attack0] || FengCustomInputs.Inputs.isInputDown[InputCode.attack1])) && ((this.escapeTimes > 0) && !baseA.IsPlaying("grabbed_jean")))
                            {
                                this.playAnimation("grabbed_jean");
                                baseA["grabbed_jean"].time = 0f;
                                this.escapeTimes--;
                            }
                            if ((baseA.IsPlaying("grabbed_jean") && (baseA["grabbed_jean"].normalizedTime > 0.64f)) && (this.titanWhoGrabMe.GetComponent<TITAN>() != null))
                            {
                                this.ungrabbed();
                                baseR.velocity = (Vector3)(Vector3.up * 30f);
                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                {
                                    this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                }
                                else
                                {
                                    basePV.RPC("netSetIsGrabbedFalse", PhotonTargets.All, new object[0]);
                                    if (PhotonNetwork.isMasterClient)
                                    {
                                        this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                    }
                                    else
                                    {
                                        PhotonView.Find(this.titanWhoGrabMeID).RPC("grabbedTargetEscape", PhotonTargets.MasterClient, new object[0]);
                                    }
                                }
                            }
                        }
                        else if (this.skillId == "eren")
                        {
                            this.showSkillCD();
                            if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) || ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) && !IN_GAME_MAIN_CAMERA.isPausing))
                            {
                                this.calcSkillCD();
                                this.calcFlareCD();
                            }
                            if (FengCustomInputs.Inputs.isInputDown[InputCode.attack1])
                            {
                                bool flag2 = false;
                                if ((this.skillCDDuration > 0f) || flag2)
                                {
                                    flag2 = true;
                                }
                                else
                                {
                                    this.skillCDDuration = this.skillCDLast;
                                    if ((this.skillId == "eren") && (this.titanWhoGrabMe.GetComponent<TITAN>() != null))
                                    {
                                        this.ungrabbed();
                                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                        {
                                            this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                        }
                                        else
                                        {
                                            basePV.RPC("netSetIsGrabbedFalse", PhotonTargets.All, new object[0]);
                                            if (PhotonNetwork.isMasterClient)
                                            {
                                                this.titanWhoGrabMe.GetComponent<TITAN>().grabbedTargetEscape();
                                            }
                                            else
                                            {
                                                PhotonView.Find(this.titanWhoGrabMeID).photonView.RPC("grabbedTargetEscape", PhotonTargets.MasterClient, new object[0]);
                                            }
                                        }
                                        this.erenTransform();
                                    }
                                }
                            }
                        }
                    }
                    else if (!this.titanForm && !this.isCannon)
                    {
                        this.bufferUpdate();
                        this.updateExt();
                        if (!this.grounded && (this.state != HERO_STATE.AirDodge))
                        {
                            if (((int)FengGameManagerMKII.settings[0xb5]) == 1)
                            {
                                this.checkDashRebind();
                            }
                            else
                            {
                                this.checkDashDoubleTap();
                            }
                            if (this.dashD)
                            {
                                this.dashD = false;
                                this.dash(0f, -1f);
                                return;
                            }
                            if (this.dashU)
                            {
                                this.dashU = false;
                                this.dash(0f, 1f);
                                return;
                            }
                            if (this.dashL)
                            {
                                this.dashL = false;
                                this.dash(-1f, 0f);
                                return;
                            }
                            if (this.dashR)
                            {
                                this.dashR = false;
                                this.dash(1f, 0f);
                                return;
                            }
                        }
                        if (this.grounded && ((this.state == HERO_STATE.Idle) || (this.state == HERO_STATE.Slide)))
                        {
                            if (!((!FengCustomInputs.Inputs.isInputDown[InputCode.jump] || baseA.IsPlaying("jump")) || baseA.IsPlaying("horse_geton")))
                            {
                                this.idle();
                                this.crossFade("jump", 0.1f);
                                this.sparks.enableEmission = false;
                            }
                            if (((FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount) && !baseA.IsPlaying("jump")) && !baseA.IsPlaying("horse_geton")) && (((this.myHorse != null) && !this.isMounted) && (Vector3.Distance(this.myHorse.transform.position, baseT.position) < 15f)))
                            {
                                this.getOnHorse();
                            }
                            if (!((!FengCustomInputs.Inputs.isInputDown[InputCode.dodge] || baseA.IsPlaying("jump")) || baseA.IsPlaying("horse_geton")))
                            {
                                this.dodge2(false);
                                return;
                            }
                        }
                        switch (state)
                        {
                            case HERO_STATE.Idle:
                                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare1])
                                {
                                    this.shootFlare(1);
                                }
                                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare2])
                                {
                                    this.shootFlare(2);
                                }
                                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare3])
                                {
                                    this.shootFlare(3);
                                }
                                if (FengCustomInputs.Inputs.isInputDown[InputCode.restart])
                                {
                                    this.suicide2();
                                }
                                if (((this.myHorse != null) && this.isMounted) && FengGameManagerMKII.inputRC.isInputHorseDown(InputCodeRC.horseMount))
                                {
                                    this.getOffHorse();
                                }
                                if (((baseA.IsPlaying(this.standAnimation) || !this.grounded) && FengCustomInputs.Inputs.isInputDown[InputCode.reload]) && ((!this.useGun || (GameSettings.ahssReload != 1)) || this.grounded))
                                {
                                    this.changeBlade();
                                    return;
                                }
                                if (baseA.IsPlaying(this.standAnimation) && FengCustomInputs.Inputs.isInputDown[InputCode.salute])
                                {
                                    this.salute();
                                    return;
                                }
                                if ((!this.isMounted && (FengCustomInputs.Inputs.isInputDown[InputCode.attack0] || FengCustomInputs.Inputs.isInputDown[InputCode.attack1])) && !this.useGun)
                                {
                                    bool flag3 = false;
                                    if (FengCustomInputs.Inputs.isInputDown[InputCode.attack1])
                                    {
                                        if ((this.skillCDDuration > 0f) || flag3)
                                        {
                                            flag3 = true;
                                        }
                                        else
                                        {
                                            this.skillCDDuration = this.skillCDLast;
                                            switch (skillId)
                                            {
                                                case "eren":
                                                    this.erenTransform();
                                                    break;

                                                case "marco":
                                                    if (this.IsGrounded())
                                                    {
                                                        this.attackAnimation = (UnityEngine.Random.Range(0, 2) != 0) ? "special_marco_1" : "special_marco_0";
                                                        this.playAnimation(this.attackAnimation);
                                                    }
                                                    else
                                                    {
                                                        flag3 = true;
                                                        this.skillCDDuration = 0f;
                                                    }
                                                    break;

                                                case "armin":
                                                    if (this.IsGrounded())
                                                    {
                                                        this.attackAnimation = "special_armin";
                                                        this.playAnimation("special_armin");
                                                    }
                                                    else
                                                    {
                                                        flag3 = true;
                                                        this.skillCDDuration = 0f;
                                                    }
                                                    break;
                                                case "sasha":
                                                    //if (this.IsGrounded())
                                                    //{
                                                    this.attackAnimation = "special_sasha";
                                                    this.playAnimation("special_sasha");
                                                    this.currentBuff = BUFF.SpeedUp;
                                                    this.buffTime = 10f;
                                                    //}
                                                    //else
                                                    //{
                                                    //    flag3 = true;
                                                    //    this.skillCDDuration = 0f;
                                                    //}
                                                    break;
                                                case "mikasa":
                                                    this.attackAnimation = "attack3_1";
                                                    this.playAnimation("attack3_1");
                                                    baseR.velocity = (Vector3)(Vector3.up * 10f);
                                                    break;
                                                case "levi":
                                                    RaycastHit hit;
                                                    this.attackAnimation = "attack5";
                                                    this.playAnimation("attack5");
                                                    baseR.velocity += (Vector3)(Vector3.up * 5f);
                                                    Ray ray = IN_GAME_MAIN_CAMERA.mainC.ScreenPointToRay(Input.mousePosition);
                                                    //LayerMask mask = ((int) 1) << LayerMask.NameToLayer("Ground");
                                                    //LayerMask mask2 = ((int) 1) << LayerMask.NameToLayer("EnemyBox");
                                                    //LayerMask mask3 = mask2 | mask;
                                                    if (Physics.Raycast(ray, out hit, 1E+07f, Layer.GroundEnemy.value))
                                                    {
                                                        if (this.bulletRight != null)
                                                        {
                                                            this.RBullet.disable();
                                                            this.releaseIfIHookSb();
                                                        }
                                                        this.dashDirection = hit.point - baseT.position;
                                                        this.launchRightRope(hit, true, 1);
                                                        this.rope.Play();
                                                    }
                                                    this.facingDirection = Mathf.Atan2(this.dashDirection.x, this.dashDirection.z) * 57.29578f;
                                                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                                    this.attackLoop = 3;
                                                    break;
                                                case "petra":
                                                    RaycastHit hit2;
                                                    this.attackAnimation = "special_petra";
                                                    this.playAnimation("special_petra");
                                                    baseR.velocity += (Vector3)(Vector3.up * 5f);
                                                    Ray ray2 = IN_GAME_MAIN_CAMERA.mainC.ScreenPointToRay(Input.mousePosition);
                                                    //LayerMask mask4 = ((int) 1) << LayerMask.NameToLayer("Ground");
                                                    //LayerMask mask5 = ((int) 1) << LayerMask.NameToLayer("EnemyBox");
                                                    //LayerMask mask6 = mask5 | mask4;
                                                    if (Physics.Raycast(ray2, out hit2, 1E+07f, Layer.GroundEnemy.value))
                                                    {
                                                        if (this.bulletRight != null)
                                                        {
                                                            this.RBullet.disable();
                                                            this.releaseIfIHookSb();
                                                        }
                                                        if (this.bulletLeft != null)
                                                        {
                                                            this.LBullet.disable();
                                                            this.releaseIfIHookSb();
                                                        }
                                                        this.dashDirection = hit2.point - baseT.position;
                                                        this.launchLeftRope(hit2, true, 0);
                                                        this.launchRightRope(hit2, true, 0);
                                                        this.rope.Play();
                                                    }
                                                    this.facingDirection = Mathf.Atan2(this.dashDirection.x, this.dashDirection.z) * 57.29578f;
                                                    this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                                    this.attackLoop = 3;
                                                    break;
                                                default:
                                                    if (this.needLean)
                                                    {

                                                        if (this.leanLeft)
                                                        {
                                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                                        }
                                                        else
                                                        {
                                                            this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        this.attackAnimation = "attack1";
                                                    }
                                                    this.playAnimation(this.attackAnimation);
                                                    break;
                                            }
                                        }
                                    }
                                    else if (FengCustomInputs.Inputs.isInputDown[InputCode.attack0])
                                    {
                                        if (this.needLean)
                                        {
                                            if (FengCustomInputs.Inputs.isInput[InputCode.left])
                                            {
                                                this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                            }
                                            else if (FengCustomInputs.Inputs.isInput[InputCode.right])
                                            {
                                                this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                            }
                                            else if (this.leanLeft)
                                            {
                                                this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_l1" : "attack1_hook_l2";
                                            }
                                            else
                                            {
                                                this.attackAnimation = (UnityEngine.Random.Range(0, 100) >= 50) ? "attack1_hook_r1" : "attack1_hook_r2";
                                            }
                                        }
                                        else if (FengCustomInputs.Inputs.isInput[InputCode.left])
                                        {
                                            this.attackAnimation = "attack2";
                                        }
                                        else if (FengCustomInputs.Inputs.isInput[InputCode.right])
                                        {
                                            this.attackAnimation = "attack1";
                                        }
                                        else if (this.lastHook != null)
                                        {
                                            if (this.lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck") != null)
                                            {
                                                this.attackAccordingToTarget(this.lastHook.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck"));
                                            }
                                            else
                                            {
                                                flag3 = true;
                                            }
                                        }
                                        else if ((this.bulletLeft != null) && (this.bulletLT.parent != null))
                                        {
                                            Transform a = this.bulletLT.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                            if (a != null)
                                            {
                                                this.attackAccordingToTarget(a);
                                            }
                                            else
                                            {
                                                this.attackAccordingToMouse();
                                            }
                                        }
                                        else if ((this.bulletRight != null) && (this.bulletRT.parent != null))
                                        {
                                            Transform transform2 = this.bulletRT.parent.transform.root.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                            if (transform2 != null)
                                            {
                                                this.attackAccordingToTarget(transform2);
                                            }
                                            else
                                            {
                                                this.attackAccordingToMouse();
                                            }
                                        }
                                        else
                                        {
                                            GameObject obj2 = this.findNearestTitan();
                                            if (obj2 != null)
                                            {
                                                Transform transform3 = obj2.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                                                if (transform3 != null)
                                                {
                                                    this.attackAccordingToTarget(transform3);
                                                }
                                                else
                                                {
                                                    this.attackAccordingToMouse();
                                                }
                                            }
                                            else
                                            {
                                                this.attackAccordingToMouse();
                                            }
                                        }
                                    }
                                    if (!flag3)
                                    {
                                        this.triggerLeft.clearHits();
                                        this.triggerRight.clearHits();
                                        if (this.grounded)
                                        {
                                            baseR.AddForce((Vector3)(baseG.transform.forward * 200f));
                                        }
                                        this.playAnimation(this.attackAnimation);
                                        baseA[this.attackAnimation].time = 0f;
                                        this.buttonAttackRelease = false;
                                        this.state = HERO_STATE.Attack;
                                        if ((this.grounded || (this.attackAnimation == "attack3_1")) || ((this.attackAnimation == "attack5") || (this.attackAnimation == "special_petra")))
                                        {
                                            this.attackReleased = true;
                                            this.buttonAttackRelease = true;
                                        }
                                        else
                                        {
                                            this.attackReleased = false;
                                        }
                                        this.sparks.enableEmission = false;
                                    }
                                }
                                if (this.useGun)
                                {
                                    if (FengCustomInputs.Inputs.isInput[InputCode.attack1])
                                    {
                                        this.leftArmAim = true;
                                        this.rightArmAim = true;
                                    }
                                    else if (FengCustomInputs.Inputs.isInput[InputCode.attack0])
                                    {
                                        if (this.leftGunHasBullet)
                                        {
                                            this.leftArmAim = true;
                                            this.rightArmAim = false;
                                        }
                                        else
                                        {
                                            this.leftArmAim = false;
                                            if (this.rightGunHasBullet)
                                            {
                                                this.rightArmAim = true;
                                            }
                                            else
                                            {
                                                this.rightArmAim = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.leftArmAim = false;
                                        this.rightArmAim = false;
                                    }
                                    if (this.leftArmAim || this.rightArmAim)
                                    {
                                        RaycastHit hit3;
                                        Ray ray3 = IN_GAME_MAIN_CAMERA.mainC.ScreenPointToRay(Input.mousePosition);
                                        //LayerMask mask7 = ((int) 1) << LayerMask.NameToLayer("Ground");
                                        //LayerMask mask8 = ((int) 1) << LayerMask.NameToLayer("EnemyBox");
                                        //LayerMask mask9 = mask8 | mask7;
                                        if (Physics.Raycast(ray3, out hit3, 1E+07f, Layer.GroundEnemy.value))
                                        {
                                            gunTarget = hit3.point;
                                        }
                                    }
                                    bool flag4 = false;
                                    bool flag5 = false;
                                    bool flag6 = false;
                                    if (FengCustomInputs.Inputs.isInputUp[InputCode.attack1] && (this.skillId != "bomb"))
                                    {
                                        if (this.leftGunHasBullet && this.rightGunHasBullet)
                                        {
                                            if (this.grounded)
                                            {
                                                this.attackAnimation = "AHSS_shoot_both";
                                            }
                                            else
                                            {
                                                this.attackAnimation = "AHSS_shoot_both_air";
                                            }
                                            flag4 = true;
                                        }
                                        else if (!(this.leftGunHasBullet || this.rightGunHasBullet))
                                        {
                                            flag5 = true;
                                        }
                                        else
                                        {
                                            flag6 = true;
                                        }
                                    }
                                    if (flag6 || FengCustomInputs.Inputs.isInputUp[InputCode.attack0])
                                    {
                                        if (this.grounded)
                                        {
                                            if (this.leftGunHasBullet && this.rightGunHasBullet)
                                            {
                                                if (this.isLeftHandHooked)
                                                {
                                                    this.attackAnimation = "AHSS_shoot_r";
                                                }
                                                else
                                                {
                                                    this.attackAnimation = "AHSS_shoot_l";
                                                }
                                            }
                                            else if (this.leftGunHasBullet)
                                            {
                                                this.attackAnimation = "AHSS_shoot_l";
                                            }
                                            else if (this.rightGunHasBullet)
                                            {
                                                this.attackAnimation = "AHSS_shoot_r";
                                            }
                                        }
                                        else if (this.leftGunHasBullet && this.rightGunHasBullet)
                                        {
                                            if (this.isLeftHandHooked)
                                            {
                                                this.attackAnimation = "AHSS_shoot_r_air";
                                            }
                                            else
                                            {
                                                this.attackAnimation = "AHSS_shoot_l_air";
                                            }
                                        }
                                        else if (this.leftGunHasBullet)
                                        {
                                            this.attackAnimation = "AHSS_shoot_l_air";
                                        }
                                        else if (this.rightGunHasBullet)
                                        {
                                            this.attackAnimation = "AHSS_shoot_r_air";
                                        }
                                        if (this.leftGunHasBullet || this.rightGunHasBullet)
                                        {
                                            flag4 = true;
                                        }
                                        else
                                        {
                                            flag5 = true;
                                        }
                                    }
                                    if (flag4)
                                    {
                                        this.state = HERO_STATE.Attack;
                                        this.crossFade(this.attackAnimation, 0.05f);
                                        this.gunDummy.transform.position = baseT.position;
                                        this.gunDummy.transform.rotation = baseT.rotation;
                                        this.gunDummy.transform.LookAt(this.gunTarget);
                                        this.attackReleased = false;
                                        this.facingDirection = this.gunDummy.transform.rotation.eulerAngles.y;
                                        this.targetRotation = Quaternion.Euler(0f, this.facingDirection, 0f);
                                    }
                                    else if (flag5 && (this.grounded || ((LevelInfo.getInfo(FengGameManagerMKII.level).type != GAMEMODE.PVP_AHSS) && (GameSettings.ahssReload == 0))))
                                    {
                                        this.changeBlade();
                                    }
                                }
                                break;
                            case HERO_STATE.Attack:
                                if (!this.useGun)
                                {
                                    if (!FengCustomInputs.Inputs.isInput[InputCode.attack0])
                                    {
                                        this.buttonAttackRelease = true;
                                    }
                                    if (!this.attackReleased)
                                    {
                                        if (this.buttonAttackRelease)
                                        {
                                            this.continueAnimation();
                                            this.attackReleased = true;
                                        }
                                        else if (baseA[this.attackAnimation].normalizedTime >= 0.32f)
                                        {
                                            this.pauseAnimation();
                                        }
                                    }
                                    if ((this.attackAnimation == "attack3_1") && (this.currentBladeSta > 0f))
                                    {
                                        if (baseA[this.attackAnimation].normalizedTime >= 0.8f)
                                        {
                                            if (!this.triggerLeft.active_me)
                                            {
                                                this.triggerLeft.active_me = true;
                                                if (((int)FengGameManagerMKII.settings[0x5c]) == 0)
                                                {
                                                    this.leftbladetrail2.Activate();
                                                    this.rightbladetrail2.Activate();
                                                    this.leftbladetrail.Activate();
                                                    this.rightbladetrail.Activate();
                                                }
                                                baseR.velocity = (Vector3)(-Vector3.up * 30f);
                                            }
                                            if (!this.triggerRight.active_me)
                                            {
                                                this.triggerRight.active_me = true;
                                                this.slash.Play();
                                            }
                                        }
                                        else if (this.triggerLeft.active_me)
                                        {
                                            this.triggerLeft.active_me = false;
                                            this.triggerRight.active_me = false;
                                            this.triggerLeft.clearHits();
                                            this.triggerRight.clearHits();
                                            this.leftbladetrail.StopSmoothly(0.1f);
                                            this.rightbladetrail.StopSmoothly(0.1f);
                                            this.leftbladetrail2.StopSmoothly(0.1f);
                                            this.rightbladetrail2.StopSmoothly(0.1f);
                                        }
                                    }
                                    else
                                    {
                                        float num;
                                        float num2;
                                        if (this.currentBladeSta == 0f)
                                        {
                                            num2 = num = -1f;
                                        }
                                        else if (this.attackAnimation == "attack5")
                                        {
                                            num2 = 0.35f;
                                            num = 0.5f;
                                        }
                                        else if (this.attackAnimation == "special_petra")
                                        {
                                            num2 = 0.35f;
                                            num = 0.48f;
                                        }
                                        else if (this.attackAnimation == "special_armin")
                                        {
                                            num2 = 0.25f;
                                            num = 0.35f;
                                        }
                                        else if (this.attackAnimation == "attack4")
                                        {
                                            num2 = 0.6f;
                                            num = 0.9f;
                                        }
                                        else if (this.attackAnimation == "special_sasha")
                                        {
                                            num2 = num = -1f;
                                        }
                                        else
                                        {
                                            num2 = 0.5f;
                                            num = 0.85f;
                                        }
                                        if ((baseA[this.attackAnimation].normalizedTime > num2) && (baseA[this.attackAnimation].normalizedTime < num))
                                        {
                                            if (!this.triggerLeft.active_me)
                                            {
                                                this.triggerLeft.active_me = true;
                                                this.slash.Play();
                                                if (((int)FengGameManagerMKII.settings[0x5c]) == 0)
                                                {
                                                    this.leftbladetrail2.Activate();
                                                    this.rightbladetrail2.Activate();
                                                    this.leftbladetrail.Activate();
                                                    this.rightbladetrail.Activate();
                                                }
                                            }
                                            if (!this.triggerRight.active_me)
                                            {
                                                this.triggerRight.active_me = true;
                                            }
                                        }
                                        else if (this.triggerLeft.active_me)
                                        {
                                            this.triggerLeft.active_me = false;
                                            this.triggerRight.active_me = false;
                                            this.triggerLeft.clearHits();
                                            this.triggerRight.clearHits();
                                            this.leftbladetrail2.StopSmoothly(0.1f);
                                            this.rightbladetrail2.StopSmoothly(0.1f);
                                            this.leftbladetrail.StopSmoothly(0.1f);
                                            this.rightbladetrail.StopSmoothly(0.1f);
                                        }
                                        if ((this.attackLoop > 0) && (baseA[this.attackAnimation].normalizedTime > num))
                                        {
                                            this.attackLoop--;
                                            this.playAnimationAt(this.attackAnimation, num2);
                                        }
                                    }
                                    if (baseA[this.attackAnimation].normalizedTime >= 1f)
                                    {
                                        if ((this.attackAnimation == "special_marco_0") || (this.attackAnimation == "special_marco_1"))
                                        {
                                            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                            {
                                                if (!PhotonNetwork.isMasterClient)
                                                {
                                                    object[] parameters = new object[] { 5f, 100f };
                                                    basePV.RPC("netTauntAttack", PhotonTargets.MasterClient, parameters);
                                                }
                                                else
                                                {
                                                    this.netTauntAttack(5f, 100f);
                                                }
                                            }
                                            else
                                            {
                                                this.netTauntAttack(5f, 100f);
                                            }
                                            this.falseAttack();
                                            this.idle();
                                        }
                                        else if (this.attackAnimation == "special_armin")
                                        {
                                            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                                            {
                                                if (!PhotonNetwork.isMasterClient)
                                                {
                                                    basePV.RPC("netlaughAttack", PhotonTargets.MasterClient, new object[0]);
                                                }
                                                else
                                                {
                                                    this.netlaughAttack();
                                                }
                                            }
                                            else
                                            {
                                                foreach (GameObject obj3 in GameObject.FindGameObjectsWithTag("titan"))
                                                {
                                                    if (((Vector3.Distance(obj3.transform.position, baseT.position) < 50f) && (Vector3.Angle(obj3.transform.forward, baseT.position - obj3.transform.position) < 90f)) && (obj3.GetComponent<TITAN>() != null))
                                                    {
                                                        obj3.GetComponent<TITAN>().beLaughAttacked();
                                                    }
                                                }
                                            }
                                            this.falseAttack();
                                            this.idle();
                                        }
                                        else if (this.attackAnimation == "attack3_1")
                                        {
                                            baseR.velocity -= (Vector3)((Vector3.up * Time.deltaTime) * 30f);
                                        }
                                        else
                                        {
                                            this.falseAttack();
                                            this.idle();
                                        }
                                    }
                                    if (baseA.IsPlaying("attack3_2") && (baseA["attack3_2"].normalizedTime >= 1f))
                                    {
                                        this.falseAttack();
                                        this.idle();
                                    }
                                }
                                else
                                {
                                    baseT.rotation = Quaternion.Lerp(baseT.rotation, this.gunDummy.transform.rotation, Time.deltaTime * 30f);
                                    if (!this.attackReleased && (baseA[this.attackAnimation].normalizedTime > 0.167f))
                                    {
                                        GameObject obj4;
                                        this.attackReleased = true;
                                        bool flag7 = false;
                                        if ((this.attackAnimation == "AHSS_shoot_both") || (this.attackAnimation == "AHSS_shoot_both_air"))
                                        {
                                            flag7 = true;
                                            this.leftGunHasBullet = false;
                                            this.rightGunHasBullet = false;
                                            baseR.AddForce((Vector3)(-baseT.forward * 1000f), ForceMode.Acceleration);
                                        }
                                        else
                                        {
                                            if ((this.attackAnimation == "AHSS_shoot_l") || (this.attackAnimation == "AHSS_shoot_l_air"))
                                            {
                                                this.leftGunHasBullet = false;
                                            }
                                            else
                                            {
                                                this.rightGunHasBullet = false;
                                            }
                                            baseR.AddForce((Vector3)(-baseT.forward * 600f), ForceMode.Acceleration);
                                        }
                                        baseR.AddForce((Vector3)(Vector3.up * 200f), ForceMode.Acceleration);
                                        string prefabName = "FX/shotGun";
                                        if (flag7)
                                        {
                                            prefabName = "FX/shotGun 1";
                                        }
                                        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && basePV.isMine)
                                        {
                                            obj4 = PhotonNetwork.Instantiate(prefabName, (Vector3)((baseT.position + (baseT.up * 0.8f)) - (baseT.right * 0.1f)), baseT.rotation, 0);
                                            if (obj4.GetComponent<EnemyfxIDcontainer>() != null)
                                            {
                                                obj4.GetComponent<EnemyfxIDcontainer>().myOwnerViewID = basePV.viewID;
                                            }
                                        }
                                        else
                                        {
                                            obj4 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load(prefabName), (Vector3)((baseT.position + (baseT.up * 0.8f)) - (baseT.right * 0.1f)), baseT.rotation);
                                        }
                                    }
                                    if (baseA[this.attackAnimation].normalizedTime >= 1f)
                                    {
                                        this.falseAttack();
                                        this.idle();
                                    }
                                    if (!baseA.IsPlaying(this.attackAnimation))
                                    {
                                        this.falseAttack();
                                        this.idle();
                                    }
                                }
                                break;
                            case HERO_STATE.ChangeBlade:
                                if (this.useGun)
                                {
                                    if (baseA[this.reloadAnimation].normalizedTime > 0.22f)
                                    {
                                        if (!(this.leftGunHasBullet || !this.setup.part_blade_l.activeSelf))
                                        {
                                            this.setup.part_blade_l.SetActive(false);
                                            Transform transform = this.setup.part_blade_l.transform;
                                            GameObject obj5 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_l"), transform.position, transform.rotation);
                                            obj5.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
                                            Vector3 force = ((Vector3)((-baseT.forward * 10f) + (baseT.up * 5f))) - baseT.right;
                                            obj5.rigidbody.AddForce(force, ForceMode.Impulse);
                                            Vector3 torque = new Vector3((float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100), (float)UnityEngine.Random.Range(-100, 100));
                                            obj5.rigidbody.AddTorque(torque, ForceMode.Acceleration);
                                        }
                                        if (!(this.rightGunHasBullet || !this.setup.part_blade_r.activeSelf))
                                        {
                                            this.setup.part_blade_r.SetActive(false);
                                            Transform transform5 = this.setup.part_blade_r.transform;
                                            GameObject obj6 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("Character_parts/character_gun_r"), transform5.position, transform5.rotation);
                                            obj6.renderer.material = CharacterMaterials.materials[this.setup.myCostume._3dmg_texture];
                                            Vector3 vector3 = ((Vector3)((-baseT.forward * 10f) + (baseT.up * 5f))) + baseT.right;
                                            obj6.rigidbody.AddForce(vector3, ForceMode.Impulse);
                                            Vector3 vector4 = new Vector3((float)UnityEngine.Random.Range(-300, 300), (float)UnityEngine.Random.Range(-300, 300), (float)UnityEngine.Random.Range(-300, 300));
                                            obj6.rigidbody.AddTorque(vector4, ForceMode.Acceleration);
                                        }
                                    }
                                    if ((baseA[this.reloadAnimation].normalizedTime > 0.62f) && !this.throwedBlades)
                                    {
                                        this.throwedBlades = true;
                                        if (!((this.leftBulletLeft <= 0) || this.leftGunHasBullet))
                                        {
                                            this.leftBulletLeft--;
                                            this.setup.part_blade_l.SetActive(true);
                                            this.leftGunHasBullet = true;
                                        }
                                        if (!((this.rightBulletLeft <= 0) || this.rightGunHasBullet))
                                        {
                                            this.setup.part_blade_r.SetActive(true);
                                            this.rightBulletLeft--;
                                            this.rightGunHasBullet = true;
                                        }
                                        this.updateRightMagUI();
                                        this.updateLeftMagUI();
                                    }
                                    if (baseA[this.reloadAnimation].normalizedTime > 1f)
                                    {
                                        this.idle();
                                    }
                                }
                                else
                                {
                                    if (!this.grounded)
                                    {
                                        if ((baseA[this.reloadAnimation].normalizedTime >= 0.2f) && !this.throwedBlades)
                                        {
                                            this.throwedBlades = true;
                                            if (this.setup.part_blade_l.activeSelf)
                                            {
                                                this.throwBlades();
                                            }
                                        }
                                        if ((baseA[this.reloadAnimation].normalizedTime >= 0.56f) && (this.currentBladeNum > 0))
                                        {
                                            this.setup.part_blade_l.SetActive(true);
                                            this.setup.part_blade_r.SetActive(true);
                                            this.currentBladeSta = this.totalBladeSta;
                                        }
                                    }
                                    else
                                    {
                                        if ((baseA[this.reloadAnimation].normalizedTime >= 0.13f) && !this.throwedBlades)
                                        {
                                            this.throwedBlades = true;
                                            if (this.setup.part_blade_l.activeSelf)
                                            {
                                                this.throwBlades();
                                            }
                                        }
                                        if ((baseA[this.reloadAnimation].normalizedTime >= 0.37f) && (this.currentBladeNum > 0))
                                        {
                                            this.setup.part_blade_l.SetActive(true);
                                            this.setup.part_blade_r.SetActive(true);
                                            this.currentBladeSta = this.totalBladeSta;
                                        }
                                    }
                                    if (baseA[this.reloadAnimation].normalizedTime >= 1f)
                                    {
                                        this.idle();
                                    }
                                }
                                break;
                            case HERO_STATE.Salute:
                                if (baseA["salute"].normalizedTime >= 1f)
                                {
                                    this.idle();
                                }
                                break;
                            case HERO_STATE.GroundDodge:
                                if (baseA.IsPlaying("dodge"))
                                {
                                    if (!(this.grounded || (baseA["dodge"].normalizedTime <= 0.6f)))
                                    {
                                        this.idle();
                                    }
                                    if (baseA["dodge"].normalizedTime >= 1f)
                                    {
                                        this.idle();
                                    }
                                }
                                break;
                            case HERO_STATE.Land:
                                if (baseA.IsPlaying("dash_land") && (baseA["dash_land"].normalizedTime >= 1f))
                                {
                                    this.idle();
                                }
                                break;
                            case HERO_STATE.FillGas:
                                if (baseA.IsPlaying("supply") && (baseA["supply"].normalizedTime >= 1f))
                                {
                                    this.currentBladeSta = this.totalBladeSta;
                                    this.currentBladeNum = this.totalBladeNum;
                                    this.currentGas = this.totalGas;
                                    if (!this.useGun)
                                    {
                                        this.setup.part_blade_l.SetActive(true);
                                        this.setup.part_blade_r.SetActive(true);
                                    }
                                    else
                                    {
                                        this.leftBulletLeft = this.rightBulletLeft = this.bulletMAX;
                                        this.leftGunHasBullet = this.rightGunHasBullet = true;
                                        this.setup.part_blade_l.SetActive(true);
                                        this.setup.part_blade_r.SetActive(true);
                                        this.updateRightMagUI();
                                        this.updateLeftMagUI();
                                    }
                                    this.idle();
                                }
                                break;
                            case HERO_STATE.Slide:
                                if (!this.grounded)
                                {
                                    this.idle();
                                }
                                break;
                            case HERO_STATE.AirDodge:
                                if (this.dashTime > 0f)
                                {
                                    this.dashTime -= Time.deltaTime;
                                    if (this.currentSpeed > this.originVM)
                                    {
                                        baseR.AddForce((Vector3)((-baseR.velocity * Time.deltaTime) * 1.7f), ForceMode.VelocityChange);
                                    }
                                }
                                else
                                {
                                    this.dashTime = 0f;
                                    this.idle();
                                }
                                break;
                        }
                        if (FengCustomInputs.Inputs.isInput[InputCode.leftRope] && (((!baseA.IsPlaying("attack3_1") && !baseA.IsPlaying("attack5")) && (!baseA.IsPlaying("special_petra") && (this.state != HERO_STATE.Grab))) || (this.state == HERO_STATE.Idle)))
                        {
                            if (this.bulletLeft != null)
                            {
                                this.QHold = true;
                            }
                            else
                            {
                                RaycastHit hit4;
                                Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask10 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask11 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask12 = mask11 | mask10;
                                if (Physics.Raycast(ray4, out hit4, 10000f, mask12.value))
                                {
                                    this.launchLeftRope(hit4, true, 0);
                                    this.rope.Play();
                                }
                            }
                        }
                        else
                        {
                            this.QHold = false;
                        }
                        if (FengCustomInputs.Inputs.isInput[InputCode.rightRope] && (((!baseA.IsPlaying("attack3_1") && !baseA.IsPlaying("attack5")) && (!baseA.IsPlaying("special_petra") && (this.state != HERO_STATE.Grab))) || (this.state == HERO_STATE.Idle)))
                        {
                            if (this.bulletRight != null)
                            {
                                this.EHold = true;
                            }
                            else
                            {
                                RaycastHit hit5;
                                Ray ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask13 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask14 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask15 = mask14 | mask13;
                                if (Physics.Raycast(ray5, out hit5, 10000f, mask15.value))
                                {
                                    this.launchRightRope(hit5, true, 0);
                                    this.rope.Play();
                                }
                            }
                        }
                        else
                        {
                            this.EHold = false;
                        }
                        if (FengCustomInputs.Inputs.isInput[InputCode.bothRope] && (((!baseA.IsPlaying("attack3_1") && !baseA.IsPlaying("attack5")) && (!baseA.IsPlaying("special_petra") && (this.state != HERO_STATE.Grab))) || (this.state == HERO_STATE.Idle)))
                        {
                            this.QHold = true;
                            this.EHold = true;
                            if ((this.bulletLeft == null) && (this.bulletRight == null))
                            {
                                RaycastHit hit6;
                                Ray ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
                                LayerMask mask16 = ((int)1) << LayerMask.NameToLayer("Ground");
                                LayerMask mask17 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                                LayerMask mask18 = mask17 | mask16;
                                if (Physics.Raycast(ray6, out hit6, 1000000f, mask18.value))
                                {
                                    this.launchLeftRope(hit6, false, 0);
                                    this.launchRightRope(hit6, false, 0);
                                    this.rope.Play();
                                }
                            }
                        }
                        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) || ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) && !IN_GAME_MAIN_CAMERA.isPausing))
                        {
                            this.calcSkillCD();
                            this.calcFlareCD();
                        }
                        if (!this.useGun)
                        {
                            if (this.leftbladetrail.gameObject.GetActive())
                            {
                                this.leftbladetrail.update();
                                this.rightbladetrail.update();
                            }
                            if (this.leftbladetrail2.gameObject.GetActive())
                            {
                                this.leftbladetrail2.update();
                                this.rightbladetrail2.update();
                            }
                            if (this.leftbladetrail.gameObject.GetActive())
                            {
                                this.leftbladetrail.lateUpdate();
                                this.rightbladetrail.lateUpdate();
                            }
                            if (this.leftbladetrail2.gameObject.GetActive())
                            {
                                this.leftbladetrail2.lateUpdate();
                                this.rightbladetrail2.lateUpdate();
                            }
                        }
                        if (!IN_GAME_MAIN_CAMERA.isPausing)
                        {
                            this.showSkillCD();
                            this.showFlareCD2();
                            this.showGas2();
                            this.showAimUI2();
                        }
                    }
                    else if (this.isCannon && !IN_GAME_MAIN_CAMERA.isPausing)
                    {
                        this.showAimUI2();
                        this.calcSkillCD();
                        this.showSkillCD();
                    }
                }
            }
        }
    }

    public void updateCannon()
    {
        baseT.position = this.myCannonPlayer.position;
        baseT.rotation = this.myCannonBase.rotation;
    }
    

    public void updateExt()
    {
        if (this.skillId == "bomb")
        {
            if (FengCustomInputs.Inputs.isInputDown[InputCode.attack1] && (this.skillCDDuration <= 0f))
            {
                if (!((this.myBomb == null) || this.myBomb.disabled))
                {
                    this.myBomb.Explode(this.bombRadius);
                }
                this.detonate = false;
                this.skillCDDuration = this.bombCD;
                RaycastHit hitInfo = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                LayerMask mask3 = mask2 | mask;
                this.currentV = this.baseT.position;
                this.targetV = this.currentV + ((Vector3)(Vector3.forward * 200f));
                if (Physics.Raycast(ray, out hitInfo, 1000000f, mask3.value))
                {
                    this.targetV = hitInfo.point;
                }
                    Vector3 vector = Vector3.Normalize(this.targetV - this.currentV);
                    GameObject bemb = PhotonNetwork.Instantiate("RCAsset/BombMain", this.currentV + ((Vector3)(vector * 4f)), new Quaternion(0f, 0f, 0f, 1f), 0);
                    bemb.rigidbody.velocity = (Vector3)(vector * this.bombSpeed);
                    this.myBomb = bemb.GetComponent<Bomb>();
                this.bombTime = 0f;
            }
            else if ((this.myBomb != null) && !this.myBomb.disabled)
            {
                this.bombTime += Time.deltaTime;
                bool flag2 = false;
                    if (FengCustomInputs.Inputs.isInputUp[InputCode.attack1])
                    {
                        this.detonate = true;
                    }
                    else if (FengCustomInputs.Inputs.isInputDown[InputCode.attack1] && this.detonate)
                    {
                        this.detonate = false;
                        flag2 = true;
                    }
                
                if (this.bombTime >= this.bombTimeMax)
                {
                    flag2 = true;
                }
                if (flag2)
                {
                    this.myBomb.Explode(this.bombRadius);
                    this.detonate = false;
                }
            }
        }
    }

    private void updateLeftMagUI()
    {
        for (int i = 1; i <= this.bulletMAX; i++)
        {
            BRM.CacheGameObject.Find<UISprite>("bulletL" + i).enabled = false;
        }
        for (int j = 1; j <= this.leftBulletLeft; j++)
        {
            BRM.CacheGameObject.Find<UISprite>("bulletL" + j).enabled = true;
        }
    }

    private void updateRightMagUI()
    {
        for (int i = 1; i <= this.bulletMAX; i++)
        {
            BRM.CacheGameObject.Find<UISprite>("bulletR" + i).enabled = false;
        }
        for (int j = 1; j <= this.rightBulletLeft; j++)
        {
            BRM.CacheGameObject.Find<UISprite>("bulletR" + j).enabled = true;
        }
    }

    public void useBlade(int amount)
    {
        if (amount == 0)
        {
            amount = 1;
        }
        amount *= 2;
        if (this.currentBladeSta > 0f)
        {
            this.currentBladeSta -= amount;
            if (this.currentBladeSta <= 0f)
            {
                if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || basePV.isMine)
                {
                    this.leftbladetrail.Deactivate();
                    this.rightbladetrail.Deactivate();
                    this.leftbladetrail2.Deactivate();
                    this.rightbladetrail2.Deactivate();
                    this.triggerLeft.active_me = false;
                    this.triggerRight.active_me = false;
                }
                this.currentBladeSta = 0f;
                this.throwBlades();
            }
        }
    }

    private void useGas(float amount)
    {
        if (amount == 0f)
        {
            amount = useGasSpeed;
        }
        if (this.currentGas > 0f)
        {
            this.currentGas -= amount;
            if (this.currentGas < 0f)
            {
                this.currentGas = 0f;
            }
        }
    }

    [RPC]
    private void whoIsMyErenTitan(int id)
    {
        this.eren_titan = PhotonView.Find(id).gameObject;
        this.titanForm = true;
    }

    public bool isGrabbed
    {
        get
        {
            return (this.state == HERO_STATE.Grab);
        }
    }

    private HERO_STATE state
    {
        get
        {
            return this._state;
        }
        set
        {
            if ((this._state == HERO_STATE.AirDodge) || (this._state == HERO_STATE.GroundDodge))
            {
                this.dashTime = 0f;
            }
            this._state = value;
        }
    }
}

