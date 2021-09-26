﻿using System;
using UnityEngine;

public class BTN_Server_EU : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        //PhotonNetwork.ConnectToMaster("app-eu.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, UIMainReferences.version);
        if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.Udp)
            PhotonNetwork.ConnectToMaster("135.125.239.180", 5055, FengGameManagerMKII.applicationId, UIMainReferences.version);
        else if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.WebSocket)
            PhotonNetwork.ConnectToMaster("135.125.239.180", 9090, FengGameManagerMKII.applicationId, UIMainReferences.version);
        else if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.Tcp)
            PhotonNetwork.ConnectToMaster("135.125.239.180", 4530, FengGameManagerMKII.applicationId, UIMainReferences.version);
        FengGameManagerMKII.OnPrivateServer = true;
    }
}

