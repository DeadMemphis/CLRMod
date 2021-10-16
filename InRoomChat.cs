using ExitGames.Client.Photon;
using Photon;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using SimpleJSON;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CommandAttribute : Attribute
{
    public string Name { get; private set; }

    public CommandAttribute() => Name = null;
    public CommandAttribute(string name) => Name = name;
}

public class InRoomChat : Photon.MonoBehaviour
{
    private bool AlignBottom = true;
    public static readonly string ChatRPC = "Chat";
    public static Rect GuiRect = new Rect(0f, 100f, 300f, 470f);
    public static Rect GuiRect2 = new Rect(30f, 575f, 300f, 25f);
    private string inputLine = string.Empty;
    public bool IsVisible = true;
    public static List<string> messages = new List<string>();
    private Vector2 scrollPos = Vector2.zero;

    public static InRoomChat ChatInstanse;

    private static readonly Dictionary<string, MethodInfo> CommandsCache = new Dictionary<string, MethodInfo>();

    static InRoomChat()
    {
        MethodInfo[] infos = typeof(InRoomChat).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

        Type cmdAttrType = typeof(CommandAttribute);

        foreach (MethodInfo info in infos)
        {
            object[] attrs = info.GetCustomAttributes(cmdAttrType, false);
            if (attrs == null) continue;

            if (attrs.Length > 0)
            {
                foreach (object attr in attrs)
                {
                    if (attr is CommandAttribute cmdAttr)
                    {
                        if (cmdAttr.Name == null) CommandsCache.Add(info.Name.ToLower(), info);
                        else CommandsCache.Add(cmdAttr.Name.ToLower(), info);
                    }
                }
            }
        }
    }

    private static bool MessageHistoryUpdatecache = true;
    private static string MessageHistoryCache = string.Empty;
    public static string GetCompiledMessages
    {
        get
        {
            if (MessageHistoryUpdatecache)
            {
                MessageHistoryUpdatecache = false;
                MessageHistoryCache = string.Empty;

                if (messages.Count < 15)
                {
                    for (int i = 0; i < messages.Count; i++)
                    {
                        MessageHistoryCache += messages[i] + Environment.NewLine;
                    }
                }
                else
                {
                    for (int i = messages.Count - 15; i < messages.Count; i++)
                    {
                        MessageHistoryCache += messages[i] + Environment.NewLine;
                    }
                }
            }

            return MessageHistoryCache;
        }
    }

    private void Awake()
    {
        ChatInstanse = this;
    }

    public void addLINE(string newLine) => Write(newLine);

    public static void Write(string newLine)
    {
        if (messages.Count > 512) messages.Clear();

        MessageHistoryUpdatecache = true;

        messages.Add(newLine.FilterSizeTag());
    }

    public static void Chat(string msg, string sender = "")
    {
        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, new object[2]
        {
            msg, sender
        });
    }

    private void ShowMessageWindow()
    {
        GUI.SetNextControlName(string.Empty);
        GUILayout.BeginArea(InRoomChat.GuiRect);
        GUILayout.FlexibleSpace();
        GUILayout.Label(GetCompiledMessages);
        GUILayout.EndArea();
    }
    
    public void OnGUI()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE && (int)FengGameManagerMKII.settings[244] == 1)
        {
            this.ShowMessageWindow();
        }
        if (!this.IsVisible || (PhotonNetwork.connectionStateDetailed != PeerStates.Joined))
        {
            return;
        }
        switch (Event.current.type)
        {
            case EventType.keyUp:
                if (((Event.current.keyCode != KeyCode.None) && (Event.current.keyCode == FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat])) && (GUI.GetNameOfFocusedControl() != "ChatInput"))
                {
                    this.inputLine = string.Empty;
                    GUI.FocusControl("ChatInput");
                }
                break;

            case EventType.KeyDown:
                switch (Event.current.keyCode)
                {
                    case KeyCode.KeypadEnter:
                    case KeyCode.Return:
                        if (!string.IsNullOrEmpty(this.inputLine))
                        {
                            if (this.inputLine == "\t")
                            {
                                this.inputLine = string.Empty;
                                GUI.FocusControl(string.Empty);
                                return;
                            }
                            if (FengGameManagerMKII.RCEvents.ContainsKey("OnChatInput"))
                            {
                                string key = (string)FengGameManagerMKII.RCVariableNames["OnChatInput"];
                                if (FengGameManagerMKII.stringVariables.ContainsKey(key))
                                {
                                    FengGameManagerMKII.stringVariables[key] = this.inputLine;
                                }
                                else
                                {
                                    FengGameManagerMKII.stringVariables.Add(key, this.inputLine);
                                }
                                ((RCEvent)FengGameManagerMKII.RCEvents["OnChatInput"]).checkEvent();
                            }
                            if (!this.inputLine.StartsWith("/"))
                            {
                                string str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]).hexColor();
                                if (str2 == string.Empty)
                                {
                                    str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
                                    if (PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null)
                                    {
                                        if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                                        {
                                            str2 = "<color=#00FFFF>" + str2 + "</color>";
                                        }
                                        else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                                        {
                                            str2 = "<color=#FF00FF>" + str2 + "</color>";
                                        }
                                    }
                                }

                                object[] parameters = new object[] { this.inputLine, str2 };
                                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, parameters);
                                
                            }
                            else
                            {
                                string[] args = this.inputLine.Substring(1).Split(' ');

                                if (CommandsCache.TryGetValue(args[0], out MethodInfo info))
                                {
                                    if (info.IsStatic) info.Invoke(null, new object[1] { args });
                                    else info.Invoke(this, new object[1] { args });
                                }
                                else
                                {
                                    Write($"No such command as {args[0]}");
                                }
                            }

                            this.inputLine = string.Empty;
                            GUI.FocusControl(string.Empty);
                            return;
                        }
                        this.inputLine = "\t";
                        GUI.FocusControl("ChatInput");
                        break;

                    default:
                        if ((((Event.current.keyCode == KeyCode.Tab) || (Event.current.character == '\t')) && !IN_GAME_MAIN_CAMERA.isPausing) && (FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] != KeyCode.Tab))
                        {
                            Event.current.Use();
                        }
                        break;
                }
                break;
        }

        GUI.SetNextControlName(string.Empty);
        GUILayout.BeginArea(GuiRect);
        GUILayout.FlexibleSpace();
        GUILayout.Label(GetCompiledMessages);
        GUILayout.EndArea();

        GUILayout.BeginArea(GuiRect2);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUI.SetNextControlName("ChatInput");
        this.inputLine = GUILayout.TextField(this.inputLine, new GUILayoutOption[] { GUILayout.MaxWidth(280f) });
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public void setPosition()
    {
        if (this.AlignBottom)
        {
            GuiRect = new Rect(5f, (float)(Screen.height - 500), 330f, 470f);
            GuiRect2 = new Rect(30f, (float)((Screen.height - 300) + 0x113), 300f, 25f);
        }
    }

    public void Start()
    {
        if (ChatInstanse == null) ChatInstanse = this;
        this.setPosition();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && FengGameManagerMKII.timeofAutorejoin.IsRunning && !PhotonNetwork.inRoom)
    //    {
    //        FengGameManagerMKII.timeofAutorejoin.Stop();
    //        FengGameManagerMKII.timeofAutorejoin.Reset();
    //        FengGameManagerMKII.shallRejoin[0] = false;
    //        UnityEngine.Object.Destroy(GameObject.Find("MultiplayerManager"));
    //        Application.LoadLevel("menu");
    //    }
    //}

    [Command("r")]
    [Command("restart")]
    private void GameRestart(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Chat("<color=#FFCC00>MasterClient has restarted the game!</color>");
            FengGameManagerMKII.instance.restartRC();
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }
    
    [Command("aso")]
    private void ASORules(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (args.Length != 2)
            {
                Write("<color=#FFCC00>Usage: /aso <kdr|racing> </color>");
                return;
            }

            switch (args[1])
            {
                case "kdr":
                    if (GameSettings.asoPreservekdr == 0)
                    {
                        GameSettings.asoPreservekdr = 1;
                        Write("<color=#FFCC00>KDRs will be preserved from disconnects.</color>");
                    }
                    else
                    {
                        GameSettings.asoPreservekdr = 0;
                        Write("<color=#FFCC00>KDRs will not be preserved from disconnects.</color>");
                    }
                    break;

                case "racing":
                    if (GameSettings.racingStatic == 0)
                    {
                        GameSettings.racingStatic = 1;
                        Write("<color=#FFCC00>Racing will not end on finish.</color>");
                    }
                    else
                    {
                        GameSettings.racingStatic = 0;
                        Write("<color=#FFCC00>Racing will end on finish.</color>");
                    }
                    break;

                default:
                    Write("<color=#FFCC00>Usage: /aso <kdr|racing> </color>");
                    break;
            }
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("pause")]
    private void GamePause(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Chat("<color=#FFCC00>MasterClient has paused the game.</color>");
            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { true });
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("unpause")]
    private void GameUnPause(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Chat("<color=#FFCC00>MasterClient has unpaused the game.</color>");
            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { false });
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("clear")]
    private void ChatClear(string[] args)
    {
        messages.Clear();
        Write("Cleared");
    }

    [Command("ignorelist")]
    private void IgnoreList(string[] args)
    {
        string str = string.Empty;

        foreach (int num2 in FengGameManagerMKII.ignoreList)
        {
            str += $"{num2}{Environment.NewLine}";
        }

        Write(str);
    }

    //[Command("mute")]
    //private void PlayerMute(string[] args)
    //{
    //    if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer player))
    //    {
    //        FengGameManagerMKII.instance.muteList.Add(player.ID);
    //        FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
    //    }
    //}

    //[Command("unmute")]
    //private void PlayerUnMute(string[] args)
    //{
    //    if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer player))
    //    {
    //        FengGameManagerMKII.instance.muteList.Remove(player.ID);
    //        FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
    //    }
    //}

    [Command("room")]
    private void Room(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            switch (args[1])
            {
                case "max":
                    int max = Convert.ToInt32(args[2]);
                    FengGameManagerMKII.instance.maxPlayers = max;
                    PhotonNetwork.room.maxPlayers = max;
                    Chat($"<color=#FFCC00>Max players changed to {max}!</color>");
                    break;

                case "time":
                    float time = Convert.ToSingle(args[2]);
                    FengGameManagerMKII.instance.addTime(time);
                    Chat($"<color=#FFCC00> {time} seconds added to the clock.</color>");
                    break;

                default:
                    Write("<color=#FFCC00>Usage: /room <max|time> <number> </color>");
                    break;
            }
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("ign")]
    private void PlayerIgnore(string[] args)
    {
        if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer player))
        {
            FengGameManagerMKII.ignoreList.Add(player.ID);
            FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
        }
    }

    [Command("uni")]
    private void PlayerUnIgnore(string[] args)
    {
        if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer player))
        {
            if (FengGameManagerMKII.ignoreList.Contains(player.ID))
                FengGameManagerMKII.ignoreList.Remove(player.ID);

            if (NetworkingPeer.instantiateCounter.ContainsKey(player))
                NetworkingPeer.instantiateCounter[player].Clear();

            if (NetworkingPeer.rpcCounter.ContainsKey(player))
                NetworkingPeer.rpcCounter[player].Clear();

            FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
        }
    }

    [Command("remove")]
    private void PlayerRemoveFromDC(string[] args)
    {
        if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer player))
        {
            FengGameManagerMKII.ignoreList.Remove(player.ID);

            if (FengGameManagerMKII.instance.DCPeopleList.Contains(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name])))
                FengGameManagerMKII.instance.DCPeopleList.Remove(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]));

            if (NetworkingPeer.instantiateCounter.ContainsKey(player))
                NetworkingPeer.instantiateCounter[player].Clear();

            if (NetworkingPeer.rpcCounter.ContainsKey(player))
                NetworkingPeer.rpcCounter[player].Clear();

            FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
        }
    }

    [Command("resetkd")]
    private void ResetKD(string[] args)
    {
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { PhotonPlayerProperty.kills, 0 },
            { PhotonPlayerProperty.deaths, 0 },
            { PhotonPlayerProperty.max_dmg, 0 },
            { PhotonPlayerProperty.total_dmg, 0 }
        });

        Write("<color=#FFCC00>Your stats have been reset. </color>");
    }

    [Command("resetkdall")]
    private void ResetKDAll(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable
            {
                { PhotonPlayerProperty.kills, 0 },
                { PhotonPlayerProperty.deaths, 0 },
                { PhotonPlayerProperty.max_dmg, 0 },
                { PhotonPlayerProperty.total_dmg, 0 }
            };

            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                player.SetCustomProperties(hash);

            Chat("<color=#FFCC00>All stats have been reset.</color>");
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("pm")]
    private void PlayerPrivateMessage(string[] args)
    {
        if (NetworkingPeer.mActors.TryGetValue(Convert.ToInt32(args[1]), out PhotonPlayer targetPlayer))
        {
            string str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]).hexColor();
            if (str2 == string.Empty)
            {
                str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]);
                if (PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                    {
                        str2 = "<color=#00FFFF>" + str2 + "</color>";
                    }
                    else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                    {
                        str2 = "<color=#FF00FF>" + str2 + "</color>";
                    }
                }
            }
            string str3 = RCextensions.returnStringFromObject(targetPlayer.customProperties[PhotonPlayerProperty.name]).hexColor();
            if (str3 == string.Empty)
            {
                str3 = RCextensions.returnStringFromObject(targetPlayer.customProperties[PhotonPlayerProperty.name]);
                if (targetPlayer.customProperties[PhotonPlayerProperty.RCteam] != null)
                {
                    if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                    {
                        str3 = "<color=#00FFFF>" + str3 + "</color>";
                    }
                    else if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                    {
                        str3 = "<color=#FF00FF>" + str3 + "</color>";
                    }
                }
            }
            string str4 = string.Join(" ", args, 2, args.Length - 2);
            
                FengGameManagerMKII.instance.photonView.RPC("ChatPM", targetPlayer, new object[] { str2, str4 });
                Write("<color=#FFC000>TO [" + targetPlayer.ID.ToString() + "]</color> " + str3 + ":" + str4);
           
        }
        else Write($"<color=#FFCC00>No player with id {args[1]}</color>");
    }
    

    [Command("specmode")]
    private void SpectateMode(string[] args)
    {
        if (((int)FengGameManagerMKII.settings[0xf5]) == 0)
        {
            FengGameManagerMKII.settings[0xf5] = 1;
            FengGameManagerMKII.instance.EnterSpecMode(true);
            Write("<color=#FFCC00>You have entered spectator mode.</color>");
        }
        else
        {
            FengGameManagerMKII.settings[0xf5] = 0;
            FengGameManagerMKII.instance.EnterSpecMode(false);
            Write("<color=#FFCC00>You have exited spectator mode.</color>");
        }
    }

    [Command("spec")]
    [Command("spectate")]
    private void PlayerSpectate(string[] args)
    {
        int ID = Convert.ToInt32(args[1]);

        foreach (GameObject obj5 in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj5.GetPhotonView().owner.ID == ID)
            {
                IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(obj5, true, false);
                IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(false);
            }
        }
    }

    [Command("rev")]
    [Command("revive")]
    private void PlayerRevive(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            int ID = Convert.ToInt32(args[1]);

            if (NetworkingPeer.mActors.TryGetValue(ID, out PhotonPlayer player))
            {
                Write($"<color=#FFCC00>Player {ID} has been revived.</color>");
                if (((player.customProperties[PhotonPlayerProperty.dead] != null) && RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead])) && (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2))
                {
                    player.Chat("<color=#FFCC00>You have been revived by the master client.</color>");
                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                }
            }
        }
    }

    [Command("revall")]
    [Command("reviveall")]
    private void PlayerReviveAll(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            Chat("<color=#FFCC00>All players have been revived.</color>");
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (((player.customProperties[PhotonPlayerProperty.dead] != null) && RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead])) && (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2))
                {
                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                }
            }
        }
    }

    [Command("kick")]
    private void PlayerKick(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    int ID = Convert.ToInt32(args[i]);
                    if (ID == PhotonNetwork.player.ID)
                    {
                        Write("error: can't kick yourself.");
                    }
                    else
                    {
                        if (NetworkingPeer.mActors.TryGetValue(ID, out PhotonPlayer player))
                        {
                            FengGameManagerMKII.instance.kickPlayerRC(player, false, "");

                            Chat($"<color=#FFCC00>{RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name])} has been kicked from the server!</color>");
                        }
                        else Write("error: no such player.");
                    }
                }
            }
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("ban")]
    private void PlayerBan(string[] args)
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    int ID = Convert.ToInt32(args[i]);
                    if (ID == PhotonNetwork.player.ID)
                    {
                        Write("error: can't ban yourself.");
                    }
                    else
                    {
                        if (NetworkingPeer.mActors.TryGetValue(ID, out PhotonPlayer player))
                        {
                            FengGameManagerMKII.instance.kickPlayerRC(player, true, "");

                            Chat($"<color=#FFCC00>{RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name])} has been banned from the server!</color>");
                        }
                        else Write("error: no such player.");
                    }
                }
            }
        }
        else Write("<color=#FFCC00>error: not master client</color>");
    }

    [Command("banlist")]
    private void PlayerBanlist(string[] args)
    {
        Write("<color=#FFCC00>List of banned players:</color>");
        string str = string.Empty;
        foreach (int ID in FengGameManagerMKII.banHash.Keys)
        {
            str += $"<color=#FFCC00>{ID} : {FengGameManagerMKII.banHash[ID]} </color>{Environment.NewLine}";
        }

        Write(str);
    }

    [Command("unban")]
    private void PlayerUnBan(string[] args)
    {
        int ID = Convert.ToInt32(args[1]);
        if (FengGameManagerMKII.banHash.ContainsKey(ID))
        {

            string name = ((string)FengGameManagerMKII.banHash[ID]);

            if (FengGameManagerMKII.instance.DCPeopleList.Contains(name))
                FengGameManagerMKII.instance.DCPeopleList.Remove(name);

            FengGameManagerMKII.banHash.Remove(ID);
            FengGameManagerMKII.banHash.Remove(name); //must clear dclist first and then banhash, or move num9 in name

            if (NetworkingPeer.mActors.TryGetValue(ID, out PhotonPlayer player))
            {
                FengGameManagerMKII.ignoreList.Remove(player.ID);
            }

            FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
            Chat($"<color=#FFCC00>{name} has been unbanned from the server. </color>");
        }
        else Write("error: no such player");
    }

    [Command("team")]
    private void ChangeTeam(string[] args)
    {
        if (GameSettings.teamMode == 1)
        {
            switch (args[1])
            {
                case "0":
                case "individual":
                    FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 0 });
                    Write("<color=#00FFFF>You have joined individuals.</color>");
                    foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (obj2.GetPhotonView().isMine)
                        {
                            HERO hero = obj2.GetComponent<HERO>();
                            hero.markDie();
                            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                        }
                    }
                    break;

                case "1":
                case "cyan":
                    FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 1 });
                    Write("<color=#00FFFF>You have joined team cyan.</color>");
                    foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (obj2.GetPhotonView().isMine)
                        {
                            HERO hero = obj2.GetComponent<HERO>();
                            hero.markDie();
                            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                        }
                    }
                    break;

                case "2":
                case "magenta":
                    FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 2 });
                    Write("<color=#00FFFF>You have joined team magenta.</color>");
                    foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (obj2.GetPhotonView().isMine)
                        {
                            HERO hero = obj2.GetComponent<HERO>();
                            hero.markDie();
                            hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                        }
                    }
                    break;

                default:
                    Write("<color=#FFCC00>Usage: /team <0|1|2> OR /team <cyan|magenta|individual>.</color>");
                    break;
            }
        }
        else Write("<color=#FFCC00>error: teams are locked or disabled. </color>");
    }

    [Command("para")]
    private void Rejoin(string[] args)
    {
        PhotonNetwork.networkingPeer.OnStatusChanged(StatusCode.DisconnectByServerLogic);
    }


    [Command("rules")]
    private void Rules(string[] args)
    {
        Write("<color=#FFCC00>Currently activated gamemodes:</color>");
        if (GameSettings.bombMode > 0)
        {
            Write("<color=#FFCC00>Bomb mode is on.</color>");
        }
        if (GameSettings.teamMode > 0)
        {
            if (GameSettings.teamMode == 1)
            {
                Write("<color=#FFCC00>Team mode is on (no sort).</color>");
            }
            else if (GameSettings.teamMode == 2)
            {
                Write("<color=#FFCC00>Team mode is on (sort by size).</color>");
            }
            else if (GameSettings.teamMode == 3)
            {
                Write("<color=#FFCC00>Team mode is on (sort by skill).</color>");
            }
        }
        if (GameSettings.pointMode > 0)
        {
            Write("<color=#FFCC00>Point mode is on (" + Convert.ToString(GameSettings.pointMode) + ").</color>");
        }
        if (GameSettings.disableRock > 0)
        {
            Write("<color=#FFCC00>Punk Rock-Throwing is disabled.</color>");
        }
        if (GameSettings.spawnMode > 0)
        {
            Write("<color=#FFCC00>Custom spawn rate is on (" + GameSettings.nRate.ToString("F2") + "% Normal, " + GameSettings.aRate.ToString("F2") + "% Abnormal, " + GameSettings.jRate.ToString("F2") + "% Jumper, " + GameSettings.cRate.ToString("F2") + "% Crawler, " + GameSettings.pRate.ToString("F2") + "% Punk </color>");
        }
        if (GameSettings.explodeMode > 0)
        {
            Write("<color=#FFCC00>Titan explode mode is on (" + Convert.ToString(GameSettings.explodeMode) + ").</color>");
        }
        if (GameSettings.healthMode > 0)
        {
            Write("<color=#FFCC00>Titan health mode is on (" + Convert.ToString(GameSettings.healthLower) + "-" + Convert.ToString(GameSettings.healthUpper) + ").</color>");
        }
        if (GameSettings.infectionMode > 0)
        {
            Write("<color=#FFCC00>Infection mode is on (" + Convert.ToString(GameSettings.infectionMode) + ").</color>");
        }
        if (GameSettings.damageMode > 0)
        {
            Write("<color=#FFCC00>Minimum nape damage is on (" + Convert.ToString(GameSettings.damageMode) + ").</color>");
        }
        if (GameSettings.moreTitans > 0)
        {
            Write("<color=#FFCC00>Custom titan is on (" + Convert.ToString(GameSettings.moreTitans) + ").</color>");
        }
        if (GameSettings.sizeMode > 0)
        {
            Write("<color=#FFCC00>Custom titan size is on (" + GameSettings.sizeLower.ToString("F2") + "," + GameSettings.sizeUpper.ToString("F2") + ").</color>");
        }
        if (GameSettings.banEren > 0)
        {
            Write("<color=#FFCC00>Anti-Eren is on. Using Titan eren will get you kicked.</color>");
        }
        if (GameSettings.waveModeOn == 1)
        {
            Write("<color=#FFCC00>Custom wave mode is on (" + Convert.ToString(GameSettings.waveModeNum) + ").</color>");
        }
        if (GameSettings.friendlyMode > 0)
        {
            Write("<color=#FFCC00>Friendly-Fire disabled. PVP is prohibited.</color>");
        }
        if (GameSettings.pvpMode > 0)
        {
            if (GameSettings.pvpMode == 1)
            {
                Write("<color=#FFCC00>AHSS/Blade PVP is on (team-based).</color>");
            }
            else if (GameSettings.pvpMode == 2)
            {
                Write("<color=#FFCC00>AHSS/Blade PVP is on (FFA).</color>");
            }
        }
        if (GameSettings.maxWave > 0)
        {
            Write("<color=#FFCC00>Max Wave set to " + GameSettings.maxWave.ToString() + "</color>");
        }
        if (GameSettings.horseMode > 0)
        {
            Write("<color=#FFCC00>Horses are enabled.</color>");
        }
        if (GameSettings.ahssReload > 0)
        {
            Write("<color=#FFCC00>AHSS Air-Reload disabled.</color>");
        }
        if (GameSettings.punkWaves > 0)
        {
            Write("<color=#FFCC00>Punk override every 5 waves enabled.</color>");
        }
        if (GameSettings.endlessMode > 0)
        {
            Write("<color=#FFCC00>Endless Respawn is enabled (" + GameSettings.endlessMode.ToString() + " seconds).</color>");
        }
        if (GameSettings.globalDisableMinimap > 0)
        {
            Write("<color=#FFCC00>Minimaps are disabled.</color>");
        }
        if (GameSettings.motd != string.Empty)
        {
            Write("<color=#FFCC00>MOTD:" + GameSettings.motd + "</color>");
        }
        if (GameSettings.deadlyCannons > 0)
        {
            Write("<color=#FFCC00>Cannons will kill humans.</color>");
        }
    }
}