using System;
using UnityEngine;

public class BTN_START_SINGLE_GAMEPLAY : MonoBehaviour
{
    private void OnClick()
    {
        string selection = BRM.CacheGameObject.Find("PopupListMap").GetComponent<UIPopupList>().selection;
        string str2 = BRM.CacheGameObject.Find("PopupListCharacter").GetComponent<UIPopupList>().selection;
        int num = !BRM.CacheGameObject.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? (!BRM.CacheGameObject.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? 0 : 2) : 1;
        IN_GAME_MAIN_CAMERA.difficulty = num;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.SINGLE;
        IN_GAME_MAIN_CAMERA.singleCharacter = str2.ToUpper();
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        Screen.showCursor = false;
        if (selection == "trainning_0")
        {
            IN_GAME_MAIN_CAMERA.difficulty = -1;
        }
        FengGameManagerMKII.level = selection;
        Application.LoadLevel(LevelInfo.getInfo(selection).mapName);
    }
}

