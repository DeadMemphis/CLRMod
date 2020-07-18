using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CLEARSKIES
{
    class CoreManager
    {
        public static CoreManager instance = new CoreManager();

        public void Core()
        {
            if (((int)FengGameManagerMKII.settings[0x40]) >= 100)
            {
                CoreEditor();
                return;
            }
            if(IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {

            }
            if(IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {

            }
        }
        public void CoreEditor()
        {

        }
        public void CoreAdd()
        {

        }
        public void CoreSingle()
        {

        }
        public void CoreMultiplayer()
        {
            if(FengGameManagerMKII.instance.needChooseSide)
            {
                if (FengCustomInputs.Inputs.isInputDown[InputCode.flare1])
                {
                    if (NGUITools.GetActive(FengGameManagerMKII.instance.uirefer.panels[3]))
                    {
                        Screen.lockCursor = true;
                        Screen.showCursor = true;
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[0], true);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[1], false);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[2], false);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[3], false);
                        IN_GAME_MAIN_CAMERA.spectate.disable = false;
                        IN_GAME_MAIN_CAMERA.mouselook.disable = false;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[0], false);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[1], false);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[2], false);
                        NGUITools.SetActive(FengGameManagerMKII.instance.uirefer.panels[3], true);
                        IN_GAME_MAIN_CAMERA.spectate.disable = true;
                        IN_GAME_MAIN_CAMERA.mouselook.disable = true;
                    }
                }
                if (FengCustomInputs.Inputs.isInputDown[15] && !NGUITools.GetActive(FengGameManagerMKII.instance.uirefer.panels[3]))
                {
                    //NGUITools.SetActive(this.uirefer.panels[0], false);
                    //NGUITools.SetActive(this.uirefer.panels[1], true);
                    //NGUITools.SetActive(this.uirefer.panels[2], false);
                    //NGUITools.SetActive(this.uirefer.panels[3], false);
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                    IN_GAME_MAIN_CAMERA.spectate.disable = true;
                    IN_GAME_MAIN_CAMERA.mouselook.disable = true;
                    //FengCustomInputs.Inputs.showKeyMap();
                    //FengCustomInputs.Inputs.justUPDATEME();
                    FengCustomInputs.Inputs.menuOn = true;
                }
            }
            CoreAdd();
            string content = string.Empty;
            switch(IN_GAME_MAIN_CAMERA.gamemode)
            {
                case GAMEMODE.RACING: 
                {
                        //FengGameManagerMKII.instance.ShowHUDInfoTopCenter("Time : " + 
                        //    ((FengGameManagerMKII.instance.roundTime >= 20f) ? (((((int)(FengGameManagerMKII.instance.roundTime * 10f)) * 0.1f) - 20f)).ToString() : "WAITING"));
                        //if (FengGameManagerMKII.instance.roundTime < 20f)
                        //{
                        //    FengGameManagerMKII.instance.ShowHUDInfoCenter("RACE START IN " + ((int)(20f - FengGameManagerMKII.instance.roundTime)) + (!(FengGameManagerMKII.instance.localRacingResult == string.Empty) ? ("\nLast Round\n" + FengGameManagerMKII.instance.localRacingResult) : "\n\n"));
                        //}
                        //else if (!FengGameManagerMKII.instance.startRacing)
                        //{
                        //    this.ShowHUDInfoCenter(string.Empty);
                        //    FengGameManagerMKII.instance.startRacing = true;
                        //    FengGameManagerMKII.instance.endRacing = false;
                        //    GameObject doors = CacheGameObject.Find("door");
                        //    if (doors != null) doors.SetActive(false);
                        //    if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                        //    {
                        //        foreach (GameObject go in this.racingDoors)
                        //            go.SetActive(false);
                        //        this.racingDoors = null;
                        //    }


                        //}
                        //else if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                        //{
                        //    foreach (GameObject go in this.racingDoors)
                        //        go.SetActive(false);
                        //    this.racingDoors = null;
                        //}
                        break;
                }
                default:
                        break;
            }
        }
    }
}
