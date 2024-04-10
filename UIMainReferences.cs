using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;
using System.Linq; 

public class UIMainReferences : MonoBehaviour
{
    public static string fengVersion;
    public static bool isGAMEFirstLaunch = true;
    public GameObject panelCredits;
    public GameObject PanelDisconnect;
    public GameObject panelMain;
    public GameObject PanelMultiJoinPrivate;
    public GameObject PanelMultiPWD;
    public GameObject panelMultiROOM;
    public GameObject panelMultiSet;
    public GameObject panelMultiStart;
    public GameObject PanelMultiWait;
    public GameObject panelOption;
    public GameObject panelSingleSet;
    public GameObject PanelSnapShot;
    public static string version = "01042015";
    public static UIMainReferences UIRefer;

     
    private void Destroy()
    {
        UIMainReferences.UIRefer = null;
    }



    public IEnumerator request(string versionShow, string versionForm)
    {
        string url = Application.dataPath + "/RCAssets.unity3d";
        if (!Application.isWebPlayer)
        {
            url = "File://" + url;
        }
        while (!Caching.ready)
        {
            yield return null;
        }
        int version = 1;
        using (WWW iteratorVariable2 = WWW.LoadFromCacheOrDownload(url, version))
        {
            yield return iteratorVariable2;
            if (iteratorVariable2.error != null)
            {
                throw new Exception("WWW download had an error:" + iteratorVariable2.error);
            }
            FengGameManagerMKII.RCassets = iteratorVariable2.assetBundle;
            FengGameManagerMKII.isAssetLoaded = true;
            FengGameManagerMKII.instance.setBackground();
        }
    }


    public static int Width = 800;
    public static int Height = 600;
    public static bool Fullscreen = false;
    static string thirdLine = File.ReadAllLines(Environment.CurrentDirectory + "\\Resolution.txt").ElementAtOrDefault(2);

    static UIMainReferences() //reads the txt
    {
        var args = File.ReadAllLines(Environment.CurrentDirectory + "\\Resolution.txt");
        Width = Convert.ToInt32(args.ElementAtOrDefault(0));
        Height = Convert.ToInt32(args.ElementAtOrDefault(1));
        if (thirdLine.Contains("False")) Fullscreen = false; //not making it element 2 or i cant put comments in the txt
        else Fullscreen = true;
    }

    private void Awake()
    { 
        UIMainReferences.UIRefer = this;
        if (isGAMEFirstLaunch)
        {
            Screen.SetResolution(Width, Height, Fullscreen);
        }
        //if (FengGameManagerMKII.isAssetLoaded) AudioSource.PlayClipAtPoint(UIMainReferences.MenuClips[2], Camera.main.transform.position);
    }

    private void Start()
    {
        string versionShow = "8/12/2015";
        string versionForm = "08122015";
        fengVersion = "01042015";
        NGUITools.SetActive(this.panelMain, true);
        if ((version == null) || version.StartsWith("error"))
        {
            CLEARSKIES.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Verification failed. Please clear your cache or try another browser";
        }
        else if (version.StartsWith("outdated"))
        {
            CLEARSKIES.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Mod is outdated. Please clear your cache or try another browser.";
        }
        else
        {
            CLEARSKIES.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
        }
        if (isGAMEFirstLaunch)
        {
            FengGameManagerMKII.instance.DCPeopleList = new List<string>();
            version = fengVersion;
            isGAMEFirstLaunch = false;
            GameObject target = (GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("InputManagerController"));
            target.name = "InputManagerController";
            UnityEngine.Object.DontDestroyOnLoad(target);
            CLEARSKIES.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(new char[] { ',' });
            base.StartCoroutine(this.request(versionShow, versionForm));
            FengGameManagerMKII.loginstate = 0;
        }
        this.CreateSAButton();
    }

    private void CreateSAButton()
    {
        GameObject gameObject = this.panelMultiStart.transform.Find("ButtonServer1").gameObject;
        GameObject gameObject2 = NGUITools.AddChild(this.panelMultiStart, gameObject);
        UnityEngine.Object.Destroy(gameObject2.GetComponent<BTN_ServerUS>());
        gameObject2.AddComponent<BTN_Server_SA>();
        gameObject2.transform.localPosition = new Vector3(-110f, -85f, 0f);
        gameObject2.transform.Find("Label").GetComponent<UILabel>().text = "Sao Paolo";
    }
    
}

