using System;
using UnityEngine;

public class BTN_ServerUS : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        //PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", 0x13bf, FengGameManagerMKII.applicationId, UIMainReferences.version);
        if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.Udp)
            PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", 5055, FengGameManagerMKII.applicationId, UIMainReferences.version);
        else if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.WebSocket)
            PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", 9090, FengGameManagerMKII.applicationId, UIMainReferences.version);
        else if (PhotonNetwork.networkingPeer.UsedProtocol == ExitGames.Client.Photon.ConnectionProtocol.Tcp)
            PhotonNetwork.ConnectToMaster("app-us.exitgamescloud.com", 4530, FengGameManagerMKII.applicationId, UIMainReferences.version);
        FengGameManagerMKII.OnPrivateServer = false;
    }
}

