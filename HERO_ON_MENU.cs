using System;
using UnityEngine;
using CLEARSKIES;
using CLEARSKIES.Pooling;

public class HERO_ON_MENU : Photon.MonoBehaviour
{
    private Vector3 cameraOffset;
    private Transform cameraPref;
    public int costumeId;
    private Transform head;
    public float headRotationX;
    public float headRotationY;


    private Transform mainCameraT;
    //private static Vector3 ThunderPos;
    //private static Vector3 TitanDiePos;
    //private static UnityEngine.Object thunder = CacheResources.Load("FX/Thunder");
    //private float time;
    //private float time2;
    //private static UnityEngine.Object smoke = CacheResources.Load("FX/colossal_steam");
    //private static Quaternion TitanDieQuat = Quaternion.Euler(-90f, 0f, 0f);
    //private static Quaternion ThunderQuat = Quaternion.Euler(270f, 0f, 0f);

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        ChangeQuality.setCurrentQuality();
    }

    private void LateUpdate()
    {
        //if ((bool)FengGameManagerMKII.settings[98]) // standart vanilla menu 
        //{
        //    this.head.rotation = Quaternion.Euler(this.head.rotation.eulerAngles.x + this.headRotationX, this.head.rotation.eulerAngles.y + this.headRotationY, this.head.rotation.eulerAngles.z);
        //}
        if (this.costumeId == 9)
        {
            //if ((bool)FengGameManagerMKII.settings[98]) // vanilla 
            //{
            //    this.mainCameraT.position = this.cameraPref.position + this.cameraOffset;
            //    return;
            //}
            //this.time += Time.fixedDeltaTime;
            //this.time2 += Time.fixedDeltaTime;
            //if (this.time > Mathf.Min(UnityEngine.Random.Range(5f, 15f), 10f))
            //{
            //    this.time = 0f;               
            //    UnityEngine.Object.Instantiate(thunder, ThunderPos, ThunderQuat * Quaternion.Inverse(this.mainCameraT.rotation));                
            //}
            //if (this.time2 > UnityEngine.Random.Range(5f, 15f))
            //{
            //    this.time2 = 0f;                
            //    ((GameObject)UnityEngine.Object.Instantiate(smoke, TitanDiePos, TitanDieQuat)).transform.rotation *= Quaternion.Inverse(this.mainCameraT.rotation);
            //}
            if (QualitySettings.masterTextureLimit != 0)
            {
                QualitySettings.masterTextureLimit = 0;
            }
        }
    }

    private void Start()
    {
        HERO_SETUP component = base.gameObject.GetComponent<HERO_SETUP>();
        HeroCostume.init2();
        component.init();
        component.myCostume = HeroCostume.costume[this.costumeId];
        component.setCharacterComponent();
        this.mainCameraT = CacheGameObject.Find("MainCamera_Mono").transform;
        this.head = base.transform.Find("Amarture/Controller_Body/hip/spine/chest/neck/head");
        this.cameraPref = base.transform.Find("Amarture/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R");
        if (this.costumeId == 9)
        {


            //GameObject gameObject = base.gameObject;
            //GameObject gameObject2 = CacheGameObject.Find("MenuBackGround");
            //string[] array = new string[]
            //{
            //    "head",
            //    "hand_L",
            //    "hand_R",
            //    "chest",
            //    "spine",
            //    "MenuBackGround"
            //};
            //foreach (Renderer renderer in gameObject2.GetComponentsInChildren<Renderer>())
            //{
            //    GameObject gameObject3;
            //    if (renderer != null && renderer.enabled && !(gameObject3 = renderer.transform.parent.gameObject).name.EqualTo(false, array) && gameObject3 != gameObject)
            //    {
            //        UnityEngine.Object.DestroyImmediate(gameObject3, false);
            //    }
            //}
            this.mainCameraT.position = new Vector3(10.9f, 5841.2f, 3338.3f);
            this.mainCameraT.rotation = new Quaternion(-0.4f, 0.3f, 0.1f, 0.9f);
            //ThunderPos = this.mainCameraT.position;
            //TitanDiePos = this.mainCameraT.position + Vector3.down * 15f + this.mainCameraT.right * 3.5f;
            //Camera.main.GetComponent<Skybox>().material = new Material(FengGameManagerMKII.skinCache[4]["NIGHT"]);
            return;
        }
        UnityEngine.Object.Destroy(base.gameObject);
    }
}

