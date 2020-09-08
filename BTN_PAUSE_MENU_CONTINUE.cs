using System;
using UnityEngine;

public class BTN_PAUSE_MENU_CONTINUE : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Time.timeScale = 1f;
        }
        GameObject obj2 = CLEARSKIES.CacheGameObject.Find("UI_IN_GAME");
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[0], true);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[1], false);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[2], false);
        NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[3], false);
        if (!IN_GAME_MAIN_CAMERA.mainCamera.enabled)
        {
            Screen.showCursor = true;
            Screen.lockCursor = true;
            CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
            CLEARSKIES.CacheGameObject.Find("MainCamera").GetComponent<SpectatorMovement>().disable = false;
            CLEARSKIES.CacheGameObject.Find("MainCamera").GetComponent<MouseLook>().disable = false;
        }
        else
        {
            IN_GAME_MAIN_CAMERA.isPausing = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS) 
            {
                Screen.showCursor = false;
                Screen.lockCursor = true;
            }
            else
            {
                Screen.showCursor = false;
                Screen.lockCursor = false;
            }
            CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
            CLEARSKIES.CacheGameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().justUPDATEME();
        }
    }
}

