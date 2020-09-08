using System;
using System.Runtime.InteropServices;
using UnityEngine;
using CLEARSKIES;
using Boo.Lang;
using System.Linq;

public class IN_GAME_MAIN_CAMERA : MonoBehaviour
{
    public RotationAxes axes;
    public AudioSource bgmusic;
    public static float cameraDistance = 0.6f;

    public static int cameraTilt = 1;
    public static int character = 1;
    private float closestDistance;
    private int currentPeekPlayerIndex;
    public static DayLight dayLight = DayLight.Dawn;
    private float decay;
    public static int difficulty;
    private float distance = 10f;
    private float distanceMulti;
    private float distanceOffsetMulti;
    private float duration;
    private float flashDuration;
    private bool flip;
    public static GAMEMODE gamemode;
    public bool gameOver;
    public static GAMETYPE gametype = GAMETYPE.STOP;
    private bool hasSnapShot;

    private float height = 5f;
    private float heightDamping = 2f;
    public float heightMulti;
    public static int invertY = 1;
    public static bool isCheating;
    public static bool isPausing;
    public static bool isTyping;
    public float justHit;
    public int lastScore;
    public static int level;
    private bool lockAngle;
    private Vector3 lockCameraPosition;

    private GameObject lockTarget;

    public float maximumX = 360f;
    public float maximumY = 60f;
    public float minimumX = -360f;
    public float minimumY = -60f;
    private bool needSetHUD;
    private float R;
    public float rotationY;
    public int score;
    public static float sensitivityMulti = 0.5f;
    public static string singleCharacter;
    public Material skyBoxDAWN;
    public Material skyBoxDAY;
    public Material skyBoxNIGHT;
    private Texture2D snapshot1;
    private Texture2D snapshot2;
    private Texture2D snapshot3;
    public GameObject snapShotCamera;
    private int snapShotCount;
    private float snapShotCountDown;
    private int snapShotDmg;
    private float snapShotInterval = 0.02f;
    public RenderTexture snapshotRT;
    private float snapShotStartCountDownTime;
    private GameObject snapShotTarget;
    private Vector3 snapShotTargetPosition;
    public bool spectatorMode;
    private bool startSnapShotFrameCount;
    public static STEREO_3D_TYPE stereoType;
    private Transform target;
    public Texture texture;
    public float timer;
    public static bool triggerAutoLock;
    public static bool usingTitan;
    private Vector3 verticalHeightOffset = Vector3.zero;
    private float verticalRotationOffset;
    private float xSpeed = -3f;
    private float ySpeed = -0.8f;




    //delegate void CameraMove();
    //static CameraMove move = MoveORIGINAL;
    public static CameraShake shake;
    public static bool moredistance = false;
    private static Light mainLightL;
    public static Skybox skybox;
    public static IN_GAME_MAIN_CAMERA mainCamera;
    public static GameObject main_object
    {
        get
        {
            return mainobject;
        }
        set
        {
            mainobject = value;
            if (value != null)
            {
                main_objectT = value.transform;
                if (main_objectT == null)
                {
                    mainobject = null;
                    head = null;
                    headT = null;
                    mainHERO = null;
                    mainTITAN = null;
                    main_objectT = null;
                    main_objectR = null;
                    return;
                }
                main_objectR = value.rigidbody;
                MONO component = value.GetComponent<MONO>();
                if (component.species == SPECIES.Hero)
                {
                    mainHERO = (component as HERO);
                    mainTITAN = null;
                }
                else if (component.species == SPECIES.Titan)
                {
                    mainTITAN = (component as TITAN);
                    mainHERO = null;
                }
                else
                {
                    mainHERO = null;
                    mainTITAN = null;
                }
                head = main_objectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head");
                if (head == null)
                {
                    mainobject = null;
                    head = null;
                    headT = null;
                    mainHERO = null;
                    mainTITAN = null;
                    main_objectT = null;
                    main_objectR = null;
                    return;
                }
                headT = head.transform;
                if (headT == null)
                {
                    mainobject = null;
                    head = null;
                    headT = null;
                    mainHERO = null;
                    mainTITAN = null;
                    main_objectT = null;
                    main_objectR = null;
                    return;
                }
            }
            else
            {
                head = null;
                headT = null;
                mainHERO = null;
                mainTITAN = null;
                main_objectT = null;
                main_objectR = null;
            }
        }
    }
    private static Vector3 MainObjectPosition
    {
        get
        {
            if (headT != null || (head != null && ((headT = head.transform) != null)))
            {
                return headT.position;
            }
            if (main_objectT != null)
            {
                return main_objectT.position;
            }
            if (main_object != null)
            {
                return (main_objectT = main_object.transform).position;
            }
            if (mainT == null && (Camera.main == null || (mainT = Camera.main.transform) == null))
            {
                return Vector3.zero;
            }
            return mainT.position;
        }
    }
    private static Transform head;
    public static Transform main_objectT { get; private set; }
    public static Rigidbody main_objectR { get; private set; }
    public static HERO mainHERO { get; private set; }
    public static TITAN mainTITAN { get; private set; }
    public static GameObject mainG;
    public static Transform mainT;
    public static Camera mainC;
    private static Transform headT;
    private static Transform snapT;
    public static GameObject mainobject;
    public static TiltShift tiltshift;
    public static SpectatorMovement spectate;
    public static MouseLook mouselook;
    private static Transform lockTargetT;
    private Transform locker;
    public static CAMERA_TYPE cameraMode = CAMERA_TYPE.ORIGINAL;
    public static bool isReady = false;
    //{     
    //    get
    //    {
    //        return _cameraMode; 
    //    }
    //    set
    //    {
    //        _cameraMode = value;
    //        switch (value)
    //        {
    //            case CAMERA_TYPE.WOW:
    //                {
    //                    move = MoveWOW;
    //                    break;
    //                }
    //            case CAMERA_TYPE.ORIGINAL:
    //                {
    //                    move = MoveORIGINAL;
    //                    break;
    //                }
    //            case CAMERA_TYPE.TPS:
    //                {
    //                    move = MoveTPS;
    //                    break;
    //                }
    //            case CAMERA_TYPE.OldTPS:
    //                {
    //                    move = MoveOldTPS;
    //                    break;
    //                }
    //        }
    //    }
    //}




    private void Awake()
    {
        isTyping = false;
        isPausing = false;
        mainCamera = this;
        base.name = "MainCamera";
        mainC = Camera.main;
        mainT = Camera.main.transform;
        mainG = Camera.main.gameObject;
        tiltshift = base.GetComponent<TiltShift>();
        tiltshift.enabled = (!PlayerPrefs.HasKey("GameQuality") || PlayerPrefs.GetFloat("GameQuality") >= 0.9f);
        tiltshift = base.GetComponent<TiltShift>();
        mainLightL = CLEARSKIES.CacheGameObject.Find<Light>("mainLight");
        skybox = base.GetComponent<Skybox>();
        rotationY = 0f;
        spectate = base.GetComponent<SpectatorMovement>();
        mouselook = base.GetComponent<MouseLook>();
        shake = base.GetComponent<CameraShake>();
        this.CreateMinimap();
    }

    private void camareMovement()
    {
        float y = mainT.eulerAngles.y;
        //this.distanceOffsetMulti = (cameraDistance * (200f - mainC.fieldOfView)) / 150f;
        Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
        float z = mainT.eulerAngles.z;
        distanceOffsetMulti = (moredistance ? ((0.6f + cameraDistance) * (200f - mainC.fieldOfView) / 150f) : (cameraDistance * (200f - mainC.fieldOfView) / 150f));
        mainT.position = MainObjectPosition;
        mainT.position += Vector3.up * this.heightMulti;
        mainT.position -= (Vector3.up * (0.6f - cameraDistance)) * 2f;

        switch (cameraMode)
        {
            case CAMERA_TYPE.ORIGINAL:
                if (Input.mousePosition.x < (float)Screen.width * 0.4f)
                {
                    mainT.RotateAround(mainT.position, Vector3.up, -(((float)Screen.width * 0.4f - Input.mousePosition.x) / (float)Screen.width * 0.4f) * getSensitivityMultiWithDeltaTime() * 150f);
                }
                else if (Input.mousePosition.x > (float)Screen.width * 0.6f)
                {
                    mainT.RotateAround(mainT.position, Vector3.up, (Input.mousePosition.x - (float)Screen.width * 0.6f) / (float)Screen.width * 0.4f * getSensitivityMultiWithDeltaTime() * 150f);
                }
                mainT.rotation = Quaternion.Euler(140f * ((float)Screen.height * 0.6f - Input.mousePosition.y) / (float)Screen.height * 0.5f, mainT.rotation.eulerAngles.y, mainT.rotation.eulerAngles.z);
                mainT.position -= mainT.forward * this.distance * this.distanceMulti * this.distanceOffsetMulti;
                break;
            case CAMERA_TYPE.TPS:
                if (!FengCustomInputs.Inputs.menuOn)
                {
                    Screen.lockCursor = true;
                }
                float num5 = (Input.GetAxis("Mouse X") * 10f) * this.getSensitivityMulti();
                float num6 = ((-Input.GetAxis("Mouse Y") * 10f) * this.getSensitivityMulti()) * this.getReverse();
                mainT.RotateAround(mainT.position, Vector3.up, num5);
                float num7 = mainT.rotation.eulerAngles.x % 360f;
                float num8 = num7 + num6;
                if (((num6 <= 0f) || (((num7 >= 260f) || (num8 <= 260f)) && ((num7 >= 80f) || (num8 <= 80f)))) && ((num6 >= 0f) || (((num7 <= 280f) || (num8 >= 280f)) && ((num7 <= 100f) || (num8 >= 100f)))))
                {
                    mainT.RotateAround(mainT.position, mainT.right, num6);
                }
                mainT.position -= (Vector3)(((mainT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
                break;
            case CAMERA_TYPE.OldTPS:
                if (!FengCustomInputs.Inputs.menuOn)
                {
                    Screen.lockCursor = true;
                }
                rotationY += Input.GetAxis("Mouse Y") * 2.5f * (sensitivityMulti * 2f) * invertY;
                rotationY = Mathf.Clamp(rotationY, -60f, 60f);
                rotationY = Mathf.Max(rotationY, -999f + heightMulti * 2f);
                rotationY = Mathf.Min(rotationY, 999f);
                mainT.localEulerAngles = new Vector3(-rotationY, mainT.localEulerAngles.y + Input.GetAxis("Mouse X") * 2.5f * (sensitivityMulti * 2f), z);
                quaternion = Quaternion.Euler(0f, mainT.eulerAngles.y, 0f);
                mainT.position -= quaternion * Vector3.forward * 10f * distanceMulti * distanceOffsetMulti;
                mainT.position += -Vector3.up * rotationY * 0.1f * (float)Math.Pow(heightMulti, 1.1) * distanceOffsetMulti;
                break;
        }
        //if (IN_GAME_MAIN_CAMERA.head == null)
        //{
        //    //if (IN_GAME_MAIN_CAMERA.mainHERO != null && IN_GAME_MAIN_CAMERA.mainHERO.head != null)
        //    //{
        //    //    IN_GAME_MAIN_CAMERA.mainT.position = IN_GAME_MAIN_CAMERA.mainHERO.head.position;
        //    //}
        //    //else if (IN_GAME_MAIN_CAMERA.main_objectT != null)
        //    //{
        //    //    IN_GAME_MAIN_CAMERA.mainT.position = IN_GAME_MAIN_CAMERA.main_objectT.position;
        //    //}
        //    //else if (IN_GAME_MAIN_CAMERA.main_object != null)
        //    //{
        //    //    IN_GAME_MAIN_CAMERA.mainT.position = (IN_GAME_MAIN_CAMERA.main_objectT = IN_GAME_MAIN_CAMERA.main_object.transform).position;
        //    //}
        //    BRM.StatsTab.AddLine("SOSAT", BRM.StatsTab.DebugType.ERROR);
        //}
        //move();
        //if (cameraMode == CAMERA_TYPE.WOW)
        //{
        //    if (Input.GetKey(KeyCode.Mouse1))
        //    {
        //        float angle = (Input.GetAxis("Mouse X") * 10f) * this.getSensitivityMulti();
        //        float num2 = ((-Input.GetAxis("Mouse Y") * 10f) * this.getSensitivityMulti()) * this.getReverse();
        //        mainT.RotateAround(mainT.position, Vector3.up, angle);
        //        mainT.RotateAround(mainT.position, mainT.right, num2);
        //    }
        //    mainT.position -= (Vector3)(((mainT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
        //}
        //else if (cameraMode == CAMERA_TYPE.ORIGINAL)
        //{
        //    float num3 = 0f;
        //    if (Input.mousePosition.x < (Screen.width * 0.4f))
        //    {
        //        num3 = (-((((Screen.width * 0.4f) - Input.mousePosition.x) / ((float)Screen.width)) * 0.4f) * this.getSensitivityMultiWithDeltaTime()) * 150f;
        //        mainT.RotateAround(mainT.position, Vector3.up, num3);
        //    }
        //    else if (Input.mousePosition.x > (Screen.width * 0.6f))
        //    {
        //        num3 = ((((Input.mousePosition.x - (Screen.width * 0.6f)) / ((float)Screen.width)) * 0.4f) * this.getSensitivityMultiWithDeltaTime()) * 150f;
        //        mainT.RotateAround(mainT.position, Vector3.up, num3);
        //    }
        //    float x = ((140f * ((Screen.height * 0.6f) - Input.mousePosition.y)) / ((float)Screen.height)) * 0.5f;
        //    mainT.rotation = Quaternion.Euler(x, mainT.rotation.eulerAngles.y, mainT.rotation.eulerAngles.z);
        //    mainT.position -= (Vector3)(((mainT.forward * 10f) * this.distanceMulti) * this.distanceOffsetMulti);
        //}
        //else if (cameraMode == CAMERA_TYPE.TPS)
        //{
        //    if (!this.inputManager.menuOn)
        //    {
        //        Screen.lockCursor = true;
        //    }
        //    float num5 = (Input.GetAxis("Mouse X") * 10f) * this.getSensitivityMulti();
        //    float num6 = ((-Input.GetAxis("Mouse Y") * 10f) * this.getSensitivityMulti()) * this.getReverse();
        //    mainT.RotateAround(mainT.position, Vector3.up, num5);
        //    float num7 = mainT.rotation.eulerAngles.x % 360f;
        //    float num8 = num7 + num6;
        //    if (((num6 <= 0f) || (((num7 >= 260f) || (num8 <= 260f)) && ((num7 >= 80f) || (num8 <= 80f)))) && ((num6 >= 0f) || (((num7 <= 280f) || (num8 >= 280f)) && ((num7 <= 100f) || (num8 >= 100f)))))
        //    {
        //        mainT.RotateAround(mainT.position, mainT.right, num6);
        //    }
        //    mainT.position -= (Vector3)(((mainT.forward * this.distance) * this.distanceMulti) * this.distanceOffsetMulti);
        //}
        //else if (cameraMode == CAMERA_TYPE.OldTPS)
        //{
        //    if (!this.inputManager.menuOn)
        //    {
        //        Screen.lockCursor = true;
        //    }
        //    rotationY += Input.GetAxis("Mouse Y") * 2.5f * (sensitivityMulti * 2f) * invertY;
        //    rotationY = Mathf.Clamp(rotationY, -60f, 60f);
        //    rotationY = Mathf.Max(rotationY, -999f + heightMulti * 2f);
        //    rotationY = Mathf.Min(rotationY, 999f);
        //    mainT.localEulerAngles = new Vector3(-rotationY, mainT.localEulerAngles.y + Input.GetAxis("Mouse X") * 2.5f * (sensitivityMulti * 2f), z);
        //    quaternion = Quaternion.Euler(0f, mainT.eulerAngles.y, 0f);
        //    mainT.position -= quaternion * Vector3.forward * 10f * distanceMulti * distanceOffsetMulti;
        //    mainT.position += -Vector3.up * rotationY * 0.1f * (float)Math.Pow(heightMulti, 1.1) * distanceOffsetMulti;
        //}
        if (cameraDistance < 0.65f)
        {
            mainT.position += mainT.right * Mathf.Max((float)((0.6f - cameraDistance) * 2f), (float)0.65f);
        }
    }
    public static void MoveTPS()
    {
        if (!FengCustomInputs.Inputs.menuOn)
        {
            Screen.lockCursor = true;
        }
        float num5 = (Input.GetAxis("Mouse X") * 10f) * mainCamera.getSensitivityMulti();
        float num6 = ((-Input.GetAxis("Mouse Y") * 10f) * mainCamera.getSensitivityMulti()) * mainCamera.getReverse();
        mainT.RotateAround(mainT.position, Vector3.up, num5);
        float num7 = mainT.rotation.eulerAngles.x % 360f;
        float num8 = num7 + num6;
        if (((num6 <= 0f) || (((num7 >= 260f) || (num8 <= 260f)) && ((num7 >= 80f) || (num8 <= 80f)))) && ((num6 >= 0f) || (((num7 <= 280f) || (num8 >= 280f)) && ((num7 <= 100f) || (num8 >= 100f)))))
        {
            mainT.RotateAround(mainT.position, mainT.right, num6);
        }
        mainT.position -= ((mainT.forward * mainCamera.distance) * mainCamera.distanceMulti) * mainCamera.distanceOffsetMulti;
    }
    public static void MoveORIGINAL()
    {
        if (Input.mousePosition.x < (Screen.width * 0.4f))
        {
            mainT.RotateAround(mainT.position, Vector3.up, -(((float)Screen.width * 0.4f - Input.mousePosition.x) / (float)Screen.width * 0.4f) *
                mainCamera.getSensitivityMultiWithDeltaTime() * 150f);
        }
        else if (Input.mousePosition.x > (Screen.width * 0.6f))
        {
            mainT.RotateAround(mainT.position, Vector3.up, (Input.mousePosition.x - (float)Screen.width * 0.6f) / (float)Screen.width * 0.4f *
                mainCamera.getSensitivityMultiWithDeltaTime() * 150f);
        }
        mainT.rotation = Quaternion.Euler(140f * ((float)Screen.height * 0.6f - Input.mousePosition.y) / (float)Screen.height * 0.5f,
            mainT.rotation.eulerAngles.y, mainT.rotation.eulerAngles.z);
        mainT.position -= mainT.forward * 10f * mainCamera.distanceMulti * mainCamera.distanceOffsetMulti;
    }
    public static void MoveWOW()
    {
        float num3 = 0f;
        if (Input.mousePosition.x < (Screen.width * 0.4f))
        {
            num3 = (-((((Screen.width * 0.4f) - Input.mousePosition.x) / Screen.width) * 0.4f) * mainCamera.getSensitivityMultiWithDeltaTime()) * 150f;
            mainT.RotateAround(mainT.position, Vector3.up, num3);
        }
        else if (Input.mousePosition.x > (Screen.width * 0.6f))
        {
            num3 = ((((Input.mousePosition.x - (Screen.width * 0.6f)) / Screen.width) * 0.4f) * mainCamera.getSensitivityMultiWithDeltaTime()) * 150f;
            mainT.RotateAround(mainT.position, Vector3.up, num3);
        }
        float x = ((140f * ((Screen.height * 0.6f) - Input.mousePosition.y)) / Screen.height) * 0.5f;
        mainT.rotation = Quaternion.Euler(x, mainT.rotation.eulerAngles.y, mainT.rotation.eulerAngles.z);
        mainT.position -= ((mainT.forward * 10f) * mainCamera.distanceMulti) * mainCamera.distanceOffsetMulti;
    }
    public static void MoveOldTPS()
    {
        float z = mainT.eulerAngles.z;
        float y = mainT.eulerAngles.y;
        Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
        if (!FengCustomInputs.Inputs.menuOn)
        {
            Screen.lockCursor = true;
        }
        mainCamera.rotationY += Input.GetAxis("Mouse Y") * 2.5f * (sensitivityMulti * 2f) * invertY;
        mainCamera.rotationY = Mathf.Clamp(mainCamera.rotationY, -60f, 60f);
        mainCamera.rotationY = Mathf.Max(mainCamera.rotationY, -999f + mainCamera.heightMulti * 2f);
        mainCamera.rotationY = Mathf.Min(mainCamera.rotationY, 999f);
        mainT.localEulerAngles = new Vector3(-mainCamera.rotationY, mainT.localEulerAngles.y + Input.GetAxis("Mouse X") * 2.5f * (IN_GAME_MAIN_CAMERA.sensitivityMulti * 2f), z);
        quaternion = Quaternion.Euler(0f, mainT.eulerAngles.y, 0f);
        mainT.position -= quaternion * Vector3.forward * 10f * mainCamera.distanceMulti * mainCamera.distanceOffsetMulti;
        mainT.position += -Vector3.up * mainCamera.rotationY * 0.1f * (float)System.Math.Pow(mainCamera.heightMulti, 1.1) * mainCamera.distanceOffsetMulti;
    }
    public void CameraMovementLive(HERO hero)
    {
        float magnitude = hero.rigidbody.velocity.magnitude;
        if (magnitude > 10f)
        {
            mainC.fieldOfView = Mathf.Lerp(mainC.fieldOfView, Mathf.Min(100f, magnitude + 40f), 0.1f);
        }
        else
        {
            mainC.fieldOfView = Mathf.Lerp(mainC.fieldOfView, 50f, 0.1f);
        }
        float num2 = (hero.CameraMultiplier * (200f - mainC.fieldOfView)) / 150f;
        mainT.position = (headT.position + (Vector3.up * this.heightMulti)) - ((Vector3.up * (0.6f - cameraDistance)) * 2f);
        mainT.position -= ((mainT.forward * this.distance) * this.distanceMulti) * num2;
        if (hero.CameraMultiplier < 0.65f)
        {
            mainT.position += mainT.right * Mathf.Max((float)((0.6f - hero.CameraMultiplier) * 2f), (float)0.65f);
        }
        mainT.rotation = Quaternion.Lerp(mainC.transform.rotation, hero.GetComponent<SmoothSyncMovement>().correctCameraRot, Time.deltaTime * 5f);
    }

    private void CreateMinimap()
    {
        LevelInfo info = LevelInfo.getInfo(FengGameManagerMKII.level);
        if (info != null)
        {
            Minimap minimap = base.gameObject.AddComponent<Minimap>();
            if (Minimap.instance.myCam == null)
            {
                Minimap.instance.myCam = new GameObject().AddComponent<Camera>();
                Minimap.instance.myCam.nearClipPlane = 0.3f;
                Minimap.instance.myCam.farClipPlane = 1000f;
                Minimap.instance.myCam.enabled = false;
            }
            minimap.CreateMinimap(Minimap.instance.myCam, 0x200, 0.3f, info.minimapPreset);
            //if ((((int) FengGameManagerMKII.settings[0xe7]) == 0) /*|| (GameSettings.globalDisableMinimap == 1)*/)
            //{
            //    minimap.SetEnabled(false);
            //}
        }
    }

    public void createSnapShotRT()
    {
        if (this.snapShotCamera.GetComponent<Camera>().targetTexture != null)
        {
            this.snapShotCamera.GetComponent<Camera>().targetTexture.Release();
        }
        if (QualitySettings.GetQualityLevel() > 3)
        {
            this.snapShotCamera.GetComponent<Camera>().targetTexture = new RenderTexture((int)(Screen.width * 0.8f), (int)(Screen.height * 0.8f), 0x18);
        }
        else
        {
            this.snapShotCamera.GetComponent<Camera>().targetTexture = new RenderTexture((int)(Screen.width * 0.4f), (int)(Screen.height * 0.4f), 0x18);
        }
    }

    public void createSnapShotRT2()
    {
        if (this.snapshotRT != null)
        {
            this.snapshotRT.Release();
        }
        if (QualitySettings.GetQualityLevel() > 3)
        {
            this.snapshotRT = new RenderTexture((int)(Screen.width * 0.8f), (int)(Screen.height * 0.8f), 0x18);
            this.snapShotCamera.GetComponent<Camera>().targetTexture = this.snapshotRT;
        }
        else
        {
            this.snapshotRT = new RenderTexture((int)(Screen.width * 0.4f), (int)(Screen.height * 0.4f), 0x18);
            this.snapShotCamera.GetComponent<Camera>().targetTexture = this.snapshotRT;
        }
    }

    private GameObject findNearestTitan()
    {
        GameObject[] objArray = GameObject.FindGameObjectsWithTag("titan");
        GameObject obj2 = null;
        float num2 = this.closestDistance = float.PositiveInfinity;
        Vector3 position = main_objectT.position;
        foreach (GameObject obj3 in objArray)
        {
            Vector3 vector2 = obj3.transform.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck").position - position;
            float magnitude = vector2.magnitude;
            if ((magnitude < num2) && ((obj3.GetComponent<TITAN>() == null) || !obj3.GetComponent<TITAN>().hasDie))
            {
                obj2 = obj3;
                num2 = magnitude;
                this.closestDistance = num2;
            }
        }
        return obj2;
    }

    public void flashBlind()
    {
        CLEARSKIES.CacheGameObject.Find("flash").GetComponent<UISprite>().alpha = 1f;
        this.flashDuration = 2f;
    }

    public int getReverse()
    {
        return invertY;
    }

    public float getSensitivityMulti()
    {
        return sensitivityMulti;
    }

    public float getSensitivityMultiWithDeltaTime()
    {
        return ((sensitivityMulti * Time.deltaTime) * 62f);
    }

    private void OnDestroy()
    {
        mainCamera = null;
        if (Camera.main == null)
        {
            mainC = null;
            mainT = null;
            mainG = null;
        }
        else
        {
            mainC = Camera.main;
            mainT = Camera.main.transform;
            mainG = Camera.main.gameObject;
        }
        skybox = null;
        tiltshift = null;
        spectate = null;
        mouselook = null;
    }

    private void reset()
    {
        if (gametype == GAMETYPE.SINGLE)
        {
            FengGameManagerMKII.instance.restartGameSingle2();
        }
    }

    private Texture2D RTImage(Camera cam)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        int num = (int)(cam.targetTexture.width * 0.04f);
        int destX = (int)(cam.targetTexture.width * 0.02f);
        textured.ReadPixels(new Rect(num, num, cam.targetTexture.width - num, cam.targetTexture.height - num), destX, destX);
        textured.Apply();
        RenderTexture.active = active;
        return textured;
    }

    private Texture2D RTImage2(Camera cam)
    {
        RenderTexture active = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D textured = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        int num = (int)(cam.targetTexture.width * 0.04f);
        int destX = (int)(cam.targetTexture.width * 0.02f);
        try
        {
            textured.SetPixel(0, 0, Color.white);
            textured.ReadPixels(new Rect(num, num, cam.targetTexture.width - num, cam.targetTexture.height - num), destX, destX);
            textured.Apply();
            RenderTexture.active = active;
        }
        catch
        {
            textured = new Texture2D(1, 1);
            textured.SetPixel(0, 0, Color.white);
            return textured;
        }
        return textured;
    }
    public void setDayLight(DayLight val)
    {
        dayLight = val;
        if (dayLight == DayLight.Night)
        {
            Transform transform = ((GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("flashlight"))).transform;
            transform.parent = mainT;
            transform.position = mainT.position;
            transform.rotation = Quaternion.Euler(353f, 0f, 0f);
            RenderSettings.ambientLight = FengColor.nightAmbientLight;
            mainLightL.color = FengColor.nightLight;
            skybox.material = this.skyBoxNIGHT;
        }
        if (dayLight == DayLight.Day)
        {
            RenderSettings.ambientLight = FengColor.dayAmbientLight;
            mainLightL.color = FengColor.dayLight;
            skybox.material = this.skyBoxDAY;
        }
        if (dayLight == DayLight.Dawn)
        {
            RenderSettings.ambientLight = FengColor.dawnAmbientLight;
            mainLightL.color = FengColor.dawnAmbientLight;
            skybox.material = this.skyBoxDAWN;
        }
        this.snapShotCamera.gameObject.GetComponent<Skybox>().material = skybox.material;
    }

    public void setHUDposition()
    {
        float mposw = (-Screen.width * 0.5f),
              mposh = (-Screen.height * 0.5f),
              pposh = (Screen.height * 0.5f),
              pposw = (Screen.width * 0.5f);
        UILabel uilabel = CLEARSKIES.CacheGameObject.Find<UILabel>("LabelInfoBottomRight");

        CLEARSKIES.CacheGameObject.Find<Transform>("Flare").localPosition = new Vector3(((int)mposw + 14), (int)mposh, 0f);

        uilabel.transform.localPosition = new Vector3((int)pposw, (int)mposh, 0f);
        uilabel.text = "Pause : " + FengCustomInputs.Inputs.inputString[InputCode.pause] + " ";

        CLEARSKIES.CacheGameObject.Find<Transform>("LabelInfoTopCenter").localPosition = new Vector3(0f, (int)pposh, 0f);
        CLEARSKIES.CacheGameObject.Find<Transform>("LabelInfoTopRight").localPosition = new Vector3((int)pposw, (int)pposh, 0f);
        CLEARSKIES.CacheGameObject.Find<Transform>("LabelNetworkStatus").localPosition = new Vector3((int)mposw, (int)pposh, 0f);
        CLEARSKIES.CacheGameObject.Find<Transform>("LabelInfoTopLeft").localPosition = new Vector3((int)mposw, (int)(pposh - 20f), 0f);

        if (InRoomChat.ChatInstanse != null)
        {
            InRoomChat.ChatInstanse.transform.localPosition = new Vector3((int)mposh, (int)mposh, 0f);
            InRoomChat.ChatInstanse.setPosition();
        }
        if (!usingTitan || (gametype == GAMETYPE.SINGLE))
        {
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_bottom").localPosition = new Vector3(0f, (int)(mposh + 5f), 0f);
            CLEARSKIES.CacheGameObject.Find<Transform>("GasUI").localPosition = CLEARSKIES.CacheGameObject.Find("skill_cd_bottom").transform.localPosition;
            CLEARSKIES.CacheGameObject.Find<Transform>("stamina_titan").localPosition = new Vector3(0f, 9999f, 0f);
            CLEARSKIES.CacheGameObject.Find<Transform>("stamina_titan_bottom").localPosition = new Vector3(0f, 9999f, 0f);
        }
        else
        {
            Vector3 vector = new Vector3(0f, 9999f, 0f);
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_bottom").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_armin").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_eren").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_jean").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_levi").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_marco").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_mikasa").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_petra").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("skill_cd_sasha").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("GasUI").localPosition = vector;
            CLEARSKIES.CacheGameObject.Find<Transform>("stamina_titan").localPosition = new Vector3(-160f, (int)(mposh + 15f), 0f);
            CLEARSKIES.CacheGameObject.Find<Transform>("stamina_titan_bottom").localPosition = new Vector3(-160f, (int)(mposh + 15f), 0f);
        }
        if (mainHERO != null && mainHERO.isLocal)
        {
            mainHERO.setSkillHUDPosition2();
        }
        if (stereoType == STEREO_3D_TYPE.SIDE_BY_SIDE)
        {
            base.gameObject.GetComponent<Camera>().aspect = Screen.width / Screen.height;
        }
        this.createSnapShotRT2();
    }

    public GameObject setMainObject(GameObject obj, bool resetRotation = true, bool lockAngle = false)
    {
        mainobject = obj;
        mainTITAN = null;
        if (obj == null)
        {
            mainHERO = null;
            main_objectT = null;
            main_objectR = null;
            head = null;
            headT = null;
            distanceMulti = this.heightMulti = 1f;
        }
        else if (mainobject != null && (main_objectT = mainobject.transform) != null && (head = main_objectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head")) != null)
        {
            mainHERO = mainobject.GetComponent<HERO>();
            main_objectR = mainobject.rigidbody;
            headT = head.transform;
            distanceMulti = (head != null) ? (Vector3.Distance(headT.position, main_objectT.position) * 0.2f) : 1f;
            this.heightMulti = (head != null) ? (Vector3.Distance(headT.position, main_objectT.position) * 0.33f) : 1f;
            if (resetRotation)
            {
                mainT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else if (mainobject != null && (main_objectT = mainobject.transform) != null && (head = main_objectT.Find("Amarture/Controller_Body/hip/spine/chest/neck/head")) != null)
        {
            mainHERO = mainobject.GetComponent<HERO>();
            main_objectR = mainobject.rigidbody;
            headT = head.transform;
            distanceMulti = this.heightMulti = 0.64f;
            if (resetRotation)
            {
                mainT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            mainHERO = null;
            main_objectT = null;
            main_objectR = null;
            head = null;
            headT = null;
            distanceMulti = this.heightMulti = 1f;
            if (resetRotation)
            {
                mainT.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        this.lockAngle = lockAngle;
        return obj;
    }

    public GameObject setMainObjectASTITAN(GameObject obj)
    {
        mainobject = obj;
        if ((main_objectT = mainobject.transform) != null && (head = main_objectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck/head")) != null)
        {
            mainTITAN = mainobject.GetComponent<TITAN>();
            main_objectR = mainobject.rigidbody;
            headT = head.transform;
            distanceMulti = (head != null) ? (Vector3.Distance(headT.position, main_objectT.position) * 0.4f) : 1f;
            this.heightMulti = (head != null) ? (Vector3.Distance(headT.position, main_objectT.position) * 0.45f) : 1f;
            mainT.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        return obj;
    }

    public void setSpectorMode(bool val)
    {
        this.spectatorMode = val;
        spectate.disable = !val;
        mouselook.disable = !val;
    }

    private void shakeUpdate()
    {
        if (this.duration > 0f)
        {
            this.duration -= Time.deltaTime;
            if (this.flip)
            {
                mainG.transform.position += Vector3.up * this.R;
            }
            else
            {
                mainG.transform.position -= Vector3.up * this.R;
            }
            this.flip = !this.flip;
            this.R *= this.decay;
        }
    }

    public void snapShot2(int index)
    {
        Vector3 vector;
        RaycastHit hit;
        snapT.position = MainObjectPosition;
        Transform transform = snapT;
        transform.position += Vector3.up * this.heightMulti;
        Transform transform2 = snapT;
        transform2.position -= Vector3.up * 1.1f;
        Vector3 worldPosition = vector = snapT.position;
        Vector3 vector3 = (worldPosition + this.snapShotTargetPosition) * 0.5f;
        snapT.position = vector3;
        worldPosition = vector3;
        snapT.LookAt(this.snapShotTargetPosition);
        if (index == 3)
        {
            snapT.RotateAround(mainT.position, Vector3.up, UnityEngine.Random.Range(-180f, 180f));
        }
        else
        {
            snapT.RotateAround(mainT.position, Vector3.up, UnityEngine.Random.Range(-20f, 20f));
        }
        snapT.LookAt(worldPosition);
        snapT.RotateAround(worldPosition, mainT.right, UnityEngine.Random.Range(-20f, 20f));
        float num = Vector3.Distance(this.snapShotTargetPosition, vector);
        if ((this.snapShotTarget != null) && (this.snapShotTarget.GetComponent<TITAN>() != null))
        {
            num += ((index - 1) * this.snapShotTarget.transform.localScale.x) * 10f;
        }
        Transform transform3 = snapT;
        transform3.position -= snapT.forward * UnityEngine.Random.Range((float)(num + 3f), (float)(num + 10f));
        snapT.LookAt(worldPosition);
        snapT.RotateAround(worldPosition, mainT.forward, UnityEngine.Random.Range(-30f, 30f));
        Vector3 end = MainObjectPosition;
        Vector3 vector5 = (MainObjectPosition) - snapT.position;
        end -= vector5;
        if (head != null)
        {
            if (Physics.Linecast(headT.position, end, out hit, Layer.GroundEnemy))
            {
                snapT.position = hit.point;
            }
            else if (Physics.Linecast(headT.position - (vector5 * this.distanceMulti) * 3f, end, out hit, Layer.GroundEnemy))
            {
                snapT.position = hit.point;
            }
        }
        else if (Physics.Linecast(main_objectT.position + Vector3.up, end, out hit, Layer.GroundEnemy))
        {
            snapT.position = hit.point;
        }
        switch (index)
        {
            case 1:
                this.snapshot1 = this.RTImage2(this.snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(this.snapshot1, this.snapShotDmg);
                break;

            case 2:
                this.snapshot2 = this.RTImage2(this.snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(this.snapshot2, this.snapShotDmg);
                break;

            case 3:
                this.snapshot3 = this.RTImage2(this.snapShotCamera.GetComponent<Camera>());
                SnapShotSaves.addIMG(this.snapshot3, this.snapShotDmg);
                break;
        }
        this.snapShotCount = index;
        this.hasSnapShot = true;
        this.snapShotCountDown = 2f;
        if (index == 1)
        {
            CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = this.snapshot1;
            CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.localScale = new Vector3(Screen.width * 0.4f, Screen.height * 0.4f, 1f);
            CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.localPosition = new Vector3(-Screen.width * 0.225f, Screen.height * 0.225f, 0f);
            CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().transform.rotation = Quaternion.Euler(0f, 0f, 10f);
            if (PlayerPrefs.HasKey("showSSInGame") && (PlayerPrefs.GetInt("showSSInGame") == 1))
            {
                CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = true;
            }
            else
            {
                CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = false;
            }
        }
    }

    public void snapShotUpdate()
    {
        if (this.startSnapShotFrameCount)
        {
            this.snapShotStartCountDownTime -= Time.deltaTime;
            if (this.snapShotStartCountDownTime <= 0f)
            {
                this.snapShot2(1);
                this.startSnapShotFrameCount = false;
            }
        }
        if (this.hasSnapShot)
        {
            this.snapShotCountDown -= Time.deltaTime;
            if (this.snapShotCountDown <= 0f)
            {
                CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().enabled = false;
                this.hasSnapShot = false;
                this.snapShotCountDown = 0f;
            }
            else if (this.snapShotCountDown < 1f)
            {
                CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = this.snapshot3;
            }
            else if (this.snapShotCountDown < 1.5f)
            {
                CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0].transform.Find("snapshot1").GetComponent<UITexture>().mainTexture = this.snapshot2;
            }
            if (this.snapShotCount < 3)
            {
                this.snapShotInterval -= Time.deltaTime;
                if (this.snapShotInterval <= 0f)
                {
                    this.snapShotInterval = 0.05f;
                    this.snapShotCount++;
                    this.snapShot2(this.snapShotCount);
                }
            }
        }
    }

    private void Start()
    {
        mainCamera = this;
        isReady = true;

        isPausing = false;


        locker = CLEARSKIES.CacheGameObject.Find<Transform>("locker");
        setDayLight(dayLight);

        snapT = snapShotCamera.transform;
        sensitivityMulti = PlayerPrefs.GetFloat("MouseSensitivity");
        invertY = PlayerPrefs.GetInt("invertMouseY");
        if (PlayerPrefs.HasKey("cameraTilt"))
        {
            cameraTilt = PlayerPrefs.GetInt("cameraTilt");
        }
        else
        {
            cameraTilt = 1;
        }
        if (PlayerPrefs.HasKey("cameraDistance"))
        {
            cameraDistance = PlayerPrefs.GetFloat("cameraDistance") + 0.3f;
        }
        this.createSnapShotRT2();
    }

    public void startShake(float R, float duration, float decay = 0.95f)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
            this.decay = decay;
        }
    }

    public void startSnapShot(Vector3 p, int dmg, GameObject target, float startTime = 0.02f)
    {
        this.snapShotCount = 1;
        this.startSnapShotFrameCount = true;
        this.snapShotTargetPosition = p;
        this.snapShotTarget = target;
        this.snapShotStartCountDownTime = startTime;
        this.snapShotInterval = 0.05f + UnityEngine.Random.Range(0f, 0.03f);
        this.snapShotDmg = dmg;
    }

    public void startSnapShot2(Vector3 p, int dmg, GameObject target, float startTime)
    {
        int num;
        if (int.TryParse((string)FengGameManagerMKII.settings[0x5f], out num))
        {
            if (dmg >= num)
            {
                this.snapShotCount = 1;
                this.startSnapShotFrameCount = true;
                this.snapShotTargetPosition = p;
                this.snapShotTarget = target;
                this.snapShotStartCountDownTime = startTime;
                this.snapShotInterval = 0.05f + UnityEngine.Random.Range(0f, 0.03f);
                this.snapShotDmg = dmg;
            }
        }
        else
        {
            this.snapShotCount = 1;
            this.startSnapShotFrameCount = true;
            this.snapShotTargetPosition = p;
            this.snapShotTarget = target;
            this.snapShotStartCountDownTime = startTime;
            this.snapShotInterval = 0.05f + UnityEngine.Random.Range(0f, 0.03f);
            this.snapShotDmg = dmg;
        }
    }

    public void Update()
    {
        if (this.flashDuration > 0f)
        {
            this.flashDuration -= Time.deltaTime;
            if (this.flashDuration <= 0f)
            {
                this.flashDuration = 0f;
            }
            CacheGameObject.Find<UISprite>("flash").alpha = this.flashDuration * 0.5f;
        }
        if (gametype == GAMETYPE.STOP)
        {
            Screen.showCursor = true;
            Screen.lockCursor = false;
            return;
        }
        else
        {
            if ((gametype != GAMETYPE.SINGLE) && this.gameOver)
            {
                if (FengCustomInputs.Inputs.isInputDown[InputCode.attack1])
                {
                    if (this.spectatorMode)
                    {
                        this.setSpectorMode(false);
                    }
                    else
                    {
                        this.setSpectorMode(true);
                    }
                }
                List<GameObject> list;
                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare1])
                {
                    list = new List<GameObject>(from h in FengGameManagerMKII.allheroes
                                                where h != null
                                                select h);
                    this.currentPeekPlayerIndex++;                  
                    if (this.currentPeekPlayerIndex >= list.Count)
                    {
                        this.currentPeekPlayerIndex = 0;
                    }
                    if (list.Count > 0)
                    {
                        this.setMainObject(list[this.currentPeekPlayerIndex], true, false);
                        this.setSpectorMode(false);
                        this.lockAngle = false;
                    }
                }
                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare2])
                {
                    list = new List<GameObject>(from h in FengGameManagerMKII.allheroes
                                                where h != null
                                                select h);
                    this.currentPeekPlayerIndex--;
                    if (this.currentPeekPlayerIndex >= list.Count)
                    {
                        this.currentPeekPlayerIndex = 0;
                    }
                    if (this.currentPeekPlayerIndex < 0)
                    {
                        this.currentPeekPlayerIndex = list.Count;
                    }
                    if (list.Count > 0)
                    {
                        this.setMainObject(list[this.currentPeekPlayerIndex], true, false);
                        this.setSpectorMode(false);
                        this.lockAngle = false;
                    }
                }
                if (this.spectatorMode)
                {
                    return;
                }
            }
            if (FengCustomInputs.Inputs.isInputDown[InputCode.pause])
            {
                if (isPausing)
                {
                    if (main_object != null && mainT != null)
                    {
                        Vector3 position = mainT.position;
                        position = MainObjectPosition;
                        position += (Vector3.up * this.heightMulti);
                        mainT.position = Vector3.Lerp(mainT.position, position - ((Vector3)(mainT.forward * 5f)), Time.deltaTime * 1f);
                    }
                    return;
                }
                else
                {
                    isPausing = !isPausing;
                    if (gametype == GAMETYPE.SINGLE)
                    {
                        Time.timeScale = 0f;
                    }
                    //UIReferArray uireferArray = CacheGameObject.Find<UIReferArray>("UI_IN_GAME");
                    //NGUITools.SetActive(uireferArray.panels[0], false);
                    //NGUITools.SetActive(uireferArray.panels[1], true);
                    //NGUITools.SetActive(uireferArray.panels[2], false);
                    //NGUITools.SetActive(uireferArray.panels[3], false);
                    //FengCustomInputs.Inputs.showKeyMap();
                    //FengCustomInputs.Inputs.justUPDATEME();
                    FengCustomInputs.Inputs.menuOn = (Screen.showCursor = true);
                    Screen.lockCursor = false;
                }
            }
            if (this.needSetHUD)
            {
                this.needSetHUD = false;
                this.setHUDposition();
                Screen.lockCursor = !Screen.lockCursor;
                Screen.lockCursor = !Screen.lockCursor;
            }
            if (FengCustomInputs.Inputs.isInputDown[InputCode.fullscreen])
            {
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    Screen.SetResolution(960, 600, false);
                }
                else
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                }
                this.needSetHUD = true;
                Minimap.OnScreenResolutionChanged();
            }
            if (FengCustomInputs.Inputs.isInputDown[InputCode.restart])
            {
                this.reset();
            }
            if (main_object != null)
            {
                if (main_objectT == null)
                {
                    main_objectT = main_object.transform;
                }
                //RaycastHit hit;
                if (FengCustomInputs.Inputs.isInputDown[InputCode.camera])
                {
                    switch (cameraMode)
                    {
                        case CAMERA_TYPE.ORIGINAL:
                            cameraMode = CAMERA_TYPE.TPS;
                            //move = MoveWOW;
                            Screen.lockCursor = false;
                            break;
                        case CAMERA_TYPE.TPS:
                            cameraMode = CAMERA_TYPE.OldTPS;
                            //move = MoveOldTPS;
                            Screen.lockCursor = true;
                            break;
                        case CAMERA_TYPE.OldTPS:
                            cameraMode = CAMERA_TYPE.ORIGINAL;
                            //move = MoveORIGINAL;
                            Screen.lockCursor = false;
                            break;
                    }
                    this.verticalRotationOffset = 0f;
                    if ((((int)FengGameManagerMKII.settings[0xf5]) == 1) || (main_object.GetComponent<HERO>() == null))
                    {
                        Screen.showCursor = false;
                    }
                }
                if (FengCustomInputs.Inputs.isInputDown[InputCode.hideCursor])
                {
                    Screen.showCursor = !Screen.showCursor;
                }
                if (FengCustomInputs.Inputs.isInputDown[InputCode.focus])
                {
                    triggerAutoLock = !triggerAutoLock;
                    if (triggerAutoLock)
                    {
                        this.lockTarget = this.findNearestTitan();
                        if (this.closestDistance >= 150f)
                        {
                            this.lockTarget = null;
                            lockTargetT = null;
                            triggerAutoLock = false;
                        }
                        else
                        {
                            lockTargetT = lockTarget.transform;
                        }
                    }
                }
                if (this.gameOver && (main_object != null))
                {
                    if (FengGameManagerMKII.inputRC.isInputHumanDown(InputCodeRC.liveCam))
                    {
                        if (((int)FengGameManagerMKII.settings[0x107]) == 0)
                        {
                            FengGameManagerMKII.settings[0x107] = 1;
                        }
                        else
                        {
                            FengGameManagerMKII.settings[0x107] = 0;
                        }
                    }
                    HERO component = main_object.GetComponent<HERO>();
                    if ((((component != null) && (((int)FengGameManagerMKII.settings[0x107]) == 1)) && component.sync.enabled) && component.isPhotonCamera)
                    {
                        this.CameraMovementLive(component);
                    }
                    else if (this.lockAngle)
                    {
                        mainT.rotation = Quaternion.Lerp(mainT.rotation, main_objectT.rotation, 0.2f);
                        mainT.position = Vector3.Lerp(mainT.position, main_objectT.position - ((Vector3)(main_objectT.forward * 5f)), 0.2f);
                    }
                    else
                    {
                        this.camareMovement();
                    }
                }
                else
                {
                    this.camareMovement();
                }
                if (triggerAutoLock && (this.lockTarget != null))
                {
                    float z = mainT.eulerAngles.z;
                    Transform transform = lockTargetT.Find("Amarture/Core/Controller_Body/hip/spine/chest/neck");
                    Vector3 vector2 = transform.position - MainObjectPosition;
                    vector2.Normalize();
                    //this.lockCameraPosition = main_objectP;
                    //this.lockCameraPosition -= (Vector3) (((vector2 * distance) * distanceMulti) * distanceOffsetMulti);
                    //this.lockCameraPosition += (Vector3) (((up * 3f) * this.heightMulti) * distanceOffsetMulti);
                    mainT.position = Vector3.Lerp(mainT.position, this.lockCameraPosition = MainObjectPosition - vector2 *
                        10f * distanceMulti * distanceOffsetMulti + Vector3.up * 3f * this.heightMulti * distanceOffsetMulti,
                        Time.deltaTime * 4f);
                    if (head != null)
                    {
                        mainT.LookAt((Vector3)((headT.position * 0.8f) + (transform.position * 0.2f)));
                    }
                    else
                    {
                        mainT.LookAt((Vector3)((main_objectT.position * 0.8f) + (transform.position * 0.2f)));
                    }
                    mainT.localEulerAngles = new Vector3(mainT.eulerAngles.x, mainT.eulerAngles.y, z);
                    Vector2 vector3 = mainC.WorldToScreenPoint(transform.position - ((Vector3)(transform.forward * lockTargetT.localScale.x)));
                    this.locker.localPosition = new Vector3(vector3.x - (Screen.width * 0.5f), vector3.y - (Screen.height * 0.5f), 0f);
                    if ((this.lockTarget.GetComponent<TITAN>() != null) && this.lockTarget.GetComponent<TITAN>().hasDie)
                    {
                        this.lockTarget = null;
                        lockTargetT = null;
                    }
                }
                else
                {
                    this.locker.localPosition = new Vector3(0f, (-Screen.height * 0.5f) - 50f, 0f);
                }
                RaycastHit hit;
                Vector3 end = MainObjectPosition;
                Vector3 normalized = (MainObjectPosition - mainT.position).normalized;
                end -= (Vector3)((distance * normalized) * distanceMulti);
                //LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                //LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("EnemyBox");
                //LayerMask mask3 = mask | mask2;
                if (head != null)
                {
                    if (headT == null)
                    {
                        headT = head.transform;
                    }
                    if (Physics.Linecast(headT.position, end, out hit, Layer.Ground))
                    {
                        mainT.position = hit.point;
                    }
                    else if (Physics.Linecast(headT.position - ((Vector3)((normalized * distanceMulti) * 3f)), end, out hit, Layer.Enemy))
                    {
                        mainT.position = hit.point;
                    }
                    Debug.DrawLine(headT.position - ((Vector3)((normalized * distanceMulti) * 3f)), end, Color.red);
                }
                else if (Physics.Linecast(main_objectT.position + Vector3.up, end, out hit, Layer.GroundEnemy))
                {
                    mainT.position = hit.point;
                }
                this.shakeUpdate();
            }
        }
    }
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
}

