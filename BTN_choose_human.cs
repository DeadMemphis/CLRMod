using ExitGames.Client.Photon;
using System;
using UnityEngine;

public class BTN_choose_human : MonoBehaviour
{
    public bool isPlayerAllDead()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (((int) player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if ((bool) player.customProperties[PhotonPlayerProperty.dead])
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isPlayerAllDead2()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    private void OnClick()
    {
        string selection = CLEARSKIES.CacheGameObject.Find("PopupListCharacterHUMAN").GetComponent<UIPopupList>().selection;
        NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
        FengGameManagerMKII.instance.needChooseSide = false;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            FengGameManagerMKII.instance.checkpoint = CLEARSKIES.CacheGameObject.Find("PVPchkPtH");
        }
        if (!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f))
        {
            if (!this.isPlayerAllDead2())
            {
                FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
            }
            else
            {
                FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
                FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
            }
        }
        else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST)) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE))
        {
            if (this.isPlayerAllDead2())
            {
                FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
                FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
            }
            else
            {
                FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
            }
        }
        else
        {
            FengGameManagerMKII.instance.SpawnPlayer(selection, "playerRespawn");
        }
        UIReferArray uireferArray = CLEARSKIES.CacheGameObject.Find<UIReferArray>("UI_IN_GAME");
        NGUITools.SetActive(uireferArray.panels[1], false);
        NGUITools.SetActive(uireferArray.panels[2], false);
        NGUITools.SetActive(uireferArray.panels[3], false);
        IN_GAME_MAIN_CAMERA.usingTitan = false;
        IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
        Hashtable hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.character, selection);
        Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
    }
}

