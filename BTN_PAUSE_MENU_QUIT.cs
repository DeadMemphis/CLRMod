using System;
using UnityEngine;

public class BTN_PAUSE_MENU_QUIT : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Time.timeScale = 1f;
        }
        else
        {
            PhotonNetwork.Disconnect();
        }
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        FengGameManagerMKII.gameStart = false;
        CLEARSKIES.CacheGameObject.Find<FengCustomInputs>("InputManagerController").menuOn = false;
        UnityEngine.Object.Destroy(CLEARSKIES.CacheGameObject.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
        FengGameManagerMKII.ShowMenuButtonGUI = true;
    }
}

