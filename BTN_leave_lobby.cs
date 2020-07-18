using System;
using UnityEngine;

public class BTN_leave_lobby : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
        PhotonNetwork.Disconnect();
    }
}

