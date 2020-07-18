using System;
using UnityEngine;

public class BTN_ToLAN : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);
    }
}

