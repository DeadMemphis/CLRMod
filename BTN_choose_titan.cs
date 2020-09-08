using ExitGames.Client.Photon;
using System;
using UnityEngine;

public class BTN_choose_titan : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            string id = "AHSS";
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
            FengGameManagerMKII.instance.needChooseSide = false;
            if (!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f))
            {
                FengGameManagerMKII.instance.NOTSpawnPlayer(id);
                FengGameManagerMKII.instance.photonView.RPC("restartGameByClient", PhotonTargets.MasterClient, new object[0]);
            }
            else
            {
                FengGameManagerMKII.instance.SpawnPlayer(id, "playerRespawn2");
            }
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            IN_GAME_MAIN_CAMERA.usingTitan = false;
            IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
            Hashtable hashtable = new Hashtable();
            hashtable.Add(PhotonPlayerProperty.character, id);
            Hashtable propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else
        {
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                FengGameManagerMKII.instance.checkpoint = CLEARSKIES.CacheGameObject.Find("PVPchkPtT");
            }
            string selection = CLEARSKIES.CacheGameObject.Find("PopupListCharacterTITAN").GetComponent<UIPopupList>().selection;
            NGUITools.SetActive(base.transform.parent.gameObject, false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
            if ((!PhotonNetwork.isMasterClient && (FengGameManagerMKII.instance.roundTime > 60f)) || FengGameManagerMKII.instance.justSuicide)
            {
                FengGameManagerMKII.instance.justSuicide = false;
                FengGameManagerMKII.instance.NOTSpawnNonAITitan(selection);
            }
            else
            {
                FengGameManagerMKII.instance.SpawnNonAITitan2(selection, "titanRespawn");
            }
            FengGameManagerMKII.instance.needChooseSide = false;
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(CLEARSKIES.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            IN_GAME_MAIN_CAMERA.usingTitan = true;
            IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
        }
    }

    private void Start()
    {
        if (!LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan)
        {
            base.gameObject.GetComponent<UIButton>().isEnabled = false;
        }
    }
}

