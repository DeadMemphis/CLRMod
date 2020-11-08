using System;
using UnityEngine;

public class BTN_ToCredit : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(base.transform.parent.gameObject, false);
        NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelCredits, true);
    }
}

