using System;
using CLEARSKIES;
using UnityEngine;

public class BTN_START_MULTI_SERVER : MonoBehaviour
{
    // Token: 0x06001643 RID: 5699 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
  

    // Token: 0x06001644 RID: 5700 RVA: 0x000F8D60 File Offset: 0x000F6F60
    private void OnClick()
    {
        //if (!(bool)FengGameManagerMKII.settings[82])
        //{
            string text = CacheGameObject.Find<UIInput>("InputServerName").label.text;
            int maxPlayers = int.Parse(CacheGameObject.Find<UIInput>("InputMaxPlayer").label.text);
            int num = int.Parse(CacheGameObject.Find<UIInput>("InputMaxTime").label.text);
            string selection = CacheGameObject.Find<UIPopupList>("PopupListMap").selection;
            string text2 = (!CacheGameObject.Find<UICheckbox>("CheckboxHard").isChecked) ? ((!CacheGameObject.Find<UICheckbox>("CheckboxAbnormal").isChecked) ? "normal" : "abnormal") : "hard";
            string text3 = IN_GAME_MAIN_CAMERA.dayLight.ToString().ToLower();
            string text4 = CacheGameObject.Find<UIInput>("InputStartServerPWD").label.text;
            //FengGameManagerMKII.settings[77] = ((text4 == string.Empty) ? string.Empty : ("PWD:" + text4 + "\r\n"));
            if (text4.Length > 0)
            {
                text4 = new SimpleAES().Encrypt(text4);
            }
            string roomName = string.Concat(new object[]
            {
                text,
                "`",
                selection,
                "`",
                text2,
                "`",
                num,
                "`",
                text3,
                "`",
                text4,
                "`",
                UnityEngine.Random.Range(0, 50000)
            });
            RoomOptions roomOptions = new RoomOptions
            {
                isVisible = true,
                isOpen = true,
                maxPlayers = maxPlayers
            };
            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        //}
    }
    //private void OnClick()
    //{
    //    string text = CLEARSKIES.CacheGameObject.Find("InputServerName").GetComponent<UIInput>().label.text;
    //    int maxPlayers = int.Parse(CLEARSKIES.CacheGameObject.Find("InputMaxPlayer").GetComponent<UIInput>().label.text);
    //    int num2 = int.Parse(CLEARSKIES.CacheGameObject.Find("InputMaxTime").GetComponent<UIInput>().label.text);
    //    string selection = CLEARSKIES.CacheGameObject.Find("PopupListMap").GetComponent<UIPopupList>().selection;
    //    string str3 = !CLEARSKIES.CacheGameObject.Find("CheckboxHard").GetComponent<UICheckbox>().isChecked ? (!CLEARSKIES.CacheGameObject.Find("CheckboxAbnormal").GetComponent<UICheckbox>().isChecked ? "normal" : "abnormal") : "hard";
    //    string str4 = string.Empty;
    //    if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Day)
    //    {
    //        str4 = "day";
    //    }
    //    if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Dawn)
    //    {
    //        str4 = "dawn";
    //    }
    //    if (IN_GAME_MAIN_CAMERA.dayLight == DayLight.Night)
    //    {
    //        str4 = "night";
    //    }
    //    string unencrypted = CLEARSKIES.CacheGameObject.Find("InputStartServerPWD").GetComponent<UIInput>().label.text;
    //    if (unencrypted.Length > 0)
    //    {
    //        unencrypted = new SimpleAES().Encrypt(unencrypted);
    //    }
    //    PhotonNetwork.CreateRoom(string.Concat(new object[] { text, "`", selection, "`", str3, "`", num2, "`", str4, "`", unencrypted, "`", UnityEngine.Random.Range(0, 0xc350) }), true, true, maxPlayers);
    //}
}

