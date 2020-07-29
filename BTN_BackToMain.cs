using System;
using UnityEngine;

public class BTN_BackToMain : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(BRM.CacheGameObject.Find<UIMainReferences>("UIRefer").panelMain, true);
        BRM.CacheGameObject.Find<FengCustomInputs>("InputManagerController").menuOn = false;
        PhotonNetwork.Disconnect();
    }
}

