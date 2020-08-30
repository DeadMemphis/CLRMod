using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UIMainReferences : MonoBehaviour
{
    public static string fengVersion;
    private static bool isGAMEFirstLaunch = true;
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
    public static Material sky_Night;


    private void Awake()
    {
        UIMainReferences.UIRefer = this;
    }

    // Token: 0x060017B2 RID: 6066 RVA: 0x00010B36 File Offset: 0x0000ED36
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
            
        }
    }

    private void Start()
    {
        if (UIMainReferences.sky_Night == null)
        {
            UIMainReferences.sky_Night = Resources.Load<IN_GAME_MAIN_CAMERA>("MainCamera_mono").skyBoxNIGHT;
        }
        Camera.main.GetComponent<Skybox>().material = UIMainReferences.sky_Night;
        Camera.main.GetComponent<Camera>().transform.rotation = new Quaternion(0f, 1f, 0f, 0f);
        Camera.main.GetComponent<Camera>().transform.position = new Vector3(0f, 0f, 0f);
        string versionShow = "8/12/2015";
        string versionForm = "08122015";
        fengVersion = "01042015";
        NGUITools.SetActive(this.panelMain, true);
        if ((version == null) || version.StartsWith("error"))
        {
            BRM.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Verification failed. Please clear your cache or try another browser";
        }
        else if (version.StartsWith("outdated"))
        {
            BRM.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Mod is outdated. Please clear your cache or try another browser.";
        }
        else
        {
            BRM.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
        }
        if (isGAMEFirstLaunch)
        {
            version = fengVersion;
            isGAMEFirstLaunch = false;
            GameObject target = (GameObject) UnityEngine.Object.Instantiate(BRM.CacheResources.Load("InputManagerController"));
            target.name = "InputManagerController";
            UnityEngine.Object.DontDestroyOnLoad(target);
            BRM.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text = "Client verified. Last updated " + versionShow + ".";
            FengGameManagerMKII.s = "verified343,hair,character_eye,glass,character_face,character_head,character_hand,character_body,character_arm,character_leg,character_chest,character_cape,character_brand,character_3dmg,r,character_blade_l,character_3dmg_gas_r,character_blade_r,3dmg_smoke,HORSE,hair,body_001,Cube,Plane_031,mikasa_asset,character_cap_,character_gun".Split(new char[] { ',' });
            base.StartCoroutine(this.request(versionShow, versionForm));
            FengGameManagerMKII.loginstate = 3;
        }
    }

}

