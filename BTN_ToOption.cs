using System;
using UnityEngine;

public class BTN_ToOption : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelOption, true);
        BRM.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().showKeyMap();
        BRM.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = true;
    }
}

