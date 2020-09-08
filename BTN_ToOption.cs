using System;
using UnityEngine;

public class BTN_ToOption : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelOption, true);
        CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().showKeyMap();
        CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = true;
    }
}

