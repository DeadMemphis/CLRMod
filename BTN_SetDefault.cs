using System;
using UnityEngine;

public class BTN_SetDefault : MonoBehaviour
{
    private void OnClick()
    {
        CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().setToDefault();
        CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().showKeyMap();
    }
}

