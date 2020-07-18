using System;
using UnityEngine;

public class LevelTriggerRacingEnd : MonoBehaviour
{
    private bool disable;

    private void OnTriggerStay(Collider other)
    {
        if (!this.disable && (other.gameObject.tag == "Player"))
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                FengGameManagerMKII.instance.gameWin2();
                this.disable = true;
            }
            else if (other.gameObject.GetComponent<HERO>().photonView.isMine)
            {
                FengGameManagerMKII.instance.multiplayerRacingFinsih();
                this.disable = true;
            }
        }
    }

    private void Start()
    {
        this.disable = false;
    }
}

