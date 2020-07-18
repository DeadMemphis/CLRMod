using System;
using UnityEngine;

public class BTN_SetDefault : MonoBehaviour
{
    private void OnClick()
    {
        BRM.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().setToDefault();
        BRM.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().showKeyMap();
    }
}

