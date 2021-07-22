using ExitGames.Client.Photon;
using Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    private void Awake()
    {
        ChatInstanse = this;
    }


    public void addLINE(string newLine)
    {
        newLine = newLine.FilterSizeTag();
        messages.Add(newLine);
    }

    public static void Write(string newLine)
    {
        messages.Add(newLine);
    }

    

    public void OnGUI()
    {
        int num4;
        if (!this.IsVisible || (PhotonNetwork.connectionStateDetailed != PeerStates.Joined))
        {
            return;
        }
        if (Event.current.type == EventType.KeyDown)
        {
            if ((((Event.current.keyCode == KeyCode.Tab) || (Event.current.character == '\t')) && !IN_GAME_MAIN_CAMERA.isPausing) && (FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat] != KeyCode.Tab))
            {
                Event.current.Use();
                goto Label_219C;
            }
        }
        else if ((Event.current.type == EventType.KeyUp) && (((Event.current.keyCode != KeyCode.None) && (Event.current.keyCode == FengGameManagerMKII.inputRC.humanKeys[InputCodeRC.chat])) && (GUI.GetNameOfFocusedControl() != "ChatInput")))
        {
            this.inputLine = string.Empty;
            GUI.FocusControl("ChatInput");
            goto Label_219C;
        }
        if ((Event.current.type == EventType.KeyDown) && ((Event.current.keyCode == KeyCode.KeypadEnter) || (Event.current.keyCode == KeyCode.Return)))
        {
            if (!string.IsNullOrEmpty(this.inputLine))
            {
                string str2;
                if (this.inputLine == "\t")
                {
                    this.inputLine = string.Empty;
                    GUI.FocusControl(string.Empty);
                    return;
                }
                if (FengGameManagerMKII.RCEvents.ContainsKey("OnChatInput"))
                {
                    string key = (string) FengGameManagerMKII.RCVariableNames["OnChatInput"];
                    if (FengGameManagerMKII.stringVariables.ContainsKey(key))
                    {
                        FengGameManagerMKII.stringVariables[key] = this.inputLine;
                    }
                    else
                    {
                        FengGameManagerMKII.stringVariables.Add(key, this.inputLine);
                    }
                    ((RCEvent) FengGameManagerMKII.RCEvents["OnChatInput"]).checkEvent();
                }
                if (!this.inputLine.StartsWith("/"))
                {
                    str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]).hexColor();
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
                else if (this.inputLine == "/cloth")
                {
                    this.addLINE(ClothFactory.GetDebugInfo());
                }
                else if(inputLine.StartsWith("/slp"))
                {
                    var chArray2 = new[] { ' ' };
                    var player3 = PhotonPlayer.Find(Convert.ToInt32(inputLine.Split(chArray2)[1]));
                    foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        obj2.GetComponent<HERO>().GetComponent<Photon.MonoBehaviour>().photonView.RPC("blowAway", player3, new Vector3(1E+18F, 1E+16F, 1E+16F));
                    }
                }
                else if (this.inputLine.StartsWith("/aso"))
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        switch (this.inputLine.Substring(5))
                        {
                            case "kdr":
                                if (GameSettings.asoPreservekdr == 0)
                                {
                                    GameSettings.asoPreservekdr = 1;
                                    this.addLINE("<color=#FFCC00>KDRs will be preserved from disconnects.</color>");
                                }
                                else
                                {
                                    GameSettings.asoPreservekdr = 0;
                                    this.addLINE("<color=#FFCC00>KDRs will not be preserved from disconnects.</color>");
                                }
                                break;

                            case "racing":
                                if (GameSettings.racingStatic == 0)
                                {
                                    GameSettings.racingStatic = 1;
                                    this.addLINE("<color=#FFCC00>Racing will not end on finish.</color>");
                                }
                                else
                                {
                                    GameSettings.racingStatic = 0;
                                    this.addLINE("<color=#FFCC00>Racing will end on finish.</color>");
                                }
                                break;
                        }
                    }
                }
                else
                {
                    object[] objArray3;
                    if (this.inputLine == "/pause")
                    {
                        if (PhotonNetwork.isMasterClient)
                        {
                            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { true });
                            objArray3 = new object[] { "<color=#FFCC00>MasterClient has paused the game.</color>", "" };
                            FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                        }
                        else
                        {
                            this.addLINE("<color=#FFCC00>error: not master client</color>");
                        }
                    }
                    else if (this.inputLine == "/unpause")
                    {
                        if (PhotonNetwork.isMasterClient)
                        {
                            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, new object[] { false });
                            objArray3 = new object[] { "<color=#FFCC00>MasterClient has unpaused the game.</color>", "" };
                            FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                        }
                        else
                        {
                            this.addLINE("<color=#FFCC00>error: not master client</color>");
                        }
                    }
                    else if (this.inputLine == "/checklevel")
                    {
                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                        {
                            this.addLINE(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.currentLevel]));
                        }
                    }
                    else if (this.inputLine == "/isrc")
                    {
                        if (FengGameManagerMKII.masterRC)
                        {
                            this.addLINE("is RC");
                        }
                        else
                        {
                            this.addLINE("not RC");
                        }
                    }
                    else if (this.inputLine == "/ignorelist")
                    {
                        foreach (int num2 in FengGameManagerMKII.ignoreList)
                        {
                            this.addLINE(num2.ToString());
                        }
                    }
                    else if (this.inputLine.StartsWith("/room"))
                    {
                        if (PhotonNetwork.isMasterClient)
                        {
                            if (this.inputLine.Substring(6).StartsWith("max"))
                            {
                                int num3 = Convert.ToInt32(this.inputLine.Substring(10));
                                FengGameManagerMKII.instance.maxPlayers = num3;
                                PhotonNetwork.room.maxPlayers = num3;
                                objArray3 = new object[] { "<color=#FFCC00>Max players changed to " + this.inputLine.Substring(10) + "!</color>", "" };
                                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                            }
                            else if (this.inputLine.Substring(6).StartsWith("time"))
                            {
                                FengGameManagerMKII.instance.addTime(Convert.ToSingle(this.inputLine.Substring(11)));
                                objArray3 = new object[] { "<color=#FFCC00>" + this.inputLine.Substring(11) + " seconds added to the clock.</color>", "" };
                                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                            }
                        }
                        else
                        {
                            this.addLINE("<color=#FFCC00>error: not master client</color>");
                        }
                    }
                    else if (this.inputLine.StartsWith("/resetkd"))
                    {
                        Hashtable hashtable;
                        if (this.inputLine == "/resetkdall")
                        {
                            if (PhotonNetwork.isMasterClient)
                            {
                                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                                {
                                    hashtable = new Hashtable();
                                    hashtable.Add(PhotonPlayerProperty.kills, 0);
                                    hashtable.Add(PhotonPlayerProperty.deaths, 0);
                                    hashtable.Add(PhotonPlayerProperty.max_dmg, 0);
                                    hashtable.Add(PhotonPlayerProperty.total_dmg, 0);
                                    player.SetCustomProperties(hashtable);
                                }
                                objArray3 = new object[] { "<color=#FFCC00>All stats have been reset.</color>", "" };
                                FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                            }
                            else
                            {
                                this.addLINE("<color=#FFCC00>error: not master client</color>");
                            }
                        }
                        else
                        {
                            hashtable = new Hashtable();
                            hashtable.Add(PhotonPlayerProperty.kills, 0);
                            hashtable.Add(PhotonPlayerProperty.deaths, 0);
                            hashtable.Add(PhotonPlayerProperty.max_dmg, 0);
                            hashtable.Add(PhotonPlayerProperty.total_dmg, 0);
                            PhotonNetwork.player.SetCustomProperties(hashtable);
                            this.addLINE("<color=#FFCC00>Your stats have been reset. </color>");
                        }
                    }
                    else if (this.inputLine.StartsWith("/pm"))
                    {
                        string[] strArray = this.inputLine.Split(new char[] { ' ' });
                        PhotonPlayer targetPlayer = PhotonPlayer.Find(Convert.ToInt32(strArray[1]));
                        str2 = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]).hexColor();
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
                        string str4 = string.Empty;
                        for (num4 = 2; num4 < strArray.Length; num4++)
                        {
                            str4 = str4 + strArray[num4] + " ";
                        }
                        FengGameManagerMKII.instance.photonView.RPC("ChatPM", targetPlayer, new object[] { /*name*/str2, str4 });
                        this.addLINE("<color=#FFC000>TO [" + targetPlayer.ID.ToString() + "]</color> " + str3 + ":" + str4);
                    }
                    else if (this.inputLine.StartsWith("/team"))
                    {
                        if (GameSettings.teamMode == 1)
                        {
                            if ((this.inputLine.Substring(6) == "1") || (this.inputLine.Substring(6) == "cyan"))
                            {
                                FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 1 });
                                this.addLINE("<color=#00FFFF>You have joined team cyan.</color>");
                                foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("Player"))
                                {
                                    if (obj2.GetPhotonView().isMine)
                                    {
                                        obj2.GetComponent<HERO>().markDie();
                                        obj2.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                                    }
                                }
                            }
                            else if ((this.inputLine.Substring(6) == "2") || (this.inputLine.Substring(6) == "magenta"))
                            {
                                FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 2 });
                                this.addLINE("<color=#FF00FF>You have joined team magenta.</color>");
                                foreach (GameObject obj3 in GameObject.FindGameObjectsWithTag("Player"))
                                {
                                    if (obj3.GetPhotonView().isMine)
                                    {
                                        obj3.GetComponent<HERO>().markDie();
                                        obj3.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                                    }
                                }
                            }
                            else if ((this.inputLine.Substring(6) == "0") || (this.inputLine.Substring(6) == "individual"))
                            {
                                FengGameManagerMKII.instance.photonView.RPC("setTeamRPC", PhotonNetwork.player, new object[] { 0 });
                                this.addLINE("<color=#00FF00>You have joined individuals.</color>");
                                foreach (GameObject obj4 in GameObject.FindGameObjectsWithTag("Player"))
                                {
                                    if (obj4.GetPhotonView().isMine)
                                    {
                                        obj4.GetComponent<HERO>().markDie();
                                        obj4.GetComponent<HERO>().photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Team Switch" });
                                    }
                                }
                            }
                            else
                            {
                                this.addLINE("<color=#FFCC00>error: invalid team code. Accepted values are 0,1, and 2.</color>");
                            }
                        }
                        else
                        {
                            this.addLINE("<color=#FFCC00>error: teams are locked or disabled. </color>");
                        }
                    }
                    else if (this.inputLine == "/restart")
                    {
                        if (PhotonNetwork.isMasterClient)
                        {
                            objArray3 = new object[] { "<color=#FFCC00>MasterClient has restarted the game!</color>", "" };
                            FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray3);
                            FengGameManagerMKII.instance.restartRC();
                        }
                        else
                        {
                            this.addLINE("<color=#FFCC00>error: not master client</color>");
                        }
                    }
                    else if (this.inputLine.StartsWith("/specmode"))
                    {
                        if (((int) FengGameManagerMKII.settings[0xf5]) == 0)
                        {
                            FengGameManagerMKII.settings[0xf5] = 1;
                            FengGameManagerMKII.instance.EnterSpecMode(true);
                            this.addLINE("<color=#FFCC00>You have entered spectator mode.</color>");
                        }
                        else
                        {
                            FengGameManagerMKII.settings[0xf5] = 0;
                            FengGameManagerMKII.instance.EnterSpecMode(false);
                            this.addLINE("<color=#FFCC00>You have exited spectator mode.</color>");
                        }
                    }
                    else if (this.inputLine.StartsWith("/fov"))
                    {
                        int num6 = Convert.ToInt32(this.inputLine.Substring(5));
                        Camera.main.fieldOfView = num6;
                        this.addLINE("<color=#FFCC00>Field of vision set to " + num6.ToString() + ".</color>");
                    }
                    else if (this.inputLine == "/colliders")
                    {
                        int num7 = 0;
                        foreach (TITAN titan in FengGameManagerMKII.titans)
                        {
                            if (titan.myTitanTrigger.isCollide)
                            {
                                num7++;
                            }
                        }
                        InRoomChat.ChatInstanse.addLINE(num7.ToString());
                    }
                    else
                    {
                        int num8;
                        if (this.inputLine.StartsWith("/spectate"))
                        {
                            num8 = Convert.ToInt32(this.inputLine.Substring(10));
                            foreach (GameObject obj5 in GameObject.FindGameObjectsWithTag("Player"))
                            {
                                if (obj5.GetPhotonView().owner.ID == num8)
                                {
                                    IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(obj5, true, false);
                                    IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(false);
                                }
                            }
                        }
                        else if (!this.inputLine.StartsWith("/kill"))
                        {
                            object[] objArray5;
                            if (this.inputLine.StartsWith("/revive"))
                            {
                                if (PhotonNetwork.isMasterClient)
                                {
                                    if (this.inputLine == "/reviveall")
                                    {
                                        objArray5 = new object[] { "<color=#FFCC00>All players have been revived.</color>", string.Empty };
                                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray5);
                                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                                        {
                                            if (((player.customProperties[PhotonPlayerProperty.dead] != null) && RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead])) && (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2))
                                            {
                                                FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        num8 = Convert.ToInt32(this.inputLine.Substring(8));
                                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                                        {
                                            if (player.ID == num8)
                                            {
                                                this.addLINE("<color=#FFCC00>Player " + num8.ToString() + " has been revived.</color>");
                                                if (((player.customProperties[PhotonPlayerProperty.dead] != null) && RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead])) && (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2))
                                                {
                                                    objArray5 = new object[] { "<color=#FFCC00>You have been revived by the master client.</color>", string.Empty };
                                                    FengGameManagerMKII.instance.photonView.RPC("Chat", player, objArray5);
                                                    FengGameManagerMKII.instance.photonView.RPC("respawnHeroInNewRound", player, new object[0]);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.addLINE("<color=#FFCC00>error: not master client</color>");
                                }
                            }
                            else if (this.inputLine.StartsWith("/ignore"))
                            {
                                string[] args = inputLine.Split(new char[] { ' ' });
                                PhotonPlayer player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                                FengGameManagerMKII.ignoreList.Add(player.ID);
                                FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
                            }
                            else if (this.inputLine.StartsWith("/unignore"))
                            {
                                string[] args = inputLine.Split(new char[] { ' ' });
                                PhotonPlayer player = PhotonPlayer.Find(Convert.ToInt32(args[1]));
                                FengGameManagerMKII.ignoreList.Remove(player.ID);
                                FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
                                if (NetworkingPeer.instantiateCounter.ContainsKey(player))
                                    NetworkingPeer.instantiateCounter[player].Clear();
                            }
                            else if (this.inputLine.StartsWith("/unban"))
                            {
                                if (FengGameManagerMKII.OnPrivateServer)
                                {
                                    FengGameManagerMKII.ServerRequestUnban(this.inputLine.Substring(7));
                                }
                                else if (PhotonNetwork.isMasterClient)
                                {
                                    int num9 = Convert.ToInt32(this.inputLine.Substring(7));
                                    if (FengGameManagerMKII.banHash.ContainsKey(num9))
                                    {
                                        objArray5 = new object[] { "<color=#FFCC00>" + ((string) FengGameManagerMKII.banHash[num9]) + " has been unbanned from the server. </color>", string.Empty };
                                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray5);
                                        FengGameManagerMKII.banHash.Remove(num9);
                                    }
                                    else
                                    {
                                        this.addLINE("error: no such player");
                                    }
                                }
                                else
                                {
                                    this.addLINE("<color=#FFCC00>error: not master client</color>");
                                }
                            }
                            else if (this.inputLine.StartsWith("/rules"))
                            {
                                this.addLINE("<color=#FFCC00>Currently activated gamemodes:</color>");
                                if (GameSettings.bombMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Bomb mode is on.</color>");
                                }
                                if (GameSettings.teamMode > 0)
                                {
                                    if (GameSettings.teamMode == 1)
                                    {
                                        this.addLINE("<color=#FFCC00>Team mode is on (no sort).</color>");
                                    }
                                    else if (GameSettings.teamMode == 2)
                                    {
                                        this.addLINE("<color=#FFCC00>Team mode is on (sort by size).</color>");
                                    }
                                    else if (GameSettings.teamMode == 3)
                                    {
                                        this.addLINE("<color=#FFCC00>Team mode is on (sort by skill).</color>");
                                    }
                                }
                                if (GameSettings.pointMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Point mode is on (" + Convert.ToString(GameSettings.pointMode) + ").</color>");
                                }
                                if (GameSettings.disableRock > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Punk Rock-Throwing is disabled.</color>");
                                }
                                if (GameSettings.spawnMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Custom spawn rate is on (" + GameSettings.nRate.ToString("F2") + "% Normal, " + GameSettings.aRate.ToString("F2") + "% Abnormal, " + GameSettings.jRate.ToString("F2") + "% Jumper, " + GameSettings.cRate.ToString("F2") + "% Crawler, " + GameSettings.pRate.ToString("F2") + "% Punk </color>");
                                }
                                if (GameSettings.explodeMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Titan explode mode is on (" + Convert.ToString(GameSettings.explodeMode) + ").</color>");
                                }
                                if (GameSettings.healthMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Titan health mode is on (" + Convert.ToString(GameSettings.healthLower) + "-" + Convert.ToString(GameSettings.healthUpper) + ").</color>");
                                }
                                if (GameSettings.infectionMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Infection mode is on (" + Convert.ToString(GameSettings.infectionMode) + ").</color>");
                                }
                                if (GameSettings.damageMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Minimum nape damage is on (" + Convert.ToString(GameSettings.damageMode) + ").</color>");
                                }
                                if (GameSettings.moreTitans > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Custom titan is on (" + Convert.ToString(GameSettings.moreTitans) + ").</color>");
                                }
                                if (GameSettings.sizeMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Custom titan size is on (" + GameSettings.sizeLower.ToString("F2") + "," + GameSettings.sizeUpper.ToString("F2") + ").</color>");
                                }
                                if (GameSettings.banEren > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Anti-Eren is on. Using Titan eren will get you kicked.</color>");
                                }
                                if (GameSettings.waveModeOn == 1)
                                {
                                    this.addLINE("<color=#FFCC00>Custom wave mode is on (" + Convert.ToString(GameSettings.waveModeNum) + ").</color>");
                                }
                                if (GameSettings.friendlyMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Friendly-Fire disabled. PVP is prohibited.</color>");
                                }
                                if (GameSettings.pvpMode > 0)
                                {
                                    if (GameSettings.pvpMode == 1)
                                    {
                                        this.addLINE("<color=#FFCC00>AHSS/Blade PVP is on (team-based).</color>");
                                    }
                                    else if (GameSettings.pvpMode == 2)
                                    {
                                        this.addLINE("<color=#FFCC00>AHSS/Blade PVP is on (FFA).</color>");
                                    }
                                }
                                if (GameSettings.maxWave > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Max Wave set to " + GameSettings.maxWave.ToString() + "</color>");
                                }
                                if (GameSettings.horseMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Horses are enabled.</color>");
                                }
                                if (GameSettings.ahssReload > 0)
                                {
                                    this.addLINE("<color=#FFCC00>AHSS Air-Reload disabled.</color>");
                                }
                                if (GameSettings.punkWaves > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Punk override every 5 waves enabled.</color>");
                                }
                                if (GameSettings.endlessMode > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Endless Respawn is enabled (" + GameSettings.endlessMode.ToString() + " seconds).</color>");
                                }
                                if (GameSettings.globalDisableMinimap > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Minimaps are disabled.</color>");
                                }
                                if (GameSettings.motd != string.Empty)
                                {
                                    this.addLINE("<color=#FFCC00>MOTD:" + GameSettings.motd + "</color>");
                                }
                                if (GameSettings.deadlyCannons > 0)
                                {
                                    this.addLINE("<color=#FFCC00>Cannons will kill humans.</color>");
                                }
                            }
                            else
                            {
                                object[] objArray6;
                                bool flag2;
                                object[] objArray7;
                                if (this.inputLine.StartsWith("/kick"))
                                {
                                    num8 = Convert.ToInt32(this.inputLine.Substring(6));
                                    if (num8 == PhotonNetwork.player.ID)
                                    {
                                        this.addLINE("error:can't kick yourself.");
                                    }
                                    else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                                    {
                                        objArray6 = new object[] { "/kick #" + Convert.ToString(num8), LoginFengKAI.player.name };
                                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray6);
                                    }
                                    else
                                    {
                                        flag2 = false;
                                        foreach (PhotonPlayer player3 in PhotonNetwork.playerList)
                                        {
                                            if (num8 == player3.ID)
                                            {
                                                flag2 = true;
                                                if (FengGameManagerMKII.OnPrivateServer)
                                                {
                                                    FengGameManagerMKII.instance.kickPlayerRC(player3, false, "");
                                                }
                                                else if (PhotonNetwork.isMasterClient)
                                                {
                                                    FengGameManagerMKII.instance.kickPlayerRC(player3, false, "");
                                                    objArray7 = new object[] { "<color=#FFCC00>" + RCextensions.returnStringFromObject(player3.customProperties[PhotonPlayerProperty.name]) + " has been kicked from the server!</color>", string.Empty };
                                                    FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray7);
                                                }
                                            }
                                        }
                                        if (!flag2)
                                        {
                                            this.addLINE("error:no such player.");
                                        }
                                    }
                                }
                                else if (this.inputLine.StartsWith("/ban"))
                                {
                                    if (this.inputLine == "/banlist")
                                    {
                                        this.addLINE("<color=#FFCC00>List of banned players:</color>");
                                        foreach (int num10 in FengGameManagerMKII.banHash.Keys)
                                        {
                                            this.addLINE("<color=#FFCC00>" + Convert.ToString(num10) + ":" + ((string) FengGameManagerMKII.banHash[num10]) + "</color>");
                                        }
                                    }
                                    else
                                    {
                                        num8 = Convert.ToInt32(this.inputLine.Substring(5));
                                        if (num8 == PhotonNetwork.player.ID)
                                        {
                                            this.addLINE("error:can't kick yourself.");
                                        }
                                        else if (!(FengGameManagerMKII.OnPrivateServer || PhotonNetwork.isMasterClient))
                                        {
                                            objArray6 = new object[] { "/kick #" + Convert.ToString(num8), LoginFengKAI.player.name };
                                            FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray6);
                                        }
                                        else
                                        {
                                            flag2 = false;
                                            foreach (PhotonPlayer player3 in PhotonNetwork.playerList)
                                            {
                                                if (num8 == player3.ID)
                                                {
                                                    flag2 = true;
                                                    if (FengGameManagerMKII.OnPrivateServer)
                                                    {
                                                        FengGameManagerMKII.instance.kickPlayerRC(player3, true, "");
                                                    }
                                                    else if (PhotonNetwork.isMasterClient)
                                                    {
                                                        FengGameManagerMKII.instance.kickPlayerRC(player3, true, "");
                                                        objArray7 = new object[] { "<color=#FFCC00>" + RCextensions.returnStringFromObject(player3.customProperties[PhotonPlayerProperty.name]) + " has been banned from the server!</color>", string.Empty };
                                                        FengGameManagerMKII.instance.photonView.RPC("Chat", PhotonTargets.All, objArray7);
                                                    }
                                                }
                                            }
                                            if (!flag2)
                                            {
                                                this.addLINE("error:no such player.");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                this.inputLine = string.Empty;
                GUI.FocusControl(string.Empty);
                return;
            }
            this.inputLine = "\t";
            GUI.FocusControl("ChatInput");
        }
    Label_219C:
        GUI.SetNextControlName(string.Empty);
        GUILayout.BeginArea(GuiRect);
        GUILayout.FlexibleSpace();
        string text = string.Empty;
        if (messages.Count < 15)
        {
            for (num4 = 0; num4 < messages.Count; num4++)
            {
                text = text + messages[num4] + "\n";
            }
        }
        else
        {
            for (int i = messages.Count - 15; i < messages.Count; i++)
            {
                text = text + messages[i] + "\n";
            }
        }
        GUILayout.Label(text, new GUILayoutOption[0]);
        GUILayout.EndArea();
        GUILayout.BeginArea(GuiRect2);
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUI.SetNextControlName("ChatInput");
        this.inputLine = GUILayout.TextField(this.inputLine, new GUILayoutOption[0]);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public void setPosition()
    {
        if (this.AlignBottom)
        {
            GuiRect = new Rect(0f, (float) (Screen.height - 500), 300f, 470f);
            GuiRect2 = new Rect(30f, (float) ((Screen.height - 300) + 0x113), 300f, 25f);
        }
    }

    public void Start()
    {
        if (ChatInstanse == null) ChatInstanse = this;
        this.setPosition();
    }
}

