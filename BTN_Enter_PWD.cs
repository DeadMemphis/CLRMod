﻿using System;
using UnityEngine;

public class BTN_Enter_PWD : MonoBehaviour
{
    private void OnClick()
    {
        string text = BRM.CacheGameObject.Find("InputEnterPWD").GetComponent<UIInput>().label.text;
        PhotonNetwork.JoinRoom(PanelMultiJoinPWD.roomName);

    }
}

