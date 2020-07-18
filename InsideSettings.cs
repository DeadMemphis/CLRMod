private ExitGames.Client.Photon.Hashtable InsideSettings()
    {
        int num;
        //int num2;
        //PhotonPlayer player;
        //int num3;
        float num4;
        float num5;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        if (((int)settings[0xe2]) > 0)
        {
            num = 50;
            if ((!int.TryParse((string)settings[0xe3], out num) || (num > 0x3e8)) || (num < 0))
            {
                settings[0xe3] = "50";
            }
            hashtable.Add("point", num);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Point limit (" + Convert.ToString(num) + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (((int)settings[0xc2]) > 0)
        {
            hashtable.Add("rock", (int)settings[0xc2]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Punk rock throwing</color></b> <color=#7b001c><i> disabled.</i></color>");
        }
        if (((int)settings[0xc3]) > 0)
        {
            num = 30;
            if ((!int.TryParse((string)settings[0xc4], out num) || (num > 100)) || (num < 0))
            {
                settings[0xc4] = "30";
            }
            hashtable.Add("explode", num);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Titan Explode Mode</color></b> <color=#7b001c><i> enabled.</i></color> <b><color=#b5ceff>(Radius " + Convert.ToString(num) + ")</color></b>");
        }
        if (((int)settings[0xc5]) > 0)
        {
            int result = 100;
            int num8 = 200;
            if ((!int.TryParse((string)settings[0xc6], out result) || (result > 0x186a0)) || (result < 0))
            {
                settings[0xc6] = "100";
            }
            if ((!int.TryParse((string)settings[0xc7], out num8) || (num8 > 0x186a0)) || (num8 < 0))
            {
                settings[0xc7] = "200";
            }
            hashtable.Add("healthMode", (int)settings[0xc5]);
            hashtable.Add("healthLower", result);
            hashtable.Add("healthUpper", num8);
            //if (PhotonNetwork.isMasterClient)
            //{
            //    GameSettings.healthMode = (int)settings[0xc5];
            //    GameSettings.healthLower = result;
            //    GameSettings.healthUpper = num8;
            //}

            string str = "Static";
            if ((int)settings[0xc5] == 2)
            {
                str = "Scaled";
            }
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Nape Health </color> <color=#" + MainColor + ">(<color=#b5ceff>" + str + ", </color><color=#7b001c><i>" + Convert.ToString(result) + "</i></color><color=#b5ceff> to </color><color=#7b001c><i>" + Convert.ToString(num8) + "</i></color>)</color></b> ");
        }
        if (((int)settings[0xca]) > 0)
        {
            hashtable.Add("eren", (int)settings[0xca]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Anti-Eren</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (((int)settings[0xcb]) > 0)
        {
            num = 1;
            if ((!int.TryParse((string)settings[0xcc], out num) || (num > 50)) || (num < 0))
            {
                settings[0xcc] = "1";
            }
            hashtable.Add("titanc", num);
            //if (PhotonNetwork.isMasterClient) GameSettings.moreTitans = num;
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add(" <color=#7b001c><i>" + Convert.ToString(num) + "</i></color> <b><color=#b5ceff> titans will spawn each round.</color></b>");
        }
        if (((int)settings[0xcd]) > 0)
        {
            num = 0x3e8;
            if ((!int.TryParse((string)settings[0xce], out num) || (num > 0x186a0)) || (num < 0))
            {
                settings[0xce] = "1000";
            }
            hashtable.Add("damage", num);
            //if (PhotonNetwork.isMasterClient)
            //{
            //    GameSettings.damageMode = num;
            //}
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Nape minimum damage</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(num) + "</i></color>" + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (((int)settings[0xcf]) > 0)
        {
            num4 = 1f;
            num5 = 3f;
            if ((!float.TryParse((string)settings[0xd0], out num4) || (num4 > 100f)) || (num4 < 0f))
            {
                settings[0xd0] = "1.0";
            }
            if ((!float.TryParse((string)settings[0xd1], out num5) || (num5 > 100f)) || (num5 < 0f))
            {
                settings[0xd1] = "3.0";
            }
            hashtable.Add("sizeMode", (int)settings[0xcf]);
            hashtable.Add("sizeLower", num4);
            hashtable.Add("sizeUpper", num5);
            //if (PhotonNetwork.isMasterClient)
            //{
            //    GameSettings.sizeLower = num4;
            //    GameSettings.sizeUpper = num5;
            //}
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Titans of sizes between </color><color=#7b001c><i>" + num4.ToString("F2") + "</i></color> <color=#b5ceff>and</color> <color=#7b001c><i>" + num5.ToString("F2") + "</i></color><color=#b5ceff> meter(s)</color></b>");
        }
        if (((int)settings[210]) > 0)
        {
            num4 = 20f;
            num5 = 20f;
            float num9 = 20f;
            float num10 = 20f;
            float num11 = 20f;
            if (!float.TryParse((string)settings[0xd3], out num4) || (num4 < 0f))
            {
                settings[0xd3] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd4], out num5) || (num5 < 0f))
            {
                settings[0xd4] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd5], out num9) || (num9 < 0f))
            {
                settings[0xd5] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd6], out num10) || (num10 < 0f))
            {
                settings[0xd6] = "20.0";
            }
            if (!float.TryParse((string)settings[0xd7], out num11) || (num11 < 0f))
            {
                settings[0xd7] = "20.0";
            }
            if (((((num4 + num5) + num9) + num10) + num11) > 100f)
            {
                settings[0xd3] = "20.0";
                settings[0xd4] = "20.0";
                settings[0xd5] = "20.0";
                settings[0xd6] = "20.0";
                settings[0xd7] = "20.0";
                num4 = 20f;
                num5 = 20f;
                num9 = 20f;
                num10 = 20f;
                num11 = 20f;
            }
            hashtable.Add("spawnMode", (int)settings[210]);
            hashtable.Add("nRate", num4);
            hashtable.Add("aRate", num5);
            hashtable.Add("jRate", num9);
            hashtable.Add("cRate", num10);
            hashtable.Add("pRate", num11);
            //if (PhotonNetwork.isMasterClient)
            //{
            //    GameSettings.nRate = num4;
            //    GameSettings.aRate = num5;
            //    GameSettings.jRate = num9;
            //    GameSettings.cRate = num10;
            //    GameSettings.pRate = num11;
            //}
            string txt = string.Empty;
            int pos = 0;
            if (num4 > 0)
            {
                txt += "<b><color=#b5ceff>NORMAL: </color></b><color=#7b001c><i>" + num4.ToString("F2") + "</i></color><b><color=#b5ceff>%";
                if (num5 > 0 || num9 > 0 || num10 > 0 || num11 > 0) txt += ", </color></b>";
                else txt += "</color></b>";
            }
            if (num5 > 0)
            {
                txt += "<b><color=#b5ceff>ABNORMAL: </color></b><color=#7b001c><i>" + num5.ToString("F2") + "</i></color><b><color=#b5ceff>%";
                if (num9 > 0 || num10 > 0 || num11 > 0) txt += ", </color></b>";
                else txt += "</color></b>";
            }
            if (num9 > 0)
            {
                txt += "<b><color=#b5ceff>JUMPER: </color></b><color=#7b001c><i>" + num9.ToString("F2") + "</i></color><b><color=#b5ceff>%";
                if (num10 > 0 || num11 > 0) txt += ", </color></b>";
                else txt += "</color></b>";
            }
            if (num10 > 0)
            {
                txt += "<b><color=#b5ceff>CRAWLER: </color></b><color=#7b001c><i>" + num10.ToString("F2") + "</i></color><b><color=#b5ceff>%";
                if (num11 > 0) txt += ", </color></b>";
                else txt += "</color></b>";
            }
            if (num11 > 0)
            {
                txt += "<b><color=#b5ceff>PUNK: </color></b><color=#7b001c><i>" + num11.ToString("F2") + "</i></color><b><color=#b5ceff>%</color></b>";
            }
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Custom spawn rate </color> <color=#" + MainColor + ">(" + txt + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }

        if (((int)settings[0xd9]) > 0)
        {
            num = 1;
            if (!int.TryParse((string)settings[0xda], out num) || (num > 50))
            {
                settings[0xda] = "1";
            }
            hashtable.Add("waveModeOn", (int)settings[0xd9]);
            hashtable.Add("waveModeNum", num);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Custom wave mode</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(num) + "</i></color>" + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }

        if (((int)settings[0xdd]) > 0)
        {
            num = 20;
            if ((!int.TryParse((string)settings[0xde], out num) || (num > 0xf4240)) || (num < 0))
            {
                settings[0xde] = "20";
            }
            hashtable.Add("maxwave", num);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Max wave</color></b> <color=#7b001c><i> is " + Convert.ToString(num) + ".</i></color>");
        }
        if (((int)settings[0xdf]) > 0)
        {
            num = 5;
            if ((!int.TryParse((string)settings[0xe0], out num) || (num > 0xf4240)) || (num < 5))
            {
                settings[0xe0] = "5";
            }
            hashtable.Add("endless", num);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Endless Respawn</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(num) + "</i></color> <color=#b5ceff>seconds</color>" + ")</color></b>");
        }
        //if ((int)settings[264] > 0)
        //{
        //    hashtable.Add("noguest", (int)settings[264]);
        //    if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>NO GUEST</color></b><color=#7b001c><i> enabled.</i></color>");
        //}
        if (((string)settings[0xe1]) != string.Empty)
        {
            hashtable.Add("motd", (string)settings[0xe1]);
        }

        if (((int)settings[0xe5]) > 0)
        {
            hashtable.Add("punkWaves", (int)settings[0xe5]);
        }
        if (((int)settings[0x105]) > 0)
        {
            hashtable.Add("deadlycannons", (int)settings[0x105]);
        }
        if (GameSettings.racingStatic > 0)
        {
            hashtable.Add("asoracing", 1);
        }
        return hashtable;
    }