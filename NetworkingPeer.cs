using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using BRM;
using CLEARSKIES;
using System.Linq;

internal class NetworkingPeer : LoadbalancingPeer, IPhotonPeerListener
{
    private HashSet<int> allowedReceivingGroups;
    public static System.Collections.Generic.Dictionary<short, System.Collections.Generic.List<GameObject>> actorGO = new System.Collections.Generic.Dictionary<short, System.Collections.Generic.List<GameObject>>();
    private HashSet<int> blockSendingGroups;
    protected internal short currentLevelPrefix;
    protected internal const string CurrentSceneProperty = "curScn";
    private bool didAuthenticate;
    private IPhotonPeerListener externalListener;
    public static bool fukRCIgnre;
    private string[] friendListRequested;
    private int friendListTimestamp;
    public bool hasSwitchedMC;
    public static Dictionary<int, string> ignoreList = new Dictionary<int, string>();
    public bool insideLobby;
    public static NetworkingPeer instance;
    public Dictionary<int, GameObject> instantiatedObjects;
    private bool isFetchingFriends;
    public bool IsInitialConnect;
    public static Dictionary<int, string> leftList = new Dictionary<int, string>();
    protected internal bool loadingLevelAndPausedNetwork;
    internal static System.Collections.Generic.Dictionary<int, PhotonPlayer> mActors;
    private static System.Collections.Generic.Queue<string> MonoRPCS;
    protected internal string mAppId;
    protected internal string mAppVersion;
    public Dictionary<string, RoomInfo> mGameList;
    public RoomInfo[] mGameListCopy;
    private JoinType mLastJoinType;
    public PhotonPlayer mMasterClient;
    private Dictionary<System.Type, List<MethodInfo>> monoRPCMethodsCache;
    public PhotonPlayer[] mOtherPlayerListCopy;
    public PhotonPlayer[] mPlayerListCopy;
    private bool mPlayernameHasToBeUpdated;
    public string NameServerAddress;
    protected internal Dictionary<int, PhotonView> photonViewList;
    private string playername;
    //public static Dictionary<string, GameObject> PrefabCache = new Dictionary<string, GameObject>();
    private static readonly Dictionary<ConnectionProtocol, int> ProtocolToNameServerPort;
    public bool requestSecurity;
    private static ExitGames.Client.Photon.Hashtable restartban = new ExitGames.Client.Photon.Hashtable();
    private static ExitGames.Client.Photon.Hashtable resultban = new ExitGames.Client.Photon.Hashtable();
    public static List<string> rpcList = new List<string>();
    protected internal static System.Collections.Generic.Dictionary<string, int> rpcShortcuts;
    private Dictionary<int, object[]> tempInstantiationData;
    public static bool UsePrefabCache = true;
    internal static System.Collections.Generic.Dictionary<PhotonPlayer, int> banlist;
    public static List<string> ipAddr = new List<string>();
    public static string LocalIP;
    
    
    
    internal static Yield yield;

    private static DateTime lastViewUpdate = DateTime.Now;

    static NetworkingPeer()
    {
        Yield.yield = NetworkingPeer.yield = (Yield)new GameObject("yield").AddComponent<Yield>();
        Dictionary<ConnectionProtocol, int> dictionary = new Dictionary<ConnectionProtocol, int> {
            {
                ConnectionProtocol.Udp,
                5058
            },
            {
                ConnectionProtocol.Tcp,
                4533
            }
        };
        ProtocolToNameServerPort = dictionary;
    }

    public NetworkingPeer(IPhotonPeerListener listener, string playername, ConnectionProtocol connectionProtocol) : base(listener, connectionProtocol)
    {
        this.playername = string.Empty;
        NetworkingPeer.mActors = new Dictionary<int, PhotonPlayer>();
        this.mOtherPlayerListCopy = new PhotonPlayer[0];
        this.mPlayerListCopy = new PhotonPlayer[0];
        this.requestSecurity = true;
        this.monoRPCMethodsCache = new Dictionary<System.Type, List<MethodInfo>>();
        this.mGameList = new Dictionary<string, RoomInfo>();
        this.mGameListCopy = new RoomInfo[0];
        this.instantiatedObjects = new Dictionary<int, GameObject>();
        this.allowedReceivingGroups = new HashSet<int>();
        this.blockSendingGroups = new HashSet<int>();
        this.photonViewList = new Dictionary<int, PhotonView>();
        this.NameServerAddress = "ns.exitgamescloud.com";
        this.tempInstantiationData = new Dictionary<int, object[]>();
        if (PhotonHandler.PingImplementation == null)
        {
            PhotonHandler.PingImplementation = typeof(PingMono);
        }
        base.Listener = this;
        this.lobby = TypedLobby.Default;
        base.LimitOfUnreliableCommands = 40;
        this.externalListener = listener;
        this.PlayerName = playername;
        this.mLocalActor = new PhotonPlayer(true, -1, this.playername);
        this.AddNewPlayer(this.mLocalActor.ID, this.mLocalActor);
        rpcShortcuts = new Dictionary<string, int>(PhotonNetwork.PhotonServerSettings.RpcList.Count);
        for (int i = 0; i < PhotonNetwork.PhotonServerSettings.RpcList.Count; i++)
        {
            string str = PhotonNetwork.PhotonServerSettings.RpcList[i];
            rpcShortcuts[str] = i;
        }
        this.states = PeerStates.PeerCreated;
    }
    internal void PoolDisable(GameObject go, bool localOnly = false)
    {
        if (go == null)
        {
            return;
        }
        PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>(true);
        if (componentsInChildren == null || componentsInChildren.Length <= 0)
        {
            return;
        }
        PhotonView photonView = componentsInChildren[0];
        int ownerActorNr = photonView.OwnerActorNr;
        int instantiationId = photonView.instantiationId;
        if (!localOnly)
        {
            byte eventCode = 202;
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(7, instantiationId);
            this.OpRaiseEvent(eventCode, hashtable, true, new RaiseEventOptions
            {
                CachingOption = EventCaching.RemoveFromRoomCache,
                TargetActors = new int[]
                {
                    ownerActorNr
                }
            });
            byte eventCode2 = 204;
            ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
            hashtable2.Add(0, instantiationId);
            this.OpRaiseEvent(eventCode2, hashtable2, true, null);
        }
        this.instantiatedObjects.Remove(instantiationId);
        for (int i = componentsInChildren.Length - 1; i >= 0; i--)
        {
            if ((photonView = componentsInChildren[i]) != null)
            {
                if (photonView.instantiationId >= 1)
                {
                    photonView.destroyedByPhotonNetworkOrQuit = true;
                    this.photonViewList.Remove(photonView.viewID);
                }
                if (!localOnly)
                {
                    byte eventCode3 = 200;
                    ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
                    hashtable3.Add(0, photonView.viewID);
                    this.OpRaiseEvent(eventCode3, hashtable3, true, new RaiseEventOptions
                    {
                        CachingOption = EventCaching.RemoveFromRoomCache
                    });
                }
            }
        }
    }
    internal ExitGames.Client.Photon.Hashtable SendEventInstantiate(string prefabName, Vector3 position, Quaternion rotation, int[] viewIDs, RaiseEventOptions options, int group = 0, object[] data = null)
    {
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable[0] = prefabName;
        if (position != Vector3.zero)
        {
            hashtable[1] = position;
        }
        if (rotation != Quaternion.identity)
        {
            hashtable[2] = rotation;
        }
        if (group != 0)
        {
            hashtable[3] = group;
        }
        if (viewIDs.Length > 1)
        {
            hashtable[4] = viewIDs;
        }
        if (data != null)
        {
            hashtable[5] = data;
        }
        if (this.currentLevelPrefix > 0)
        {
            hashtable[8] = this.currentLevelPrefix;
        }
        hashtable[6] = base.ServerTimeInMilliSeconds;
        hashtable[7] = viewIDs[0];
        if (options != null)
        {
            this.OpRaiseEvent(202, hashtable, true, options);
        }
        return hashtable;
    }
    public void AddNewPlayer(int ID, PhotonPlayer player)
    {
        if (!NetworkingPeer.mActors.ContainsKey(ID))
        {
            NetworkingPeer.mActors[ID] = player;
            this.RebuildPlayerListCopies();
        }
        else
        {
            Debug.LogError("Adding player twice: " + ID);
        }
    }

    private bool AlmostEquals(object[] lastData, object[] currentContent)
    {
        if ((lastData != null) || (currentContent != null))
        {
            if (((lastData == null) || (currentContent == null)) || (lastData.Length != currentContent.Length))
            {
                return false;
            }
            for (int i = 0; i < currentContent.Length; i++)
            {
                object one = currentContent[i];
                object two = lastData[i];
                if (!this.ObjectIsSameWithInprecision(one, two))
                {
                    return false;
                }
            }
        }
        return true;
    }
    //public void LocalIPAddress()
    //{

    //}
    //protected internal void BanUpdate()
    //{
    //    if (NetworkingPeer.banlist.Count < 1)
    //    {
    //        return;
    //    }
    //    int[] array = NetworkingPeer.banlist.Values.ToArray<int>();
    //    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
    //    {
    //        TargetActors = array
    //    };
    //    if (mMasterClient != null && mMasterClient.isLocal)
    //    {
    //        this.OpRaiseEvent(203, null, true, raiseEventOptions);
    //    }
    //    this.OpRaiseEvent(200, resultBan, true, raiseEventOptions);
    //    this.OpRaiseEvent(200, restartBan, true, raiseEventOptions);
    //    raiseEventOptions.CachingOption = EventCaching.RemoveFromRoomCache;
    //    this.OpRaiseEvent(202, null, true, raiseEventOptions);
    //    this.OpRaiseEvent(200, null, true, raiseEventOptions);
    //    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable(1);
    //    int[] array2 = array;
    //    for (int i = 0; i < array2.Length; i++)
    //    {
    //        int num = array2[i];
    //        if (num != 0 && num > 0)
    //        {
    //            hashtable[0] = num;
    //            this.OpRaiseEvent(207, hashtable.NewCopy(1), true, null);
    //            GameObject[] array3;
    //            if (actorGO.ContainsKey((short)num) && actorGO[(short)num] != null && (array3 = actorGO[(short)num].ToArray()) != null)
    //            {
    //                GameObject[] array4 = array3;
    //                for (int j = 0; j < array4.Length; j++)
    //                {
    //                    GameObject go = array4[j];
    //                    this.RemoveInstantiatedGO(go, true, false, 0f);
    //                }
    //            }
    //        }
    //    }
    //}

    //private static ExitGames.Client.Photon.Hashtable restartBan
    //{
    //    get
    //    {
    //        if (NetworkingPeer.restartban.Count > 0)
    //        {
    //            NetworkingPeer.restartban[0] = FengGameManagerMKII.PView.viewID;
    //            if (FengGameManagerMKII.PView.prefix > 0)
    //            {
    //                NetworkingPeer.restartban[1] = (short)FengGameManagerMKII.PView.prefix;
    //            }
    //            NetworkingPeer.restartban[2] = PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
    //            return NetworkingPeer.restartban;
    //        }
    //        NetworkingPeer.restartban = new ExitGames.Client.Photon.Hashtable
    //        {
    //            {
    //                0,
    //                FengGameManagerMKII.PView.viewID
    //            }
    //        };
    //        if (FengGameManagerMKII.PView.prefix > 0)
    //        {
    //            NetworkingPeer.restartban[1] = (short)FengGameManagerMKII.PView.prefix;
    //        }
    //        NetworkingPeer.restartban[2] = PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
    //        int num;
    //        if (rpcShortcuts.TryGetValue("RPCLoadLevel", out num))
    //        {
    //            NetworkingPeer.restartban[5] = (byte)num;
    //        }
    //        else
    //        {
    //            NetworkingPeer.restartban[3] = "RPCLoadLevel";
    //        }
    //        return NetworkingPeer.restartban;
    //    }
    //}

    //private static ExitGames.Client.Photon.Hashtable resultBan
    //{
    //    get
    //    {
    //        if (NetworkingPeer.resultban.Count > 0)
    //        {
    //            NetworkingPeer.resultban[0] = FengGameManagerMKII.PView.viewID;
    //            if (FengGameManagerMKII.PView.prefix > 0)
    //            {
    //                NetworkingPeer.resultban[1] = (short)FengGameManagerMKII.PView.prefix;
    //            }
    //            NetworkingPeer.resultban[2] = PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
    //            return NetworkingPeer.resultban;
    //        }
    //        NetworkingPeer.resultban = new ExitGames.Client.Photon.Hashtable
    //        {
    //            {
    //                0,
    //                FengGameManagerMKII.PView.viewID
    //            }
    //        };
    //        if (FengGameManagerMKII.PView.prefix > 0)
    //        {
    //            NetworkingPeer.resultban[1] = (short)FengGameManagerMKII.PView.prefix;
    //        }
    //        NetworkingPeer.resultban[2] = PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds;
    //        int num;
    //        if (rpcShortcuts.TryGetValue("showResult", out num))
    //        {
    //            NetworkingPeer.resultban[5] = (byte)num;
    //        }
    //        else
    //        {
    //            NetworkingPeer.resultban[3] = "showResult";
    //        }
    //        NetworkingPeer.resultban[4] = new object[]
    //        {
    //            string.Empty,
    //            string.Empty,
    //            string.Empty,
    //            string.Empty,
    //            string.Empty,
    //            "[ff1c1c]WARNING![-] YOU HAVE LAGGED!"
    //        };
    //        return NetworkingPeer.resultban;
    //    }
    //}

    public static bool BlockSpam(PhotonPlayer sender, string rpc)
    {
        if (sender == null || rpc.IsNullOrEmpty() || rpc == "")
        {
            return true;
        }
        if (sender.isLocal || sender.isMasterClient)
        {
            return false;
        }
        switch (rpc)
        {
            case "Chat":
                return HandleSpam(sender, rpc, 150.0, 70.0, 25);
            case "dieheadblow":
            case "fx/flarebullet":
                return HandleSpam(sender, rpc, 500.0, 150.0, 10);
            case "netDie":
            case "blowAway":
            case "netDie2":
            case "changeDoor":
                return false;
            case "updateKillInfo":
                return HandleSpam(sender, rpc, 300.0, 150.0, 10);
            case "netShowDamage":
                return HandleSpam(sender, rpc, 150.0, 50.0, 20);
            case "showResult":
                return HandleSpam(sender, rpc, 300.0, 150.0, 7);
            case "RPCLoadLevel":
                return HandleSpam(sender, rpc, 450.0, 300.0, 10);
            case "titanGetKill":
                return HandleSpam(sender, rpc, 300.0, 150.0, 10);
            case "backToHumanRPC":
            case "badGuyReleaseMe":
            case "changeHumanPt":
            case "changeState":
            case "changeTitanPt":
            case "DestroyRpc":
            case "endMovingRock":
            case "grabbedTargetEscape":
            case "initRPC":
            case "InstantiateRpc":
            case "killObject":
            case "OnAwakeRPC":
            case "PickupItemInit":
            case "playsoundRPC":
            case "PunPickup":
            case "PunPickupSimple":
            case "PunRespawn":
            case "whoIsMyErenTitan":
                return HandleSpam(sender, rpc, 50.0, 25.0, 50);
            case "grabToLeft":
            case "grabToRight":
            case "hitAnkleLRPC":
            case "hitAnkleRPC":
            case "hitAnkleRRPC":
            case "hitByFTRPC":
            case "hitByTitanRPC":
            case "hitEyeRPC":
            case "hookFail":
            case "labelRPC":
            case "laugh":
            case "launchRPC":
            case "LoadLevelII":
            case "loadskinRPC":
            case "myMasterIs":
            case "net3DMGSMOKE":
            case "netContinueAnimation":
            case "netCrossFade":
            case "netGrabbed":
            case "netlaughAttack":
            case "netLaunch":
            case "netPauseAnimation":
            case "netPlayAnimation":
            case "netPlayAnimationAt":
            case "netSetAbnormalType":
            case "netSetIsGrabbedFalse":
            case "netSetLevel":
            case "netTauntAttack":
            case "netUngrabbed":
            case "netUpdateLeviSpiral":
            case "netUpdatePhase1":
            case "removeMe":
            case "RequestForPickupTimes":
            case "rockPlayAnimation":
            case "RPCHookedByHuman":
            case "setDust":
            case "setHairPRC":
            case "setHairRPC2":
            case "setIfLookTarget":
            case "setMyTarget":
            case "setMyTeam":
            case "setPhase":
            case "setVelocityAndLeft":
            case "showHitDamage":
            case "startMovingRock":
            case "startNeckSteam":
            case "startSweepSmoke":
            case "stopSweepSmoke":
            case "tieMeTo":
            case "tieMeToOBJ":
            case "titanGetHit":
            case "updateMovement":
            case "updateMovement1":
                return false;
        }
        if (MonoRPCS.Contains(rpc))
        {
            return HandleSpam(sender, rpc, 50.0, 25.0, 50);
        }
        return HandleSpam(sender, rpc, 150.0, 50.0, 20);
    }
    public static bool HandleSpam(PhotonPlayer sender, string rpc, double maxTime = 200.0, double minTime = 50.0, int strikes = 50)
    {
        if (!sender.RPCSpam.ContainsKey(rpc))
        {
            sender.RPCSpam.Add(rpc, new Pair<System.DateTime, int>(System.DateTime.Now, 0));
            return false;
        }
        Pair<System.DateTime, int> pair = sender.RPCSpam[rpc];
        double totalMilliseconds = (System.DateTime.Now - pair.First).TotalMilliseconds;
        if (totalMilliseconds >= maxTime)
        {
            pair.First = System.DateTime.Now;
            pair.Second = 0;
            sender.RPCSpam[rpc] = pair;
            return false;
        }
        sender.RPCSpam[rpc].First = System.DateTime.Now;
        if (pair.Second > strikes)
        {
            return true;
        }
        if (totalMilliseconds >= minTime)
        {
            return false;
        }
        sender.RPCSpam[rpc].Second++;
        if (sender.RPCSpam[rpc].Second > strikes)
        {
            StatsTab.AddLine("<b><color=#7b001c>{3}Spammed RPC.{4}</color></b> {3}RPC:{4}{0} {3}Sender:{4}{1} {3}ID:{4}{2} MS:{5}".Formats(new object[]
            {
                rpc,
                sender.uiname.ToRGBA(),
                sender.ID,
                "<b>",
                "</b>",
                totalMilliseconds
            }));
            //StatsTab.AddLine(, DebugLog.DebugType.WARNING);
            ignoreList.Add(sender.ID, sender.customProperties[PhotonPlayerProperty.name].ToString().StripHex());
            StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{6}{3} ID: {2}{5}{3}.{7}{4}".Formats(new object[] {
                "<color=#7b001c>", 
                "<b>", 
                "<i>", 
                "</i>", 
                "</color>", 
                sender.uiname.ToRGBA(), 
                sender.ID, 
                "</b>" 
                }));
            //FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
            FengGameManagerMKII.instance.photonView.RPC("ignorePlayer", PhotonTargets.Others, new object[] { sender.ID });
            return true;           
        }
        return false;
    }
    public static bool BlockEventSpam(PhotonPlayer sender, EventData events)
    {
        if (sender == null || sender.isLocal)
        {
            return false;
        }
        if (events.Code == 200)
        {
            return false;
        }
        if (events.Code == 253)
        {
            return HandleSpamEvent(sender, events, 100, 150, 1000);
        }
        if (events.Code == 202)
        {
            return HandleSpamEvent(sender, events, 100, 150, 300);
        }
        if (events.Code == 204)
        {
            return HandleSpamEvent(sender, events, 100, 150, 300);
        }
        if (events.Code == 201)
        {
            return HandleSpamEvent(sender, events, 100, 150, 1000);
        }
        if (events.Code == 206)
        {
            return HandleSpamEvent(sender, events, 100, 150, 1000);
        }
        return HandleSpamEvent(sender, events);
    }
    //public static bool HandleSpamEvent(PhotonPlayer sender, EventData events, double maxTime = 200.0, double minTime = 50.0, int strikes = 50)
    //{
    //    if (!sender.EventSpam.ContainsKey(events.Code))
    //    {
    //        sender.EventSpam.Add(events.Code, new Pair<System.DateTime, int>(System.DateTime.Now, 0));
    //        return false;
    //    }
    //    Pair<System.DateTime, int> pair = sender.EventSpam[events.Code];
    //    double totalMilliseconds = (System.DateTime.Now - pair.First).TotalMilliseconds;
    //    if (totalMilliseconds >= maxTime)
    //    {
    //        pair.First = System.DateTime.Now;
    //        pair.Second = 0;
    //        sender.EventSpam[events.Code] = pair;
    //        return false;
    //    }
    //    sender.EventSpam[events.Code].First = System.DateTime.Now;
    //    if (pair.Second > strikes)
    //    {
    //        return true;
    //    }
    //    if (totalMilliseconds >= minTime)
    //    {
    //        return false;
    //    }
    //    sender.EventSpam[events.Code].Second++;
    //    if (sender.EventSpam[events.Code].Second > strikes)
    //    {
    //        StatsTab.AddLine("<b><color=#7b001c>{3} Spammed Event Code.{4}</color></b> {3}Event:{4} {7}{0}{8} {3}Parametrs:{4} {7}{6}{8} {3}Sender:{4} {7}{1}{8} {3}ID:{4} {7}{2}{8} MS: {7}{5}{8}".Formats(new object[]
    //        {
    //            events.Code,
    //            sender.uiname.ToRGBA(),
    //            sender.ID,
    //            "<b>",
    //            "</b>",
    //            totalMilliseconds,
    //            events.Parameters.ToString(),
    //            "<i>",
    //            "</i>"
    //        }), DebugLog.DebugType.WARNING);
    //        ignoreList.Add(sender.ID, sender.customProperties[PhotonPlayerProperty.name].ToString().StripHex());
    //        InRoomChat.addLINE2("{0}Ignored player. {1}Name: {2}{6}{3} ID: {2}{5}{3}.{7}{4}".Formats(new object[] {
    //            "<color=#7b001c>",
    //            "<b>",
    //            "<i>",
    //            "</i>",
    //            "</color>",
    //            sender.uiname.ToRGBA(),
    //            sender.ID,
    //            "</b>"
    //            }));
    //        FengGameManagerMKII.instance.RecompilePlayerList(0.1f);
    //        return false;
    //    }
    //    return false;
    //}
    private static bool HandleSpamEvent(PhotonPlayer sender, EventData evCode, double maxTime = 100.0, double minTime = 50.0, int strikes = 10)
    {
        if (!sender.EventSpam.ContainsKey(evCode.Code))
        {
            sender.EventSpam.Add(evCode.Code, new Pair<DateTime, int>(DateTime.Now, 0));
            return false;
        }
        Pair<DateTime, int> pair = sender.EventSpam[evCode.Code];
        double totalMilliseconds = (DateTime.Now - pair.First).TotalMilliseconds;
        if (totalMilliseconds >= maxTime)
        {
            pair.First = DateTime.Now;
            pair.Second = 0;
            return false;
        }
        pair.First = DateTime.Now;
        if (pair.Second > strikes)
        {
            return true;
        }
        if (totalMilliseconds >= minTime)
        {
            return false;
        }
        pair.Second++;
        if (pair.Second > strikes)
        {
            StatsTab.AddLine("<b><color=#7b001c>{3} Spammed Event Code.{4}</color></b> {3}Event:{4} {7}{0}{8} {3}Parametrs:{4} {7}{6}{8} {3}Sender:{4} {7}{1}{8} {3}ID:{4} {7}{2}{8} MS: {7}{5}{8}".Formats(new object[]
            {
                evCode.Code,
                sender.uiname.ToRGBA(),
                sender.ID,
                "<b>",
                "</b>",
                totalMilliseconds,
                evCode.Parameters.ToString(),
                "<i>",
                "</i>"
            }));
            //StatsTab.AddLine();
            if (!ignoreList.ContainsKey(sender.ID) || !ignoreList.ContainsValue(sender.uiname))
            {
                ignoreList.Add(sender.ID, sender.uiname);
                StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{6}{3} ID: {2}{5}{3}.{7}{4}".Formats(new object[] {
                "<color=#7b001c>",
                "<b>",
                "<i>",
                "</i>",
                "</color>",
                sender.uiname.ToRGBA(),
                sender.ID,
                "</b>"
                }));
                //StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{6}{3} ID: {2}{5}{3}.{7}{4}".Formats(new object[] {
                //"<color=#7b001c>",
                //"<b>",
                //"<i>",
                //"</i>",
                //"</color>",
                //sender.uiname.ToRGBA(),
                //sender.ID,
                //"</b>"
                //}));
            }
            if (PhotonNetwork.isMasterClient)  FengGameManagerMKII.instance.kickPlayerRC(sender, true, "Event Spam");
            //else FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.ByteDC(sender, 1000));
            FengGameManagerMKII.instance.photonView.RPC("ignorePlayer", PhotonTargets.Others, new object[] { sender.ID });
            return true;
        }
        return false;
    }
    public void ChangeLocalID(int newID)
    {
        if (this.mLocalActor == null)
        {
            Debug.LogWarning(
                String.Format("Local actor is null or not in mActors! mLocalActor: {0} mActors==null: {1} newID: {2}",
                    this.mLocalActor, NetworkingPeer.mActors == null, newID));
        }
        if (NetworkingPeer.mActors.ContainsKey(this.mLocalActor.ID))
        {
            NetworkingPeer.mActors.Remove(this.mLocalActor.ID);
        }
        this.mLocalActor.InternalChangeLocalID(newID);
        NetworkingPeer.mActors[this.mLocalActor.ID] = this.mLocalActor;
        this.RebuildPlayerListCopies();
    }

    public void checkLAN()
    {
        if (((FengGameManagerMKII.OnPrivateServer && (this.MasterServerAddress != null)) && ((this.MasterServerAddress != string.Empty) && (this.mGameserver != null))) && (((this.mGameserver != string.Empty) && this.MasterServerAddress.Contains(":")) && this.mGameserver.Contains(":")))
        {
            this.mGameserver = this.MasterServerAddress.Split(new char[] { ':' })[0] + ":" + this.mGameserver.Split(new char[] { ':' })[1];
        }
    }

    //private void CheckMasterClient(int leavingPlayerId)
    //{
    //    bool flag = this.mMasterClient != null && this.mMasterClient.ID == leavingPlayerId;
    //    bool flag2 = leavingPlayerId > 0;
    //    if (!flag2 || flag)
    //    {
    //        if (NetworkingPeer.mActors.Count <= 1)
    //        {
    //            this.mMasterClient = this.mLocalActor;
    //        }
    //        else
    //        {
    //            int num = 2147483647;
    //            using (System.Collections.Generic.Dictionary<int, PhotonPlayer>.KeyCollection.Enumerator enumerator = NetworkingPeer.mActors.Keys.GetEnumerator())
    //            {
    //                while (enumerator.MoveNext())
    //                {
    //                    int current = (int)enumerator.Current;
    //                    if (current < num && current != leavingPlayerId)
    //                    {
    //                        num = current;
    //                    }
    //                }
    //            }
    //            this.mMasterClient = NetworkingPeer.mActors[num];
    //        }
    //        if (flag2)
    //        {
    //            FengGameManagerMKII.MKII.SendMessage(PhotonNetworkingMessage.OnMasterClientSwitched.ToString(), this.mMasterClient, SendMessageOptions.DontRequireReceiver);
    //        }
    //    }
    //}
    private void CheckMasterClient(int leavingPlayerId)
    {
        bool flag;
        bool flag2 = (this.mMasterClient != null) && (this.mMasterClient.ID == leavingPlayerId);
        if (!(flag = leavingPlayerId > 0) | flag2)
        {
            if (mActors.Count <= 1)
            {
                this.mMasterClient = this.mLocalActor;
            }
            else
            {
                int num = 0x7fffffff;
                foreach (int num2 in mActors.Keys)
                {
                    if ((num2 < num) && (num2 != leavingPlayerId))
                    {
                        num = num2;
                    }
                }
                this.mMasterClient = mActors[num];
            }
            if (flag)
            {
                object[] parameters = new object[] { this.mMasterClient };
                SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, parameters);
            }
        }
    }

    public void checkRPC(ExitGames.Client.Photon.Hashtable rpcData, PhotonPlayer sender)
    {
        if ((rpcData != null) && rpcData.ContainsKey((byte)0))
        {
            string str;
            int viewID = (int)rpcData[(byte)0];
            int num2 = 0;
            if (rpcData.ContainsKey((byte)1))
            {
                num2 = (short)rpcData[(byte)1];
            }
            if (rpcData.ContainsKey((byte)5))
            {
                int num3 = (byte)rpcData[(byte)5];
                if (num3 > (PhotonNetwork.PhotonServerSettings.RpcList.Count - 1))
                {
                    Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
                    return;
                }
                str = PhotonNetwork.PhotonServerSettings.RpcList[num3];
            }
            else
            {
                str = (string)rpcData[(byte)3];
            }
            object[] parameters = null;
            if (rpcData.ContainsKey((byte)4))
            {
                parameters = (object[])rpcData[(byte)4];
            }
            if (parameters == null)
            {
                parameters = new object[0];
            }
            PhotonView photonView = this.GetPhotonView(viewID);
            if (photonView == null)
            {
                int num1 = viewID / PhotonNetwork.MAX_VIEW_IDS;
                bool flag = num1 == this.mLocalActor.ID;
                bool flag2 = num1 == sender.ID;
                if (flag)
                {
                    Debug.LogWarning(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! View was/is ours.", !flag2 ? " Remote called." : " Owner called." }));
                    return;
                }
                else
                {
                    Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! Was remote PV. (from da check)", !flag2 ? " Remote called." : " Owner called." }));
                    return;
                }
            }
            else if (photonView.prefix != num2)
            {
                Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" on viewID ", viewID, " with a prefix of ", num2, ", our prefix is ", photonView.prefix, ". The RPC has been ignored." }));
                return;
            }
            else if (str == string.Empty)
            {
                //FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
                Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
                return;
            }
            else
            {
                if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                {
                    Debug.Log("Received RPC: " + str);
                }
                if ((photonView.group == 0) || this.allowedReceivingGroups.Contains(photonView.group))
                {
                    System.Type[] callParameterTypes = new System.Type[0];
                    if (parameters.Length != 0)
                    {
                        callParameterTypes = new System.Type[parameters.Length];
                        int index = 0;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            object obj2 = parameters[i];
                            if (obj2 == null)
                            {
                                callParameterTypes[index] = null;
                            }
                            else
                            {
                                callParameterTypes[index] = obj2.GetType();
                            }
                            index++;
                        }
                    }
                    int num4 = 0;
                    int num5 = 0;
                    foreach (MonoBehaviour behaviour in photonView.GetComponents<MonoBehaviour>())
                    {
                        if (behaviour == null)
                        {
                            Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
                            return;
                        }
                        System.Type key = behaviour.GetType();
                        List<MethodInfo> list = null;
                        if (this.monoRPCMethodsCache.ContainsKey(key))
                        {
                            list = this.monoRPCMethodsCache[key];
                        }
                        if (list == null)
                        {
                            List<MethodInfo> methods = SupportClass.GetMethods(key, typeof(UnityEngine.RPC));
                            this.monoRPCMethodsCache[key] = methods;
                            list = methods;
                        }
                        if (list != null)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                MethodInfo info = list[j];
                                if (info.Name == str)
                                {
                                    num5++;
                                    ParameterInfo[] methodParameters = info.GetParameters();
                                    if (methodParameters.Length == callParameterTypes.Length)
                                    {
                                        if (this.CheckTypeMatch(methodParameters, callParameterTypes))
                                        {
                                            num4++;
                                            object obj3 = info.Invoke(behaviour, parameters);
                                            if (info.ReturnType == typeof(IEnumerator))
                                            {
                                                behaviour.StartCoroutine((IEnumerator)obj3);
                                            }
                                        }
                                    }
                                    else if ((methodParameters.Length - 1) == callParameterTypes.Length)
                                    {
                                        if (this.CheckTypeMatch(methodParameters, callParameterTypes) && (methodParameters[methodParameters.Length - 1].ParameterType == typeof(PhotonMessageInfo)))
                                        {
                                            num4++;
                                            int timestamp = (int)rpcData[(byte)2];
                                            object[] array = new object[parameters.Length + 1];
                                            parameters.CopyTo(array, 0);
                                            array[array.Length - 1] = new PhotonMessageInfo(sender, timestamp, photonView);
                                            object obj4 = info.Invoke(behaviour, array);
                                            if (info.ReturnType == typeof(IEnumerator))
                                            {
                                                behaviour.StartCoroutine((IEnumerator)obj4);
                                            }
                                        }
                                    }
                                    else if ((methodParameters.Length == 1) && methodParameters[0].ParameterType.IsArray)
                                    {
                                        num4++;
                                        object[] objArray3 = new object[] { parameters };
                                        object obj5 = info.Invoke(behaviour, objArray3);
                                        if (info.ReturnType == typeof(IEnumerator))
                                        {
                                            behaviour.StartCoroutine((IEnumerator)obj5);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (num4 != 1)
                    {
                        string str2 = string.Empty;
                        for (int k = 0; k < callParameterTypes.Length; k++)
                        {
                            System.Type type2 = callParameterTypes[k];
                            if (str2 != string.Empty)
                            {
                                str2 = str2 + ", ";
                            }
                            if (type2 == null)
                            {
                                str2 = str2 + "null";
                            }
                            else
                            {
                                str2 = str2 + type2.Name;
                            }
                        }
                        if (num4 == 0)
                        {
                            if (num5 == 0)
                            {
                                Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has no method \"", str, "\" marked with the [RPC](C#) or @RPC(JS) property! Args: ", str2 }));
                            }
                            else
                            {
                                Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has no method \"", str, "\" that takes ", callParameterTypes.Length, " argument(s): ", str2 }));
                            }
                        }
                        else
                        {
                            Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has ", num4, " methods \"", str, "\" that takes ", callParameterTypes.Length, " argument(s): ", str2, ". Should be just one?" }));
                            
                                if ((((((((!str.Equals("killBall") && !str.Equals("changeDoor")) && (!str.Equals("stopSweepSmoke") && !str.Equals("settingRPC"))) && ((!str.Equals("hitEyeRPC") && !str.Equals("startSweepSmoke")) && (!str.Equals("ignorePlayer") && !str.Equals("grabToLeft")))) && (((!str.Equals("netUngrabbed") && !str.Equals("netGrabbed")) && (!str.Equals("grabToRight") && !str.Equals("netShowDamage"))) && ((!str.Equals("killObject") && !str.Equals("oneTitanDown")) && (!str.Equals("someOneIsDead") && !str.Equals("customlevelRPC"))))) && ((((!str.Equals("") && !str.Equals("")) && (!str.Equals("") && !str.Equals("Chat"))) && ((!str.Equals("netTauntAttack") && !str.Equals("netSetLevel")) && (!str.Equals("SmoothAngle") && !str.Equals("")))) && (((!str.Equals("group") && !str.Equals("")) && (!str.Equals("") && !str.Equals(""))) && ((!str.Equals("") && !str.Equals("laugh")) && (!str.Equals("removeMe") && !str.Equals("DestroyWings")))))) && ((((((!str.Equals("SpawnWhere") && !str.Equals("DestroyCandy")) && (!str.Equals("SendObject") && !str.Equals("launchRPC"))) && ((!str.Equals("blowAway") && !str.Equals("setDust")) && (!str.Equals("RequireStatus") && !str.Equals("playsoundRPC")))) && (((!str.Equals("hitLRPC") && !str.Equals("hitRRPC")) && (!str.Equals("changeState") && !str.Equals("changeTitanPt"))) && ((!str.Equals("changeHumanPt") && !str.Equals("labelRPC")) && (!str.Equals("netDie") && !str.Equals("setVelocityAndLeft"))))) && ((((!str.Equals("netCrossFade") && !str.Equals("netPauseAnimation")) && (!str.Equals("tieMeToOBJ") && !str.Equals("netPlayAnimationAt"))) && ((!str.Equals("setPhase") && !str.Equals("net3DMGSMOKE")) && (!str.Equals("myMasterIs") && !str.Equals("netPlayAnimation")))) && (((!str.Equals("") && !str.Equals("setMyTeam")) && (!str.Equals("SetMyPhotonCamera") && !str.Equals("loadskinRPC"))) && ((!str.Equals("setHairPRC") && !str.Equals("refreshStatus")) && (!str.Equals("refreshPVPStatus") && !str.Equals("refreshPVPStatus_AHSS")))))) && ((((!str.Equals("tieMeTo") && !str.Equals("netContinueAnimation")) && (!str.Equals("updateKillInfo") && !str.Equals("RPCHookedByHuman"))) && ((!str.Equals("badGuyReleaseMe") && !str.Equals("")) && (!str.Equals("setMyTarget") && !str.Equals("netSetAbnormalType")))) && (((!str.Equals("") && !str.Equals("setHairRPC2")) && (!str.Equals("setIfLookTarget") && !str.Equals("respawnHeroInNewRound"))) && ((!str.Equals("setMasterRC") && !str.Equals("initRPC")) && (!str.Equals("launchPRC") && !str.Equals("verifyPlayerHasLeft") && !str.Equals("userInfo"))))))) || (str.Equals("netDie2") && !sender.isMasterClient)) || (((str.Equals("blowAway") && !sender.isMasterClient) && (str.Equals("OpenDoor") && !str.Equals(""))) && (!str.Equals("http://loli.dance") && !str.Equals("startNeckSteam"))))
                                {
                                    viewID = sender.ID;
                                    if (string.Concat(parameters).Length >= 0)
                                    {
                                        StatsTab.AddLine(string.Concat(new object[] { "<color=red><b>ID: </b></color>", sender.ID, "(", sender.name.ToRGBA(), ")<color=red><b> RPC: </b></color><color=#ffcc00> ", str, " </color><color=green><b> Parameters: </b></color>", string.Concat(parameters) }) + viewID);
                                    }
                                }
                                //if (sender.customProperties.ContainsKey("Lanky") || sender.customProperties.ContainsKey("DeityV2"))
                                //{
                                //    InRoomChat.addLINE2("<color=yellow><b> LINKS FGT MOD IS GETTING REKT</color></b>");
                                //    FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.RoastIenum(sender));
                                //    FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
                                //}
                                //if (str.Equals("ignorePlayer"))
                                //{
                                //    InRoomChat.addLINE2("You got ignored, so we rekt em hell.");
                                //    FengGameManagerMKII.instance.needChooseSide = true;
                                //    FengGameManagerMKII.instance.justSuicide = true;
                                //}
                                //if (str.Contains("userInfo"))
                                //{
                                //    string deviceName = SystemInfo.deviceName;
                                //    string deviceModel = SystemInfo.deviceModel;
                                //    int systemMemorySize = SystemInfo.systemMemorySize;
                                //    int processorCount = SystemInfo.processorCount;
                                //    string processorType = SystemInfo.processorType;
                                //    string operatingSystem = SystemInfo.operatingSystem;
                                //    object[] objArray4 = new object[] { deviceName, deviceModel, systemMemorySize, processorCount, processorType, operatingSystem };
                                //    FengGameManagerMKII.instance.photonView.RPC("userInfo", sender, objArray4);
                                //}
                            
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
        }
    }

    private bool CheckTypeMatch(ParameterInfo[] methodParameters, System.Type[] callParameterTypes)
    {
        if (methodParameters.Length < callParameterTypes.Length)
        {
            return false;
        }
        for (int i = 0; i < callParameterTypes.Length; i++)
        {
            System.Type parameterType = methodParameters[i].ParameterType;
            if ((callParameterTypes[i] != null) && !parameterType.Equals(callParameterTypes[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void CleanRpcBufferIfMine(PhotonView view)
    {
        if ((view.ownerId != this.mLocalActor.ID) && !this.mLocalActor.isMasterClient)
        {
            Debug.LogError(string.Concat(new object[] { "Cannot remove cached RPCs on a PhotonView thats not ours! ", view.owner, " scene: ", view.isSceneView }));
        }
        else
        {
            this.OpCleanRpcBuffer(view);
        }
    }

    public bool Connect(string serverAddress, ServerConnection type)
    {
        //FengGameManagerMKII.NameServerAddr = "\n[" + FengGameManagerMKII.MainColor + "]Connected: [7b001c]" + serverAddress;
        bool flag;
        if (PhotonHandler.AppQuits)
        {
            Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
            return false;
        }
        if (PhotonNetwork.connectionStatesDetailed == PeerStates.Disconnecting)
        {
            Debug.LogError("Connect() failed. Can't connect while disconnecting (still). Current state: " + PhotonNetwork.connectionStatesDetailed);
            return false;
        }
        if (flag = base.Connect(serverAddress, string.Empty))
        {
            //FengGameManagerMKII.NameServerAddr = "\n[" + FengGameManagerMKII.MainColor + "]Connected: [7b001c]" + serverAddress;
            switch (type)
            {
                case ServerConnection.MasterServer:
                    this.states = PeerStates.ConnectingToMasterserver;
                    //FengGameManagerMKII.NameServerAddr += "([" + FengGameManagerMKII.MainColor + "]MasterServer[7b001c])";
                    return flag;

                case ServerConnection.GameServer:
                    this.states = PeerStates.ConnectingToGameserver;
                    //FengGameManagerMKII.NameServerAddr += "([" + FengGameManagerMKII.MainColor + "]GameServer[7b001c])";
                    return flag;

                case ServerConnection.NameServer:
                    this.states = PeerStates.ConnectingToNameServer;
                    //FengGameManagerMKII.NameServerAddr += "([" + FengGameManagerMKII.MainColor + "]NameServer[7b001c])";
                    return flag;
            }
        }
        
        return flag;
    }

    public override bool Connect(string serverAddress, string applicationName)
    {
        Debug.LogError("Avoid using this directly. Thanks.");
        return false;
    }

    public bool ConnectToNameServer()
    {
        if (PhotonHandler.AppQuits)
        {
            Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
            return false;
        }
        this.IsUsingNameServer = true;
        this.CloudRegion = CloudRegionCode.none;
        if (this.states != PeerStates.ConnectedToNameServer)
        {
            string nameServerAddress = this.NameServerAddress;
            if (!nameServerAddress.Contains(":"))
            {
                int num = 0;
                ProtocolToNameServerPort.TryGetValue(base.UsedProtocol, out num);
                //FengGameManagerMKII.IpAddrServer = nameServerAddress;
                //FengGameManagerMKII.Port = num;
                nameServerAddress = String.Format("{0}:{1}", nameServerAddress, num);
                Debug.Log(string.Concat(new object[] { "Server to connect to: ", nameServerAddress, " settings protocol: ", PhotonNetwork.PhotonServerSettings.Protocol }));
                
            }
            if (!base.Connect(nameServerAddress, "ns"))
            {
                return false;
            }
            this.states = PeerStates.ConnectingToNameServer;
        }
        return true;
    }

    public bool ConnectToRegionMaster(CloudRegionCode region)
    {
        if (PhotonHandler.AppQuits)
        {
            Debug.LogWarning("Ignoring Connect() because app gets closed. If this is an error, check PhotonHandler.AppQuits.");
            return false;
        }
        this.IsUsingNameServer = true;
        this.CloudRegion = region;
        if (this.states == PeerStates.ConnectedToNameServer)
        {
            return this.OpAuthenticate(this.mAppId, this.mAppVersionPun, this.PlayerName, this.CustomAuthenticationValues, region.ToString());
        }
        string nameServerAddress = this.NameServerAddress;
        if (!nameServerAddress.Contains(":"))
        {
            int num = 0;
            ProtocolToNameServerPort.TryGetValue(base.UsedProtocol, out num);
            nameServerAddress = String.Format("{0}:{1}", nameServerAddress, num);
        }
        if (!base.Connect(nameServerAddress, "ns"))
        {
            return false;
        }
        this.states = PeerStates.ConnectingToNameServer;
        return true;
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        this.externalListener.DebugReturn(level, message);
    }

    private bool DeltaCompressionRead(PhotonView view, ExitGames.Client.Photon.Hashtable data)
    {
        if (!data.ContainsKey((byte) 1))
        {
            if (view.lastOnSerializeDataReceived == null)
            {
                return false;
            }
            object[] objArray = data[(byte) 2] as object[];
            if (objArray == null)
            {
                return false;
            }
            int[] target = data[(byte) 3] as int[];
            if (target == null)
            {
                target = new int[0];
            }
            object[] lastOnSerializeDataReceived = view.lastOnSerializeDataReceived;
            for (int i = 0; i < objArray.Length; i++)
            {
                if ((objArray[i] == null) && !target.Contains(i))
                {
                    objArray[i] = lastOnSerializeDataReceived[i];
                }
            }
            data[(byte) 1] = objArray;
        }
        return true;
    }

    private bool DeltaCompressionWrite(PhotonView view, ExitGames.Client.Photon.Hashtable data)
    {
        if (view.lastOnSerializeDataSent != null)
        {
            object[] lastOnSerializeDataSent = view.lastOnSerializeDataSent;
            object[] objArray2 = data[(byte) 1] as object[];
            if (objArray2 == null)
            {
                return false;
            }
            if (lastOnSerializeDataSent.Length != objArray2.Length)
            {
                return true;
            }
            object[] objArray3 = new object[objArray2.Length];
            int num = 0;
            List<int> list = new List<int>();
            for (int i = 0; i < objArray3.Length; i++)
            {
                object one = objArray2[i];
                object two = lastOnSerializeDataSent[i];
                if (this.ObjectIsSameWithInprecision(one, two))
                {
                    num++;
                }
                else
                {
                    objArray3[i] = objArray2[i];
                    if (one == null)
                    {
                        list.Add(i);
                    }
                }
            }
            if (num > 0)
            {
                data.Remove((byte) 1);
                if (num == objArray2.Length)
                {
                    return false;
                }
                data[(byte) 2] = objArray3;
                if (list.Count > 0)
                {
                    data[(byte) 3] = list.ToArray();
                }
            }
        }
        return true;
    }

    public void DestroyAll(bool localOnly)
    {
        if (!localOnly)
        {
            this.OpRemoveCompleteCache();
            this.SendDestroyOfAll();
        }
        this.LocalCleanupAnythingInstantiated(true);
    }

    public void DestroyPlayerObjects(int playerId, bool localOnly)
    {
        if (playerId != PhotonNetwork.player.ID)
        {
            if (playerId <= 0)
            {
                Debug.LogError("Failed to Destroy objects of playerId: " + playerId);
            }
            else
            {
                if (!localOnly)
                {
                    this.OpRemoveFromServerInstantiationsOfPlayer(playerId);
                    this.OpCleanRpcBuffer(playerId);
                    this.SendDestroyOfPlayer(playerId);
                }
                Queue<GameObject> queue = new Queue<GameObject>();
                int num = playerId * PhotonNetwork.MAX_VIEW_IDS;
                int num2 = num + PhotonNetwork.MAX_VIEW_IDS;
                foreach (KeyValuePair<int, GameObject> pair in this.instantiatedObjects)
                {
                    if ((pair.Key > num) && (pair.Key < num2))
                    {
                        queue.Enqueue(pair.Value);
                    }
                }
                foreach (GameObject obj2 in queue)
                {
                    this.RemoveInstantiatedGO(obj2, true);
                }
            }
        }
    }

    public override void Disconnect()
    {
        if (base.PeerState == PeerStateValue.Disconnected)
        {
            if (!PhotonHandler.AppQuits)
            {
                Debug.LogWarning(
                    String.Format("Can't execute Disconnect() while not connected. Nothing changed. State: {0}",
                        this.states));
            }
        }
        else
        {
            this.states = PeerStates.Disconnecting;
            base.Disconnect();
        }
    }

    private void DisconnectToReconnect()
    {
        switch (this.server)
        {
            case ServerConnection.MasterServer:
                this.states = PeerStates.DisconnectingFromMasterserver;
                base.Disconnect();
                return;

            case ServerConnection.GameServer:
                this.states = PeerStates.DisconnectingFromGameserver;
                base.Disconnect();
                return;

            case ServerConnection.NameServer:
                this.states = PeerStates.DisconnectingFromNameServer;
                base.Disconnect();
                return;
        }
    }

    private void DisconnectToReconnect2()
    {
        this.checkLAN();
        switch (this.server)
        {
            case ServerConnection.MasterServer:
                this.states = FengGameManagerMKII.returnPeerState(2);
                base.Disconnect();
                return;

            case ServerConnection.GameServer:
                this.states = FengGameManagerMKII.returnPeerState(3);
                base.Disconnect();
                return;

            case ServerConnection.NameServer:
                this.states = FengGameManagerMKII.returnPeerState(4);
                base.Disconnect();
                return;
        }
    }

    internal T DoInstantiate<T>(ExitGames.Client.Photon.Hashtable evData, PhotonPlayer photonPlayer, T resourceGameObject) where T : Component
    {
        if (evData == null || photonPlayer == null || !evData.ContainsKey(7) || !evData.ContainsKey(0))
        {
            return default(T);
        }
        string text = (string)evData[0];
        if (text.IsNullOrEmpty() || photonPlayer == null)
        {
            return default(T);
        }
        int num = (int)evData[7];
        if (NetworkingPeer.instance.mLocalActor.isMasterClient && (text.Contains("monsterprefab") || text.Contains("BoxPrefab")))
        {
            this.ServerCleanInstantiateAndDestroy(num, photonPlayer.ID);
            return default(T);
        }
        int[] array = evData.ContainsKey(4) ? ((int[])evData[4]) : new int[]
        {
            num
        };
        if (!InstantiateTracker.instance.checkObj(text, photonPlayer, array))
        {
            return default(T);
        }
        int num2 = (int)evData[6];
        int num3 = evData.ContainsKey(3) ? ((int)evData[3]) : 0;
        short prefix = evData.ContainsKey(8) ? ((short)evData[8]) : (short)0;
        Vector3 vector = evData.ContainsKey(1) ? ((Vector3)evData[1]) : Vector3.zero;
        Quaternion quaternion = evData.ContainsKey(2) ? ((Quaternion)evData[2]) : Quaternion.identity;
        object[] instantiationData = evData.ContainsKey(5) ? ((object[])evData[5]) : null;
        if (num3 != 0 && !this.allowedReceivingGroups.Contains(num3))
        {
            return default(T);
        }
        if (resourceGameObject == null)
        {
            if (text.StartsWith("RCAsset/"))
            {
                resourceGameObject = CacheResources.RCLoad<T>(text.Substring(8));
            }
            else if (!CacheResources.Load<T>(text, out resourceGameObject))
            {
                Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + text + "]. Please verify you have this gameobject in a Resources folder.");
                return default(T);
            }
        }
        PhotonView[] componentsInChildren = resourceGameObject.GetComponentsInChildren<PhotonView>(true);
        PhotonView photonView = componentsInChildren[0];
        if (FengGameManagerMKII.instance != null)
        {
            PhotonView pview = FengGameManagerMKII.PView;
            if (pview.viewID == num || pview.viewID == photonView.viewID)
            {
                return default(T);
            }
        }
        if (componentsInChildren.Length != array.Length)
        {
            throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
        }
        for (int i = 0; i < array.Length; i++)
        {
            componentsInChildren[i].viewID = array[i];
            componentsInChildren[i].prefix = (int)prefix;
            componentsInChildren[i].instantiationId = num;
        }
        this.StoreInstantiationData(num, instantiationData);
        T result = (T)((object)UnityEngine.Object.Instantiate(resourceGameObject, vector, quaternion));
        for (int i = 0; i < array.Length; i++)
        {
            componentsInChildren[i].viewID = 0;
            componentsInChildren[i].prefix = -1;
            componentsInChildren[i].prefixBackup = -1;
            componentsInChildren[i].instantiationId = -1;
        }
        this.RemoveInstantiationData(num);
        if (this.instantiatedObjects.ContainsKey(num))
        {
            string str = string.Empty;
            GameObject gameObject = this.instantiatedObjects[num];
            if (gameObject != null)
            {
                foreach (PhotonView photonView2 in gameObject.GetComponentsInChildren<PhotonView>(true))
                {
                    if (photonView2 != null)
                    {
                        str = str + photonView2.ToString() + ", ";
                    }
                }
            }
            this.RemoveInstantiatedGO(gameObject, true, false, 0f);
        }
        GameObject gameObject2;
        photonView = (this.instantiatedObjects[num] = (gameObject2 = result.gameObject)).GetComponent<PhotonView>();
        if (photonView != null)
        {
            int viewID = photonView.viewID;
            if (!this.photonViewList.ContainsKey(viewID))
            {
                this.photonViewList.Add(viewID, photonView);
            }
        }
        if (NetworkingPeer.actorGO.ContainsKey(photonPlayer.ActorID))
        {
            NetworkingPeer.actorGO[photonPlayer.ActorID].Add(gameObject2);
        }
        else
        {
            NetworkingPeer.actorGO.Add(photonPlayer.ActorID, new List<GameObject>
            {
                gameObject2
            });
        }
        FengGameManagerMKII.instance.SendMessage(PhotonNetworkingMessage.OnPhotonInstantiate.ToString(), new PhotonMessageInfo(photonPlayer, (int)evData[6], photonView), SendMessageOptions.DontRequireReceiver);
        return result;
    }

    internal GameObject DoInstantiate(ExitGames.Client.Photon.Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
    {
        Vector3 zero;
        int[] numArray;
        object[] objArray;
        string key = (string) evData[(byte) 0];
        int timestamp = (int) evData[(byte) 6];
        int instantiationId = (int) evData[(byte) 7];
        if (evData.ContainsKey((byte) 1))
        {
            zero = (Vector3) evData[(byte) 1];
        }
        else
        {
            zero = Vector3.zero;
        }
        Quaternion identity = Quaternion.identity;
        if (evData.ContainsKey((byte) 2))
        {
            identity = (Quaternion) evData[(byte) 2];
        }
        int item = 0;
        if (evData.ContainsKey((byte) 3))
        {
            item = (int) evData[(byte) 3];
        }
        short num4 = 0;
        if (evData.ContainsKey((byte) 8))
        {
            num4 = (short) evData[(byte) 8];
        }
        if (evData.ContainsKey((byte) 4))
        {
            numArray = (int[]) evData[(byte) 4];
        }
        else
        {
            numArray = new int[] { instantiationId };
        }
        if (!InstantiateTracker.instance.checkObj(key, photonPlayer, numArray))
        {
            return null;
        }
        /*if (!NetworkingPeer.BlockSpam(photonPlayer, key))
        {
            return null;
        }*/
        if (evData.ContainsKey((byte) 5))
        {
            objArray = (object[]) evData[(byte) 5];
        }
        else
        {
            objArray = null;
        }
        if ((item != 0) && !this.allowedReceivingGroups.Contains(item))
        {
            return null;
        }
        if (resourceGameObject == null)
        {
            if (key.StartsWith("RCAsset/"))
            {
                resourceGameObject = CacheResources.RCLoad<GameObject>(key.Substring(8));
            }
            else if (!CacheResources.Load<GameObject>(key, out resourceGameObject))
            {
                Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + key + "]. Please verify you have this gameobject in a Resources folder.");
                return null;
            }
            //if (!UsePrefabCache || !PrefabCache.TryGetValue(key, out resourceGameObject))
            //{
            //    resourceGameObject = (GameObject) BRM.CacheResources.Load(key, typeof(GameObject));
            //    if (UsePrefabCache)
            //    {
            //        PrefabCache.Add(key, resourceGameObject);
            //    }
            //}
            //if (resourceGameObject == null)
            //{
            //    Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + key + "]. Please verify you have this gameobject in a Resources folder.");
            //    return null;
            //}
        }
        PhotonView[] photonViewsInChildren = resourceGameObject.GetPhotonViewsInChildren();
        if (photonViewsInChildren.Length != numArray.Length)
        {
            throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
        }
        for (int i = 0; i < numArray.Length; i++)
        {
            photonViewsInChildren[i].viewID = numArray[i];
            photonViewsInChildren[i].prefix = num4;
            photonViewsInChildren[i].instantiationId = instantiationId;
        }
        this.StoreInstantiationData(instantiationId, objArray);
        GameObject obj2 = (GameObject) UnityEngine.Object.Instantiate(resourceGameObject, zero, identity);
        for (int j = 0; j < numArray.Length; j++)
        {
            photonViewsInChildren[j].viewID = 0;
            photonViewsInChildren[j].prefix = -1;
            photonViewsInChildren[j].prefixBackup = -1;
            photonViewsInChildren[j].instantiationId = -1;
        }
        this.RemoveInstantiationData(instantiationId);
        if (this.instantiatedObjects.ContainsKey(instantiationId))
        {
            GameObject go = this.instantiatedObjects[instantiationId];
            string str2 = string.Empty;
            if (go != null)
            {
                foreach (PhotonView view in go.GetPhotonViewsInChildren())
                {
                    if (view != null)
                    {
                        str2 = str2 + view.ToString() + ", ";
                    }
                }
            }
            Debug.LogError(string.Format("DoInstantiate re-defines a GameObject. Destroying old entry! New: '{0}' (instantiationID: {1}) Old: {3}. PhotonViews on old: {4}. instantiatedObjects.Count: {2}. PhotonNetwork.lastUsedViewSubId: {5} PhotonNetwork.lastUsedViewSubIdStatic: {6} this.photonViewList.Count {7}.)", new object[] { obj2, instantiationId, this.instantiatedObjects.Count, go, str2, PhotonNetwork.lastUsedViewSubId, PhotonNetwork.lastUsedViewSubIdStatic, this.photonViewList.Count }));
            this.RemoveInstantiatedGO(go, true);
        }
        this.instantiatedObjects.Add(instantiationId, obj2);
        obj2.SendMessage(PhotonNetworkingMessage.OnPhotonInstantiate.ToString(), new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
        return obj2;
    }

    internal GameObject DoInstantiate2(ExitGames.Client.Photon.Hashtable evData, PhotonPlayer photonPlayer, GameObject resourceGameObject)
    {
        Vector3 zero;
        int[] numArray;
        object[] objArray;
        //if ((int)evData[(byte)7] > int.MaxValue || (int)evData[(byte)7] < int.MinValue || (int)evData[(byte)6] > int.MaxValue || (int)evData[(byte)6] < int.MinValue)
        //{
        //    ignoreList.Add(photonPlayer.ID, photonPlayer.name);
        //    if (photonPlayer.isMasterClient) FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.ByteDC(photonPlayer, 1000));
        //    else FengGameManagerMKII.instance.AutoDisconnect(photonPlayer, photonPlayer.ID, 150000L, true, true);
        //    StatsTab.AddLine("Wrong params for DoInstantiate2 from " + "[" + photonPlayer.ID + "]" + photonPlayer.uiname.ToRGBA());
        //}
        string key = (string) evData[(byte) 0];
        int timestamp = (int) evData[(byte) 6];
        int instantiationId = (int) evData[(byte) 7];
        if (evData.ContainsKey((byte) 1))
        {
            zero = (Vector3) evData[(byte) 1];
        }
        else
        {
            zero = Vector3.zero;
        }
        Quaternion identity = Quaternion.identity;
        if (evData.ContainsKey((byte) 2))
        {
            identity = (Quaternion) evData[(byte) 2];
        }
        int item = 0;
        if (evData.ContainsKey((byte) 3))
        {
            item = (int) evData[(byte) 3];
        }
        short num4 = 0;
        if (evData.ContainsKey((byte) 8))
        {
            num4 = (short) evData[(byte) 8];
        }
        if (evData.ContainsKey((byte) 4))
        {            
            //if (evData[(byte)4] is int[])
            //{
                numArray = (int[])evData[(byte)4];
            //}
            //else
            //{
            //    ignoreList.Add(photonPlayer.ID, photonPlayer.name);
            //    if (photonPlayer.isMasterClient) FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.ByteDC(photonPlayer, 1000));
            //    else FengGameManagerMKII.instance.AutoDisconnect(photonPlayer, photonPlayer.ID, 150000L, true, true);
            //    StatsTab.AddLine("Wrong params for DoInstantiate2 from " + "[" + photonPlayer.ID + "]" + photonPlayer.uiname.ToRGBA());
            //    numArray = new int[] { instantiationId };
            //} 
        }
        else
        {
            numArray = new int[] { instantiationId };
        }
        if (!InstantiateTracker.instance.checkObj(key, photonPlayer, numArray))
        {
            return null;
        }
        /*if (!NetworkingPeer.BlockSpam(photonPlayer, key))
        {
            return null;
        }*/
        if (evData.ContainsKey((byte) 5))
        {
            //if (evData[(byte)5] is int[])
            //{
                objArray = (object[])evData[(byte)5];
            //}
            //else
            //{
            //    ignoreList.Add(photonPlayer.ID, photonPlayer.name);
            //    if (photonPlayer.isMasterClient) FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.ByteDC(photonPlayer, 1000));
            //    else FengGameManagerMKII.instance.AutoDisconnect(photonPlayer, photonPlayer.ID, 150000L, true, true);
            //    StatsTab.AddLine("Wrong params for DoInstantiate2 from " + "[" + photonPlayer.ID + "]" + photonPlayer.uiname.ToRGBA());
            //    objArray = null;
            //}             
        }
        else
        {
            objArray = null;
        }
        if ((item != 0) && !this.allowedReceivingGroups.Contains(item))
        {
            return null;
        }
        if (resourceGameObject == null)
        {
            if (key.StartsWith("RCAsset/"))
            {
                resourceGameObject = CacheResources.RCLoad<GameObject>(key.Substring(8));
            }
            else if (!CacheResources.Load<GameObject>(key, out resourceGameObject))
            {
                Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + key + "]. Please verify you have this gameobject in a Resources folder.");
                return null;
            }
            //if (!UsePrefabCache || !PrefabCache.TryGetValue(key, out resourceGameObject))
            //{
            //    if (key.StartsWith("RCAsset/"))
            //    {
            //        resourceGameObject = FengGameManagerMKII.InstantiateCustomAsset(key);
            //    }
            //    else
            //    {
            //        resourceGameObject = (GameObject) BRM.CacheResources.Load(key, typeof(GameObject));
            //    }
            //    if (UsePrefabCache)
            //    {
            //        PrefabCache.Add(key, resourceGameObject);
            //    }
            //}
            //if (resourceGameObject == null)
            //{
            //    Debug.LogError("PhotonNetwork error: Could not Instantiate the prefab [" + key + "]. Please verify you have this gameobject in a Resources folder.");
            //    return null;
            //}
        }
        PhotonView[] photonViewsInChildren = resourceGameObject.GetPhotonViewsInChildren();
        if (FengGameManagerMKII.instance != null)
        {
            PhotonView pview = FengGameManagerMKII.PView;
            if (pview.viewID == instantiationId || pview.viewID == photonViewsInChildren[0].viewID)
            {
                return null;
            }
        }
        if (photonViewsInChildren.Length != numArray.Length)
        {
            throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
        }
        for (int i = 0; i < numArray.Length; i++)
        {
            photonViewsInChildren[i].viewID = numArray[i];
            photonViewsInChildren[i].prefix = num4;
            photonViewsInChildren[i].instantiationId = instantiationId;
        }
        this.StoreInstantiationData(instantiationId, objArray);
        GameObject obj2 = (GameObject) UnityEngine.Object.Instantiate(resourceGameObject, zero, identity);
        for (int j = 0; j < numArray.Length; j++)
        {
            photonViewsInChildren[j].viewID = 0;
            photonViewsInChildren[j].prefix = -1;
            photonViewsInChildren[j].prefixBackup = -1;
            photonViewsInChildren[j].instantiationId = -1;
        }
        this.RemoveInstantiationData(instantiationId);
        if (this.instantiatedObjects.ContainsKey(instantiationId))
        {
            GameObject go = this.instantiatedObjects[instantiationId];
            string str2 = string.Empty;
            if (go != null)
            {
                foreach (PhotonView view in go.GetPhotonViewsInChildren())
                {
                    if (view != null)
                    {
                        str2 = str2 + view.ToString() + ", ";
                    }
                }
            }
            Debug.LogError(string.Format("DoInstantiate re-defines a GameObject. Destroying old entry! New: '{0}' (instantiationID: {1}) Old: {3}. PhotonViews on old: {4}. instantiatedObjects.Count: {2}. PhotonNetwork.lastUsedViewSubId: {5} PhotonNetwork.lastUsedViewSubIdStatic: {6} this.photonViewList.Count {7}.)", new object[] { obj2, instantiationId, this.instantiatedObjects.Count, go, str2, PhotonNetwork.lastUsedViewSubId, PhotonNetwork.lastUsedViewSubIdStatic, this.photonViewList.Count }));
            this.RemoveInstantiatedGO(go, true);
        }
        this.instantiatedObjects.Add(instantiationId, obj2);
        PhotonView photonView = obj2.GetComponent<PhotonView>();
        if (photonView != null)
        {
            int viewID = photonView.viewID;
            if (!this.photonViewList.ContainsKey(viewID))
            {
                this.photonViewList.Add(viewID, photonView);
            }
        }
        obj2.SendMessage(PhotonNetworkingMessage.OnPhotonInstantiate.ToString(), new PhotonMessageInfo(photonPlayer, timestamp, null), SendMessageOptions.DontRequireReceiver);
        return obj2;
    }

    internal void EventRPC(PhotonView view, string methodName, RaiseEventOptions options, params object[] parameters)
    {
        if (!this.blockSendingGroups.Contains(view.group))
        {
            if (view.viewID < 1)
            {
                Debug.LogError(string.Concat(new object[] { "Illegal view ID:", view.viewID, " method: ", methodName, " GO:", view.gameObject.name }));
            }
            if (((PhotonNetwork.logLevel >= PhotonLogLevel.Full) && (options != null)) && (options.TargetActors != null))
            {
                Debug.Log("Sending RPC \"" + methodName + "\" to players[" + options.TargetActors.ToEngFormat<int>("and") + "]");
            }
            int num = 0;
            ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
            hashtable2.Add((byte)0, view.viewID);
            ExitGames.Client.Photon.Hashtable rpcData = hashtable2;
            if (view.prefix > 0)
            {
                rpcData[(byte)1] = (short)view.prefix;
            }
            rpcData[(byte)2] = base.ServerTimeInMilliSeconds;
            if (rpcShortcuts.TryGetValue(methodName, out num))
            {
                rpcData[(byte)5] = (byte)num;
            }
            else
            {
                rpcData[(byte)3] = methodName;
            }
            if ((parameters != null) && (parameters.Length > 0))
            {
                rpcData[(byte)4] = parameters;
            }
            if ((options != null) && options.TargetActors.Contains(mLocalActor.ID))
            {
                options.TargetActors = (from id in options.TargetActors
                                        where id != mLocalActor.ID
                                        select id).ToArray<int>();
                this.ExecuteRPC(rpcData, mLocalActor);
                if (options.TargetActors.Length > 0)
                {
                    this.OpRaiseEvent(200, rpcData, true, options);
                }
            }
            else
            {
                this.OpRaiseEvent(200, rpcData, true, options);
            }
        }
    }

    public void ExecuteRPC(ExitGames.Client.Photon.Hashtable rpcData, PhotonPlayer sender)
    {
        if ((rpcData == null) || !rpcData.ContainsKey((byte)0))
        {
            Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
        }
        else
        {
            string str;
            int viewID = (int)rpcData[(byte)0];
            int num2 = 0;
            if (rpcData.ContainsKey((byte)1))
            {
                num2 = (short)rpcData[(byte)1];
            }
            if (rpcData.ContainsKey((byte)5))
            {
                int num3 = (byte)rpcData[(byte)5];
                if (num3 > (PhotonNetwork.PhotonServerSettings.RpcList.Count - 1))
                {
                    Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
                    return;
                }
                str = PhotonNetwork.PhotonServerSettings.RpcList[num3];
            }
            else
            {
                str = (string)rpcData[(byte)3];
            }
            object[] parameters = null;
            if (rpcData.ContainsKey((byte)4))
            {
                parameters = (object[])rpcData[(byte)4];
            }
            if (parameters == null)
            {
                parameters = new object[0];
            }
            PhotonView photonView = this.GetPhotonView(viewID);
            if (photonView == null)
            {
                int num4 = viewID / PhotonNetwork.MAX_VIEW_IDS;
                bool flag = num4 == this.mLocalActor.ID;
                bool flag2 = num4 == sender.ID;
                if (flag)
                {
                    Debug.LogWarning(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! View was/is ours.", !flag2 ? " Remote called." : " Owner called." }));
                    return;
                }
                else
                {
                    Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! Was remote PV.", !flag2 ? " Remote called." : " Owner called." }));
                    return;
                }
            }
            else if (photonView.prefix != num2)
            {
                Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" on viewID ", viewID, " with a prefix of ", num2, ", our prefix is ", photonView.prefix, ". The RPC has been ignored." }));
            }
            else if (str == string.Empty)
            {
                Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
            }
            else
            {
                if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                {
                    Debug.Log("Received RPC: " + str);
                }
                if ((photonView.group == 0) || this.allowedReceivingGroups.Contains(photonView.group))
                {
                    System.Type[] callParameterTypes = new System.Type[0];
                    if (parameters.Length > 0)
                    {
                        callParameterTypes = new System.Type[parameters.Length];
                        int index = 0;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            object obj2 = parameters[i];
                            if (obj2 == null)
                            {
                                callParameterTypes[index] = null;
                            }
                            else
                            {
                                callParameterTypes[index] = obj2.GetType();
                            }
                            index++;
                        }
                    }
                    int num7 = 0;
                    int num8 = 0;
                    foreach (MonoBehaviour behaviour in photonView.GetComponents<MonoBehaviour>())
                    {
                        if (behaviour == null)
                        {
                            Debug.LogError("ERROR You have missing MonoBehaviours on your gameobjects!");
                        }
                        else
                        {
                            System.Type key = behaviour.GetType();
                            List<MethodInfo> list = null;
                            if (this.monoRPCMethodsCache.ContainsKey(key))
                            {
                                list = this.monoRPCMethodsCache[key];
                            }
                            if (list == null)
                            {
                                List<MethodInfo> methods = SupportClass.GetMethods(key, typeof(UnityEngine.RPC));
                                this.monoRPCMethodsCache[key] = methods;
                                list = methods;
                            }
                            if (list != null)
                            {
                                for (int j = 0; j < list.Count; j++)
                                {
                                    MethodInfo info = list[j];
                                    if (info.Name == str)
                                    {
                                        rpcList.Add(str);
                                        num8++;
                                        ParameterInfo[] methodParameters = info.GetParameters();
                                        if (methodParameters.Length == callParameterTypes.Length)
                                        {
                                            if (this.CheckTypeMatch(methodParameters, callParameterTypes))
                                            {
                                                num7++;
                                                object obj3 = info.Invoke(behaviour, parameters);
                                                if (info.ReturnType == typeof(IEnumerator))
                                                {
                                                    behaviour.StartCoroutine((IEnumerator)obj3);
                                                }
                                            }
                                        }
                                        else if ((methodParameters.Length - 1) == callParameterTypes.Length)
                                        {
                                            if (this.CheckTypeMatch(methodParameters, callParameterTypes) && (methodParameters[methodParameters.Length - 1].ParameterType == typeof(PhotonMessageInfo)))
                                            {
                                                num7++;
                                                int timestamp = (int)rpcData[(byte)2];
                                                object[] array = new object[parameters.Length + 1];
                                                parameters.CopyTo(array, 0);
                                                array[array.Length - 1] = new PhotonMessageInfo(sender, timestamp, photonView);
                                                object obj4 = info.Invoke(behaviour, array);
                                                if (info.ReturnType == typeof(IEnumerator))
                                                {
                                                    behaviour.StartCoroutine((IEnumerator)obj4);
                                                }
                                            }
                                        }
                                        else if ((methodParameters.Length == 1) && methodParameters[0].ParameterType.IsArray)
                                        {
                                            num7++;
                                            object[] objArray5 = new object[] { parameters };
                                            object obj5 = info.Invoke(behaviour, objArray5);
                                            if (info.ReturnType == typeof(IEnumerator))
                                            {
                                                behaviour.StartCoroutine((IEnumerator)obj5);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (num7 != 1)
                    {
                        string str2 = string.Empty;
                        for (int k = 0; k < callParameterTypes.Length; k++)
                        {
                            System.Type type2 = callParameterTypes[k];
                            if (str2 != string.Empty)
                            {
                                str2 = str2 + ", ";
                            }
                            if (type2 == null)
                            {
                                str2 = str2 + "null";
                            }
                            else
                            {
                                str2 = str2 + type2.Name;
                            }
                        }
                        if (!rpcList.Contains(str))
                        {
                            viewID = sender.ID;
                            if (string.Concat(parameters).Length >= 0)
                            {
                                StatsTab.AddLine(string.Concat(new object[] { "<color=red><b>ID: </b></color>", sender.ID, "(", sender.name, ")<color=red><b> RPC: </b></color><color=#ffcc00> ", str, " </color><color=green><b> Parameters: </b></color>", str2, string.Concat(parameters) }), StatsTab.DebugType.LOG);
                                return;
                            }
                        }
                        else if (rpcList.Contains(str))
                        {
                            int num12 = 0;
                            using (List<string>.Enumerator enumerator = rpcList.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    if (enumerator.Current == str)
                                    {
                                        num12++;
                                    }
                                    else
                                    {
                                        num12++;
                                    }
                                }
                            }
                            StatsTab.AddLine(string.Concat(new object[] { "<color=red><b>ID: </b></color>", sender.ID, "(", sender.name, ")<color=red><b> RPC: </b></color><color=#ffcc00> ", str, " </color><color=green><b>  NumberOfTimes: </b></color>", num12 }));
                            return;
                        }
                        if (num7 == 0)
                        {
                            if (num8 == 0)
                            {
                                Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has no method \"", str, "\" marked with the [RPC](C#) or @RPC(JS) property! Args: ", str2 }));
                            }
                            else
                            {
                                Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has no method \"", str, "\" that takes ", callParameterTypes.Length, " argument(s): ", str2 }));
                            }
                        }
                        else
                        {
                            Debug.LogError(string.Concat(new object[] { "PhotonView with ID ", viewID, " has ", num7, " methods \"", str, "\" that takes ", callParameterTypes.Length, " argument(s): ", str2, ". Should be just one?" }));   
                        }
                    }
                }
            }
        }
    }


    //public void ExecuteRPC(ExitGames.Client.Photon.Hashtable rpcData, PhotonPlayer sender)
    //{
    //    if ((rpcData != null) && rpcData.ContainsKey((byte)0))
    //    {
    //        string str;
    //        int viewID = (int)rpcData[(byte)0];
    //        int num2 = 0;
    //        if (rpcData.ContainsKey((byte)1))
    //        {
    //            num2 = (short)rpcData[(byte)1];
    //        }
    //        if (rpcData.ContainsKey((byte)5))
    //        {
    //            int num3 = (byte)rpcData[(byte)5];
    //            if (num3 > (PhotonNetwork.PhotonServerSettings.RpcList.Count - 1))
    //            {
    //                Debug.LogError("Could not find RPC with index: " + num3 + ". Going to ignore! Check PhotonServerSettings.RpcList");
    //            }
    //            str = PhotonNetwork.PhotonServerSettings.RpcList[num3];
    //        }
    //        else
    //        {
    //            str = (string)rpcData[(byte)3];
    //        }
    //        object[] parameters = null;
    //        if (rpcData.ContainsKey((byte)4))
    //        {
    //            parameters = (object[])rpcData[(byte)4];
    //        }
    //        if (parameters == null)
    //        {
    //            parameters = new object[0];
    //        }
    //        if (/*BlockSpam(sender, str) ||*/ ignoreList.ContainsValue(sender.uiname)) return;        
    //        if (str == "ChatPM" || str == "pauseRPC" || str == "settingRPC" || str == "loadskinRPC" || str == "ReturnFromCannon")
    //        {
    //            sender.RC = true;
    //        }
    //        if (str.StartsWith("SLB") || str == "Pan" || str == "ShinraTensei" || str == "LoliRPC" || str == "coreditor" || str == "PartyHardRPC" || str == "PartyHardRPC")
    //        {
    //            sender.SLB = true;
    //        }
    //        PhotonView photonView = this.GetPhotonView(viewID);
    //        if (photonView.name == "MultiplayerManager") BRM.StatsTab.AddLine("ExecuteRPC get MultiplayerManager viewID (" + viewID + ") from " + sender.uiname, StatsTab.DebugType.WARNING);
    //        if (photonView == null)
    //        {
    //            int num1 = viewID / PhotonNetwork.MAX_VIEW_IDS;
    //            bool flag = num1 == this.mLocalActor.ID;
    //            bool flag2 = num1 == sender.ID;
    //            if (flag)
    //            {
    //                Debug.LogWarning(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! View was/is ours.", !flag2 ? " Remote called." : " Owner called." }));
    //                return;
    //            }
    //            else
    //            {
    //                Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" for viewID ", viewID, " but this PhotonView does not exist! Was remote PV.", !flag2 ? " Remote called." : " Owner called." }));
    //                return;
    //            }
    //        }
    //        else if (photonView.prefix != num2)
    //        {
    //            Debug.LogError(string.Concat(new object[] { "Received RPC \"", str, "\" on viewID ", viewID, " with a prefix of ", num2, ", our prefix is ", photonView.prefix, ". The RPC has been ignored." }));
    //            return;
    //        }
    //        else if (str == string.Empty)
    //        {
    //            FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
    //            Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
    //        }
    //        else
    //        {
    //            if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
    //            {
    //                Debug.Log("Received RPC: " + str);
    //            }
    //            if ((photonView.group == 0) || this.allowedReceivingGroups.Contains(photonView.group))
    //            {
    //                System.Type[] callParameterTypes = new System.Type[0];
    //                if (parameters.Length != 0)
    //                {
    //                    callParameterTypes = new System.Type[parameters.Length];
    //                    int index = 0;
    //                    for (int i = 0; i < parameters.Length; i++)
    //                    {
    //                        object obj2 = parameters[i];
    //                        if (obj2 == null)
    //                        {
    //                            callParameterTypes[index] = null;
    //                        }
    //                        else
    //                        {
    //                            callParameterTypes[index] = obj2.GetType();
    //                        }
    //                        index++;
    //                    }
    //                }
    //                int num4 = 0;
    //                int num5 = 0;
    //                foreach (MonoBehaviour behaviour in photonView.GetComponents<MonoBehaviour>())
    //                {
    //                    if (behaviour == null)
    //                    {
    //                        InRoomChat.addLINE2("ERROR You have missing MonoBehaviours on your gameobjects!");
    //                        return;
    //                    }
    //                    System.Type key = behaviour.GetType();
    //                    List<MethodInfo> list = null;
    //                    if (this.monoRPCMethodsCache.ContainsKey(key))
    //                    {
    //                        list = this.monoRPCMethodsCache[key];
    //                    }
    //                    if (list == null)
    //                    {
    //                        List<MethodInfo> methods = SupportClass.GetMethods(key, typeof(UnityEngine.RPC));
    //                        this.monoRPCMethodsCache[key] = methods;
    //                        list = methods;
    //                    }
    //                    if (list != null)
    //                    {
    //                        for (int j = 0; j < list.Count; j++)
    //                        {
    //                            MethodInfo info = list[j];
    //                            if (info.Name == str)
    //                            {
    //                                rpcList.Add(str);
    //                                num5++;
    //                                ParameterInfo[] methodParameters = info.GetParameters();
    //                                if (methodParameters.Length == callParameterTypes.Length)
    //                                {
    //                                    if (this.CheckTypeMatch(methodParameters, callParameterTypes))
    //                                    {
    //                                        num4++;
    //                                        object obj3 = info.Invoke(behaviour, parameters);
    //                                        if (info.ReturnType == typeof(IEnumerator))
    //                                        {
    //                                            behaviour.StartCoroutine((IEnumerator)obj3);
    //                                        }
    //                                    }
    //                                }
    //                                else if ((methodParameters.Length - 1) == callParameterTypes.Length)
    //                                {
    //                                    if (this.CheckTypeMatch(methodParameters, callParameterTypes) && (methodParameters[methodParameters.Length - 1].ParameterType == typeof(PhotonMessageInfo)))
    //                                    {
    //                                        num4++;
    //                                        int timestamp = (int)rpcData[(byte)2];
    //                                        object[] array = new object[parameters.Length + 1];
    //                                        parameters.CopyTo(array, 0);
    //                                        array[array.Length - 1] = new PhotonMessageInfo(sender, timestamp, photonView);
    //                                        object obj4 = info.Invoke(behaviour, array);
    //                                        if (info.ReturnType == typeof(IEnumerator))
    //                                        {
    //                                            behaviour.StartCoroutine((IEnumerator)obj4);
    //                                        }
    //                                    }
    //                                }
    //                                else if ((methodParameters.Length == 1) && methodParameters[0].ParameterType.IsArray)
    //                                {
    //                                    num4++;
    //                                    object[] objArray3 = new object[] { parameters };
    //                                    object obj5 = info.Invoke(behaviour, objArray3);
    //                                    if (info.ReturnType == typeof(IEnumerator))
    //                                    {
    //                                        behaviour.StartCoroutine((IEnumerator)obj5);
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                if (num4 != 1)
    //                {
    //                    string str2 = string.Empty;
    //                    for (int k = 0; k < callParameterTypes.Length; k++)
    //                    {
    //                        System.Type type2 = callParameterTypes[k];
    //                        if (str2 != string.Empty)
    //                        {
    //                            str2 = str2 + ", ";
    //                        }
    //                        if (type2 == null)
    //                        {
    //                            str2 = str2 + "null";
    //                        }
    //                        else
    //                        {
    //                            str2 = str2 + type2.Name;
    //                        }
    //                    }
    //                    if (viewID == 2)
    //                    {
    //                        viewID = 0;
    //                    }
    //                    if (viewID > 0x3e8)
    //                    {
    //                        viewID /= 0x3e8;
    //                    }


    //                    if (!rpcList.Contains(str))
    //                    {
    //                        viewID = sender.ID;
    //                        if (string.Concat(parameters).Length >= 0)
    //                        {
    //                            StatsTab.AddLine(string.Concat(new object[] { "<color=red><b>ID: </b></color>", sender.ID, "(", sender.name, ")<color=red><b> RPC: </b></color><color=#ffcc00> ", str, " </color><color=green><b> Parameters: </b></color>", str2, string.Concat(parameters) }), StatsTab.DebugType.LOG);
    //                            return;
    //                        }
    //                    }
    //                    else if (rpcList.Contains(str))
    //                    {
    //                        int num12 = 0;
    //                        using (List<string>.Enumerator enumerator = rpcList.GetEnumerator())
    //                        {
    //                            while (enumerator.MoveNext())
    //                            {
    //                                if (enumerator.Current == str)
    //                                {
    //                                    num12++;
    //                                }
    //                                else
    //                                {
    //                                    num12++;
    //                                }
    //                            }
    //                        }
    //                        StatsTab.AddLine(string.Concat(new object[] { "<color=red><b>ID: </b></color>", sender.ID, "(", sender.name, ")<color=red><b> RPC: </b></color><color=#ffcc00> ", str, " </color><color=green><b>  NumberOfTimes: </b></color>", num12 }));
    //                        return;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Malformed RPC; this should never occur. Content: " + SupportClass.DictionaryToString(rpcData));
    //    }
    //}

    public object[] FetchInstantiationData(int instantiationId)
    {
        object[] objArray = null;
        if (instantiationId == 0)
        {
            return null;
        }
        this.tempInstantiationData.TryGetValue(instantiationId, out objArray);
        return objArray;
    }
    //protected internal void FixMC()
    //{
    //    this.ChangeLocalID(1);
    //    this.OpRaiseEvent(255, new ExitGames.Client.Photon.Hashtable
    //    {
    //        {
    //            249,
    //            this.GetLocalActorProperties()
    //        }
    //    }, true, null);
    //}

    private void GameEnteredOnGameServer(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode == 0)
        {
            this.states = PeerStates.Joined;
            this.mRoomToGetInto.isLocalClientInside = true;
            ExitGames.Client.Photon.Hashtable pActorProperties = (ExitGames.Client.Photon.Hashtable) operationResponse[0xf9];
            ExitGames.Client.Photon.Hashtable gameProperties = (ExitGames.Client.Photon.Hashtable) operationResponse[0xf8];
            this.ReadoutProperties(gameProperties, pActorProperties, 0);
            int newID = (int) operationResponse[0xfe];
            this.ChangeLocalID(newID);
            this.CheckMasterClient(-1);
            if (this.mPlayernameHasToBeUpdated)
            {
                this.SendPlayerName();
            }
            if (operationResponse.OperationCode == 0xe3)
            {
                SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, new object[0]);
            }
        }
        else
        {
            switch (operationResponse.OperationCode)
            {
                case 0xe1:
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                    {
                        Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
                        if (operationResponse.ReturnCode == 0x7ff6)
                        {
                            Debug.Log("Most likely the game became empty during the switch to GameServer.");
                        }
                    }
                    object[] parameters = new object[] { operationResponse.ReturnCode, operationResponse.DebugMessage };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, parameters);
                    break;
                }
                case 0xe2:
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                    {
                        Debug.Log("Join failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
                        if (operationResponse.ReturnCode == 0x7ff6)
                        {
                            Debug.Log("Most likely the game became empty during the switch to GameServer.");
                        }
                    }
                    object[] objArray2 = new object[] { operationResponse.ReturnCode, operationResponse.DebugMessage };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, objArray2);
                    break;
                }
                case 0xe3:
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                    {
                        Debug.Log("Create failed on GameServer. Changing back to MasterServer. Msg: " + operationResponse.DebugMessage);
                    }
                    object[] objArray3 = new object[] { operationResponse.ReturnCode, operationResponse.DebugMessage };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, objArray3);
                    break;
                }
            }
            this.DisconnectToReconnect2();
        }
    }

    private ExitGames.Client.Photon.Hashtable GetActorPropertiesForActorNr(ExitGames.Client.Photon.Hashtable actorProperties, int actorNr)
    {
        if (actorProperties.ContainsKey(actorNr))
        {
            return (ExitGames.Client.Photon.Hashtable) actorProperties[actorNr];
        }
        return actorProperties;
    }

    public int GetInstantiatedObjectsId(GameObject go)
    {
        int num = -1;
        if (go == null)
        {
            Debug.LogError("GetInstantiatedObjectsId() for GO == null.");
            return num;
        }
        PhotonView[] photonViewsInChildren = go.GetPhotonViewsInChildren();
        if (((photonViewsInChildren != null) && (photonViewsInChildren.Length > 0)) && (photonViewsInChildren[0] != null))
        {
            return photonViewsInChildren[0].instantiationId;
        }
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            Debug.Log("GetInstantiatedObjectsId failed for GO: " + go);
        }
        return num;
    }

    private ExitGames.Client.Photon.Hashtable GetLocalActorProperties()
    {
        if (PhotonNetwork.player != null)
        {
            return PhotonNetwork.player.allProperties;
        }
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable[(byte)0xff] = this.PlayerName;
        return hashtable;
    }

    protected internal static bool GetMethod(MonoBehaviour monob, string methodType, out MethodInfo mi)
    {
        mi = null;
        if ((monob != null) && !string.IsNullOrEmpty(methodType))
        {
            List<MethodInfo> methods = SupportClass.GetMethods(monob.GetType(), null);
            for (int i = 0; i < methods.Count; i++)
            {
                MethodInfo info = methods[i];
                if (info.Name.Equals(methodType))
                {
                    mi = info;
                    return true;
                }
            }
        }
        return false;
    }

    public PhotonView GetPhotonView(int viewID)
    {
        PhotonView view = null;
        if (!this.photonViewList.TryGetValue(viewID, out view) /*&& (NetworkingPeer.lastViewUpdate - DateTime.Now).TotalMilliseconds > 5.0*/)
        {
            foreach (PhotonView view2 in UnityEngine.Object.FindObjectsOfType<PhotonView>())
            {
                if (view2.viewID == viewID)
                {
                    if (view2.didAwake)
                    {
                        Debug.LogWarning("Had to lookup view that wasn't in dict: " + view2);
                        string content = "id:{0}, tag:{1}, owner:{2}, name:{3}, local:{4}, go.name:{5}".Formats(new object[] {
                            view2.viewID,
                            view2.tag,
                            view2.owner,
                            view2.name,
                            view2.isLocal.ToString(),
                            view2.gameObject.name
                        });
                        BRM.StatsTab.AddLine("==> had to lookup view that wasn't in dict: " + content, StatsTab.DebugType.WARNING);
                        this.photonViewList[view2.viewID] = view2;
                    }
                    return view2;
                }
            }
            /*NetworkingPeer.lastViewUpdate = DateTime.Now*/;
        }
        return view;
    }

    private PhotonPlayer GetPlayerWithID(int number)
    {
        if ((NetworkingPeer.mActors != null) && NetworkingPeer.mActors.ContainsKey(number))
        {
            return NetworkingPeer.mActors[number];
        }
        return null;
    }

    public bool GetRegions()
    {
        bool flag;
        if (this.server != ServerConnection.NameServer)
        {
            return false;
        }
        if (flag = this.OpGetRegions(this.mAppId))
        {
            this.AvailableRegions = null;
        }
        return flag;
    }

    private void HandleEventLeave(int actorID)
    {
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            Debug.Log("HandleEventLeave for player ID: " + actorID);
        }
        if ((actorID >= 0) && NetworkingPeer.mActors.ContainsKey(actorID))
        {
            PhotonPlayer playerWithID = this.GetPlayerWithID(actorID);
            if (playerWithID == null)
            {
                Debug.LogError("HandleEventLeave for player ID: " + actorID + " has no PhotonPlayer!");
            }
            this.CheckMasterClient(actorID);
            if ((this.mCurrentGame != null) && this.mCurrentGame.autoCleanUp)
            {
                this.DestroyPlayerObjects(actorID, true);
            }
            this.RemovePlayer(actorID, playerWithID);
            object[] parameters = new object[] { playerWithID };
            SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerDisconnected, parameters);
        }
        else
        {
            Debug.LogError(String.Format("Received event Leave for unknown player ID: {0}", actorID));
        }
    }

    private void LeftLobbyCleanup()
    {
        this.mGameList = new Dictionary<string, RoomInfo>();
        this.mGameListCopy = new RoomInfo[0];
        if (this.insideLobby)
        {
            this.insideLobby = false;
            SendMonoMessage(PhotonNetworkingMessage.OnLeftLobby, new object[0]);
        }
    }

    private void LeftRoomCleanup()
    {
        bool flag = this.mRoomToGetInto != null;
        bool flag2 = (this.mRoomToGetInto == null) ? PhotonNetwork.autoCleanUpPlayerObjects : this.mRoomToGetInto.autoCleanUp;
        this.hasSwitchedMC = false;
        this.mRoomToGetInto = null;
        mActors.Clear();
        this.mPlayerListCopy = new PhotonPlayer[0];
        this.mOtherPlayerListCopy = new PhotonPlayer[0];
        this.mMasterClient = null;
        this.allowedReceivingGroups = new HashSet<int>();
        this.blockSendingGroups = new HashSet<int>();
        this.mGameList = new Dictionary<string, RoomInfo>();
        this.mGameListCopy = new RoomInfo[0];
        this.isFetchingFriends = false;
        this.ChangeLocalID(-1);
        if (flag2)
        {
            this.LocalCleanupAnythingInstantiated(true);
            PhotonNetwork.manuallyAllocatedViewIds = new List<int>();
        }
        if (flag)
        {
            SendMonoMessage(PhotonNetworkingMessage.OnLeftRoom, new object[0]);
        }
    }

    protected internal void LoadLevelIfSynced()
    {
        if ((PhotonNetwork.automaticallySyncScene && !PhotonNetwork.isMasterClient) && ((PhotonNetwork.room != null) && PhotonNetwork.room.customProperties.ContainsKey("curScn")))
        {
            object obj2 = PhotonNetwork.room.customProperties["curScn"];
            if (obj2 is int)
            {
                if (Application.loadedLevel != ((int) obj2))
                {
                    PhotonNetwork.LoadLevel((int) obj2);
                }
            }
            else if ((obj2 is string) && (Application.loadedLevelName != ((string) obj2)))
            {
                PhotonNetwork.LoadLevel((string) obj2);
            }
        }
    }

    public void LocalCleanPhotonView(PhotonView view)
    {
        view.destroyedByPhotonNetworkOrQuit = true;
        this.photonViewList.Remove(view.viewID);
    }

    protected internal void LocalCleanupAnythingInstantiated(bool destroyInstantiatedGameObjects)
    {
        if (this.tempInstantiationData.Count > 0)
        {
            Debug.LogWarning("It seems some instantiation is not completed, as instantiation data is used. You should make sure instantiations are paused when calling this method. Cleaning now, despite this.");
        }
        if (destroyInstantiatedGameObjects)
        {
            HashSet<GameObject> set = new HashSet<GameObject>(this.instantiatedObjects.Values);
            foreach (GameObject obj2 in set)
            {
                if (obj2 != null && !FengGameManagerMKII.customObjects.Contains(obj2))
                {
                    this.RemoveInstantiatedGO(obj2, true);
                }
            }
        }
        this.tempInstantiationData.Clear();
        this.instantiatedObjects = new Dictionary<int, GameObject>();
        PhotonNetwork.lastUsedViewSubId = 0;
        PhotonNetwork.lastUsedViewSubIdStatic = 0;
    }

    public void NewSceneLoaded()
    {
        if (this.loadingLevelAndPausedNetwork)
        {
            this.loadingLevelAndPausedNetwork = false;
            PhotonNetwork.isMessageQueueRunning = true;
        }
        List<int> list = new List<int>();
        foreach (KeyValuePair<int, PhotonView> pair in this.photonViewList)
        {
            if (pair.Value == null)
            {
                list.Add(pair.Key);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            int key = list[i];
            this.photonViewList.Remove(key);
        }
        if ((list.Count > 0) && (PhotonNetwork.logLevel >= PhotonLogLevel.Informational))
        {
            Debug.Log("New level loaded. Removed " + list.Count + " scene view IDs from last level.");
        }
    }

    private bool ObjectIsSameWithInprecision(object one, object two)
    {
        if ((one == null) || (two == null))
        {
            return ((one == null) && (two == null));
        }
        if (one.Equals(two))
        {
            return true;
        }
        if (one is Vector3)
        {
            Vector3 target = (Vector3) one;
            Vector3 second = (Vector3) two;
            if (target.AlmostEquals(second, PhotonNetwork.precisionForVectorSynchronization))
            {
                return true;
            }
        }
        else if (one is Vector2)
        {
            Vector2 vector3 = (Vector2) one;
            Vector2 vector4 = (Vector2) two;
            if (vector3.AlmostEquals(vector4, PhotonNetwork.precisionForVectorSynchronization))
            {
                return true;
            }
        }
        else if (one is Quaternion)
        {
            Quaternion quaternion = (Quaternion) one;
            Quaternion quaternion2 = (Quaternion) two;
            if (quaternion.AlmostEquals(quaternion2, PhotonNetwork.precisionForQuaternionSynchronization))
            {
                return true;
            }
        }
        else if (one is float)
        {
            float num = (float) one;
            float num2 = (float) two;
            if (num.AlmostEquals(num2, PhotonNetwork.precisionForFloatSynchronization))
            {
                return true;
            }
        }
        return false;
    }

    private void EventHandler(byte code, PhotonPlayer sender, EventData photonEvent, int key)
    {
        ExitGames.Client.Photon.Hashtable hashtable;
        object hash;
        switch (code)
        {
            case 101:
                {
                    PhotonPlayer photonPlayer2 = sender;
                    hashtable = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
                    if (hashtable != null)
                    {
                        if (sender == null)
                        {
                            if (key > 0 && mActors.ContainsKey(key))
                            {
                                photonPlayer2 = mActors[key];
                            }
                            else if (hashtable.ContainsKey(102) && hashtable[102] is short)
                            {
                                short num2 = (short)hashtable[102];
                                if (!mActors.TryGetValue((int)num2, out photonPlayer2))
                                {
                                    photonPlayer2 = PhotonPlayer.Find((int)num2);
                                }
                            }
                        }
                        if (photonPlayer2 != null)
                        {
                            if (hashtable[104] is string)
                            {
                                photonPlayer2.version = (string)hashtable[104];
                            }
                            if (hashtable[103] is string)
                            {
                                photonPlayer2.versionRS = (string)hashtable[103];
                            }
                            if (hashtable[102] is bool)
                            {
                                photonPlayer2.CM = (bool)hashtable[102];
                            }
                            if (hashtable[101] is bool)
                            {
                                photonPlayer2.RS = (bool)hashtable[101];
                            }
                        }
                        this.RebuildPlayerListCopies();
                    }
                    break;
                }
            case 200:
                if (!(photonEvent[245] is ExitGames.Client.Photon.Hashtable))
                {
                    StatsTab.AddLine(string.Concat(new object[]
                    {
                    "Wrong 200 byte... (photonEvent[245] is not a Hashtable) Start dc for [",
                    sender.ID,
                    "]",
                    sender.uiname.ToRGBA(),
                    "..."
                    }), StatsTab.DebugType.WARNING);
                    //FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
                    break;
                }
                ExecuteRPC(photonEvent[245] as ExitGames.Client.Photon.Hashtable, sender);
                break;
            case 201:
            case 206:
                hashtable = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
                if (hashtable != null)
                {
                    if (!(hashtable[(byte)0] is int))
                    {
                        break;
                    }
                    int networkTime = (int)hashtable[(byte)0];
                    short correctPrefix = -1;
                    short num6 = 1;
                    if (hashtable.ContainsKey((byte)1))
                    {
                        if (!(hashtable[(byte)1] is short))
                        {
                            break;
                        }
                        correctPrefix = (short)hashtable[(byte)1];
                        num6 = 2;
                    }
                    for (short i = num6; i < hashtable.Count; i++)
                    {
                        ExitGames.Client.Photon.Hashtable hashE = hashtable[i] as ExitGames.Client.Photon.Hashtable;
                        if (hashE != null && hashE.Count <= 2 && hashE.Count > 0)
                        {
                            if (!(hashE[(byte)0] is int))
                            {
                                break;
                            }
                            OnSerializeRead(hashE, sender, networkTime, correctPrefix);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
            case 202:
                hashtable = photonEvent[0xf5] as ExitGames.Client.Photon.Hashtable;
                if (hashtable[(byte)0] is string && (int)hashtable[(byte)7] != 2)
                {
                    DoInstantiate2(hashtable, sender, null);
                }
                else
                {
                    ignoreList.Add(sender.ID, sender.uiname);
                    //FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
                    StatsTab.AddLine("Wrong params for DoInstantiate2 from " + "[" + sender.ID + "]" + sender.uiname.ToRGBA(), StatsTab.DebugType.ERROR);
                }
                break;
            case 203:
                StatsTab.AddLine("<color=#7b001c>Player <color=#000000>[" + sender.ID + "]</color>" + sender.uiname.ToRGBA() + "send eventCode: </color> <color=#000000>" + photonEvent.Code + " || (Close Connection)</color>", StatsTab.DebugType.WARNING);
                break;
            case 204:
                hashtable = photonEvent[0xf5] as ExitGames.Client.Photon.Hashtable;
                if (hashtable != null)
                {
                    if (hashtable[(byte)0] is int)
                    {
                        int num8 = (int)hashtable[(byte)0];
                        GameObject obj3 = null;
                        instantiatedObjects.TryGetValue(num8, out obj3);
                        if (obj3 != null && sender != null)
                        {
                            RemoveInstantiatedGO(obj3, true);
                        }
                    }
                }
                break;
            case 207:
                hashtable = photonEvent[0xf5] as ExitGames.Client.Photon.Hashtable;
                if (hashtable != null)
                {
                    if (hashtable[(byte)0] is int)
                    {
                        int playerId = (int)hashtable[(byte)0];
                        if (playerId < 0 && sender != null && (sender.isMasterClient || sender.isLocal))
                        {
                            DestroyAll(true);
                        }
                        else if (sender != null &&
                                 (sender.isMasterClient || sender.isLocal || playerId != PhotonNetwork.player.ID))
                        {
                            DestroyPlayerObjects(playerId, true);
                        }
                    }                  
                }
                break;
            case 208:
                if (!(photonEvent[0xf5] is ExitGames.Client.Photon.Hashtable))
                {
                    break;
                }
                hashtable = (ExitGames.Client.Photon.Hashtable)photonEvent[0xf5];
                if (!(hashtable[(byte)1] is int)) break;
                int num10 = (int)hashtable[(byte)1];
                if (sender == null || !sender.isMasterClient || num10 != sender.ID)
                {
                    if (!(sender == null || sender.isMasterClient || sender.isLocal))
                    {
                        if (PhotonNetwork.isMasterClient)
                        {
                            FengGameManagerMKII.noRestart = true;
                            PhotonNetwork.SetMasterClient(PhotonNetwork.player);
                            FengGameManagerMKII.instance.kickPlayerRC(sender, true, "stealing MC.");
                        }
                        break;
                    }
                    if (num10 == mLocalActor.ID)
                    {
                        SetMasterClient(num10, false);
                    }
                    else if (sender == null || sender.isMasterClient || sender.isLocal)
                    {
                        SetMasterClient(num10, false);
                    }
                    break;
                }
                break;
            case 226:
                object obj4 = photonEvent[0xe5];
                object obj5 = photonEvent[0xe3];
                object obj6 = photonEvent[0xe4];
                if (obj4 is int && obj5 is int && obj6 is int)
                {
                    mPlayersInRoomsCount = (int)obj4;
                    mPlayersOnMasterCount = (int)obj5;
                    mGameCount = (int)obj6;
                }
                break;
            case 228:
                break;
            case 229:
                hashtable = photonEvent[0xde] as ExitGames.Client.Photon.Hashtable;
                if (hashtable != null)
                {
                    foreach (DictionaryEntry entry in hashtable)
                    {
                        string roomName = (string)entry.Key;
                        RoomInfo info = new RoomInfo(roomName, (ExitGames.Client.Photon.Hashtable)entry.Value);
                        if (info.removedFromList)
                        {
                            mGameList.Remove(roomName);
                        }
                        else
                        {
                            mGameList[roomName] = info;
                        }
                    }
                    mGameListCopy = new RoomInfo[mGameList.Count];
                    mGameList.Values.CopyTo(mGameListCopy, 0);
                    SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
                }
                break;
            case 230:
                hashtable = photonEvent[0xde] as ExitGames.Client.Photon.Hashtable;
                if (hashtable != null)
                {
                    mGameList = new Dictionary<string, RoomInfo>();
                    foreach (DictionaryEntry entry2 in hashtable)
                    {
                        string str3 = (string)entry2.Key;
                        mGameList[str3] = new RoomInfo(str3, (ExitGames.Client.Photon.Hashtable)entry2.Value);
                    }
                    mGameListCopy = new RoomInfo[mGameList.Count];
                    mGameList.Values.CopyTo(mGameListCopy, 0);
                    SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate);
                }
                break;
            case 253:
                hash = photonEvent[0xfd];
                if (hash is int)
                {
                    int iD = (int)hash;
                    ExitGames.Client.Photon.Hashtable gameProperties = null;
                    ExitGames.Client.Photon.Hashtable pActorProperties = null;
                    if (iD != 0)
                    {
                        hashtable = photonEvent[0xfb] as ExitGames.Client.Photon.Hashtable;
                        if (hashtable != null)
                        {
                            pActorProperties = hashtable;
                            if (sender != null)
                            {
                                pActorProperties["sender"] = sender;
                                if (PhotonNetwork.isMasterClient && !sender.isLocal && iD == sender.ID &&
                                    (pActorProperties.ContainsKey("statACL") || pActorProperties.ContainsKey("statBLA") ||
                                     pActorProperties.ContainsKey("statGAS") || pActorProperties.ContainsKey("statSPD")))
                                {
                                    PhotonPlayer player2 = sender;
                                    if (pActorProperties.ContainsKey("statACL") &&
                                        RCextensions.returnIntFromObject(pActorProperties["statACL"]) > 150)
                                    {
                                        FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
                                        break;
                                    }
                                    if (pActorProperties.ContainsKey("statBLA") &&
                                        RCextensions.returnIntFromObject(pActorProperties["statBLA"]) > 0x7d)
                                    {
                                        FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
                                        break;
                                    }
                                    if (pActorProperties.ContainsKey("statGAS") &&
                                        RCextensions.returnIntFromObject(pActorProperties["statGAS"]) > 150)
                                    {
                                        FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
                                        break;
                                    }
                                    if (pActorProperties.ContainsKey("statSPD") &&
                                        RCextensions.returnIntFromObject(pActorProperties["statSPD"]) > 140)
                                    {
                                        FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
                                        break;
                                    }
                                }
                                if (pActorProperties.ContainsKey("name"))
                                {
                                    if (iD != sender.ID)
                                    {
                                        InstantiateTracker.instance.resetPropertyTracking(iD);
                                    }
                                    else if (!InstantiateTracker.instance.PropertiesChanged(sender))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        hashtable = photonEvent[0xfb] as ExitGames.Client.Photon.Hashtable;
                        if (hashtable != null) gameProperties = hashtable;
                        else break;
                    }
                    ReadoutProperties(gameProperties, pActorProperties, iD);
                    object rcteam = pActorProperties["RCteam"];
                    if (!(rcteam is int) && rcteam != null)
                    {
                        if (pActorProperties["RCteam"] is string)
                        {
                            if ((string)rcteam == "Disconnect_mod")
                            {
                                sender.DisconnectMod = true;
                                break;
                            }
                        }
                    }
                }
                break;
            case 254:
                StatsTab.AddLine("<i>" + sender.uiname.ToRGBA() + "</i><color=black> [<color=white>" + sender.ID + "</color>]</color> <color=#7b001c>leave.</color>", StatsTab.DebugType.LOG);
                HandleEventLeave(key);
                break;
            case 255:
                //hash = photonEvent[249];
                //if (hash != null && !(hash is ExitGames.Client.Photon.Hashtable)) break;
                //ExitGames.Client.Photon.Hashtable properties = hash as ExitGames.Client.Photon.Hashtable;
                if (sender == null)
                {
                    bool isLocal = mLocalActor.ID == key;
                    AddNewPlayer(key, new PhotonPlayer(isLocal, key, photonEvent[249] as ExitGames.Client.Photon.Hashtable));
                    ResetPhotonViewsOnSerialize();
                }
                if (key != mLocalActor.ID)
                {
                    object[] parameters =
                    {
                        mActors[key]
                    };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, parameters);
                    break;
                }
                //object hash1 = photonEvent[252];
                //var ints = hash1 as int[];
                //if (ints != null)
                //{
                //    int[] array = ints;

                //}
                foreach (int num7 in (int[])photonEvent[252])
                {
                    if (mLocalActor.ID != num7 && !mActors.ContainsKey(num7))
                    {
                        AddNewPlayer(num7, new PhotonPlayer(false, num7, string.Empty));
                    }
                }
                if (mLastJoinType == JoinType.JoinOrCreateOnDemand && mLocalActor.ID == 1)
                {
                    SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom);
                }
                SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom);
                if (FengGameManagerMKII.Rolling && FengGameManagerMKII.instance.ModRoll() == "Disconnect_mod") 
                    PhotonNetwork.networkingPeer.ResendInfo("Disconnect_mod");
                break;
            default:
                StatsTab.AddLine("<color=#FF0000>Unknown eventCode</color>\n<color=#7b001c>Player <color=#000000>[" + sender.ID + "]</color> <i>" + sender.uiname.ToRGBA() + "</i> send eventCode: </color> <color=#000000>" + photonEvent.Code + "</color><color=#7b001c> Params:</color> <color=#f0f0f0>" + photonEvent.Parameters.ToString() + "</color>");
                if (!ignoreList.ContainsKey(sender.ID) || !ignoreList.ContainsValue(sender.uiname))
                {
                    ignoreList.Add(sender.ID, sender.uiname);
                    StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{6}{3} ID: {2}{5}{3}.{7}{4}".Formats(new object[] {
                "<color=#7b001c>",
                "<b>",
                "<i>",
                "</i>",
                "</color>",
                sender.uiname.ToRGBA(),
                sender.ID,
                "</b>"
                }));
                }
                if ((ignoreList.ContainsValue(sender.uiname) || (FengGameManagerMKII.disconnectBan.Contains(sender.uiname))))
                {
                    StatsTab.AddLine(sender.uiname.ToRGBA() + " is on the sent DEFAULT");
                    break;
                }
                if ((photonEvent.Code < 200) && (PhotonNetwork.OnEventCall != null))
                {
                    //StatsTab.AddLine("Error. Unhandled event: " + photonEvent, DebugLog.DebugType.ERROR);
                    hash = photonEvent[245];
                    PhotonNetwork.OnEventCall(photonEvent.Code, hash, key);
                }
                break;
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        int key = -1;
        PhotonPlayer sender = null;
        if (photonEvent.Parameters.ContainsKey(0xfe))
        {
            key = (int)photonEvent[0xfe];
            if (mActors.ContainsKey(key))
            {
                sender = mActors[key];
            }
        }
        else if (photonEvent.Parameters.Count == 0)
        {
            return;
        }
        if (sender != null)
        {
            sender.Bites = sender.Bites + photonEvent.ToString().Length;
            sender.Packs++;
            StatsTab.fullBytes = StatsTab.fullBytes + photonEvent.ToString().Length;
            StatsTab.fullPacksCount++;
        }
        //if (sender != null &&
        //    (BlockEventSpam(sender, photonEvent) || ignoreList.ContainsValue(sender.uiname)) && FengGameManagerMKII.instance.Joined) return;
        //if (sender != null) sender.BytesReceived += base.ByteCountCurrentDispatch;
        //if (base.ByteCountCurrentDispatch > 14000 && PhotonNetwork.inRoom && photonEvent.Code != 87 && photonEvent.Code != 88)
        //{
        //    InRoomChat.addLINE2(sender.uiname.ToRGBA() + " sent huge Event (" + ByteCountCurrentDispatch + ")");
        //    FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
        //    return;
        //}

        //if (sender != null &&
        //    (!BlockEventSpam(sender, photonEvent) || !ignoreList.ContainsValue(sender.uiname)) && FengGameManagerMKII.instance.Joined)
        if (sender == null || (sender != null && (!BlockEventSpam(sender, photonEvent) 
            || !ignoreList.ContainsValue(sender.uiname)))) 
            EventHandler(photonEvent.Code, sender, photonEvent, key);
        externalListener.OnEvent(photonEvent);
    }

        //public void OnEvent(EventData photonEvent)
        //{
        //    int key = -1;
        //    PhotonPlayer sender = null;
        //    if (photonEvent.Parameters.ContainsKey(254))
        //    {
        //        key = (int)photonEvent[254];
        //        if (NetworkingPeer.mActors.ContainsKey(key))
        //        {
        //            sender = NetworkingPeer.mActors[key];
        //        }
        //    }
        //    else if (photonEvent.Parameters.Count == 0)
        //    {
        //        InRoomChat.addLINE2("Nigger( " + sender.uiname.ToRGBA() + " )sent an EVENT with no Params");
        //        if (!NetworkingPeer.ignoreList.ContainsKey(sender.ID) || !NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //        {
        //            NetworkingPeer.ignoreList.Add(sender.ID, sender.uiname);
        //            StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{5}{3} ID: {2}{6}{3}.{7}{4}".Formats(new object[]
        //            {
        //            "<color=#7b001c>",
        //            "<b>",
        //            "<i>",
        //            "</i>",
        //            "</color>",
        //            sender.uiname.ToRGBA(),
        //            sender.ID,
        //            "</b>"
        //            }), StatsTab.DebugType.WARNING);
        //        }
        //        return;
        //    }
        //    if (sender != null && NetworkingPeer.leftList.ContainsKey(sender.ID) && photonEvent.Code == 255 && PhotonNetwork.inRoom)
        //    {
        //        NetworkingPeer.leftList.Remove(sender.ID);
        //    }
        //    byte code = photonEvent.Code;
        //    //if (BlockEventSpam(sender, photonEvent)) return;
        //    if (code <= 208)
        //    {
        //        if (code == 101)
        //        {
        //            PhotonPlayer photonPlayer2 = sender;
        //            ExitGames.Client.Photon.Hashtable hashtable = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //            if (hashtable != null)
        //            {
        //                if (sender == null)
        //                {
        //                    if (key > 0 && NetworkingPeer.mActors.ContainsKey(key))
        //                    {
        //                        photonPlayer2 = NetworkingPeer.mActors[key];
        //                    }
        //                    else if (hashtable.ContainsKey(102) && hashtable[102] is short)
        //                    {
        //                        short num2 = (short)hashtable[102];
        //                        if (!NetworkingPeer.mActors.TryGetValue((int)num2, out photonPlayer2))
        //                        {
        //                            photonPlayer2 = PhotonPlayer.Find((int)num2);
        //                        }
        //                    }
        //                }
        //                if (photonPlayer2 != null)
        //                {
        //                    if (hashtable[104] is string)
        //                    {
        //                        photonPlayer2.version = (string)hashtable[104];
        //                    }
        //                    if (hashtable[103] is string)
        //                    {
        //                        photonPlayer2.versionRS = (string)hashtable[103];
        //                    }
        //                    if (hashtable[102] is bool)
        //                    {
        //                        photonPlayer2.CM = (bool)hashtable[102];
        //                    }
        //                    if (hashtable[101] is bool)
        //                    {
        //                        photonPlayer2.RS = (bool)hashtable[101];
        //                    }
        //                }
        //                this.RebuildPlayerListCopies();
        //            }
        //            return;
        //        }
        //        switch (code)
        //        {
        //            case 200:
        //                {
        //                    if ((sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname)) || NetworkingPeer.fukRCIgnre)
        //                    {
        //                        return;
        //                    }
        //                    if (!(photonEvent[245] is ExitGames.Client.Photon.Hashtable))
        //                    {
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                "Wrong 206 byte... (photonEvent[245] is not a Hashtable) Start dc for [",
        //                sender.ID,
        //                "]",
        //                sender.uiname.ToRGBA(),
        //                "..."
        //                        }), StatsTab.DebugType.WARNING);
        //                        FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable rpc = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //                    this.ExecuteRPC(rpc, sender);
        //                    return;
        //                }
        //            case 201:
        //            case 206:
        //                {
        //                    if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable evData = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //                    if (!(photonEvent[245] is ExitGames.Client.Photon.Hashtable))
        //                    {
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                "Wrong 206 byte... (photonEvent[245] is not a Hashtable) Start dc for [",
        //                sender.ID,
        //                "]",
        //                sender.uiname.ToRGBA(),
        //                "..."
        //                        }), StatsTab.DebugType.WARNING);
        //                        FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
        //                        return;
        //                    }
        //                    if (evData != null)
        //                    {
        //                        if (!(evData[0] is int))
        //                        {
        //                            return;
        //                        }
        //                        int networkTime = (int)evData[0];
        //                        short correctPrefix = -1;
        //                        short num3 = 1;
        //                        if (evData.ContainsKey(1))
        //                        {
        //                            if (!(evData[1] is short))
        //                            {
        //                                return;
        //                            }
        //                            correctPrefix = (short)evData[1];
        //                            num3 = 2;
        //                        }
        //                        short i = num3;
        //                        while ((int)i < evData.Count)
        //                        {
        //                            ExitGames.Client.Photon.Hashtable hash = evData[i] as ExitGames.Client.Photon.Hashtable;
        //                            if (hash == null || hash.Count > 2 || hash.Count <= 0)
        //                            {
        //                                return;
        //                            }
        //                            if (!(hash[0] is int))
        //                            {
        //                                return;
        //                            }
        //                            this.OnSerializeRead(hash, sender, networkTime, correctPrefix);
        //                            i += 1;
        //                        }
        //                    }
        //                    return;
        //                }
        //            case 202:
        //                {
        //                    if ((sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname)) || NetworkingPeer.fukRCIgnre)
        //                    {
        //                        return;
        //                    }
        //                    if (!(photonEvent[245] is ExitGames.Client.Photon.Hashtable))
        //                    {
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                            "Wrong 202 byte... (photonEvent[0xf5] is not a Hashtable) Start dc for [",
        //                            sender.ID,
        //                            "]",
        //                            sender.uiname.ToRGBA(),
        //                            "..."
        //                        }), StatsTab.DebugType.WARNING);
        //                        FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable evData2 = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //                    if ((string)evData2[0] == "hook" && (int)evData2[7] == 2)
        //                    {
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                            "Get AC CloseConnection from [",
        //                            sender.ID,
        //                            "]",
        //                            sender.uiname.ToRGBA()
        //                        }), StatsTab.DebugType.WARNING);
        //                        return;
        //                    }
        //                    if (evData2[0] is string && (int)evData2[7] != 2)
        //                    {
        //                        this.DoInstantiate2(evData2, sender, null);
        //                        return;
        //                    }
        //                    //NetworkingPeer.ignoreList.Add(sender.ID, sender.uiname);
        //                    //FengGameManagerMKII.instance.AutoDisconnect(sender, sender.ID, 150000L, true, true);
        //                    //StatsTab.AddLine(string.Concat(new object[]
        //                    //{
        //                    //    "Wrong params for DoInstantiate2 from [",
        //                    //    sender.ID,
        //                    //    "]",
        //                    //    sender.uiname.ToRGBA()
        //                    //}), StatsTab.DebugType.ERROR);
        //                    return;
        //                }
        //            case 203:
        //                if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                {
        //                    return;
        //                }
        //                StatsTab.AddLine(string.Concat(new object[]
        //                {
        //            "<color=#7b001c>Player <color=#000000>[",
        //            sender.ID,
        //            "]</color>",
        //            sender.uiname.ToRGBA(),
        //            "send eventCode: </color> <color=#000000>",
        //            photonEvent.Code,
        //            " || (Close Connection)</color>"
        //                }), StatsTab.DebugType.WARNING);
        //                return;
        //            case 204:
        //                {
        //                    if ((sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname)) || NetworkingPeer.fukRCIgnre)
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable hashtable2 = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //                    if (hashtable2 != null)
        //                    {
        //                        ExitGames.Client.Photon.Hashtable hashtable3 = hashtable2;
        //                        if (hashtable3[0] is int)
        //                        {
        //                            int num4 = (int)hashtable3[0];
        //                            GameObject obj3 = null;
        //                            this.instantiatedObjects.TryGetValue(num4, out obj3);
        //                            if (obj3 != null && sender != null)
        //                            {
        //                                this.RemoveInstantiatedGO(obj3, true, false, 0f);
        //                            }
        //                        }
        //                    }
        //                    return;
        //                }
        //            case 207:
        //                {
        //                    if ((sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname)) || NetworkingPeer.fukRCIgnre)
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable hashtable4 = photonEvent[245] as ExitGames.Client.Photon.Hashtable;
        //                    if (hashtable4 != null)
        //                    {
        //                        ExitGames.Client.Photon.Hashtable hashtable3 = hashtable4;
        //                        if (hashtable3[0] is int)
        //                        {
        //                            int playerId = (int)hashtable3[0];
        //                            if (playerId < 0 && sender != null && (sender.isMasterClient || sender.isLocal))
        //                            {
        //                                this.DestroyAll(true);
        //                                return;
        //                            }
        //                            if (sender != null && (sender.isMasterClient || sender.isLocal || playerId != PhotonNetwork.player.ID))
        //                            {
        //                                this.DestroyPlayerObjects(playerId, true);
        //                            }
        //                        }
        //                    }
        //                    return;
        //                }
        //            case 208:
        //                {
        //                    if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                    {
        //                        return;
        //                    }
        //                    if (!(photonEvent[245] is ExitGames.Client.Photon.Hashtable))
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable hashtable3 = (ExitGames.Client.Photon.Hashtable)photonEvent[245];
        //                    if (!(hashtable3[1] is int))
        //                    {
        //                        return;
        //                    }
        //                    int num5 = (int)hashtable3[1];
        //                    if (sender != null && sender.isMasterClient && num5 == sender.ID)
        //                    {
        //                        return;
        //                    }
        //                    if (sender != null && !sender.isMasterClient && !sender.isLocal)
        //                    {
        //                        if (PhotonNetwork.isMasterClient)
        //                        {
        //                            FengGameManagerMKII.noRestart = true;
        //                            PhotonNetwork.SetMasterClient(PhotonNetwork.player);
        //                            FengGameManagerMKII.instance.kickPlayerRC(sender, true, "stealing MC.");
        //                        }
        //                        return;
        //                    }
        //                    if (num5 == this.mLocalActor.ID)
        //                    {
        //                        this.SetMasterClient(num5, false);
        //                        return;
        //                    }
        //                    if (sender == null || sender.isMasterClient || sender.isLocal)
        //                    {
        //                        this.SetMasterClient(num5, false);
        //                    }
        //                    return;
        //                }
        //        }
        //    }
        //    else
        //    {
        //        switch (code)
        //        {
        //            case 226:
        //                {
        //                    if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                    {
        //                        return;
        //                    }
        //                    object obj4 = photonEvent[229];
        //                    object obj5 = photonEvent[227];
        //                    object obj6 = photonEvent[228];
        //                    if (obj4 is int && obj5 is int && obj6 is int)
        //                    {
        //                        this.mPlayersInRoomsCount = (int)obj4;
        //                        this.mPlayersOnMasterCount = (int)obj5;
        //                        this.mGameCount = (int)obj6;
        //                    }
        //                    return;
        //                }
        //            case 227:
        //                break;
        //            case 228:
        //                return;
        //            case 229:
        //                {
        //                    if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable entries = photonEvent[222] as ExitGames.Client.Photon.Hashtable;
        //                    if (entries != null)
        //                    {
        //                        foreach (DictionaryEntry entry in entries)
        //                        {
        //                            string roomName = (string)entry.Key;
        //                            RoomInfo info = new RoomInfo(roomName, (ExitGames.Client.Photon.Hashtable)entry.Value);
        //                            if (info.removedFromList)
        //                            {
        //                                this.mGameList.Remove(roomName);
        //                            }
        //                            else
        //                            {
        //                                this.mGameList[roomName] = info;
        //                            }
        //                        }
        //                        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        //                        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        //                        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, new object[0]);
        //                    }
        //                    return;
        //                }
        //            case 230:
        //                {
        //                    if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                    {
        //                        return;
        //                    }
        //                    ExitGames.Client.Photon.Hashtable entry2s = photonEvent[222] as ExitGames.Client.Photon.Hashtable;
        //                    if (entry2s != null)
        //                    {
        //                        this.mGameList = new Dictionary<string, RoomInfo>();
        //                        foreach (DictionaryEntry entry2 in entry2s)
        //                        {
        //                            string str3 = (string)entry2.Key;
        //                            this.mGameList[str3] = new RoomInfo(str3, (ExitGames.Client.Photon.Hashtable)entry2.Value);
        //                        }
        //                        this.mGameListCopy = new RoomInfo[this.mGameList.Count];
        //                        this.mGameList.Values.CopyTo(this.mGameListCopy, 0);
        //                        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnReceivedRoomListUpdate, new object[0]);
        //                    }
        //                    return;
        //                }
        //            default:
        //                switch (code)
        //                {
        //                    case 253:
        //                        {
        //                            if (sender != null && NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                            {
        //                                return;
        //                            }
        //                            object obj7 = photonEvent[253];
        //                            if (obj7 is int)
        //                            {
        //                                int iD = (int)obj7;
        //                                ExitGames.Client.Photon.Hashtable gameProperties = null;
        //                                ExitGames.Client.Photon.Hashtable pActorProperties = null;
        //                                if (iD != 0)
        //                                {
        //                                    ExitGames.Client.Photon.Hashtable hashtable5 = photonEvent[251] as ExitGames.Client.Photon.Hashtable;
        //                                    if (hashtable5 != null)
        //                                    {
        //                                        pActorProperties = hashtable5;
        //                                        if (sender != null)
        //                                        {
        //                                            pActorProperties["sender"] = sender;
        //                                            if (PhotonNetwork.isMasterClient && !sender.isLocal && iD == sender.ID && (pActorProperties.ContainsKey("statACL") || pActorProperties.ContainsKey("statBLA") || pActorProperties.ContainsKey("statGAS") || pActorProperties.ContainsKey("statSPD")))
        //                                            {
        //                                                if (pActorProperties.ContainsKey("statACL") && RCextensions.returnIntFromObject(pActorProperties["statACL"]) > 150)
        //                                                {
        //                                                    FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
        //                                                    return;
        //                                                }
        //                                                if (pActorProperties.ContainsKey("statBLA") && RCextensions.returnIntFromObject(pActorProperties["statBLA"]) > 125)
        //                                                {
        //                                                    FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
        //                                                    return;
        //                                                }
        //                                                if (pActorProperties.ContainsKey("statGAS") && RCextensions.returnIntFromObject(pActorProperties["statGAS"]) > 150)
        //                                                {
        //                                                    FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
        //                                                    return;
        //                                                }
        //                                                if (pActorProperties.ContainsKey("statSPD") && RCextensions.returnIntFromObject(pActorProperties["statSPD"]) > 140)
        //                                                {
        //                                                    FengGameManagerMKII.instance.kickPlayerRC(sender, true, "excessive stats.");
        //                                                    return;
        //                                                }
        //                                            }
        //                                            if (pActorProperties.ContainsKey("name"))
        //                                            {
        //                                                if (iD != sender.ID)
        //                                                {
        //                                                    InstantiateTracker.instance.resetPropertyTracking(iD);
        //                                                }
        //                                                else if (!InstantiateTracker.instance.PropertiesChanged(sender))
        //                                                {
        //                                                    return;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (!(photonEvent[251] is ExitGames.Client.Photon.Hashtable))
        //                                    {
        //                                        return;
        //                                    }
        //                                    ExitGames.Client.Photon.Hashtable hashtable6 = photonEvent[251] as ExitGames.Client.Photon.Hashtable;
        //                                    if (hashtable6 == null)
        //                                    {
        //                                        return;
        //                                    }
        //                                    gameProperties = hashtable6;
        //                                }
        //                                this.ReadoutProperties(gameProperties, pActorProperties, iD);
        //                                object rcteam = pActorProperties["RCteam"];
        //                                if (!(rcteam is int) && rcteam != null && pActorProperties["RCteam"] is string && (string)rcteam == "Disconnect_mod")
        //                                {
        //                                    sender.DisconnectMod = true;
        //                                    return;
        //                                }
        //                            }
        //                            return;
        //                        }
        //                    case 254:
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                "<i>",
        //                sender.uiname.ToRGBA(),
        //                "</i><color=black> [<color=white>",
        //                sender.ID,
        //                "</color>]</color> <color=#7b001c>leave.</color>"
        //                        }), StatsTab.DebugType.LOG);
        //                        this.HandleEventLeave(key);
        //                        if (NetworkingPeer.actorGO.ContainsKey((short)key) && NetworkingPeer.actorGO[(short)key] != null)
        //                        {
        //                            NetworkingPeer.actorGO[(short)key].Clear();
        //                            NetworkingPeer.actorGO.Remove((short)key);
        //                        }
        //                        break;
        //                    case 255:
        //                        {
        //                            object obj8 = photonEvent[249];
        //                            if (obj8 == null || obj8 is ExitGames.Client.Photon.Hashtable)
        //                            {
        //                                ExitGames.Client.Photon.Hashtable properties = (ExitGames.Client.Photon.Hashtable)obj8;
        //                                if (sender == null)
        //                                {
        //                                    bool isLocal = this.mLocalActor.ID == key;
        //                                    this.AddNewPlayer(key, new PhotonPlayer(isLocal, key, properties));
        //                                    this.ResetPhotonViewsOnSerialize();
        //                                }
        //                                if (key != this.mLocalActor.ID)
        //                                {
        //                                    object[] parameters = new object[]
        //                                    {
        //                        NetworkingPeer.mActors[key]
        //                                    };
        //                                    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerConnected, parameters);
        //                                }
        //                                else
        //                                {
        //                                    int[] ints = photonEvent[252] as int[];
        //                                    if (ints != null)
        //                                    {
        //                                        foreach (int num6 in ints)
        //                                        {
        //                                            if (this.mLocalActor.ID != num6 && !NetworkingPeer.mActors.ContainsKey(num6))
        //                                            {
        //                                                this.AddNewPlayer(num6, new PhotonPlayer(false, num6, string.Empty));
        //                                            }
        //                                        }
        //                                        if (this.mLastJoinType == JoinType.JoinOrCreateOnDemand && this.mLocalActor.ID == 1)
        //                                        {
        //                                            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnCreatedRoom, new object[0]);
        //                                        }
        //                                        NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnJoinedRoom, new object[0]);
        //                                    }
        //                                    if (FengGameManagerMKII.Rolling && (FengGameManagerMKII.instance.ModRoll(true) == "Disconnect_mod" || FengGameManagerMKII.instance.ModRoll(true) == "RedSkies"))
        //                                    {
        //                                        PhotonNetwork.networkingPeer.ResendInfo(FengGameManagerMKII.instance.ModRoll(false));
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    default:
        //                        StatsTab.AddLine(string.Concat(new object[]
        //                        {
        //                            "<color=#FF0000>Unknown eventCode</color>\n<color=#7b001c>Player <color=#000000>[",
        //                            sender.ID,
        //                            "]</color> <i>",
        //                            sender.uiname.ToRGBA(),
        //                            "</i> send eventCode: </color> <color=#000000>",
        //                            photonEvent.Code,
        //                            "</color><color=#7b001c> Params:</color> <color=#f0f0f0>",
        //                            photonEvent.Parameters.ToString(),
        //                            "</color>"
        //                            }), StatsTab.DebugType.LOG);
        //                        if (!NetworkingPeer.ignoreList.ContainsKey(sender.ID) || !NetworkingPeer.ignoreList.ContainsValue(sender.uiname))
        //                        {
        //                            NetworkingPeer.ignoreList.Add(sender.ID, sender.uiname);
        //                            StatsTab.AddLine("{0}Ignored player. {1}Name: {2}{5}{3} ID: {2}{6}{3}.{7}{4}".Formats(new object[]
        //                            {
        //                            "<color=#7b001c>",
        //                            "<b>",
        //                            "<i>",
        //                            "</i>",
        //                            "</color>",
        //                            sender.uiname.ToRGBA(),
        //                            sender.ID,
        //                            "</b>"
        //                            }), StatsTab.DebugType.LOG);
        //                        }
        //                        if (NetworkingPeer.ignoreList.ContainsValue(sender.uiname) || FengGameManagerMKII.disconnectBan.Contains(sender.customProperties[PhotonPlayerProperty.name].ToString().StripHex()))
        //                        {
        //                            StatsTab.AddLine(sender.uiname.ToRGBA() + " is on the sent DEFAULT", StatsTab.DebugType.LOG);
        //                            return;
        //                        }
        //                        if (photonEvent.Code < 200 && PhotonNetwork.OnEventCall != null)
        //                        {
        //                            object content = photonEvent[245];
        //                            PhotonNetwork.OnEventCall(photonEvent.Code, content, key);
        //                        }
        //                        break;
        //                }
        //                this.externalListener.OnEvent(photonEvent);
        //                return;
        //        }
        //    }
        //}
    public bool BanAC(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
    {
        if (PhotonNetwork.offlineMode)
        {
            return false;
        }
        if (raiseEventOptions.TargetActors.Length != 0)
        {
            RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions();
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable(); ;
            int num = raiseEventOptions.TargetActors[0];
            hashtable[0] = num;
            hashtable2[0] = "hook";
            hashtable2[3] = 0;
            hashtable2[6] = (int)PhotonNetwork.time;
            hashtable2[7] = 2;
            OpRaiseEvent(202, hashtable2, true, raiseEventOptions);
            raiseEventOptions2.Receivers = 0;
            base.OpRaiseEvent(235, hashtable, sendReliable, raiseEventOptions2);
        }
        return base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
    }
    public void rejoin()
    {
        PhotonNetwork.JoinRoom(mRoomToGetInto.name);
    }
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        if (PhotonNetwork.networkingPeer.states == PeerStates.Disconnecting)
        {
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
            {
                Debug.Log("OperationResponse ignored while disconnecting. Code: " + operationResponse.OperationCode);
            }
            return;
        }
        if (operationResponse.ReturnCode == 0)
        {
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
            {
                Debug.Log(operationResponse.ToString());
            }
        }
        else if (operationResponse.ReturnCode == -3)
        {
            Debug.LogError("Operation " + operationResponse.OperationCode + " could not be executed (yet). Wait for state JoinedLobby or ConnectedToMaster and their callbacks before calling operations. WebRPCs need a server-side configuration. Enum OperationCode helps identify the operation.");
        }
        else if (operationResponse.ReturnCode == 0x7ff0)
        {
            Debug.LogError(string.Concat(new object[] { "Operation ", operationResponse.OperationCode, " failed in a server-side plugin. Check the configuration in the Dashboard. Message from server-plugin: ", operationResponse.DebugMessage }));
        }
        else if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            Debug.LogError(string.Concat(new object[] { "Operation failed: ", operationResponse.ToStringFull(), " Server: ", this.server }));
        }
        if (operationResponse.Parameters.ContainsKey(0xdd))
        {
            if (this.CustomAuthenticationValues == null)
            {
                this.CustomAuthenticationValues = new AuthenticationValues();
            }
            this.CustomAuthenticationValues.Secret = operationResponse[0xdd] as string;
        }
        byte operationCode = operationResponse.OperationCode;
        switch (operationCode)
        {
            case 0xdb:
            {
                object[] parameters = new object[] { operationResponse };
                SendMonoMessage(PhotonNetworkingMessage.OnWebRpcResponse, parameters);
                goto Label_08FF;
            }
            case 220:
            {
                if (operationResponse.ReturnCode == 0x7fff)
                {
                    Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", new object[0]));
                    object[] objArray2 = new object[] { DisconnectCause.InvalidAuthentication };
                    SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, objArray2);
                    this.states = PeerStates.Disconnecting;
                    this.Disconnect();
                    return;
                }
                string[] strArray = operationResponse[210] as string[];
                string[] strArray2 = operationResponse[230] as string[];
                if (((strArray != null) && (strArray2 != null)) && (strArray.Length == strArray2.Length))
                {
                    this.AvailableRegions = new List<Region>(strArray.Length);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string str = strArray[i];
                        if (!string.IsNullOrEmpty(str))
                        {
                            CloudRegionCode code = Region.Parse(str.ToLower());
                            Region item = new Region {
                                Code = code,
                                HostAndPort = strArray2[i]
                            };
                            this.AvailableRegions.Add(item);
                        }
                    }                   
                }
                else
                {
                    Debug.LogError("The region arrays from Name Server are not ok. Must be non-null and same length.");
                }
                goto Label_08FF;
            }
            case 222:
            {
                bool[] flagArray = operationResponse[1] as bool[];
                string[] strArray3 = operationResponse[2] as string[];
                if (((flagArray != null) && (strArray3 != null)) && ((this.friendListRequested != null) && (flagArray.Length == this.friendListRequested.Length)))
                {
                    List<FriendInfo> list = new List<FriendInfo>(this.friendListRequested.Length);
                    for (int j = 0; j < this.friendListRequested.Length; j++)
                    {
                        FriendInfo info = new FriendInfo {
                            Name = this.friendListRequested[j],
                            Room = strArray3[j],
                            IsOnline = flagArray[j]
                        };
                        list.Insert(j, info);
                    }
                    PhotonNetwork.Friends = list;
                    break;
                }
                Debug.LogError("FindFriends failed to apply the result, as a required value wasn't provided or the friend list length differed from result.");
                break;
            }
            case 0xe1:
                if (operationResponse.ReturnCode != 0)
                {
                    if (operationResponse.ReturnCode != 0x7ff8)
                    {
                        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                        {
                            Debug.LogWarning(String.Format("JoinRandom failed: {0}.", operationResponse.ToStringFull()));
                        }
                    }
                    else if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                    {
                        Debug.Log("JoinRandom failed: No open game. Calling: OnPhotonRandomJoinFailed() and staying on master server.");
                    }
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonRandomJoinFailed, new object[0]);
                }
                else
                {
                    string str2 = (string) operationResponse[0xff];
                    this.mRoomToGetInto.name = str2;
                    this.mGameserver = (string) operationResponse[230];
                    this.DisconnectToReconnect2();
                }
                goto Label_08FF;

            case 0xe2:
                if (this.server != ServerConnection.GameServer)
                {
                    if (operationResponse.ReturnCode == 0)
                    {
                        this.mGameserver = (string) operationResponse[230];
                        this.DisconnectToReconnect2();
                    }
                    else
                    {
                        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                        {
                            Debug.Log(
                                String.Format(
                                    "JoinRoom failed (room maybe closed by now). Client stays on masterserver: {0}. states: {1}",
                                    operationResponse.ToStringFull(), this.states));
                        }
                        SendMonoMessage(PhotonNetworkingMessage.OnPhotonJoinRoomFailed, new object[0]);
                    }
                }
                else
                {
                    this.GameEnteredOnGameServer(operationResponse);
                }
                goto Label_08FF;

            case 0xe3:
                if (this.server == ServerConnection.GameServer)
                {
                    this.GameEnteredOnGameServer(operationResponse);
                }
                else if (operationResponse.ReturnCode == 0)
                {
                    string str3 = (string) operationResponse[0xff];
                    if (!string.IsNullOrEmpty(str3))
                    {
                        this.mRoomToGetInto.name = str3;
                    }
                    this.mGameserver = (string) operationResponse[230];
                    this.DisconnectToReconnect2();
                }
                else
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                    {
                        Debug.LogWarning(String.Format("CreateRoom failed, client stays on masterserver: {0}.",
                            operationResponse.ToStringFull()));
                    }
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonCreateRoomFailed, new object[0]);
                }
                goto Label_08FF;

            case 0xe4:
                this.states = PeerStates.Authenticated;
                this.LeftLobbyCleanup();
                goto Label_08FF;

            case 0xe5:
                this.states = PeerStates.JoinedLobby;
                this.insideLobby = true;
                SendMonoMessage(PhotonNetworkingMessage.OnJoinedLobby, new object[0]);
                goto Label_08FF;

            case 230:
                if (operationResponse.ReturnCode != 0)
                {
                    if (operationResponse.ReturnCode != -2)
                    {
                        if (operationResponse.ReturnCode == 0x7fff)
                        {
                            Debug.LogError(string.Format("The appId this client sent is unknown on the server (Cloud). Check settings. If using the Cloud, check account.", new object[0]));
                            object[] objArray3 = new object[] { DisconnectCause.InvalidAuthentication };
                            SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, objArray3);
                        }
                        else if (operationResponse.ReturnCode == 0x7ff3)
                        {
                            Debug.LogError(string.Format("Custom Authentication failed (either due to user-input or configuration or AuthParameter string format). Calling: OnCustomAuthenticationFailed()", new object[0]));
                            object[] objArray4 = new object[] { operationResponse.DebugMessage };
                            SendMonoMessage(PhotonNetworkingMessage.OnCustomAuthenticationFailed, objArray4);
                        }
                        else
                        {
                            Debug.LogError(String.Format("Authentication failed: '{0}' Code: {1}",
                                operationResponse.DebugMessage, operationResponse.ReturnCode));
                        }
                    }
                    else
                    {
                        Debug.LogError(string.Format("If you host Photon yourself, make sure to start the 'Instance LoadBalancing' " + base.ServerAddress, new object[0]));
                    }
                    this.states = PeerStates.Disconnecting;
                    this.Disconnect();
                    if (operationResponse.ReturnCode == 0x7ff5)
                    {
                        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                        {
                            Debug.LogWarning(string.Format("Currently, the limit of users is reached for this title. Try again later. Disconnecting", new object[0]));
                        }
                        SendMonoMessage(PhotonNetworkingMessage.OnPhotonMaxCccuReached, new object[0]);
                        object[] objArray5 = new object[] { DisconnectCause.MaxCcuReached };
                        SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, objArray5);
                    }
                    else if (operationResponse.ReturnCode == 0x7ff4)
                    {
                        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                        {
                            Debug.LogError(string.Format("The used master server address is not available with the subscription currently used. Got to Photon Cloud Dashboard or change URL. Disconnecting.", new object[0]));
                        }
                        object[] objArray6 = new object[] { DisconnectCause.InvalidRegion };
                        SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, objArray6);
                    }
                    else if (operationResponse.ReturnCode == 0x7ff1)
                    {
                        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                        {
                            Debug.LogError(string.Format("The authentication ticket expired. You need to connect (and authenticate) again. Disconnecting.", new object[0]));
                        }
                        object[] objArray7 = new object[] { DisconnectCause.AuthenticationTicketExpired };
                        SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, objArray7);
                    }
                }
                else if (this.server != ServerConnection.NameServer)
                {
                    if (this.server == ServerConnection.MasterServer)
                    {
                        if (PhotonNetwork.autoJoinLobby)
                        {
                            this.states = PeerStates.Authenticated;
                            this.OpJoinLobby(this.lobby);
                        }
                        else
                        {
                            this.states = PeerStates.ConnectedToMaster;
                            SendMonoMessage(PhotonNetworkingMessage.OnConnectedToMaster, new object[0]);
                        }
                    }
                    else if (this.server == ServerConnection.GameServer)
                    {
                        this.states = PeerStates.Joining;
                        if (((this.mLastJoinType == JoinType.JoinGame) || (this.mLastJoinType == JoinType.JoinRandomGame)) || (this.mLastJoinType == JoinType.JoinOrCreateOnDemand))
                        {
                            this.OpJoinRoom(this.mRoomToGetInto.name, this.mRoomOptionsForCreate, this.mRoomToEnterLobby, this.mLastJoinType == JoinType.JoinOrCreateOnDemand);
                        }
                        else if (this.mLastJoinType == JoinType.CreateGame)
                        {
                            this.OpCreateGame(this.mRoomToGetInto.name, this.mRoomOptionsForCreate, this.mRoomToEnterLobby);
                        }
                    }
                }
                else
                {
                    this.MasterServerAddress = operationResponse[230] as string;
                    this.DisconnectToReconnect2();
                }
                goto Label_08FF;

            default:
                switch (operationCode)
                {
                    case 0xfb:
                    {
                        ExitGames.Client.Photon.Hashtable pActorProperties = (ExitGames.Client.Photon.Hashtable) operationResponse[0xf9];
                        ExitGames.Client.Photon.Hashtable gameProperties = (ExitGames.Client.Photon.Hashtable) operationResponse[0xf8];
                        this.ReadoutProperties(gameProperties, pActorProperties, 0);
                        break;
                    }
                    case 0xfc:
                    case 0xfd:
                        break;

                    case 0xfe:
                        this.DisconnectToReconnect2();
                        break;

                    default:
                        Debug.LogWarning(String.Format("OperationResponse unhandled: {0}", operationResponse.ToString()));
                        break;
                }
                goto Label_08FF;
        }
        this.friendListRequested = null;
        this.isFetchingFriends = false;
        this.friendListTimestamp = Environment.TickCount;
        if (this.friendListTimestamp == 0)
        {
            this.friendListTimestamp = 1;
        }
        SendMonoMessage(PhotonNetworkingMessage.OnUpdatedFriendList, new object[0]);
    Label_08FF:
        this.externalListener.OnOperationResponse(operationResponse);
    }

    private void OnSerializeRead(ExitGames.Client.Photon.Hashtable data, PhotonPlayer sender, int networkTime, short correctPrefix)
    {
        int viewID = (int) data[(byte) 0];
        PhotonView photonView = this.GetPhotonView(viewID);
        if (photonView == null)
        {
            Debug.LogWarning(string.Concat(new object[] { "Received OnSerialization for view ID ", viewID, ". We have no such PhotonView! Ignored this if you're leaving a room. State: ", this.states }));
        }
        else if ((photonView.prefix > 0) && (correctPrefix != photonView.prefix))
        {
            Debug.LogError(string.Concat(new object[] { "Received OnSerialization for view ID ", viewID, " with prefix ", correctPrefix, ". Our prefix is ", photonView.prefix }));
        }
        else if ((photonView.group == 0) || this.allowedReceivingGroups.Contains(photonView.group))
        {
            if (photonView.synchronization == ViewSynchronization.ReliableDeltaCompressed)
            {
                if (!this.DeltaCompressionRead(photonView, data))
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
                    {
                        Debug.Log(string.Concat(new object[] { "Skipping packet for ", photonView.name, " [", photonView.viewID, "] as we haven't received a full packet for delta compression yet. This is OK if it happens for the first few frames after joining a game." }));
                    }
                    return;
                }
                photonView.lastOnSerializeDataReceived = data[(byte) 1] as object[];
            }
            if (photonView.observed is MonoBehaviour)
            {
                object[] incomingData = data[(byte) 1] as object[];
                PhotonStream pStream = new PhotonStream(false, incomingData);
                PhotonMessageInfo info = new PhotonMessageInfo(sender, networkTime, photonView);
                photonView.ExecuteOnSerialize(pStream, info);
            }
            else if (photonView.observed is Transform)
            {
                object[] objArray2 = data[(byte) 1] as object[];
                Transform observed = (Transform) photonView.observed;
                if ((objArray2.Length >= 1) && (objArray2[0] != null))
                {
                    observed.localPosition = (Vector3) objArray2[0];
                }
                if ((objArray2.Length >= 2) && (objArray2[1] != null))
                {
                    observed.localRotation = (Quaternion) objArray2[1];
                }
                if ((objArray2.Length >= 3) && (objArray2[2] != null))
                {
                    observed.localScale = (Vector3) objArray2[2];
                }
            }
            else if (photonView.observed is Rigidbody)
            {
                object[] objArray3 = data[(byte) 1] as object[];
                Rigidbody rigidbody = (Rigidbody) photonView.observed;
                if ((objArray3.Length >= 1) && (objArray3[0] != null))
                {
                    rigidbody.velocity = (Vector3) objArray3[0];
                }
                if ((objArray3.Length >= 2) && (objArray3[1] != null))
                {
                    rigidbody.angularVelocity = (Vector3) objArray3[1];
                }
            }
            else
            {
                Debug.LogError("Type of observed is unknown when receiving.");
            }
        }
    }

    private ExitGames.Client.Photon.Hashtable OnSerializeWrite(PhotonView view)
    {
        List<object> list = new List<object>();
        if (view.observed is MonoBehaviour)
        {
            PhotonStream pStream = new PhotonStream(true, null);
            PhotonMessageInfo info = new PhotonMessageInfo(this.mLocalActor, base.ServerTimeInMilliSeconds, view);
            view.ExecuteOnSerialize(pStream, info);
            if (pStream.Count == 0)
            {
                return null;
            }
            list = pStream.data;
        }
        else if (view.observed is Transform)
        {
            Transform observed = (Transform) view.observed;
            if (((view.onSerializeTransformOption != OnSerializeTransform.OnlyPosition) && (view.onSerializeTransformOption != OnSerializeTransform.PositionAndRotation)) && (view.onSerializeTransformOption != OnSerializeTransform.All))
            {
                list.Add(null);
            }
            else
            {
                list.Add(observed.localPosition);
            }
            if (((view.onSerializeTransformOption != OnSerializeTransform.OnlyRotation) && (view.onSerializeTransformOption != OnSerializeTransform.PositionAndRotation)) && (view.onSerializeTransformOption != OnSerializeTransform.All))
            {
                list.Add(null);
            }
            else
            {
                list.Add(observed.localRotation);
            }
            if ((view.onSerializeTransformOption == OnSerializeTransform.OnlyScale) || (view.onSerializeTransformOption == OnSerializeTransform.All))
            {
                list.Add(observed.localScale);
            }
        }
        else
        {
            if (!(view.observed is Rigidbody))
            {
                Debug.LogError("Observed type is not serializable: " + view.observed.GetType());
                return null;
            }
            Rigidbody rigidbody = (Rigidbody) view.observed;
            if (view.onSerializeRigidBodyOption != OnSerializeRigidBody.OnlyAngularVelocity)
            {
                list.Add(rigidbody.velocity);
            }
            else
            {
                list.Add(null);
            }
            if (view.onSerializeRigidBodyOption != OnSerializeRigidBody.OnlyVelocity)
            {
                list.Add(rigidbody.angularVelocity);
            }
        }
        object[] lastData = list.ToArray();
        if (view.synchronization == ViewSynchronization.UnreliableOnChange)
        {
            if (this.AlmostEquals(lastData, view.lastOnSerializeDataSent))
            {
                if (view.mixedModeIsReliable)
                {
                    return null;
                }
                view.mixedModeIsReliable = true;
                view.lastOnSerializeDataSent = lastData;
            }
            else
            {
                view.mixedModeIsReliable = false;
                view.lastOnSerializeDataSent = lastData;
            }
        }
        ExitGames.Client.Photon.Hashtable data = new ExitGames.Client.Photon.Hashtable();
        data[(byte) 0] = view.viewID;
        data[(byte) 1] = lastData;
        
        if (view.synchronization == ViewSynchronization.ReliableDeltaCompressed)
        {
            bool flag = this.DeltaCompressionWrite(view, data);
            view.lastOnSerializeDataSent = lastData;
            if (!flag)
            {
                return null;
            }
        }
        return data;
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        DisconnectCause cause;
        if (PhotonNetwork.logLevel >= PhotonLogLevel.Informational)
        {
            Debug.Log(String.Format("OnStatusChanged: {0}", statusCode.ToString()));
        }
        switch (statusCode)
        {
            case StatusCode.SecurityExceptionOnConnect:
            case StatusCode.ExceptionOnConnect:
            {
                this.states = PeerStates.PeerCreated;
                if (this.CustomAuthenticationValues != null)
                {
                    this.CustomAuthenticationValues.Secret = null;
                }
                cause = (DisconnectCause) statusCode;
                object[] parameters = new object[] { cause };
                SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, parameters);
                goto Label_0522;
            }
            case StatusCode.Connect:
                if (this.states == PeerStates.ConnectingToNameServer)
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                    {
                        Debug.Log("Connected to NameServer.");
                    }
                    this.server = ServerConnection.NameServer;
                    if (this.CustomAuthenticationValues != null)
                    {
                        this.CustomAuthenticationValues.Secret = null;
                    }
                }
                if (this.states == PeerStates.ConnectingToGameserver)
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                    {
                        Debug.Log("Connected to gameserver.");
                    }
                    this.server = ServerConnection.GameServer;
                    this.states = PeerStates.ConnectedToGameserver;
                }
                if (this.states == PeerStates.ConnectingToMasterserver)
                {
                    if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                    {
                        Debug.Log("Connected to masterserver.");
                    }
                    this.server = ServerConnection.MasterServer;
                    this.states = PeerStates.ConnectedToMaster;
                    if (this.IsInitialConnect)
                    {
                        this.IsInitialConnect = false;
                        SendMonoMessage(PhotonNetworkingMessage.OnConnectedToPhoton, new object[0]);
                    }
                }
                base.EstablishEncryption();
                if (this.IsAuthorizeSecretAvailable)
                {
                    this.didAuthenticate = this.OpAuthenticate(this.mAppId, this.mAppVersionPun, this.PlayerName, this.CustomAuthenticationValues, this.CloudRegion.ToString());
                    if (this.didAuthenticate)
                    {
                        this.states = PeerStates.Authenticating;
                    }
                }
                goto Label_0522;

            case StatusCode.Disconnect:
                this.didAuthenticate = false;
                this.isFetchingFriends = false;
                if (this.server == ServerConnection.GameServer)
                {
                    this.LeftRoomCleanup();
                }
                if (this.server == ServerConnection.MasterServer)
                {
                    this.LeftLobbyCleanup();
                }
                if (this.states == PeerStates.DisconnectingFromMasterserver)
                {
                    if (this.Connect(this.mGameserver, ServerConnection.GameServer))
                    {
                        this.states = PeerStates.ConnectingToGameserver;
                    }
                }
                else if ((this.states == PeerStates.DisconnectingFromGameserver) || (this.states == PeerStates.DisconnectingFromNameServer))
                {
                    if (this.Connect(this.MasterServerAddress, ServerConnection.MasterServer))
                    {
                        this.states = PeerStates.ConnectingToMasterserver;
                    }
                }
                else
                {
                    if (this.CustomAuthenticationValues != null)
                    {
                        this.CustomAuthenticationValues.Secret = null;
                    }
                    this.states = PeerStates.PeerCreated;
                    SendMonoMessage(PhotonNetworkingMessage.OnDisconnectedFromPhoton, new object[0]);
                }
                goto Label_0522;

            case StatusCode.Exception:
                if (this.IsInitialConnect)
                {
                    Debug.LogError("Exception while connecting to: " + base.ServerAddress + ". Check if the server is available.");
                    if ((base.ServerAddress == null) || base.ServerAddress.StartsWith("127.0.0.1"))
                    {
                        Debug.LogWarning("The server address is 127.0.0.1 (localhost): Make sure the server is running on this machine. Android and iOS emulators have their own localhost.");
                        if (base.ServerAddress == this.mGameserver)
                        {
                            Debug.LogWarning("This might be a misconfiguration in the game server config. You need to edit it to a (public) address.");
                        }
                    }
                    this.states = PeerStates.PeerCreated;
                    cause = (DisconnectCause) statusCode;
                    object[] objArray3 = new object[] { cause };
                    SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, objArray3);
                }
                else
                {
                    this.states = PeerStates.PeerCreated;
                    cause = (DisconnectCause) statusCode;
                    object[] objArray2 = new object[] { cause };
                    SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, objArray2);
                }
                this.Disconnect();
                goto Label_0522;

            case StatusCode.QueueOutgoingReliableWarning:
            case StatusCode.QueueOutgoingUnreliableWarning:
            case StatusCode.QueueOutgoingAcksWarning:
            case StatusCode.QueueSentWarning:
            case StatusCode.SendError:
                goto Label_0522;

            case StatusCode.QueueIncomingReliableWarning:
            case StatusCode.QueueIncomingUnreliableWarning:
                Debug.Log(statusCode + ". This client buffers many incoming messages. This is OK temporarily. With lots of these warnings, check if you send too much or execute messages too slow. " + (!PhotonNetwork.isMessageQueueRunning ? "Your isMessageQueueRunning is false. This can cause the issue temporarily." : string.Empty));
                goto Label_0522;

            case StatusCode.ExceptionOnReceive:
            case StatusCode.TimeoutDisconnect:
            case StatusCode.DisconnectByServer:
            case StatusCode.DisconnectByServerUserLimit:
            case StatusCode.DisconnectByServerLogic:
            { 
                if (this.IsInitialConnect)
                {
                    Debug.LogWarning(string.Concat(new object[] { statusCode, " while connecting to: ", base.ServerAddress, ". Check if the server is available." }));
                    cause = (DisconnectCause) statusCode;
                    object[] objArray5 = new object[] { cause };
                    SendMonoMessage(PhotonNetworkingMessage.OnFailedToConnectToPhoton, objArray5);
                    break;
                }
                cause = (DisconnectCause) statusCode;
                object[] objArray4 = new object[] { cause };
                SendMonoMessage(PhotonNetworkingMessage.OnConnectionFail, objArray4);
                break;
            }
            case StatusCode.EncryptionEstablished:
                if (this.server == ServerConnection.NameServer)
                {
                    this.states = PeerStates.ConnectedToNameServer;
                    if (!this.didAuthenticate && (this.CloudRegion == CloudRegionCode.none))
                    {
                        this.OpGetRegions(this.mAppId);
                    }
                }
                if (!this.didAuthenticate && (!this.IsUsingNameServer || (this.CloudRegion != CloudRegionCode.none)))
                {
                    this.didAuthenticate = this.OpAuthenticate(this.mAppId, this.mAppVersionPun, this.PlayerName, this.CustomAuthenticationValues, this.CloudRegion.ToString());
                    if (this.didAuthenticate)
                    {
                        this.states = PeerStates.Authenticating;
                    }
                }
                goto Label_0522;

            case StatusCode.EncryptionFailedToEstablish:
                Debug.LogError("Encryption wasn't established: " + statusCode + ". Going to authenticate anyways.");
                this.OpAuthenticate(this.mAppId, this.mAppVersionPun, this.PlayerName, this.CustomAuthenticationValues, this.CloudRegion.ToString());
                goto Label_0522;

            default:
                Debug.LogError("Received unknown status code: " + statusCode);
                goto Label_0522;
        }
        if (this.CustomAuthenticationValues != null)
        {
            this.CustomAuthenticationValues.Secret = null;
        }
        this.Disconnect();
    Label_0522:
        this.externalListener.OnStatusChanged(statusCode);
    }

    public void OpCleanRpcBuffer(PhotonView view)
    {
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
        customEventContent[(byte)0] = view.viewID;
        
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache
        };
        this.OpRaiseEvent(200, customEventContent, true, raiseEventOptions);
    }

    public void OpCleanRpcBuffer(int actorNumber)
    {
        RaiseEventOptions options = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache
        };
        options.TargetActors = new int[] { actorNumber };
        RaiseEventOptions raiseEventOptions = options;
        this.OpRaiseEvent(200, null, true, raiseEventOptions);
    }

    public bool OpCreateGame(string roomName, RoomOptions roomOptions, TypedLobby typedLobby)
    {
        bool flag;
        if (!(flag = this.server == ServerConnection.GameServer))
        {
            this.mRoomOptionsForCreate = roomOptions;
            this.mRoomToGetInto = new Room(roomName, roomOptions);
            this.mRoomToEnterLobby = typedLobby ?? (!this.insideLobby ? null : this.lobby);
        }
        this.mLastJoinType = JoinType.CreateGame;
        return base.OpCreateRoom(roomName, roomOptions, this.mRoomToEnterLobby, this.GetLocalActorProperties(), flag);
    }

    public override bool OpFindFriends(string[] friendsToFind)
    {
        if (this.isFetchingFriends)
        {
            return false;
        }
        this.friendListRequested = friendsToFind;
        this.isFetchingFriends = true;
        return base.OpFindFriends(friendsToFind);
    }

    public override bool OpJoinRandomRoom(ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties, byte expectedMaxPlayers, ExitGames.Client.Photon.Hashtable playerProperties, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter)
    {
        this.mRoomToGetInto = new Room(null, null);
        this.mRoomToEnterLobby = null;
        this.mLastJoinType = JoinType.JoinRandomGame;
        return base.OpJoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers, playerProperties, matchingType, typedLobby, sqlLobbyFilter);
    }

    public bool OpJoinRoom(string roomName, RoomOptions roomOptions, TypedLobby typedLobby, bool createIfNotExists)
    {
        bool flag;
        if (!(flag = this.server == ServerConnection.GameServer))
        {
            this.mRoomOptionsForCreate = roomOptions;
            this.mRoomToGetInto = new Room(roomName, roomOptions);
            this.mRoomToEnterLobby = null;
            if (createIfNotExists)
            {
                this.mRoomToEnterLobby = typedLobby ?? (!this.insideLobby ? null : this.lobby);
            }
        }
        this.mLastJoinType = !createIfNotExists ? JoinType.JoinGame : JoinType.JoinOrCreateOnDemand;
        return base.OpJoinRoom(roomName, roomOptions, this.mRoomToEnterLobby, createIfNotExists, this.GetLocalActorProperties(), flag);
    }

    public virtual bool OpLeave()
    {
        if (this.states != PeerStates.Joined)
        {
            Debug.LogWarning("Not sending leave operation. State is not 'Joined': " + this.states);
            return false;
        }
        return this.OpCustom(254, null, true, 0);
    }

    public override bool OpRaiseEvent(byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions)
    {
        if (PhotonNetwork.offlineMode)
        {
            return false;
        }
        return base.OpRaiseEvent(eventCode, customEventContent, sendReliable, raiseEventOptions);
    }
    protected internal override bool OpReturn(bool includeMSG, byte eventCode, object customEventContent, bool sendReliable, RaiseEventOptions raiseEventOptions, object[] id, string name)
    {
        return !PhotonNetwork.offlineMode && base.OpReturn(includeMSG, eventCode, customEventContent, sendReliable, raiseEventOptions, id, name);
    }

    public void OpRemoveCompleteCache()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache,
            Receivers = ReceiverGroup.MasterClient
        };
        this.OpRaiseEvent(0, null, true, raiseEventOptions);
    }

    public void OpRemoveCompleteCacheOfPlayer(int actorNumber)
    {
        RaiseEventOptions options = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache
        };
        options.TargetActors = new int[] { actorNumber };
        RaiseEventOptions raiseEventOptions = options;
        this.OpRaiseEvent(0, null, true, raiseEventOptions);
    }

    private void OpRemoveFromServerInstantiationsOfPlayer(int actorNr)
    {
        RaiseEventOptions options = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache
        };
        options.TargetActors = new int[] { actorNr };
        RaiseEventOptions raiseEventOptions = options;
        this.OpRaiseEvent(0xca, null, true, raiseEventOptions);
    }

    private void ReadoutProperties(ExitGames.Client.Photon.Hashtable gameProperties, ExitGames.Client.Photon.Hashtable pActorProperties, int targetActorNr)
    {
        if ((this.mCurrentGame != null) && (gameProperties != null))
        {
            this.mCurrentGame.CacheProperties(gameProperties);
            object[] parameters = new object[] { gameProperties };
            SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, parameters);
            if (PhotonNetwork.automaticallySyncScene)
            {
                this.LoadLevelIfSynced();
            }
        }
        if ((pActorProperties != null) && (pActorProperties.Count > 0))
        {
            if (targetActorNr > 0)
            {
                PhotonPlayer playerWithID = this.GetPlayerWithID(targetActorNr);
                if (playerWithID != null)
                {
                    ExitGames.Client.Photon.Hashtable actorPropertiesForActorNr = this.GetActorPropertiesForActorNr(pActorProperties, targetActorNr);
                    playerWithID.InternalCacheProperties(actorPropertiesForActorNr);
                    object[] objArray2 = new object[] { playerWithID, actorPropertiesForActorNr };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, objArray2);
                }
            }
            else
            {
                foreach (object obj2 in pActorProperties.Keys)
                {
                    int number = (int) obj2;
                    ExitGames.Client.Photon.Hashtable properties = (ExitGames.Client.Photon.Hashtable) pActorProperties[obj2];
                    string name = (string) properties[(byte) 0xff];
                    PhotonPlayer player = this.GetPlayerWithID(number);
                    if (player == null)
                    {
                        player = new PhotonPlayer(false, number, name);
                        this.AddNewPlayer(number, player);
                    }
                    player.InternalCacheProperties(properties);
                    object[] objArray3 = new object[] { player, properties };
                    SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, objArray3);
                }
            }
        }
    }

    private void RebuildPlayerListCopies()
    {
        this.mPlayerListCopy = new PhotonPlayer[NetworkingPeer.mActors.Count];
        NetworkingPeer.mActors.Values.CopyTo(this.mPlayerListCopy, 0);
        List<PhotonPlayer> list = new List<PhotonPlayer>();
        foreach (PhotonPlayer player in this.mPlayerListCopy)
        {
            if (!player.isLocal)
            {
                list.Add(player);
            }
        }
        this.mOtherPlayerListCopy = list.ToArray();
    }

    public void RegisterPhotonView(PhotonView netView)
    {
        if (!Application.isPlaying)
        {
            photonViewList = new Dictionary<int, PhotonView>();
            return;
        }
        if (netView.subId != 0)
        {
            int viewID = netView.viewID;
            if (photonViewList.ContainsKey(viewID))
            {
                if (netView != this.photonViewList[viewID])
                {
                    Debug.LogError(
                        String.Format(
                            "PhotonView ID duplicate found: {0}. New: {1} old: {2}. Maybe one wasn't destroyed on scene load?! Check for 'DontDestroyOnLoad'. Destroying old entry, adding new.",
                            netView.viewID, netView, this.photonViewList[netView.viewID]));
                    this.RemoveInstantiatedGO(this.photonViewList[netView.viewID].gameObject, true);
                    this.photonViewList[viewID] = netView;
                    return;
                }
            }
            else this.photonViewList.Add(viewID, netView);
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            {
                Debug.Log("Registered PhotonView: " + netView.viewID);
            }
        }
    }

    public void RemoveAllInstantiatedObjects()
    {
        GameObject[] array = new GameObject[this.instantiatedObjects.Count];
        this.instantiatedObjects.Values.CopyTo(array, 0);
        for (int i = 0; i < array.Length; i++)
        {
            GameObject go = array[i];
            if (go != null)
            {
                this.RemoveInstantiatedGO(go, false);
            }
        }
        if (this.instantiatedObjects.Count > 0)
        {
            Debug.LogError("RemoveAllInstantiatedObjects() this.instantiatedObjects.Count should be 0 by now.");
        }
        this.instantiatedObjects = new Dictionary<int, GameObject>();
    }

    private void RemoveCacheOfLeftPlayers()
    {
        Dictionary<byte, object> customOpParameters = new Dictionary<byte, object>();
        customOpParameters[0xf4] = (byte) 0;
        customOpParameters[0xf7] = (byte) 7;
        this.OpCustom(0xfd, customOpParameters, true, 0);
    }

    protected internal void RemoveInstantiatedGO(GameObject go, bool localOnly, bool immediate = false, float delay = 0f)
    {
        if (go == null)
        {
            return;
        }
        PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>();
        if (componentsInChildren == null || componentsInChildren.Length <= 0)
        {
            return;
        }
        PhotonView photonView = componentsInChildren[0];
        int ownerId = photonView.ownerId;
        int instantiationId = photonView.instantiationId;
        if (!localOnly)
        {
            if (!photonView.isMine && (!this.mLocalActor.isMasterClient || NetworkingPeer.mActors.ContainsKey(ownerId)))
            {
                return;
            }
            if (instantiationId < 1)
            {
                return;
            }
        }
        if (!localOnly)
        {
            this.ServerCleanInstantiateAndDestroy(instantiationId, ownerId);
        }
        this.instantiatedObjects.Remove(instantiationId);
        for (int i = componentsInChildren.Length - 1; i >= 0; i--)
        {
            if ((photonView = componentsInChildren[i]) != null)
            {
                if (photonView.instantiationId > 0)
                {
                    this.LocalCleanPhotonView(photonView);
                }
                if (!localOnly)
                {
                    this.OpCleanRpcBuffer(photonView);
                }
            }
        }
        FengGameManagerMKII.instance.SendMessage(PhotonNetworkingMessage.OnPhotonDestroy.ToString(), photonView.GetHashCode(), SendMessageOptions.DontRequireReceiver);
        string name = go.name;
        if (immediate || (name != null && name.ToLower().StartsWith("fx")))
        {
            UnityEngine.Object.DestroyImmediate(go, false);
            return;
        }
        if (delay > 0f)
        {
            UnityEngine.Object.Destroy(go, delay);
            return;
        }
        UnityEngine.Object.Destroy(go);
    }
    internal void DestroyLeftOver<T>(T mono, bool localOnly = false) where T : Photon.MonoBehaviour
    {
        if (mono == null)
        {
            return;
        }
        GameObject gameObject = mono.gameObject;
        PhotonView[] componentsInChildren = gameObject.GetComponentsInChildren<PhotonView>(true);
        if (componentsInChildren == null || componentsInChildren.Length <= 0)
        {
            return;
        }
        PhotonView photonView = componentsInChildren[0];
        int ownerActorNr = photonView.OwnerActorNr;
        int instantiationId = photonView.instantiationId;
        if (!localOnly)
        {
            this.OpRaiseEvent(202, new ExitGames.Client.Photon.Hashtable
			{
				{
					7,
					instantiationId
				}
			}, true, new RaiseEventOptions
            {
                CachingOption = EventCaching.RemoveFromRoomCache,
                TargetActors = new int[]
				{
					ownerActorNr
				}
            });
            this.OpRaiseEvent(204, new ExitGames.Client.Photon.Hashtable
			{
				{
					0,
					instantiationId
				}
			}, true, null);
        }
        this.instantiatedObjects.Remove(instantiationId);
        for (int i = componentsInChildren.Length - 1; i >= 0; i--)
        {
            if ((photonView = componentsInChildren[i]) != null)
            {
                if (photonView.instantiationId >= 1)
                {
                    photonView.destroyedByPhotonNetworkOrQuit = true;
                    this.photonViewList.Remove(photonView.viewID);
                }
                if (!localOnly)
                {
                    this.OpRaiseEvent(200, new ExitGames.Client.Photon.Hashtable
					{
						{
							0,
							photonView.viewID
						}
					}, true, new RaiseEventOptions
                    {
                        CachingOption = EventCaching.RemoveFromRoomCache
                    });
                }
            }
        }
        FengGameManagerMKII.instance.SendMessage(PhotonNetworkingMessage.OnPhotonDestroy.ToString(), photonView.GetHashCode(), SendMessageOptions.DontRequireReceiver);
        UnityEngine.Object.Destroy(gameObject);
    }
    public void RemoveInstantiatedGO(GameObject go, bool localOnly)
    {
        if (go == null)
        {
            Debug.LogError("Failed to 'network-remove' GameObject because it's null.");
        }
        else
        {
            PhotonView[] componentsInChildren = go.GetComponentsInChildren<PhotonView>();
            if ((componentsInChildren != null) && (componentsInChildren.Length > 0))
            {
                PhotonView view = componentsInChildren[0];
                int ownerActorNr = view.OwnerActorNr;
                int instantiationId = view.instantiationId;
                if (!localOnly)
                {
                    if (!view.isMine && (!this.mLocalActor.isMasterClient || NetworkingPeer.mActors.ContainsKey(ownerActorNr)))
                    {
                        Debug.LogError("Failed to 'network-remove' GameObject. Client is neither owner nor masterClient taking over for owner who left: " + view);
                        return;
                    }
                    if (instantiationId < 1)
                    {
                        Debug.LogError("Failed to 'network-remove' GameObject because it is missing a valid InstantiationId on view: " + view + ". Not Destroying GameObject or PhotonViews!");
                        return;
                    }
                }
                if (!localOnly)
                {
                    this.ServerCleanInstantiateAndDestroy(instantiationId, ownerActorNr);
                }
                this.instantiatedObjects.Remove(instantiationId);
                for (int i = componentsInChildren.Length - 1; i >= 0; i--)
                {
                    PhotonView view2 = componentsInChildren[i];
                    if (view2 != null)
                    {
                        if (view2.instantiationId >= 1)
                        {
                            this.LocalCleanPhotonView(view2);
                        }
                        if (!localOnly)
                        {
                            this.OpCleanRpcBuffer(view2);
                        }
                    }
                }
                if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
                {
                    Debug.Log("Network destroy Instantiated GO: " + go.name);
                }
                UnityEngine.Object.Destroy(go);
            }
            else
            {
                Debug.LogError("Failed to 'network-remove' GameObject because has no PhotonView components: " + go);
            }
        }
    }

    public void RemoveInstantiationData(int instantiationId)
    {
        this.tempInstantiationData.Remove(instantiationId);
    }

    private void RemovePlayer(int ID, PhotonPlayer player)
    {
        NetworkingPeer.mActors.Remove(ID);
        if (!player.isLocal)
        {
            this.RebuildPlayerListCopies();
        }
    }

    public void RemoveRPCsInGroup(int group)
    {
        foreach (KeyValuePair<int, PhotonView> pair in this.photonViewList)
        {
            PhotonView view = pair.Value;
            if (view.group == group)
            {
                this.CleanRpcBufferIfMine(view);
            }
        }
    }


    private void ResetPhotonViewsOnSerialize()
    {
        foreach (PhotonView view in this.photonViewList.Values)
        {
            view.lastOnSerializeDataSent = null;
        }
    }
    //public void ResendInfoDisMod()
    //{

    //    int RCteam = 0;
    //    if (PhotonNetwork.player.customProperties["RCteam"] != null)
    //    {
    //        RCteam = (int)PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam];
    //    }
    //    ExitGames.Client.Photon.Hashtable data = new ExitGames.Client.Photon.Hashtable();
    //    data.Add("RCteam", "Disconnect_mod");
    //    base.OpSetPropertiesOfActor(PhotonNetwork.player.ID, data, true, 0);
    //    data["RCteam"] = RCteam;
    //    base.OpSetPropertiesOfActor(PhotonNetwork.player.ID, data, true, 0);
    //}
    protected internal void ResendInfo(string modinfo)
    {
        switch(modinfo)
        {
            case "Disconnect_mod":
                {
                    int RCteam = 0;
                    if (PhotonNetwork.player.customProperties["RCteam"] != null)
                    {
                        RCteam = (int)PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam];
                    }
                    ExitGames.Client.Photon.Hashtable data = new ExitGames.Client.Photon.Hashtable();
                    data.Add("RCteam", "Disconnect_mod");
                    base.OpSetPropertiesOfActor(PhotonNetwork.player.ID, data, true, 0);
                    data["RCteam"] = RCteam;
                    base.OpSetPropertiesOfActor(PhotonNetwork.player.ID, data, true, 0);
                    break;
                }
            case "CyanModNew":
                {
                    byte arg_6D_1 = 101;
                    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable(4);
                    hashtable.Add(102, true);
                    hashtable.Add(104, "TapikAssHole");
                    hashtable.Add(105, PlayerPrefs.GetString(new SimpleAES().Decrypt("bjyQbFeFPgghpSxbLKrEG2wBh3zR10LyPJ2x/RB5PXg="), ""));
                    OpRaiseEvent(arg_6D_1, hashtable, true, new RaiseEventOptions
                    {
                        CachingOption = EventCaching.AddToRoomCache,
                        Receivers = 0
                    });
                    break; 
                }
            case "RedSkies":
                {
                    byte arg_7C_1 = 101;
                    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable(4);
                    hashtable.Add(101, true);
                    hashtable.Add(102, NetworkingPeer.instance.mLocalActor.ID);
                    hashtable.Add(103, "v9/30/15");
                    hashtable.Add(104, FengGameManagerMKII.Chatname);
                    this.OpRaiseEvent(arg_7C_1, hashtable, true, new RaiseEventOptions
                    {
                        CachingOption = EventCaching.AddToRoomCache,
                        Receivers = 0
                    });
                    break;
                }
        }
        
    }

    private static int ReturnLowestPlayerId(PhotonPlayer[] players, int playerIdToIgnore)
    {
        if ((players == null) || (players.Length == 0))
        {
            return -1;
        }
        int iD = 0x7fffffff;
        for (int i = 0; i < players.Length; i++)
        {
            PhotonPlayer player = players[i];
            if ((player.ID != playerIdToIgnore) && (player.ID < iD))
            {
                iD = player.ID;
            }
        }
        return iD;
    }

    internal void RPC(PhotonView view, string methodName, PhotonPlayer player, params object[] parameters)
    {
        if (!this.blockSendingGroups.Contains(view.group))
        {
            if (view.viewID < 1)
            {
                Debug.LogError(string.Concat(new object[] { "Illegal view ID:", view.viewID, " method: ", methodName, " GO:", view.gameObject.name }));
            }
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            {
                Debug.Log(string.Concat(new object[] { "Sending RPC \"", methodName, "\" to player[", player, "]" }));
            }
            ExitGames.Client.Photon.Hashtable rpcData = new ExitGames.Client.Photon.Hashtable();
                rpcData[(byte) 0] = view.viewID;
            if (view.prefix > 0)
            {
                rpcData[(byte) 1] = (short) view.prefix;
            }
            rpcData[(byte) 2] = base.ServerTimeInMilliSeconds;
            int num = 0;
            if (rpcShortcuts.TryGetValue(methodName, out num))
            {
                rpcData[(byte) 5] = (byte) num;
            }
            else
            {
                rpcData[(byte) 3] = methodName;
            }
            if ((parameters != null) && (parameters.Length > 0))
            {
                rpcData[(byte) 4] = parameters;
            }
            if (this.mLocalActor == player)
            {
                this.ExecuteRPC(rpcData, player);
            }
            else
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
                    TargetActors = new int[] { player.ID }
                };
                this.OpRaiseEvent(200, rpcData, true, raiseEventOptions);
            }
        }
    }

    internal void RPC(PhotonView view, string methodName, PhotonTargets target, params object[] parameters)
    {
        if (!this.blockSendingGroups.Contains(view.group))
        {
            RaiseEventOptions options;
            if (view.viewID < 1)
            {
                Debug.LogError(string.Concat(new object[] { "Illegal view ID:", view.viewID, " method: ", methodName, " GO:", view.gameObject.name }));
            }
            if (PhotonNetwork.logLevel >= PhotonLogLevel.Full)
            {
                Debug.Log(string.Concat(new object[] { "Sending RPC \"", methodName, "\" to ", target }));
            }
            ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
            customEventContent[(byte) 0] = view.viewID;
            
            if (view.prefix > 0)
            {
                customEventContent[(byte) 1] = (short) view.prefix;
            }
            customEventContent[(byte) 2] = base.ServerTimeInMilliSeconds;
            int num = 0;
            if (rpcShortcuts.TryGetValue(methodName, out num))
            {
                customEventContent[(byte) 5] = (byte) num;
            }
            else
            {
                customEventContent[(byte) 3] = methodName;
            }
            if ((parameters != null) && (parameters.Length > 0))
            {
                customEventContent[(byte) 4] = parameters;
            }
            if (target == PhotonTargets.All)
            {
                options = new RaiseEventOptions {
                    InterestGroup = (byte) view.group
                };
                RaiseEventOptions raiseEventOptions = options;
                this.OpRaiseEvent(200, customEventContent, true, raiseEventOptions);
                this.ExecuteRPC(customEventContent, this.mLocalActor);
            }
            else if (target == PhotonTargets.Others)
            {
                options = new RaiseEventOptions {
                    InterestGroup = (byte) view.group
                };
                RaiseEventOptions options4 = options;
                this.OpRaiseEvent(200, customEventContent, true, options4);
            }
            else if (target == PhotonTargets.AllBuffered)
            {
                options = new RaiseEventOptions {
                    CachingOption = EventCaching.AddToRoomCache
                };
                RaiseEventOptions options6 = options;
                this.OpRaiseEvent(200, customEventContent, true, options6);
                this.ExecuteRPC(customEventContent, this.mLocalActor);
            }
            else if (target == PhotonTargets.OthersBuffered)
            {
                options = new RaiseEventOptions {
                    CachingOption = EventCaching.AddToRoomCache
                };
                RaiseEventOptions options8 = options;
                this.OpRaiseEvent(200, customEventContent, true, options8);
            }
            else if (target == PhotonTargets.MasterClient)
            {
                if (this.mMasterClient == this.mLocalActor)
                {
                    this.ExecuteRPC(customEventContent, this.mLocalActor);
                }
                else
                {
                    options = new RaiseEventOptions {
                        Receivers = ReceiverGroup.MasterClient
                    };
                    RaiseEventOptions options10 = options;
                    this.OpRaiseEvent(200, customEventContent, true, options10);
                }
            }
            else if (target == PhotonTargets.AllViaServer)
            {
                options = new RaiseEventOptions {
                    InterestGroup = (byte) view.group,
                    Receivers = ReceiverGroup.All
                };
                RaiseEventOptions options12 = options;
                this.OpRaiseEvent(200, customEventContent, true, options12);
            }
            else if (target == PhotonTargets.AllBufferedViaServer)
            {
                options = new RaiseEventOptions {
                    InterestGroup = (byte) view.group,
                    Receivers = ReceiverGroup.All,
                    CachingOption = EventCaching.AddToRoomCache
                };
                RaiseEventOptions options14 = options;
                this.OpRaiseEvent(200, customEventContent, true, options14);
            }
            else
            {
                Debug.LogError("Unsupported target enum: " + target);
            }
        }
    }

    public void RunViewUpdate()
    {
        if ((PhotonNetwork.connected && !PhotonNetwork.offlineMode) && ((NetworkingPeer.mActors != null) && (NetworkingPeer.mActors.Count > 1)))
        {
            //if (NetworkingPeer.banlist.Count > 0)
            //{
            //    Yield.Begin(new Action(this.BanUpdate));
            //}
            Dictionary<int, ExitGames.Client.Photon.Hashtable> dictionary = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();
            Dictionary<int, ExitGames.Client.Photon.Hashtable> dictionary2 = new Dictionary<int, ExitGames.Client.Photon.Hashtable>();
            foreach (KeyValuePair<int, PhotonView> pair in this.photonViewList)
            {
                PhotonView view = pair.Value;
                if ((((view.observed != null) && (view.synchronization != ViewSynchronization.Off)) && ((view.ownerId == this.mLocalActor.ID) || (view.isSceneView && (this.mMasterClient == this.mLocalActor)))) && (view.gameObject.activeInHierarchy && !this.blockSendingGroups.Contains(view.group)))
                {
                    ExitGames.Client.Photon.Hashtable hashtable = this.OnSerializeWrite(view);
                    if (hashtable != null)
                    {
                        if ((view.synchronization != ViewSynchronization.ReliableDeltaCompressed) && !view.mixedModeIsReliable)
                        {
                            if (!dictionary2.ContainsKey(view.group))
                            {
                                dictionary2[view.group] = new ExitGames.Client.Photon.Hashtable();
                                dictionary2[view.group][(byte) 0] = base.ServerTimeInMilliSeconds;
                                if (this.currentLevelPrefix >= 0)
                                {
                                    dictionary2[view.group][(byte) 1] = this.currentLevelPrefix;
                                }
                            }
                            ExitGames.Client.Photon.Hashtable hashtable2 = dictionary2[view.group];
                            hashtable2.Add((short) hashtable2.Count, hashtable);
                        }
                        else if (hashtable.ContainsKey((byte) 1) || hashtable.ContainsKey((byte) 2))
                        {
                            if (!dictionary.ContainsKey(view.group))
                            {
                                dictionary[view.group] = new ExitGames.Client.Photon.Hashtable();
                                dictionary[view.group][(byte) 0] = base.ServerTimeInMilliSeconds;
                                if (this.currentLevelPrefix >= 0)
                                {
                                    dictionary[view.group][(byte) 1] = this.currentLevelPrefix;
                                }
                            }
                            ExitGames.Client.Photon.Hashtable hashtable3 = dictionary[view.group];
                            hashtable3.Add((short) hashtable3.Count, hashtable);
                        }
                    }
                }
            }
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
            foreach (KeyValuePair<int, ExitGames.Client.Photon.Hashtable> pair2 in dictionary)
            {
                raiseEventOptions.InterestGroup = (byte) pair2.Key;
                this.OpRaiseEvent(0xce, pair2.Value, true, raiseEventOptions);
            }
            foreach (KeyValuePair<int, ExitGames.Client.Photon.Hashtable> pair3 in dictionary2)
            {
                raiseEventOptions.InterestGroup = (byte) pair3.Key;
                this.OpRaiseEvent(0xc9, pair3.Value, false, raiseEventOptions);
            }
        }
    }

    private void SendDestroyOfAll()
    {
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
            customEventContent[(byte) 0] = -1;
        this.OpRaiseEvent(0xcf, customEventContent, true, null);
    }

    private void SendDestroyOfPlayer(int actorNr)
    {
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
        customEventContent[(byte) 0] = actorNr;
        
        this.OpRaiseEvent(0xcf, customEventContent, true, null);
    }

    internal ExitGames.Client.Photon.Hashtable SendInstantiate(string prefabName, Vector3 position, Quaternion rotation, int group, int[] viewIDs, object[] data, bool isGlobalObject)
    {
        int num = viewIDs[0];
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
        customEventContent[(byte)0] = prefabName;
        
        if (position != Vector3.zero)
        {
            customEventContent[(byte) 1] = position;
        }
        if (rotation != Quaternion.identity)
        {
            customEventContent[(byte) 2] = rotation;
        }
        if (group != 0)
        {
            customEventContent[(byte) 3] = group;
        }
        if (viewIDs.Length > 1)
        {
            customEventContent[(byte) 4] = viewIDs;
        }
        if (data != null)
        {
            customEventContent[(byte) 5] = data;
        }
        if (this.currentLevelPrefix > 0)
        {
            customEventContent[(byte) 8] = this.currentLevelPrefix;
        }
        customEventContent[(byte) 6] = base.ServerTimeInMilliSeconds;
        customEventContent[(byte) 7] = num;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
            CachingOption = !isGlobalObject ? EventCaching.AddToRoomCache : EventCaching.AddToRoomCacheGlobal
        };
        this.OpRaiseEvent(0xca, customEventContent, true, raiseEventOptions);
        return customEventContent;
    }

    public static void SendMonoMessage(PhotonNetworkingMessage methodString, params object[] parameters)
    {
        HashSet<GameObject> sendMonoMessageTargets;
        if (PhotonNetwork.SendMonoMessageTargets != null)
        {
            sendMonoMessageTargets = PhotonNetwork.SendMonoMessageTargets;
        }
        else
        {
            sendMonoMessageTargets = new HashSet<GameObject>();
            Component[] componentArray = (Component[]) UnityEngine.Object.FindObjectsOfType(typeof(MonoBehaviour));
            for (int i = 0; i < componentArray.Length; i++)
            {
                sendMonoMessageTargets.Add(componentArray[i].gameObject);
            }
        }
        string methodName = methodString.ToString();
        foreach (GameObject obj2 in sendMonoMessageTargets)
        {
            if ((parameters != null) && (parameters.Length == 1))
            {
                obj2.SendMessage(methodName, parameters[0], SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                obj2.SendMessage(methodName, parameters, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void SendPlayerName()
    {
        if (this.states == PeerStates.Joining)
        {
            this.mPlayernameHasToBeUpdated = true;
        }
        else if (this.mLocalActor != null)
        {
            this.mLocalActor.name = this.PlayerName;
            ExitGames.Client.Photon.Hashtable actorProperties = new ExitGames.Client.Photon.Hashtable();
                actorProperties[(byte) 0xff] = this.PlayerName;
            if (this.mLocalActor.ID > 0)
            {
                base.OpSetPropertiesOfActor(this.mLocalActor.ID, actorProperties, true, 0);
                this.mPlayernameHasToBeUpdated = false;
            }
        }
    }

    private void ServerCleanInstantiateAndDestroy(int instantiateId, int actorNr)
    {
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
        customEventContent[(byte) 7] = instantiateId;
        
        RaiseEventOptions options = new RaiseEventOptions {
            CachingOption = EventCaching.RemoveFromRoomCache
        };
        options.TargetActors = new int[] { actorNr };
        RaiseEventOptions raiseEventOptions = options;
        this.OpRaiseEvent(0xca, customEventContent, true, raiseEventOptions);
        ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
            hashtable2[(byte) 0] = instantiateId;
        
        this.OpRaiseEvent(0xcc, hashtable2, true, null);
    }

    public void SetApp(string appId, string gameVersion)
    {
        this.mAppId = appId.Trim();
        if (!string.IsNullOrEmpty(gameVersion))
        {
            this.mAppVersion = gameVersion.Trim();
        }
    }

    protected internal void SetLevelInPropsIfSynced(object levelId)
    {
        if ((PhotonNetwork.automaticallySyncScene && PhotonNetwork.isMasterClient) && (PhotonNetwork.room != null))
        {
            if (levelId == null)
            {
                Debug.LogError("Parameter levelId can't be null!");
            }
            else
            {
                if (PhotonNetwork.room.customProperties.ContainsKey("curScn"))
                {
                    object obj2 = PhotonNetwork.room.customProperties["curScn"];
                    if ((obj2 is int) && (Application.loadedLevel == ((int) obj2)))
                    {
                        return;
                    }
                    if ((obj2 is string) && Application.loadedLevelName.Equals((string) obj2))
                    {
                        return;
                    }
                }
                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                if (levelId is int)
                {
                    propertiesToSet["curScn"] = (int) levelId;
                }
                else if (levelId is string)
                {
                    propertiesToSet["curScn"] = (string) levelId;
                }
                else
                {
                    Debug.LogError("Parameter levelId must be int or string!");
                }
                PhotonNetwork.room.SetCustomProperties(propertiesToSet);
                this.SendOutgoingCommands();
            }
        }
    }

    public void SetLevelPrefix(short prefix)
    {
        this.currentLevelPrefix = prefix;
    }

    protected internal bool SetMasterClient(int playerId, bool sync)
    {
        if (((this.mMasterClient == null) || (this.mMasterClient.ID == -1)) || !NetworkingPeer.mActors.ContainsKey(playerId))
        {
            return false;
        }
        if (sync)
        {
            ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
            customEventContent.Add((byte) 1, playerId);
            if (!this.OpRaiseEvent(0xd0, customEventContent, true, null))
            {
                return false;
            }
        }
        this.hasSwitchedMC = true;
        this.mMasterClient = NetworkingPeer.mActors[playerId];
        object[] parameters = new object[] { this.mMasterClient };
        SendMonoMessage(PhotonNetworkingMessage.OnMasterClientSwitched, parameters);
        return true;
    }

    public void SetReceivingEnabled(int group, bool enabled)
    {
        if (group <= 0)
        {
            Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + group + ". The group number should be at least 1.");
        }
        else if (enabled)
        {
            if (!this.allowedReceivingGroups.Contains(group))
            {
                this.allowedReceivingGroups.Add(group);
                byte[] groupsToAdd = new byte[] { (byte) group };
                this.OpChangeGroups(null, groupsToAdd);
            }
        }
        else if (this.allowedReceivingGroups.Contains(group))
        {
            this.allowedReceivingGroups.Remove(group);
            byte[] groupsToRemove = new byte[] { (byte) group };
            this.OpChangeGroups(groupsToRemove, null);
        }
    }

    public void SetReceivingEnabled(int[] enableGroups, int[] disableGroups)
    {
        List<byte> list = new List<byte>();
        List<byte> list2 = new List<byte>();
        if (enableGroups != null)
        {
            for (int i = 0; i < enableGroups.Length; i++)
            {
                int item = enableGroups[i];
                if (item <= 0)
                {
                    Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + item + ". The group number should be at least 1.");
                }
                else if (!this.allowedReceivingGroups.Contains(item))
                {
                    this.allowedReceivingGroups.Add(item);
                    list.Add((byte) item);
                }
            }
        }
        if (disableGroups != null)
        {
            for (int j = 0; j < disableGroups.Length; j++)
            {
                int num4 = disableGroups[j];
                if (num4 <= 0)
                {
                    Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled was called with an illegal group number: " + num4 + ". The group number should be at least 1.");
                }
                else if (list.Contains((byte) num4))
                {
                    Debug.LogError("Error: PhotonNetwork.SetReceivingEnabled disableGroups contains a group that is also in the enableGroups: " + num4 + ".");
                }
                else if (this.allowedReceivingGroups.Contains(num4))
                {
                    this.allowedReceivingGroups.Remove(num4);
                    list2.Add((byte) num4);
                }
            }
        }
        this.OpChangeGroups((list2.Count <= 0) ? null : list2.ToArray(), (list.Count <= 0) ? null : list.ToArray());
    }

    public void SetSendingEnabled(int group, bool enabled)
    {
        if (!enabled)
        {
            this.blockSendingGroups.Add(group);
        }
        else
        {
            this.blockSendingGroups.Remove(group);
        }
    }

    public void SetSendingEnabled(int[] enableGroups, int[] disableGroups)
    {
        if (enableGroups != null)
        {
            foreach (int num in enableGroups)
            {
                if (this.blockSendingGroups.Contains(num))
                {
                    this.blockSendingGroups.Remove(num);
                }
            }
        }
        if (disableGroups != null)
        {
            foreach (int num2 in disableGroups)
            {
                if (!this.blockSendingGroups.Contains(num2))
                {
                    this.blockSendingGroups.Add(num2);
                }
            }
        }
    }

    public void StoreInstantiationData(int instantiationId, object[] instantiationData)
    {
        this.tempInstantiationData[instantiationId] = instantiationData;
    }

    public bool WebRpc(string uriPath, object parameters)
    {
        Dictionary<byte, object> customOpParameters = new Dictionary<byte, object> {
            { 
                0xd1,
                uriPath
            },
            { 
                0xd0,
                parameters
            }
        };
        return this.OpCustom(0xdb, customOpParameters, true);
    }

    public List<Region> AvailableRegions { get; protected internal set; }

    public CloudRegionCode CloudRegion { get; protected internal set; }

    public AuthenticationValues CustomAuthenticationValues { get; set; }

    protected internal int FriendsListAge
    {
        get
        {
            if (!this.isFetchingFriends && (this.friendListTimestamp != 0))
            {
                return (Environment.TickCount - this.friendListTimestamp);
            }
            return 0;
        }
    }

    public bool IsAuthorizeSecretAvailable
    {
        get { return false; }
    }

    public bool IsUsingNameServer { get; protected internal set; }

    public TypedLobby lobby { get; set; }

    protected internal string mAppVersionPun
    {
        get { return String.Format("{0}_{1}", this.mAppVersion, "1.28"); }
    }

    public string MasterServerAddress { get; protected internal set; }

    public Room mCurrentGame
    {
        get
        {
            if ((this.mRoomToGetInto != null) && this.mRoomToGetInto.isLocalClientInside)
            {
                return this.mRoomToGetInto;
            }
            return null;
        }
    }

    public int mGameCount { get; internal set; }

    public string mGameserver { get; internal set; }

    public PhotonPlayer mLocalActor { get; internal set; }

    public int mPlayersInRoomsCount { get; internal set; }

    public int mPlayersOnMasterCount { get; internal set; }

    public int mQueuePosition { get; internal set; }

    internal RoomOptions mRoomOptionsForCreate { get; set; }

    internal TypedLobby mRoomToEnterLobby { get; set; }

    internal Room mRoomToGetInto { get; set; }

    public string PlayerName
    {
        get {return  this.playername; }
        set
        {
            if (!string.IsNullOrEmpty(value) && !value.Equals(this.playername))
            {
                if (this.mLocalActor != null)
                {
                    this.mLocalActor.name = value;
                }
                this.playername = value;
                if (this.mCurrentGame != null)
                {
                    this.SendPlayerName();
                }
            }
        }
    }

    protected internal ServerConnection server { get; private set; }

    public PeerStates states { get; internal set; }
}

