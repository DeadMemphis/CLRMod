using System;
using UnityEngine;

public class BTN_toPublicServer : MonoBehaviour
{
    private void OnClick()
    {
        //NGUITools.SetActive(base.transform.parent.gameObject, false);
        //if ((string)FengGameManagerMKII.settings[263] == "yes" || (string)FengGameManagerMKII.settings[263] == "on")
        if (FengGameManagerMKII.serverList)
        {
            CLEARSKIES.ServerListGUI.serverlistGUI.enabled = true;
            //return;
        }
        //NGUITools.SetActive(UIMainReferences.UIRefer.panelMultiROOM, true);
        else
        {
            NGUITools.SetActive(base.transform.parent.gameObject, false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiROOM, true);
        }
       
    }
}

