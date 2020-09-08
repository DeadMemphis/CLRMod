using System;
using UnityEngine;

public class BTN_RESULT_TO_MAIN : MonoBehaviour
{
    private void OnClick()
    {
        Time.timeScale = 1f;
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        FengGameManagerMKII.gameStart = false;
        Screen.lockCursor = false;
        Screen.showCursor = true;
        CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        UnityEngine.Object.Destroy(CLEARSKIES.CacheGameObject.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}

