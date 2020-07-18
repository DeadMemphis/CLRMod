using System;
using UnityEngine;

public class BTN_Server_List_PgUp : MonoBehaviour
{
    private void OnClick()
    {
        BRM.CacheGameObject.Find("PanelMultiROOM").GetComponent<PanelMultiJoin>().pageUp();
    }
}

