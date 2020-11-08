using System;
using UnityEngine;

public class BTN_BackToMain : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find<UIMainReferences>("UIRefer").panelMain, true);
        CLEARSKIES.CacheGameObject.Find<FengCustomInputs>("InputManagerController").menuOn = false;
        PhotonNetwork.Disconnect();
    }
}

