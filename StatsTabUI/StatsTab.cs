using ExitGames.Client.Photon;
using UnityEngine;
using System;
using System.CodeDom;
using System.Linq;
using System.Collections.Generic;
using BRM;
using CLEARSKIES;
using System.Collections;

namespace BRM
{
    public class StatsTab : Photon.MonoBehaviour
    {
        public static int fullBytes;
        public static int fullPacksCount;
        public Rect IncRect = new Rect(0f, 100f, 500f, 520f);
        public bool IncWindowOn;
        public static int Choose;
        public static int UndChoose;
        public static int buttOn;
        public bool buttonsOn;
        public bool healthStatsVisible;
        public bool trafficStatsOn;
        public bool statsOn = true;
        private Vector2 scroll = Vector2.zero;
        private Vector2 scroll2 = Vector2.zero;
        private PhotonPlayer currPlayer;
        private int buttSettings;
        private string Paks = "1000";
        private string[] strarr;
        public static List<string> InGameLog = new List<string>();
        private string StatsTabStatus = "debug";
        private bool permanent = false;
        static int doubles = 0;
        static bool Stop = false;
        #region RCProps Keys
        private static List<string> propsrc = new List<string>
        {
            "beard_texture_id",
            "body_texture",
            "cape",
            "character",
            "costumeId",
            "currentLevel",
            "customBool",
            "customFloat",
            "customInt",
            "customString",
            "dead",
            "deaths",
            "division",
            "eye_texture_id",
            "glass_texture_id",
            "guildName",
            "hair_color1",
            "hair_color2",
            "hair_color3",
            "hairInfo",
            "heroCostumeId",
            "isTitan",
            "kills",
            "max_dmg",
            "name",
            "part_chest_1_object_mesh",
            "part_chest_1_object_texture",
            "part_chest_object_mesh",
            "part_chest_object_texture",
            "part_chest_skinned_cloth_mesh",
            "part_chest_skinned_cloth_texture",
            "RCBombA",
            "RCBombB",
            "RCBombG",
            "RCBombR",
            "RCBombRadius",
            "RCteam",
            "sex",
            "skin_color",
            "statACL",
            "statBLA",
            "statGAS",
            "statSKILL",
            "statSPD",
            "team",
            "total_dmg",
            "uniform_type",
            "sender"
        };
        #endregion

        struct Log
        {
            public string message;
            public string stackTrace;
            public LogType type;
        }

        #region Inspector Settings
        public bool shakeToOpen = true;
        public float shakeAcceleration = 3f;
        public bool restrictLogCount = false;
        public int maxLogs = 100;
        #endregion

        List<Log> logs = new List<Log>();
        Vector2 scrollPosition;
        Vector2 scrollPosition2;
        bool collapse;
        string time;

        static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

        const int margin = 20;
        static readonly GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        static readonly GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        readonly Rect titleBarRect = new Rect(0, 0, 10000, 20);

        private void Permanent()
        {
            permanent = currPlayer.isDCMarked;
            bool perm = GUILayout.Toggle(permanent, "Permanent");
            if (perm != permanent)
            {
                LoginFengKAI.AddToBanList(currPlayer);
                currPlayer.isDCMarked = true;
            }
        }

        private void TestThreading()
        {
            AddLine("TestThreading()", DebugType.LOG);
        }

        public void GUIstatusPlayer(int windowID)
        {
            //GUILayout.BeginArea(new Rect(0f, 0f, 200f, 150f));
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("NetworkingPeer"))
            {
                Choose = 0;
            }
            if (GUILayout.Button("Full NetTraffic"))
            {
                Choose = 1;
            }
            if (GUILayout.Button("RPC List"))
            {
                Choose = 2;
            }
            if (GUILayout.Button("Event List"))
            {
                Choose = 3;
            }
            if (GUILayout.Button("Player List"))
            {
                Choose = 4;
            }
            if (GUILayout.Button("Console"))
            {
                Choose = 5;
                //BRM.Console.ConsoleCall = true; 
            }
            if (GUILayout.Button("Custom"))
            {
                Choose = 6;
                //BRM.Console.ConsoleCall = true; 
            }
            GUILayout.EndHorizontal();
            switch (Choose)
            {
                case 0:
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.BeginVertical(GUILayout.Width(160f));
                        {
                            GUILayout.Label("Nickname:");
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                GUILayout.Label("[" + player.ID.ToString() + "]" + RCextensions.returnStringFromObject(player.customProperties["name"]).ToRGBA(), GUILayout.Height(20f));
                            }
                        }
                        GUILayout.EndVertical();

                        GUILayout.BeginVertical(GUILayout.Width(100f));
                        {
                            GUILayout.Label("Bytes:");
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                GUILayout.Label((player.Bites / 8f).ToString("F"), GUILayout.Height(20f));
                            }
                        }
                        GUILayout.EndVertical();
                        GUILayout.BeginVertical(GUILayout.Width(100f));
                        {
                            GUILayout.Label("KB:");
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                GUILayout.Label(((player.Bites / 8f) / 1024f).ToString("F"), GUILayout.Height(20f));
                            }
                        }
                        GUILayout.EndVertical();
                        GUILayout.BeginVertical(GUILayout.Width(100f));
                        {
                            GUILayout.Label("Paks:");
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                GUILayout.Label((player.Packs).ToString(), GUILayout.Height(20f));
                            }
                        }
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Total:", GUILayout.Width(160f));
                        GUILayout.Label(((fullBytes / 8f)).ToString("F"), GUILayout.Width(100f));
                        GUILayout.Label(((fullBytes / 8f) / 1024f).ToString("F"), GUILayout.Width(100f));
                        GUILayout.Label((fullPacksCount).ToString(), GUILayout.Width(100f));
                        GUILayout.EndHorizontal();
                        if (GUILayout.Button("Reset:", GUILayout.Width(100f)))
                        {
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                player.Packs = 0;
                                player.Bites = 0;
                                fullBytes = 0;
                                fullPacksCount = 0;
                            }
                        }
                        break;
                    }
                case 1:
                    {
                        bool flag = false;
                        TrafficStatsGameLevel trafficStatsGameLevel = PhotonNetwork.networkingPeer.TrafficStatsGameLevel;
                        long num = PhotonNetwork.networkingPeer.TrafficStatsElapsedMs / 0x3e8L;
                        if (num == 0L)
                        {
                            num = 1L;
                        }

                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                        this.buttonsOn = GUILayout.Toggle(this.buttonsOn, "buttons", new GUILayoutOption[0]);
                        this.healthStatsVisible = GUILayout.Toggle(this.healthStatsVisible, "health", new GUILayoutOption[0]);
                        this.trafficStatsOn = GUILayout.Toggle(this.trafficStatsOn, "traffic", new GUILayoutOption[0]);
                        GUILayout.EndHorizontal();
                        string text = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", trafficStatsGameLevel.TotalOutgoingMessageCount, trafficStatsGameLevel.TotalIncomingMessageCount, trafficStatsGameLevel.TotalMessageCount);
                        string str2 = String.Format("{0} sec average:", num);
                        string str3 = string.Format("Out|In|Sum:\t{0,4} | {1,4} | {2,4}", ((long)trafficStatsGameLevel.TotalOutgoingMessageCount) / num, ((long)trafficStatsGameLevel.TotalIncomingMessageCount) / num, ((long)trafficStatsGameLevel.TotalMessageCount) / num);
                        GUILayout.Label(text, new GUILayoutOption[0]);
                        GUILayout.Label(str2, new GUILayoutOption[0]);
                        GUILayout.Label(str3, new GUILayoutOption[0]);
                        if (this.buttonsOn)
                        {
                            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                            this.statsOn = GUILayout.Toggle(this.statsOn, "stats on", new GUILayoutOption[0]);
                            if (GUILayout.Button("Reset", new GUILayoutOption[0]))
                            {
                                PhotonNetwork.networkingPeer.TrafficStatsReset();
                                PhotonNetwork.networkingPeer.TrafficStatsEnabled = true;
                            }
                            flag = GUILayout.Button("To Log", new GUILayoutOption[0]);
                            GUILayout.EndHorizontal();
                        }
                        string str4 = string.Empty;
                        string str5 = string.Empty;
                        if (this.trafficStatsOn)
                        {
                            str4 = "Incoming: " + PhotonNetwork.networkingPeer.TrafficStatsIncoming.ToString();
                            str5 = "Outgoing: " + PhotonNetwork.networkingPeer.TrafficStatsOutgoing.ToString();
                            GUILayout.Label(str4, new GUILayoutOption[0]);
                            GUILayout.Label(str5, new GUILayoutOption[0]);
                        }
                        string str6 = string.Empty;
                        if (this.healthStatsVisible)
                        {
                            str6 = string.Format("ping: {6}[+/-{7}]ms\nlongest delta between\nsend: {0,4}ms disp: {1,4}ms\nlongest time for:\nev({3}):{2,3}ms op ({5}):{4,3}ms", new object[] { trafficStatsGameLevel.LongestDeltaBetweenSending, trafficStatsGameLevel.LongestDeltaBetweenDispatching, trafficStatsGameLevel.LongestEventCallback, trafficStatsGameLevel.LongestEventCallbackCode, trafficStatsGameLevel.LongestOpResponseCallback, trafficStatsGameLevel.LongestOpResponseCallbackOpCode, PhotonNetwork.networkingPeer.RoundTripTime, PhotonNetwork.networkingPeer.RoundTripTimeVariance });
                            GUILayout.Label(str6, new GUILayoutOption[0]);
                        }
                        if (flag)
                        {
                            Debug.Log(String.Format("{0}{1}{2}{3}{4}{5}", text, str2, str3, str4, str5, str6));
                        }
                        break;
                    }
                case 3:
                    GUILayout.BeginVertical();
                    GUILayout.Label("IN_GAME_MAIN_CAMERA calls find neck: ");
                    if (GUILayout.Button("Test Thrading"))
                    {
                        AsyncHelper.BeginInBackground(new Action(TestThreading));
                    }
                    GUILayout.EndVertical();
                    break;
                case 4:
                    {
                        GUILayout.BeginArea(new Rect(5, 30, 250, 450));
                        scroll = GUILayout.BeginScrollView(scroll, false, false);

                        GUIStyle style = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleLeft };
                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                        {
                            style.normal = player == currPlayer ? GUI.skin.button.onNormal : GUI.skin.button.normal;
                            if (GUILayout.Button(new GUIContent("[" + player.ID.ToString() + "]" + player.uiname.ToRGBA()), style))
                            {
                                currPlayer = player;
                            }
                        }
                        GUILayout.EndScrollView();
                        if (currPlayer != null)
                        {
                            GUILayout.BeginVertical(GUILayout.MaxWidth(250f));
                            GUILayout.Label(FengGameManagerMKII.Colr("ChatName: " + currPlayer.Chatname.ToRGBA()));
                            GUILayout.Label(FengGameManagerMKII.Colr("Guild: " + currPlayer.guildname.ToRGBA()));
                            GUILayout.Label(FengGameManagerMKII.Colr("Stats: SPD: " + Convert.ToString(currPlayer.customProperties[PhotonPlayerProperty.statSPD]) + " /GAS: " + Convert.ToString(currPlayer.customProperties[PhotonPlayerProperty.statGAS]) + " \n/BLA: " + Convert.ToString(currPlayer.customProperties[PhotonPlayerProperty.statBLA]) + " /ACL: " + Convert.ToString(currPlayer.customProperties[PhotonPlayerProperty.statACL])));
                            GUILayout.Label(FengGameManagerMKII.Colr("HashCode: " + currPlayer.GetHashCode().ToString()));
                            if (GUILayout.Button("Set mark"))
                            {
                                currPlayer.isDCMarked = true;
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndArea();
                        GUILayout.BeginArea(new Rect(252, 30, 240, 450));
                        GUI.Box(new Rect(252, 30, 200, 300), " ");
                        GUI.backgroundColor = Color.gray;
                        GUILayout.Label(FengGameManagerMKII.Colr("All props:"));
                        scroll2 = GUILayout.BeginScrollView(scroll2, false, false);
                        if (currPlayer != null)
                        {
                            foreach (DictionaryEntry prop in currPlayer.customProperties)
                            {
                                GUILayout.BeginHorizontal();
                                if (!propsrc.Contains(prop.Key.ToString()))
                                {
                                    GUILayout.Label("<i><color=#ff0000>unusual key</color></i>: " + prop.Key.ToString());
                                    GUILayout.Label("<i><color=#ff0000>unusual value</color></i>: " + prop.Value.ToString());
                                }
                                else
                                {
                                    GUILayout.Label("<i><color=#ffdc2e>key</color></i>: " + prop.Key.ToString());
                                    GUILayout.Label("<i><color=#ffdc2e>value</color></i>: " + prop.Value.ToString());
                                }
                                GUILayout.EndHorizontal();
                            }
                        }
                        GUILayout.EndScrollView();

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(FengGameManagerMKII.Colr("Kill")))
                        {
                            FengGameManagerMKII.instance.killPlayer1(currPlayer);
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("LagDC")))
                        {
                            buttSettings = buttSettings != 4 ? 4 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("DC")))
                        {
                            buttSettings = buttSettings != 5 ? 5 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("Rekt")))
                        {
                            foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                            {
                                if (((string)player2.customProperties[PhotonPlayerProperty.name]) == currPlayer.uiname)
                                {
                                    for (int i = 0; i < 120; i++)
                                    {
                                        PhotonNetwork.DestroyPlayerObjects(player2);
                                        RaiseEventOptions options3 = new RaiseEventOptions();
                                        options3.TargetActors = new int[] { player2.ID };
                                        RaiseEventOptions options4 = options3;
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcb, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfe, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe1, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe5, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd0, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcb, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe5, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(220, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe1, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe4, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfd, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfe, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(210, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd3, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd4, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd7, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcb, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfe, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe1, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe5, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe1, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd0, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(210, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd3, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcb, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcf, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe5, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(220, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe1, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xe4, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfd, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xfe, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xef, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd4, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xd6, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xc9, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xca, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcb, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(0xcc, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options4);
                                        FengGameManagerMKII.instance.photonView.RPC("titanGetKill", player2, new object[0]);
                                        FengGameManagerMKII.instance.photonView.RPC("Chat", player2, new object[0]);
                                    }
                                }
                            }
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("HashDC")))
                        {
                            buttSettings = buttSettings != 6 ? 6 : 0;

                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(FengGameManagerMKII.Colr("ByteDc")))
                        {
                            buttSettings = buttSettings != 1 ? 1 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("Spam Events")))
                        {
                            InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID + "...</color>");
                            FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.EventSpam(currPlayer));
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(FengGameManagerMKII.Colr("SerializeDC")))
                        {
                            buttSettings = buttSettings != 2 ? 2 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("DoInstDC")))
                        {
                            buttSettings = buttSettings != 3 ? 3 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("SRC")))
                        {
                            buttSettings = buttSettings != 7 ? 7 : 0;
                        }
                        if (GUILayout.Button(FengGameManagerMKII.Colr("BanAC")))
                        {

                            NetworkingPeer.instance.BanAC(203, null, true, new RaiseEventOptions()
                            {
                                TargetActors = new int[]
                            {
                                currPlayer.ID
                            }
                            });
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(FengGameManagerMKII.Colr("Bytes")))
                        {
                            buttSettings = buttSettings != 8 ? 8 : 0;
                        }
                        GUILayout.EndHorizontal();
                        if (buttSettings == 4 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();
                            Permanent();
                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.Lag(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID +
                                          "...</color>");
                            }
                        }
                        if (buttSettings == 7 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();

                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.SerializeCrash(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID +
                                          "...</color>");
                            }
                        }
                        if (buttSettings == 8 && currPlayer != null)
                        {
                            Permanent();
                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                DC.LightDC(currPlayer.ID);
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID +
                                          "...</color>");
                            }
                        }
                        if (buttSettings == 1 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();
                            Permanent();
                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.ByteDC(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID + "...</color>");
                            }
                        }
                        if (buttSettings == 3 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();

                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.DoInstantiateDC(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID + "...</color>");
                            }
                        }
                        if (buttSettings == 2 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();

                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.SerializeDC(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID + "...</color>");
                            }
                        }
                        if (buttSettings == 6 && currPlayer != null)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Paks: ");
                            Paks = GUILayout.TextField(Paks);
                            GUILayout.EndHorizontal();

                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.myDC(currPlayer, Convert.ToInt32(Paks)));
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID +
                                          "...</color>");
                            }
                        }
                        if (buttSettings == 5 && currPlayer != null)
                        {
                            long loop = 0;
                            int num = 0;
                            Queue<int> queue14 = new Queue<int>();
                            queue14.Enqueue(currPlayer.ID);
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Bytes: ");
                            Paks = GUILayout.TextField(Paks);
                            strarr = new string[] { FengGameManagerMKII.Colr("b"), FengGameManagerMKII.Colr("kb"), FengGameManagerMKII.Colr("mb"), FengGameManagerMKII.Colr("gb"), FengGameManagerMKII.Colr("tb") };
                            num = GUILayout.SelectionGrid(num, strarr, 1, GUI.skin.toggle);
                            GUILayout.EndHorizontal();
                            if (num == 1)
                            {
                                loop = 1000L;
                            }
                            if (num == 2)
                            {
                                loop = 1000000L;
                            }
                            //switch (selct)
                            //{
                            //    case 1:
                            //        {
                            //            loop = 1000L;
                            //            break;
                            //        }
                            //    case 2:
                            //        {
                            //            loop = 1000000L;
                            //            break;
                            //        }
                            //    case 3:
                            //        {
                            //            loop = 1000000000L;
                            //            break;
                            //        }
                            //    case 4:
                            //        {
                            //            loop = 1000000000000L;
                            //            break;
                            //        }
                            //}
                            loop = Convert.ToInt64(Paks) * loop;

                            if (GUILayout.Button(FengGameManagerMKII.Colr("Start")))
                            {
                                FengGameManagerMKII.instance.AutoDisconnect(currPlayer, currPlayer.ID, 250000L, true, true);
                                //FengGameManagerMKII.instance.AutoDisconnect(currPlayer, currPlayer.ID, loop, true, true);
                                InRoomChat.addLINE2("<color=red>Disconnecting <color=white>ID:</color> " + currPlayer.ID +
                                         "...</color>");
                            }
                        }
                        GUILayout.EndArea();
                        //GUILayout.BeginArea(new Rect(0, 205, 245, 190));
                        //GUI.backgroundColor = Color.gray;

                        //GUILayout.BeginVertical(GUILayout.MaxWidth(240f));
                        //GUILayout.EndVertical();

                        //GUILayout.EndArea();
                        break;
                    }
                case 5:
                    {
                        GUILayout.BeginArea(new Rect(5, 500, 495, 20));
                        DrawToolbar();
                        GUILayout.EndArea();

                        GUILayout.BeginArea(new Rect(5, 30, 495, 470));
                        DrawLogsList();
                        GUILayout.EndArea();

                        break;
                    }
                case 6:
                    GUILayout.BeginVertical();
                    GUILayout.Label(FengGameManagerMKII.Colr("currient Script:"));
                    GUI.Box(new Rect(5, 35, 400, 250), " ");
                    GUI.backgroundColor = Color.black;
                    GUILayout.BeginArea(new Rect(5, 30, 400, 250));
                    scroll = GUILayout.BeginScrollView(scroll, false, false);
                    GUILayout.Label(FengGameManagerMKII.currentScript == null ? "Empty" : FengGameManagerMKII.currentScript);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.Label(FengGameManagerMKII.Colr("currient Level:"));
                    GUI.Box(new Rect(5, 285, 400, 250), " ");
                    GUI.backgroundColor = Color.black;
                    GUILayout.BeginArea(new Rect(5, 280, 400, 250));
                    scroll2 = GUILayout.BeginScrollView(scroll2, false, false);
                    GUILayout.Label(FengGameManagerMKII.currentLevel == null ? "Empty" : FengGameManagerMKII.currentLevel);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();

                    GUILayout.EndVertical();
                    break;
                default:
                    break;
            }
            //GUILayout.EndArea();
            GUI.DragWindow();







        }

        public static System.Collections.IEnumerator CacheDC() //slow cache 
        {
            StatsTab.AddLine("Start crash region.", DebugType.LOG);
            float[] arrayedFS = new float[150];
            for (int f = 0; f < arrayedFS.Length; f++) { arrayedFS[f] = -928132231f; }
            for (int i = 0; i < 2; i++) { PhotonNetwork.networkingPeer.OpRaiseEvent(206, arrayedFS, true, new RaiseEventOptions { CachingOption = EventCaching.AddToRoomCacheGlobal }); }
            yield return new WaitForEndOfFrame();
        }

        public void DrawToolbar()
        {

            GUILayout.BeginHorizontal();
            if (StatsTabStatus == "debug")
            {
                if (GUILayout.Button(clearLabel))
                {
                    logs.Clear();
                }
                collapse = GUILayout.Toggle(collapse, collapseLabel, GUILayout.ExpandWidth(false));
            }
            if (StatsTabStatus == "console")
            {
                string incommand = "";
                incommand = GUILayout.TextField(incommand, GUILayout.MaxWidth(450f));
                if (GUILayout.Button(clearLabel))
                {
                    InGameLog.Clear();
                }
            }
            GUILayout.EndHorizontal();
        }

        public void DrawLogsList()
        {
            GUI.backgroundColor = Color.gray;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Debug"))
            {
                UndChoose = 0;
                StatsTabStatus = "debug";
            }
            if (GUILayout.Button("In Game Console"))
            {
                UndChoose = 1;
                StatsTabStatus = "console";
            }

            GUILayout.EndHorizontal();
            
            if (UndChoose == 0)
            {
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                GUILayout.BeginVertical();
                for (var i = 0; i < logs.Count; i++)
                {
                    var log = logs[i];

                    if (collapse && i > 0)
                    {
                        var previousMessage = logs[i - 1].message;

                        if (log.message == previousMessage)
                        {
                            continue;
                        }
                    }

                    GUI.contentColor = logTypeColors[log.type];

                    time = "<b><color=black>[<color=yellow>" + System.DateTime.UtcNow.ToLocalTime().Hour + "<color=black>:</color>";
                    if (System.DateTime.UtcNow.ToLocalTime().Minute < 10) time += "0" + System.DateTime.UtcNow.ToLocalTime().Minute + "</color>]</color></b>";
                    else time += System.DateTime.UtcNow.ToLocalTime().Minute + "</color>]</color></b>";
                    GUILayout.Label(time + " <color=white>Console</color>: " + log.message + "\n<b><color=white>[StackTrace]:" + log.stackTrace + "</color></b>");
                }
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }

            if (UndChoose == 1)
            {
                scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
                GUILayout.BeginVertical();
                string text = string.Empty;
                if (InGameLog.Count < 15)
                {
                    text = InGameLog.Aggregate(text, (current, t) => current + t + "\n");
                }
                else
                {
                    for (int j = InGameLog.Count - 15; j < InGameLog.Count; j++)
                    {
                        text = text + InGameLog[j] + "\n";
                    }
                }
                time = "<b><color=black>[<color=yellow>" + System.DateTime.UtcNow.ToLocalTime().Hour + "<color=black>:</color>";
                if (System.DateTime.UtcNow.ToLocalTime().Minute < 10) time += "0" + System.DateTime.UtcNow.ToLocalTime().Minute + "</color>]</color></b>";
                else time += System.DateTime.UtcNow.ToLocalTime().Minute + "</color>]</color></b>";
                GUILayout.Label(time + " <color=white>Console</color>: " + text);
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
            }

            var innerScrollRect = GUILayoutUtility.GetLastRect();
            
            var outerScrollRect = GUILayoutUtility.GetLastRect();

            if (Event.current.type == EventType.Repaint && IsScrolledToBottom(innerScrollRect, outerScrollRect))
            {
                ScrollToBottom();
            }

            GUI.contentColor = Color.white;
        }

        void ScrollToBottom()
        {
            scrollPosition = new Vector2(0, Int32.MaxValue);
        }

        bool IsScrolledToBottom(Rect innerScrollRect, Rect outerScrollRect)
        {
            var innerScrollHeight = innerScrollRect.height;

            var outerScrollHeight = outerScrollRect.height - GUI.skin.box.padding.vertical;

            if (outerScrollHeight > innerScrollHeight)
            {
                return true;
            }

            var scrolledToBottom = Mathf.Approximately(innerScrollHeight, scrollPosition.y + outerScrollHeight);
            return scrolledToBottom;
        }

        public static void AddLine(string content, DebugType type = (DebugType)1)
        {
            string str = "<b><color=black>[<color=yellow>" + System.DateTime.UtcNow.ToLocalTime().Hour + "<color=black>:</color>" + System.DateTime.UtcNow.Minute.ToString() + "</color>]</color><color=";
            string str2 = string.Empty;
            if (type == DebugType.WARNING)
            {
                str2 = "yellow>";
                InRoomChat.addLINE2("Get <color=yellow>warning</color>. Check inGame console for more information.");
            }
            if (type == DebugType.LOG)
            {
                str2 = "7b001c>";
            }
            if (type == DebugType.ERROR)
            {
                str2 = "red>";
                InRoomChat.addLINE2("Get <color=red>error</color>. Check inGame console for more information.");
            }
            if (type == DebugType.RPC)
            {
                str2 = "#00ffff>";
            }
            if (type == DebugType.UPDATE)
            {
                str2 = "#660066>";
            }
            if (!InGameLog.Contains(content)) InGameLog.Add(str + str2 + content + "</color></b>");
            else InGameLog.Add(str + str2 + content + "</color></b> (x" + (doubles++) + ")");
        }

        public enum DebugType
        {
            ERROR,
            LOG,
            RPC,
            WARNING,
            UPDATE
        }

        void HandleLog(string message, string stackTrace, LogType type)
        {
            //if (!logs.Contains(message)) logs.Add(new Log
            //{
            //    message = message,
            //    stackTrace = stackTrace,
            //    type = type,
            //});
            //else 
            logs.Add(new Log
            {
                message = message,
                stackTrace = stackTrace,
                type = type,
            });

            TrimExcessLogs();
        }

        void HandleCommand(string arguments)
        {

        }

        void TrimExcessLogs()
        {
            if (!restrictLogCount)
            {
                return;
            }

            var amountToRemove = Mathf.Max(logs.Count - maxLogs, 0);

            if (amountToRemove == 0)
            {
                return;
            }

            logs.RemoveRange(0, amountToRemove);
        }

        void OnEnable()
        {
#if UNITY_5
			Application.logMessageReceived += HandleLog;
#else
            Application.RegisterLogCallback(HandleLog);
#endif
        }

        void OnDisable()
        {
#if UNITY_5
			Application.logMessageReceived -= HandleLog;
#else
            Application.RegisterLogCallback(null);
#endif
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                IncWindowOn = !IncWindowOn;
                //this.statsOn = true;
            }
        }

        void OnGUI()
        {
            if (IncWindowOn)
            {
                GUI.backgroundColor = Color.black;
                /*new Color(/*FengGameManagerMKII.BackGRColor1, FengGameManagerMKII.BackGRColor2, FengGameManagerMKII.BackGRColor3, FengGameManagerMKII.BackGRColor4);*/
                GUI.Box(IncRect, string.Empty);

                IncRect = GUILayout.Window(100, IncRect, GUIstatusPlayer, ".:Traffic:.", GUI.skin.box);
            }
        }
    }
}
