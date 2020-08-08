using ExitGames.Client.Photon;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Text;
using System.Linq;
//using ExitGames.Client.Photon.Lite;
using BRM;
using System.Threading;
//using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using UnityEditor.VersionControl;
using System.Diagnostics.PerformanceData;

public class FengGameManagerMKII : Photon.MonoBehaviour
{
    public static bool ShowMenuButtonGUI = true;
    public Dictionary<int, CannonValues> allowedToCannon;
    public static readonly string applicationId = "5578b046-8264-438c-99c5-fb15c71b6744";//new connection string /*"f1f6195c-df4a-40f9-bae5-4744c32901ef"; old connection string*/
    public Dictionary<string, Texture2D> assetCacheTextures;
    public static ExitGames.Client.Photon.Hashtable banHash;
    public static ExitGames.Client.Photon.Hashtable boolVariables;
    public static Dictionary<string, GameObject> CachedPrefabs;
    private ArrayList chatContent;
    public GameObject checkpoint;
    private ArrayList cT;
    public static List<string> ActiveGameModes = new List<string>();
    //internal static GameObject GUICtrl = new GameObject("MainGUI");
    //internal static ServerListGUI serverlistGUI;
    public static string Colr(string Stroke)
    {
        return "<b><color=#" + MainColor + ">" + Stroke + "</color></b>";
    }
    public static string Colr(string Stroke, bool i, bool b, string HashCode)
    {
        if (i) Stroke = "<i>" + Stroke + "</i>";
        if (b) Stroke = "<b>" + Stroke + "</b>";
        return "<color=#" + HashCode + ">" + Stroke + "</color>";
    }
    public static string currentLevel;
    public static string currentScript;
    public static string currentScriptLogic;
    private float currentSpeed;
    public static bool customLevelLoaded;
    public int cyanKills;
    internal float deltaTime;
    public int difficulty;
    public float distanceSlider;
    private bool endRacing;
    private ArrayList eT;
    public static ExitGames.Client.Photon.Hashtable floatVariables;
    private ArrayList fT;
    private float gameEndCD;
    private float gameEndTotalCDtime = 9f;
    public static bool gameStart = false;
    private bool gameTimesUp;
    public static ExitGames.Client.Photon.Hashtable globalVariables;
    public List<GameObject> groundList;
    public static bool hasLogged;

    public static ExitGames.Client.Photon.Hashtable heroHash;
    private int highestwave = 1;
    private ArrayList hooks;
    private int humanScore;
    public static List<int> ignoreList;
    public static ExitGames.Client.Photon.Hashtable imatitan;
    public static InputManagerRC inputRC;
    public static FengGameManagerMKII instance;
    public static ExitGames.Client.Photon.Hashtable intVariables;
    public static bool isAssetLoaded;
    public bool isFirstLoad;
    private bool isLosing;
    private bool isPlayer1Winning;
    private bool isPlayer2Winning;
    public bool isRecompiling;
    public bool isRestarting;
    public bool isSpawning;
    public bool isUnloading;
    private bool isWinning;
    public bool justSuicide;
    public bool Joined = false;
    private ArrayList kicklist;
    private ArrayList killInfoGO = new ArrayList();
    public static bool LAN;

    public List<string[]> levelCache;
    public static ExitGames.Client.Photon.Hashtable[] linkHash;
    public string localRacingResult;
    public static bool logicLoaded;
    public static int loginstate;
    public int magentaKills;
    //private IN_GAME_MAIN_CAMERA mainCamera;
    public static bool masterRC;
    public int maxPlayers;
    private float maxSpeed;
    public float mouseSlider;
    private string myLastHero;
    private string myLastRespawnTag = "playerRespawn";
    public float myRespawnTime;
    public string name;
    public static string Chatname;
    public static string nameField;
    public bool needChooseSide;
    public static bool noRestart;
    public static string oldScript;
    public static string oldScriptLogic;
    public static bool OnPrivateServer;
    public static string passwordField;
    public float pauseWaitTime;
    public string playerList;
    public List<Vector3> playerSpawnsC;
    public List<Vector3> playerSpawnsM;
    public List<PhotonPlayer> playersRPC;
    public static ExitGames.Client.Photon.Hashtable playerVariables;
    public Dictionary<string, int[]> PreservedPlayerKDR;
    public static string PrivateServerAuthPass;
    public static string privateServerField;
    public int PVPhumanScore;
    private int PVPhumanScoreMax = 200;
    public int PVPtitanScore;
    private int PVPtitanScoreMax = 200;
    public float qualitySlider;
    public List<GameObject> racingDoors;
    private ArrayList racingResult;
    public Vector3 racingSpawnPoint;
    public bool racingSpawnPointSet;
    public static AssetBundle RCassets;
    public static ExitGames.Client.Photon.Hashtable RCEvents;
    public static ExitGames.Client.Photon.Hashtable RCRegions;
    public static ExitGames.Client.Photon.Hashtable RCRegionTriggers;
    public static ExitGames.Client.Photon.Hashtable RCVariableNames;
    public List<float> restartCount;
    public bool restartingBomb;
    public bool restartingEren;
    public bool restartingHorse;
    public bool restartingMC;
    public bool restartingTitan;
    public float retryTime;
    public float roundTime;
    public static string[] s;
    public Vector2 scroll;
    public Vector2 scroll2;
    public GameObject selectedObj;
    public static object[] settings;
    private int single_kills;
    private int single_maxDamage;
    private int single_totalDamage;
    public static Material skyMaterial;
    public List<GameObject> spectateSprites;
    public bool startRacing;
    public static ExitGames.Client.Photon.Hashtable stringVariables;
    private int[] teamScores;
    private int teamWinner;
    public Texture2D textureBackgroundPitchBlack;
    public Texture2D textureBackgroundBlack;
    public Texture2D textureBackgroundBlue;
    public Texture2D textureBackgroundYellow;
    public Texture2D textureBackgroundDarkGrey;
    public Texture2D textureBackgroundGrey;
    public Texture2D textureBackgroundWhite;
    public int time = 600;
    private float timeElapse;
    private float timeTotalServer;
    private int titanScore;
    public List<TitanSpawner> titanSpawners;
    public List<Vector3> titanSpawns;
    public static ExitGames.Client.Photon.Hashtable titanVariables;
    public float transparencySlider;
    public float updateTime;
    public static string usernameField;
    public int wave = 1;

    public static string level = string.Empty;
    private static LevelInfo current;
    public static LevelInfo levelinfo
    {
        get
        {
            if (Application.loadedLevel != 0)
            {
                return current;
            }
            return new LevelInfo();
        }
        set
        {
            current = value;
        }
    }
    public UIReferArray uirefer;
    public static PhotonView PView;
    public static GameObject LabelScore;
    public static UILabel LabelScoreUI;
    public static Transform LabelScoreT;

    private UILabel LabelInfoTopRight;
    private UILabel LabelInfoCenter;
    private UILabel LabelInfoTopCenter;
    private UILabel LabelInfoTopLeft;
    private UILabel LabelNetworkStatus;
    private string scriptcache = string.Empty; //level script cache 

    public static bool AIMBotBomb = false;
    public static bool AutoExplodeBomb = false;
    public static bool RandomizeBombColor = false;

    public static List<GameObject> allheroes = new List<GameObject>();
    public static List<TITAN> titans = new List<TITAN>();
    public static List<HERO> heroes = new List<HERO>();
    public static List<GameObject> alltitans = new List<GameObject>();





    public static Texture2D ImageFront;
    //public static Texture2D ImageBack;
    //public static Texture2D ImageLeft;
    //public static Texture2D ImageRight;
    //public static Texture2D ImageUp;
    //public static Texture2D ImageDown;
    internal static Queue<GameObject> customObjects = new Queue<GameObject>();
    public static string MainColor = "F0F0F0";
    public static bool Agressive = false;
    public static List<string> disconnectBan = new List<string>();
    public static string Chatcolor;
    public static bool serverList;
    public static int SelectMode;
    public static int SelectMod = 0;
    public static string ServerName = "";

    public static List<string> Rollingmodlist = new List<string>
    {
        "Disconnect_mod",
        "CyanModNew",
        "RedSkies"
    };
    public static bool Rolling;
    private bool isNameAnimated;
    private static List<string> hexcodes = new List<string>();
    private static Regex regexHex = new Regex("\\[([a-fA-F0-9]{6})\\]");
    internal static string linearSpeed = "0.09";
    private bool rewind;
    private int placeFade;
    private static float timeTillReplaced = 0f;
    internal static int animHexSelected = 0;
    internal static string animSpeed = "0.09";
    internal static Dictionary<int, string> fadedSpeed = new Dictionary<int, string>();
    internal static string linearSpeedTest = "0.09";
    internal static Dictionary<string, string> reboundSpeed = new Dictionary<string, string>(2)
    {
        {
            "fwd",
            "0.09"
        },
        {
            "back",
            "0.09"
        }
    };
    internal static Dictionary<int, string> fadedSpeedTest = new Dictionary<int, string>();
    internal static Dictionary<string, string> reboundSpeedTest = new Dictionary<string, string>(2)
    {
        {
            "fwd",
            "0.09"
        },
        {
            "back",
            "0.09"
        }
    };

    private static Pair<Dictionary<int, string>, bool> PhotonObjectsText = new Pair<Dictionary<int, string>, bool>(new Dictionary<int, string>
    {
        {
            0,
            "[000000][[cfcfcf]LIST OF OBJECTS[-]][-]\nOwnerID:Name:Tag:ViewID\n"
        }
    }, false);

    public enum BuildType : byte
    {
        Dev = 1,
        Beta,
        Relase,
        Stable,
        Other = 255
    }

    public string ModRoll(bool justCheck)
    {
        if (!justCheck)
        {
            if (FengGameManagerMKII.Rollingmodlist.Count <= 0)
            {
                FengGameManagerMKII.SelectMod = 0;
                return string.Empty;
            }
            if (++FengGameManagerMKII.SelectMod >= FengGameManagerMKII.Rollingmodlist.Count)
            {
                FengGameManagerMKII.SelectMod = 0;
                return FengGameManagerMKII.Rollingmodlist[0];
            }
        }
        return FengGameManagerMKII.Rollingmodlist[FengGameManagerMKII.SelectMod];
    }

    public string Checkmod(PhotonPlayer player)
    {
        ExitGames.Client.Photon.Hashtable customProperties = player.customProperties;
        string text = customProperties[PhotonPlayerProperty.name].isString();
        if (player.SLB)
        {

            return "[F0F0F0]SLB[7b001c]";
        }
        if (player.CM)
        {
            return "[00FFFF]Cyan_mod [F0F0F0]v." + player.version + "[7b001c]";
        }
        if (player.RS)
        {
            return "[000000]DEATH/[FF0000]RedSkies [F0F0F0]mod " + player.versionRS;
        }
        if (player.DisconnectMod)
        {
            return "[dbdbdb]Disconnect [A8FF24]mod";
        }
        if (customProperties != null && customProperties.Count > 0)
        {
            if (customProperties["CyanModNew"] != null && customProperties["CyanMod"] != null)
            {
                return "[00FFFF]Cyan_mod [F0F0F0]v." + customProperties["CyanModNew"].isString() + "[7b001c]";
            }
            if (customProperties["CyanMod"] != null)
            {
                return "[00FFFF]Cyan_mod(old)[7b001c]";
            }
            if (text.StartsWith("[6c1dcb]"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[6c1dcb]Galaxy[7b001c]";
            }
            if (text.StartsWith("Remus") || text.StartsWith("[77c2a7]") || text.StartsWith("Kurome"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[001d18]KUROI SORA/AkaNime[7b001c]";
            }
            if (text.StartsWith("[FF0000]Vivid-Assassin") || text.StartsWith("[ffd700]Vivid-Assassin") || text.StartsWith("[00ff00]Tokyo Ghoul") || text.Contains("Vivid-Assassin") || text.Contains("Tokyo Ghoul"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[00FF00]Parrot's [F0F0F0]mod";
            }
            if (text.StartsWith("[ffd700]Hyper-MegaCannon"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[ffd700]Hyper-MegaCannon [F0F0F0]mod";
            }
            if (customProperties["KageNoKishi"] != null)
            {
                return "[F0F0F0]KageNoKishi mod [FF0000]{Danger!}[7b001c]";
            }
            if (customProperties["DT"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[F0F0F0]DT/Arche [F0F0F0]mod";
            }
            if (customProperties["JakesMod"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[F0F0F0]JakesMod[7b001c]";
            }
            if (customProperties["Alpha_X"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[F0F0F0]Alpha_X [F0F0F0]mod";
            }

            if (customProperties["Robbie'sMod"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[FF0000]Robbie's [F0F0F0]mod";
            }
            if (customProperties["not null"] != null)
            {
                return "[6c4675]ЕС [F0F0F0]mod";
            }
            if (customProperties["Destroy"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[000000]Destroy [F0F0F0]mod";
            }
            if (customProperties["Alpha"] != null)
            {
                return "[F0F0F0]Alpha [F0F0F0]mod";
            }
            if (customProperties.ContainsKey("INS"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[000000]INSANE [F0F0F0]mod";
            }
            if (customProperties.ContainsKey("UPublica2") || customProperties.ContainsKey("coins"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[7e0059]Universal [F0F0F0]mod";
            }
            if (customProperties["SRC"] != null)
            {
                return "[FF0000]SRC [F0F0F0]mod";
            }

            if (customProperties.ContainsKey("Raoh"))
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "RaohMod";
            }
            if (customProperties["BRC"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[000000]BRC [F0F0F0]mod";
            }
            if (customProperties["BRM"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[000000]B[720114]R[e2e8bc]M mod[7b001c]";
            }
            if (customProperties["woahWtf"] != null)
            {
                return "[F0F0F0]woahWtf mod[7b001c]";
            }
            if (customProperties["OhSoShO"] != null)
            {
                return "[F0F0F0]OhSoShO [F0F0F0]mod [FF0000]{Danger!}[7b001c]";
            }
            if (customProperties["Nathan"] != null)
            {
                return "[0000FF]Nathan [F0F0F0]mod";
            }
            if (customProperties["Arch"] != null)
            {
                return "[FFFF00]Arch [F0F0F0]mod";
            }
            if (customProperties["NRC"] != null)
            {
                return "[FF0000]NRC [F0F0F0]mod";
            }
            if (customProperties["Doge"] != null)
            {
                return "[F0F0F0]Doge [F0F0F0]mod";
            }
            if (customProperties["GHOST"] != null)
            {
                disconnectBan.Add(player.uiname);
                if (Agressive) base.StartCoroutine(ByteDC(player, 1000));
                return "[000000]GHOST [F0F0F0]mod";
            }
            if (customProperties["CMod_CModVersion"] != null)
            {
                byte[] array = (byte[])customProperties["CMod_CModVersion"];
                FengGameManagerMKII.BuildType buildType = FengGameManagerMKII.BuildType.Other;
                string text2 = string.Empty;
                if (array.Length == 3)
                {
                    if (System.Enum.GetValues(typeof(FengGameManagerMKII.BuildType)).Cast<byte>().Contains(array[0]))
                    {
                        buildType = (FengGameManagerMKII.BuildType)array[0];
                    }
                    text2 = array[1].ToString();
                    text2 = text2 + "." + array[2].ToString();
                }
                return string.Concat(new string[]
                {
                "[00FFFF]CMod [F0F0F0]v.",
                text2,
                " (",
                buildType.ToString(),
                ")[7b001c]"
                });
            }
            if (customProperties["RCteams"] != null || customProperties["Rage"] != null)
            {
                return "[F0F0F0]Rage [7b001c]mod";
            }
            if (customProperties["DEATH"] != null || customProperties["redskies"] != null || customProperties["REDSKIES"] != null || customProperties["REDSKIESV2"] != null)
            {
                return "[000000]DEATH/[FF0000]RedSkies [F0F0F0]mod";
            }
        }
        if (player.RC)
        {
            return "[9999FF]RC [F0F0F0]mod";
        }
        if (customProperties["RCteam"] != null)
        {
            return "[A8FF24]Anti-Cheat";
        }
        return "[F0F0F0]Vanilla/Other";
    }

    public ExitGames.Client.Photon.Hashtable reviveProps;

    //public static List<string> MyCurrPlayerPropsKeys = new List<string>
    //{
    //    "beard_texture_id",
    //    "body_texture",
    //    "cape",
    //    "character",
    //    "costumeId",
    //    "currentLevel",
    //    "customBool",
    //    "customFloat",
    //    "customInt",
    //    "customString",
    //    "dead",
    //    "deaths",
    //    "division",
    //    "eye_texture_id",
    //    "glass_texture_id",
    //    "guildName",
    //    "hair_color1",
    //    "hair_color2",
    //    "hair_color3",
    //    "hairInfo",
    //    "heroCostumeId",
    //    "isTitan",
    //    "kills",
    //    "max_dmg",
    //    "name",
    //    "part_chest_1_object_mesh",
    //    "part_chest_1_object_texture",
    //    "part_chest_object_mesh",
    //    "part_chest_object_texture",
    //    "part_chest_skinned_cloth_mesh",
    //    "part_chest_skinned_cloth_texture",
    //    "RCBombA",
    //    "RCBombB",
    //    "RCBombG",
    //    "RCBombR",
    //    "RCBombRadius",
    //    "RCteam",
    //    "sex",
    //    "skin_color",
    //    "statACL",
    //    "statBLA",
    //    "statGAS",
    //    "statSKILL",
    //    "statSPD",
    //    "team",
    //    "total_dmg",
    //    "uniform_type",
    //    "sender"
    //};

    public static IEnumerator DoInstantiateDC(PhotonPlayer player, int strikes)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable
            {
            {(byte) 0, "b"},
            {(byte) 1, null}
        };
        RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ID } };
        while (PhotonPlayer.Find(player.ID) != null)
        {
            for (int i = 0; i < strikes; i++)
            {
                PhotonNetwork.networkingPeer.OpRaiseEvent(202, hash, true, options);
            }
            PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator SerializeCrash(PhotonPlayer player, int iterations)
    {
        Dictionary<byte, object> hash = new Dictionary<byte, object>();
        hash[(byte)0] = new FengGameManagerMKII[] { FengGameManagerMKII.instance, new FengGameManagerMKII() };
        hash[(byte)1] = new FengGameManagerMKII();
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions() { TargetActors = new int[] { player.ID } };
        //ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable
        //{
        //    [(byte)0] = PhotonPeer.RegisterType(typeof(FengGameManagerMKII), 69, new SerializeMethod(CustomTypes.SerializePhotonPlayer),
        //new DeserializeMethod(CustomTypes.DeserializePhotonPlayer))
        //};
        for (int i = 0; i < iterations; i++)
        {
            PhotonNetwork.networkingPeer.OpRaiseEvent((byte)0, hash, true, raiseEventOptions);
        }
        yield return new WaitForSeconds(1f);
    }
    public static IEnumerator SerializeDC(PhotonPlayer player, int strikes)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable
        {
            [(byte)0] = PhotonNetwork.networkingPeer.ServerTimeInMilliSeconds,
            [(byte)1] = PhotonNetwork.networkingPeer.currentLevelPrefix,
            [(byte)2] = new ExitGames.Client.Photon.Hashtable { [(byte)0] = null }
        };
        RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ID } };
        while (PhotonPlayer.Find(player.ID) != null)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < strikes; i++)
            {
                PhotonNetwork.networkingPeer.OpRaiseEvent(206, hash, true, options);
            }
            PhotonNetwork.networkingPeer.OpRaiseEvent(200, null, true, options);
        }
    }
    public IEnumerator myDC(PhotonPlayer player, int Packs)
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        StatsTab.AddLine("Player <color=black>[<color=white>" + player.ID + "</color>]</color> " + player.uiname.ToRGBA() + " has been sent <b>packs:<i> " + Packs + "</i></b> and he was add in DCBan list... ", StatsTab.DebugType.UPDATE);
        while (player != null)
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
            raiseEventOptions.TargetActors = new int[] { player.ID };
            ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable();
            int j = 0;
            for (j = 0; j < Packs; j++)
            {
                customEventContent[245] = PhotonNetwork.networkingPeer.mMasterClient.ID;
                customEventContent[244] = PhotonNetwork.networkingPeer.mMasterClient.ID;
                customEventContent[243] = PhotonNetwork.networkingPeer.mMasterClient.ID;
                customEventContent[-1] = 0xcb;
                customEventContent[-4] = 0x7fffffff;
                customEventContent[(byte)0] = 0x7fffffff;
                customEventContent[(byte)1] = (short)0x7fff;
                PhotonNetwork.networkingPeer.OpRaiseEvent(0xce, customEventContent, true, raiseEventOptions);
            }
            if ((PhotonPlayer.Find(player.ID) == null) || (player == null))
            {
                stopwatch.Stop();
                InRoomChat.addLINE2("<color=#7b001c>Done.</color> <b><color=#7b001c>Name:</color>{4}{0}{5} <color=#7b001c>ID:</color>{4}{1}{5} <color=#7b001c>Packs:</color>{4}{2}{5} <color=#7b001c>MS:</color>{4}{3}{5}</b>".Formats(new object[]
                {
                    player.uiname.ToRGBA(),
                    player.ID,
                    j,
                    stopwatch.ElapsedMilliseconds,
                    "<i>",
                    "</i>"
                }));
                yield break;
            }
            if (PhotonNetwork.GetPing() > 900)
            {
                stopwatch.Stop();
                InRoomChat.addLINE2("<b><color=red>Warning! DC was stopped. Reason: ping > <color=white><i>900</i></color>. May be DC disconnected yourself...</color></b>");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator Lag(PhotonPlayer player, int packs)
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        StatsTab.AddLine("Player <color=black>[<color=white>" + player.ID + "</color>]</color> " + player.uiname.ToRGBA() + " has been sent <b>packs:<i> " + packs + "</i></b> and he was add in DCBan list... ", StatsTab.DebugType.UPDATE);
        int i = 0;
        while (player != null)
        {
            for (i = 0; i < packs; i++)
            {
                object[] objArray1 = new object[] {
                            (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff,
                            (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff,
                            (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff
                        };
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
                raiseEventOptions.TargetActors = new int[] { player.ID };
                ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable
                {
                    [0xf5] = PhotonNetwork.networkingPeer.mMasterClient.ID,
                    [(byte)0] = -2147483648,
                    [(byte)1] = (short)0x7fff,
                    [(byte)2] = PhotonPlayer.Find(Convert.ToInt32(player.ID))
                };
                PhotonNetwork.networkingPeer.OpRaiseEvent(206, customEventContent, true, raiseEventOptions);
                PhotonNetwork.networkingPeer.OpRaiseEvent(206, customEventContent, true, raiseEventOptions);
                if ((player == null) || (PhotonPlayer.Find(player.ID) == null))
                {
                    stopwatch.Stop();
                    InRoomChat.addLINE2("<color=#7b001c>Done.</color> <b><color=#7b001c>Name:</color>{4}{0}{5} <color=#7b001c>ID:</color>{4}{1}{5} <color=#7b001c>Packs:</color>{4}{2}{5} <color=#7b001c>MS:</color>{4}{3}{5}</b>".Formats(new object[]
                    {
                        player.uiname.ToRGBA(),
                        player.ID,
                        i,
                        stopwatch.ElapsedMilliseconds,
                        "<i>",
                        "</i>"
                    }));
                    yield break;
                }
                if (PhotonNetwork.GetPing() > 900)
                {
                    stopwatch.Stop();
                    InRoomChat.addLINE2("<b><color=red>Warning! DC was stopped. Reason: ping > <color=white><i>900</i></color>. May be DC disconnected yourself...</color></b>");
                    yield break;
                }
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
    }
    public IEnumerator EventSpam(PhotonPlayer player)
    //{
    //    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    //    stopwatch.Start();
    //    StatsTab.AddLine("Player <color=black>[<color=white>" + player.ID + "</color>]</color> " + player.uiname.ToRGBA() + " has been sent event's codes and he was add in DCBan list... ", StatsTab.DebugType.UPDATE);
    //    if (!disconnectBan.Contains(player.uiname.StripHex()))
    //    {
    //        disconnectBan.Add(player.customProperties[PhotonPlayerProperty.name].ToString().StripHex());
    //    }
    //    RaiseEventOptions opt = new RaiseEventOptions
    //    {
    //        TargetActors = new int[]
    //        {
    //            player.ID
    //        }
    //    };     
    //    byte i = 0;
    //    while (player != null)
    //    {
    //        for (i = 0; i < 255; i++)
    //        {
    //            PhotonNetwork.networkingPeer.OpRaiseEvent(i, null, true, opt);             
    //            if ((player == null) || (PhotonPlayer.Find(player.ID) == null))
    //            {
    //                stopwatch.Stop();
    //                InRoomChat.addLINE2("<color=#7b001c>Done.</color> <b><color=#7b001c>Name:</color>{4}{0}{5} <color=#7b001c>ID:</color>{4}{1}{5} <color=#7b001c>Packs:</color>{4}{2}{5} <color=#7b001c>MS:</color>{4}{3}{5}</b>".Formats(new object[]
    //                {
    //                    player.uiname.ToRGBA(),
    //                    player.ID,
    //                    i,
    //                    stopwatch.ElapsedMilliseconds,
    //                    "<i>",
    //                    "</i>"
    //                }));
    //                yield break;
    //            }
    //            if (PhotonNetwork.GetPing() > 900)
    //            {
    //                stopwatch.Stop();
    //                InRoomChat.addLINE2("<b><color=red>Warning! DC was stopped. Reason: ping > <color=white><i>900</i></color>. May be DC disconnected yourself...</color></b>");
    //                yield break;
    //            }
    //        }
    //        yield return new WaitForSeconds(1f);
    //    }
    //    yield return new WaitForSeconds(1f);
    {
        while (player != null)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            InRoomChat.addLINE2("Player <color=black>[<color=white>" + player.ID + "</color>]</color> " + player.uiname.ToRGBA() + " has been sent event's codes and he was add in DCBan list... ");
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                TargetActors = new int[]
            {
                player.ID
            }
            };
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            for (int parameters = 0; parameters < 1000; parameters++)
            {
                PhotonNetwork.networkingPeer.OpRaiseEvent((byte)UnityEngine.Random.Range(0, 255), hashtable, true, raiseEventOptions);
                PhotonNetwork.networkingPeer.OpRaiseEvent((byte)UnityEngine.Random.Range(0, 255), hashtable, true, raiseEventOptions);
                if (PhotonPlayer.Find(player.ID) == null)
                {
                    stopwatch.Stop();
                    InRoomChat.addLINE2("<color=#7b001c>Done.</color> <b><color=#7b001c>Name:</color>{4}{0}{5} <color=#7b001c>ID:</color>{4}{1}{5} <color=#7b001c>Packs:</color>{4}{2}{5} <color=#7b001c>MS:</color>{4}{3}{5}</b>".Formats(new object[]
                    {
                                        player.uiname.ToRGBA(),
                                        player.ID,
                                        parameters,
                                        stopwatch.ElapsedMilliseconds,
                                        "<i>",
                                        "</i>"
                    }));
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void HardToKill(PhotonPlayer player)
    {
        if (!player.isDead)
        {
            foreach (HERO hero in heroes)
            {
                PhotonView photonView;
                if (hero != null && (photonView = hero.photonView).owner == player)
                {
                    hero.markDie();
                    hero.photonView.RPC("netDie2", player, -1, "[CFCFCF]Y[EFEFEF]O[CFCFCF]U[EFEFEF]R [CFCFCF]M[EFEFEF]O[CFCFCF]M [EFEFEF]G[CFCFCF]A[EFEFEF]Y");
                }
            }
        }
    }

    public void killPlayer1(PhotonPlayer player)
    {

        HardToKill(player);
        foreach (GameObject obj4 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if ((obj4 != null) && (obj4.GetPhotonView().owner == player))
            {
                obj4.GetComponent<TITAN>().photonView.RPC("titanGetHit", player, new object[] { obj4.GetPhotonView().viewID, 0x1b39 });
                if (!((bool)player.customProperties[PhotonPlayerProperty.dead]))
                {
                    obj4.GetComponent<TITAN>().photonView.RPC("titanGetHit", player, new object[] { obj4.GetPhotonView().viewID, 0x3e8 });
                    if (!((bool)player.customProperties[PhotonPlayerProperty.dead]))
                    {
                        obj4.GetComponent<TITAN>().photonView.RPC("titanGetHit", player, new object[] { obj4.GetPhotonView().viewID, 0x7d0 });
                        if (!((bool)player.customProperties[PhotonPlayerProperty.dead]))
                        {
                            obj4.GetComponent<TITAN>().photonView.RPC("titanGetHit", player, new object[] { obj4.GetPhotonView().viewID, 0x1388 });
                            if (!((bool)player.customProperties[PhotonPlayerProperty.dead]))
                            {
                                obj4.GetComponent<TITAN>().photonView.RPC("titanGetHit", player, new object[] { obj4.GetPhotonView().viewID, 0x2329 });
                            }
                        }
                    }
                }
            }
        }
    }
    internal static string ModMessage()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (ActiveGameModes.Count <= 0)
            {
                SelectMode = 0;
                return string.Empty;
            }
            int num = SelectMode + 1;
            SelectMode = num;
            if (num >= ActiveGameModes.Count)
            {
                SelectMode = 0;
                return "\n" + ActiveGameModes[0];
            }
            return "\n" + ActiveGameModes[num];
        }
        return string.Empty;
    }

    public static void GrabHexes()
    {
        FengGameManagerMKII.hexcodes.Clear();
        string name = LoginFengKAI.player.name;
        if (!name.IsNullOrEmpty() && FengGameManagerMKII.regexHex.IsMatch(name))
        {
            MatchCollection matchCollection = FengGameManagerMKII.regexHex.Matches(name);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                if (name.HasHexAt(matchCollection[i].Index))
                {
                    FengGameManagerMKII.hexcodes.Add(matchCollection[i].Value);
                }
            }
        }
        name = LoginFengKAI.player.name;
    }

    //public void StartNameAnim(bool start)
    //{
    //    name = LoginFengKAI.player.name;
    //    if (start)
    //    {
    //        if (!this.isNameAnimated && name.HasHex())
    //        {
    //            this.isNameAnimated = true;
    //            base.StartCoroutine(this.NameAnimation());
    //            return;
    //        }
    //    }
    //    else if (this.isNameAnimated)
    //    {
    //        this.isNameAnimated = false;         
    //        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
    //        hashtable.Add(PhotonPlayerProperty.name, LoginFengKAI.player.name);
    //        PhotonNetwork.player.SetCustomProperties(hashtable);
    //        base.StopCoroutine(this.NameAnimation());
    //    }
    //}

    //// Token: 0x06001456 RID: 5206 RVA: 0x000D9054 File Offset: 0x000D7254
    //private IEnumerator NameAnimation()
    //{
    //    FengGameManagerMKII.GrabHexes();
    //    yield return new WaitForSeconds(1f);
    //    while (this.isNameAnimated)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        int key;
    //        this.Animate((string)FengGameManagerMKII.settings[270], out key);
    //        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
    //        hashtable.Add(PhotonPlayerProperty.name, name);
    //        PhotonNetwork.player.SetCustomProperties(hashtable);
    //        if ((string)FengGameManagerMKII.settings[267] != "None" && (string)FengGameManagerMKII.settings[270] == "Faded" && FengGameManagerMKII.fadedSpeed.ContainsKey(key))
    //        {
    //            float num;
    //            if (float.TryParse(FengGameManagerMKII.fadedSpeed[key], out num))
    //            {
    //                yield return new WaitForSeconds(num);
    //            }
    //            else
    //            {
    //                yield return new WaitForSeconds(0.09f);
    //            }
    //        }
    //        else if ((string)FengGameManagerMKII.settings[268] != "None" && (string)FengGameManagerMKII.settings[270] == "Linear")
    //        {
    //            float num;
    //            if (float.TryParse(FengGameManagerMKII.linearSpeed, out num))
    //            {
    //                yield return new WaitForSeconds(num);
    //            }
    //            else
    //            {
    //                yield return new WaitForSeconds(0.09f);
    //            }
    //        }
    //        else if ((string)FengGameManagerMKII.settings[269] != "None" && (string)FengGameManagerMKII.settings[270] == "Rebound")
    //        {
    //            float num;
    //            if (this.rewind)
    //            {
    //                if (float.TryParse(FengGameManagerMKII.reboundSpeed["back"], out num))
    //                {
    //                    yield return new WaitForSeconds(num);
    //                }
    //                else
    //                {
    //                    yield return new WaitForSeconds(0.09f);
    //                }
    //            }
    //            else if (float.TryParse(FengGameManagerMKII.reboundSpeed["fwd"], out num))
    //            {
    //                yield return new WaitForSeconds(num);
    //            }
    //            else
    //            {
    //                yield return new WaitForSeconds(0.09f);
    //            }
    //        }
    //        else
    //        {
    //            yield return new WaitForSeconds(0.09f);
    //        }
    //        if (name == LoginFengKAI.player.name)
    //        {
    //            this.rewind = !this.rewind;
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield break;
    //}

    // Token: 0x06001457 RID: 5207 RVA: 0x000D9070 File Offset: 0x000D7270
    //private void Animate(string type, out int place)
    //{
    //    place = 0;
    //    string text = name;
    //    if (type == "Faded")
    //    {
    //        if (this.rewind)
    //        {
    //            this.placeFade--;
    //            if (this.placeFade < 0)
    //            {
    //                this.rewind = false;
    //                this.placeFade++;
    //            }
    //        }
    //        else
    //        {
    //            this.placeFade++;
    //            if (this.placeFade >= FengGameManagerMKII.hexcodes.Count)
    //            {
    //                this.rewind = true;
    //                this.placeFade--;
    //            }
    //        }
    //        place = this.placeFade;
    //        text = text.StripHex();
    //        text = FengGameManagerMKII.hexcodes[this.placeFade] + text + "[-]";
    //    }
    //    else if (type == "Rebound")
    //    {
    //        MatchCollection matchCollection = FengGameManagerMKII.regexHex.Matches(text);
    //        for (int i = 0; i < matchCollection.Count; i++)
    //        {
    //            int num;
    //            if (!this.rewind)
    //            {
    //                num = ((i > 0) ? (i - 1) : (matchCollection.Count - 1));
    //            }
    //            else
    //            {
    //                num = ((i < matchCollection.Count - 1) ? (i + 1) : 0);
    //            }
    //            text = text.Remove(matchCollection[i].Index, 8);
    //            text = text.Insert(matchCollection[i].Index, matchCollection[num].Value);
    //        }
    //    }
    //    else if (type == "Linear")
    //    {
    //        MatchCollection matchCollection2 = FengGameManagerMKII.regexHex.Matches(text);
    //        for (int j = 0; j < matchCollection2.Count; j++)
    //        {
    //            int num2 = (j > 0) ? (j - 1) : (matchCollection2.Count - 1);
    //            text = text.Remove(matchCollection2[j].Index, 8);
    //            text = text.Insert(matchCollection2[j].Index, matchCollection2[num2].Value);
    //        }
    //    }
    //    name = text;
    //}
    //public static void LoadAnimationToEdit(string animation = null)
    //{
    //    if (animation.IsNullOrEmpty())
    //    {
    //        return;
    //    }
    //    FengGameManagerMKII.GrabHexes();
    //    if (animation.StartsWith("Faded"))
    //    {
    //        Regex regex = new Regex("\\[\\d+\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            MatchCollection matchCollection = regex.Matches(animation);
    //            FengGameManagerMKII.fadedSpeedTest.Clear();
    //            for (int i = 0; i < matchCollection.Count; i++)
    //            {
    //                string value = matchCollection[i].Value;
    //                int num = value.IndexOf("[") + 1;
    //                string s = value.Substring(num, value.IndexOf("]") - num).Trim();
    //                int key;
    //                if (int.TryParse(s, out key))
    //                {
    //                    string text = value.Substring(value.IndexOf("spd=") + 4).Trim();
    //                    float num2;
    //                    if (float.TryParse(text, out num2))
    //                    {
    //                        if (FengGameManagerMKII.fadedSpeedTest.ContainsKey(key))
    //                        {
    //                            FengGameManagerMKII.fadedSpeedTest[key] = text;
    //                        }
    //                        else
    //                        {
    //                            FengGameManagerMKII.fadedSpeedTest.Add(key, text);
    //                        }
    //                    }
    //                }
    //            }
    //            return;
    //        }
    //    }
    //    else if (animation.StartsWith("Linear"))
    //    {
    //        Regex regex = new Regex("spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string s2 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num3;
    //            if (float.TryParse(s2, out num3))
    //            {
    //                FengGameManagerMKII.linearSpeedTest = s2;
    //                return;
    //            }
    //        }
    //    }
    //    else if (animation.StartsWith("Rebound"))
    //    {
    //        Regex regex = new Regex("\\[fwd\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string text2 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num4;
    //            if (float.TryParse(text2, out num4))
    //            {
    //                FengGameManagerMKII.reboundSpeedTest["fwd"] = text2;
    //            }
    //        }
    //        regex = new Regex("\\[back\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string text3 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num5;
    //            if (float.TryParse(text3, out num5))
    //            {
    //                FengGameManagerMKII.reboundSpeedTest["back"] = text3;
    //            }
    //        }
    //    }
    //}
    //public static void EncodeAnimation(string animation = null)
    //{
    //    if (animation.IsNullOrEmpty())
    //    {
    //        return;
    //    }
    //    FengGameManagerMKII.GrabHexes();
    //    if (animation.StartsWith("Faded"))
    //    {
    //        Regex regex = new Regex("\\[\\d+\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            MatchCollection matchCollection = regex.Matches(animation);
    //            FengGameManagerMKII.fadedSpeed.Clear();
    //            for (int i = 0; i < matchCollection.Count; i++)
    //            {
    //                string value = matchCollection[i].Value;
    //                int num = value.IndexOf("[") + 1;
    //                string s = value.Substring(num, value.IndexOf("]") - num).Trim();
    //                int key;
    //                if (int.TryParse(s, out key))
    //                {
    //                    string text = value.Substring(value.IndexOf("spd=") + 4).Trim();
    //                    float num2;
    //                    if (float.TryParse(text, out num2))
    //                    {
    //                        if (FengGameManagerMKII.fadedSpeed.ContainsKey(key))
    //                        {
    //                            FengGameManagerMKII.fadedSpeed[key] = text;
    //                        }
    //                        else
    //                        {
    //                            FengGameManagerMKII.fadedSpeed.Add(key, text);
    //                        }
    //                    }
    //                }
    //            }
    //            return;
    //        }
    //    }
    //    else if (animation.StartsWith("Linear"))
    //    {
    //        Regex regex = new Regex("spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string s2 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num3;
    //            if (float.TryParse(s2, out num3))
    //            {
    //                FengGameManagerMKII.linearSpeed = s2;
    //                return;
    //            }
    //        }
    //    }
    //    else if (animation.StartsWith("Rebound"))
    //    {
    //        Regex regex = new Regex("\\[fwd\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string text2 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num4;
    //            if (float.TryParse(text2, out num4))
    //            {
    //                FengGameManagerMKII.reboundSpeed["fwd"] = text2;
    //            }
    //        }
    //        regex = new Regex("\\[back\\]spd=\\d+(\\d+|\\.\\d+)?");
    //        if (regex.IsMatch(animation))
    //        {
    //            Match match = regex.Match(animation);
    //            string text3 = match.Value.Substring(match.Value.IndexOf("spd=") + 4).Trim();
    //            float num5;
    //            if (float.TryParse(text3, out num5))
    //            {
    //                FengGameManagerMKII.reboundSpeed["back"] = text3;
    //            }
    //        }
    //    }
    //}
    //public void addCamera(IN_GAME_MAIN_CAMERA c)
    //{
    //    this.mainCamera = c;
    //}

    public void addCT(COLOSSAL_TITAN titan)
    {
        this.cT.Add(titan);
    }

    public void addET(TITAN_EREN hero)
    {
        this.eT.Add(hero);
    }

    public void addFT(FEMALE_TITAN titan)
    {
        this.fT.Add(titan);
    }

    public void addHero(HERO hero, GameObject GO)
    {
        heroes.Add(hero);
        allheroes.Add(GO);
    }

    public void addHook(Bullet h)
    {
        this.hooks.Add(h);
    }

    public void addTime(float time)
    {
        this.timeTotalServer -= time;
    }

    //public void addTitan(TITAN titan)
    //{
    //    this.titans.Add(titan);
    //}

    public static void addTitan(TITAN titan, GameObject GO)
    {
        //if (titan.isLocal)
        //{
        //    localTitans.Add(titan);
        //}
        titans.Add(titan);
        alltitans.Add(GO);
    }

    private void Awake()
    {
        this.isBackground = true;
        (PView = base.photonView).isBackground = true;
        instance = this;
    }

    internal void AutoDisconnect(PhotonPlayer player, bool isBytes, bool includeMSG, long loop, params int[] ids)
    {
        byte eventCode;
        if (player == null || player.isMasterClient)
        {
            eventCode = 254;
        }
        else
        {
            eventCode = (byte)UnityEngine.Random.Range(0, 256);
        }
        string name = (player == null) ? string.Empty : player.uiname.ToRGBA();
        if (isBytes)
        {
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable
            {
                {
                    254,
                    PhotonNetwork.masterClient.ID
                }
            };
            if (loop >= 1000000000L)
            {
                byte[] value = new byte[loop / 3L];
                for (int i = 0; i < 6; i++)
                {
                    hashtable.Add((byte)i, value);
                }
            }
            else if (loop >= 1000000L)
            {
                byte[] value = new byte[loop / 2L];
                for (int j = 0; j < 4; j++)
                {
                    hashtable.Add((byte)j, value);
                }
            }
            else if (loop >= 1000L)
            {
                byte[] value = new byte[loop];
                for (int k = 0; k < 2; k++)
                {
                    hashtable.Add((byte)k, value);
                }
            }
            if (ids[0] == 0)
            {
                if (PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, hashtable, true, new RaiseEventOptions
                {
                    Receivers = 0
                }, new object[]
                {
                    ids,
                    5L,
                    loop
                }, string.Empty))
                {
                    base.Invoke("LeaveServer", 1.8f);
                }
                return;
            }
            PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, hashtable, true, new RaiseEventOptions
            {
                TargetActors = ids
            }, new object[]
            {
                ids,
                5L,
                loop
            }, name);
            return;
        }
        else
        {
            if (ids[0] == 0)
            {
                if (PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, new ExitGames.Client.Photon.Hashtable
                {
                    {
                        254,
                        PhotonNetwork.masterClient.ID
                    }
                }, true, new RaiseEventOptions
                {
                    Receivers = 0
                }, new object[]
                {
                    ids,
                    5L,
                    loop
                }, string.Empty))
                {
                    base.Invoke("LeaveServer", 1.8f);
                }
                return;
            }
            PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, new ExitGames.Client.Photon.Hashtable
            {
                {
                    254,
                    PhotonNetwork.masterClient.ID
                }
            }, true, new RaiseEventOptions
            {
                TargetActors = ids
            }, new object[]
            {
                ids,
                loop
            }, name);
            return;
        }
    }
    internal void AutoDisconnect(PhotonPlayer player, int id, long loop, bool isBytes = false, bool includeMSG = true)
    {
        byte eventCode;
        if (player == null || player.isMasterClient)
        {
            eventCode = 254;
        }
        else
        {
            eventCode = (byte)UnityEngine.Random.Range(0, 256);
        }
        string name = (player == null) ? string.Empty : player.uiname.ToRGBA();
        if (isBytes)
        {
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable
            {
                {
                    254,
                    PhotonNetwork.masterClient.ID
                }
            };
            if (loop >= 1000000000L)
            {
                byte[] value = new byte[loop / 3L];
                for (int i = 0; i < 6; i++)
                {
                    hashtable.Add((byte)i, value);
                }
            }
            else if (loop >= 1000000L)
            {
                byte[] value = new byte[loop / 2L];
                for (int i = 0; i < 4; i++)
                {
                    hashtable.Add((byte)i, value);
                }
            }
            else if (loop >= 1000L)
            {
                byte[] value = new byte[loop];
                for (int i = 0; i < 2; i++)
                {
                    hashtable.Add((byte)i, value);
                }
            }
            if (id == 0)
            {
                if (PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, hashtable, true, new RaiseEventOptions
                {
                    Receivers = 0
                }, new object[]
                {
                    id,
                    5L,
                    loop
                }, string.Empty))
                {
                    base.Invoke("LeaveServer", 1.8f);
                }
                return;
            }
            PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, hashtable, true, new RaiseEventOptions
            {
                TargetActors = new int[]
                {
                    id
                }
            }, new object[]
            {
                id,
                5L,
                loop
            }, name);
            return;
        }
        else
        {
            if (id == 0)
            {
                if (PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, new ExitGames.Client.Photon.Hashtable
                {
                    {
                        254,
                        PhotonNetwork.masterClient.ID
                    }
                }, true, new RaiseEventOptions
                {
                    Receivers = 0
                }, new object[]
                {
                    id,
                    5L,
                    loop
                }, string.Empty))
                {
                    base.Invoke("LeaveServer", 1.8f);
                }
                return;
            }
            PhotonNetwork.networkingPeer.OpReturn(includeMSG, eventCode, new ExitGames.Client.Photon.Hashtable
            {
                {
                    254,
                    PhotonNetwork.masterClient.ID
                }
            }, true, new RaiseEventOptions
            {
                TargetActors = new int[]
                {
                    id
                }
            }, new object[]
            {
                id,
                loop
            }, name);
            return;
        }
    }
    public IEnumerator RoastIenum(PhotonPlayer roaster)
    {
        for (int j = 0; j < 2; j++)
        {
            RaiseEventOptions options = new RaiseEventOptions();
            options.TargetActors = new int[] { roaster.ID };
            RaiseEventOptions raiseEventOptions = options;
            PhotonNetwork.networkingPeer.OpRaiseEvent(0xe4, null, true, raiseEventOptions);
            if (PhotonPlayer.Find(roaster.ID) == null)
            {
            }
        }
        yield return new WaitForEndOfFrame();
    }
    public IEnumerator ByteDC(PhotonPlayer player, int packs)
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        InRoomChat.addLINE2("Player <color=black>[<color=white>" + player.ID + "</color>]</color> " + player.uiname.ToRGBA() + " has been sent <b>packs:<i> " + packs + "</i></b> and he was add in DCBan list... ");
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
        raiseEventOptions.TargetActors = new int[] { player.ID };
        object[] objArray = new object[] {
                    (byte) 255, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff,
                    (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff,
                    (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff, (byte) 0xff
                };
        ExitGames.Client.Photon.Hashtable customEventContent = new ExitGames.Client.Photon.Hashtable
        {
            [245] = PhotonNetwork.networkingPeer.mMasterClient.ID,
            [244] = PhotonNetwork.networkingPeer.mMasterClient.ID,
            [243] = PhotonNetwork.networkingPeer.mMasterClient.ID,
            [(byte)0] = player.ID,
            [(byte)2] = 3.402823E+38f,
            [(byte)3] = "refreshStatus",
            [(byte)4] = objArray,
            [(byte)1] = objArray
        };
        int i = 0;
        while (player != null)
        {
            for (i = 0; i < packs; i++)
            {
                PhotonNetwork.networkingPeer.OpRaiseEvent(200, customEventContent, true, raiseEventOptions);
                PhotonNetwork.networkingPeer.OpRaiseEvent(200, customEventContent, true, raiseEventOptions);
                if ((player == null) || (PhotonPlayer.Find(player.ID) == null))
                {
                    stopwatch.Stop();
                    InRoomChat.addLINE2("<color=#7b001c>Done.</color> <b><color=#7b001c>Name:</color>{4}{0}{5} <color=#7b001c>ID:</color>{4}{1}{5} <color=#7b001c>Packs:</color>{4}{2}{5} <color=#7b001c>MS:</color>{4}{3}{5}</b>".Formats(new object[]
                    {
                        player.uiname.ToRGBA(),
                        player.ID,
                        i,
                        stopwatch.ElapsedMilliseconds,
                        "<i>",
                        "</i>"
                    }));
                    yield break;
                }
                if (PhotonNetwork.GetPing() > 900)
                {
                    stopwatch.Stop();
                    InRoomChat.addLINE2("<b><color=red>Warning! DC was stopped. Reason: ping > <color=white><i>900</i></color>. May be DC disconnected yourself...</color></b>");
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.0075f);
        }
        yield return new WaitForSeconds(0.0075f);
    }

    private void cache()
    {
        PView = base.photonView;

        IN_GAME_MAIN_CAMERA.mainC = Camera.main;
        IN_GAME_MAIN_CAMERA.mainT = Camera.main.transform;
        IN_GAME_MAIN_CAMERA.mainG = Camera.main.gameObject;

        //IN_GAME_MAIN_CAMERA.behaviour = Camera.main.GetComponent<Behaviour>();
        //IN_GAME_MAIN_CAMERA.listener = Camera.main.GetComponent<AudioListener>();

        IN_GAME_MAIN_CAMERA.skybox = Camera.main.GetComponent<Skybox>();
        IN_GAME_MAIN_CAMERA.mainCamera = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>();
        IN_GAME_MAIN_CAMERA.shake = Camera.main.GetComponent<CameraShake>();
        IN_GAME_MAIN_CAMERA.tiltshift = Camera.main.GetComponent<TiltShift>();
        IN_GAME_MAIN_CAMERA.mouselook = Camera.main.GetComponent<MouseLook>();
        IN_GAME_MAIN_CAMERA.spectate = Camera.main.GetComponent<SpectatorMovement>();

        //IN_GAME_MAIN_CAMERA.uipanel = Camera.main.GetComponent<UIPanel>();

        ClothFactory.ClearClothCache();
        this.playersRPC.Clear();
        this.titanSpawners.Clear();
        this.groundList.Clear();
        this.PreservedPlayerKDR = new Dictionary<string, int[]>();
        noRestart = false;
        skyMaterial = null;
        this.isSpawning = false;
        this.retryTime = 0f;
        logicLoaded = false;
        customLevelLoaded = true;
        this.isUnloading = false;
        this.isRecompiling = false;
        FengCustomInputs.Inputs = CacheGameObject.Find<FengCustomInputs>("InputManagerController");
        LabelScoreT = (LabelScoreUI = (LabelScore = CacheGameObject.Find("LabelScore")).GetComponent<UILabel>()).transform;
        Time.timeScale = 1f;
        //Camera.main.farClipPlane = 1500f;
        this.pauseWaitTime = 0f;
        this.spectateSprites = new List<GameObject>();
        this.isRestarting = false;
        if (PhotonNetwork.isMasterClient)
        {
            base.StartCoroutine(this.WaitAndResetRestarts());
        }
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
        {
            this.roundTime = 0f;
            if (level.StartsWith("Custom"))
            {
                customLevelLoaded = false;
            }
            if (PhotonNetwork.isMasterClient)
            {
                if (this.isFirstLoad)
                {
                    this.setGameSettings(this.checkGameGUI());
                }
                if (GameSettings.endlessMode > 0)
                {
                    base.StartCoroutine(this.respawnE((float)GameSettings.endlessMode));
                }
            }
            if (((int)settings[0xf4]) == 1)
            {
                InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round Start.");
            }
        }
        this.isFirstLoad = false;
        //this.RecompilePlayerList(0.5f);
        if (instance == null || (instance != this && this != null)) instance = this;
    }

    [RPC]
    private void Chat(string content, string sender, PhotonMessageInfo info)
    {
        if (info.sender.chatname == null && !sender.IsNullOrEmpty())
        {
            info.sender.chatname = sender;
        }
        sender = chatformat(sender);
        content = (content.HasRGBA() ? chatformat(content) : chatformat(content).RemoveTags());
        string str = info.sender.ID.ToString() + "<color=black>|| </color>";
        //if (content.Contains(BotInServer.BotName)) BotInServer.GetBotText();
        if (!sender.IsNullOrEmpty() || info.sender.uiname == string.Empty)
        {
            if (content != string.Empty)
            {
                InRoomChat.addLINE(str + sender + ":" + content, info.sender, true);

                return;
            }
        }
        else if (content != string.Empty)
        {
            InRoomChat.addLINE(str + content, info.sender, !info.sender.isMasterClient || !info.sender.uiname.IsNullOrEmpty());

        }
    }
    [RPC]
    public void ChatPM(string sender, string content, PhotonMessageInfo info)
    {
        sender = "<color=black>{<color=yellow>PM</color>}</color>" + sender;
        sender = chatformat(sender);
        content = (content.HasRGBA() ? chatformat(content) : chatformat(content).RemoveTags());
        string str = info.sender.ID.ToString() + "<color=black>|| </color>";
        if (!sender.IsNullOrEmpty() || info.sender.uiname == string.Empty)
        {
            if (content != string.Empty)
            {
                InRoomChat.addLINE(str + sender + ":" + content, info.sender, true);
                return;
            }
        }
        else if (content != string.Empty)
        {
            InRoomChat.addLINE(str + content, info.sender, !info.sender.isMasterClient || !info.sender.uiname.IsNullOrEmpty());
        }

    }
    //private static string checkclocetag(string text)
    //{
    //    if (text.Contains("<size=") ||
    //       text.Contains("<color=") ||
    //       text.Contains("<b>") ||
    //       text.Contains("<i>"))         
    //    {
    //        return text;
    //    }
    //    return text;
    //}

    private static string chatformat(string input)
    {
        if (input.Contains("<size=") || input.Contains("</size>"))
        {
            input = Regex.Replace(input, "<size=((\\d+>)|(\\w+?>?))|<\\/size>?", string.Empty);
        }

        return input;
    }

    private ExitGames.Client.Photon.Hashtable checkGameGUI(string type = "")
    {
        ActiveGameModes.Clear();
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        if (type == "out")
        {
            hashtable = OutsideSettings();
        }
        else if (type == "in")
        {
            hashtable = InsideSettings();
        }
        else if (type == "")
        {
            hashtable = OutsideSettings();
            hashtable.Merge(InsideSettings());
        }

        return hashtable;
    }
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

    private ExitGames.Client.Photon.Hashtable OutsideSettings()
    {
        int num;
        int num2;
        PhotonPlayer player;
        int num3;
        //float num4;
        //float num5;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        if (((int)settings[0xe4]) > 0)
        {
            hashtable.Add("ahssReload", (int)settings[0xe4]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>AHSS Air-Reload</color></b> <color=#7b001c><i> allowed.</i></color>");
        }
        if (((int)settings[0xdb]) > 0)
        {
            hashtable.Add("friendly", (int)settings[0xdb]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>PVP</color></b> <color=#7b001c><i> is allowed.</i></color>");
        }
        if (((int)settings[220]) > 0)
        {
            hashtable.Add("pvp", (int)settings[220]);
            string str = string.Empty;
            if ((int)settings[220] == 1)
            {
                str = "Team-Based";
            }
            else if ((int)settings[220] == 2)
            {
                str = "FFA";
            }
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Blade/AHSS PVP</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + str + "</i></color>" + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
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
        if (((int)settings[0xd8]) > 0)
        {
            hashtable.Add("horse", (int)settings[0xd8]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Horses are </color></b> <color=#7b001c><i>allowed.</i></color>");
        }
        if (((int)settings[200]) > 0)
        {
            settings[0xc0] = 0;
            settings[0xc1] = 0;
            settings[0xe2] = 0;
            settings[220] = 0;
            num = 1;
            if ((!int.TryParse((string)settings[0xc9], out num) || (num > PhotonNetwork.countOfPlayers)) || (num < 0))
            {
                settings[201] = "1";
            }
            hashtable.Add("infection", num);
            if (GameSettings.infectionMode != num)
            {
                imatitan.Clear();
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
                    player.SetCustomProperties(propertiesToSet);
                }
                int length = PhotonNetwork.playerList.Length;
                num3 = num;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    PhotonPlayer player2 = PhotonNetwork.playerList[num2];
                    if ((length > 0) && (UnityEngine.Random.Range((float)0f, (float)1f) <= (((float)num3) / ((float)length))))
                    {
                        ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
                        hashtable3.Add(PhotonPlayerProperty.isTitan, 2);
                        player2.SetCustomProperties(hashtable3);
                        imatitan.Add(player2.ID, 2);
                        num3--;
                    }
                    length--;
                }
            }
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Infection mode</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (((int)settings[0xc0]) > 0)
        {
            hashtable.Add("bomb", (int)settings[0xc0]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>~RC PVP BOMB~</color></b> <color=#7b001c><i>is on.</i></color>");
        }
        if (((int)settings[0xeb]) > 0)
        {
            hashtable.Add("globalDisableMinimap", (int)settings[0xeb]);
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Minimaps are </color></b> <color=#7b001c><i>not allowed.</i></color>");
        }
        if (((int)settings[0xc1]) > 0)
        {
            hashtable.Add("team", (int)settings[0xc1]);
            if (GameSettings.teamMode != ((int)settings[0xc1]))
            {
                num3 = 1;
                for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
                {
                    player = PhotonNetwork.playerList[num2];
                    switch (num3)
                    {
                        case 1:
                            PView.RPC("setTeamRPC", player, new object[] { 1 });
                            num3 = 2;
                            break;

                        case 2:
                            PView.RPC("setTeamRPC", player, new object[] { 2 });
                            num3 = 1;
                            break;
                    }
                }
            }
            string str = string.Empty;
            if ((int)settings[0xc1] == 1)
            {
                str = "no sort";
            }
            else if ((int)settings[0xc1] == 2)
            {
                str = "locked by size";
            }
            else if ((int)settings[0xc1] == 3)
            {
                str = "locked by skill";
            }
            if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Team Mode (" + str + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        return hashtable;
    }
    //private ExitGames.Client.Photon.Hashtable checkGameGUI()
    //{
    //    int num;
    //    int num2;
    //    PhotonPlayer player;
    //    int num4;
    //    float num8;
    //    float num9;
    //    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
    //    if (((int) settings[200]) > 0)
    //    {
    //        settings[0xc0] = 0;
    //        settings[0xc1] = 0;
    //        settings[0xe2] = 0;
    //        settings[220] = 0;
    //        num = 1;
    //        if ((!int.TryParse((string) settings[0xc9], out num) || (num > PhotonNetwork.countOfPlayers)) || (num < 0))
    //        {
    //            settings[0xc9] = "1";
    //        }
    //        hashtable.Add("infection", num);
    //        if (GameSettings.infectionMode != num)
    //        {
    //            imatitan.Clear();
    //            for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
    //            {
    //                player = PhotonNetwork.playerList[num2];
    //                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
    //                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
    //                player.SetCustomProperties(propertiesToSet);
    //            }
    //            int length = PhotonNetwork.playerList.Length;
    //            num4 = num;
    //            for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
    //            {
    //                PhotonPlayer player2 = PhotonNetwork.playerList[num2];
    //                if ((length > 0) && (UnityEngine.Random.Range((float) 0f, (float) 1f) <= (((float) num4) / ((float) length))))
    //                {
    //                    ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
    //                    hashtable3.Add(PhotonPlayerProperty.isTitan, 2);
    //                    player2.SetCustomProperties(hashtable3);
    //                    imatitan.Add(player2.ID, 2);
    //                    num4--;
    //                }
    //                length--;
    //            }
    //        }
    //    }
    //    if (((int) settings[0xc0]) > 0)
    //    {
    //        hashtable.Add("bomb", (int) settings[0xc0]);
    //    }
    //    if (((int) settings[0xeb]) > 0)
    //    {
    //        hashtable.Add("globalDisableMinimap", (int) settings[0xeb]);
    //    }
    //    if (((int) settings[0xc1]) > 0)
    //    {
    //        hashtable.Add("team", (int) settings[0xc1]);
    //        if (GameSettings.teamMode != ((int) settings[0xc1]))
    //        {
    //            num4 = 1;
    //            for (num2 = 0; num2 < PhotonNetwork.playerList.Length; num2++)
    //            {
    //                player = PhotonNetwork.playerList[num2];
    //                switch (num4)
    //                {
    //                    case 1:
    //                        PView.RPC("setTeamRPC", player, new object[] { 1 });
    //                        num4 = 2;
    //                        break;

    //                    case 2:
    //                        PView.RPC("setTeamRPC", player, new object[] { 2 });
    //                        num4 = 1;
    //                        break;
    //                }
    //            }
    //        }
    //    }
    //    if (((int) settings[0xe2]) > 0)
    //    {
    //        num = 50;
    //        if ((!int.TryParse((string) settings[0xe3], out num) || (num > 0x3e8)) || (num < 0))
    //        {
    //            settings[0xe3] = "50";
    //        }
    //        hashtable.Add("point", num);
    //    }
    //    if (((int) settings[0xc2]) > 0)
    //    {
    //        hashtable.Add("rock", (int) settings[0xc2]);
    //    }
    //    if (((int) settings[0xc3]) > 0)
    //    {
    //        num = 30;
    //        if ((!int.TryParse((string) settings[0xc4], out num) || (num > 100)) || (num < 0))
    //        {
    //            settings[0xc4] = "30";
    //        }
    //        hashtable.Add("explode", num);
    //    }
    //    if (((int) settings[0xc5]) > 0)
    //    {
    //        int result = 100;
    //        int num7 = 200;
    //        if ((!int.TryParse((string) settings[0xc6], out result) || (result > 0x186a0)) || (result < 0))
    //        {
    //            settings[0xc6] = "100";
    //        }
    //        if ((!int.TryParse((string) settings[0xc7], out num7) || (num7 > 0x186a0)) || (num7 < 0))
    //        {
    //            settings[0xc7] = "200";
    //        }
    //        hashtable.Add("healthMode", (int) settings[0xc5]);
    //        hashtable.Add("healthLower", result);
    //        hashtable.Add("healthUpper", num7);
    //    }
    //    if (((int) settings[0xca]) > 0)
    //    {
    //        hashtable.Add("eren", (int) settings[0xca]);
    //    }
    //    if (((int) settings[0xcb]) > 0)
    //    {
    //        num = 1;
    //        if ((!int.TryParse((string) settings[0xcc], out num) || (num > 50)) || (num < 0))
    //        {
    //            settings[0xcc] = "1";
    //        }
    //        hashtable.Add("titanc", num);
    //    }
    //    if (((int) settings[0xcd]) > 0)
    //    {
    //        num = 0x3e8;
    //        if ((!int.TryParse((string) settings[0xce], out num) || (num > 0x186a0)) || (num < 0))
    //        {
    //            settings[0xce] = "1000";
    //        }
    //        hashtable.Add("damage", num);
    //    }
    //    if (((int) settings[0xcf]) > 0)
    //    {
    //        num8 = 1f;
    //        num9 = 3f;
    //        if ((!float.TryParse((string) settings[0xd0], out num8) || (num8 > 100f)) || (num8 < 0f))
    //        {
    //            settings[0xd0] = "1.0";
    //        }
    //        if ((!float.TryParse((string) settings[0xd1], out num9) || (num9 > 100f)) || (num9 < 0f))
    //        {
    //            settings[0xd1] = "3.0";
    //        }
    //        hashtable.Add("sizeMode", (int) settings[0xcf]);
    //        hashtable.Add("sizeLower", num8);
    //        hashtable.Add("sizeUpper", num9);
    //    }
    //    if (((int) settings[210]) > 0)
    //    {
    //        num8 = 20f;
    //        num9 = 20f;
    //        float num10 = 20f;
    //        float num11 = 20f;
    //        float num12 = 20f;
    //        if (!(float.TryParse((string) settings[0xd3], out num8) && (num8 >= 0f)))
    //        {
    //            settings[0xd3] = "20.0";
    //        }
    //        if (!(float.TryParse((string) settings[0xd4], out num9) && (num9 >= 0f)))
    //        {
    //            settings[0xd4] = "20.0";
    //        }
    //        if (!(float.TryParse((string) settings[0xd5], out num10) && (num10 >= 0f)))
    //        {
    //            settings[0xd5] = "20.0";
    //        }
    //        if (!(float.TryParse((string) settings[0xd6], out num11) && (num11 >= 0f)))
    //        {
    //            settings[0xd6] = "20.0";
    //        }
    //        if (!(float.TryParse((string) settings[0xd7], out num12) && (num12 >= 0f)))
    //        {
    //            settings[0xd7] = "20.0";
    //        }
    //        if (((((num8 + num9) + num10) + num11) + num12) > 100f)
    //        {
    //            settings[0xd3] = "20.0";
    //            settings[0xd4] = "20.0";
    //            settings[0xd5] = "20.0";
    //            settings[0xd6] = "20.0";
    //            settings[0xd7] = "20.0";
    //            num8 = 20f;
    //            num9 = 20f;
    //            num10 = 20f;
    //            num11 = 20f;
    //            num12 = 20f;
    //        }
    //        hashtable.Add("spawnMode", (int) settings[210]);
    //        hashtable.Add("nRate", num8);
    //        hashtable.Add("aRate", num9);
    //        hashtable.Add("jRate", num10);
    //        hashtable.Add("cRate", num11);
    //        hashtable.Add("pRate", num12);
    //    }
    //    if (((int) settings[0xd8]) > 0)
    //    {
    //        hashtable.Add("horse", (int) settings[0xd8]);
    //    }
    //    if (((int) settings[0xd9]) > 0)
    //    {
    //        num = 1;
    //        if (!(int.TryParse((string) settings[0xda], out num) && (num <= 50)))
    //        {
    //            settings[0xda] = "1";
    //        }
    //        hashtable.Add("waveModeOn", (int) settings[0xd9]);
    //        hashtable.Add("waveModeNum", num);
    //    }
    //    if (((int) settings[0xdb]) > 0)
    //    {
    //        hashtable.Add("friendly", (int) settings[0xdb]);
    //    }
    //    if (((int) settings[220]) > 0)
    //    {
    //        hashtable.Add("pvp", (int) settings[220]);
    //    }
    //    if (((int) settings[0xdd]) > 0)
    //    {
    //        num = 20;
    //        if ((!int.TryParse((string) settings[0xde], out num) || (num > 0xf4240)) || (num < 0))
    //        {
    //            settings[0xde] = "20";
    //        }
    //        hashtable.Add("maxwave", num);
    //    }
    //    if (((int) settings[0xdf]) > 0)
    //    {
    //        num = 5;
    //        if ((!int.TryParse((string) settings[0xe0], out num) || (num > 0xf4240)) || (num < 5))
    //        {
    //            settings[0xe0] = "5";
    //        }
    //        hashtable.Add("endless", num);
    //    }
    //    if (((string) settings[0xe1]) != string.Empty)
    //    {
    //        hashtable.Add("motd", (string) settings[0xe1]);
    //    }
    //    if (((int) settings[0xe4]) > 0)
    //    {
    //        hashtable.Add("ahssReload", (int) settings[0xe4]);
    //    }
    //    if (((int) settings[0xe5]) > 0)
    //    {
    //        hashtable.Add("punkWaves", (int) settings[0xe5]);
    //    }
    //    if (((int) settings[0x105]) > 0)
    //    {
    //        hashtable.Add("deadlycannons", (int) settings[0x105]);
    //    }
    //    if (GameSettings.racingStatic > 0)
    //    {
    //        hashtable.Add("asoracing", 1);
    //    }
    //    return hashtable;
    //}

    private bool checkIsTitanAllDie()
    {
        foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("titan"))
        {
            if ((obj2.GetComponent<TITAN>() != null) && !obj2.GetComponent<TITAN>().hasDie)
            {
                return false;
            }
            if (obj2.GetComponent<FEMALE_TITAN>() != null)
            {
                return false;
            }
        }
        return true;
    }

    public void checkPVPpts()
    {
        if (this.PVPtitanScore >= this.PVPtitanScoreMax)
        {
            this.PVPtitanScore = this.PVPtitanScoreMax;
            this.gameLose2();
        }
        else if (this.PVPhumanScore >= this.PVPhumanScoreMax)
        {
            this.PVPhumanScore = this.PVPhumanScoreMax;
            this.gameWin2();
        }
    }



    [RPC]
    private void clearlevel(string[] link, int gametype, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            switch (gametype)
            {
                case 0:
                    IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.KILL_TITAN;
                    break;
                case 1:
                    IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.SURVIVE_MODE;
                    break;
                case 2:
                    IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.PVP_AHSS;
                    break;
                case 3:
                    IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.RACING;
                    break;
                case 4:
                    IN_GAME_MAIN_CAMERA.gamemode = GAMEMODE.None;
                    break;
            }
            if (info.sender.isMasterClient && (link.Length > 6))
            {
                base.StartCoroutine(this.clearlevelE(link));
            }
        }
    }

    private IEnumerator clearlevelE(string[] skybox)
    {
        string key = skybox[6];
        bool mipmap = true;
        bool iteratorVariable2 = false;
        if (((int)settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        if (((((skybox[0] != string.Empty) || (skybox[1] != string.Empty)) || ((skybox[2] != string.Empty) || (skybox[3] != string.Empty))) || (skybox[4] != string.Empty)) || (skybox[5] != string.Empty))
        {
            string iteratorVariable3 = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(iteratorVariable3))
            {
                iteratorVariable2 = true;
                Material material = IN_GAME_MAIN_CAMERA.skybox.material;
                string url = skybox[0];
                string iteratorVariable6 = skybox[1];
                string iteratorVariable7 = skybox[2];
                string iteratorVariable8 = skybox[3];
                string iteratorVariable9 = skybox[4];
                string iteratorVariable10 = skybox[5];
                if ((url.EndsWith(".jpg") || url.EndsWith(".png")) || url.EndsWith(".jpeg"))
                {
                    WWW link = new WWW(url);
                    yield return link;
                    Texture2D texture = RCextensions.loadimage(link, mipmap, 0x7a120);
                    link.Dispose();
                    material.SetTexture("_FrontTex", texture);
                }
                if ((iteratorVariable6.EndsWith(".jpg") || iteratorVariable6.EndsWith(".png")) || iteratorVariable6.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable13 = new WWW(iteratorVariable6);
                    yield return iteratorVariable13;
                    Texture2D iteratorVariable14 = RCextensions.loadimage(iteratorVariable13, mipmap, 0x7a120);
                    iteratorVariable13.Dispose();
                    material.SetTexture("_BackTex", iteratorVariable14);
                }
                if ((iteratorVariable7.EndsWith(".jpg") || iteratorVariable7.EndsWith(".png")) || iteratorVariable7.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable15 = new WWW(iteratorVariable7);
                    yield return iteratorVariable15;
                    Texture2D iteratorVariable16 = RCextensions.loadimage(iteratorVariable15, mipmap, 0x7a120);
                    iteratorVariable15.Dispose();
                    material.SetTexture("_LeftTex", iteratorVariable16);
                }
                if ((iteratorVariable8.EndsWith(".jpg") || iteratorVariable8.EndsWith(".png")) || iteratorVariable8.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable17 = new WWW(iteratorVariable8);
                    yield return iteratorVariable17;
                    Texture2D iteratorVariable18 = RCextensions.loadimage(iteratorVariable17, mipmap, 0x7a120);
                    iteratorVariable17.Dispose();
                    material.SetTexture("_RightTex", iteratorVariable18);
                }
                if ((iteratorVariable9.EndsWith(".jpg") || iteratorVariable9.EndsWith(".png")) || iteratorVariable9.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable19 = new WWW(iteratorVariable9);
                    yield return iteratorVariable19;
                    Texture2D iteratorVariable20 = RCextensions.loadimage(iteratorVariable19, mipmap, 0x7a120);
                    iteratorVariable19.Dispose();
                    material.SetTexture("_UpTex", iteratorVariable20);
                }
                if ((iteratorVariable10.EndsWith(".jpg") || iteratorVariable10.EndsWith(".png")) || iteratorVariable10.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable21 = new WWW(iteratorVariable10);
                    yield return iteratorVariable21;
                    Texture2D iteratorVariable22 = RCextensions.loadimage(iteratorVariable21, mipmap, 0x7a120);
                    iteratorVariable21.Dispose();
                    material.SetTexture("_DownTex", iteratorVariable22);
                }
                IN_GAME_MAIN_CAMERA.skybox.material = material;
                linkHash[1].Add(iteratorVariable3, material);
                skyMaterial = material;
            }
            else
            {
                IN_GAME_MAIN_CAMERA.skybox.material = (Material)linkHash[1][iteratorVariable3];
                skyMaterial = (Material)linkHash[1][iteratorVariable3];
            }
        }
        if ((key.EndsWith(".jpg") || key.EndsWith(".png")) || key.EndsWith(".jpeg"))
        {
            foreach (GameObject iteratorVariable23 in this.groundList)
            {
                if ((iteratorVariable23 != null) && (iteratorVariable23.renderer != null))
                {
                    foreach (Renderer iteratorVariable24 in iteratorVariable23.GetComponentsInChildren<Renderer>())
                    {
                        if (!linkHash[0].ContainsKey(key))
                        {
                            WWW iteratorVariable25 = new WWW(key);
                            yield return iteratorVariable25;
                            Texture2D iteratorVariable26 = RCextensions.loadimage(iteratorVariable25, mipmap, 0x30d40);
                            iteratorVariable25.Dispose();
                            if (!linkHash[0].ContainsKey(key))
                            {
                                iteratorVariable2 = true;
                                iteratorVariable24.material.mainTexture = iteratorVariable26;
                                linkHash[0].Add(key, iteratorVariable24.material);
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                            else
                            {
                                iteratorVariable24.material = (Material)linkHash[0][key];
                            }
                        }
                        else
                        {
                            iteratorVariable24.material = (Material)linkHash[0][key];
                        }
                    }
                }
            }
        }
        else if (key.ToLower() == "transparent")
        {
            foreach (GameObject obj2 in this.groundList)
            {
                if ((obj2 != null) && (obj2.renderer != null))
                {
                    foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                    {
                        renderer.enabled = false;
                    }
                }
            }
        }
        if (iteratorVariable2)
        {
            this.unloadAssets();
        }
    }

    public void compileScript(string str)
    {
        int num3;
        string[] strArray2 = str.Replace(" ", string.Empty).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        int num = 0;
        int num2 = 0;
        bool flag = false;
        for (num3 = 0; num3 < strArray2.Length; num3++)
        {
            if (strArray2[num3] == "{")
            {
                num++;
            }
            else if (strArray2[num3] == "}")
            {
                num2++;
            }
            else
            {
                int num4 = 0;
                int num5 = 0;
                int num6 = 0;
                foreach (char ch in strArray2[num3])
                {
                    switch (ch)
                    {
                        case '(':
                            num4++;
                            break;

                        case ')':
                            num5++;
                            break;

                        case '"':
                            num6++;
                            break;
                    }
                }
                if (num4 != num5)
                {
                    int num8 = num3 + 1;
                    InRoomChat.ChatInstanse.addLINE("Script Error: Parentheses not equal! (line " + num8.ToString() + ")");
                    flag = true;
                }
                if ((num6 % 2) != 0)
                {
                    InRoomChat.ChatInstanse.addLINE("Script Error: Quotations not equal! (line " + ((num3 + 1)).ToString() + ")");
                    flag = true;
                }
            }
        }
        if (num != num2)
        {
            InRoomChat.ChatInstanse.addLINE("Script Error: Bracket count not equivalent!");
            flag = true;
        }
        if (!flag)
        {
            try
            {
                int num10;
                num3 = 0;
                while (num3 < strArray2.Length)
                {
                    if (strArray2[num3].StartsWith("On") && (strArray2[num3 + 1] == "{"))
                    {
                        int key = num3;
                        num10 = num3 + 2;
                        int num11 = 0;
                        for (int i = num3 + 2; i < strArray2.Length; i++)
                        {
                            if (strArray2[i] == "{")
                            {
                                num11++;
                            }
                            if (strArray2[i] == "}")
                            {
                                if (num11 > 0)
                                {
                                    num11--;
                                }
                                else
                                {
                                    num10 = i - 1;
                                    i = strArray2.Length;
                                }
                            }
                        }
                        hashtable.Add(key, num10);
                        num3 = num10;
                    }
                    num3++;
                }
                foreach (int num9 in hashtable.Keys)
                {
                    int num14;
                    int num15;
                    string str4;
                    string str5;
                    RegionTrigger trigger;
                    string str3 = strArray2[num9];
                    num10 = (int)hashtable[num9];
                    string[] stringArray = new string[(num10 - num9) + 1];
                    int index = 0;
                    for (num3 = num9; num3 <= num10; num3++)
                    {
                        stringArray[index] = strArray2[num3];
                        index++;
                    }
                    RCEvent event2 = this.parseBlock(stringArray, 0, 0, null);
                    if (str3.StartsWith("OnPlayerEnterRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.playerEventEnter = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                playerEventEnter = event2,
                                myName = str4
                            };
                            RCRegionTriggers.Add(str4, trigger);
                        }
                        RCVariableNames.Add("OnPlayerEnterRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnPlayerLeaveRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.playerEventExit = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                playerEventExit = event2,
                                myName = str4
                            };
                            RCRegionTriggers.Add(str4, trigger);
                        }
                        RCVariableNames.Add("OnPlayerExitRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnTitanEnterRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.titanEventEnter = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                titanEventEnter = event2,
                                myName = str4
                            };
                            RCRegionTriggers.Add(str4, trigger);
                        }
                        RCVariableNames.Add("OnTitanEnterRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnTitanLeaveRegion"))
                    {
                        num14 = str3.IndexOf('[');
                        num15 = str3.IndexOf(']');
                        str4 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        num14 = str3.IndexOf('(');
                        num15 = str3.IndexOf(')');
                        str5 = str3.Substring(num14 + 2, (num15 - num14) - 3);
                        if (RCRegionTriggers.ContainsKey(str4))
                        {
                            trigger = (RegionTrigger)RCRegionTriggers[str4];
                            trigger.titanEventExit = event2;
                            trigger.myName = str4;
                            RCRegionTriggers[str4] = trigger;
                        }
                        else
                        {
                            trigger = new RegionTrigger
                            {
                                titanEventExit = event2,
                                myName = str4
                            };
                            RCRegionTriggers.Add(str4, trigger);
                        }
                        RCVariableNames.Add("OnTitanExitRegion[" + str4 + "]", str5);
                    }
                    else if (str3.StartsWith("OnFirstLoad()"))
                    {
                        RCEvents.Add("OnFirstLoad", event2);
                    }
                    else if (str3.StartsWith("OnRoundStart()"))
                    {
                        RCEvents.Add("OnRoundStart", event2);
                    }
                    else if (str3.StartsWith("OnUpdate()"))
                    {
                        RCEvents.Add("OnUpdate", event2);
                    }
                    else
                    {
                        string[] strArray4;
                        if (str3.StartsWith("OnTitanDie"))
                        {
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, (num15 - num14) - 1).Split(new char[] { ',' });
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnTitanDie", strArray4);
                            RCEvents.Add("OnTitanDie", event2);
                        }
                        else if (str3.StartsWith("OnPlayerDieByTitan"))
                        {
                            RCEvents.Add("OnPlayerDieByTitan", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, (num15 - num14) - 1).Split(new char[] { ',' });
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByTitan", strArray4);
                        }
                        else if (str3.StartsWith("OnPlayerDieByPlayer"))
                        {
                            RCEvents.Add("OnPlayerDieByPlayer", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            strArray4 = str3.Substring(num14 + 1, (num15 - num14) - 1).Split(new char[] { ',' });
                            strArray4[0] = strArray4[0].Substring(1, strArray4[0].Length - 2);
                            strArray4[1] = strArray4[1].Substring(1, strArray4[1].Length - 2);
                            RCVariableNames.Add("OnPlayerDieByPlayer", strArray4);
                        }
                        else if (str3.StartsWith("OnChatInput"))
                        {
                            RCEvents.Add("OnChatInput", event2);
                            num14 = str3.IndexOf('(');
                            num15 = str3.LastIndexOf(')');
                            str5 = str3.Substring(num14 + 1, (num15 - num14) - 1);
                            RCVariableNames.Add("OnChatInput", str5.Substring(1, str5.Length - 2));
                        }
                    }
                }
            }
            catch (UnityException exception)
            {
                InRoomChat.ChatInstanse.addLINE(exception.Message);
            }
        }
    }

    public int conditionType(string str)
    {
        if (!str.StartsWith("Int"))
        {
            if (str.StartsWith("Bool"))
            {
                return 1;
            }
            if (str.StartsWith("String"))
            {
                return 2;
            }
            if (str.StartsWith("Float"))
            {
                return 3;
            }
            if (str.StartsWith("Titan"))
            {
                return 5;
            }
            if (str.StartsWith("Player"))
            {
                return 4;
            }
        }
        return 0;
    }

    //public string getStateString(string checkpt)
    //{
    //    switch (this.state)
    //    {
    //        case CheckPointState.Human:
    //            return "[" + ColorSet.color_human + "]H[-]";
    //        case CheckPointState.Titan:
    //            return "[" + ColorSet.color_titan_player + "]T[-]";
    //    }
    //    if (this.humanPt == this.humanPtMax)
    //    {
    //        this.state = CheckPointState.Human;
    //        return "[" + ColorSet.color_human + "]H[-]";
    //    }
    //    if (this.titanPt == this.titanPtMax)
    //    {
    //        this.state = CheckPointState.Titan;
    //        return "[" + ColorSet.color_titan_player + "]T[-]";
    //    }
    //    return "[" + ColorSet.color_D + "]_[-]";
    //}

    private void core()
    {
        if (((int)settings[0x40]) >= 100)
        {
            this.coreeditor();
            return;
        }
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && this.needChooseSide)
        {
            if (FengCustomInputs.Inputs.isInputDown[InputCode.flare1])
            {
                if (NGUITools.GetActive(this.uirefer.panels[3]))
                {
                    Screen.lockCursor = true;
                    Screen.showCursor = true;
                    NGUITools.SetActive(this.uirefer.panels[0], true);
                    NGUITools.SetActive(this.uirefer.panels[1], false);
                    NGUITools.SetActive(this.uirefer.panels[2], false);
                    NGUITools.SetActive(this.uirefer.panels[3], false);
                    IN_GAME_MAIN_CAMERA.spectate.disable = false;
                    IN_GAME_MAIN_CAMERA.mouselook.disable = false;
                }
                else
                {
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    NGUITools.SetActive(this.uirefer.panels[0], false);
                    NGUITools.SetActive(this.uirefer.panels[1], false);
                    NGUITools.SetActive(this.uirefer.panels[2], false);
                    NGUITools.SetActive(this.uirefer.panels[3], true);
                    IN_GAME_MAIN_CAMERA.spectate.disable = true;
                    IN_GAME_MAIN_CAMERA.mouselook.disable = true;
                }
            }
            if (FengCustomInputs.Inputs.isInputDown[15] && !NGUITools.GetActive(this.uirefer.panels[3]))
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
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER))
        {
            string str2;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                this.coreadd();
                //this.ShowHUDInfoTopLeft(this.playerList);
                string content = string.Empty;
                if ((IN_GAME_MAIN_CAMERA.mainG != null && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING)) && (IN_GAME_MAIN_CAMERA.mainCamera.gameOver && !this.needChooseSide))
                {
                    this.ShowHUDInfoCenter(string.Concat(new string[]
                {
                    "Press [F7D358]",
                    FengCustomInputs.Inputs.inputString[InputCode.flare1],
                    "[-] to spectate the next player. \nPress [F7D358]",
                    FengCustomInputs.Inputs.inputString[InputCode.flare2],
                    "[-] to spectate the previous player.\nPress [F7D358]",
                    FengCustomInputs.Inputs.inputString[InputCode.attack1],
                    "[-] to enter the spectator mode.\n\n\n\n"
                }));
                    if (LevelInfo.getInfo(level).respawnMode == RespawnMode.DEATHMATCH || (GameSettings.endlessMode > 0) || (((GameSettings.bombMode == 1) || (GameSettings.pvpMode > 0)) && (GameSettings.pointMode > 0)))
                    {
                        this.myRespawnTime += Time.deltaTime;
                        int endlessMode = 5;
                        if (PhotonNetwork.player.isTitan)
                        {
                            endlessMode = 10;
                        }
                        if (GameSettings.endlessMode > 0)
                        {
                            endlessMode = GameSettings.endlessMode;
                        }
                        this.ShowHUDInfoCenterADD("Respawn in " + (endlessMode - (int)this.myRespawnTime) + "s.");
                        if (this.myRespawnTime > endlessMode)
                        {
                            this.myRespawnTime = 0f;
                            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
                            if (PhotonNetwork.player.isTitan)
                            {
                                this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
                            }
                            else
                            {
                                base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
                            }
                            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
                            this.ShowHUDInfoCenter(string.Empty);
                        }
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                {
                    if (!this.isLosing && IN_GAME_MAIN_CAMERA.main_objectR != null)
                    {
                        this.currentSpeed = IN_GAME_MAIN_CAMERA.main_objectR.velocity.magnitude;
                        this.maxSpeed = Mathf.Max(this.maxSpeed, this.currentSpeed);
                        this.ShowHUDInfoTopLeft(string.Concat(new object[] {
                            "Current Speed : ",
                            (int)this.currentSpeed,
                            "\nMax Speed:",
                            this.maxSpeed }));
                    }
                }
                else
                {
                    this.ShowHUDInfoTopLeft(string.Concat(new object[] {
                        "Kills:",
                        this.single_kills,
                        "\nMax Damage:",
                        this.single_maxDamage,
                        "\nTotal Damage:",
                        this.single_totalDamage }));
                }
            }
            if (this.isLosing && IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        this.ShowHUDInfoCenter(string.Concat(new object[] {
                            "Survive ",
                            this.wave,
                            " Waves!\n Press ",
                            FengCustomInputs.Inputs.inputString[InputCode.restart],
                            " to Restart.\n\n\n" }));
                    }
                    else
                    {
                        this.ShowHUDInfoCenter("Humanity Fail!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
                    }
                }
                else
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        this.ShowHUDInfoCenter(string.Concat(new object[] { "Survive ", this.wave, " Waves!\nGame Restart in ", (int)this.gameEndCD, "s\n\n" }));
                    }
                    else
                    {
                        this.ShowHUDInfoCenter("Humanity Fail!\nAgain!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
                    }
                    if (this.gameEndCD <= 0f)
                    {
                        this.gameEndCD = 0f;
                        if (PhotonNetwork.isMasterClient)
                        {
                            this.restartRC();
                        }
                        this.ShowHUDInfoCenter(string.Empty);
                    }
                    else
                    {
                        this.gameEndCD -= Time.deltaTime;
                    }
                }
            }
            if (this.isWinning)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        this.ShowHUDInfoCenter((((((int)(this.timeTotalServer * 10f)) * 0.1f) - 5f)).ToString() + "s !\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        this.ShowHUDInfoCenter("Survive All Waves!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
                    }
                    else
                    {
                        this.ShowHUDInfoCenter("Humanity Win!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
                    }
                }
                else
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                    {
                        this.ShowHUDInfoCenter(string.Concat(new object[] { this.localRacingResult, "\n\nGame Restart in ", (int)this.gameEndCD, "s" }));
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        this.ShowHUDInfoCenter("Survive All Waves!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
                    }
                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                    {
                        this.ShowHUDInfoCenter(string.Concat(new object[] { "Team ", this.teamWinner, " Win!\nGame Restart in ", (int)this.gameEndCD, "s\n\n" }));
                    }
                    else
                    {
                        this.ShowHUDInfoCenter("Humanity Win!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
                    }
                    if (this.gameEndCD <= 0f)
                    {
                        this.gameEndCD = 0f;
                        if (PhotonNetwork.isMasterClient)
                        {
                            this.restartGame2(false);
                        }
                        this.ShowHUDInfoCenter(string.Empty);
                    }
                    else
                    {
                        this.gameEndCD -= Time.deltaTime;
                    }
                }
            }
            this.timeElapse += Time.deltaTime;
            this.roundTime += Time.deltaTime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
                {
                    if (!this.isWinning)
                    {
                        this.timeTotalServer += Time.deltaTime;
                    }
                }
                else if (!this.isLosing && !this.isWinning)
                {
                    this.timeTotalServer += Time.deltaTime;
                }
            }
            else
            {
                this.timeTotalServer += Time.deltaTime;
            }
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (!this.isWinning)
                    {
                        this.ShowHUDInfoTopCenter("Time : " + ((((int)(this.timeTotalServer * 10f)) * 0.1f) - 5f));
                    }
                    if (this.timeTotalServer < 5f)
                    {
                        this.ShowHUDInfoCenter("RACE START IN " + ((int)(5f - this.timeTotalServer)));
                    }
                    else if (!this.startRacing)
                    {
                        this.ShowHUDInfoCenter(string.Empty);
                        this.startRacing = true;
                        this.endRacing = false;
                        BRM.CacheGameObject.Find("door").SetActive(false);
                    }
                }
                else
                {
                    this.ShowHUDInfoTopCenter("Time : " + ((this.roundTime >= 20f) ? (((((int)(this.roundTime * 10f)) * 0.1f) - 20f)).ToString() : "WAITING"));
                    if (this.roundTime < 20f)
                    {
                        this.ShowHUDInfoCenter("RACE START IN " + ((int)(20f - this.roundTime)) + (!(this.localRacingResult == string.Empty) ? ("\nLast Round\n" + this.localRacingResult) : "\n\n"));
                    }
                    else if (!this.startRacing)
                    {
                        this.ShowHUDInfoCenter(string.Empty);
                        this.startRacing = true;
                        this.endRacing = false;
                        GameObject doors = CacheGameObject.Find("door");
                        if (doors != null) doors.SetActive(false);
                        if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                        {
                            foreach (GameObject go in this.racingDoors)
                                go.SetActive(false);
                            this.racingDoors = null;
                        }


                    }
                    else if (this.racingDoors != null && FengGameManagerMKII.customLevelLoaded)
                    {
                        foreach (GameObject go in this.racingDoors)
                            go.SetActive(false);
                        this.racingDoors = null;
                    }
                }
                if (IN_GAME_MAIN_CAMERA.mainCamera.gameOver && !this.needChooseSide)
                {
                    this.myRespawnTime += Time.deltaTime;
                    if (this.myRespawnTime > 1.5f)
                    {
                        this.myRespawnTime = 0f;
                        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
                        if (this.checkpoint != null)
                        {
                            base.StartCoroutine(this.WaitAndRespawn2(0.1f, this.checkpoint));
                        }
                        else
                        {
                            base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
                        }
                        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
                        this.ShowHUDInfoCenter(string.Empty);
                    }
                }
            }
            if (this.timeElapse > 1f)
            {
                this.timeElapse -= 1f;
                string text = string.Empty;
                GAMEMODE gamemode2 = IN_GAME_MAIN_CAMERA.gamemode;
                string text2;
                switch (gamemode2)
                {
                    case GAMEMODE.KILL_TITAN:
                        text = "Titan Left: " + FengGameManagerMKII.alltitans.Count + "  Time : ";
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            text += (int)this.timeTotalServer;
                        }
                        else
                        {
                            text += this.time - (int)this.timeTotalServer;
                        }
                        break;
                    case GAMEMODE.PVP_AHSS:
                    case GAMEMODE.CAGE_FIGHT:
                        break;
                    case GAMEMODE.ENDLESS_TITAN:
                        text = "Time : " + (this.time - (int)this.timeTotalServer);
                        break;
                    case GAMEMODE.SURVIVE_MODE:
                        text = string.Concat(new object[]
                        {
                    "Titan Left: ",
                    FengGameManagerMKII.titans.Count.ToString(),
                    " Wave : ",
                    this.wave
                        });
                        break;
                    case GAMEMODE.BOSS_FIGHT_CT:
                        text = "Time : " + (this.time - (int)this.timeTotalServer) + "\nDefeat the Colossal Titan.\nPrevent abnormal titan from running to the north gate";
                        break;
                    default:
                        if (gamemode2 == GAMEMODE.PVP_CAPTURE)
                        {
                            text2 = "| ";
                            for (int i = 0; i < PVPcheckPoint.chkPts.Count; i++)
                            {
                                text2 = text2 + (PVPcheckPoint.chkPts[i] as PVPcheckPoint).getStateString() + " ";
                            }
                            text2 += "|";
                            text = string.Concat(new object[] { this.PVPtitanScoreMax - this.PVPtitanScore, "  ", text2, "  ", this.PVPhumanScoreMax - this.PVPhumanScore, "\n" }) + "Time : " + (this.time - ((int)this.timeTotalServer)).ToString();
                        }
                        break;
                }
                this.ShowHUDInfoTopCenter(text);
                text = string.Empty;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                    {
                        text = "Time : " + (int)this.timeTotalServer;
                    }
                    this.ShowHUDInfoTopRight(text);
                }
                text2 = ((IN_GAME_MAIN_CAMERA.difficulty >= 0) ? ((IN_GAME_MAIN_CAMERA.difficulty != 0) ? ((IN_GAME_MAIN_CAMERA.difficulty != 1) ? "Abnormal" : "Hard") : "Normal") : "Trainning");
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.CAGE_FIGHT)
                {
                    this.ShowHUDInfoTopRightMAPNAME(string.Concat(new object[]
                    {
                    (int)this.roundTime,
                    "s\n",
                    FengGameManagerMKII.level,
                    " : ",
                    text2
                    }));
                }
                else
                {
                    this.ShowHUDInfoTopRightMAPNAME("\n" + FengGameManagerMKII.level + " : " + text2);
                }
                this.ShowHUDInfoTopRightMAPNAME("\nCamera(" + FengCustomInputs.Inputs.inputString[InputCode.camera] + "):" + IN_GAME_MAIN_CAMERA.cameraMode.ToString());
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && this.needChooseSide)
                {
                    this.ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
                }
            }
            if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) &&
                (this.killInfoGO.Count > 0)) && (this.killInfoGO[0] == null))
            {
                this.killInfoGO.RemoveAt(0);
            }
            if (((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && PhotonNetwork.isMasterClient) && (this.timeTotalServer > this.time))
            {
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                Screen.lockCursor = (gameStart = !(Screen.showCursor = true));
                string text3 = string.Empty;
                string text4 = string.Empty;
                string text5 = string.Empty;
                string text6 = string.Empty;
                string text7 = string.Empty;
                string text8 = string.Empty;
                foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList)
                {
                    ExitGames.Client.Photon.Hashtable customProperties;
                    if (photonPlayer != null && (customProperties = photonPlayer.customProperties) != null)
                    {
                        text3 = text3 + customProperties["name"] + "\n";
                        text4 = text4 + customProperties["kills"] + "\n";
                        text5 = text5 + customProperties["deaths"] + "\n";
                        text6 = text6 + customProperties["max_dmg"] + "\n";
                        text7 = text7 + customProperties["total_dmg"] + "\n";
                    }
                }
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
                {
                    text8 = string.Empty;
                    for (int k = 0; k < this.teamScores.Length; k++)
                    {
                        text8 += ((k == 0) ? string.Concat(new object[]
                        {
                        "Team",
                        (k + 1).ToString(),
                        " ",
                        this.teamScores[k],
                        " "
                        }) : " : ");
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    text8 = "Highest Wave : " + this.highestwave;
                }
                else
                {
                    text8 = string.Concat(new object[]
                    {
                    "Humanity ",
                    this.humanScore,
                    " : Titan ",
                    this.titanScore
                    });
                }
                FengGameManagerMKII.PView.RPC("showResult", PhotonTargets.AllBuffered, new object[]
                {
                text3,
                text4,
                text5,
                text6,
                text7,
                text8
                });
            }
        }
        this.core2();
    }

    //private void core2()
    //{
    //    this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    //    if (((int)settings[0x40]) >= 100)
    //    {
    //        this.coreeditor();
    //        return;
    //    }
    //    else
    //    {
    //        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && this.needChooseSide)
    //        {
    //            if (FengCustomInputs.Inputs.isInputDown[InputCode.flare1])
    //            {
    //                if (NGUITools.GetActive(this.uirefer.panels[3]))
    //                {
    //                    Screen.lockCursor = true;
    //                    Screen.showCursor = true;
    //                    NGUITools.SetActive(this.uirefer.panels[0], true);
    //                    NGUITools.SetActive(this.uirefer.panels[1], false);
    //                    NGUITools.SetActive(this.uirefer.panels[2], false);
    //                    NGUITools.SetActive(this.uirefer.panels[3], false);
    //                    Camera.main.GetComponent<SpectatorMovement>().disable = false;
    //                    Camera.main.GetComponent<MouseLook>().disable = false;
    //                }
    //                else
    //                {
    //                    Screen.lockCursor = false;
    //                    Screen.showCursor = true;
    //                    NGUITools.SetActive(this.uirefer.panels[0], false);
    //                    NGUITools.SetActive(this.uirefer.panels[1], false);
    //                    NGUITools.SetActive(this.uirefer.panels[2], false);
    //                    NGUITools.SetActive(this.uirefer.panels[3], true);
    //                    Camera.main.GetComponent<SpectatorMovement>().disable = true;
    //                    Camera.main.GetComponent<MouseLook>().disable = true;
    //                }
    //            }
    //            //if (FengCustomInputs.Inputs.isInputDown[15] && !FengCustomInputs.Inputs.menuOn)
    //            //{
    //            //    Screen.showCursor = true;
    //            //    Screen.lockCursor = false;
    //            //    Camera.main.GetComponent<SpectatorMovement>().disable = true;
    //            //    Camera.main.GetComponent<MouseLook>().disable = true;
    //            //    FengCustomInputs.Inputs.menuOn = true;
    //            //}
    //            if (FengCustomInputs.Inputs.isInputDown[15] && !NGUITools.GetActive(this.uirefer.panels[3]))
    //            {

    //                NGUITools.SetActive(this.uirefer.panels[0], false);
    //                NGUITools.SetActive(this.uirefer.panels[1], true);
    //                NGUITools.SetActive(this.uirefer.panels[2], false);
    //                NGUITools.SetActive(this.uirefer.panels[3], false);
    //                FengCustomInputs.Inputs.showKeyMap();
    //                FengCustomInputs.Inputs.justUPDATEME();
    //                Screen.lockCursor = !(Screen.showCursor = (FengCustomInputs.Inputs.menuOn = (IN_GAME_MAIN_CAMERA.mouselook.disable = (IN_GAME_MAIN_CAMERA.spectate.disable = true))));
    //            }
    //        }
    //        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER))
    //        {
    //            int length;
    //            float num3;
    //            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
    //            {
    //                this.coreadd();
    //                this.ShowHUDInfoTopLeft(this.playerList);
    //                if ((((IN_GAME_MAIN_CAMERA.mainG != null) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING)) && (IN_GAME_MAIN_CAMERA.mainCamera.gameOver && !this.needChooseSide)) && (((int)settings[0xf5]) == 0))
    //                {
    //                    this.ShowHUDInfoCenter("Press [F7D358]" + FengCustomInputs.Inputs.inputString[InputCode.flare1] + "[-] to spectate the next player. \nPress [F7D358]" + FengCustomInputs.Inputs.inputString[InputCode.flare2] + "[-] to spectate the previous player.\nPress [F7D358]" + FengCustomInputs.Inputs.inputString[InputCode.attack1] + "[-] to enter the spectator mode.\n\n\n\n");
    //                    if (((LevelInfo.getInfo(level).respawnMode == RespawnMode.DEATHMATCH) || (GameSettings.endlessMode > 0)) || (((GameSettings.bombMode == 1) || (GameSettings.pvpMode > 0)) && (GameSettings.pointMode > 0)))
    //                    {
    //                        this.myRespawnTime += Time.deltaTime;
    //                        int endlessMode = 5;
    //                        if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
    //                        {
    //                            endlessMode = 10;
    //                        }
    //                        if (GameSettings.endlessMode > 0)
    //                        {
    //                            endlessMode = GameSettings.endlessMode;
    //                        }
    //                        length = endlessMode - ((int)this.myRespawnTime);
    //                        this.ShowHUDInfoCenterADD("Respawn in " + length.ToString() + "s.");
    //                        if (this.myRespawnTime > endlessMode)
    //                        {
    //                            this.myRespawnTime = 0f;
    //                            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
    //                            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
    //                            {
    //                                this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
    //                            }
    //                            else
    //                            {
    //                                base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
    //                            }
    //                            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
    //                            this.ShowHUDInfoCenter(string.Empty);
    //                        }
    //                    }
    //                }
    //            }
    //            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //            {
    //                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
    //                {
    //                    if (!this.isLosing)
    //                    {
    //                        this.currentSpeed = IN_GAME_MAIN_CAMERA.main_objectR.velocity.magnitude;
    //                        this.maxSpeed = Mathf.Max(this.maxSpeed, this.currentSpeed);
    //                        this.ShowHUDInfoTopLeft(string.Concat(new object[] { "Current Speed : ", (int)this.currentSpeed, "\nMax Speed:", this.maxSpeed }));
    //                    }
    //                }
    //                else
    //                {
    //                    this.ShowHUDInfoTopLeft(string.Concat(new object[] { "Kills:", this.single_kills, "\nMax Damage:", this.single_maxDamage, "\nTotal Damage:", this.single_totalDamage }));
    //                }
    //            }
    //            if (this.isLosing && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.RACING))
    //            {
    //                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        this.ShowHUDInfoCenter(string.Concat(new object[] { "Survive ", this.wave, " Waves!\n Press ", FengCustomInputs.Inputs.inputString[InputCode.restart], " to Restart.\n\n\n" }));
    //                    }
    //                    else
    //                    {
    //                        this.ShowHUDInfoCenter("Humanity Fail!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
    //                    }
    //                }
    //                else
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        this.ShowHUDInfoCenter(string.Concat(new object[] { "Survive ", this.wave, " Waves!\nGame Restart in ", (int)this.gameEndCD, "s\n\n" }));
    //                    }
    //                    else
    //                    {
    //                        this.ShowHUDInfoCenter("Humanity Fail!\nAgain!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
    //                    }
    //                    if (this.gameEndCD <= 0f)
    //                    {
    //                        this.gameEndCD = 0f;
    //                        if (PhotonNetwork.isMasterClient)
    //                        {
    //                            this.restartRC();
    //                        }
    //                        this.ShowHUDInfoCenter(string.Empty);
    //                    }
    //                    else
    //                    {
    //                        this.gameEndCD -= Time.deltaTime;
    //                    }
    //                }
    //            }
    //            if (this.isWinning)
    //            {
    //                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
    //                    {
    //                        num3 = (((int)(this.timeTotalServer * 10f)) * 0.1f) - 5f;
    //                        this.ShowHUDInfoCenter(num3.ToString() + "s !\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
    //                    }
    //                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        this.ShowHUDInfoCenter("Survive All Waves!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
    //                    }
    //                    else
    //                    {
    //                        this.ShowHUDInfoCenter("Humanity Win!\n Press " + FengCustomInputs.Inputs.inputString[InputCode.restart] + " to Restart.\n\n\n");
    //                    }
    //                }
    //                else
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
    //                    {
    //                        this.ShowHUDInfoCenter(string.Concat(new object[] { this.localRacingResult, "\n\nGame Restart in ", (int)this.gameEndCD, "s" }));
    //                    }
    //                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        this.ShowHUDInfoCenter("Survive All Waves!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
    //                    }
    //                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
    //                    {
    //                        if ((GameSettings.pvpMode == 0) && (GameSettings.bombMode == 0))
    //                        {
    //                            this.ShowHUDInfoCenter(string.Concat(new object[] { "Team ", this.teamWinner, " Win!\nGame Restart in ", (int)this.gameEndCD, "s\n\n" }));
    //                        }
    //                        else
    //                        {
    //                            this.ShowHUDInfoCenter(string.Concat(new object[] { "Round Ended!\nGame Restart in ", (int)this.gameEndCD, "s\n\n" }));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        this.ShowHUDInfoCenter("Humanity Win!\nGame Restart in " + ((int)this.gameEndCD) + "s\n\n");
    //                    }
    //                    if (this.gameEndCD <= 0f)
    //                    {
    //                        this.gameEndCD = 0f;
    //                        if (PhotonNetwork.isMasterClient)
    //                        {
    //                            this.restartRC();
    //                        }
    //                        this.ShowHUDInfoCenter(string.Empty);
    //                    }
    //                    else
    //                    {
    //                        this.gameEndCD -= Time.deltaTime;
    //                    }
    //                }
    //            }
    //            this.timeElapse += Time.deltaTime;
    //            this.roundTime += Time.deltaTime;
    //            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //            {
    //                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
    //                {
    //                    if (!this.isWinning)
    //                    {
    //                        this.timeTotalServer += Time.deltaTime;
    //                    }
    //                }
    //                else if (!(this.isLosing || this.isWinning))
    //                {
    //                    this.timeTotalServer += Time.deltaTime;
    //                }
    //            }
    //            else
    //            {
    //                this.timeTotalServer += Time.deltaTime;
    //            }
    //            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
    //            {
    //                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //                {
    //                    if (!this.isWinning)
    //                    {
    //                        this.ShowHUDInfoTopCenter("Time : " + ((((int)(this.timeTotalServer * 10f)) * 0.1f) - 5f));
    //                    }
    //                    if (this.timeTotalServer < 5f)
    //                    {
    //                        this.ShowHUDInfoCenter("RACE START IN " + ((int)(5f - this.timeTotalServer)));
    //                    }
    //                    else if (!this.startRacing)
    //                    {
    //                        this.ShowHUDInfoCenter(string.Empty);
    //                        this.startRacing = true;
    //                        this.endRacing = false;
    //                        BRM.CacheGameObject.Find("door").SetActive(false);
    //                    }
    //                }
    //                else
    //                {
    //                    this.ShowHUDInfoTopCenter("Time : " + ((this.roundTime >= 20f) ? (num3 = (((int)(this.roundTime * 10f)) * 0.1f) - 20f).ToString() : "WAITING"));
    //                    if (this.roundTime < 20f)
    //                    {
    //                        this.ShowHUDInfoCenter("RACE START IN " + ((int)(20f - this.roundTime)) + (!(this.localRacingResult == string.Empty) ? ("\nLast Round\n" + this.localRacingResult) : "\n\n"));
    //                    }
    //                    else if (!this.startRacing)
    //                    {
    //                        this.ShowHUDInfoCenter(string.Empty);
    //                        this.startRacing = true;
    //                        this.endRacing = false;
    //                        GameObject obj2 = BRM.CacheGameObject.Find("door");
    //                        if (obj2 != null)
    //                        {
    //                            obj2.SetActive(false);
    //                        }
    //                        if ((this.racingDoors != null) && customLevelLoaded)
    //                        {
    //                            foreach (GameObject obj3 in this.racingDoors)
    //                            {
    //                                obj3.SetActive(false);
    //                            }
    //                            this.racingDoors = null;
    //                        }
    //                    }
    //                    else if ((this.racingDoors != null) && customLevelLoaded)
    //                    {
    //                        foreach (GameObject obj3 in this.racingDoors)
    //                        {
    //                            obj3.SetActive(false);
    //                        }
    //                        this.racingDoors = null;
    //                    }
    //                }
    //                if ((IN_GAME_MAIN_CAMERA.mainCamera.gameOver && !this.needChooseSide) && customLevelLoaded)
    //                {
    //                    this.myRespawnTime += Time.deltaTime;
    //                    if (this.myRespawnTime > 1.5f)
    //                    {
    //                        this.myRespawnTime = 0f;
    //                        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
    //                        if (this.checkpoint != null)
    //                        {
    //                            base.StartCoroutine(this.WaitAndRespawn2(0.1f, this.checkpoint));
    //                        }
    //                        else
    //                        {
    //                            base.StartCoroutine(this.WaitAndRespawn1(0.1f, this.myLastRespawnTag));
    //                        }
    //                        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
    //                        this.ShowHUDInfoCenter(string.Empty);
    //                    }
    //                }
    //            }
    //            if (this.timeElapse > 1f)
    //            {
    //                this.timeElapse--;
    //                string content = string.Empty;
    //                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
    //                {
    //                    length = this.time - ((int)this.timeTotalServer);
    //                    content = content + "Time : " + length.ToString();
    //                }
    //                else if ((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.None))
    //                {
    //                    content = "Titan Left: ";
    //                    length = GameObject.FindGameObjectsWithTag("titan").Length;
    //                    content = content + length.ToString() + "  Time : ";
    //                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //                    {
    //                        length = (int)this.timeTotalServer;
    //                        content = content + length.ToString();
    //                    }
    //                    else
    //                    {
    //                        length = this.time - ((int)this.timeTotalServer);
    //                        content = content + length.ToString();
    //                    }
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                {
    //                    content = "Titan Left: ";
    //                    object[] objArray = new object[4];
    //                    objArray[0] = content;
    //                    length = GameObject.FindGameObjectsWithTag("titan").Length;
    //                    objArray[1] = length.ToString();
    //                    objArray[2] = " Wave : ";
    //                    objArray[3] = this.wave;
    //                    content = string.Concat(objArray);
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)
    //                {
    //                    content = "Time : ";
    //                    length = this.time - ((int)this.timeTotalServer);
    //                    content = content + length.ToString() + "\nDefeat the Colossal Titan.\nPrevent abnormal titan from running to the north gate";
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
    //                {
    //                    string str2 = "| ";
    //                    for (int i = 0; i < PVPcheckPoint.chkPts.Count; i++)
    //                    {
    //                        str2 = str2 + (PVPcheckPoint.chkPts[i] as PVPcheckPoint).getStateString() + " ";
    //                    }
    //                    str2 = str2 + "|";
    //                    length = this.time - ((int)this.timeTotalServer);
    //                    content = string.Concat(new object[] { this.PVPtitanScoreMax - this.PVPtitanScore, "  ", str2, "  ", this.PVPhumanScoreMax - this.PVPhumanScore, "\n" }) + "Time : " + length.ToString();
    //                }
    //                if (GameSettings.teamMode > 0)
    //                {
    //                    content = content + "\n[00FFFF]Cyan:" + Convert.ToString(this.cyanKills) + "       [FF00FF]Magenta:" + Convert.ToString(this.magentaKills) + "[ffffff]";
    //                }
    //                this.ShowHUDInfoTopCenter(content);
    //                content = string.Empty;
    //                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        content = "Time : ";
    //                        length = (int)this.timeTotalServer;
    //                        content = content + length.ToString();
    //                    }
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
    //                {
    //                    content = string.Concat(new object[] { "Humanity ", this.humanScore, " : Titan ", this.titanScore, " " });
    //                }
    //                else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT)) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE))
    //                {
    //                    content = string.Concat(new object[] { "Humanity ", this.humanScore, " : Titan ", this.titanScore, " " });
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
    //                {
    //                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                    {
    //                        content = "Time : ";
    //                        length = this.time - ((int)this.timeTotalServer);
    //                        content = content + length.ToString();
    //                    }
    //                    else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
    //                    {
    //                        for (int j = 0; j < this.teamScores.Length; j++)
    //                        {
    //                            string str3 = content;
    //                            content = string.Concat(new object[] { str3, (j == 0) ? string.Empty : " : ", "Team", j + 1, " ", this.teamScores[j], string.Empty });
    //                        }
    //                        content = content + "\nTime : " + ((this.time - ((int)this.timeTotalServer))).ToString();
    //                    }
    //                }
    //                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
    //                {
    //                    content += RIGHTTOPINFO(); 
    //                }
    //                this.ShowHUDInfoTopRight(content);
    //                string str4 = (IN_GAME_MAIN_CAMERA.difficulty >= 0) ? ((IN_GAME_MAIN_CAMERA.difficulty != 0) ? ((IN_GAME_MAIN_CAMERA.difficulty != 1) ? "Abnormal" : "Hard") : "Normal") : "Trainning";
    //                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.CAGE_FIGHT)
    //                {
    //                    this.ShowHUDInfoTopRightMAPNAME(string.Concat(new object[] { (int)this.roundTime, "s\n", level, " : ", str4 }));
    //                }
    //                else
    //                {
    //                    //this.ShowHUDInfoTopRightMAPNAME("\n" + level + " : " + str4);
    //                }
    //                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
    //                {
    //                    //char[] separator = new char[] { "`"[0] };
    //                    //string str5 = PhotonNetwork.room.name.Split(separator)[0];
    //                    //if (str5.Length > 20)
    //                    //{
    //                    //    str5 = str5.Remove(0x13) + "...";
    //                    //}
    //                    //this.ShowHUDInfoTopRightMAPNAME("\n" + str5 + " [FFC000](" + Convert.ToString(PhotonNetwork.room.playerCount) + "/" + Convert.ToString(PhotonNetwork.room.maxPlayers) + ")");
    //                    if (this.needChooseSide)
    //                    {
    //                        this.ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
    //                    }
    //                }
    //            }
    //            if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && (this.killInfoGO.Count > 0)) && (this.killInfoGO[0] == null))
    //            {
    //                this.killInfoGO.RemoveAt(0);
    //            }
    //            if (((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && PhotonNetwork.isMasterClient) && (this.timeTotalServer > this.time))
    //            {
    //                string str11;
    //                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
    //                this.gameStart = false;
    //                Screen.lockCursor = false;
    //                Screen.showCursor = true;
    //                string str6 = string.Empty;
    //                string str7 = string.Empty;
    //                string str8 = string.Empty;
    //                string str9 = string.Empty;
    //                string str10 = string.Empty;
    //                foreach (PhotonPlayer player in PhotonNetwork.playerList)
    //                {
    //                    if (player != null)
    //                    {
    //                        str6 = str6 + player.customProperties[PhotonPlayerProperty.name] + "\n";
    //                        str7 = str7 + player.customProperties[PhotonPlayerProperty.kills] + "\n";
    //                        str8 = str8 + player.customProperties[PhotonPlayerProperty.deaths] + "\n";
    //                        str9 = str9 + player.customProperties[PhotonPlayerProperty.max_dmg] + "\n";
    //                        str10 = str10 + player.customProperties[PhotonPlayerProperty.total_dmg] + "\n";
    //                    }
    //                }
    //                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
    //                {
    //                    str11 = string.Empty;
    //                    for (int k = 0; k < this.teamScores.Length; k++)
    //                    {
    //                        str11 = str11 + ((k == 0) ? string.Concat(new object[] { "Team", k + 1, " ", this.teamScores[k], " " }) : " : ");
    //                    }
    //                }
    //                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
    //                {
    //                    str11 = "Highest Wave : " + this.highestwave;
    //                }
    //                else
    //                {
    //                    str11 = string.Concat(new object[] { "Humanity ", this.humanScore, " : Titan ", this.titanScore });
    //                }
    //                object[] parameters = new object[] { str6, str7, str8, str9, str10, str11 };
    //                PView.RPC("showResult", PhotonTargets.AllBuffered, parameters);
    //            }
    //        }
    //    }
    //}

    private void core2()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
            if (this.LabelInfoTopRight == null)
            {
                this.LabelInfoTopRight = CacheGameObject.Find<UILabel>("LabelInfoTopRight");
            }
            else
            {
                this.LabelInfoTopRight.text = string.Empty;
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                {
                    this.LabelInfoTopRight.text = RIGHTTOPINFO();
                }
            }
        }
    }

    private string RIGHTTOPINFO()
    {
        string str6 = (IN_GAME_MAIN_CAMERA.difficulty >= 0)
                      ? ((IN_GAME_MAIN_CAMERA.difficulty != 0)
                          ? ((IN_GAME_MAIN_CAMERA.difficulty != 1) ? "[7b001c]Abnormal" : "[7b001c]Hard")
                          : "[7b001c]Normal")
                      : "[7b001c]Trainning";
        string text6 = string.Empty;
        string text7 = string.Empty;
        int num = GameSettings.waveModeNum;
        float num3;
        string content;
        if (GameSettings.waveModeOn > 0)
        {
            object obj = text6;
            text6 = string.Concat(new object[]
            {
                            obj,
                            "\n[d4ecff]",
                            num,
                            " WAVES[-]"
            });
        }
        int numpoint = GameSettings.pointMode;
        if (GameSettings.pointMode > 0)
        {
            object obj4 = text6;
            text6 = string.Concat(new object[]
            {
                            obj4,
                            "\n[d4ecff]RC POINT MODE",
                            numpoint,
                            "[-]"
            });
        }
        if (GameSettings.disableRock > 0)
        {
            text6 += "\n[d4ecff]NO PUNK ROCK THROWING[-]";
        }
        if (GameSettings.explodeMode > 0)
        {
            text6 += "\n[d4ecff]TITAN EXPLODE MODE RC[-]";
        }
        if (GameSettings.banEren > 0)
        {
            text6 += "\n[d4ecff]ANTI - EREN[-]";
        }
        if (GameSettings.horseMode > 0)
        {
            text6 += "\n[d4ecff]HORSES MODE RC[-]";
        }
        if (GameSettings.sizeMode > 0)
        {
            object obj2 = text6;
            text6 = string.Concat(new object[]
            {
                            obj2,
                            "\n[d4ecff]SIZES ",
                            GameSettings.sizeLower,
                            " & ",
                            GameSettings.sizeUpper,
                            "[-]"
            });
        }
        num = GameSettings.moreTitans;
        if (num > 0)
        {
            object obj3 = text6;
            text6 = string.Concat(new object[]
            {
                            obj3,
                            "\n[d4ecff]NUMBERLOCK ",
                            num,
                            "[-]"
            });
        }
        string text8;
        if (GameSettings.healthMode > 0)
        {
            if (GameSettings.healthLower == GameSettings.healthUpper)
            {
                object obj5 = text6;
                text6 = string.Concat(new object[]
                {
                                obj5,
                                "\n[d4ecff]",
                                GameSettings.healthUpper,
                                " NAPE HEALTH ",
                                (GameSettings.healthMode == 2) ? "(Scaled)" : "(Static)",
                                "[-]"
                });
            }
            else
            {
                text8 = text6;
                text6 = string.Concat(new string[]
                {
                                text8,
                                "\n[d4ecff]",
                                GameSettings.healthLower.ToString(),
                                " to ",
                                GameSettings.healthUpper.ToString(),
                                " NAPE HEALTH ",
                                (GameSettings.healthMode == 2) ? "(Scaled)" : "(Static",
                                "[-]"
                });
            }
        }
        if (GameSettings.endlessMode > 0)
        {
            object obj6 = text6;
            text6 = string.Concat(new object[]
            {
                            obj6,
                            "\n[d4ecff]RC ENDLESS RESPAWN (",
                            GameSettings.endlessMode.ToString(),
                            " SEC)[-]"
            });
        }
        if (GameSettings.teamMode > 0)
        {
            string str = string.Empty;
            if (GameSettings.teamMode == 1)
            {
                str = "NO SORT";
            }
            else if (GameSettings.teamMode == 2)
            {
                str = "LOCKED BY SIZE";
            }
            else if (GameSettings.teamMode == 3)
            {
                str = "LOCKED BY SKILL";
            }
            object obj7 = text6;
            text6 = string.Concat(new object[]
            {
                            obj7,
                            "\n[d4ecff]RC TEAM MODE (",
                            str,
                            ")[-]"
            });
        }
        if (GameSettings.infectionMode > 0)
        {
            text6 += "\n[d4ecff]RC INFECTION[-]";
        }
        if (GameSettings.globalDisableMinimap > 0)
        {
            text6 += "\n[d4ecff]DISABLE MINIMAPPING[-]";
        }
        if (GameSettings.damageMode > 0)
        {
            text6 = text6 + "\n[d4ecff]" + GameSettings.damageMode.ToString() + " DAMAGE[-]";
        }
        if (GameSettings.spawnMode > 0)
        {
            string txt = string.Empty;
            int pos = 0;
            if ((int)GameSettings.nRate > 0)
            {
                if (pos != 0) txt += "\n";
                txt += "[d4ecff]NORMAL: " + GameSettings.nRate.ToString("F2") + "%";
                pos++;
            }
            if ((int)GameSettings.aRate > 0)
            {
                if (pos != 0) txt += "\n";
                txt += "[d4ecff]ABNORMAL: " + GameSettings.aRate.ToString("F2") + "%";
                pos++;
            }
            if ((int)GameSettings.jRate > 0)
            {
                if (pos != 0) txt += "\n";
                txt += "[d4ecff]JUMPER: " + GameSettings.jRate.ToString("F2") + "%";
                pos++;
            }
            if ((int)GameSettings.cRate > 0)
            {
                if (pos != 0) txt += "\n";
                txt += "[d4ecff]CRAWLER: " + GameSettings.cRate.ToString("F2") + "%";
                pos++;
            }
            if ((int)GameSettings.pRate > 0)
            {
                if (pos != 0) txt += "\n";
                txt += "[d4ecff]PUNK: " + GameSettings.pRate.ToString("F2") + "%";
                pos++;
            }
            object obj8 = text6;
            text6 = string.Concat(new object[]
            {
                            obj8,
                            "\n[d4ecff]RC CUSTOM SPAWN MODE:\n",
                            txt,
                            "[-]"
            });
        }
        if (GameSettings.bombMode > 0)
        {
            text6 += "\n[d4ecff]PVP BOMB[-]";
        }
        if (GameSettings.ahssReload > 0)
        {
            text6 += "\n[d4ecff]AHSS AIR-RELOAD[-]";
        }
        if (GameSettings.punkWaves > 0)
        {
            text6 += "\n[d4ecff]PUNK WAWE MODE RC[-]";
        }
        if (GameSettings.pvpMode > 0)
        {
            string str = string.Empty;
            if (GameSettings.pvpMode == 1)
            {
                str = "TEAM-BASED";
            }
            else if (GameSettings.pvpMode == 2)
            {
                str = "FFA";
            }
            text6 += "\n[d4ecff]BLADE/AHSS PVP (" + str + ")[-]";
        }
        if (GameSettings.friendlyMode > 0)
        {
            text6 += "\n[d4ecff]PVP IS PROHIBITED[-]";
        }
        if (text6 != string.Empty)
        {
            text7 = "\n[000000]-~-~-~-~-~-~-~-~-~-~-~-[-]";
        }
        string text14 = PhotonNetwork.networkingPeer.ServerAddress;
        string text15 = string.Empty;
        string text17 = string.Empty;
        //text17 = text14.Remove(text14.IndexOf(":"));
        //IpAddrServer = text17;
        text15 = text14.Substring(text14.IndexOf(":"));
        string text16 = "\n[7b001c]" + DateTime.UtcNow.ToLocalTime().ToString() + "[-]";
        //string text12 = "\n[" + MainColor + "]ServerAddress: [7b001c]" + IpAddrServer;
        //string str11 = "\n[" + MainColor + "]Your Local IP Address: [7b001c]" + Network.player.ipAddress.ToString();
        //string AllIP = "\n[000000][[" + MainColor + "]LIST OF IP ADDRESSES[000000]]";
        //AllIP += text12 += str11;
        //try
        //{
        //    if (UpDateIPList == true)
        //    {
        //        if (NetworkingPeer.ipAddr != null) NetworkingPeer.ipAddr.Clear();
        //        NetworkingPeer.ipAddr.Add("\n[" + MainColor + "]Trying connect to [7b001c]" + IpAddrServer);
        //        IPHostEntry host = Dns.GetHostEntry(IpAddrServer);
        //        NetworkingPeer.ipAddr.Clear();
        //        foreach (IPAddress ip in host.AddressList)
        //        {
        //            if (ip.AddressFamily == AddressFamily.InterNetwork)
        //            {
        //                NetworkingPeer.LocalIP = ip.ToString();
        //            }
        //            if (!NetworkingPeer.ipAddr.Contains(ip.ToString()))
        //                NetworkingPeer.ipAddr.Add(ip.ToString());
        //        }
        //        UpDateIPList = false;
        //    }
        //}
        //catch (ArgumentOutOfRangeException)
        //{
        //    NetworkingPeer.ipAddr.Clear();
        //    NetworkingPeer.ipAddr.Add("[FF0000]Error... \nCan't connect with GetHostEntry to " + FengGameManagerMKII.IpAddrServer + " \nArgumentOutOfRangeException...[-]");
        //}
        //catch (SocketException)
        //{
        //    NetworkingPeer.ipAddr.Clear();
        //    NetworkingPeer.ipAddr.Add("[FF0000]Error... \nCan't connect with GetHostEntry to " + FengGameManagerMKII.IpAddrServer + " \nSocketException...[-]");
        //}
        //catch (ArgumentNullException)
        //{
        //    NetworkingPeer.ipAddr.Clear();
        //    NetworkingPeer.ipAddr.Add("[FF0000]Error... \nCan't connect with GetHostEntry to " + FengGameManagerMKII.IpAddrServer + " \nArgumentNullException...[-]");
        //}
        //catch (ArgumentException)
        //{
        //    NetworkingPeer.ipAddr.Clear();
        //    NetworkingPeer.ipAddr.Add("[FF0000]Error... \nCan't connect with GetHostEntry to " + FengGameManagerMKII.IpAddrServer + " \nArgumentException...[-]");
        //}
        //if (NetworkingPeer.ipAddr.Count > 0)
        //{
        //    foreach (string ip in NetworkingPeer.ipAddr)
        //    {
        //        if (ip == NetworkingPeer.LocalIP && !AllIP.Equals(ip.ToString()))
        //        {
        //            AllIP += "\n[" + MainColor + "]Local IP address: [7b001c]" + ip;
        //        }
        //        if (!AllIP.Equals(ip.ToString()))
        //        {
        //            AllIP += "\n[7b001c]" + ip;
        //        }
        //    }
        //}
        //else
        //{
        //    AllIP += "\nOther IP not exist in IP list. \nMay be GetHostEntry not work...";
        //}
        string text4 = (string)FengGameManagerMKII.settings[275];
        string text5 = string.Concat(new string[]
        {
                        "[ffc700](",
                        PhotonNetwork.room.playerCount.ToString(),
                        "/",
                        PhotonNetwork.room.maxPlayers.ToString(),
                        ")[-][-][7b001c]",
        });
        //text4 += text5;
        float num4 = this.deltaTime * 1000f;
        num3 = 1f / this.deltaTime;
        string text9 = string.Empty;
        string text10 = string.Empty;
        string text3 = "Camera(" + FengCustomInputs.Inputs.inputString[InputCode.camera] + "):" + IN_GAME_MAIN_CAMERA.cameraMode.ToString();
        string str8 = "\n" + level + ": " + str6;
        if (num3 < 30f)
        {
            text9 = ((num3 < 20f) ? "[ff1c1c]" : "[ffc700]");
            text10 = "[-]";
        }
        string text11 = string.Format("\n{0:0.0} MS ({1:0.} FPS)", num4, num3);
        GUI.Label(new Rect(10f, 557f, 185f, 40f), text11, "Label");
        return content = string.Concat(new string[]
        {
                        text3,
                        str8,
                        text4,
                        text5,
                        text9,
                        text11,
                        text10,
                        text16,
                        //text17,
                        //NameServerAddr,
                        //AllIP,
                        text7,
                        text6,
        });

    }

    private void coreadd()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.OnUpdate();
            if (customLevelLoaded)
            {
                for (int i = 0; i < this.titanSpawners.Count; i++)
                {
                    TitanSpawner item = this.titanSpawners[i];
                    item.time -= Time.deltaTime;
                    if ((item.time <= 0f) && ((titans.Count + this.fT.Count) < Math.Min(GameSettings.titanCap, 80)))
                    {
                        string name = item.name;
                        if (name == "spawnAnnie")
                        {
                            PhotonNetwork.Instantiate("FEMALE_TITAN", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                        }
                        else
                        {
                            GameObject obj2 = PhotonNetwork.Instantiate("TITAN_VER3.1", item.location, new Quaternion(0f, 0f, 0f, 1f), 0);
                            if (name == "spawnAbnormal")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
                            }
                            else if (name == "spawnJumper")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                            }
                            else if (name == "spawnCrawler")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                            }
                            else if (name == "spawnPunk")
                            {
                                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                            }
                        }
                        if (item.endless)
                        {
                            item.time = item.delay;
                        }
                        else
                        {
                            this.titanSpawners.Remove(item);
                        }
                    }
                }
            }
        }
        //if (Time.timeScale <= 0.1f)
        //{
        //    if (this.pauseWaitTime <= 3f)
        //    {
        //        this.pauseWaitTime -= Time.deltaTime * 1000000f;
        //        if (this.pauseWaitTime <= 1f)
        //        {
        //            Camera.main.farClipPlane = 1500f;
        //        }
        //        if (this.pauseWaitTime <= 0f)
        //        {
        //            this.pauseWaitTime = 0f;
        //            Time.timeScale = 1f;
        //        }
        //    }
        //    //this.justRecompileThePlayerList();
        //}
    }

    private void coreeditor()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            GUI.FocusControl(null);
        }
        if (this.selectedObj != null)
        {
            float num = 0.2f;
            if (inputRC.isInputLevel(InputCodeRC.levelSlow))
            {
                num = 0.04f;
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelFast))
            {
                num = 0.6f;
            }
            if (inputRC.isInputLevel(InputCodeRC.levelForward))
            {
                selectedObj.transform.position += (Vector3)(num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelBack))
            {
                selectedObj.transform.position -= (Vector3)(num * new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            }
            if (inputRC.isInputLevel(InputCodeRC.levelLeft))
            {
                selectedObj.transform.position -= (Vector3)(num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z));
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelRight))
            {
                selectedObj.transform.position += (Vector3)(num * new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z));
            }
            if (inputRC.isInputLevel(InputCodeRC.levelDown))
            {
                selectedObj.transform.position -= (Vector3)(Vector3.up * num);
            }
            else if (inputRC.isInputLevel(InputCodeRC.levelUp))
            {
                selectedObj.transform.position += (Vector3)(Vector3.up * num);
            }
            if (!this.selectedObj.name.StartsWith("misc,region"))
            {
                if (inputRC.isInputLevel(InputCodeRC.levelRRight))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.up * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRLeft))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.down * num));
                }
                if (inputRC.isInputLevel(InputCodeRC.levelRCCW))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.forward * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRCW))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.back * num));
                }
                if (inputRC.isInputLevel(InputCodeRC.levelRBack))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.left * num));
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRForward))
                {
                    this.selectedObj.transform.Rotate((Vector3)(Vector3.right * num));
                }
            }
            if (inputRC.isInputLevel(InputCodeRC.levelPlace))
            {
                linkHash[3].Add(this.selectedObj.GetInstanceID(), this.selectedObj.name + "," + Convert.ToString(this.selectedObj.transform.position.x) + "," + Convert.ToString(this.selectedObj.transform.position.y) + "," + Convert.ToString(this.selectedObj.transform.position.z) + "," + Convert.ToString(this.selectedObj.transform.rotation.x) + "," + Convert.ToString(this.selectedObj.transform.rotation.y) + "," + Convert.ToString(this.selectedObj.transform.rotation.z) + "," + Convert.ToString(this.selectedObj.transform.rotation.w));
                this.selectedObj = null;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                Screen.lockCursor = true;
            }
            if (inputRC.isInputLevel(InputCodeRC.levelDelete))
            {
                UnityEngine.Object.Destroy(this.selectedObj);
                this.selectedObj = null;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                Screen.lockCursor = true;
                linkHash[3].Remove(this.selectedObj.GetInstanceID());
            }
        }
        else
        {
            if (Screen.lockCursor)
            {
                float num2 = 100f;
                if (inputRC.isInputLevel(InputCodeRC.levelSlow))
                {
                    num2 = 20f;
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelFast))
                {
                    num2 = 400f;
                }
                Transform mainT = IN_GAME_MAIN_CAMERA.mainT;
                if (inputRC.isInputLevel(InputCodeRC.levelForward))
                {
                    mainT.position += (Vector3)((mainT.forward * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelBack))
                {
                    mainT.position -= (Vector3)((mainT.forward * num2) * Time.deltaTime);
                }
                if (inputRC.isInputLevel(InputCodeRC.levelLeft))
                {
                    mainT.position -= (Vector3)((mainT.right * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelRight))
                {
                    mainT.position += (Vector3)((mainT.right * num2) * Time.deltaTime);
                }
                if (inputRC.isInputLevel(InputCodeRC.levelUp))
                {
                    mainT.position += (Vector3)((mainT.up * num2) * Time.deltaTime);
                }
                else if (inputRC.isInputLevel(InputCodeRC.levelDown))
                {
                    mainT.position -= (Vector3)((mainT.up * num2) * Time.deltaTime);
                }
            }
            if (inputRC.isInputLevelDown(InputCodeRC.levelCursor))
            {
                if (Screen.lockCursor)
                {
                    IN_GAME_MAIN_CAMERA.mouselook.enabled = false;
                    Screen.lockCursor = false;
                }
                else
                {
                    IN_GAME_MAIN_CAMERA.mouselook.enabled = true;
                    Screen.lockCursor = true;
                }
            }
            if (((Input.GetKeyDown(KeyCode.Mouse0) && !Screen.lockCursor) && (GUIUtility.hotControl == 0)) && (((Input.mousePosition.x > 300f) && (Input.mousePosition.x < (Screen.width - 300f))) || ((Screen.height - Input.mousePosition.y) > 600f)))
            {
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
                {
                    Transform transform8 = hitInfo.transform;
                    if ((((transform8.gameObject.name.StartsWith("custom") || transform8.gameObject.name.StartsWith("base")) || (transform8.gameObject.name.StartsWith("racing") || transform8.gameObject.name.StartsWith("photon"))) || transform8.gameObject.name.StartsWith("spawnpoint")) || transform8.gameObject.name.StartsWith("misc"))
                    {
                        this.selectedObj = transform8.gameObject;
                        IN_GAME_MAIN_CAMERA.mouselook.enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(this.selectedObj.GetInstanceID());
                    }
                    else if (((transform8.parent.gameObject.name.StartsWith("custom") || transform8.parent.gameObject.name.StartsWith("base")) || transform8.parent.gameObject.name.StartsWith("racing")) || transform8.parent.gameObject.name.StartsWith("photon"))
                    {
                        this.selectedObj = transform8.parent.gameObject;
                        IN_GAME_MAIN_CAMERA.mouselook.enabled = false;
                        Screen.lockCursor = true;
                        linkHash[3].Remove(this.selectedObj.GetInstanceID());
                    }
                }
            }
        }
    }

    private IEnumerator customlevelcache()
    {
        for (int i = 0; i < this.levelCache.Count; i++)
        {
            //this.customlevelclientE(this.levelCache[i], false);
            base.StartCoroutine(this.customlevelclientE(this.levelCache[i], false));
            //BRM.StatsTab.AddLine("Call customlevelcache() for load this.levelCache[" + this.levelCache[i] + "in customlevelclientE()", StatsTab.DebugType.LOG);
        }
        yield return new WaitForEndOfFrame();
        yield break;
    }

    private IEnumerator customlevelclientE(string[] content, bool renewHash)
    {
        int num;
        string[] strArray;
        bool flag = false;
        bool flag2 = false;
        if (content[content.Length - 1].StartsWith("a"))
        {
            flag = true;
        }
        else if (content[content.Length - 1].StartsWith("z"))
        {
            flag2 = true;
            customLevelLoaded = true;
            this.spawnPlayerCustomMap();
            Minimap.TryRecaptureInstance();
            this.unloadAssets();
            IN_GAME_MAIN_CAMERA.tiltshift.enabled = false;
        }
        if (renewHash)
        {
            if (flag)
            {
                currentLevel = string.Empty;
                //while (customObjects.Count > 0)
                //{
                //    GameObject gameObject;
                //    if (gameObject = customObjects.Dequeue())
                //    {
                //        UnityEngine.Object.Destroy(gameObject);
                //    }
                //}
                ClearCustomObjects();
                this.levelCache.Clear();
                this.titanSpawns.Clear();
                this.playerSpawnsC.Clear();
                this.playerSpawnsM.Clear();
                for (num = 0; num < content.Length; num++)
                {
                    strArray = content[num].Split(new char[] { ',' });
                    if (strArray[0] == "titan")
                    {
                        this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerC")
                    {
                        this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                    else if (strArray[0] == "playerM")
                    {
                        this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray[1]), Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3])));
                    }
                }
                this.spawnPlayerCustomMap();
            }
            currentLevel = currentLevel + content[content.Length - 1];
            this.levelCache.Add(content);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.currentLevel, currentLevel);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        if (!flag && !flag2)
        {
            for (num = 0; num < content.Length; num++)
            {
                float num2;
                GameObject obj2;
                float num3;
                float num5;
                float num6;
                float num7;
                Color color;
                Mesh mesh;
                Color[] colorArray;
                int num8;
                strArray = content[num].Split(new char[] { ',' });
                if (strArray[0].StartsWith("custom"))
                {
                    num2 = 1f;
                    obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])));
                    if (obj2 != null)
                    {
                        UnityEngine.Object.DontDestroyOnLoad(obj2);
                        FengGameManagerMKII.customObjects.Enqueue(obj2);
                        if (strArray[2] != "default")
                        {
                            if (strArray[2].StartsWith("transparent"))
                            {
                                if (float.TryParse(strArray[2].Substring(11), out num3))
                                {
                                    num2 = num3;
                                }
                                foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    renderer.material = CacheResources.RCLoad<Material>("transparent");
                                    if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                    {
                                        renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                    }
                                }
                            }
                            else
                            {
                                foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                                {
                                    renderer.material = CacheResources.RCLoad<Material>(strArray[2]);
                                    if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                    {
                                        renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                    }
                                }
                            }
                        }
                        num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                        num5 -= 0.001f;
                        num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                        num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                        obj2.transform.localScale = new Vector3(num5, num6, num7);
                        if (strArray[6] != "0")
                        {
                            color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                            foreach (MeshFilter filter in obj2.GetComponentsInChildren<MeshFilter>())
                            {
                                mesh = filter.mesh;
                                colorArray = new Color[mesh.vertexCount];
                                num8 = 0;
                                while (num8 < mesh.vertexCount)
                                {
                                    colorArray[num8] = color;
                                    num8++;
                                }
                                mesh.colors = colorArray;
                            }
                        }
                    }

                }
                else if (strArray[0].StartsWith("base"))
                {
                    if (strArray.Length < 15)
                    {
                        UnityEngine.Object.Instantiate(CacheResources.Load(strArray[1]), new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])));
                    }
                    else
                    {
                        num2 = 1f;
                        obj2 = null;
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.Load<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            if (strArray[2] != "default")
                            {
                                if (strArray[2].StartsWith("transparent"))
                                {
                                    if (float.TryParse(strArray[2].Substring(11), out num3))
                                    {
                                        num2 = num3;
                                    }
                                    foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                                    {
                                        renderer.material = CacheResources.RCLoad<Material>("transparent");
                                        if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                        {
                                            renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                            renderer.material.color = new Color(1f, 0f, 0f);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Renderer renderer in obj2.GetComponentsInChildren<Renderer>())
                                    {
                                        if (!renderer.name.Contains("Particle System") || !obj2.name.Contains("aot_supply"))
                                        {
                                            renderer.material = CacheResources.RCLoad<Material>(strArray[2]);
                                            if ((Convert.ToSingle(strArray[10]) != 1f) || (Convert.ToSingle(strArray[11]) != 1f))
                                            {
                                                renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray[11]));
                                            }
                                        }
                                    }
                                }
                            }
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[3]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[4]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[5]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            if (strArray[6] != "0")
                            {
                                color = new Color(Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), num2);
                                foreach (MeshFilter filter in obj2.GetComponentsInChildren<MeshFilter>())
                                {
                                    mesh = filter.mesh;
                                    colorArray = new Color[mesh.vertexCount];
                                    for (num8 = 0; num8 < mesh.vertexCount; num8++)
                                    {
                                        colorArray[num8] = color;
                                    }
                                    mesh.colors = colorArray;
                                }
                            }
                        }

                    }
                }
                else if (strArray[0].StartsWith("misc"))
                {
                    if (strArray[1].StartsWith("barrier"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                        }
                    }
                    else if (strArray[1].StartsWith("racingStart"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            if (this.racingDoors != null)
                            {
                                this.racingDoors.Add(obj2);
                            }
                        }
                    }
                    else if (strArray[1].StartsWith("racingEnd"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            obj2.AddComponent<LevelTriggerRacingEnd>();
                        }

                    }
                    else if (strArray[1].StartsWith("region") && PhotonNetwork.isMasterClient)
                    {
                        Vector3 loc = new Vector3(Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8]));
                        RCRegion region = new RCRegion(loc, Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]));
                        string key = strArray[2];
                        if (RCRegionTriggers.ContainsKey(key))
                        {
                            GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>("region"));
                            if (obj3 != null)
                            {
                                UnityEngine.Object.DontDestroyOnLoad(obj3);
                                FengGameManagerMKII.customObjects.Enqueue(obj3);
                                obj3.transform.position = loc;
                                obj3.AddComponent<RegionTrigger>();
                                obj3.GetComponent<RegionTrigger>().CopyTrigger((RegionTrigger)RCRegionTriggers[key]);
                                num5 = obj3.transform.localScale.x * Convert.ToSingle(strArray[3]);
                                num5 -= 0.001f;
                                num6 = obj3.transform.localScale.y * Convert.ToSingle(strArray[4]);
                                num7 = obj3.transform.localScale.z * Convert.ToSingle(strArray[5]);
                                obj3.transform.localScale = new Vector3(num5, num6, num7);
                                region.myBox = obj3;
                            }
                        }
                        RCRegions.Add(key, region);
                    }
                }
                else if (strArray[0].StartsWith("racing"))
                {
                    if (strArray[1].StartsWith("start"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            if (this.racingDoors != null)
                            {
                                this.racingDoors.Add(obj2);
                            }
                        }
                    }
                    else if (strArray[1].StartsWith("end"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<LevelTriggerRacingEnd>();
                        }
                    }
                    else if (strArray[1].StartsWith("kill"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingKillTrigger>();
                        }
                    }
                    else if (strArray[1].StartsWith("checkpoint"))
                    {
                        obj2 = (GameObject)UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>(strArray[1]), new Vector3(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7])), new Quaternion(Convert.ToSingle(strArray[8]), Convert.ToSingle(strArray[9]), Convert.ToSingle(strArray[10]), Convert.ToSingle(strArray[11])));
                        if (obj2 != null)
                        {
                            UnityEngine.Object.DontDestroyOnLoad(obj2);
                            FengGameManagerMKII.customObjects.Enqueue(obj2);
                            num5 = obj2.transform.localScale.x * Convert.ToSingle(strArray[2]);
                            num5 -= 0.001f;
                            num6 = obj2.transform.localScale.y * Convert.ToSingle(strArray[3]);
                            num7 = obj2.transform.localScale.z * Convert.ToSingle(strArray[4]);
                            obj2.transform.localScale = new Vector3(num5, num6, num7);
                            obj2.GetComponentInChildren<Collider>().gameObject.AddComponent<RacingCheckpointTrigger>();
                        }
                    }
                }
                else if (strArray[0].StartsWith("map"))
                {
                    if (strArray[1].StartsWith("disablebounds"))
                    {
                        UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("gameobjectOutSide"));
                        UnityEngine.Object.Instantiate(CacheResources.RCLoad<GameObject>("outside"));
                    }
                }
                else if (PhotonNetwork.isMasterClient && strArray[0].StartsWith("photon"))
                {
                    if (strArray[1].StartsWith("Cannon"))
                    {
                        if (strArray.Length > 15)
                        {
                            GameObject go = PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[12]), Convert.ToSingle(strArray[13]), Convert.ToSingle(strArray[14])), new Quaternion(Convert.ToSingle(strArray[15]), Convert.ToSingle(strArray[0x10]), Convert.ToSingle(strArray[0x11]), Convert.ToSingle(strArray[0x12])), 0);
                            go.GetComponent<CannonPropRegion>().settings = content[num];
                            go.GetPhotonView().RPC("SetSize", PhotonTargets.AllBuffered, new object[] { content[num] });
                        }
                        else
                        {
                            PhotonNetwork.Instantiate("RCAsset/" + strArray[1] + "Prop", new Vector3(Convert.ToSingle(strArray[2]), Convert.ToSingle(strArray[3]), Convert.ToSingle(strArray[4])), new Quaternion(Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]), Convert.ToSingle(strArray[7]), Convert.ToSingle(strArray[8])), 0).GetComponent<CannonPropRegion>().settings = content[num];
                        }
                    }
                    else
                    {
                        TitanSpawner item = new TitanSpawner();
                        num5 = 30f;
                        if (float.TryParse(strArray[2], out num3))
                        {
                            num5 = Mathf.Max(Convert.ToSingle(strArray[2]), 1f);
                        }
                        item.time = num5;
                        item.delay = num5;
                        item.name = strArray[1];
                        if (strArray[3] == "1")
                        {
                            item.endless = true;
                        }
                        else
                        {
                            item.endless = false;
                        }
                        item.location = new Vector3(Convert.ToSingle(strArray[4]), Convert.ToSingle(strArray[5]), Convert.ToSingle(strArray[6]));
                        this.titanSpawners.Add(item);
                    }
                }
            }
        }
        yield break;
    }

    private IEnumerator customlevelE(List<PhotonPlayer> players, bool hasJoined = false)
    {
        if (PView == null) PView = base.photonView;
        string[] strArray;
        if(currentLevel == string.Empty)
        {
            strArray = new string[] { "loadempty" };
            foreach (PhotonPlayer player in players)
            {
               if (!player.isLocal) 
                    PView.RPC("customlevelRPC", player, new object[] { strArray });
            }
        }
        else
        {
            for (int i = 0; i < this.levelCache.Count; i++)
            {
                foreach (PhotonPlayer player in players)
                {
                    if (((player.customProperties["currentLevel"] != null) 
                        && (currentLevel != string.Empty) && !hasJoined) 
                        && ((string)player.customProperties["currentLevel"] == currentLevel))
                    {
                        if (i == 0)
                        {
                            strArray = new string[] { "loadcached" };
                            PView.RPC("customlevelRPC", player, new object[] { strArray });
                        }
                    }
                    else
                    {
                        PView.RPC("customlevelRPC", player, new object[] { this.levelCache[i] });
                    }
                }
                if (i > 0)
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }            
        yield break;
    }

    [RPC]
    private void customlevelRPC(string[] content, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (content.Length == 1)
            {
                if (content[0] == "loadcached")
                {
                    //base.StartCoroutine(this.customlevelcache());
                    BRM.StatsTab.AddLine("customlevelRPC = loadcached");
                    logicLoaded = true;
                    customLevelLoaded = true;
                    scriptcache = string.Empty;
                    this.spawnPlayerCustomMap();
                    return;
                }
                if (content[0] == "loadempty")
                {
                    BRM.StatsTab.AddLine("customlevelRPC = loadempty");
                    currentLevel = string.Empty;
                    scriptcache = string.Empty;
                    this.levelCache.Clear();
                    this.titanSpawns.Clear();
                    this.playerSpawnsC.Clear();
                    this.playerSpawnsM.Clear();
                    ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                    propertiesToSet.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                    PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                    logicLoaded = true;
                    customLevelLoaded = true;
                    this.spawnPlayerCustomMap();
                    //while (customObjects.Count > 0)
                    //{
                    //    GameObject gameObject = customObjects.Dequeue();
                    //    if (gameObject != null)
                    //    {
                    //        UnityEngine.Object.Destroy(gameObject);
                    //    }
                    //}
                    ClearCustomObjects();
                    return;
                }
            }
            else
            {
                string script = string.Join(";\r\n", content);
                if (script != scriptcache)
                {
                    BRM.StatsTab.AddLine("customlevelRPC = load customlevelclientE");
                    scriptcache = script;
                    base.StartCoroutine(this.customlevelclientE(content, true));
                }
            }
        }
    }

    public void debugChat(string str)
    {
        InRoomChat.ChatInstanse.addLINE(str);
    }

    public void DestroyAllExistingCloths()
    {
        Cloth[] clothArray = UnityEngine.Object.FindObjectsOfType<Cloth>();
        if (clothArray.Length > 0)
        {
            for (int i = 0; i < clothArray.Length; i++)
            {
                ClothFactory.DisposeObject(clothArray[i].gameObject);
            }
        }
    }

    private void endGameInfectionRC()
    {
        int num;
        imatitan.Clear();
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[num];
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
            player.SetCustomProperties(propertiesToSet);
        }
        int length = PhotonNetwork.playerList.Length;
        int infectionMode = GameSettings.infectionMode;
        for (num = 0; num < PhotonNetwork.playerList.Length; num++)
        {
            PhotonPlayer player2 = PhotonNetwork.playerList[num];
            if ((length > 0) && (UnityEngine.Random.Range((float)0f, (float)1f) <= (((float)infectionMode) / ((float)length))))
            {
                ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                hashtable2.Add(PhotonPlayerProperty.isTitan, 2);
                player2.SetCustomProperties(hashtable2);
                imatitan.Add(player2.ID, 2);
                infectionMode--;
            }
            length--;
        }
        this.gameEndCD = 0f;
        this.restartGame2(false);
    }

    private void endGameRC()
    {
        if (GameSettings.pointMode > 0)
        {
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                PhotonPlayer player = PhotonNetwork.playerList[i];
                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.kills, 0);
                propertiesToSet.Add(PhotonPlayerProperty.deaths, 0);
                propertiesToSet.Add(PhotonPlayerProperty.max_dmg, 0);
                propertiesToSet.Add(PhotonPlayerProperty.total_dmg, 0);
                player.SetCustomProperties(propertiesToSet);
            }
        }
        this.gameEndCD = 0f;
        this.restartGame2(false);
    }

    public void EnterSpecMode(bool enter)
    {
        if (enter)
        {
            this.spectateSprites = new List<GameObject>();
            foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if ((obj2.GetComponent<UISprite>() != null) && obj2.activeInHierarchy)
                {
                    string name = obj2.name;
                    if (((name.Contains("blade") || name.Contains("bullet")) || (name.Contains("gas") || name.Contains("flare"))) || name.Contains("skill_cd"))
                    {
                        if (!this.spectateSprites.Contains(obj2))
                        {
                            this.spectateSprites.Add(obj2);
                        }
                        obj2.SetActive(false);
                    }
                }
            }
            string[] strArray2 = new string[] { "Flare", "LabelInfoBottomRight" };
            foreach (string str2 in strArray2)
            {
                GameObject item = BRM.CacheGameObject.Find(str2);
                if (item != null)
                {
                    if (!this.spectateSprites.Contains(item))
                    {
                        this.spectateSprites.Add(item);
                    }
                    item.SetActive(false);
                }
            }
            foreach (HERO hero in heroes)
            {
                if (hero.photonView.isMine)
                {
                    PhotonNetwork.Destroy(hero.photonView);
                }
            }
            if ((RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2) && !RCextensions.returnBoolFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.dead]))
            {
                foreach (TITAN titan in titans)
                {
                    if (titan.photonView.isMine)
                    {
                        PhotonNetwork.Destroy(titan.photonView);
                    }
                }
            }
            NGUITools.SetActive(this.uirefer.panels[1], false);
            NGUITools.SetActive(this.uirefer.panels[2], false);
            NGUITools.SetActive(this.uirefer.panels[3], false);
            instance.needChooseSide = false;
            IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.ORIGINAL)
            {
                Screen.lockCursor = false;
                Screen.showCursor = false;
            }
            GameObject obj4 = GameObject.FindGameObjectWithTag("Player");
            if ((obj4 != null) && (obj4.GetComponent<HERO>() != null))
            {
                IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(obj4, true, false);
            }
            else
            {
                IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
            }
            IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(false);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
            base.StartCoroutine(this.reloadSky());
        }
        else
        {
            if (BRM.CacheGameObject.Find("cross1") != null)
            {
                BRM.CacheGameObject.Find("cross1").transform.localPosition = (Vector3)(Vector3.up * 5000f);
            }
            if (this.spectateSprites != null)
            {
                foreach (GameObject obj2 in this.spectateSprites)
                {
                    if (obj2 != null)
                    {
                        obj2.SetActive(true);
                    }
                }
            }
            this.spectateSprites = new List<GameObject>();
            NGUITools.SetActive(BRM.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(BRM.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(BRM.CacheGameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            instance.needChooseSide = true;
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
            IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
        }
    }

    public void gameLose()
    {
        if (!this.isWinning && !this.isLosing)
        {
            this.isLosing = true;
            this.titanScore++;
            this.gameEndCD = this.gameEndTotalCDtime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = new object[] { this.titanScore };
                PView.RPC("netGameLose", PhotonTargets.Others, parameters);
            }
        }
    }

    public void gameLose2()
    {
        if (!this.isWinning && !this.isLosing)
        {
            this.isLosing = true;
            this.titanScore++;
            this.gameEndCD = this.gameEndTotalCDtime;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                object[] parameters = new object[] { this.titanScore };
                PView.RPC("netGameLose", PhotonTargets.Others, parameters);
                if (((int)settings[0xf4]) == 1)
                {
                    InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
                }
            }
        }
    }

    public void gameWin()
    {
        if (!this.isLosing && !this.isWinning)
        {
            this.isWinning = true;
            this.humanScore++;
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                this.gameEndCD = 20f;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] parameters = new object[] { 0 };
                    PView.RPC("netGameWin", PhotonTargets.Others, parameters);
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray2 = new object[] { this.teamWinner };
                    PView.RPC("netGameWin", PhotonTargets.Others, objArray2);
                }
                this.teamScores[this.teamWinner - 1]++;
            }
            else
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray3 = new object[] { this.humanScore };
                    PView.RPC("netGameWin", PhotonTargets.Others, objArray3);
                }
            }
        }
    }

    public void gameWin2()
    {
        if (!this.isLosing && !this.isWinning)
        {
            this.isWinning = true;
            this.humanScore++;
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
            {
                if (GameSettings.racingStatic == 1)
                {
                    this.gameEndCD = 1000f;
                }
                else
                {
                    this.gameEndCD = 20f;
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] parameters = new object[] { 0 };
                    PView.RPC("netGameWin", PhotonTargets.Others, parameters);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray3 = new object[] { this.teamWinner };
                    PView.RPC("netGameWin", PhotonTargets.Others, objArray3);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                    }
                }
                this.teamScores[this.teamWinner - 1]++;
            }
            else
            {
                this.gameEndCD = this.gameEndTotalCDtime;
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    object[] objArray4 = new object[] { this.humanScore };
                    PView.RPC("netGameWin", PhotonTargets.Others, objArray4);
                    if (((int)settings[0xf4]) == 1)
                    {
                        InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
                    }
                }
            }
        }
    }

    [RPC]
    private void getRacingResult(string player, float time)
    {
        RacingResult result = new RacingResult
        {
            name = player,
            time = time
        };
        this.racingResult.Add(result);
        this.refreshRacingResult2();
    }

    //public ArrayList getTitans()
    //{
    //    return this.titans;
    //}

    private string hairtype(int lol)
    {
        if (lol < 0)
        {
            return "Random";
        }
        return ("Male " + lol);
    }

    [RPC]
    private void ignorePlayer(int ID, PhotonMessageInfo info)
    {
        //if (ID == PhotonNetwork.player.ID)
        //{
        //    StartCoroutine(this.RCBan());
        //}
        StatsTab.AddLine(string.Concat(new object[] { "<color=red>ID: </color>", info.sender.ID, "<color=#0066FF> Ignored </color>", (ID == PhotonNetwork.player.ID) ? "<color=#0066FF>you</color>" : (PhotonPlayer.Find(ID).uiname.ToRGBA() + " <color=green>ID: </color>" + ID) }), StatsTab.DebugType.WARNING);
        this.RecompilePlayerList(0.1f);
    }

    [RPC]
    private void ignorePlayerArray(int[] IDS, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            for (int i = 0; i < IDS.Length; i++)
            {
                int iD = IDS[i];
                PhotonPlayer player = PhotonPlayer.Find(iD);
                if ((player != null) && !ignoreList.Contains(iD))
                {
                    for (int j = 0; j < PhotonNetwork.playerList.Length; j++)
                    {
                        if (PhotonNetwork.playerList[j] == player)
                        {
                            ignoreList.Add(iD);
                            RaiseEventOptions options = new RaiseEventOptions
                            {
                                TargetActors = new int[] { iD }
                            };
                            PhotonNetwork.RaiseEvent(0xfe, null, true, options);
                        }
                    }
                }
            }
        }
        this.RecompilePlayerList(0.1f);
    }

    public static GameObject InstantiateCustomAsset(string key)
    {
        key = key.Substring(8);
        return (GameObject)RCassets.Load(key);
    }

    public bool isPlayerAllDead()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (((int)player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if ((bool)player.customProperties[PhotonPlayerProperty.dead])
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isPlayerAllDead2()
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isTeamAllDead(int team)
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((((int)player.customProperties[PhotonPlayerProperty.isTitan]) == 1) && (((int)player.customProperties[PhotonPlayerProperty.team]) == team))
            {
                num++;
                if ((bool)player.customProperties[PhotonPlayerProperty.dead])
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public bool isTeamAllDead2(int team)
    {
        int num = 0;
        int num2 = 0;
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (((player.customProperties[PhotonPlayerProperty.isTitan] != null) && (player.customProperties[PhotonPlayerProperty.team] != null)) && ((RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1) && (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.team]) == team)))
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }
        return (num == num2);
    }

    public void justRecompileThePlayerList()  // pvp
    {
        int num15;
        string str3;
        int num16;
        int num17;
        int num18;
        int num19;
        string str = string.Empty;
        if (GameSettings.teamMode != 0)
        {
            int num10;
            string str2;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            Dictionary<int, PhotonPlayer> dictionary = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary2 = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary3 = new Dictionary<int, PhotonPlayer>();
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if ((player.customProperties[PhotonPlayerProperty.dead] != null) && !ignoreList.Contains(player.ID))
                {
                    num10 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                    switch (num10)
                    {
                        case 0:
                            dictionary3.Add(player.ID, player);
                            break;

                        case 1:
                            dictionary.Add(player.ID, player);
                            num += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num3 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num5 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num7 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;

                        case 2:
                            dictionary2.Add(player.ID, player);
                            num2 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num4 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num6 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num8 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;
                    }
                }
            }
            this.cyanKills = num;
            this.magentaKills = num2;
            if (PhotonNetwork.isMasterClient)
            {
                if (GameSettings.teamMode == 2)
                {
                    foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                    {
                        int num11 = 0;
                        if (dictionary.Count > (dictionary2.Count + 1))
                        {
                            num11 = 2;
                            if (dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Remove(player2.ID);
                            }
                            if (!dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Add(player2.ID, player2);
                            }
                        }
                        else if (dictionary2.Count > (dictionary.Count + 1))
                        {
                            num11 = 1;
                            if (!dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Add(player2.ID, player2);
                            }
                            if (dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Remove(player2.ID);
                            }
                        }
                        if (num11 > 0)
                        {
                            PView.RPC("setTeamRPC", player2, new object[] { num11 });
                        }
                    }
                }
                else if (GameSettings.teamMode == 3)
                {
                    foreach (PhotonPlayer player3 in PhotonNetwork.playerList)
                    {
                        int num12 = 0;
                        num10 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.RCteam]);
                        if (num10 > 0)
                        {
                            switch (num10)
                            {
                                case 1:
                                    {
                                        int num13 = 0;
                                        num13 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                        if (((num2 + num13) + 7) < (num - num13))
                                        {
                                            num12 = 2;
                                            num2 += num13;
                                            num -= num13;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        int num14 = 0;
                                        num14 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                        if (((num + num14) + 7) < (num2 - num14))
                                        {
                                            num12 = 1;
                                            num += num14;
                                            num2 -= num14;
                                        }
                                        break;
                                    }
                            }
                            if (num12 > 0)
                            {
                                PView.RPC("setTeamRPC", player3, new object[] { num12 });
                            }
                        }
                    }
                }
            }
            str = string.Concat(new object[] { str, "[00FFFF]TEAM CYAN", "[ffffff]:", this.cyanKills, "/", num3, "/", num5, "/", num7, "\n" });
            foreach (PhotonPlayer player4 in dictionary.Values)
            {
                num10 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player4.customProperties[PhotonPlayerProperty.dead] != null) && (num10 == 1))
                {
                    if (ignoreList.Contains(player4.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }
                    if (player4.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }
                    str = str + "[" + Convert.ToString(player4.ID) + "] ";
                    if (player4.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player4.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(new object[] { str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19 });
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }
                    str = str + "\n";
                }
            }
            str = string.Concat(new object[] { str, " \n", "[FF00FF]TEAM MAGENTA", "[ffffff]:", this.magentaKills, "/", num4, "/", num6, "/", num8, "\n" });
            foreach (PhotonPlayer player5 in dictionary2.Values)
            {
                num10 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player5.customProperties[PhotonPlayerProperty.dead] != null) && (num10 == 2))
                {
                    if (ignoreList.Contains(player5.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }
                    if (player5.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }
                    str = str + "[" + Convert.ToString(player5.ID) + "] ";
                    if (player5.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player5.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(new object[] { str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19 });
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }
                    str = str + "\n";
                }
            }
            str = string.Concat(new object[] { str, " \n", "[00FF00]INDIVIDUAL\n" });
            foreach (PhotonPlayer player6 in dictionary3.Values)
            {
                num10 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player6.customProperties[PhotonPlayerProperty.dead] != null) && (num10 == 0))
                {
                    if (ignoreList.Contains(player6.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }
                    if (player6.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }
                    str = str + "[" + Convert.ToString(player6.ID) + "] ";
                    if (player6.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str2 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player6.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(new object[] { str2, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19 });
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }
                    str = str + "\n";
                }
            }
        }
        else
        {
            foreach (PhotonPlayer player7 in PhotonNetwork.playerList)
            {
                if (player7.customProperties[PhotonPlayerProperty.dead] != null)
                {
                    if (ignoreList.Contains(player7.ID))
                    {
                        str = str + "[FF0000][X] ";
                    }
                    if (player7.isLocal)
                    {
                        str = str + "[00CC00]";
                    }
                    else
                    {
                        str = str + "[FFCC00]";
                    }
                    str = str + "[" + Convert.ToString(player7.ID) + "] ";
                    if (player7.isMasterClient)
                    {
                        str = str + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num15 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.team]);
                        if (num15 < 2)
                        {
                            str = str + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num15 == 2)
                        {
                            str = str + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        str = str + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    string str4 = str;
                    str3 = string.Empty;
                    str3 = RCextensions.returnStringFromObject(player7.customProperties[PhotonPlayerProperty.name]);
                    num16 = 0;
                    num16 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.kills]);
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.deaths]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.max_dmg]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.total_dmg]);
                    str = string.Concat(new object[] { str4, string.Empty, str3, "[ffffff]:", num16, "/", num17, "/", num18, "/", num19 });
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        str = str + "[-]";
                    }
                    str = str + "\n";
                }
            }
        }
        this.playerList = str;
        if (PhotonNetwork.isMasterClient && ((!this.isWinning && !this.isLosing) && (this.roundTime >= 5f)))
        {
            int num21;
            if (GameSettings.infectionMode > 0)
            {
                int num20 = 0;
                for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                {
                    PhotonPlayer targetPlayer = PhotonNetwork.playerList[num21];
                    if ((!ignoreList.Contains(targetPlayer.ID) && (targetPlayer.customProperties[PhotonPlayerProperty.dead] != null)) && (targetPlayer.customProperties[PhotonPlayerProperty.isTitan] != null))
                    {
                        if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) == 1)
                        {
                            if (RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead]) && (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.deaths]) > 0))
                            {
                                if (!imatitan.ContainsKey(targetPlayer.ID))
                                {
                                    imatitan.Add(targetPlayer.ID, 2);
                                }
                                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 2);
                                targetPlayer.SetCustomProperties(propertiesToSet);
                                PView.RPC("spawnTitanRPC", targetPlayer, new object[0]);
                            }
                            else if (imatitan.ContainsKey(targetPlayer.ID))
                            {
                                for (int i = 0; i < heroes.Count; i++)
                                {
                                    HERO hero = heroes[i];
                                    if (hero.photonView.owner == targetPlayer)
                                    {
                                        hero.markDie();
                                        hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "noswitchingfagt" });
                                    }
                                }
                            }
                        }
                        else if (!((RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2) || RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead])))
                        {
                            num20++;
                        }
                    }
                }
                if ((num20 <= 0) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.KILL_TITAN))
                {
                    this.gameWin2();
                }
            }
            else if (GameSettings.pointMode > 0)
            {
                if (GameSettings.teamMode > 0)
                {
                    if (this.cyanKills >= GameSettings.pointMode)
                    {
                        object[] parameters = new object[] { "<color=#00FFFF>Team Cyan wins! </color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, parameters);
                        this.gameWin2();
                    }
                    else if (this.magentaKills >= GameSettings.pointMode)
                    {
                        object[] objArray2 = new object[] { "<color=#FF00FF>Team Magenta wins! </color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, objArray2);
                        this.gameWin2();
                    }
                }
                else if (GameSettings.teamMode == 0)
                {
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        PhotonPlayer player9 = PhotonNetwork.playerList[num21];
                        if (RCextensions.returnIntFromObject(player9.customProperties[PhotonPlayerProperty.kills]) >= GameSettings.pointMode)
                        {
                            object[] objArray4 = new object[] { "<color=#FFCC00>" + RCextensions.returnStringFromObject(player9.customProperties[PhotonPlayerProperty.name]).hexColor() + " wins!</color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray4);
                            this.gameWin2();
                        }
                    }
                }
            }
            else if ((GameSettings.pointMode <= 0) && ((GameSettings.bombMode == 1) || (GameSettings.pvpMode > 0)))
            {
                if ((GameSettings.teamMode > 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num23 = 0;
                    int num24 = 0;
                    int num25 = 0;
                    int num26 = 0;
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        PhotonPlayer player10 = PhotonNetwork.playerList[num21];
                        if ((!ignoreList.Contains(player10.ID) && (player10.customProperties[PhotonPlayerProperty.RCteam] != null)) && (player10.customProperties[PhotonPlayerProperty.dead] != null))
                        {
                            if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                num25++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num23++;
                                }
                            }
                            else if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                num26++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num24++;
                                }
                            }
                        }
                    }
                    if ((num25 > 0) && (num26 > 0))
                    {
                        if (num23 == 0)
                        {
                            object[] objArray5 = new object[] { "<color=#FF00FF>Team Magenta wins! </color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray5);
                            this.gameWin2();
                        }
                        else if (num24 == 0)
                        {
                            object[] objArray6 = new object[] { "<color=#00FFFF>Team Cyan wins! </color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray6);
                            this.gameWin2();
                        }
                    }
                }
                else if ((GameSettings.teamMode == 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num27 = 0;
                    string text = "Nobody";
                    PhotonPlayer player11 = PhotonNetwork.playerList[0];
                    for (num21 = 0; num21 < PhotonNetwork.playerList.Length; num21++)
                    {
                        PhotonPlayer player12 = PhotonNetwork.playerList[num21];
                        if (!((player12.customProperties[PhotonPlayerProperty.dead] == null) || RCextensions.returnBoolFromObject(player12.customProperties[PhotonPlayerProperty.dead])))
                        {
                            text = (player11 = player12).uiname;
                            //player11 = player12;
                            num27++;
                        }
                    }
                    if (num27 <= 1)
                    {
                        string str6 = " 5 points added.";
                        if (text == "Nobody")
                        {
                            str6 = string.Empty;
                        }
                        else
                        {
                            for (num21 = 0; num21 < 5; num21++)
                            {
                                this.playerKillInfoUpdate(player11, 0);
                            }
                        }
                        object[] objArray7 = new object[] { "<color=#FFCC00>" + text.hexColor() + " wins." + str6 + "</color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, objArray7);
                        this.gameWin2();
                    }
                }
            }
        }
    }

    private void kickPhotonPlayer(string name)
    {
        UnityEngine.MonoBehaviour.print("KICK " + name + "!!!");
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if ((player.ID.ToString() == name) && !player.isMasterClient)
            {
                PhotonNetwork.CloseConnection(player);
                return;
            }
        }
    }

    private void kickPlayer(string kickPlayer, string kicker)
    {
        KickState state;
        bool flag = false;
        for (int i = 0; i < this.kicklist.Count; i++)
        {
            if (((KickState)this.kicklist[i]).name == kickPlayer)
            {
                state = (KickState)this.kicklist[i];
                state.addKicker(kicker);
                this.tryKick(state);
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            state = new KickState();
            state.init(kickPlayer);
            state.addKicker(kicker);
            this.kicklist.Add(state);
            this.tryKick(state);
        }
    }

    public void kickPlayerRC(PhotonPlayer player, bool ban, string reason)
    {
        string str;
        if (OnPrivateServer)
        {
            str = string.Empty;
            str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            ServerCloseConnection(player, ban, str);
        }
        else
        {
            PhotonNetwork.DestroyPlayerObjects(player);
            PhotonNetwork.CloseConnection(player);
            PView.RPC("ignorePlayer", PhotonTargets.Others, new object[] { player.ID });
            if (!ignoreList.Contains(player.ID))
            {
                ignoreList.Add(player.ID);
                RaiseEventOptions options = new RaiseEventOptions
                {
                    TargetActors = new int[] { player.ID }
                };
                PhotonNetwork.RaiseEvent(0xfe, null, true, options);
            }
            if (!(!ban || banHash.ContainsKey(player.ID)))
            {
                str = string.Empty;
                str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
                banHash.Add(player.ID, str);
            }
            if (reason != string.Empty)
            {
                InRoomChat.ChatInstanse.addLINE("Player " + player.ID.ToString() + " was autobanned. Reason:" + reason);
            }
            this.RecompilePlayerList(0.1f);
        }
    }

    [RPC]
    private void labelRPC(int setting, PhotonMessageInfo info)
    {
        if (PhotonView.Find(setting) != null)
        {
            if (photonView != null)
            {
                PhotonPlayer owner = photonView.owner;
                if (owner == info.sender)
                {
                    string guildname = owner.guildname;
                    HERO component = photonView.GetComponent<HERO>();
                    if (guildname != string.Empty)
                    {
                        component.myNetWorkName.GetComponent<UILabel>().text = "[FFFF00]" + guildname + "\n[FFFFFF]" + owner.name;
                        return;
                    }
                    component.myNetWorkName.GetComponent<UILabel>().text = owner.name;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (gameStart)
        {
            foreach (HERO hERO in heroes)
            {
                hERO.lateUpdate();
            }
            foreach (TITAN_EREN tITAN_EREN in this.eT)
            {
                tITAN_EREN.lateUpdate();
            }
            foreach (TITAN tITAN in titans)
            {
                tITAN.lateUpdate();
            }
            foreach (FEMALE_TITAN fEMALE_TITAN in this.fT)
            {
                fEMALE_TITAN.lateUpdate();
            }
            this.core();
        }
    }


    public void loadconfig()
    {

        int num;
        int num2;
        object[] objArray = new object[280];
        objArray[0] = PlayerPrefs.GetInt("human", 1);
        objArray[1] = PlayerPrefs.GetInt("titan", 1);
        objArray[2] = PlayerPrefs.GetInt("level", 1);
        objArray[3] = PlayerPrefs.GetString("horse", string.Empty);
        objArray[4] = PlayerPrefs.GetString("hair", string.Empty);
        objArray[5] = PlayerPrefs.GetString("eye", string.Empty);
        objArray[6] = PlayerPrefs.GetString("glass", string.Empty);
        objArray[7] = PlayerPrefs.GetString("face", string.Empty);
        objArray[8] = PlayerPrefs.GetString("skin", string.Empty);
        objArray[9] = PlayerPrefs.GetString("costume", string.Empty);
        objArray[10] = PlayerPrefs.GetString("logo", string.Empty);
        objArray[11] = PlayerPrefs.GetString("bladel", string.Empty);
        objArray[12] = PlayerPrefs.GetString("blader", string.Empty);
        objArray[13] = PlayerPrefs.GetString("gas", string.Empty);
        objArray[14] = PlayerPrefs.GetString("hoodie", string.Empty);
        objArray[15] = PlayerPrefs.GetInt("gasenable", 0);
        objArray[0x10] = PlayerPrefs.GetInt("titantype1", -1);
        objArray[0x11] = PlayerPrefs.GetInt("titantype2", -1);
        objArray[0x12] = PlayerPrefs.GetInt("titantype3", -1);
        objArray[0x13] = PlayerPrefs.GetInt("titantype4", -1);
        objArray[20] = PlayerPrefs.GetInt("titantype5", -1);
        objArray[0x15] = PlayerPrefs.GetString("titanhair1", string.Empty);
        objArray[0x16] = PlayerPrefs.GetString("titanhair2", string.Empty);
        objArray[0x17] = PlayerPrefs.GetString("titanhair3", string.Empty);
        objArray[0x18] = PlayerPrefs.GetString("titanhair4", string.Empty);
        objArray[0x19] = PlayerPrefs.GetString("titanhair5", string.Empty);
        objArray[0x1a] = PlayerPrefs.GetString("titaneye1", string.Empty);
        objArray[0x1b] = PlayerPrefs.GetString("titaneye2", string.Empty);
        objArray[0x1c] = PlayerPrefs.GetString("titaneye3", string.Empty);
        objArray[0x1d] = PlayerPrefs.GetString("titaneye4", string.Empty);
        objArray[30] = PlayerPrefs.GetString("titaneye5", string.Empty);
        objArray[0x1f] = 0;
        objArray[0x20] = PlayerPrefs.GetInt("titanR", 0);
        objArray[0x21] = PlayerPrefs.GetString("tree1", "http://i.imgur.com/QhvQaOY.png");
        objArray[0x22] = PlayerPrefs.GetString("tree2", "http://i.imgur.com/QhvQaOY.png");
        objArray[0x23] = PlayerPrefs.GetString("tree3", "http://i.imgur.com/k08IX81.png");
        objArray[0x24] = PlayerPrefs.GetString("tree4", "http://i.imgur.com/k08IX81.png");
        objArray[0x25] = PlayerPrefs.GetString("tree5", "http://i.imgur.com/JQPNchU.png");
        objArray[0x26] = PlayerPrefs.GetString("tree6", "http://i.imgur.com/JQPNchU.png");
        objArray[0x27] = PlayerPrefs.GetString("tree7", "http://i.imgur.com/IZdYWv4.png");
        objArray[40] = PlayerPrefs.GetString("tree8", "http://i.imgur.com/IZdYWv4.png");
        objArray[0x29] = PlayerPrefs.GetString("leaf1", "http://i.imgur.com/oFGV5oL.png");
        objArray[0x2a] = PlayerPrefs.GetString("leaf2", "http://i.imgur.com/oFGV5oL.png");
        objArray[0x2b] = PlayerPrefs.GetString("leaf3", "http://i.imgur.com/mKzawrQ.png");
        objArray[0x2c] = PlayerPrefs.GetString("leaf4", "http://i.imgur.com/mKzawrQ.png");
        objArray[0x2d] = PlayerPrefs.GetString("leaf5", "http://i.imgur.com/Ymzavsi.png");
        objArray[0x2e] = PlayerPrefs.GetString("leaf6", "http://i.imgur.com/Ymzavsi.png");
        objArray[0x2f] = PlayerPrefs.GetString("leaf7", "http://i.imgur.com/oQfD1So.png");
        objArray[0x30] = PlayerPrefs.GetString("leaf8", "http://i.imgur.com/oQfD1So.png");
        objArray[0x31] = PlayerPrefs.GetString("forestG", "http://i.imgur.com/IsDTn7x.png");
        objArray[50] = PlayerPrefs.GetInt("forestR", 0);
        objArray[0x33] = PlayerPrefs.GetString("house1", "http://i.imgur.com/wuy77R8.png");
        objArray[0x34] = PlayerPrefs.GetString("house2", "http://i.imgur.com/wuy77R8.png");
        objArray[0x35] = PlayerPrefs.GetString("house3", "http://i.imgur.com/wuy77R8.png");
        objArray[0x36] = PlayerPrefs.GetString("house4", "http://i.imgur.com/wuy77R8.png");
        objArray[0x37] = PlayerPrefs.GetString("house5", "http://i.imgur.com/wuy77R8.png");
        objArray[0x38] = PlayerPrefs.GetString("house6", "http://i.imgur.com/wuy77R8.png");
        objArray[0x39] = PlayerPrefs.GetString("house7", "http://i.imgur.com/wuy77R8.png");
        objArray[0x3a] = PlayerPrefs.GetString("house8", "http://i.imgur.com/wuy77R8.png");
        objArray[0x3b] = PlayerPrefs.GetString("cityG", "http://i.imgur.com/Mr9ZXip.png");
        objArray[60] = PlayerPrefs.GetString("cityW", "http://i.imgur.com/Tm7XfQP.png");
        objArray[0x3d] = PlayerPrefs.GetString("cityH", "http://i.imgur.com/Q3YXkNM.png");
        objArray[0x3e] = PlayerPrefs.GetInt("skinQ", 0);
        objArray[0x3f] = PlayerPrefs.GetInt("skinQL", 0);
        objArray[0x40] = 0;
        objArray[0x41] = PlayerPrefs.GetString("eren", string.Empty);
        objArray[0x42] = PlayerPrefs.GetString("annie", string.Empty);
        objArray[0x43] = PlayerPrefs.GetString("colossal", string.Empty);
        objArray[0x44] = 100;
        objArray[0x45] = "default";
        objArray[70] = "1";
        objArray[0x47] = "1";
        objArray[0x48] = "1";
        objArray[0x49] = 1f;
        objArray[0x4a] = 1f;
        objArray[0x4b] = 1f;
        objArray[0x4c] = 0;
        objArray[0x4d] = string.Empty;
        objArray[0x4e] = 0;
        objArray[0x4f] = "1.0";
        objArray[80] = "1.0";
        objArray[0x51] = 0;
        objArray[0x52] = PlayerPrefs.GetString("cnumber", "1");
        objArray[0x53] = "30";
        objArray[0x54] = 0;
        objArray[0x55] = PlayerPrefs.GetString("cmax", "20");
        objArray[0x56] = PlayerPrefs.GetString("titanbody1", string.Empty);
        objArray[0x57] = PlayerPrefs.GetString("titanbody2", string.Empty);
        objArray[0x58] = PlayerPrefs.GetString("titanbody3", string.Empty);
        objArray[0x59] = PlayerPrefs.GetString("titanbody4", string.Empty);
        objArray[90] = PlayerPrefs.GetString("titanbody5", string.Empty);
        objArray[0x5b] = 0;
        objArray[0x5c] = PlayerPrefs.GetInt("traildisable", 0);
        objArray[0x5d] = PlayerPrefs.GetInt("wind", 0);
        objArray[0x5e] = PlayerPrefs.GetString("trailskin", string.Empty);
        objArray[0x5f] = PlayerPrefs.GetString("snapshot", "0");
        objArray[0x60] = PlayerPrefs.GetString("trailskin2", string.Empty);
        objArray[0x61] = PlayerPrefs.GetInt("reel", 0);
        objArray[0x62] = PlayerPrefs.GetString("reelin", "LeftControl");
        objArray[0x63] = PlayerPrefs.GetString("reelout", "LeftAlt");
        objArray[100] = 0;
        objArray[0x65] = PlayerPrefs.GetString("tforward", "W");
        objArray[0x66] = PlayerPrefs.GetString("tback", "S");
        objArray[0x67] = PlayerPrefs.GetString("tleft", "A");
        objArray[0x68] = PlayerPrefs.GetString("tright", "D");
        objArray[0x69] = PlayerPrefs.GetString("twalk", "LeftShift");
        objArray[0x6a] = PlayerPrefs.GetString("tjump", "Space");
        objArray[0x6b] = PlayerPrefs.GetString("tpunch", "Q");
        objArray[0x6c] = PlayerPrefs.GetString("tslam", "E");
        objArray[0x6d] = PlayerPrefs.GetString("tgrabfront", "Alpha1");
        objArray[110] = PlayerPrefs.GetString("tgrabback", "Alpha3");
        objArray[0x6f] = PlayerPrefs.GetString("tgrabnape", "Mouse1");
        objArray[0x70] = PlayerPrefs.GetString("tantiae", "Mouse0");
        objArray[0x71] = PlayerPrefs.GetString("tbite", "Alpha2");
        objArray[0x72] = PlayerPrefs.GetString("tcover", "Z");
        objArray[0x73] = PlayerPrefs.GetString("tsit", "X");
        objArray[0x74] = PlayerPrefs.GetInt("reel2", 0);
        objArray[0x75] = PlayerPrefs.GetString("lforward", "W");
        objArray[0x76] = PlayerPrefs.GetString("lback", "S");
        objArray[0x77] = PlayerPrefs.GetString("lleft", "A");
        objArray[120] = PlayerPrefs.GetString("lright", "D");
        objArray[0x79] = PlayerPrefs.GetString("lup", "Mouse1");
        objArray[0x7a] = PlayerPrefs.GetString("ldown", "Mouse0");
        objArray[0x7b] = PlayerPrefs.GetString("lcursor", "X");
        objArray[0x7c] = PlayerPrefs.GetString("lplace", "Space");
        objArray[0x7d] = PlayerPrefs.GetString("ldel", "Backspace");
        objArray[0x7e] = PlayerPrefs.GetString("lslow", "LeftShift");
        objArray[0x7f] = PlayerPrefs.GetString("lrforward", "R");
        objArray[0x80] = PlayerPrefs.GetString("lrback", "F");
        objArray[0x81] = PlayerPrefs.GetString("lrleft", "Q");
        objArray[130] = PlayerPrefs.GetString("lrright", "E");
        objArray[0x83] = PlayerPrefs.GetString("lrccw", "Z");
        objArray[0x84] = PlayerPrefs.GetString("lrcw", "C");
        objArray[0x85] = PlayerPrefs.GetInt("humangui", 0);
        objArray[0x86] = PlayerPrefs.GetString("horse2", string.Empty);
        objArray[0x87] = PlayerPrefs.GetString("hair2", string.Empty);
        objArray[0x88] = PlayerPrefs.GetString("eye2", string.Empty);
        objArray[0x89] = PlayerPrefs.GetString("glass2", string.Empty);
        objArray[0x8a] = PlayerPrefs.GetString("face2", string.Empty);
        objArray[0x8b] = PlayerPrefs.GetString("skin2", string.Empty);
        objArray[140] = PlayerPrefs.GetString("costume2", string.Empty);
        objArray[0x8d] = PlayerPrefs.GetString("logo2", string.Empty);
        objArray[0x8e] = PlayerPrefs.GetString("bladel2", string.Empty);
        objArray[0x8f] = PlayerPrefs.GetString("blader2", string.Empty);
        objArray[0x90] = PlayerPrefs.GetString("gas2", string.Empty);
        objArray[0x91] = PlayerPrefs.GetString("hoodie2", string.Empty);
        objArray[0x92] = PlayerPrefs.GetString("trail2", string.Empty);
        objArray[0x93] = PlayerPrefs.GetString("horse3", string.Empty);
        objArray[0x94] = PlayerPrefs.GetString("hair3", string.Empty);
        objArray[0x95] = PlayerPrefs.GetString("eye3", string.Empty);
        objArray[150] = PlayerPrefs.GetString("glass3", string.Empty);
        objArray[0x97] = PlayerPrefs.GetString("face3", string.Empty);
        objArray[0x98] = PlayerPrefs.GetString("skin3", string.Empty);
        objArray[0x99] = PlayerPrefs.GetString("costume3", string.Empty);
        objArray[0x9a] = PlayerPrefs.GetString("logo3", string.Empty);
        objArray[0x9b] = PlayerPrefs.GetString("bladel3", string.Empty);
        objArray[0x9c] = PlayerPrefs.GetString("blader3", string.Empty);
        objArray[0x9d] = PlayerPrefs.GetString("gas3", string.Empty);
        objArray[0x9e] = PlayerPrefs.GetString("hoodie3", string.Empty);
        objArray[0x9f] = PlayerPrefs.GetString("trail3", string.Empty);
        objArray[0xa1] = PlayerPrefs.GetString("lfast", "LeftControl");
        objArray[0xa2] = PlayerPrefs.GetString("customGround", string.Empty);
        objArray[0xa3] = PlayerPrefs.GetString("forestskyfront", string.Empty);
        objArray[0xa4] = PlayerPrefs.GetString("forestskyback", string.Empty);
        objArray[0xa5] = PlayerPrefs.GetString("forestskyleft", string.Empty);
        objArray[0xa6] = PlayerPrefs.GetString("forestskyright", string.Empty);
        objArray[0xa7] = PlayerPrefs.GetString("forestskyup", string.Empty);
        objArray[0xa8] = PlayerPrefs.GetString("forestskydown", string.Empty);
        objArray[0xa9] = PlayerPrefs.GetString("cityskyfront", string.Empty);
        objArray[170] = PlayerPrefs.GetString("cityskyback", string.Empty);
        objArray[0xab] = PlayerPrefs.GetString("cityskyleft", string.Empty);
        objArray[0xac] = PlayerPrefs.GetString("cityskyright", string.Empty);
        objArray[0xad] = PlayerPrefs.GetString("cityskyup", string.Empty);
        objArray[0xae] = PlayerPrefs.GetString("cityskydown", string.Empty);
        objArray[0xaf] = PlayerPrefs.GetString("customskyfront", string.Empty);
        objArray[0xb0] = PlayerPrefs.GetString("customskyback", string.Empty);
        objArray[0xb1] = PlayerPrefs.GetString("customskyleft", string.Empty);
        objArray[0xb2] = PlayerPrefs.GetString("customskyright", string.Empty);
        objArray[0xb3] = PlayerPrefs.GetString("customskyup", string.Empty);
        objArray[180] = PlayerPrefs.GetString("customskydown", string.Empty);
        objArray[0xb5] = PlayerPrefs.GetInt("dashenable", 0);
        objArray[0xb6] = PlayerPrefs.GetString("dashkey", "RightControl");
        objArray[0xb7] = PlayerPrefs.GetInt("vsync", 0);
        objArray[0xb8] = PlayerPrefs.GetString("fpscap", "0");
        objArray[0xb9] = 0;
        objArray[0xba] = 0;
        objArray[0xbb] = 0;
        objArray[0xbc] = 0;
        objArray[0xbd] = PlayerPrefs.GetInt("speedometer", 0);
        objArray[190] = 0;
        objArray[0xbf] = string.Empty;
        objArray[0xc0] = PlayerPrefs.GetInt("bombMode", 0);
        objArray[0xc1] = PlayerPrefs.GetInt("teamMode", 0);
        objArray[0xc2] = PlayerPrefs.GetInt("rockThrow", 0);
        objArray[0xc3] = PlayerPrefs.GetInt("explodeModeOn", 0);
        objArray[0xc4] = PlayerPrefs.GetString("explodeModeNum", "30");
        objArray[0xc5] = PlayerPrefs.GetInt("healthMode", 0);
        objArray[0xc6] = PlayerPrefs.GetString("healthLower", "100");
        objArray[0xc7] = PlayerPrefs.GetString("healthUpper", "200");
        objArray[200] = PlayerPrefs.GetInt("infectionModeOn", 0);
        objArray[0xc9] = PlayerPrefs.GetString("infectionModeNum", "1");
        objArray[0xca] = PlayerPrefs.GetInt("banEren", 0);
        objArray[0xcb] = PlayerPrefs.GetInt("moreTitanOn", 0);
        objArray[0xcc] = PlayerPrefs.GetString("moreTitanNum", "1");
        objArray[0xcd] = PlayerPrefs.GetInt("damageModeOn", 0);
        objArray[0xce] = PlayerPrefs.GetString("damageModeNum", "1000");
        objArray[0xcf] = PlayerPrefs.GetInt("sizeMode", 0);
        objArray[0xd0] = PlayerPrefs.GetString("sizeLower", "1.0");
        objArray[0xd1] = PlayerPrefs.GetString("sizeUpper", "3.0");
        objArray[210] = PlayerPrefs.GetInt("spawnModeOn", 0);
        objArray[0xd3] = PlayerPrefs.GetString("nRate", "20.0");
        objArray[0xd4] = PlayerPrefs.GetString("aRate", "20.0");
        objArray[0xd5] = PlayerPrefs.GetString("jRate", "20.0");
        objArray[0xd6] = PlayerPrefs.GetString("cRate", "20.0");
        objArray[0xd7] = PlayerPrefs.GetString("pRate", "20.0");
        objArray[0xd8] = PlayerPrefs.GetInt("horseMode", 0);
        objArray[0xd9] = PlayerPrefs.GetInt("waveModeOn", 0);
        objArray[0xda] = PlayerPrefs.GetString("waveModeNum", "1");
        objArray[0xdb] = PlayerPrefs.GetInt("friendlyMode", 0);
        objArray[220] = PlayerPrefs.GetInt("pvpMode", 0);
        objArray[0xdd] = PlayerPrefs.GetInt("maxWaveOn", 0);
        objArray[0xde] = PlayerPrefs.GetString("maxWaveNum", "20");
        objArray[0xdf] = PlayerPrefs.GetInt("endlessModeOn", 0);
        objArray[0xe0] = PlayerPrefs.GetString("endlessModeNum", "10");
        objArray[0xe1] = PlayerPrefs.GetString("motd", string.Empty);
        objArray[0xe2] = PlayerPrefs.GetInt("pointModeOn", 0);
        objArray[0xe3] = PlayerPrefs.GetString("pointModeNum", "50");
        objArray[0xe4] = PlayerPrefs.GetInt("ahssReload", 0);
        objArray[0xe5] = PlayerPrefs.GetInt("punkWaves", 0);
        objArray[230] = 0;
        objArray[0xe7] = PlayerPrefs.GetInt("mapOn", 0);
        objArray[0xe8] = PlayerPrefs.GetString("mapMaximize", "Tab");
        objArray[0xe9] = PlayerPrefs.GetString("mapToggle", "M");
        objArray[0xea] = PlayerPrefs.GetString("mapReset", "K");
        objArray[0xeb] = PlayerPrefs.GetInt("globalDisableMinimap", 0);
        objArray[0xec] = PlayerPrefs.GetString("chatRebind", "None");
        objArray[0xed] = PlayerPrefs.GetString("hforward", "W");
        objArray[0xee] = PlayerPrefs.GetString("hback", "S");
        objArray[0xef] = PlayerPrefs.GetString("hleft", "A");
        objArray[240] = PlayerPrefs.GetString("hright", "D");
        objArray[0xf1] = PlayerPrefs.GetString("hwalk", "LeftShift");
        objArray[0xf2] = PlayerPrefs.GetString("hjump", "Q");
        objArray[0xf3] = PlayerPrefs.GetString("hmount", "LeftControl");
        objArray[0xf4] = PlayerPrefs.GetInt("chatfeed", 0);
        objArray[0xf5] = 0;
        objArray[0xf6] = PlayerPrefs.GetFloat("bombR", 1f);
        objArray[0xf7] = PlayerPrefs.GetFloat("bombG", 1f);
        objArray[0xf8] = PlayerPrefs.GetFloat("bombB", 1f);
        objArray[0xf9] = PlayerPrefs.GetFloat("bombA", 1f);
        objArray[250] = PlayerPrefs.GetInt("bombRadius", 5);
        objArray[0xfb] = PlayerPrefs.GetInt("bombRange", 5);
        objArray[0xfc] = PlayerPrefs.GetInt("bombSpeed", 5);
        objArray[0xfd] = PlayerPrefs.GetInt("bombCD", 5);
        objArray[0xfe] = PlayerPrefs.GetString("cannonUp", "W");
        objArray[0xff] = PlayerPrefs.GetString("cannonDown", "S");
        objArray[0x100] = PlayerPrefs.GetString("cannonLeft", "A");
        objArray[0x101] = PlayerPrefs.GetString("cannonRight", "D");
        objArray[0x102] = PlayerPrefs.GetString("cannonFire", "Q");
        objArray[0x103] = PlayerPrefs.GetString("cannonMount", "G");
        objArray[260] = PlayerPrefs.GetString("cannonSlow", "LeftShift");
        objArray[0x105] = PlayerPrefs.GetInt("deadlyCannon", 0);
        objArray[0x106] = PlayerPrefs.GetString("liveCam", "Y");
        objArray[0x107] = 0;
        //objArray[263] = true;

        inputRC = new InputManagerRC();
        inputRC.setInputHuman(InputCodeRC.reelin, (string)objArray[0x62]);
        inputRC.setInputHuman(InputCodeRC.reelout, (string)objArray[0x63]);
        inputRC.setInputHuman(InputCodeRC.dash, (string)objArray[0xb6]);
        inputRC.setInputHuman(InputCodeRC.mapMaximize, (string)objArray[0xe8]);
        inputRC.setInputHuman(InputCodeRC.mapToggle, (string)objArray[0xe9]);
        inputRC.setInputHuman(InputCodeRC.mapReset, (string)objArray[0xea]);
        inputRC.setInputHuman(InputCodeRC.chat, (string)objArray[0xec]);
        inputRC.setInputHuman(InputCodeRC.liveCam, (string)objArray[0x106]);
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[0xe8]))
        {
            objArray[0xe8] = "None";
        }
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[0xe9]))
        {
            objArray[0xe9] = "None";
        }
        if (!Enum.IsDefined(typeof(KeyCode), (string)objArray[0xea]))
        {
            objArray[0xea] = "None";
        }
        for (num = 0; num < 15; num++)
        {
            inputRC.setInputTitan(num, (string)objArray[0x65 + num]);
        }
        for (num = 0; num < 0x10; num++)
        {
            inputRC.setInputLevel(num, (string)objArray[0x75 + num]);
        }
        for (num = 0; num < 7; num++)
        {
            inputRC.setInputHorse(num, (string)objArray[0xed + num]);
        }
        for (num = 0; num < 7; num++)
        {
            inputRC.setInputCannon(num, (string)objArray[0xfe + num]);
        }
        inputRC.setInputLevel(InputCodeRC.levelFast, (string)objArray[0xa1]);
        Application.targetFrameRate = -1;
        if (int.TryParse((string)objArray[0xb8], out num2) && (num2 > 0))
        {
            Application.targetFrameRate = num2;
        }
        QualitySettings.vSyncCount = 0;
        if (((int)objArray[0xb7]) == 1)
        {
            QualitySettings.vSyncCount = 1;
        }
        AudioListener.volume = PlayerPrefs.GetFloat("vol", 1f);
        QualitySettings.masterTextureLimit = PlayerPrefs.GetInt("skinQ", 0);
        linkHash = new ExitGames.Client.Photon.Hashtable[] { new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable(), new ExitGames.Client.Photon.Hashtable() };
        settings = objArray;
        this.scroll = Vector2.zero;
        this.scroll2 = Vector2.zero;
        this.distanceSlider = PlayerPrefs.GetFloat("cameraDistance", 1f);
        this.mouseSlider = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
        this.qualitySlider = PlayerPrefs.GetFloat("GameQuality", 0f);
        this.transparencySlider = 1f;
        FengGameManagerMKII.settings[271] = false;
        FengGameManagerMKII.settings[270] = PlayerPrefs.GetString("AnimatedName", "None");
        FengGameManagerMKII.settings[267] = PlayerPrefs.GetString("Faded", "None");
        FengGameManagerMKII.settings[268] = PlayerPrefs.GetString("Linear", "None");
        FengGameManagerMKII.settings[269] = PlayerPrefs.GetString("Rebound", "None");
        //StatsTab.AddLine("Compleated loadconfig. FengGameManagerMKII.settings[271] = " + FengGameManagerMKII.settings[271].ToString() +
        //                                     "\n FengGameManagerMKII.settings[270] = " + FengGameManagerMKII.settings[270].ToString() +
        //                                     "\n FengGameManagerMKII.settings[267] = " + FengGameManagerMKII.settings[267].ToString() +
        //                                     "\n FengGameManagerMKII.settings[268] = " + FengGameManagerMKII.settings[268].ToString() +
        //                                     "\n FengGameManagerMKII.settings[269] = " + FengGameManagerMKII.settings[269].ToString(), StatsTab.DebugType.LOG);
    }
    private void loadskin()
    {
        GameObject[] objArray;
        int num;
        GameObject obj2;
        if (((int)settings[0x40]) >= 100)
        {
            string[] strArray2 = new string[] { "Flare", "LabelInfoBottomRight", "LabelNetworkStatus", "skill_cd_bottom", "GasUI" };
            objArray = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            for (num = 0; num < objArray.Length; num++)
            {
                obj2 = objArray[num];
                if ((obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply")) || obj2.name.Contains("gameobjectOutSide"))
                {
                    UnityEngine.Object.Destroy(obj2);
                }
            }
            BRM.CacheGameObject.Find("Cube_001").renderer.material.mainTexture = CacheResources.RCLoad<Material>("grass").mainTexture;
            UnityEngine.Object.Instantiate(RCassets.Load("spawnPlayer"), new Vector3(-10f, 1f, -10f), new Quaternion(0f, 0f, 0f, 1f));
            for (num = 0; num < strArray2.Length; num++)
            {
                string name = strArray2[num];
                GameObject obj3 = BRM.CacheGameObject.Find(name);
                if (obj3 != null)
                {
                    UnityEngine.Object.Destroy(obj3);
                }
            }
            IN_GAME_MAIN_CAMERA.spectate.disable = true;
            return;
        }
        else
        {
            GameObject obj4;
            string[] strArray3;
            int num2;
            InstantiateTracker.instance.Dispose();
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && PhotonNetwork.isMasterClient)
            {
                this.updateTime = 1f;
                if (oldScriptLogic != currentScriptLogic)
                {
                    intVariables.Clear();
                    boolVariables.Clear();
                    stringVariables.Clear();
                    floatVariables.Clear();
                    globalVariables.Clear();
                    RCEvents.Clear();
                    RCVariableNames.Clear();
                    playerVariables.Clear();
                    titanVariables.Clear();
                    RCRegionTriggers.Clear();
                    oldScriptLogic = currentScriptLogic;
                    this.compileScript(currentScriptLogic);
                    if (RCEvents.ContainsKey("OnFirstLoad"))
                    {
                        RCEvent event2 = (RCEvent)RCEvents["OnFirstLoad"];
                        event2.checkEvent();
                    }
                }
                if (RCEvents.ContainsKey("OnRoundStart"))
                {
                    ((RCEvent)RCEvents["OnRoundStart"]).checkEvent();
                }
                PView.RPC("setMasterRC", PhotonTargets.All, new object[0]);
            }
            logicLoaded = true;
            this.racingSpawnPoint = new Vector3(0f, 0f, 0f);
            this.racingSpawnPointSet = false;
            this.racingDoors = new List<GameObject>();
            this.allowedToCannon = new Dictionary<int, CannonValues>();
            if ((!level.StartsWith("Custom") && (((int)settings[2]) == 1)) && ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || PhotonNetwork.isMasterClient))
            {
                obj4 = BRM.CacheGameObject.Find("aot_supply");
                if ((obj4 != null) && (Minimap.instance != null))
                {
                    Minimap.instance.TrackGameObjectOnMinimap(obj4, Color.white, false, true, Minimap.IconStyle.SUPPLY);
                }
                string url = string.Empty;
                string str3 = string.Empty;
                string n = string.Empty;
                strArray3 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                if (LevelInfo.getInfo(level).mapName.Contains("City"))
                {
                    for (num = 0x33; num < 0x3b; num++)
                    {
                        url = url + ((string)settings[num]) + ",";
                    }
                    url.TrimEnd(new char[] { ',' });
                    num2 = 0;
                    while (num2 < 250)
                    {
                        n = n + Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        num2++;
                    }
                    str3 = ((string)settings[0x3b]) + "," + ((string)settings[60]) + "," + ((string)settings[0x3d]);
                    for (num = 0; num < 6; num++)
                    {
                        strArray3[num] = (string)settings[num + 0xa9];
                    }
                }
                else if (LevelInfo.getInfo(level).mapName.Contains("Forest"))
                {
                    for (int i = 0x21; i < 0x29; i++)
                    {
                        url = url + ((string)settings[i]) + ",";
                    }
                    url.TrimEnd(new char[] { ',' });
                    for (int j = 0x29; j < 0x31; j++)
                    {
                        str3 = str3 + ((string)settings[j]) + ",";
                    }
                    str3 = str3 + ((string)settings[0x31]);
                    for (int k = 0; k < 150; k++)
                    {
                        string str5 = Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        n = n + str5;
                        if (((int)settings[50]) == 0)
                        {
                            n = n + str5;
                        }
                        else
                        {
                            n = n + Convert.ToString((int)UnityEngine.Random.Range((float)0f, (float)8f));
                        }
                    }
                    for (num = 0; num < 6; num++)
                    {
                        strArray3[num] = (string)settings[num + 0xa3];
                    }
                }
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    base.StartCoroutine(this.loadskinE(n, url, str3, strArray3));
                }
                else if (PhotonNetwork.isMasterClient)
                {
                    PView.RPC("loadskinRPC", PhotonTargets.AllBuffered, new object[] { n, url, str3, strArray3 });
                }
            }
            else if (level.StartsWith("Custom") && (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE))
            {
                GameObject[] objArray3 = GameObject.FindGameObjectsWithTag("playerRespawn");
                for (num = 0; num < objArray3.Length; num++)
                {
                    obj4 = objArray3[num];
                    obj4.transform.position = new Vector3(UnityEngine.Random.Range((float)-5f, (float)5f), 0f, UnityEngine.Random.Range((float)-5f, (float)5f));
                }
                objArray = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
                for (num = 0; num < objArray.Length; num++)
                {
                    obj2 = objArray[num];
                    if (obj2.name.Contains("TREE") || obj2.name.Contains("aot_supply"))
                    {
                        UnityEngine.Object.Destroy(obj2);
                    }
                    else if (((obj2.name == "Cube_001") && (obj2.transform.parent.gameObject.tag != "player")) && (obj2.renderer != null))
                    {
                        this.groundList.Add(obj2);
                        obj2.renderer.material.mainTexture = ((Material)RCassets.Load("grass")).mainTexture;
                    }
                }
                if (PhotonNetwork.isMasterClient)
                {
                    int num6;
                    strArray3 = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
                    for (num = 0; num < 6; num++)
                    {
                        strArray3[num] = (string)settings[num + 0xaf];
                    }
                    strArray3[6] = (string)settings[0xa2];
                    if (int.TryParse((string)settings[0x55], out num6))
                    {
                        GameSettings.titanCap = num6;
                    }
                    else
                    {
                        GameSettings.titanCap = 0;
                        settings[0x55] = "0";
                    }
                    GameSettings.titanCap = Math.Min(50, GameSettings.titanCap);
                    PView.RPC("clearlevel", PhotonTargets.AllBuffered, new object[] { strArray3, GameSettings.gameType });
                    RCRegions.Clear();
                    bool needLoad;
                    if (needLoad = (oldScript != currentScript))
                    {
                        //while (customObjects.Count > 0)
                        //{
                        //    GameObject gameObject5 = customObjects.Dequeue();
                        //    if (gameObject5 != null)
                        //    {
                        //        UnityEngine.Object.Destroy(gameObject5);
                        //    }
                        //}
                        ClearCustomObjects();
                        ExitGames.Client.Photon.Hashtable hashtable;
                        this.levelCache.Clear();
                        this.titanSpawns.Clear();
                        this.playerSpawnsC.Clear();
                        this.playerSpawnsM.Clear();
                        this.titanSpawners.Clear();
                        currentLevel = string.Empty;
                        if (currentScript == string.Empty)
                        {
                            hashtable = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                            PhotonNetwork.player.SetCustomProperties(hashtable);
                            oldScript = currentScript;
                        }
                        else
                        {
                            string[] strArray4 = Regex.Replace(currentScript, @"\s+", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Split(new char[] { ';' });
                            for (num = 0; num < (Mathf.FloorToInt((float)((strArray4.Length - 1) / 100)) + 1); num++)
                            {
                                string[] strArray5;
                                int num7;
                                string[] strArray6;
                                string str6;
                                if (num < Mathf.FloorToInt((float)(strArray4.Length / 100)))
                                {
                                    strArray5 = new string[0x65];
                                    num7 = 0;
                                    num2 = 100 * num;
                                    while (num2 < ((100 * num) + 100))
                                    {
                                        if (strArray4[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray6 = strArray4[num2].Split(new char[] { ',' });
                                            if (strArray6[1] == "titan")
                                            {
                                                this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerC")
                                            {
                                                this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerM")
                                            {
                                                this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                        }
                                        strArray5[num7] = strArray4[num2];
                                        num7++;
                                        num2++;
                                    }
                                    str6 = UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                                    strArray5[100] = str6;
                                    currentLevel = currentLevel + str6;
                                    this.levelCache.Add(strArray5);
                                }
                                else
                                {
                                    strArray5 = new string[(strArray4.Length % 100) + 1];
                                    num7 = 0;
                                    for (num2 = 100 * num; num2 < ((100 * num) + (strArray4.Length % 100)); num2++)
                                    {
                                        if (strArray4[num2].StartsWith("spawnpoint"))
                                        {
                                            strArray6 = strArray4[num2].Split(new char[] { ',' });
                                            if (strArray6[1] == "titan")
                                            {
                                                this.titanSpawns.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerC")
                                            {
                                                this.playerSpawnsC.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                            else if (strArray6[1] == "playerM")
                                            {
                                                this.playerSpawnsM.Add(new Vector3(Convert.ToSingle(strArray6[2]), Convert.ToSingle(strArray6[3]), Convert.ToSingle(strArray6[4])));
                                            }
                                        }
                                        strArray5[num7] = strArray4[num2];
                                        num7++;
                                    }
                                    str6 = UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                                    strArray5[strArray4.Length % 100] = str6;
                                    currentLevel += str6;
                                    this.levelCache.Add(strArray5);
                                }
                            }
                            List<string> list = new List<string>();
                            foreach (Vector3 vector in this.titanSpawns)
                            {
                                list.Add("titan," + vector.x.ToString() + "," + vector.y.ToString() + "," + vector.z.ToString());
                            }
                            foreach (Vector3 vector in this.playerSpawnsC)
                            {
                                list.Add("playerC," + vector.x.ToString() + "," + vector.y.ToString() + "," + vector.z.ToString());
                            }
                            foreach (Vector3 vector in this.playerSpawnsM)
                            {
                                list.Add("playerM," + vector.x.ToString() + "," + vector.y.ToString() + "," + vector.z.ToString());
                            }
                            string item = "a" + UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                            list.Add(item);
                            currentLevel = item + currentLevel;
                            this.levelCache.Insert(0, list.ToArray());
                            string str8 = "z" + UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                            this.levelCache.Add(new string[] { str8 });
                            currentLevel += str8;
                            hashtable = new ExitGames.Client.Photon.Hashtable();
                            hashtable.Add(PhotonPlayerProperty.currentLevel, currentLevel);
                            PhotonNetwork.player.SetCustomProperties(hashtable);
                            oldScript = currentScript;
                        }
                    }
                    for (num = 0; num < PhotonNetwork.playerList.Length; num++)
                    {
                        PhotonPlayer player = PhotonNetwork.playerList[num];
                        if (!player.isMasterClient)
                        {
                            this.playersRPC.Add(player);
                        }
                    }
                    base.StartCoroutine(this.customlevelE(this.playersRPC));
                    if (needLoad)
                    {
                        customLevelLoaded = false;
                        base.StartCoroutine(this.customlevelcache());
                        return;
                    }
                    customLevelLoaded = true;
                }
                else
                {
                    customLevelLoaded = true;
                }
            }
        }
    }

    private IEnumerator loadskinE(string n, string url, string url2, string[] skybox)
    {
        bool mipmap = true;
        bool iteratorVariable1 = false;
        if (((int)settings[0x3f]) == 1)
        {
            mipmap = false;
        }
        if ((skybox.Length > 5) && (((((skybox[0] != string.Empty) || (skybox[1] != string.Empty)) || ((skybox[2] != string.Empty) || (skybox[3] != string.Empty))) || (skybox[4] != string.Empty)) || (skybox[5] != string.Empty)))
        {
            string key = string.Join(",", skybox);
            if (!linkHash[1].ContainsKey(key))
            {
                iteratorVariable1 = true;
                Material material = Camera.main.GetComponent<Skybox>().material;
                string iteratorVariable4 = skybox[0];
                string iteratorVariable5 = skybox[1];
                string iteratorVariable6 = skybox[2];
                string iteratorVariable7 = skybox[3];
                string iteratorVariable8 = skybox[4];
                string iteratorVariable9 = skybox[5];
                if ((iteratorVariable4.EndsWith(".jpg") || iteratorVariable4.EndsWith(".png")) || iteratorVariable4.EndsWith(".jpeg"))
                {
                    WWW link = new WWW(iteratorVariable4);
                    yield return link;
                    Texture2D texture = RCextensions.loadimage(link, mipmap, 0x7a120);
                    link.Dispose();
                    texture.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_FrontTex", texture);
                }
                if ((iteratorVariable5.EndsWith(".jpg") || iteratorVariable5.EndsWith(".png")) || iteratorVariable5.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable12 = new WWW(iteratorVariable5);
                    yield return iteratorVariable12;
                    Texture2D iteratorVariable13 = RCextensions.loadimage(iteratorVariable12, mipmap, 0x7a120);
                    iteratorVariable12.Dispose();
                    iteratorVariable13.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_BackTex", iteratorVariable13);
                }
                if ((iteratorVariable6.EndsWith(".jpg") || iteratorVariable6.EndsWith(".png")) || iteratorVariable6.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable14 = new WWW(iteratorVariable6);
                    yield return iteratorVariable14;
                    Texture2D iteratorVariable15 = RCextensions.loadimage(iteratorVariable14, mipmap, 0x7a120);
                    iteratorVariable14.Dispose();
                    iteratorVariable15.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_LeftTex", iteratorVariable15);
                }
                if ((iteratorVariable7.EndsWith(".jpg") || iteratorVariable7.EndsWith(".png")) || iteratorVariable7.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable16 = new WWW(iteratorVariable7);
                    yield return iteratorVariable16;
                    Texture2D iteratorVariable17 = RCextensions.loadimage(iteratorVariable16, mipmap, 0x7a120);
                    iteratorVariable16.Dispose();
                    iteratorVariable17.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_RightTex", iteratorVariable17);
                }
                if ((iteratorVariable8.EndsWith(".jpg") || iteratorVariable8.EndsWith(".png")) || iteratorVariable8.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable18 = new WWW(iteratorVariable8);
                    yield return iteratorVariable18;
                    Texture2D iteratorVariable19 = RCextensions.loadimage(iteratorVariable18, mipmap, 0x7a120);
                    iteratorVariable18.Dispose();
                    iteratorVariable19.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_UpTex", iteratorVariable19);
                }
                if ((iteratorVariable9.EndsWith(".jpg") || iteratorVariable9.EndsWith(".png")) || iteratorVariable9.EndsWith(".jpeg"))
                {
                    WWW iteratorVariable20 = new WWW(iteratorVariable9);
                    yield return iteratorVariable20;
                    Texture2D iteratorVariable21 = RCextensions.loadimage(iteratorVariable20, mipmap, 0x7a120);
                    iteratorVariable20.Dispose();
                    iteratorVariable21.wrapMode = TextureWrapMode.Clamp;
                    material.SetTexture("_DownTex", iteratorVariable21);
                }
                Camera.main.GetComponent<Skybox>().material = material;
                skyMaterial = material;
                linkHash[1].Add(key, material);
            }
            else
            {
                Camera.main.GetComponent<Skybox>().material = (Material)linkHash[1][key];
                skyMaterial = (Material)linkHash[1][key];
            }
        }
        if (LevelInfo.getInfo(level).mapName.Contains("Forest"))
        {
            string[] iteratorVariable22 = url.Split(new char[] { ',' });
            string[] iteratorVariable23 = url2.Split(new char[] { ',' });
            int startIndex = 0;
            object[] iteratorVariable25 = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable26 in iteratorVariable25)
            {
                if (iteratorVariable26 != null)
                {
                    if (iteratorVariable26.name.Contains("TREE") && (n.Length > (startIndex + 1)))
                    {
                        int iteratorVariable28;
                        int iteratorVariable27;
                        string s = n.Substring(startIndex, 1);
                        string iteratorVariable30 = n.Substring(startIndex + 1, 1);
                        if ((((int.TryParse(s, out iteratorVariable27) && int.TryParse(iteratorVariable30, out iteratorVariable28)) && ((iteratorVariable27 >= 0) && (iteratorVariable27 < 8))) && (((iteratorVariable28 >= 0) && (iteratorVariable28 < 8)) && ((iteratorVariable22.Length >= 8) && (iteratorVariable23.Length >= 8)))) && ((iteratorVariable22[iteratorVariable27] != null) && (iteratorVariable23[iteratorVariable28] != null)))
                        {
                            string iteratorVariable31 = iteratorVariable22[iteratorVariable27];
                            string iteratorVariable32 = iteratorVariable23[iteratorVariable28];
                            foreach (Renderer iteratorVariable33 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[0x16]))
                                {
                                    if ((iteratorVariable31.EndsWith(".jpg") || iteratorVariable31.EndsWith(".png")) || iteratorVariable31.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[2].ContainsKey(iteratorVariable31))
                                        {
                                            WWW iteratorVariable34 = new WWW(iteratorVariable31);
                                            yield return iteratorVariable34;
                                            Texture2D iteratorVariable35 = RCextensions.loadimage(iteratorVariable34, mipmap, 0xf4240);
                                            iteratorVariable34.Dispose();
                                            if (!linkHash[2].ContainsKey(iteratorVariable31))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable33.material.mainTexture = iteratorVariable35;
                                                linkHash[2].Add(iteratorVariable31, iteratorVariable33.material);
                                                iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                            }
                                            else
                                            {
                                                iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[2][iteratorVariable31];
                                        }
                                    }
                                }
                                else if (iteratorVariable33.name.Contains(FengGameManagerMKII.s[0x17]))
                                {
                                    if ((iteratorVariable32.EndsWith(".jpg") || iteratorVariable32.EndsWith(".png")) || iteratorVariable32.EndsWith(".jpeg"))
                                    {
                                        if (!linkHash[0].ContainsKey(iteratorVariable32))
                                        {
                                            WWW iteratorVariable36 = new WWW(iteratorVariable32);
                                            yield return iteratorVariable36;
                                            Texture2D iteratorVariable37 = RCextensions.loadimage(iteratorVariable36, mipmap, 0x30d40);
                                            iteratorVariable36.Dispose();
                                            if (!linkHash[0].ContainsKey(iteratorVariable32))
                                            {
                                                iteratorVariable1 = true;
                                                iteratorVariable33.material.mainTexture = iteratorVariable37;
                                                linkHash[0].Add(iteratorVariable32, iteratorVariable33.material);
                                                iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                            }
                                            else
                                            {
                                                iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                            }
                                        }
                                        else
                                        {
                                            iteratorVariable33.material = (Material)linkHash[0][iteratorVariable32];
                                        }
                                    }
                                    else if (iteratorVariable32.ToLower() == "transparent")
                                    {
                                        iteratorVariable33.enabled = false;
                                    }
                                }
                            }
                        }
                        startIndex += 2;
                    }
                    else if ((iteratorVariable26.name.Contains("Cube_001") && (iteratorVariable26.transform.parent.gameObject.tag != "Player")) && ((iteratorVariable23.Length > 8) && (iteratorVariable23[8] != null)))
                    {
                        string iteratorVariable38 = iteratorVariable23[8];
                        if ((iteratorVariable38.EndsWith(".jpg") || iteratorVariable38.EndsWith(".png")) || iteratorVariable38.EndsWith(".jpeg"))
                        {
                            foreach (Renderer iteratorVariable39 in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[0].ContainsKey(iteratorVariable38))
                                {
                                    WWW iteratorVariable40 = new WWW(iteratorVariable38);
                                    yield return iteratorVariable40;
                                    Texture2D iteratorVariable41 = RCextensions.loadimage(iteratorVariable40, mipmap, 0x30d40);
                                    iteratorVariable40.Dispose();
                                    if (!linkHash[0].ContainsKey(iteratorVariable38))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable39.material.mainTexture = iteratorVariable41;
                                        linkHash[0].Add(iteratorVariable38, iteratorVariable39.material);
                                        iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                    }
                                    else
                                    {
                                        iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                    }
                                }
                                else
                                {
                                    iteratorVariable39.material = (Material)linkHash[0][iteratorVariable38];
                                }
                            }
                        }
                        else if (iteratorVariable38.ToLower() == "transparent")
                        {
                            foreach (Renderer renderer in iteratorVariable26.GetComponentsInChildren<Renderer>())
                            {
                                renderer.enabled = false;
                            }
                        }
                    }
                }
            }
        }
        else if (LevelInfo.getInfo(level).mapName.Contains("City"))
        {
            string[] iteratorVariable42 = url.Split(new char[] { ',' });
            string[] iteratorVariable43 = url2.Split(new char[] { ',' });
            string iteratorVariable44 = iteratorVariable43[2];
            int iteratorVariable45 = 0;
            object[] iteratorVariable46 = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
            foreach (GameObject iteratorVariable47 in iteratorVariable46)
            {
                if ((iteratorVariable47 != null) && (iteratorVariable47.name.Contains("Cube_") && (iteratorVariable47.transform.parent.gameObject.tag != "Player")))
                {
                    if (iteratorVariable47.name.EndsWith("001"))
                    {
                        if ((iteratorVariable43.Length > 0) && (iteratorVariable43[0] != null))
                        {
                            string iteratorVariable48 = iteratorVariable43[0];
                            if ((iteratorVariable48.EndsWith(".jpg") || iteratorVariable48.EndsWith(".png")) || iteratorVariable48.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable49 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable48))
                                    {
                                        WWW iteratorVariable50 = new WWW(iteratorVariable48);
                                        yield return iteratorVariable50;
                                        Texture2D iteratorVariable51 = RCextensions.loadimage(iteratorVariable50, mipmap, 0x30d40);
                                        iteratorVariable50.Dispose();
                                        if (!linkHash[0].ContainsKey(iteratorVariable48))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable49.material.mainTexture = iteratorVariable51;
                                            linkHash[0].Add(iteratorVariable48, iteratorVariable49.material);
                                            iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                        }
                                        else
                                        {
                                            iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable49.material = (Material)linkHash[0][iteratorVariable48];
                                    }
                                }
                            }
                            else if (iteratorVariable48.ToLower() == "transparent")
                            {
                                foreach (Renderer renderer in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    renderer.enabled = false;
                                }
                            }
                        }
                    }
                    else if (((iteratorVariable47.name.EndsWith("006") || iteratorVariable47.name.EndsWith("007")) || (iteratorVariable47.name.EndsWith("015") || iteratorVariable47.name.EndsWith("000"))) || ((iteratorVariable47.name.EndsWith("002") && (iteratorVariable47.transform.position.x == 0f)) && ((iteratorVariable47.transform.position.y == 0f) && (iteratorVariable47.transform.position.z == 0f))))
                    {
                        if ((iteratorVariable43.Length > 0) && (iteratorVariable43[1] != null))
                        {
                            string iteratorVariable52 = iteratorVariable43[1];
                            if ((iteratorVariable52.EndsWith(".jpg") || iteratorVariable52.EndsWith(".png")) || iteratorVariable52.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable53 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[0].ContainsKey(iteratorVariable52))
                                    {
                                        WWW iteratorVariable54 = new WWW(iteratorVariable52);
                                        yield return iteratorVariable54;
                                        Texture2D iteratorVariable55 = RCextensions.loadimage(iteratorVariable54, mipmap, 0x30d40);
                                        iteratorVariable54.Dispose();
                                        if (!linkHash[0].ContainsKey(iteratorVariable52))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable53.material.mainTexture = iteratorVariable55;
                                            linkHash[0].Add(iteratorVariable52, iteratorVariable53.material);
                                            iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                        }
                                        else
                                        {
                                            iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable53.material = (Material)linkHash[0][iteratorVariable52];
                                    }
                                }
                            }
                        }
                    }
                    else if ((iteratorVariable47.name.EndsWith("005") || iteratorVariable47.name.EndsWith("003")) || ((iteratorVariable47.name.EndsWith("002") && (((iteratorVariable47.transform.position.x != 0f) || (iteratorVariable47.transform.position.y != 0f)) || (iteratorVariable47.transform.position.z != 0f))) && (n.Length > iteratorVariable45)))
                    {
                        int iteratorVariable56;
                        string iteratorVariable57 = n.Substring(iteratorVariable45, 1);
                        if (((int.TryParse(iteratorVariable57, out iteratorVariable56) && (iteratorVariable56 >= 0)) && ((iteratorVariable56 < 8) && (iteratorVariable42.Length >= 8))) && (iteratorVariable42[iteratorVariable56] != null))
                        {
                            string iteratorVariable58 = iteratorVariable42[iteratorVariable56];
                            if ((iteratorVariable58.EndsWith(".jpg") || iteratorVariable58.EndsWith(".png")) || iteratorVariable58.EndsWith(".jpeg"))
                            {
                                foreach (Renderer iteratorVariable59 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                                {
                                    if (!linkHash[2].ContainsKey(iteratorVariable58))
                                    {
                                        WWW iteratorVariable60 = new WWW(iteratorVariable58);
                                        yield return iteratorVariable60;
                                        Texture2D iteratorVariable61 = RCextensions.loadimage(iteratorVariable60, mipmap, 0xf4240);
                                        iteratorVariable60.Dispose();
                                        if (!linkHash[2].ContainsKey(iteratorVariable58))
                                        {
                                            iteratorVariable1 = true;
                                            iteratorVariable59.material.mainTexture = iteratorVariable61;
                                            linkHash[2].Add(iteratorVariable58, iteratorVariable59.material);
                                            iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                        }
                                        else
                                        {
                                            iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                        }
                                    }
                                    else
                                    {
                                        iteratorVariable59.material = (Material)linkHash[2][iteratorVariable58];
                                    }
                                }
                            }
                        }
                        iteratorVariable45++;
                    }
                    else if ((iteratorVariable47.name.EndsWith("019") || iteratorVariable47.name.EndsWith("020")) && ((iteratorVariable43.Length > 2) && (iteratorVariable43[2] != null)))
                    {
                        string iteratorVariable62 = iteratorVariable43[2];
                        if ((iteratorVariable62.EndsWith(".jpg") || iteratorVariable62.EndsWith(".png")) || iteratorVariable62.EndsWith(".jpeg"))
                        {
                            foreach (Renderer iteratorVariable63 in iteratorVariable47.GetComponentsInChildren<Renderer>())
                            {
                                if (!linkHash[2].ContainsKey(iteratorVariable62))
                                {
                                    WWW iteratorVariable64 = new WWW(iteratorVariable62);
                                    yield return iteratorVariable64;
                                    Texture2D iteratorVariable65 = RCextensions.loadimage(iteratorVariable64, mipmap, 0xf4240);
                                    iteratorVariable64.Dispose();
                                    if (!linkHash[2].ContainsKey(iteratorVariable62))
                                    {
                                        iteratorVariable1 = true;
                                        iteratorVariable63.material.mainTexture = iteratorVariable65;
                                        linkHash[2].Add(iteratorVariable62, iteratorVariable63.material);
                                        iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                    }
                                    else
                                    {
                                        iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                    }
                                }
                                else
                                {
                                    iteratorVariable63.material = (Material)linkHash[2][iteratorVariable62];
                                }
                            }
                        }
                    }
                }
            }
        }
        Minimap.TryRecaptureInstance();
        if (iteratorVariable1)
        {
            this.unloadAssets();
        }
    }

    [RPC]
    private void loadskinRPC(string n, string url, string url2, string[] skybox, PhotonMessageInfo info)
    {
        if ((((int)settings[2]) == 1) && info.sender.isMasterClient)
        {
            base.StartCoroutine(this.loadskinE(n, url, url2, skybox));
        }
    }

    //private IEnumerator loginFeng()
    //{
    //    WWW iteratorVariable1;
    //    WWWForm form = new WWWForm();
    //    form.AddField("userid", usernameField);
    //    form.AddField("password", passwordField);
    //    if (Application.isWebPlayer)
    //    {
    //        iteratorVariable1 = new WWW("http://aotskins.com/version/getinfo.php", form);
    //    }
    //    else
    //    {
    //        iteratorVariable1 = new WWW("http://fenglee.com/game/aog/require_user_info.php", form);
    //    }
    //    yield return iteratorVariable1;
    //    if (!((iteratorVariable1.error != null) || iteratorVariable1.text.Contains("Error,please sign in again.")))
    //    {
    //        char[] separator = new char[] { '|' };
    //        string[] strArray = iteratorVariable1.text.Split(separator);
    //        LoginFengKAI.player.name = usernameField;
    //        LoginFengKAI.player.guildname = strArray[0];
    //        loginstate = 3;
    //    }
    //    else
    //    {
    //        loginstate = 2;
    //    }
    //}

    public string mastertexturetype(int lol)
    {
        if (lol == 0)
        {
            return "High";
        }
        if (lol == 1)
        {
            return "Med";
        }
        return "Low";
    }

    public void multiplayerRacingFinsih()
    {
        float time = this.roundTime - 20f;
        if (PhotonNetwork.isMasterClient)
        {
            this.getRacingResult(LoginFengKAI.player.name, time);
        }
        else
        {
            object[] parameters = new object[] { LoginFengKAI.player.name, time };
            PView.RPC("getRacingResult", PhotonTargets.MasterClient, parameters);
        }
        this.gameWin2();
    }

    [RPC]
    private void netGameLose(int score, PhotonMessageInfo info)
    {
        this.isLosing = true;
        this.titanScore = score;
        this.gameEndCD = this.gameEndTotalCDtime;
        if (((int)settings[0xf4]) == 1)
        {
            InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game lose).");
        }
        if (((info.sender != PhotonNetwork.masterClient) && !info.sender.isLocal) && PhotonNetwork.isMasterClient)
        {
            InRoomChat.ChatInstanse.addLINE("<color=#FFC000>Round end sent from Player " + info.sender.ID.ToString() + "</color>");
        }
    }

    [RPC]
    private void netGameWin(int score, PhotonMessageInfo info)
    {
        this.humanScore = score;
        this.isWinning = true;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            this.teamWinner = score;
            this.teamScores[this.teamWinner - 1]++;
            this.gameEndCD = this.gameEndTotalCDtime;
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.RACING)
        {
            if (GameSettings.racingStatic == 1)
            {
                this.gameEndCD = 1000f;
            }
            else
            {
                this.gameEndCD = 20f;
            }
        }
        else
        {
            this.gameEndCD = this.gameEndTotalCDtime;
        }
        if (((int)settings[0xf4]) == 1)
        {
            InRoomChat.ChatInstanse.addLINE("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> Round ended (game win).");
        }
        if (!((info.sender == PhotonNetwork.masterClient) || info.sender.isLocal))
        {
            InRoomChat.ChatInstanse.addLINE("<color=#FFC000>Round end sent from Player " + info.sender.ID.ToString() + "</color>");
        }
    }

    [RPC]
    private void netRefreshRacingResult(string tmp)
    {
        this.localRacingResult = tmp;
    }

    [RPC]
    public void netShowDamage(int speed)
    {
        StylishComponent.stylebar.Style(speed);
        if (LabelScore != null)
        {
            LabelScoreUI.text = speed.ToString();
            LabelScoreT.localScale = Vector3.zero;
            speed = (int)(speed * 0.1f);
            speed = Mathf.Max(40, speed);
            speed = Mathf.Min(150, speed);
            iTween.Stop(LabelScore);
            object[] args = new object[] { "x", speed, "y", speed, "z", speed, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(LabelScore, iTween.Hash(args));
            object[] objArray2 = new object[] { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2f };
            iTween.ScaleTo(LabelScore, iTween.Hash(objArray2));
        }

    }

    public void NOTSpawnNonAITitan(string id)
    {
        this.myLastHero = id.ToUpper();
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add("dead", true);
        ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter("the game has started for 60 seconds.\n please wait for next round.\n Click Right Mouse Key to Enter or Exit the Spectator Mode.");
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
    }

    public void NOTSpawnNonAITitanRC(string id)
    {
        this.myLastHero = id.ToUpper();
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add("dead", true);
        ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter("Syncing spawn locations...");
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
    }

    public void NOTSpawnPlayer(string id)
    {
        this.myLastHero = id.ToUpper();
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add("dead", true);
        ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 1);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = false;
        this.ShowHUDInfoCenter("the game has started for 60 seconds.\n please wait for next round.\n Click Right Mouse Key to Enter or Exit the Spectator Mode.");
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
    }

    public void NOTSpawnPlayerRC(string id)
    {
        this.myLastHero = id.ToUpper();
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add("dead", true);
        ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 1);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = false;
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(null, true, false);
        IN_GAME_MAIN_CAMERA.mainCamera.setSpectorMode(true);
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
    }

    private static bool ObjectListCheck(string key)
    {
        key = key.ToLower();
        if (key.EndsWith("(clone)"))
        {
            key = key.Remove(key.Length - 7);
        }
        string key2;
        switch (key2 = key)
        {
            case "hook":
            case "fx/justSmoke":
            case "fx/boost_smoke":
            case "aottg_hero 1":
            case "hitmeat":
            case "hitmeat2":
            case "redcross":
            case "redcross1":
            case "shotgun":
            case "shotgun 1":
            case "fx/fxtitandie":
            case "fx/fxtitandie1":
            case "fx/fxtitanspawn":
            case "fx/bite":
            case "fxtitandie":
            case "fxtitandie1":
            case "fxtitanspawn":
            case "bite":
            case "justSmoke":
            case "boost_smoke":
                return false;
        }
        return true;
    }

    public void OnConnectedToMaster()
    {
        UnityEngine.MonoBehaviour.print("OnConnectedToMaster");
    }

    public void OnConnectedToPhoton()
    {
        UnityEngine.MonoBehaviour.print("OnConnectedToPhoton");
    }

    public void OnConnectionFail(DisconnectCause cause)
    {
        StatsTab.AddLine(cause.ToString(), StatsTab.DebugType.WARNING);
        if (cause == DisconnectCause.DisconnectByServerLogic || cause == DisconnectCause.DisconnectByServer || cause == DisconnectCause.DisconnectByClientTimeout)
        {
            //shallRejoin[0] = true;
            //timeofAutorejoin.Start();
            //RoomInfo roomInfo = PhotonNetwork.GetRoomList()[UnityEngine.Random.Range(0, PhotonNetwork.GetRoomList().Length)];
            //string[] array = roomInfo.name.Split(new char[] { '`' });
            //PhotonNetwork.RejoinRoom(roomInfo.name, array[6]);

        }
        if (cause == DisconnectCause.DisconnectByClientTimeout)
        {
            //if (AudioManager.List_string_of_loaded_sounds.Contains("AINoConnection") && !AudioManager.AudioSource_TimeOut.isPlaying && RCSettings.AITimeOut > 0) AudioManager.AudioSource_TimeOut.Play();
            //FleurConsole.Log.DebugLine("<color=#" + FengGameManagerMKII.finalhex() + ">Net vanish</color>", FleurConsole.DebugType.LOG);
        }
        //FleurConsole.Log.DebugLine("<color=#" + FengGameManagerMKII.finalhex() + ">OnConnectionFail :</color>" + cause.ToString(), FleurConsole.DebugType.LOG);
        UnityEngine.MonoBehaviour.print("OnConnectionFail : " + cause.ToString());
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        gameStart = false;

        if (cause == DisconnectCause.DisconnectByClientTimeout)
        {
            NGUITools.SetActive(this.uirefer.panels[0], false);
            NGUITools.SetActive(this.uirefer.panels[1], false);
            NGUITools.SetActive(this.uirefer.panels[2], false);
            NGUITools.SetActive(this.uirefer.panels[3], false);
            NGUITools.SetActive(this.uirefer.panels[4], true);
            CacheGameObject.Find("LabelDisconnectInfo").GetComponent<UILabel>().text = "OnConnectionFail : " + cause.ToString();
        }
    }

    public void OnCreatedRoom()
    {
        this.kicklist = new ArrayList();
        this.racingResult = new ArrayList();
        this.teamScores = new int[2];
        UnityEngine.MonoBehaviour.print("OnCreatedRoom");
    }

    public void OnCustomAuthenticationFailed()
    {
        UnityEngine.MonoBehaviour.print("OnCustomAuthenticationFailed");
    }

    public void OnDisconnectedFromPhoton()
    {
        UnityEngine.MonoBehaviour.print("OnDisconnectedFromPhoton");
        Screen.lockCursor = false;
        Screen.showCursor = true;
    }

    [RPC]
    public void oneTitanDown(string name1, bool onPlayerLeave)
    {
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || PhotonNetwork.isMasterClient)
        {
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                if (name1 != string.Empty)
                {
                    if (name1 == "Titan")
                    {
                        this.PVPhumanScore++;
                    }
                    else if (name1 == "Aberrant")
                    {
                        this.PVPhumanScore += 2;
                    }
                    else if (name1 == "Jumper")
                    {
                        this.PVPhumanScore += 3;
                    }
                    else if (name1 == "Crawler")
                    {
                        this.PVPhumanScore += 4;
                    }
                    else if (name1 == "Female Titan")
                    {
                        this.PVPhumanScore += 10;
                    }
                    else
                    {
                        this.PVPhumanScore += 3;
                    }
                }
                this.checkPVPpts();
                object[] parameters = new object[] { this.PVPhumanScore, this.PVPtitanScore };
                PView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
            }
            else if (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.CAGE_FIGHT)
            {
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN)
                {
                    if (this.checkIsTitanAllDie())
                    {
                        this.gameWin2();
                        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = true;
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
                {
                    if (this.checkIsTitanAllDie())
                    {
                        this.wave++;
                        if (((LevelInfo.getInfo(level).respawnMode == RespawnMode.NEWROUND) || (level.StartsWith("Custom") && (GameSettings.gameType == 1))) && (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER))
                        {
                            foreach (PhotonPlayer player in PhotonNetwork.playerList)
                            {
                                if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) != 2)
                                {
                                    PView.RPC("respawnHeroInNewRound", player, new object[0]);
                                }
                            }
                        }
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                        {
                            this.sendChatContentInfo("<color=#A8FF24>Wave : " + this.wave + "</color>");
                        }
                        if (this.wave > this.highestwave)
                        {
                            this.highestwave = this.wave;
                        }
                        if (PhotonNetwork.isMasterClient)
                        {
                            this.RequireStatus();
                        }
                        if (((GameSettings.maxWave == 0) && (this.wave > 20)) || ((GameSettings.maxWave > 0) && (this.wave > GameSettings.maxWave)))
                        {
                            this.gameWin2();
                        }
                        else
                        {
                            int abnormal = 90;
                            if (this.difficulty == 1)
                            {
                                abnormal = 70;
                            }
                            if (!LevelInfo.getInfo(level).punk)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, this.wave + 2, false);
                            }
                            else if (this.wave == 5)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 1, true);
                            }
                            else if (this.wave == 10)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 2, true);
                            }
                            else if (this.wave == 15)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 3, true);
                            }
                            else if (this.wave == 20)
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, 4, true);
                            }
                            else
                            {
                                this.spawnTitanCustom("titanRespawn", abnormal, this.wave + 2, false);
                            }
                        }
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
                {
                    if (!onPlayerLeave)
                    {
                        this.humanScore++;
                        int num2 = 90;
                        if (this.difficulty == 1)
                        {
                            num2 = 70;
                        }
                        this.spawnTitanCustom("titanRespawn", num2, 1, false);
                    }
                }
                else if (LevelInfo.getInfo(level).enemyNumber == -1)
                {
                }
            }
        }
    }

    public void OnFailedToConnectToPhoton()
    {
        UnityEngine.MonoBehaviour.print("OnFailedToConnectToPhoton");
    }

    public void UIName()
    {
        if (string.IsNullOrEmpty((string)FengGameManagerMKII.settings[265])) FengGameManagerMKII.settings[265] = "Linear";
        //if (string.IsNullOrEmpty((string)FengGameManagerMKII.settings[266])) FengGameManagerMKII.settings[266] = "Linear spd=10";
        GUI.backgroundColor = Color.black;
        List<string> list = new List<string>();
        if ((string)FengGameManagerMKII.settings[265] == "Faded")
        {
            float num = 270f;
            float num2 = 500f;
            float num3 = 400f;
            float num4 = 100f;
            GUI.Box(new Rect(num, num4, num2, num3), string.Empty);
            GUI.Box(new Rect(num - 5f, num4 - 5f, num2 + 10f, num3 + 10f), string.Empty);
            GUI.Box(new Rect(num - 5f, num4 - 5f, num2 + 10f, num3 + 10f), string.Empty);
            GUI.Box(new Rect(num - 5f, num4 - 5f, num2 + 10f, num3 + 10f), string.Empty);
            string text = LoginFengKAI.player.name.Replace("[-]", string.Empty);
            int i;
            if (regexHex.IsMatch(text))
            {
                MatchCollection matchCollection = regexHex.Matches(text);
                for (i = 0; i < matchCollection.Count; i++)
                {
                    int index = matchCollection[i].Index;
                    if (text.HasHexAt(index))
                    {
                        if (i >= matchCollection.Count - 1)
                        {
                            list.Add(text.Substring(index));
                        }
                        else
                        {
                            list.Add(text.Substring(index, matchCollection[i + 1].Index - index));
                        }
                    }
                }
            }
            GUI.backgroundColor = Color.green;
            if (list.Count < 10)
            {
                i = GUI.SelectionGrid(new Rect(num + 10f, num4 + 5f, num2 - 20f, num3 / 2f - 10f), FengGameManagerMKII.animHexSelected, list.ToArray(), 3);
            }
            else
            {
                i = GUI.SelectionGrid(new Rect(num + 10f, num4 + 5f, num2 - 20f, num3 - 10f), FengGameManagerMKII.animHexSelected, list.ToArray(), 3);
            }
            if (i != FengGameManagerMKII.animHexSelected)
            {
                FengGameManagerMKII.animHexSelected = i;
            }
        }
        GUI.backgroundColor = Color.green;
        scroll = GUI.BeginScrollView(new Rect(5f, 80f, 257f, 480f), scroll, new Rect(5f, 80f, 200f, 640f), false, true);
        if (GUI.Button(new Rect(10f, 80f, 225f, 20f), "Animation : " + FengGameManagerMKII.settings[265].ToString()))
        {
            FengGameManagerMKII.settings[271] = !(bool)FengGameManagerMKII.settings[271];
        }
        if ((bool)FengGameManagerMKII.settings[271])
        {
            string[] array = new string[]
            {
                        "Faded",
                        "Linear",
                        "Rebound"
            };
            int j;
            for (j = 0; j < array.Length; j++)
            {
                if (string.IsNullOrEmpty((string)FengGameManagerMKII.settings[265]))
                {
                    FengGameManagerMKII.settings[265] = array[0];
                    j = 0;
                    break;
                }
                if ((string)FengGameManagerMKII.settings[265] == array[j])
                {
                    break;
                }
            }
            int num5 = GUI.SelectionGrid(new Rect(10f, 105f, 230f, 50f), j, array, 3);
            GUI.backgroundColor = new Color(0f, 0f, 0f, 0f);
            if (j != num5)
            {
                FengGameManagerMKII.settings[265] = array[num5];
                FengGameManagerMKII.settings[271] = false;
            }
        }
        else
        {
            GUI.Label(new Rect(10f, 105f, 230f, 50f), "You can put the script in name.cfg with the type and anim after it ex: " + (string)FengGameManagerMKII.settings[265] + "Anim:(script)", "Label");
            if (GUI.Button(new Rect(10f, 160f, 230f, 20f), "Copy Script"))
            {
                TextEditor textEditor = new TextEditor
                {
                    content = new GUIContent((string)FengGameManagerMKII.settings[266])
                };
                textEditor.SelectAll();
                textEditor.Copy();
            }
            string a;
            if (GUI.Button(new Rect(10f, 185f, 230f, 20f), "Load Current Animation") && (a = (string)FengGameManagerMKII.settings[265]) != null)
            {
                if (!(a == "Faded"))
                {
                    if (!(a == "Linear"))
                    {
                        if (a == "Rebound")
                        {
                            //FengGameManagerMKII.LoadAnimationToEdit((string)FengGameManagerMKII.settings[269]);
                        }
                    }
                    else
                    {
                        //FengGameManagerMKII.LoadAnimationToEdit((string)FengGameManagerMKII.settings[268]);
                    }
                }
                else
                {
                    //FengGameManagerMKII.LoadAnimationToEdit((string)FengGameManagerMKII.settings[267]);
                }
            }
            string a2;
            if ((a2 = (string)FengGameManagerMKII.settings[265]) != null)
            {
                if (!(a2 == "Faded"))
                {
                    if (!(a2 == "Linear"))
                    {
                        if (a2 == "Rebound")
                        {
                            bool flag = false;
                            bool flag2 = false;
                            bool flag3 = false;
                            bool flag4 = false;
                            bool flag5 = false;
                            bool flag6 = false;
                            bool flag7 = false;
                            bool flag8 = false;
                            float num6 = 0.5f;
                            GUI.Label(new Rect(10f, 210f, 145f, 20f), "Speed Forwards:", "Label");
                            string text2 = GUI.TextField(new Rect(125f, 210f, 45f, 20f), FengGameManagerMKII.reboundSpeedTest["fwd"]).Trim();
                            GUI.Label(new Rect(175f, 210f, 145f, 20f), "seconds", "Label");
                            if (float.TryParse(text2, out num6))
                            {
                                if (num6 < 0.015f)
                                {
                                    flag2 = true;
                                }
                                if (num6 > 15f)
                                {
                                    flag6 = true;
                                }
                                if (num6 < 0.025f)
                                {
                                    flag = true;
                                }
                                if (num6 > 10f)
                                {
                                    flag5 = true;
                                }
                                if (num6 < 0f)
                                {
                                    FengGameManagerMKII.reboundSpeedTest["fwd"] = "0.09";
                                }
                                else
                                {
                                    FengGameManagerMKII.reboundSpeedTest["fwd"] = text2;
                                }
                            }
                            GUI.Label(new Rect(10f, 235f, 145f, 20f), "Speed Backwards:", "Label");
                            text2 = GUI.TextField(new Rect(125f, 235f, 45f, 20f), FengGameManagerMKII.reboundSpeedTest["back"]).Trim();
                            GUI.Label(new Rect(175f, 235f, 145f, 20f), "seconds", "Label");
                            if (float.TryParse(text2, out num6))
                            {
                                if (num6 < 0.025f)
                                {
                                    flag3 = true;
                                }
                                if (num6 > 10f)
                                {
                                    flag7 = true;
                                }
                                if (num6 < 0.015f)
                                {
                                    flag4 = true;
                                }
                                if (num6 > 15f)
                                {
                                    flag8 = true;
                                }
                                if (num6 < 0f)
                                {
                                    FengGameManagerMKII.reboundSpeedTest["back"] = "0.09";
                                }
                                else
                                {
                                    FengGameManagerMKII.reboundSpeedTest["back"] = text2;
                                }
                            }
                            flag = (flag && flag3);
                            flag5 = (flag5 && flag7);
                            if (flag)
                            {
                                timeTillReplaced += Time.smoothDeltaTime;
                                if (timeTillReplaced > 3f)
                                {
                                    FengGameManagerMKII.reboundSpeedTest["fwd"] = "0.025";
                                    FengGameManagerMKII.reboundSpeedTest["back"] = "0.025";
                                    timeTillReplaced = 0f;
                                }
                                FengGameManagerMKII.settings[266] = "Rebound [fwd]spd=0.025[back]spd=0.025";
                            }
                            else if (flag5)
                            {
                                timeTillReplaced += Time.smoothDeltaTime;
                                if (timeTillReplaced > 3f)
                                {
                                    FengGameManagerMKII.reboundSpeedTest["fwd"] = "10";
                                    FengGameManagerMKII.reboundSpeedTest["back"] = "10";
                                    timeTillReplaced = 0f;
                                }
                                FengGameManagerMKII.settings[266] = "Rebound [fwd]spd=10[back]spd=10";
                            }
                            else
                            {
                                if (flag2)
                                {
                                    timeTillReplaced += Time.smoothDeltaTime;
                                    if (timeTillReplaced > 3f)
                                    {
                                        FengGameManagerMKII.reboundSpeedTest["fwd"] = "0.015";
                                        timeTillReplaced = 0f;
                                    }
                                }
                                else if (flag4)
                                {
                                    timeTillReplaced += Time.smoothDeltaTime;
                                    if (timeTillReplaced > 3f)
                                    {
                                        FengGameManagerMKII.reboundSpeedTest["back"] = "0.015";
                                        timeTillReplaced = 0f;
                                    }
                                }
                                else if (flag6)
                                {
                                    timeTillReplaced += Time.smoothDeltaTime;
                                    if (timeTillReplaced > 3f)
                                    {
                                        FengGameManagerMKII.reboundSpeedTest["fwd"] = "15";
                                        timeTillReplaced = 0f;
                                    }
                                }
                                else if (flag8)
                                {
                                    timeTillReplaced += Time.smoothDeltaTime;
                                    if (timeTillReplaced > 3f)
                                    {
                                        FengGameManagerMKII.reboundSpeedTest["back"] = "15";
                                        timeTillReplaced = 0f;
                                    }
                                }
                                else
                                {
                                    timeTillReplaced = 0f;
                                }
                                FengGameManagerMKII.settings[266] = string.Format("Rebound [fwd]spd={0}[back]spd={1}", flag2 ? "0.015" : (flag6 ? "15" : FengGameManagerMKII.reboundSpeedTest["fwd"]), flag4 ? "0.015" : (flag8 ? "15" : FengGameManagerMKII.reboundSpeedTest["back"]));
                            }
                        }
                    }
                    else
                    {
                        float num7 = 0.5f;
                        GUI.Label(new Rect(10f, 210f, 145f, 20f), "Speed Forwards:", "Label");
                        string text3 = GUI.TextField(new Rect(115f, 210f, 45f, 20f), FengGameManagerMKII.linearSpeedTest).Trim();
                        GUI.Label(new Rect(165f, 210f, 145f, 20f), "seconds", "Label");
                        if (float.TryParse(text3, out num7))
                        {
                            if (num7 > 10f)
                            {
                                FengGameManagerMKII.linearSpeedTest = "10";
                            }
                            else if (num7 < 0f)
                            {
                                FengGameManagerMKII.linearSpeedTest = "0.09";
                            }
                            else
                            {
                                FengGameManagerMKII.linearSpeedTest = text3;
                            }
                        }
                        if (num7 > 10f)
                        {
                            timeTillReplaced += Time.smoothDeltaTime;
                            if (timeTillReplaced > 2.5f)
                            {
                                FengGameManagerMKII.linearSpeedTest = "10";
                                timeTillReplaced = 0f;
                            }
                            FengGameManagerMKII.settings[266] = "Linear spd=10";
                        }
                        else if (num7 < 0.025f)
                        {
                            timeTillReplaced += Time.smoothDeltaTime;
                            if (timeTillReplaced > 2.5f)
                            {
                                FengGameManagerMKII.linearSpeedTest = "0.025";
                                timeTillReplaced = 0f;
                            }
                            FengGameManagerMKII.settings[266] = "Linear spd=0.025";
                        }
                        else
                        {
                            timeTillReplaced = 0f;
                            FengGameManagerMKII.settings[266] = "Linear spd=" + FengGameManagerMKII.linearSpeedTest;
                        }
                    }
                }
                else
                {
                    float num8 = 0.09f;
                    if (FengGameManagerMKII.fadedSpeedTest.ContainsKey(FengGameManagerMKII.animHexSelected))
                    {
                        FengGameManagerMKII.animSpeed = FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected];
                    }
                    else
                    {
                        FengGameManagerMKII.animSpeed = "0.09";
                        FengGameManagerMKII.fadedSpeedTest.Add(FengGameManagerMKII.animHexSelected, "0.09");
                    }
                    GUI.Label(new Rect(10f, 210f, 145f, 20f), "Speed To Next:", "Label");
                    string text4 = GUI.TextField(new Rect(110f, 210f, 45f, 20f), FengGameManagerMKII.animSpeed).Trim();
                    GUI.Label(new Rect(160f, 210f, 145f, 20f), "seconds", "Label");
                    GUI.Label(new Rect(10f, 235f, 203f, 20f), "     <i>The time for the selected hex to change into the next hex.</i>", "Label");
                    if (float.TryParse(text4, out num8))
                    {
                        if (num8 > 10f)
                        {
                            FengGameManagerMKII.animSpeed = "10";
                        }
                        else if (num8 < 0.05f)
                        {
                            FengGameManagerMKII.animSpeed = "0.05";
                        }
                        else
                        {
                            FengGameManagerMKII.animSpeed = text4;
                        }
                        if (FengGameManagerMKII.fadedSpeedTest.ContainsKey(FengGameManagerMKII.animHexSelected))
                        {
                            FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected] = text4;
                        }
                        else
                        {
                            FengGameManagerMKII.fadedSpeedTest.Add(FengGameManagerMKII.animHexSelected, text4);
                        }
                    }
                    if (num8 > 10f)
                    {
                        timeTillReplaced += Time.smoothDeltaTime;
                        if (timeTillReplaced > 2.5f)
                        {
                            FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected] = (FengGameManagerMKII.animSpeed = "10");
                            timeTillReplaced = 0f;
                        }
                    }
                    else if (num8 < 0.05f)
                    {
                        timeTillReplaced += Time.smoothDeltaTime;
                        if (timeTillReplaced > 2.5f)
                        {
                            FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected] = (FengGameManagerMKII.animSpeed = "0.05");
                            timeTillReplaced = 0f;
                        }
                    }
                    else
                    {
                        timeTillReplaced = 0f;
                    }
                    if (!((string)FengGameManagerMKII.settings[266]).StartsWith("Faded"))
                    {
                        FengGameManagerMKII.settings[266] = "Faded";
                    }
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!((string)FengGameManagerMKII.settings[266]).Contains("[" + i + "]"))
                        {



                            object obj = FengGameManagerMKII.settings[266];
                            FengGameManagerMKII.settings[266] = string.Concat(new object[]
                            {
                                        obj,
                                        "[",
                                        i,
                                        "]spd=0.09"
                            });
                        }
                    }
                    string[] array2 = ((string)FengGameManagerMKII.settings[266]).Split(new char[]
                    {
                                '['
                    });
                    for (int i = 0; i < array2.Length; i++)
                    {
                        if (array2[i].Trim().StartsWith(FengGameManagerMKII.animHexSelected.ToString()))
                        {
                            float num9;
                            if ((num9 = float.Parse(FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected])) > 10f)
                            {
                                array2[i] = string.Format("{0}]spd={1}", new object[]
                                {
                                            FengGameManagerMKII.animHexSelected,
                                            "10"
                                });
                            }
                            else if ((double)num9 < 0.05)
                            {
                                array2[i] = string.Format("{0}]spd={1}", new object[]
                                {
                                            FengGameManagerMKII.animHexSelected,
                                            "0.05"
                                });
                            }
                            else
                            {
                                array2[i] = string.Format("{0}]spd={1}", new object[]
                                {
                                            FengGameManagerMKII.animHexSelected,
                                            FengGameManagerMKII.fadedSpeedTest[FengGameManagerMKII.animHexSelected]
                                });
                            }
                        }
                    }
                    FengGameManagerMKII.settings[266] = string.Join("[", array2);
                }
            }
        }
        if (GUI.Button(new Rect(10f, 250f, 230f, 20f), "Disable Name Animation"))
        {
            FengGameManagerMKII.settings[265] = "None";
        }
        GUI.EndScrollView();
    }

    public void OnGUI()
    {
        float num7;
        float num8;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.STOP) && (Application.loadedLevelName != "characterCreation"))
        {
            if (isAssetLoaded)
            {
                string text = BRM.CacheGameObject.Find("VERSION").GetComponent<UILabel>().text;
                if (text != null)
                {
                    if (text.StartsWith("Verifying"))
                    {
                        GUI.backgroundColor = Color.black;
                        float left = (Screen.width / 2) - 115f;
                        float top = (Screen.height / 2) - 45f;
                        GUI.Box(new Rect(left, top, 230f, 90f), string.Empty);
                        GUI.DrawTexture(new Rect(left + 2f, top + 2f, 226f, 86f), this.textureBackgroundBlack);
                        GUI.Label(new Rect(left + 13f, top + 20f, 172f, 70f), "Verifying client...please wait before joining the server.");
                    }
                    else if (text.StartsWith("Verification"))
                    {
                        GUI.backgroundColor = Color.black;
                        float num3 = (Screen.width / 2) - 115f;
                        float num4 = (Screen.height / 2) - 45f;
                        GUI.Box(new Rect(num3, num4, 230f, 90f), string.Empty);
                        GUI.DrawTexture(new Rect(num3 + 2f, num4 + 2f, 226f, 86f), this.textureBackgroundBlack);
                        GUI.Label(new Rect(num3 + 13f, num4 + 20f, 172f, 70f), "Verification failed. Please clear your cache or try a different browser.");
                    }
                    else if (text.StartsWith("Mod"))
                    {
                        GUI.backgroundColor = Color.black;
                        float num5 = (Screen.width / 2) - 115f;
                        float num6 = (Screen.height / 2) - 45f;
                        GUI.Box(new Rect(num5, num6, 230f, 90f), string.Empty);
                        GUI.DrawTexture(new Rect(num5 + 2f, num6 + 2f, 226f, 86f), this.textureBackgroundBlack);
                        GUI.Label(new Rect(num5 + 13f, num6 + 20f, 172f, 70f), "Mod is outdated. Please clear your cache or try a different browser.");
                    }


                    num7 = (((float)Screen.width) / 2f) - 85f;
                    num8 = ((float)Screen.height) / 2f;
                    GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
                    GUI.DrawTexture(new Rect(12f, 32f, 216f, 146f), this.textureBackgroundBlack);
                    //GUI.DrawTexture(new Rect(num7 + 2f, 7f, 146f, 50f), this.textureBackgroundBlack);
                    GUI.Box(new Rect(num7, 5f, 150f, 45f), string.Empty);
                    if (GUI.Button(new Rect(num7 + 11f, 15f, 128f, 25f), "Level Editor"))
                    {
                        settings[0x40] = 0x65;
                        Application.LoadLevel(2);
                    }
                    //else if (GUI.Button(new Rect(num7 + 11f, 45f, 128f, 25f), "Custom Characters"))
                    //{
                    //    Application.LoadLevel("characterCreation");
                    //}
                    //else if (GUI.Button(new Rect(num7 + 11f, 75f, 128f, 25f), "Snapshot Reviewer"))
                    //{
                    //    Application.LoadLevel("SnapShot");
                    //}
                    //GUI.Box(new Rect(10f, 350f, 220f, 150f), string.Empty);
                    //if (GUI.Button(new Rect(17.5f, 40f, 40f, 25f), "Login", "box"))
                    //{
                    //    settings[0xbb] = 0;
                    //}
                    //else if (GUI.Button(new Rect(65f, 40f, 95f, 25f), "Custom Name", "box"))
                    //{
                    //    settings[0xbb] = 1;
                    //}
                    else if (GUI.Button(new Rect(167.5f, 40f, 55f, 25f), "Servers", "box"))
                    {
                        settings[0xbb] = 2;
                    }
                    //if (((int)settings[0xbb]) == 1)
                    //{
                    //if (loginstate == 3)
                    //{
                    //    GUI.Label(new Rect(30f, 80f, 180f, 60f), "You're already logged in!", "Label");
                    //}
                    //else
                    //{
                    //    GUI.Label(new Rect(20f, 80f, 45f, 20f), "Name:", "Label");
                    //    nameField = GUI.TextField(new Rect(65f, 80f, 145f, 20f), nameField, 990);
                    //    GUI.Label(new Rect(20f, 105f, 45f, 20f), "Guild:", "Label");
                    //    LoginFengKAI.player.guildname = GUI.TextField(new Rect(65f, 105f, 145f, 20f), LoginFengKAI.player.guildname, 990);
                    //    if (GUI.Button(new Rect(42f, 140f, 50f, 25f), "Save"))
                    //    {
                    //        PlayerPrefs.SetString("name", nameField);
                    //        PlayerPrefs.SetString("guildname", LoginFengKAI.player.guildname);
                    //    }
                    //    else if (GUI.Button(new Rect(128f, 140f, 50f, 25f), "Load"))
                    //    {
                    //        nameField = PlayerPrefs.GetString("name", string.Empty);
                    //        LoginFengKAI.player.guildname = PlayerPrefs.GetString("guildname", string.Empty);
                    //    }
                    //}


                    //}
                    //else if (((int)settings[0xbb]) == 0)
                    //{
                    //    if (loginstate == 3)
                    //    {
                    //        GUI.Label(new Rect(20f, 80f, 70f, 20f), "Username:", "Label");
                    //        GUI.Label(new Rect(90f, 80f, 90f, 20f), LoginFengKAI.player.name, "Label");
                    //        GUI.Label(new Rect(20f, 105f, 45f, 20f), "Guild:", "Label");
                    //        LoginFengKAI.player.guildname = GUI.TextField(new Rect(65f, 105f, 145f, 20f), LoginFengKAI.player.guildname, 990);
                    //        if (GUI.Button(new Rect(35f, 140f, 70f, 25f), "Set Guild"))
                    //        {
                    //            base.StartCoroutine(this.setGuildFeng());
                    //        }
                    //        else if (GUI.Button(new Rect(130f, 140f, 65f, 25f), "Logout"))
                    //        {
                    //            loginstate = 0;
                    //        }
                    //    }
                    //    else
                    //    {
                    //GUI.Label(new Rect(20f, 80f, 70f, 20f), "Username:", "Label");
                    //usernameField = GUI.TextField(new Rect(90f, 80f, 130f, 20f), usernameField, 40);
                    //GUI.Label(new Rect(20f, 105f, 70f, 20f), "Password:", "Label");
                    //passwordField = GUI.PasswordField(new Rect(90f, 105f, 130f, 20f), passwordField, '*', 40);
                    //if (GUI.Button(new Rect(30f, 140f, 50f, 25f), "Login") && (loginstate != 1))
                    //{
                    //    base.StartCoroutine(this.loginFeng());
                    //    loginstate = 1;
                    //}
                    //if (loginstate == 1)
                    //{
                    //    GUI.Label(new Rect(100f, 140f, 120f, 25f), "Logging in...", "Label");
                    //}
                    //else if (loginstate == 2)
                    //{
                    //    GUI.Label(new Rect(100f, 140f, 120f, 25f), "Login Failed.", "Label");
                    //}
                    //}
                    //}
                    else if (((int)settings[0xbb]) == 2)
                    {
                        if (UIMainReferences.version == UIMainReferences.fengVersion)
                        {
                            GUI.Label(new Rect(37f, 75f, 190f, 25f), "Connected to public server.", "Label");
                        }
                        else if (UIMainReferences.version == s[0])
                        {
                            GUI.Label(new Rect(28f, 75f, 190f, 25f), "Connected to RC private server.", "Label");
                        }
                        else if (UIMainReferences.version == "DontUseThisVersionPlease173")
                        {
                            GUI.Label(new Rect(28f, 75f, 190f, 25f), "Connecting to crypto server...", "Label");
                        }
                        else
                        {
                            GUI.Label(new Rect(37f, 75f, 190f, 25f), "Connected to custom server.", "Label");
                        }
                        GUI.Label(new Rect(20f, 100f, 90f, 25f), "Public Server:", "Label");
                        GUI.Label(new Rect(20f, 125f, 80f, 25f), "RC Private:", "Label");
                        GUI.Label(new Rect(20f, 150f, 60f, 25f), "Custom:", "Label");
                        if (GUI.Button(new Rect(160f, 100f, 60f, 20f), "Connect"))
                        {
                            UIMainReferences.version = UIMainReferences.fengVersion;
                        }
                        else if (GUI.Button(new Rect(160f, 125f, 60f, 20f), "Connect"))
                        {
                            UIMainReferences.version = s[0];
                        }
                        else if (GUI.Button(new Rect(160f, 150f, 60f, 20f), "Connect"))
                        {
                            UIMainReferences.version = privateServerField;
                        }
                        privateServerField = GUI.TextField(new Rect(78f, 153f, 70f, 18f), privateServerField, 50);
                    }

                }
            }
            else
            {
                GUI.backgroundColor = Color.black;
                float num9 = (Screen.width / 2) - 115f;
                float num10 = (Screen.height / 2) - 45f;
                GUI.Box(new Rect(num9, num10, 230f, 90f), string.Empty);
                GUI.DrawTexture(new Rect(num9 + 2f, num10 + 2f, 226f, 86f), this.textureBackgroundBlack);
                GUI.Label(new Rect(num9 + 13f, num10 + 20f, 172f, 70f), "Downloading custom assets. Clear your cache or try a different browser if this takes longer than 10 seconds.");
            }
            if (ShowMenuButtonGUI)
            {
                GUI.backgroundColor = Color.black;
                //GUI.Box(new Rect(25, 1000, 400, 200), string.Empty);
                GUI.DrawTexture(new Rect(25, 1000, 400, 200), this.textureBackgroundBlack);
                GUILayout.BeginArea(new Rect(25, Screen.height - 50, 500, 200));
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(FengGameManagerMKII.Colr("Single"), GUILayout.MaxWidth(100f)))
                {
                    NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelSingleSet, true);
                    ShowMenuButtonGUI = false;
                }
                if (GUILayout.Button(FengGameManagerMKII.Colr("Multiplayer"), GUILayout.MaxWidth(100f)))
                {

                    NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, true);

                    ShowMenuButtonGUI = false;
                }
                if (GUILayout.Button(FengGameManagerMKII.Colr("Options"), GUILayout.MaxWidth(100f)))
                {
                    NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelOption, true);
                    FengCustomInputs.Inputs.showKeyMap();
                    FengCustomInputs.Inputs.menuOn = true;
                    ShowMenuButtonGUI = false;
                }
                if (GUILayout.Button(FengGameManagerMKII.Colr("Credits"), GUILayout.MaxWidth(100f)))
                {
                    NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelCredits, true);
                    ShowMenuButtonGUI = false;
                }
                if (GUILayout.Button(FengGameManagerMKII.Colr("Quit"), GUILayout.MaxWidth(100f)))
                {
                    Application.Quit();
                }
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP)
        {
            bool flag2;
            int num13;
            int num18;
            TextEditor editor;
            int num23;
            Event current;
            bool flag4;
            string str4;
            bool flag5;
            Texture2D textured;
            bool flag6;
            int num30;
            bool flag10;
            if (((int)settings[0x40]) >= 100)
            {
                GameObject obj4;
                float num14;
                Color color;
                Mesh mesh;
                Color[] colorArray;
                float num20;
                float num21;
                float num27;
                int num28;
                int num29;
                float num31;
                float num11 = Screen.width - 300f;
                GUI.backgroundColor = Color.black;
                GUI.DrawTexture(new Rect(7f, 7f, 291f, 586f), this.textureBackgroundBlack);
                GUI.DrawTexture(new Rect(num11 + 2f, 7f, 291f, 586f), this.textureBackgroundBlack);
                flag2 = false;
                bool flag3 = false;
                GUI.Box(new Rect(5f, 5f, 295f, 590f), string.Empty);
                GUI.Box(new Rect(num11, 5f, 295f, 590f), string.Empty);
                if (GUI.Button(new Rect(10f, 10f, 60f, 25f), "Script", "box"))
                {
                    settings[0x44] = 100;
                }
                if (GUI.Button(new Rect(75f, 10f, 65f, 25f), "Controls", "box"))
                {
                    settings[0x44] = 0x65;
                }
                if (GUI.Button(new Rect(210f, 10f, 80f, 25f), "Full Screen", "box"))
                {
                    Screen.fullScreen = !Screen.fullScreen;
                    if (Screen.fullScreen)
                    {
                        Screen.SetResolution(960, 600, false);
                    }
                    else
                    {
                        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                    }
                }
                if ((((int)settings[0x44]) == 100) || (((int)settings[0x44]) == 0x66))
                {
                    string str2;
                    int num19;
                    GUI.Label(new Rect(115f, 40f, 100f, 20f), "Level Script:", "Label");
                    GUI.Label(new Rect(115f, 115f, 100f, 20f), "Import Data", "Label");
                    GUI.Label(new Rect(12f, 535f, 280f, 60f), "Warning: your current level will be lost if you quit or import data. Make sure to save the level to a text document.", "Label");
                    settings[0x4d] = GUI.TextField(new Rect(10f, 140f, 285f, 350f), (string)settings[0x4d]);
                    if (GUI.Button(new Rect(35f, 500f, 60f, 30f), "Apply"))
                    {
                        foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
                        {
                            if ((((obj2.name.StartsWith("custom") || obj2.name.StartsWith("base")) || (obj2.name.StartsWith("photon") || obj2.name.StartsWith("spawnpoint"))) || obj2.name.StartsWith("misc")) || obj2.name.StartsWith("racing"))
                            {
                                UnityEngine.Object.Destroy(obj2);
                            }
                        }
                        linkHash[3].Clear();
                        settings[0xba] = 0;
                        string[] strArray = Regex.Replace((string)settings[0x4d], @"\s+", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Split(new char[] { ';' });
                        for (num13 = 0; num13 < strArray.Length; num13++)
                        {
                            string[] strArray2 = strArray[num13].Split(new char[] { ',' });
                            if ((((strArray2[0].StartsWith("custom") || strArray2[0].StartsWith("base")) || (strArray2[0].StartsWith("photon") || strArray2[0].StartsWith("spawnpoint"))) || strArray2[0].StartsWith("misc")) || strArray2[0].StartsWith("racing"))
                            {
                                float num15;
                                float num16;
                                float num17;
                                GameObject obj3 = null;
                                if (strArray2[0].StartsWith("custom"))
                                {
                                    obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[12]), Convert.ToSingle(strArray2[13]), Convert.ToSingle(strArray2[14])), new Quaternion(Convert.ToSingle(strArray2[15]), Convert.ToSingle(strArray2[0x10]), Convert.ToSingle(strArray2[0x11]), Convert.ToSingle(strArray2[0x12])));
                                }
                                else if (strArray2[0].StartsWith("photon"))
                                {
                                    if (strArray2[1].StartsWith("Cannon"))
                                    {
                                        if (strArray2.Length < 15)
                                        {
                                            obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1] + "Prop"), new Vector3(Convert.ToSingle(strArray2[2]), Convert.ToSingle(strArray2[3]), Convert.ToSingle(strArray2[4])), new Quaternion(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8])));
                                        }
                                        else
                                        {
                                            obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1] + "Prop"), new Vector3(Convert.ToSingle(strArray2[12]), Convert.ToSingle(strArray2[13]), Convert.ToSingle(strArray2[14])), new Quaternion(Convert.ToSingle(strArray2[15]), Convert.ToSingle(strArray2[0x10]), Convert.ToSingle(strArray2[0x11]), Convert.ToSingle(strArray2[0x12])));
                                        }
                                    }
                                    else
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[4]), Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6])), new Quaternion(Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10])));
                                    }
                                }
                                else if (strArray2[0].StartsWith("spawnpoint"))
                                {
                                    obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[2]), Convert.ToSingle(strArray2[3]), Convert.ToSingle(strArray2[4])), new Quaternion(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8])));
                                }
                                else if (strArray2[0].StartsWith("base"))
                                {
                                    if (strArray2.Length < 15)
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)BRM.CacheResources.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[2]), Convert.ToSingle(strArray2[3]), Convert.ToSingle(strArray2[4])), new Quaternion(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8])));
                                    }
                                    else
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)BRM.CacheResources.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[12]), Convert.ToSingle(strArray2[13]), Convert.ToSingle(strArray2[14])), new Quaternion(Convert.ToSingle(strArray2[15]), Convert.ToSingle(strArray2[0x10]), Convert.ToSingle(strArray2[0x11]), Convert.ToSingle(strArray2[0x12])));
                                    }
                                }
                                else if (strArray2[0].StartsWith("misc"))
                                {
                                    if (strArray2[1].StartsWith("barrier"))
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load("barrierEditor"), new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7])), new Quaternion(Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]), Convert.ToSingle(strArray2[11])));
                                    }
                                    else if (strArray2[1].StartsWith("region"))
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load("regionEditor"));
                                        obj3.transform.position = new Vector3(Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8]));
                                        obj4 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("UI/LabelNameOverHead"));
                                        obj4.name = "RegionLabel";
                                        obj4.transform.parent = obj3.transform;
                                        num14 = 1f;
                                        if (Convert.ToSingle(strArray2[4]) > 100f)
                                        {
                                            num14 = 0.8f;
                                        }
                                        else if (Convert.ToSingle(strArray2[4]) > 1000f)
                                        {
                                            num14 = 0.5f;
                                        }
                                        obj4.transform.localPosition = new Vector3(0f, num14, 0f);
                                        obj4.transform.localScale = new Vector3(5f / Convert.ToSingle(strArray2[3]), 5f / Convert.ToSingle(strArray2[4]), 5f / Convert.ToSingle(strArray2[5]));
                                        obj4.GetComponent<UILabel>().text = strArray2[2];
                                        obj3.AddComponent<RCRegionLabel>();
                                        obj3.GetComponent<RCRegionLabel>().myLabel = obj4;
                                    }
                                    else if (strArray2[1].StartsWith("racingStart"))
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load("racingStart"), new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7])), new Quaternion(Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]), Convert.ToSingle(strArray2[11])));
                                    }
                                    else if (strArray2[1].StartsWith("racingEnd"))
                                    {
                                        obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load("racingEnd"), new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7])), new Quaternion(Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]), Convert.ToSingle(strArray2[11])));
                                    }
                                }
                                else if (strArray2[0].StartsWith("racing"))
                                {
                                    obj3 = (GameObject)UnityEngine.Object.Instantiate((GameObject)RCassets.Load(strArray2[1]), new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7])), new Quaternion(Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]), Convert.ToSingle(strArray2[11])));
                                }
                                if ((strArray2[2] != "default") && ((strArray2[0].StartsWith("custom") || (strArray2[0].StartsWith("base") && (strArray2.Length > 15))) || (strArray2[0].StartsWith("photon") && (strArray2.Length > 15))))
                                {
                                    foreach (Renderer renderer in obj3.GetComponentsInChildren<Renderer>())
                                    {
                                        if (!(renderer.name.Contains("Particle System") && obj3.name.Contains("aot_supply")))
                                        {
                                            renderer.material = (Material)RCassets.Load(strArray2[2]);
                                            renderer.material.mainTextureScale = new Vector2(renderer.material.mainTextureScale.x * Convert.ToSingle(strArray2[10]), renderer.material.mainTextureScale.y * Convert.ToSingle(strArray2[11]));
                                        }
                                    }
                                }
                                if ((strArray2[0].StartsWith("custom") || (strArray2[0].StartsWith("base") && (strArray2.Length > 15))) || (strArray2[0].StartsWith("photon") && (strArray2.Length > 15)))
                                {
                                    num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[3]);
                                    num15 -= 0.001f;
                                    num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[4]);
                                    num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[5]);
                                    obj3.transform.localScale = new Vector3(num15, num16, num17);
                                    if (strArray2[6] != "0")
                                    {
                                        color = new Color(Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), 1f);
                                        foreach (MeshFilter filter in obj3.GetComponentsInChildren<MeshFilter>())
                                        {
                                            mesh = filter.mesh;
                                            colorArray = new Color[mesh.vertexCount];
                                            num18 = 0;
                                            while (num18 < mesh.vertexCount)
                                            {
                                                colorArray[num18] = color;
                                                num18++;
                                            }
                                            mesh.colors = colorArray;
                                        }
                                    }
                                    obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4] + "," + strArray2[5] + "," + strArray2[6] + "," + strArray2[7] + "," + strArray2[8] + "," + strArray2[9] + "," + strArray2[10] + "," + strArray2[11];
                                }
                                else if (strArray2[0].StartsWith("misc"))
                                {
                                    if (strArray2[1].StartsWith("barrier") || strArray2[1].StartsWith("racing"))
                                    {
                                        num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[2]);
                                        num15 -= 0.001f;
                                        num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[3]);
                                        num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[4]);
                                        obj3.transform.localScale = new Vector3(num15, num16, num17);
                                        obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4];
                                    }
                                    else if (strArray2[1].StartsWith("region"))
                                    {
                                        num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[3]);
                                        num15 -= 0.001f;
                                        num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[4]);
                                        num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[5]);
                                        obj3.transform.localScale = new Vector3(num15, num16, num17);
                                        obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4] + "," + strArray2[5];
                                    }
                                }
                                else if (strArray2[0].StartsWith("racing"))
                                {
                                    num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[2]);
                                    num15 -= 0.001f;
                                    num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[3]);
                                    num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[4]);
                                    obj3.transform.localScale = new Vector3(num15, num16, num17);
                                    obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4];
                                }
                                else if (!(!strArray2[0].StartsWith("photon") || strArray2[1].StartsWith("Cannon")))
                                {
                                    obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3];
                                }
                                else
                                {
                                    obj3.name = strArray2[0] + "," + strArray2[1];
                                }
                                linkHash[3].Add(obj3.GetInstanceID(), strArray[num13]);
                            }
                            else if (strArray2[0].StartsWith("map") && strArray2[1].StartsWith("disablebounds"))
                            {
                                settings[0xba] = 1;
                                if (!linkHash[3].ContainsKey("mapbounds"))
                                {
                                    linkHash[3].Add("mapbounds", "map,disablebounds");
                                }
                            }
                        }
                        this.unloadAssets();
                        settings[0x4d] = string.Empty;
                    }
                    else if (GUI.Button(new Rect(205f, 500f, 60f, 30f), "Exit"))
                    {
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                        FengCustomInputs.Inputs.menuOn = false;
                        UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("MultiplayerManager"));
                        Application.LoadLevel("menu");
                    }
                    else if (GUI.Button(new Rect(15f, 70f, 115f, 30f), "Copy to Clipboard"))
                    {
                        str2 = string.Empty;
                        num19 = 0;
                        foreach (string str3 in linkHash[3].Values)
                        {
                            num19++;
                            str2 = str2 + str3 + ";\n";
                        }
                        editor = new TextEditor
                        {
                            content = new GUIContent(str2)
                        };
                        editor.SelectAll();
                        editor.Copy();
                    }
                    else if (GUI.Button(new Rect(175f, 70f, 115f, 30f), "View Script"))
                    {
                        settings[0x44] = 0x66;
                    }
                    if (((int)settings[0x44]) == 0x66)
                    {
                        str2 = string.Empty;
                        num19 = 0;
                        foreach (string str3 in linkHash[3].Values)
                        {
                            num19++;
                            str2 = str2 + str3 + ";\n";
                        }
                        num20 = (Screen.width / 2) - 110.5f;
                        num21 = (Screen.height / 2) - 250f;
                        GUI.DrawTexture(new Rect(num20 + 2f, num21 + 2f, 217f, 496f), this.textureBackgroundBlue);
                        GUI.Box(new Rect(num20, num21, 221f, 500f), string.Empty);
                        if (GUI.Button(new Rect(num20 + 10f, num21 + 460f, 60f, 30f), "Copy"))
                        {
                            editor = new TextEditor
                            {
                                content = new GUIContent(str2)
                            };
                            editor.SelectAll();
                            editor.Copy();
                        }
                        else if (GUI.Button(new Rect(num20 + 151f, num21 + 460f, 60f, 30f), "Done"))
                        {
                            settings[0x44] = 100;
                        }
                        GUI.TextArea(new Rect(num20 + 5f, num21 + 5f, 211f, 415f), str2);
                        GUI.Label(new Rect(num20 + 10f, num21 + 430f, 150f, 20f), "Object Count: " + Convert.ToString(num19), "Label");
                    }
                }
                else if (((int)settings[0x44]) == 0x65)
                {
                    GUI.Label(new Rect(92f, 50f, 180f, 20f), "Level Editor Rebinds:", "Label");
                    GUI.Label(new Rect(12f, 80f, 145f, 20f), "Forward:", "Label");
                    GUI.Label(new Rect(12f, 105f, 145f, 20f), "Back:", "Label");
                    GUI.Label(new Rect(12f, 130f, 145f, 20f), "Left:", "Label");
                    GUI.Label(new Rect(12f, 155f, 145f, 20f), "Right:", "Label");
                    GUI.Label(new Rect(12f, 180f, 145f, 20f), "Up:", "Label");
                    GUI.Label(new Rect(12f, 205f, 145f, 20f), "Down:", "Label");
                    GUI.Label(new Rect(12f, 230f, 145f, 20f), "Toggle Cursor:", "Label");
                    GUI.Label(new Rect(12f, 255f, 145f, 20f), "Place Object:", "Label");
                    GUI.Label(new Rect(12f, 280f, 145f, 20f), "Delete Object:", "Label");
                    GUI.Label(new Rect(12f, 305f, 145f, 20f), "Movement-Slow:", "Label");
                    GUI.Label(new Rect(12f, 330f, 145f, 20f), "Rotate Forward:", "Label");
                    GUI.Label(new Rect(12f, 355f, 145f, 20f), "Rotate Backward:", "Label");
                    GUI.Label(new Rect(12f, 380f, 145f, 20f), "Rotate Left:", "Label");
                    GUI.Label(new Rect(12f, 405f, 145f, 20f), "Rotate Right:", "Label");
                    GUI.Label(new Rect(12f, 430f, 145f, 20f), "Rotate CCW:", "Label");
                    GUI.Label(new Rect(12f, 455f, 145f, 20f), "Rotate CW:", "Label");
                    GUI.Label(new Rect(12f, 480f, 145f, 20f), "Movement-Speedup:", "Label");
                    for (num13 = 0; num13 < 0x11; num13++)
                    {
                        float num22 = 80f + (25f * num13);
                        num23 = 0x75 + num13;
                        if (num13 == 0x10)
                        {
                            num23 = 0xa1;
                        }
                        if (GUI.Button(new Rect(135f, num22, 60f, 20f), (string)settings[num23], "box"))
                        {
                            settings[num23] = "waiting...";
                            settings[100] = num23;
                        }
                    }
                    if (((int)settings[100]) != 0)
                    {
                        current = Event.current;
                        flag4 = false;
                        str4 = "waiting...";
                        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                        {
                            flag4 = true;
                            str4 = current.keyCode.ToString();
                        }
                        else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            flag4 = true;
                            str4 = KeyCode.LeftShift.ToString();
                        }
                        else if (Input.GetKey(KeyCode.RightShift))
                        {
                            flag4 = true;
                            str4 = KeyCode.RightShift.ToString();
                        }
                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                        {
                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                            {
                                flag4 = true;
                                str4 = "Scroll Up";
                            }
                            else
                            {
                                flag4 = true;
                                str4 = "Scroll Down";
                            }
                        }
                        else
                        {
                            num13 = 0;
                            while (num13 < 7)
                            {
                                if (Input.GetKeyDown((KeyCode)(0x143 + num13)))
                                {
                                    flag4 = true;
                                    str4 = "Mouse" + Convert.ToString(num13);
                                }
                                num13++;
                            }
                        }
                        if (flag4)
                        {
                            for (num13 = 0; num13 < 0x11; num13++)
                            {
                                num23 = 0x75 + num13;
                                if (num13 == 0x10)
                                {
                                    num23 = 0xa1;
                                }
                                if (((int)settings[100]) == num23)
                                {
                                    settings[num23] = str4;
                                    settings[100] = 0;
                                    inputRC.setInputLevel(num13, str4);
                                }
                            }
                        }
                    }
                    if (GUI.Button(new Rect(100f, 515f, 110f, 30f), "Save Controls"))
                    {
                        PlayerPrefs.SetString("lforward", (string)settings[0x75]);
                        PlayerPrefs.SetString("lback", (string)settings[0x76]);
                        PlayerPrefs.SetString("lleft", (string)settings[0x77]);
                        PlayerPrefs.SetString("lright", (string)settings[120]);
                        PlayerPrefs.SetString("lup", (string)settings[0x79]);
                        PlayerPrefs.SetString("ldown", (string)settings[0x7a]);
                        PlayerPrefs.SetString("lcursor", (string)settings[0x7b]);
                        PlayerPrefs.SetString("lplace", (string)settings[0x7c]);
                        PlayerPrefs.SetString("ldel", (string)settings[0x7d]);
                        PlayerPrefs.SetString("lslow", (string)settings[0x7e]);
                        PlayerPrefs.SetString("lrforward", (string)settings[0x7f]);
                        PlayerPrefs.SetString("lrback", (string)settings[0x80]);
                        PlayerPrefs.SetString("lrleft", (string)settings[0x81]);
                        PlayerPrefs.SetString("lrright", (string)settings[130]);
                        PlayerPrefs.SetString("lrccw", (string)settings[0x83]);
                        PlayerPrefs.SetString("lrcw", (string)settings[0x84]);
                        PlayerPrefs.SetString("lfast", (string)settings[0xa1]);
                    }
                }
                if ((((int)settings[0x40]) != 0x69) && (((int)settings[0x40]) != 0x6a))
                {
                    GUI.Label(new Rect(num11 + 13f, 445f, 125f, 20f), "Scale Multipliers:", "Label");
                    GUI.Label(new Rect(num11 + 13f, 470f, 50f, 22f), "Length:", "Label");
                    settings[0x48] = GUI.TextField(new Rect(num11 + 58f, 470f, 40f, 20f), (string)settings[0x48]);
                    GUI.Label(new Rect(num11 + 13f, 495f, 50f, 20f), "Width:", "Label");
                    settings[70] = GUI.TextField(new Rect(num11 + 58f, 495f, 40f, 20f), (string)settings[70]);
                    GUI.Label(new Rect(num11 + 13f, 520f, 50f, 22f), "Height:", "Label");
                    settings[0x47] = GUI.TextField(new Rect(num11 + 58f, 520f, 40f, 20f), (string)settings[0x47]);
                    if (((int)settings[0x40]) <= 0x6a)
                    {
                        GUI.Label(new Rect(num11 + 155f, 554f, 50f, 22f), "Tiling:", "Label");
                        settings[0x4f] = GUI.TextField(new Rect(num11 + 200f, 554f, 40f, 20f), (string)settings[0x4f]);
                        settings[80] = GUI.TextField(new Rect(num11 + 245f, 554f, 40f, 20f), (string)settings[80]);
                        GUI.Label(new Rect(num11 + 219f, 570f, 10f, 22f), "x:", "Label");
                        GUI.Label(new Rect(num11 + 264f, 570f, 10f, 22f), "y:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 445f, 50f, 20f), "Color:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 470f, 10f, 20f), "R:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 495f, 10f, 20f), "G:", "Label");
                        GUI.Label(new Rect(num11 + 155f, 520f, 10f, 20f), "B:", "Label");
                        settings[0x49] = GUI.HorizontalSlider(new Rect(num11 + 170f, 475f, 100f, 20f), (float)settings[0x49], 0f, 1f);
                        settings[0x4a] = GUI.HorizontalSlider(new Rect(num11 + 170f, 500f, 100f, 20f), (float)settings[0x4a], 0f, 1f);
                        settings[0x4b] = GUI.HorizontalSlider(new Rect(num11 + 170f, 525f, 100f, 20f), (float)settings[0x4b], 0f, 1f);
                        GUI.Label(new Rect(num11 + 13f, 554f, 57f, 22f), "Material:", "Label");
                        if (GUI.Button(new Rect(num11 + 66f, 554f, 60f, 20f), (string)settings[0x45]))
                        {
                            settings[0x4e] = 1;
                        }
                        if (((int)settings[0x4e]) == 1)
                        {
                            string[] strArray4 = new string[] { "bark", "bark2", "bark3", "bark4" };
                            string[] strArray5 = new string[] { "wood1", "wood2", "wood3", "wood4" };
                            string[] strArray6 = new string[] { "grass", "grass2", "grass3", "grass4" };
                            string[] strArray7 = new string[] { "brick1", "brick2", "brick3", "brick4" };
                            string[] strArray8 = new string[] { "metal1", "metal2", "metal3", "metal4" };
                            string[] strArray9 = new string[] { "rock1", "rock2", "rock3" };
                            string[] strArray10 = new string[] { "stone1", "stone2", "stone3", "stone4", "stone5", "stone6", "stone7", "stone8", "stone9", "stone10" };
                            string[] strArray11 = new string[] { "earth1", "earth2", "ice1", "lava1", "crystal1", "crystal2", "empty" };
                            string[] strArray12 = new string[0];
                            List<string[]> list2 = new List<string[]> {
                                strArray4,
                                strArray5,
                                strArray6,
                                strArray7,
                                strArray8,
                                strArray9,
                                strArray10,
                                strArray11
                            };
                            string[] strArray13 = new string[] { "bark", "wood", "grass", "brick", "metal", "rock", "stone", "misc", "transparent" };
                            int index = 0x4e;
                            int num25 = 0x45;
                            num20 = (Screen.width / 2) - 110.5f;
                            num21 = (Screen.height / 2) - 220f;
                            int num26 = (int)settings[0xb9];
                            num27 = 10f + (104f * ((list2[num26].Length / 3) + 1));
                            num27 = Math.Max(num27, 280f);
                            GUI.DrawTexture(new Rect(num20 + 2f, num21 + 2f, 208f, 446f), this.textureBackgroundBlue);
                            GUI.Box(new Rect(num20, num21, 212f, 450f), string.Empty);
                            for (num13 = 0; num13 < list2.Count; num13++)
                            {
                                num28 = num13 / 3;
                                num29 = num13 % 3;
                                if (GUI.Button(new Rect((num20 + 5f) + (69f * num29), (num21 + 5f) + (30 * num28), 64f, 25f), strArray13[num13], "box"))
                                {
                                    settings[0xb9] = num13;
                                }
                            }
                            this.scroll2 = GUI.BeginScrollView(new Rect(num20, num21 + 110f, 225f, 290f), this.scroll2, new Rect(num20, num21 + 110f, 212f, num27), true, true);
                            if (num26 != 8)
                            {
                                for (num13 = 0; num13 < list2[num26].Length; num13++)
                                {
                                    num28 = num13 / 3;
                                    num29 = num13 % 3;
                                    GUI.DrawTexture(new Rect((num20 + 5f) + (69f * num29), (num21 + 115f) + (104f * num28), 64f, 64f), this.RCLoadTexture("p" + list2[num26][num13]));
                                    if (GUI.Button(new Rect((num20 + 5f) + (69f * num29), (num21 + 184f) + (104f * num28), 64f, 30f), list2[num26][num13]))
                                    {
                                        settings[num25] = list2[num26][num13];
                                        settings[index] = 0;
                                    }
                                }
                            }
                            GUI.EndScrollView();
                            if (GUI.Button(new Rect(num20 + 24f, num21 + 410f, 70f, 30f), "Default"))
                            {
                                settings[num25] = "default";
                                settings[index] = 0;
                            }
                            else if (GUI.Button(new Rect(num20 + 118f, num21 + 410f, 70f, 30f), "Done"))
                            {
                                settings[index] = 0;
                            }
                        }
                        flag5 = false;
                        if (((int)settings[0x4c]) == 1)
                        {
                            flag5 = true;
                            textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                            textured.SetPixel(0, 0, new Color((float)settings[0x49], (float)settings[0x4a], (float)settings[0x4b], 1f));
                            textured.Apply();
                            GUI.DrawTexture(new Rect(num11 + 235f, 445f, 30f, 20f), textured, ScaleMode.StretchToFill);
                            UnityEngine.Object.Destroy(textured);
                        }
                        flag6 = GUI.Toggle(new Rect(num11 + 193f, 445f, 40f, 20f), flag5, "On");
                        if (flag5 != flag6)
                        {
                            if (flag6)
                            {
                                settings[0x4c] = 1;
                            }
                            else
                            {
                                settings[0x4c] = 0;
                            }
                        }
                    }
                }
                if (GUI.Button(new Rect(num11 + 5f, 10f, 60f, 25f), "General", "box"))
                {
                    settings[0x40] = 0x65;
                }
                else if (GUI.Button(new Rect(num11 + 70f, 10f, 70f, 25f), "Geometry", "box"))
                {
                    settings[0x40] = 0x66;
                }
                else if (GUI.Button(new Rect(num11 + 145f, 10f, 65f, 25f), "Buildings", "box"))
                {
                    settings[0x40] = 0x67;
                }
                else if (GUI.Button(new Rect(num11 + 215f, 10f, 50f, 25f), "Nature", "box"))
                {
                    settings[0x40] = 0x68;
                }
                else if (GUI.Button(new Rect(num11 + 5f, 45f, 70f, 25f), "Spawners", "box"))
                {
                    settings[0x40] = 0x69;
                }
                else if (GUI.Button(new Rect(num11 + 80f, 45f, 70f, 25f), "Racing", "box"))
                {
                    settings[0x40] = 0x6c;
                }
                else if (GUI.Button(new Rect(num11 + 155f, 45f, 40f, 25f), "Misc", "box"))
                {
                    settings[0x40] = 0x6b;
                }
                else if (GUI.Button(new Rect(num11 + 200f, 45f, 70f, 25f), "Credits", "box"))
                {
                    settings[0x40] = 0x6a;
                }
                if (((int)settings[0x40]) == 0x65)
                {
                    GameObject obj5;
                    this.scroll = GUI.BeginScrollView(new Rect(num11, 80f, 305f, 350f), this.scroll, new Rect(num11, 80f, 300f, 470f), true, true);
                    GUI.Label(new Rect(num11 + 100f, 80f, 120f, 20f), "General Objects:", "Label");
                    GUI.Label(new Rect(num11 + 108f, 245f, 120f, 20f), "Spawn Points:", "Label");
                    GUI.Label(new Rect(num11 + 7f, 415f, 290f, 60f), "* The above titan spawn points apply only to randomly spawned titans specified by the Random Titan #.", "Label");
                    GUI.Label(new Rect(num11 + 7f, 470f, 290f, 60f), "* If team mode is disabled both cyan and magenta spawn points will be randomly chosen for players.", "Label");
                    GUI.DrawTexture(new Rect(num11 + 27f, 110f, 64f, 64f), this.RCLoadTexture("psupply"));
                    GUI.DrawTexture(new Rect(num11 + 118f, 110f, 64f, 64f), this.RCLoadTexture("pcannonwall"));
                    GUI.DrawTexture(new Rect(num11 + 209f, 110f, 64f, 64f), this.RCLoadTexture("pcannonground"));
                    GUI.DrawTexture(new Rect(num11 + 27f, 275f, 64f, 64f), this.RCLoadTexture("pspawnt"));
                    GUI.DrawTexture(new Rect(num11 + 118f, 275f, 64f, 64f), this.RCLoadTexture("pspawnplayerC"));
                    GUI.DrawTexture(new Rect(num11 + 209f, 275f, 64f, 64f), this.RCLoadTexture("pspawnplayerM"));
                    if (GUI.Button(new Rect(num11 + 27f, 179f, 64f, 60f), "Supply"))
                    {
                        flag2 = true;
                        obj5 = (GameObject)BRM.CacheResources.Load("aot_supply");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "base,aot_supply";
                    }
                    else if (GUI.Button(new Rect(num11 + 118f, 179f, 64f, 60f), "Cannon \nWall"))
                    {
                        flag2 = true;
                        obj5 = (GameObject)RCassets.Load("CannonWallProp");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "photon,CannonWall";
                    }
                    else if (GUI.Button(new Rect(num11 + 209f, 179f, 64f, 60f), "Cannon\n Ground"))
                    {
                        flag2 = true;
                        obj5 = (GameObject)RCassets.Load("CannonGroundProp");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "photon,CannonGround";
                    }
                    else if (GUI.Button(new Rect(num11 + 27f, 344f, 64f, 60f), "Titan"))
                    {
                        flag2 = true;
                        flag3 = true;
                        obj5 = (GameObject)RCassets.Load("titan");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "spawnpoint,titan";
                    }
                    else if (GUI.Button(new Rect(num11 + 118f, 344f, 64f, 60f), "Player \nCyan"))
                    {
                        flag2 = true;
                        flag3 = true;
                        obj5 = (GameObject)RCassets.Load("playerC");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "spawnpoint,playerC";
                    }
                    else if (GUI.Button(new Rect(num11 + 209f, 344f, 64f, 60f), "Player \nMagenta"))
                    {
                        flag2 = true;
                        flag3 = true;
                        obj5 = (GameObject)RCassets.Load("playerM");
                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj5);
                        this.selectedObj.name = "spawnpoint,playerM";
                    }
                    GUI.EndScrollView();
                }
                else
                {
                    GameObject obj6;
                    if (((int)settings[0x40]) == 0x6b)
                    {
                        GUI.DrawTexture(new Rect(num11 + 30f, 90f, 64f, 64f), this.RCLoadTexture("pbarrier"));
                        GUI.DrawTexture(new Rect(num11 + 30f, 199f, 64f, 64f), this.RCLoadTexture("pregion"));
                        GUI.Label(new Rect(num11 + 110f, 243f, 200f, 22f), "Region Name:", "Label");
                        GUI.Label(new Rect(num11 + 110f, 179f, 200f, 22f), "Disable Map Bounds:", "Label");
                        bool flag7 = false;
                        if (((int)settings[0xba]) == 1)
                        {
                            flag7 = true;
                            if (!linkHash[3].ContainsKey("mapbounds"))
                            {
                                linkHash[3].Add("mapbounds", "map,disablebounds");
                            }
                        }
                        else if (linkHash[3].ContainsKey("mapbounds"))
                        {
                            linkHash[3].Remove("mapbounds");
                        }
                        if (GUI.Button(new Rect(num11 + 30f, 159f, 64f, 30f), "Barrier"))
                        {
                            flag2 = true;
                            flag3 = true;
                            obj6 = (GameObject)RCassets.Load("barrierEditor");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj6);
                            this.selectedObj.name = "misc,barrier";
                        }
                        else if (GUI.Button(new Rect(num11 + 30f, 268f, 64f, 30f), "Region"))
                        {
                            if (((string)settings[0xbf]) == string.Empty)
                            {
                                settings[0xbf] = "Region" + UnityEngine.Random.Range(0x2710, 0x1869f).ToString();
                            }
                            flag2 = true;
                            flag3 = true;
                            obj6 = (GameObject)RCassets.Load("regionEditor");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj6);
                            obj4 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("UI/LabelNameOverHead"));
                            obj4.name = "RegionLabel";
                            if (!float.TryParse((string)settings[0x47], out num31))
                            {
                                settings[0x47] = "1";
                            }
                            if (!float.TryParse((string)settings[70], out num31))
                            {
                                settings[70] = "1";
                            }
                            if (!float.TryParse((string)settings[0x48], out num31))
                            {
                                settings[0x48] = "1";
                            }
                            obj4.transform.parent = this.selectedObj.transform;
                            num14 = 1f;
                            if (Convert.ToSingle((string)settings[0x47]) > 100f)
                            {
                                num14 = 0.8f;
                            }
                            else if (Convert.ToSingle((string)settings[0x47]) > 1000f)
                            {
                                num14 = 0.5f;
                            }
                            obj4.transform.localPosition = new Vector3(0f, num14, 0f);
                            obj4.transform.localScale = new Vector3(5f / Convert.ToSingle((string)settings[70]), 5f / Convert.ToSingle((string)settings[0x47]), 5f / Convert.ToSingle((string)settings[0x48]));
                            obj4.GetComponent<UILabel>().text = (string)settings[0xbf];
                            this.selectedObj.AddComponent<RCRegionLabel>();
                            this.selectedObj.GetComponent<RCRegionLabel>().myLabel = obj4;
                            this.selectedObj.name = "misc,region," + ((string)settings[0xbf]);
                        }
                        settings[0xbf] = GUI.TextField(new Rect(num11 + 200f, 243f, 75f, 20f), (string)settings[0xbf]);
                        bool flag8 = GUI.Toggle(new Rect(num11 + 240f, 179f, 40f, 20f), flag7, "On");
                        if (flag8 != flag7)
                        {
                            if (flag8)
                            {
                                settings[0xba] = 1;
                            }
                            else
                            {
                                settings[0xba] = 0;
                            }
                        }
                    }
                    else if (((int)settings[0x40]) == 0x69)
                    {
                        float num32;
                        GameObject obj7;
                        GUI.Label(new Rect(num11 + 95f, 85f, 130f, 20f), "Custom Spawners:", "Label");
                        GUI.DrawTexture(new Rect(num11 + 7.8f, 110f, 64f, 64f), this.RCLoadTexture("ptitan"));
                        GUI.DrawTexture(new Rect(num11 + 79.6f, 110f, 64f, 64f), this.RCLoadTexture("pabnormal"));
                        GUI.DrawTexture(new Rect(num11 + 151.4f, 110f, 64f, 64f), this.RCLoadTexture("pjumper"));
                        GUI.DrawTexture(new Rect(num11 + 223.2f, 110f, 64f, 64f), this.RCLoadTexture("pcrawler"));
                        GUI.DrawTexture(new Rect(num11 + 7.8f, 224f, 64f, 64f), this.RCLoadTexture("ppunk"));
                        GUI.DrawTexture(new Rect(num11 + 79.6f, 224f, 64f, 64f), this.RCLoadTexture("pannie"));
                        if (GUI.Button(new Rect(num11 + 7.8f, 179f, 64f, 30f), "Titan"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnTitan");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnTitan," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        else if (GUI.Button(new Rect(num11 + 79.6f, 179f, 64f, 30f), "Aberrant"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnAbnormal");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnAbnormal," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        else if (GUI.Button(new Rect(num11 + 151.4f, 179f, 64f, 30f), "Jumper"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnJumper");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnJumper," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        else if (GUI.Button(new Rect(num11 + 223.2f, 179f, 64f, 30f), "Crawler"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnCrawler");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnCrawler," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        else if (GUI.Button(new Rect(num11 + 7.8f, 293f, 64f, 30f), "Punk"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnPunk");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnPunk," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        else if (GUI.Button(new Rect(num11 + 79.6f, 293f, 64f, 30f), "Annie"))
                        {
                            if (!float.TryParse((string)settings[0x53], out num32))
                            {
                                settings[0x53] = "30";
                            }
                            flag2 = true;
                            flag3 = true;
                            obj7 = (GameObject)RCassets.Load("spawnAnnie");
                            this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj7);
                            num30 = (int)settings[0x54];
                            this.selectedObj.name = "photon,spawnAnnie," + ((string)settings[0x53]) + "," + num30.ToString();
                        }
                        GUI.Label(new Rect(num11 + 7f, 379f, 140f, 22f), "Spawn Timer:", "Label");
                        settings[0x53] = GUI.TextField(new Rect(num11 + 100f, 379f, 50f, 20f), (string)settings[0x53]);
                        GUI.Label(new Rect(num11 + 7f, 356f, 140f, 22f), "Endless spawn:", "Label");
                        GUI.Label(new Rect(num11 + 7f, 405f, 290f, 80f), "* The above settings apply only to the next placed spawner. You can have unique spawn times and settings for each individual titan spawner.", "Label");
                        bool flag9 = false;
                        if (((int)settings[0x54]) == 1)
                        {
                            flag9 = true;
                        }
                        flag10 = GUI.Toggle(new Rect(num11 + 100f, 356f, 40f, 20f), flag9, "On");
                        if (flag9 != flag10)
                        {
                            if (flag10)
                            {
                                settings[0x54] = 1;
                            }
                            else
                            {
                                settings[0x54] = 0;
                            }
                        }
                    }
                    else
                    {
                        string[] strArray14;
                        if (((int)settings[0x40]) == 0x66)
                        {
                            strArray14 = new string[] { "cuboid", "plane", "sphere", "cylinder", "capsule", "pyramid", "cone", "prism", "arc90", "arc180", "torus", "tube" };
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), 90f + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), 159f + (114f * num28), 64f, 30f), strArray14[num13]))
                                {
                                    flag2 = true;
                                    obj6 = (GameObject)RCassets.Load(strArray14[num13]);
                                    this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj6);
                                    this.selectedObj.name = "custom," + strArray14[num13];
                                }
                            }
                        }
                        else
                        {
                            List<string> list4;
                            GameObject obj8;
                            if (((int)settings[0x40]) == 0x67)
                            {
                                list4 = new List<string> { "arch1", "house1" };
                                strArray14 = new string[] {
                                    "tower1", "tower2", "tower3", "tower4", "tower5", "house1", "house2", "house3", "house4", "house5", "house6", "house7", "house8", "house9", "house10", "house11",
                                    "house12", "house13", "house14", "pillar1", "pillar2", "village1", "village2", "windmill1", "arch1", "canal1", "castle1", "church1", "cannon1", "statue1", "statue2", "wagon1",
                                    "elevator1", "bridge1", "dummy1", "spike1", "wall1", "wall2", "wall3", "wall4", "arena1", "arena2", "arena3", "arena4"
                                 };
                                num27 = 110f + (114f * ((strArray14.Length - 1) / 4));
                                this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num27), true, true);
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), 90f + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), 159f + (114f * num28), 64f, 30f), strArray14[num13]))
                                    {
                                        flag2 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        if (list4.Contains(strArray14[num13]))
                                        {
                                            this.selectedObj.name = "customb," + strArray14[num13];
                                        }
                                        else
                                        {
                                            this.selectedObj.name = "custom," + strArray14[num13];
                                        }
                                    }
                                }
                                GUI.EndScrollView();
                            }
                            else if (((int)settings[0x40]) == 0x68)
                            {
                                list4 = new List<string> { "tree0" };
                                strArray14 = new string[] {
                                    "leaf0", "leaf1", "leaf2", "field1", "field2", "tree0", "tree1", "tree2", "tree3", "tree4", "tree5", "tree6", "tree7", "log1", "log2", "trunk1",
                                    "boulder1", "boulder2", "boulder3", "boulder4", "boulder5", "cave1", "cave2"
                                 };
                                num27 = 110f + (114f * ((strArray14.Length - 1) / 4));
                                this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num27), true, true);
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), 90f + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), 159f + (114f * num28), 64f, 30f), strArray14[num13]))
                                    {
                                        flag2 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        if (list4.Contains(strArray14[num13]))
                                        {
                                            this.selectedObj.name = "customb," + strArray14[num13];
                                        }
                                        else
                                        {
                                            this.selectedObj.name = "custom," + strArray14[num13];
                                        }
                                    }
                                }
                                GUI.EndScrollView();
                            }
                            else if (((int)settings[0x40]) == 0x6c)
                            {
                                string[] strArray15 = new string[] { "Cuboid", "Plane", "Sphere", "Cylinder", "Capsule", "Pyramid", "Cone", "Prism", "Arc90", "Arc180", "Torus", "Tube" };
                                strArray14 = new string[12];
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    strArray14[num13] = "start" + strArray15[num13];
                                }
                                num27 = 110f + (114f * ((strArray14.Length - 1) / 4));
                                num27 *= 4f;
                                num27 += 200f;
                                this.scroll = GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), this.scroll, new Rect(num11, 90f, 300f, num27), true, true);
                                GUI.Label(new Rect(num11 + 90f, 90f, 200f, 22f), "Racing Start Barrier");
                                int num33 = 0x7d;
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), num33 + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), (num33 + 69f) + (114f * num28), 64f, 30f), strArray15[num13]))
                                    {
                                        flag2 = true;
                                        flag3 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        this.selectedObj.name = "racing," + strArray14[num13];
                                    }
                                }
                                num33 += (0x72 * (strArray14.Length / 4)) + 10;
                                GUI.Label(new Rect(num11 + 93f, (float)num33, 200f, 22f), "Racing End Trigger");
                                num33 += 0x23;
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    strArray14[num13] = "end" + strArray15[num13];
                                }
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), num33 + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), (num33 + 69f) + (114f * num28), 64f, 30f), strArray15[num13]))
                                    {
                                        flag2 = true;
                                        flag3 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        this.selectedObj.name = "racing," + strArray14[num13];
                                    }
                                }
                                num33 += (0x72 * (strArray14.Length / 4)) + 10;
                                GUI.Label(new Rect(num11 + 113f, (float)num33, 200f, 22f), "Kill Trigger");
                                num33 += 0x23;
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    strArray14[num13] = "kill" + strArray15[num13];
                                }
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), num33 + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), (num33 + 69f) + (114f * num28), 64f, 30f), strArray15[num13]))
                                    {
                                        flag2 = true;
                                        flag3 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        this.selectedObj.name = "racing," + strArray14[num13];
                                    }
                                }
                                num33 += (0x72 * (strArray14.Length / 4)) + 10;
                                GUI.Label(new Rect(num11 + 95f, (float)num33, 200f, 22f), "Checkpoint Trigger");
                                num33 += 0x23;
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    strArray14[num13] = "checkpoint" + strArray15[num13];
                                }
                                for (num13 = 0; num13 < strArray14.Length; num13++)
                                {
                                    num29 = num13 % 4;
                                    num28 = num13 / 4;
                                    GUI.DrawTexture(new Rect((num11 + 7.8f) + (71.8f * num29), num33 + (114f * num28), 64f, 64f), this.RCLoadTexture("p" + strArray14[num13]));
                                    if (GUI.Button(new Rect((num11 + 7.8f) + (71.8f * num29), (num33 + 69f) + (114f * num28), 64f, 30f), strArray15[num13]))
                                    {
                                        flag2 = true;
                                        flag3 = true;
                                        obj8 = (GameObject)RCassets.Load(strArray14[num13]);
                                        this.selectedObj = (GameObject)UnityEngine.Object.Instantiate(obj8);
                                        this.selectedObj.name = "racing," + strArray14[num13];
                                    }
                                }
                                GUI.EndScrollView();
                            }
                            else if (((int)settings[0x40]) == 0x6a)
                            {
                                GUI.Label(new Rect(num11 + 10f, 80f, 200f, 22f), "- Tree 2 designed by Ken P.", "Label");
                                GUI.Label(new Rect(num11 + 10f, 105f, 250f, 22f), "- Tower 2, House 5 designed by Matthew Santos", "Label");
                                GUI.Label(new Rect(num11 + 10f, 130f, 200f, 22f), "- Cannon retextured by Mika", "Label");
                                GUI.Label(new Rect(num11 + 10f, 155f, 200f, 22f), "- Arena 1,2,3 & 4 created by Gun", "Label");
                                GUI.Label(new Rect(num11 + 10f, 180f, 250f, 22f), "- Cannon Wall/Ground textured by Bellfox", "Label");
                                GUI.Label(new Rect(num11 + 10f, 205f, 250f, 120f), "- House 7 - 14, Statue1, Statue2, Wagon1, Wall 1, Wall 2, Wall 3, Wall 4, CannonWall, CannonGround, Tower5, Bridge1, Dummy1, Spike1 created by meecube", "Label");
                            }
                        }
                    }
                }
                if (flag2 && (this.selectedObj != null))
                {
                    float y;
                    float num37;
                    float num38;
                    float num39;
                    float z;
                    float num41;
                    string name;
                    if (!float.TryParse((string)settings[70], out num31))
                    {
                        settings[70] = "1";
                    }
                    if (!float.TryParse((string)settings[0x47], out num31))
                    {
                        settings[0x47] = "1";
                    }
                    if (!float.TryParse((string)settings[0x48], out num31))
                    {
                        settings[0x48] = "1";
                    }
                    if (!float.TryParse((string)settings[0x4f], out num31))
                    {
                        settings[0x4f] = "1";
                    }
                    if (!float.TryParse((string)settings[80], out num31))
                    {
                        settings[80] = "1";
                    }
                    if (!flag3)
                    {
                        float a = 1f;
                        if (((string)settings[0x45]) != "default")
                        {
                            if (((string)settings[0x45]).StartsWith("transparent"))
                            {
                                float num35;
                                if (float.TryParse(((string)settings[0x45]).Substring(11), out num35))
                                {
                                    a = num35;
                                }
                                foreach (Renderer renderer2 in this.selectedObj.GetComponentsInChildren<Renderer>())
                                {
                                    renderer2.material = (Material)RCassets.Load("transparent");
                                    renderer2.material.mainTextureScale = new Vector2(renderer2.material.mainTextureScale.x * Convert.ToSingle((string)settings[0x4f]), renderer2.material.mainTextureScale.y * Convert.ToSingle((string)settings[80]));
                                }
                            }
                            else
                            {
                                foreach (Renderer renderer2 in this.selectedObj.GetComponentsInChildren<Renderer>())
                                {
                                    if (!(renderer2.name.Contains("Particle System") && this.selectedObj.name.Contains("aot_supply")))
                                    {
                                        renderer2.material = (Material)RCassets.Load((string)settings[0x45]);
                                        renderer2.material.mainTextureScale = new Vector2(renderer2.material.mainTextureScale.x * Convert.ToSingle((string)settings[0x4f]), renderer2.material.mainTextureScale.y * Convert.ToSingle((string)settings[80]));
                                    }
                                }
                            }
                        }
                        y = 1f;
                        foreach (MeshFilter filter in this.selectedObj.GetComponentsInChildren<MeshFilter>())
                        {
                            if (this.selectedObj.name.StartsWith("customb"))
                            {
                                if (y < filter.mesh.bounds.size.y)
                                {
                                    y = filter.mesh.bounds.size.y;
                                }
                            }
                            else if (y < filter.mesh.bounds.size.z)
                            {
                                y = filter.mesh.bounds.size.z;
                            }
                        }
                        num37 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                        num37 -= 0.001f;
                        num38 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[0x47]);
                        num39 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[0x48]);
                        this.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                        if (((int)settings[0x4c]) == 1)
                        {
                            color = new Color((float)settings[0x49], (float)settings[0x4a], (float)settings[0x4b], a);
                            foreach (MeshFilter filter in this.selectedObj.GetComponentsInChildren<MeshFilter>())
                            {
                                mesh = filter.mesh;
                                colorArray = new Color[mesh.vertexCount];
                                num18 = 0;
                                while (num18 < mesh.vertexCount)
                                {
                                    colorArray[num18] = color;
                                    num18++;
                                }
                                mesh.colors = colorArray;
                            }
                        }
                        z = this.selectedObj.transform.localScale.z;
                        if ((this.selectedObj.name.Contains("boulder2") || this.selectedObj.name.Contains("boulder3")) || this.selectedObj.name.Contains("field2"))
                        {
                            z *= 0.01f;
                        }
                        num41 = 10f + (((z * y) * 1.2f) / 2f);
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.forward.x * num41), Camera.main.transform.position.y + (Camera.main.transform.forward.y * 10f), Camera.main.transform.position.z + (Camera.main.transform.forward.z * num41));
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                        name = this.selectedObj.name;
                        string[] strArray3 = new string[0x15];
                        strArray3[0] = name;
                        strArray3[1] = ",";
                        strArray3[2] = (string)settings[0x45];
                        strArray3[3] = ",";
                        strArray3[4] = (string)settings[70];
                        strArray3[5] = ",";
                        strArray3[6] = (string)settings[0x47];
                        strArray3[7] = ",";
                        strArray3[8] = (string)settings[0x48];
                        strArray3[9] = ",";
                        strArray3[10] = settings[0x4c].ToString();
                        strArray3[11] = ",";
                        float num42 = (float)settings[0x49];
                        strArray3[12] = num42.ToString();
                        strArray3[13] = ",";
                        num42 = (float)settings[0x4a];
                        strArray3[14] = num42.ToString();
                        strArray3[15] = ",";
                        strArray3[0x10] = ((float)settings[0x4b]).ToString();
                        strArray3[0x11] = ",";
                        strArray3[0x12] = (string)settings[0x4f];
                        strArray3[0x13] = ",";
                        strArray3[20] = (string)settings[80];
                        this.selectedObj.name = string.Concat(strArray3);
                        this.unloadAssetsEditor();
                    }
                    else if (this.selectedObj.name.StartsWith("misc"))
                    {
                        if ((this.selectedObj.name.Contains("barrier") || this.selectedObj.name.Contains("region")) || this.selectedObj.name.Contains("racing"))
                        {
                            y = 1f;
                            num37 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                            num37 -= 0.001f;
                            num38 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[0x47]);
                            num39 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[0x48]);
                            this.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                            z = this.selectedObj.transform.localScale.z;
                            num41 = 10f + (((z * y) * 1.2f) / 2f);
                            this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.forward.x * num41), Camera.main.transform.position.y + (Camera.main.transform.forward.y * 10f), Camera.main.transform.position.z + (Camera.main.transform.forward.z * num41));
                            if (!this.selectedObj.name.Contains("region"))
                            {
                                this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                            }
                            name = this.selectedObj.name;
                            this.selectedObj.name = name + "," + ((string)settings[70]) + "," + ((string)settings[0x47]) + "," + ((string)settings[0x48]);
                        }
                    }
                    else if (this.selectedObj.name.StartsWith("racing"))
                    {
                        y = 1f;
                        num37 = this.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                        num37 -= 0.001f;
                        num38 = this.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[0x47]);
                        num39 = this.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[0x48]);
                        this.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                        z = this.selectedObj.transform.localScale.z;
                        num41 = 10f + (((z * y) * 1.2f) / 2f);
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.forward.x * num41), Camera.main.transform.position.y + (Camera.main.transform.forward.y * 10f), Camera.main.transform.position.z + (Camera.main.transform.forward.z * num41));
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                        name = this.selectedObj.name;
                        this.selectedObj.name = name + "," + ((string)settings[70]) + "," + ((string)settings[0x47]) + "," + ((string)settings[0x48]);
                    }
                    else
                    {
                        this.selectedObj.transform.position = new Vector3(Camera.main.transform.position.x + (Camera.main.transform.forward.x * 10f), Camera.main.transform.position.y + (Camera.main.transform.forward.y * 10f), Camera.main.transform.position.z + (Camera.main.transform.forward.z * 10f));
                        this.selectedObj.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                    }
                    Screen.lockCursor = true;
                    GUI.FocusControl(null);
                }
            }
            else if ((FengCustomInputs.Inputs != null) && FengCustomInputs.Inputs.menuOn)
            {
                Screen.showCursor = true;
                Screen.lockCursor = false;
                if (((int)settings[0x40]) != 6)
                {
                    num7 = (((float)Screen.width) / 2f) - 350f;
                    num8 = (((float)Screen.height) / 2f) - 250f;
                    GUI.backgroundColor = Color.gray;
                    GUI.DrawTexture(new Rect(num7 + 2f, num8 + 2f, 696f, 496f), this.textureBackgroundBlack);
                    GUI.Box(new Rect(num7, num8, 700f, 500f), string.Empty);
                    if (GUI.Button(new Rect(num7 + 7f, num8 + 7f, 59f, 25f), "General"))
                    {
                        settings[0x40] = 0;
                    }
                    else if (GUI.Button(new Rect(num7 + 71f, num8 + 7f, 60f, 25f), "Rebinds"))
                    {
                        settings[0x40] = 1;
                    }
                    else if (GUI.Button(new Rect(num7 + 136f, num8 + 7f, 85f, 25f), "Human Skins"))
                    {
                        settings[0x40] = 2;
                    }
                    else if (GUI.Button(new Rect(num7 + 226f, num8 + 7f, 85f, 25f), "Titan Skins"))
                    {
                        settings[0x40] = 3;
                    }
                    else if (GUI.Button(new Rect(num7 + 316f, num8 + 7f, 85f, 25f), "Level Skins"))
                    {
                        settings[0x40] = 7;
                    }
                    else if (GUI.Button(new Rect(num7 + 406f, num8 + 7f, 85f, 25f), "Custom Map"))
                    {
                        settings[0x40] = 8;
                    }
                    else if (GUI.Button(new Rect(num7 + 496f, num8 + 7f, 88f, 25f), "Custom Logic"))
                    {
                        settings[0x40] = 9;
                    }
                    else if (GUI.Button(new Rect(num7 + 589f, num8 + 7f, 95f, 25f), "Game Settings"))
                    {
                        settings[0x40] = 10;
                    }
                    else if (GUI.Button(new Rect(num7 + 7f, num8 + 37f, 70f, 25f), "Abilities"))
                    {
                        settings[0x40] = 11;
                    }
                    else if (GUI.Button(new Rect(num7 + 82f, num8 + 37f, 70f, 25f), "UIName"))
                    {
                        settings[0x40] = 12;
                    }
                    if (((int)settings[0x40]) == 9)
                    {
                        currentScriptLogic = GUI.TextField(new Rect(num7 + 50f, num8 + 82f, 600f, 270f), currentScriptLogic);
                        if (GUI.Button(new Rect(num7 + 250f, num8 + 365f, 50f, 20f), "Copy"))
                        {
                            editor = new TextEditor
                            {
                                content = new GUIContent(currentScriptLogic)
                            };
                            editor.SelectAll();
                            editor.Copy();
                        }
                        else if (GUI.Button(new Rect(num7 + 400f, num8 + 365f, 50f, 20f), "Clear"))
                        {
                            currentScriptLogic = string.Empty;
                        }
                    }
                    else if (((int)settings[0x40]) == 12)
                    {
                        UIName();
                    }
                    else if (((int)settings[0x40]) == 11)
                    {
                        GUI.Label(new Rect(num7 + 150f, num8 + 80f, 185f, 22f), "Bomb Mode", "Label");
                        GUI.Label(new Rect(num7 + 80f, num8 + 110f, 80f, 22f), "Color:", "Label");
                        textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                        textured.SetPixel(0, 0, new Color((float)settings[0xf6], (float)settings[0xf7], (float)settings[0xf8], (float)settings[0xf9]));
                        textured.Apply();
                        GUI.DrawTexture(new Rect(num7 + 120f, num8 + 113f, 40f, 15f), textured, ScaleMode.StretchToFill);
                        UnityEngine.Object.Destroy(textured);
                        GUI.Label(new Rect(num7 + 72f, num8 + 135f, 20f, 22f), "R:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 160f, 20f, 22f), "G:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 185f, 20f, 22f), "B:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 210f, 20f, 22f), "A:", "Label");
                        settings[0xf6] = GUI.HorizontalSlider(new Rect(num7 + 92f, num8 + 138f, 100f, 20f), (float)settings[0xf6], 0f, 1f);
                        settings[0xf7] = GUI.HorizontalSlider(new Rect(num7 + 92f, num8 + 163f, 100f, 20f), (float)settings[0xf7], 0f, 1f);
                        settings[0xf8] = GUI.HorizontalSlider(new Rect(num7 + 92f, num8 + 188f, 100f, 20f), (float)settings[0xf8], 0f, 1f);
                        settings[0xf9] = GUI.HorizontalSlider(new Rect(num7 + 92f, num8 + 213f, 100f, 20f), (float)settings[0xf9], 0.5f, 1f);
                        GUI.Label(new Rect(num7 + 72f, num8 + 235f, 95f, 22f), "Bomb Radius:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 260f, 95f, 22f), "Bomb Range:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 285f, 95f, 22f), "Bomb Speed:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 310f, 95f, 22f), "Bomb CD:", "Label");
                        GUI.Label(new Rect(num7 + 72f, num8 + 335f, 95f, 22f), "Unused Points:", "Label");
                        num30 = (int)settings[250];
                        GUI.Label(new Rect(num7 + 168f, num8 + 235f, 20f, 22f), num30.ToString(), "Label");
                        num30 = (int)settings[0xfb];
                        GUI.Label(new Rect(num7 + 168f, num8 + 260f, 20f, 22f), num30.ToString(), "Label");
                        num30 = (int)settings[0xfc];
                        GUI.Label(new Rect(num7 + 168f, num8 + 285f, 20f, 22f), num30.ToString(), "Label");
                        GUI.Label(new Rect(num7 + 168f, num8 + 310f, 20f, 22f), ((int)settings[0xfd]).ToString(), "Label");
                        int num43 = (((20 - ((int)settings[250])) - ((int)settings[0xfb])) - ((int)settings[0xfc])) - ((int)settings[0xfd]);
                        GUI.Label(new Rect(num7 + 168f, num8 + 335f, 20f, 22f), num43.ToString(), "Label");
                        if (GUI.Button(new Rect(num7 + 190f, num8 + 235f, 20f, 20f), "-"))
                        {
                            if (((int)settings[250]) > 0)
                            {
                                settings[250] = ((int)settings[250]) - 1;
                            }
                        }
                        else if (GUI.Button(new Rect(num7 + 215f, num8 + 235f, 20f, 20f), "+") && ((((int)settings[250]) < 10) && (num43 > 0)))
                        {
                            settings[250] = ((int)settings[250]) + 1;
                        }
                        if (GUI.Button(new Rect(num7 + 190f, num8 + 260f, 20f, 20f), "-"))
                        {
                            if (((int)settings[0xfb]) > 0)
                            {
                                settings[0xfb] = ((int)settings[0xfb]) - 1;
                            }
                        }
                        else if (GUI.Button(new Rect(num7 + 215f, num8 + 260f, 20f, 20f), "+") && ((((int)settings[0xfb]) < 10) && (num43 > 0)))
                        {
                            settings[0xfb] = ((int)settings[0xfb]) + 1;
                        }
                        if (GUI.Button(new Rect(num7 + 190f, num8 + 285f, 20f, 20f), "-"))
                        {
                            if (((int)settings[0xfc]) > 0)
                            {
                                settings[0xfc] = ((int)settings[0xfc]) - 1;
                            }
                        }
                        else if (GUI.Button(new Rect(num7 + 215f, num8 + 285f, 20f, 20f), "+") && ((((int)settings[0xfc]) < 10) && (num43 > 0)))
                        {
                            settings[0xfc] = ((int)settings[0xfc]) + 1;
                        }
                        if (GUI.Button(new Rect(num7 + 190f, num8 + 310f, 20f, 20f), "-"))
                        {
                            if (((int)settings[0xfd]) > 0)
                            {
                                settings[0xfd] = ((int)settings[0xfd]) - 1;
                            }
                        }
                        else if (GUI.Button(new Rect(num7 + 215f, num8 + 310f, 20f, 20f), "+") && ((((int)settings[0xfd]) < 10) && (num43 > 0)))
                        {
                            settings[0xfd] = ((int)settings[0xfd]) + 1;
                        }
                    }
                    else
                    {
                        float num44;
                        if (((int)settings[0x40]) == 2)
                        {
                            GUI.Label(new Rect(num7 + 205f, num8 + 52f, 120f, 30f), "Human Skin Mode:", "Label");
                            flag2 = false;
                            if (((int)settings[0]) == 1)
                            {
                                flag2 = true;
                            }
                            flag5 = GUI.Toggle(new Rect(num7 + 325f, num8 + 52f, 40f, 20f), flag2, "On");
                            if (flag2 != flag5)
                            {
                                if (flag5)
                                {
                                    settings[0] = 1;
                                }
                                else
                                {
                                    settings[0] = 0;
                                }
                            }
                            num44 = 44f;
                            if (((int)settings[0x85]) == 0)
                            {
                                if (GUI.Button(new Rect(num7 + 375f, num8 + 51f, 120f, 22f), "Human Set 1"))
                                {
                                    settings[0x85] = 1;
                                }
                                settings[3] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[3]);
                                settings[4] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[4]);
                                settings[5] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[5]);
                                settings[6] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[6]);
                                settings[7] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[7]);
                                settings[8] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[8]);
                                settings[14] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 6f), 230f, 20f), (string)settings[14]);
                                settings[9] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[9]);
                                settings[10] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[10]);
                                settings[11] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[11]);
                                settings[12] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[12]);
                                settings[13] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[13]);
                                settings[0x5e] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[0x5e]);
                            }
                            else if (((int)settings[0x85]) == 1)
                            {
                                if (GUI.Button(new Rect(num7 + 375f, num8 + 51f, 120f, 22f), "Human Set 2"))
                                {
                                    settings[0x85] = 2;
                                }
                                settings[0x86] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[0x86]);
                                settings[0x87] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[0x87]);
                                settings[0x88] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[0x88]);
                                settings[0x89] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[0x89]);
                                settings[0x8a] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[0x8a]);
                                settings[0x8b] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[0x8b]);
                                settings[0x91] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 6f), 230f, 20f), (string)settings[0x91]);
                                settings[140] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[140]);
                                settings[0x8d] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[0x8d]);
                                settings[0x8e] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[0x8e]);
                                settings[0x8f] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[0x8f]);
                                settings[0x90] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[0x90]);
                                settings[0x92] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[0x92]);
                            }
                            else if (((int)settings[0x85]) == 2)
                            {
                                if (GUI.Button(new Rect(num7 + 375f, num8 + 51f, 120f, 22f), "Human Set 3"))
                                {
                                    settings[0x85] = 0;
                                }
                                settings[0x93] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[0x93]);
                                settings[0x94] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[0x94]);
                                settings[0x95] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[0x95]);
                                settings[150] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[150]);
                                settings[0x97] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[0x97]);
                                settings[0x98] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[0x98]);
                                settings[0x9e] = GUI.TextField(new Rect(num7 + 80f, (num8 + 114f) + (num44 * 6f), 230f, 20f), (string)settings[0x9e]);
                                settings[0x99] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 0f), 230f, 20f), (string)settings[0x99]);
                                settings[0x9a] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 1f), 230f, 20f), (string)settings[0x9a]);
                                settings[0x9b] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 2f), 230f, 20f), (string)settings[0x9b]);
                                settings[0x9c] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 3f), 230f, 20f), (string)settings[0x9c]);
                                settings[0x9d] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 4f), 230f, 20f), (string)settings[0x9d]);
                                settings[0x9f] = GUI.TextField(new Rect(num7 + 390f, (num8 + 114f) + (num44 * 5f), 230f, 20f), (string)settings[0x9f]);
                            }
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 0f), 150f, 20f), "Horse:", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 1f), 227f, 20f), "Hair (model dependent):", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 2f), 150f, 20f), "Eyes:", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 3f), 240f, 20f), "Glass (must have a glass enabled):", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 4f), 150f, 20f), "Face:", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 5f), 150f, 20f), "Skin:", "Label");
                            GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 6f), 240f, 20f), "Hoodie (costume dependent):", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 0f), 240f, 20f), "Costume (model dependent):", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 1f), 150f, 20f), "Logo & Cape:", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 2f), 240f, 20f), "3DMG Center & 3DMG/Blade/Gun(left):", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 3f), 227f, 20f), "3DMG/Blade/Gun(right):", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 4f), 150f, 20f), "Gas:", "Label");
                            GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 5f), 150f, 20f), "Weapon Trail:", "Label");
                        }
                        else if (((int)settings[0x40]) == 3)
                        {
                            int num45;
                            int num46;
                            GUI.Label(new Rect(num7 + 270f, num8 + 52f, 120f, 30f), "Titan Skin Mode:", "Label");
                            flag6 = false;
                            if (((int)settings[1]) == 1)
                            {
                                flag6 = true;
                            }
                            bool flag11 = GUI.Toggle(new Rect(num7 + 390f, num8 + 52f, 40f, 20f), flag6, "On");
                            if (flag6 != flag11)
                            {
                                if (flag11)
                                {
                                    settings[1] = 1;
                                }
                                else
                                {
                                    settings[1] = 0;
                                }
                            }
                            GUI.Label(new Rect(num7 + 270f, num8 + 77f, 120f, 30f), "Randomized Pairs:", "Label");
                            flag6 = false;
                            if (((int)settings[0x20]) == 1)
                            {
                                flag6 = true;
                            }
                            flag11 = GUI.Toggle(new Rect(num7 + 390f, num8 + 77f, 40f, 20f), flag6, "On");
                            if (flag6 != flag11)
                            {
                                if (flag11)
                                {
                                    settings[0x20] = 1;
                                }
                                else
                                {
                                    settings[0x20] = 0;
                                }
                            }
                            GUI.Label(new Rect(num7 + 158f, num8 + 112f, 150f, 20f), "Titan Hair:", "Label");
                            settings[0x15] = GUI.TextField(new Rect(num7 + 80f, num8 + 134f, 165f, 20f), (string)settings[0x15]);
                            settings[0x16] = GUI.TextField(new Rect(num7 + 80f, num8 + 156f, 165f, 20f), (string)settings[0x16]);
                            settings[0x17] = GUI.TextField(new Rect(num7 + 80f, num8 + 178f, 165f, 20f), (string)settings[0x17]);
                            settings[0x18] = GUI.TextField(new Rect(num7 + 80f, num8 + 200f, 165f, 20f), (string)settings[0x18]);
                            settings[0x19] = GUI.TextField(new Rect(num7 + 80f, num8 + 222f, 165f, 20f), (string)settings[0x19]);
                            if (GUI.Button(new Rect(num7 + 250f, num8 + 134f, 60f, 20f), this.hairtype((int)settings[0x10])))
                            {
                                num45 = 0x10;
                                num46 = (int)settings[num45];
                                if (num46 >= 9)
                                {
                                    num46 = -1;
                                }
                                else
                                {
                                    num46++;
                                }
                                settings[num45] = num46;
                            }
                            else if (GUI.Button(new Rect(num7 + 250f, num8 + 156f, 60f, 20f), this.hairtype((int)settings[0x11])))
                            {
                                num45 = 0x11;
                                num46 = (int)settings[num45];
                                if (num46 >= 9)
                                {
                                    num46 = -1;
                                }
                                else
                                {
                                    num46++;
                                }
                                settings[num45] = num46;
                            }
                            else if (GUI.Button(new Rect(num7 + 250f, num8 + 178f, 60f, 20f), this.hairtype((int)settings[0x12])))
                            {
                                num45 = 0x12;
                                num46 = (int)settings[num45];
                                if (num46 >= 9)
                                {
                                    num46 = -1;
                                }
                                else
                                {
                                    num46++;
                                }
                                settings[num45] = num46;
                            }
                            else if (GUI.Button(new Rect(num7 + 250f, num8 + 200f, 60f, 20f), this.hairtype((int)settings[0x13])))
                            {
                                num45 = 0x13;
                                num46 = (int)settings[num45];
                                if (num46 >= 9)
                                {
                                    num46 = -1;
                                }
                                else
                                {
                                    num46++;
                                }
                                settings[num45] = num46;
                            }
                            else if (GUI.Button(new Rect(num7 + 250f, num8 + 222f, 60f, 20f), this.hairtype((int)settings[20])))
                            {
                                num45 = 20;
                                num46 = (int)settings[num45];
                                if (num46 >= 9)
                                {
                                    num46 = -1;
                                }
                                else
                                {
                                    num46++;
                                }
                                settings[num45] = num46;
                            }
                            GUI.Label(new Rect(num7 + 158f, num8 + 252f, 150f, 20f), "Titan Eye:", "Label");
                            settings[0x1a] = GUI.TextField(new Rect(num7 + 80f, num8 + 274f, 230f, 20f), (string)settings[0x1a]);
                            settings[0x1b] = GUI.TextField(new Rect(num7 + 80f, num8 + 296f, 230f, 20f), (string)settings[0x1b]);
                            settings[0x1c] = GUI.TextField(new Rect(num7 + 80f, num8 + 318f, 230f, 20f), (string)settings[0x1c]);
                            settings[0x1d] = GUI.TextField(new Rect(num7 + 80f, num8 + 340f, 230f, 20f), (string)settings[0x1d]);
                            settings[30] = GUI.TextField(new Rect(num7 + 80f, num8 + 362f, 230f, 20f), (string)settings[30]);
                            GUI.Label(new Rect(num7 + 455f, num8 + 112f, 150f, 20f), "Titan Body:", "Label");
                            settings[0x56] = GUI.TextField(new Rect(num7 + 390f, num8 + 134f, 230f, 20f), (string)settings[0x56]);
                            settings[0x57] = GUI.TextField(new Rect(num7 + 390f, num8 + 156f, 230f, 20f), (string)settings[0x57]);
                            settings[0x58] = GUI.TextField(new Rect(num7 + 390f, num8 + 178f, 230f, 20f), (string)settings[0x58]);
                            settings[0x59] = GUI.TextField(new Rect(num7 + 390f, num8 + 200f, 230f, 20f), (string)settings[0x59]);
                            settings[90] = GUI.TextField(new Rect(num7 + 390f, num8 + 222f, 230f, 20f), (string)settings[90]);
                            GUI.Label(new Rect(num7 + 472f, num8 + 252f, 150f, 20f), "Eren:", "Label");
                            settings[0x41] = GUI.TextField(new Rect(num7 + 390f, num8 + 274f, 230f, 20f), (string)settings[0x41]);
                            GUI.Label(new Rect(num7 + 470f, num8 + 296f, 150f, 20f), "Annie:", "Label");
                            settings[0x42] = GUI.TextField(new Rect(num7 + 390f, num8 + 318f, 230f, 20f), (string)settings[0x42]);
                            GUI.Label(new Rect(num7 + 465f, num8 + 340f, 150f, 20f), "Colossal:", "Label");
                            settings[0x43] = GUI.TextField(new Rect(num7 + 390f, num8 + 362f, 230f, 20f), (string)settings[0x43]);
                        }
                        else if (((int)settings[0x40]) == 7)
                        {
                            num44 = 22f;
                            GUI.Label(new Rect(num7 + 205f, num8 + 52f, 145f, 30f), "Level Skin Mode:", "Label");
                            bool flag12 = false;
                            if (((int)settings[2]) == 1)
                            {
                                flag12 = true;
                            }
                            bool flag13 = GUI.Toggle(new Rect(num7 + 325f, num8 + 52f, 40f, 20f), flag12, "On");
                            if (flag12 != flag13)
                            {
                                if (flag13)
                                {
                                    settings[2] = 1;
                                }
                                else
                                {
                                    settings[2] = 0;
                                }
                            }
                            if (((int)settings[0xbc]) == 0)
                            {
                                if (GUI.Button(new Rect(num7 + 375f, num8 + 51f, 120f, 22f), "Forest Skins"))
                                {
                                    settings[0xbc] = 1;
                                }
                                GUI.Label(new Rect(num7 + 205f, num8 + 77f, 145f, 30f), "Randomized Pairs:", "Label");
                                flag12 = false;
                                if (((int)settings[50]) == 1)
                                {
                                    flag12 = true;
                                }
                                flag13 = GUI.Toggle(new Rect(num7 + 325f, num8 + 77f, 40f, 20f), flag12, "On");
                                if (flag12 != flag13)
                                {
                                    if (flag13)
                                    {
                                        settings[50] = 1;
                                    }
                                    else
                                    {
                                        settings[50] = 0;
                                    }
                                }
                                this.scroll = GUI.BeginScrollView(new Rect(num7, num8 + 115f, 712f, 340f), this.scroll, new Rect(num7, num8 + 115f, 700f, 475f), true, true);
                                GUI.Label(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 0f), 150f, 20f), "Ground:", "Label");
                                settings[0x31] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 1f), 227f, 20f), (string)settings[0x31]);
                                GUI.Label(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 2f), 150f, 20f), "Forest Trunks", "Label");
                                settings[0x21] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 3f), 227f, 20f), (string)settings[0x21]);
                                settings[0x22] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 4f), 227f, 20f), (string)settings[0x22]);
                                settings[0x23] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 5f), 227f, 20f), (string)settings[0x23]);
                                settings[0x24] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 6f), 227f, 20f), (string)settings[0x24]);
                                settings[0x25] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 7f), 227f, 20f), (string)settings[0x25]);
                                settings[0x26] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 8f), 227f, 20f), (string)settings[0x26]);
                                settings[0x27] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 9f), 227f, 20f), (string)settings[0x27]);
                                settings[40] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 10f), 227f, 20f), (string)settings[40]);
                                GUI.Label(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 11f), 150f, 20f), "Forest Leaves:", "Label");
                                settings[0x29] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 12f), 227f, 20f), (string)settings[0x29]);
                                settings[0x2a] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 13f), 227f, 20f), (string)settings[0x2a]);
                                settings[0x2b] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 14f), 227f, 20f), (string)settings[0x2b]);
                                settings[0x2c] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 15f), 227f, 20f), (string)settings[0x2c]);
                                settings[0x2d] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 16f), 227f, 20f), (string)settings[0x2d]);
                                settings[0x2e] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 17f), 227f, 20f), (string)settings[0x2e]);
                                settings[0x2f] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 18f), 227f, 20f), (string)settings[0x2f]);
                                settings[0x30] = GUI.TextField(new Rect(num7 + 79f, (num8 + 117f) + (num44 * 19f), 227f, 20f), (string)settings[0x30]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 0f), 150f, 20f), "Skybox Front:", "Label");
                                settings[0xa3] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 1f), 227f, 20f), (string)settings[0xa3]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 2f), 150f, 20f), "Skybox Back:", "Label");
                                settings[0xa4] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 3f), 227f, 20f), (string)settings[0xa4]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 4f), 150f, 20f), "Skybox Left:", "Label");
                                settings[0xa5] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 5f), 227f, 20f), (string)settings[0xa5]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 6f), 150f, 20f), "Skybox Right:", "Label");
                                settings[0xa6] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 7f), 227f, 20f), (string)settings[0xa6]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 8f), 150f, 20f), "Skybox Up:", "Label");
                                settings[0xa7] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 9f), 227f, 20f), (string)settings[0xa7]);
                                GUI.Label(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 10f), 150f, 20f), "Skybox Down:", "Label");
                                settings[0xa8] = GUI.TextField(new Rect(num7 + 379f, (num8 + 117f) + (num44 * 11f), 227f, 20f), (string)settings[0xa8]);
                                GUI.EndScrollView();
                            }
                            else if (((int)settings[0xbc]) == 1)
                            {
                                if (GUI.Button(new Rect(num7 + 375f, num8 + 51f, 120f, 22f), "City Skins"))
                                {
                                    settings[0xbc] = 0;
                                }
                                GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 0f), 150f, 20f), "Ground:", "Label");
                                settings[0x3b] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 1f), 230f, 20f), (string)settings[0x3b]);
                                GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 2f), 150f, 20f), "Wall:", "Label");
                                settings[60] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 3f), 230f, 20f), (string)settings[60]);
                                GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 4f), 150f, 20f), "Gate:", "Label");
                                settings[0x3d] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 5f), 230f, 20f), (string)settings[0x3d]);
                                GUI.Label(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 6f), 150f, 20f), "Houses:", "Label");
                                settings[0x33] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 7f), 230f, 20f), (string)settings[0x33]);
                                settings[0x34] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 8f), 230f, 20f), (string)settings[0x34]);
                                settings[0x35] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 9f), 230f, 20f), (string)settings[0x35]);
                                settings[0x36] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 10f), 230f, 20f), (string)settings[0x36]);
                                settings[0x37] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 11f), 230f, 20f), (string)settings[0x37]);
                                settings[0x38] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 12f), 230f, 20f), (string)settings[0x38]);
                                settings[0x39] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 13f), 230f, 20f), (string)settings[0x39]);
                                settings[0x3a] = GUI.TextField(new Rect(num7 + 80f, (num8 + 92f) + (num44 * 14f), 230f, 20f), (string)settings[0x3a]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 0f), 150f, 20f), "Skybox Front:", "Label");
                                settings[0xa9] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 1f), 230f, 20f), (string)settings[0xa9]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 2f), 150f, 20f), "Skybox Back:", "Label");
                                settings[170] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 3f), 230f, 20f), (string)settings[170]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 4f), 150f, 20f), "Skybox Left:", "Label");
                                settings[0xab] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 5f), 230f, 20f), (string)settings[0xab]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 6f), 150f, 20f), "Skybox Right:", "Label");
                                settings[0xac] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 7f), 230f, 20f), (string)settings[0xac]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 8f), 150f, 20f), "Skybox Up:", "Label");
                                settings[0xad] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 9f), 230f, 20f), (string)settings[0xad]);
                                GUI.Label(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 10f), 150f, 20f), "Skybox Down:", "Label");
                                settings[0xae] = GUI.TextField(new Rect(num7 + 390f, (num8 + 92f) + (num44 * 11f), 230f, 20f), (string)settings[0xae]);
                            }
                        }
                        else if (((int)settings[0x40]) == 4)
                        {
                            GUI.TextArea(new Rect(num7 + 80f, num8 + 52f, 270f, 30f), "Settings saved to playerprefs!", 100, "Label");
                        }
                        else if (((int)settings[0x40]) == 5)
                        {
                            GUI.TextArea(new Rect(num7 + 80f, num8 + 52f, 270f, 30f), "Settings reloaded from playerprefs!", 100, "Label");
                        }
                        else
                        {
                            string[] strArray16;
                            if (((int)settings[0x40]) == 0)
                            {
                                int num47;
                                GUI.Label(new Rect(num7 + 150f, num8 + 51f, 185f, 22f), "Graphics", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 81f, 185f, 22f), "Disable custom gas textures:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 106f, 185f, 22f), "Disable weapon trail:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 131f, 185f, 22f), "Disable wind effect:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 156f, 185f, 22f), "Enable vSync:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 184f, 227f, 20f), "FPS Cap (0 for disabled):", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 212f, 150f, 22f), "Texture Quality:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 242f, 150f, 22f), "Overall Quality:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 272f, 185f, 22f), "Disable Mipmapping:", "Label");
                                GUI.Label(new Rect(num7 + 72f, num8 + 297f, 185f, 65f), "*Disabling mipmapping will increase custom texture quality at the cost of performance.", "Label");
                                this.qualitySlider = GUI.HorizontalSlider(new Rect(num7 + 199f, num8 + 247f, 115f, 20f), this.qualitySlider, 0f, 1f);
                                PlayerPrefs.SetFloat("GameQuality", this.qualitySlider);
                                if (this.qualitySlider < 0.167f)
                                {
                                    QualitySettings.SetQualityLevel(0, true);
                                }
                                else if (this.qualitySlider < 0.33f)
                                {
                                    QualitySettings.SetQualityLevel(1, true);
                                }
                                else if (this.qualitySlider < 0.5f)
                                {
                                    QualitySettings.SetQualityLevel(2, true);
                                }
                                else if (this.qualitySlider < 0.67f)
                                {
                                    QualitySettings.SetQualityLevel(3, true);
                                }
                                else if (this.qualitySlider < 0.83f)
                                {
                                    QualitySettings.SetQualityLevel(4, true);
                                }
                                else if (this.qualitySlider <= 1f)
                                {
                                    QualitySettings.SetQualityLevel(5, true);
                                }
                                if (!((this.qualitySlider < 0.9f) || level.StartsWith("Custom")))
                                {
                                    Camera.main.GetComponent<TiltShift>().enabled = true;
                                }
                                else
                                {
                                    Camera.main.GetComponent<TiltShift>().enabled = false;
                                }
                                bool flag14 = false;
                                bool flag15 = false;
                                bool flag16 = false;
                                bool flag17 = false;
                                bool flag18 = false;
                                if (((int)settings[15]) == 1)
                                {
                                    flag14 = true;
                                }
                                if (((int)settings[0x5c]) == 1)
                                {
                                    flag15 = true;
                                }
                                if (((int)settings[0x5d]) == 1)
                                {
                                    flag16 = true;
                                }
                                if (((int)settings[0x3f]) == 1)
                                {
                                    flag17 = true;
                                }
                                if (((int)settings[0xb7]) == 1)
                                {
                                    flag18 = true;
                                }
                                bool flag19 = GUI.Toggle(new Rect(num7 + 274f, num8 + 81f, 40f, 20f), flag14, "On");
                                if (flag19 != flag14)
                                {
                                    if (flag19)
                                    {
                                        settings[15] = 1;
                                    }
                                    else
                                    {
                                        settings[15] = 0;
                                    }
                                }
                                flag10 = GUI.Toggle(new Rect(num7 + 274f, num8 + 106f, 40f, 20f), flag15, "On");
                                if (flag10 != flag15)
                                {
                                    if (flag10)
                                    {
                                        settings[0x5c] = 1;
                                    }
                                    else
                                    {
                                        settings[0x5c] = 0;
                                    }
                                }
                                bool flag20 = GUI.Toggle(new Rect(num7 + 274f, num8 + 131f, 40f, 20f), flag16, "On");
                                if (flag20 != flag16)
                                {
                                    if (flag20)
                                    {
                                        settings[0x5d] = 1;
                                    }
                                    else
                                    {
                                        settings[0x5d] = 0;
                                    }
                                }
                                bool flag21 = GUI.Toggle(new Rect(num7 + 274f, num8 + 156f, 40f, 20f), flag18, "On");
                                if (flag21 != flag18)
                                {
                                    if (flag21)
                                    {
                                        settings[0xb7] = 1;
                                        QualitySettings.vSyncCount = 1;
                                    }
                                    else
                                    {
                                        settings[0xb7] = 0;
                                        QualitySettings.vSyncCount = 0;
                                    }
                                    Minimap.WaitAndTryRecaptureInstance(0.5f);
                                }
                                bool flag22 = GUI.Toggle(new Rect(num7 + 274f, num8 + 272f, 40f, 20f), flag17, "On");
                                if (flag22 != flag17)
                                {
                                    if (flag22)
                                    {
                                        settings[0x3f] = 1;
                                    }
                                    else
                                    {
                                        settings[0x3f] = 0;
                                    }
                                    linkHash[0].Clear();
                                    linkHash[1].Clear();
                                    linkHash[2].Clear();
                                }
                                if (GUI.Button(new Rect(num7 + 254f, num8 + 212f, 60f, 20f), this.mastertexturetype(QualitySettings.masterTextureLimit)))
                                {
                                    if (QualitySettings.masterTextureLimit <= 0)
                                    {
                                        QualitySettings.masterTextureLimit = 2;
                                    }
                                    else
                                    {
                                        QualitySettings.masterTextureLimit--;
                                    }
                                    linkHash[0].Clear();
                                    linkHash[1].Clear();
                                    linkHash[2].Clear();
                                }
                                settings[0xb8] = GUI.TextField(new Rect(num7 + 234f, num8 + 184f, 80f, 20f), (string)settings[0xb8]);
                                Application.targetFrameRate = -1;
                                if (int.TryParse((string)settings[0xb8], out num47) && (num47 > 0))
                                {
                                    Application.targetFrameRate = num47;
                                }
                                GUI.Label(new Rect(num7 + 470f, num8 + 51f, 185f, 22f), "Snapshots", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 81f, 185f, 22f), "Enable Snapshots:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 106f, 185f, 22f), "Show In Game:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 131f, 227f, 22f), "Snapshot Minimum Damage:", "Label");
                                settings[0x5f] = GUI.TextField(new Rect(num7 + 563f, num8 + 131f, 65f, 20f), (string)settings[0x5f]);
                                bool flag23 = false;
                                bool flag24 = false;
                                if (PlayerPrefs.GetInt("EnableSS", 0) == 1)
                                {
                                    flag23 = true;
                                }
                                if (PlayerPrefs.GetInt("showSSInGame", 0) == 1)
                                {
                                    flag24 = true;
                                }
                                bool flag25 = GUI.Toggle(new Rect(num7 + 588f, num8 + 81f, 40f, 20f), flag23, "On");
                                if (flag25 != flag23)
                                {
                                    if (flag25)
                                    {
                                        PlayerPrefs.SetInt("EnableSS", 1);
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetInt("EnableSS", 0);
                                    }
                                }
                                bool flag26 = GUI.Toggle(new Rect(num7 + 588f, num8 + 106f, 40f, 20f), flag24, "On");
                                if (flag24 != flag26)
                                {
                                    if (flag26)
                                    {
                                        PlayerPrefs.SetInt("showSSInGame", 1);
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetInt("showSSInGame", 0);
                                    }
                                }
                                GUI.Label(new Rect(num7 + 485f, num8 + 161f, 185f, 22f), "Other", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 186f, 80f, 20f), "Volume:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 211f, 95f, 20f), "Mouse Speed:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 236f, 95f, 20f), "Camera Dist:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 261f, 80f, 20f), "Camera Tilt:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 283f, 80f, 20f), "Invert Mouse:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 305f, 80f, 20f), "Speedometer:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 375f, 80f, 20f), "Minimap:", "Label");
                                GUI.Label(new Rect(num7 + 386f, num8 + 397f, 100f, 20f), "Game Feed:", "Label");
                                strArray16 = new string[] { "Off", "Speed", "Damage" };
                                settings[0xbd] = GUI.SelectionGrid(new Rect(num7 + 480f, num8 + 305f, 140f, 60f), (int)settings[0xbd], strArray16, 1, GUI.skin.toggle);
                                AudioListener.volume = GUI.HorizontalSlider(new Rect(num7 + 478f, num8 + 191f, 150f, 20f), AudioListener.volume, 0f, 1f);
                                this.mouseSlider = GUI.HorizontalSlider(new Rect(num7 + 478f, num8 + 216f, 150f, 20f), this.mouseSlider, 0.1f, 1f);
                                PlayerPrefs.SetFloat("MouseSensitivity", this.mouseSlider);
                                IN_GAME_MAIN_CAMERA.sensitivityMulti = PlayerPrefs.GetFloat("MouseSensitivity");
                                this.distanceSlider = GUI.HorizontalSlider(new Rect(num7 + 478f, num8 + 241f, 150f, 20f), this.distanceSlider, 0f, 1f);
                                PlayerPrefs.SetFloat("cameraDistance", this.distanceSlider);
                                IN_GAME_MAIN_CAMERA.cameraDistance = 0.3f + this.distanceSlider;
                                bool flag27 = false;
                                bool flag28 = false;
                                bool flag29 = false;
                                bool flag30 = false;
                                if (((int)settings[0xe7]) == 1)
                                {
                                    flag29 = true;
                                }
                                if (((int)settings[0xf4]) == 1)
                                {
                                    flag30 = true;
                                }
                                if (PlayerPrefs.HasKey("cameraTilt"))
                                {
                                    if (PlayerPrefs.GetInt("cameraTilt") == 1)
                                    {
                                        flag27 = true;
                                    }
                                }
                                else
                                {
                                    PlayerPrefs.SetInt("cameraTilt", 1);
                                }
                                if (PlayerPrefs.HasKey("invertMouseY"))
                                {
                                    if (PlayerPrefs.GetInt("invertMouseY") == -1)
                                    {
                                        flag28 = true;
                                    }
                                }
                                else
                                {
                                    PlayerPrefs.SetInt("invertMouseY", 1);
                                }
                                bool flag31 = GUI.Toggle(new Rect(num7 + 480f, num8 + 261f, 40f, 20f), flag27, "On");
                                if (flag27 != flag31)
                                {
                                    if (flag31)
                                    {
                                        PlayerPrefs.SetInt("cameraTilt", 1);
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetInt("cameraTilt", 0);
                                    }
                                }
                                bool flag32 = GUI.Toggle(new Rect(num7 + 480f, num8 + 283f, 40f, 20f), flag28, "On");
                                if (flag32 != flag28)
                                {
                                    if (flag32)
                                    {
                                        PlayerPrefs.SetInt("invertMouseY", -1);
                                    }
                                    else
                                    {
                                        PlayerPrefs.SetInt("invertMouseY", 1);
                                    }
                                }
                                bool flag33 = GUI.Toggle(new Rect(num7 + 480f, num8 + 375f, 40f, 20f), flag29, "On");
                                if (flag29 != flag33)
                                {
                                    if (flag33)
                                    {
                                        settings[0xe7] = 1;
                                    }
                                    else
                                    {
                                        settings[0xe7] = 0;
                                    }
                                }
                                bool flag34 = GUI.Toggle(new Rect(num7 + 480f, num8 + 397f, 40f, 20f), flag30, "On");
                                if (flag30 != flag34)
                                {
                                    if (flag34)
                                    {
                                        settings[0xf4] = 1;
                                    }
                                    else
                                    {
                                        settings[0xf4] = 0;
                                    }
                                }
                                IN_GAME_MAIN_CAMERA.cameraTilt = PlayerPrefs.GetInt("cameraTilt");
                                IN_GAME_MAIN_CAMERA.invertY = PlayerPrefs.GetInt("invertMouseY");
                            }
                            else if (((int)settings[0x40]) == 10)
                            {
                                bool flag35;
                                bool flag36;
                                GUI.Label(new Rect(num7 + 200f, num8 + 382f, 400f, 22f), "Master Client only. Changes will take effect upon restart.");
                                if (GUI.Button(new Rect(num7 + 267.5f, num8 + 50f, 60f, 25f), "Titans"))
                                {
                                    settings[230] = 0;
                                }
                                else if (GUI.Button(new Rect(num7 + 332.5f, num8 + 50f, 40f, 25f), "PVP"))
                                {
                                    settings[230] = 1;
                                }
                                else if (GUI.Button(new Rect(num7 + 377.5f, num8 + 50f, 50f, 25f), "Misc"))
                                {
                                    settings[230] = 2;
                                }
                                else if (GUI.Button(new Rect(num7 + 320f, num8 + 415f, 60f, 30f), "Reset"))
                                {
                                    settings[0xc0] = 0;
                                    settings[0xc1] = 0;
                                    settings[0xc2] = 0;
                                    settings[0xc3] = 0;
                                    settings[0xc4] = "30";
                                    settings[0xc5] = 0;
                                    settings[0xc6] = "100";
                                    settings[0xc7] = "200";
                                    settings[200] = 0;
                                    settings[0xc9] = "1";
                                    settings[0xca] = 0;
                                    settings[0xcb] = 0;
                                    settings[0xcc] = "1";
                                    settings[0xcd] = 0;
                                    settings[0xce] = "1000";
                                    settings[0xcf] = 0;
                                    settings[0xd0] = "1.0";
                                    settings[0xd1] = "3.0";
                                    settings[210] = 0;
                                    settings[0xd3] = "20.0";
                                    settings[0xd4] = "20.0";
                                    settings[0xd5] = "20.0";
                                    settings[0xd6] = "20.0";
                                    settings[0xd7] = "20.0";
                                    settings[0xd8] = 0;
                                    settings[0xd9] = 0;
                                    settings[0xda] = "1";
                                    settings[0xdb] = 0;
                                    settings[220] = 0;
                                    settings[0xdd] = 0;
                                    settings[0xde] = "20";
                                    settings[0xdf] = 0;
                                    settings[0xe0] = "10";
                                    settings[0xe1] = string.Empty;
                                    settings[0xe2] = 0;
                                    settings[0xe3] = "50";
                                    settings[0xe4] = 0;
                                    settings[0xe5] = 0;
                                    settings[0xeb] = 0;
                                }
                                if (((int)settings[230]) == 0)
                                {
                                    GUI.Label(new Rect(num7 + 100f, num8 + 90f, 160f, 22f), "Custom Titan Number:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 112f, 200f, 22f), "Amount (Integer):", "Label");
                                    settings[0xcc] = GUI.TextField(new Rect(num7 + 250f, num8 + 112f, 50f, 22f), (string)settings[0xcc]);
                                    flag35 = false;
                                    if (((int)settings[0xcb]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 90f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xcb] = 1;
                                        }
                                        else
                                        {
                                            settings[0xcb] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 152f, 160f, 22f), "Custom Titan Spawns:", "Label");
                                    flag35 = false;
                                    if (((int)settings[210]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 152f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[210] = 1;
                                        }
                                        else
                                        {
                                            settings[210] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 174f, 150f, 22f), "Normal (Decimal):", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 196f, 150f, 22f), "Aberrant (Decimal):", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 218f, 150f, 22f), "Jumper (Decimal):", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 240f, 150f, 22f), "Crawler (Decimal):", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 262f, 150f, 22f), "Punk (Decimal):", "Label");
                                    settings[0xd3] = GUI.TextField(new Rect(num7 + 250f, num8 + 174f, 50f, 22f), (string)settings[0xd3]);
                                    settings[0xd4] = GUI.TextField(new Rect(num7 + 250f, num8 + 196f, 50f, 22f), (string)settings[0xd4]);
                                    settings[0xd5] = GUI.TextField(new Rect(num7 + 250f, num8 + 218f, 50f, 22f), (string)settings[0xd5]);
                                    settings[0xd6] = GUI.TextField(new Rect(num7 + 250f, num8 + 240f, 50f, 22f), (string)settings[0xd6]);
                                    settings[0xd7] = GUI.TextField(new Rect(num7 + 250f, num8 + 262f, 50f, 22f), (string)settings[0xd7]);
                                    GUI.Label(new Rect(num7 + 100f, num8 + 302f, 160f, 22f), "Titan Size Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 324f, 150f, 22f), "Minimum (Decimal):", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 346f, 150f, 22f), "Maximum (Decimal):", "Label");
                                    settings[0xd0] = GUI.TextField(new Rect(num7 + 250f, num8 + 324f, 50f, 22f), (string)settings[0xd0]);
                                    settings[0xd1] = GUI.TextField(new Rect(num7 + 250f, num8 + 346f, 50f, 22f), (string)settings[0xd1]);
                                    flag35 = false;
                                    if (((int)settings[0xcf]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 302f, 40f, 20f), flag35, "On");
                                    if (flag36 != flag35)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xcf] = 1;
                                        }
                                        else
                                        {
                                            settings[0xcf] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 90f, 160f, 22f), "Titan Health Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 400f, num8 + 161f, 150f, 22f), "Minimum (Integer):", "Label");
                                    GUI.Label(new Rect(num7 + 400f, num8 + 183f, 150f, 22f), "Maximum (Integer):", "Label");
                                    settings[0xc6] = GUI.TextField(new Rect(num7 + 550f, num8 + 161f, 50f, 22f), (string)settings[0xc6]);
                                    settings[0xc7] = GUI.TextField(new Rect(num7 + 550f, num8 + 183f, 50f, 22f), (string)settings[0xc7]);
                                    strArray16 = new string[] { "Off", "Fixed", "Scaled" };
                                    settings[0xc5] = GUI.SelectionGrid(new Rect(num7 + 550f, num8 + 90f, 100f, 66f), (int)settings[0xc5], strArray16, 1, GUI.skin.toggle);
                                    GUI.Label(new Rect(num7 + 400f, num8 + 223f, 160f, 22f), "Titan Damage Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 400f, num8 + 245f, 150f, 22f), "Damage (Integer):", "Label");
                                    settings[0xce] = GUI.TextField(new Rect(num7 + 550f, num8 + 245f, 50f, 22f), (string)settings[0xce]);
                                    flag35 = false;
                                    if (((int)settings[0xcd]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 223f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xcd] = 1;
                                        }
                                        else
                                        {
                                            settings[0xcd] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 285f, 160f, 22f), "Titan Explode Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 400f, num8 + 307f, 160f, 22f), "Radius (Integer):", "Label");
                                    settings[0xc4] = GUI.TextField(new Rect(num7 + 550f, num8 + 307f, 50f, 22f), (string)settings[0xc4]);
                                    flag35 = false;
                                    if (((int)settings[0xc3]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 285f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xc3] = 1;
                                        }
                                        else
                                        {
                                            settings[0xc3] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 347f, 160f, 22f), "Disable Rock Throwing:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xc2]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 347f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xc2] = 1;
                                        }
                                        else
                                        {
                                            settings[0xc2] = 0;
                                        }
                                    }
                                }
                                else if (((int)settings[230]) == 1)
                                {
                                    GUI.Label(new Rect(num7 + 100f, num8 + 90f, 160f, 22f), "Point Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 112f, 160f, 22f), "Max Points (Integer):", "Label");
                                    settings[0xe3] = GUI.TextField(new Rect(num7 + 250f, num8 + 112f, 50f, 22f), (string)settings[0xe3]);
                                    flag35 = false;
                                    if (((int)settings[0xe2]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 90f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xe2] = 1;
                                        }
                                        else
                                        {
                                            settings[0xe2] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 152f, 160f, 22f), "PVP Bomb Mode:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xc0]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 152f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xc0] = 1;
                                        }
                                        else
                                        {
                                            settings[0xc0] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 182f, 100f, 66f), "Team Mode:", "Label");
                                    strArray16 = new string[] { "Off", "No Sort", "Size-Lock", "Skill-Lock" };
                                    settings[0xc1] = GUI.SelectionGrid(new Rect(num7 + 250f, num8 + 182f, 120f, 88f), (int)settings[0xc1], strArray16, 1, GUI.skin.toggle);
                                    GUI.Label(new Rect(num7 + 100f, num8 + 278f, 160f, 22f), "Infection Mode:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 300f, 160f, 22f), "Starting Titans (Integer):", "Label");
                                    settings[0xc9] = GUI.TextField(new Rect(num7 + 250f, num8 + 300f, 50f, 22f), (string)settings[0xc9]);
                                    flag35 = false;
                                    if (((int)settings[200]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 278f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[200] = 1;
                                        }
                                        else
                                        {
                                            settings[200] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 330f, 160f, 22f), "Friendly Mode:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xdb]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 330f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xdb] = 1;
                                        }
                                        else
                                        {
                                            settings[0xdb] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 90f, 160f, 22f), "Sword/AHSS PVP:", "Label");
                                    strArray16 = new string[] { "Off", "Teams", "FFA" };
                                    settings[220] = GUI.SelectionGrid(new Rect(num7 + 550f, num8 + 90f, 100f, 66f), (int)settings[220], strArray16, 1, GUI.skin.toggle);
                                    GUI.Label(new Rect(num7 + 400f, num8 + 164f, 160f, 22f), "No AHSS Air-Reloading:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xe4]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 164f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xe4] = 1;
                                        }
                                        else
                                        {
                                            settings[0xe4] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 194f, 160f, 22f), "Cannons kill humans:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0x105]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 194f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0x105] = 1;
                                        }
                                        else
                                        {
                                            settings[0x105] = 0;
                                        }
                                    }
                                }
                                else if (((int)settings[230]) == 2)
                                {
                                    GUI.Label(new Rect(num7 + 100f, num8 + 90f, 160f, 22f), "Custom Titans/Wave:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 112f, 160f, 22f), "Amount (Integer):", "Label");
                                    settings[0xda] = GUI.TextField(new Rect(num7 + 250f, num8 + 112f, 50f, 22f), (string)settings[0xda]);
                                    flag35 = false;
                                    if (((int)settings[0xd9]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 90f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xd9] = 1;
                                        }
                                        else
                                        {
                                            settings[0xd9] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 152f, 160f, 22f), "Maximum Waves:", "Label");
                                    GUI.Label(new Rect(num7 + 100f, num8 + 174f, 160f, 22f), "Amount (Integer):", "Label");
                                    settings[0xde] = GUI.TextField(new Rect(num7 + 250f, num8 + 174f, 50f, 22f), (string)settings[0xde]);
                                    flag35 = false;
                                    if (((int)settings[0xdd]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 152f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xdd] = 1;
                                        }
                                        else
                                        {
                                            settings[0xdd] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 214f, 160f, 22f), "Punks every 5 waves:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xe5]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 214f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xe5] = 1;
                                        }
                                        else
                                        {
                                            settings[0xe5] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 100f, num8 + 244f, 160f, 22f), "Global Minimap Disable:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xeb]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 250f, num8 + 244f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xeb] = 1;
                                        }
                                        else
                                        {
                                            settings[0xeb] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 90f, 160f, 22f), "Endless Respawn:", "Label");
                                    GUI.Label(new Rect(num7 + 400f, num8 + 112f, 160f, 22f), "Respawn Time (Integer):", "Label");
                                    settings[0xe0] = GUI.TextField(new Rect(num7 + 550f, num8 + 112f, 50f, 22f), (string)settings[0xe0]);
                                    flag35 = false;
                                    if (((int)settings[0xdf]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 90f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xdf] = 1;
                                        }
                                        else
                                        {
                                            settings[0xdf] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 152f, 160f, 22f), "Kick Eren Titan:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xca]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 152f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xca] = 1;
                                        }
                                        else
                                        {
                                            settings[0xca] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 182f, 160f, 22f), "Allow Horses:", "Label");
                                    flag35 = false;
                                    if (((int)settings[0xd8]) == 1)
                                    {
                                        flag35 = true;
                                    }
                                    flag36 = GUI.Toggle(new Rect(num7 + 550f, num8 + 182f, 40f, 20f), flag35, "On");
                                    if (flag35 != flag36)
                                    {
                                        if (flag36)
                                        {
                                            settings[0xd8] = 1;
                                        }
                                        else
                                        {
                                            settings[0xd8] = 0;
                                        }
                                    }
                                    GUI.Label(new Rect(num7 + 400f, num8 + 212f, 180f, 22f), "Message of the day:", "Label");
                                    settings[0xe1] = GUI.TextField(new Rect(num7 + 400f, num8 + 234f, 200f, 22f), (string)settings[0xe1]);
                                }
                            }
                            else if (((int)settings[0x40]) == 1)
                            {
                                List<string> list7;
                                float num48;
                                if (GUI.Button(new Rect(num7 + 233f, num8 + 51f, 55f, 25f), "Human"))
                                {
                                    settings[190] = 0;
                                }
                                else if (GUI.Button(new Rect(num7 + 293f, num8 + 51f, 52f, 25f), "Titan"))
                                {
                                    settings[190] = 1;
                                }
                                else if (GUI.Button(new Rect(num7 + 350f, num8 + 51f, 53f, 25f), "Horse"))
                                {
                                    settings[190] = 2;
                                }
                                else if (GUI.Button(new Rect(num7 + 408f, num8 + 51f, 59f, 25f), "Cannon"))
                                {
                                    settings[190] = 3;
                                }
                                if (((int)settings[190]) == 0)
                                {
                                    list7 = new List<string> {
                                        "Forward:", "Backward:", "Left:", "Right:", "Jump:", "Dodge:", "Left Hook:", "Right Hook:", "Both Hooks:", "Lock:", "Attack:", "Special:", "Salute:", "Change Camera:", "Reset:", "Pause:",
                                        "Show/Hide Cursor:", "Fullscreen:", "Change Blade:", "Flare Green:", "Flare Red:", "Flare Black:", "Reel in:", "Reel out:", "Gas Burst:", "Minimap Max:", "Minimap Toggle:", "Minimap Reset:", "Open Chat:", "Live Spectate"
                                     };
                                    for (num13 = 0; num13 < list7.Count; num13++)
                                    {
                                        num18 = num13;
                                        num48 = 80f;
                                        if (num18 > 14)
                                        {
                                            num48 = 390f;
                                            num18 -= 15;
                                        }
                                        GUI.Label(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 145f, 22f), list7[num13], "Label");
                                    }
                                    bool flag37 = false;
                                    if (((int)settings[0x61]) == 1)
                                    {
                                        flag37 = true;
                                    }
                                    bool flag38 = false;
                                    if (((int)settings[0x74]) == 1)
                                    {
                                        flag38 = true;
                                    }
                                    bool flag39 = false;
                                    if (((int)settings[0xb5]) == 1)
                                    {
                                        flag39 = true;
                                    }
                                    bool flag40 = GUI.Toggle(new Rect(num7 + 457f, num8 + 261f, 40f, 20f), flag37, "On");
                                    if (flag37 != flag40)
                                    {
                                        if (flag40)
                                        {
                                            settings[0x61] = 1;
                                        }
                                        else
                                        {
                                            settings[0x61] = 0;
                                        }
                                    }
                                    bool flag41 = GUI.Toggle(new Rect(num7 + 457f, num8 + 286f, 40f, 20f), flag38, "On");
                                    if (flag38 != flag41)
                                    {
                                        if (flag41)
                                        {
                                            settings[0x74] = 1;
                                        }
                                        else
                                        {
                                            settings[0x74] = 0;
                                        }
                                    }
                                    bool flag42 = GUI.Toggle(new Rect(num7 + 457f, num8 + 311f, 40f, 20f), flag39, "On");
                                    if (flag39 != flag42)
                                    {
                                        if (flag42)
                                        {
                                            settings[0xb5] = 1;
                                        }
                                        else
                                        {
                                            settings[0xb5] = 0;
                                        }
                                    }
                                    for (num13 = 0; num13 < 0x16; num13++)
                                    {
                                        num18 = num13;
                                        num48 = 190f;
                                        if (num18 > 14)
                                        {
                                            num48 = 500f;
                                            num18 -= 15;
                                        }
                                        if (GUI.Button(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 120f, 20f), FengCustomInputs.Inputs.getKeyRC(num13), "box"))
                                        {
                                            settings[100] = num13 + 1;
                                            FengCustomInputs.Inputs.setNameRC(num13, "waiting...");
                                        }
                                    }
                                    if (GUI.Button(new Rect(num7 + 500f, num8 + 261f, 120f, 20f), (string)settings[0x62], "box"))
                                    {
                                        settings[0x62] = "waiting...";
                                        settings[100] = 0x62;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 286f, 120f, 20f), (string)settings[0x63], "box"))
                                    {
                                        settings[0x63] = "waiting...";
                                        settings[100] = 0x63;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 311f, 120f, 20f), (string)settings[0xb6], "box"))
                                    {
                                        settings[0xb6] = "waiting...";
                                        settings[100] = 0xb6;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 336f, 120f, 20f), (string)settings[0xe8], "box"))
                                    {
                                        settings[0xe8] = "waiting...";
                                        settings[100] = 0xe8;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 361f, 120f, 20f), (string)settings[0xe9], "box"))
                                    {
                                        settings[0xe9] = "waiting...";
                                        settings[100] = 0xe9;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 386f, 120f, 20f), (string)settings[0xea], "box"))
                                    {
                                        settings[0xea] = "waiting...";
                                        settings[100] = 0xea;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 411f, 120f, 20f), (string)settings[0xec], "box"))
                                    {
                                        settings[0xec] = "waiting...";
                                        settings[100] = 0xec;
                                    }
                                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 436f, 120f, 20f), (string)settings[0x106], "box"))
                                    {
                                        settings[0x106] = "waiting...";
                                        settings[100] = 0x106;
                                    }
                                    if (((int)settings[100]) != 0)
                                    {
                                        current = Event.current;
                                        flag4 = false;
                                        str4 = "waiting...";
                                        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                                        {
                                            flag4 = true;
                                            str4 = current.keyCode.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.LeftShift.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.RightShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.RightShift.ToString();
                                        }
                                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                                        {
                                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Up";
                                            }
                                            else
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Down";
                                            }
                                        }
                                        else
                                        {
                                            num13 = 0;
                                            while (num13 < 7)
                                            {
                                                if (Input.GetKeyDown((KeyCode)(0x143 + num13)))
                                                {
                                                    flag4 = true;
                                                    str4 = "Mouse" + Convert.ToString(num13);
                                                }
                                                num13++;
                                            }
                                        }
                                        if (flag4)
                                        {
                                            if (((int)settings[100]) == 0x62)
                                            {
                                                settings[0x62] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.reelin, str4);
                                            }
                                            else if (((int)settings[100]) == 0x63)
                                            {
                                                settings[0x63] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.reelout, str4);
                                            }
                                            else if (((int)settings[100]) == 0xb6)
                                            {
                                                settings[0xb6] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.dash, str4);
                                            }
                                            else if (((int)settings[100]) == 0xe8)
                                            {
                                                settings[0xe8] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.mapMaximize, str4);
                                            }
                                            else if (((int)settings[100]) == 0xe9)
                                            {
                                                settings[0xe9] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.mapToggle, str4);
                                            }
                                            else if (((int)settings[100]) == 0xea)
                                            {
                                                settings[0xea] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.mapReset, str4);
                                            }
                                            else if (((int)settings[100]) == 0xec)
                                            {
                                                settings[0xec] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.chat, str4);
                                            }
                                            else if (((int)settings[100]) == 0x106)
                                            {
                                                settings[0x106] = str4;
                                                settings[100] = 0;
                                                inputRC.setInputHuman(InputCodeRC.liveCam, str4);
                                            }
                                            else
                                            {
                                                for (num13 = 0; num13 < 0x16; num13++)
                                                {
                                                    num23 = num13 + 1;
                                                    if (((int)settings[100]) == num23)
                                                    {
                                                        FengCustomInputs.Inputs.setKeyRC(num13, str4);
                                                        settings[100] = 0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (((int)settings[190]) == 1)
                                {
                                    list7 = new List<string> { "Forward:", "Back:", "Left:", "Right:", "Walk:", "Jump:", "Punch:", "Slam:", "Grab (front):", "Grab (back):", "Grab (nape):", "Slap:", "Bite:", "Cover Nape:" };
                                    for (num13 = 0; num13 < list7.Count; num13++)
                                    {
                                        num18 = num13;
                                        num48 = 80f;
                                        if (num18 > 6)
                                        {
                                            num48 = 390f;
                                            num18 -= 7;
                                        }
                                        GUI.Label(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 145f, 22f), list7[num13], "Label");
                                    }
                                    for (num13 = 0; num13 < 14; num13++)
                                    {
                                        num23 = 0x65 + num13;
                                        num18 = num13;
                                        num48 = 190f;
                                        if (num18 > 6)
                                        {
                                            num48 = 500f;
                                            num18 -= 7;
                                        }
                                        if (GUI.Button(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 120f, 20f), (string)settings[num23], "box"))
                                        {
                                            settings[num23] = "waiting...";
                                            settings[100] = num23;
                                        }
                                    }
                                    if (((int)settings[100]) != 0)
                                    {
                                        current = Event.current;
                                        flag4 = false;
                                        str4 = "waiting...";
                                        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                                        {
                                            flag4 = true;
                                            str4 = current.keyCode.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.LeftShift.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.RightShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.RightShift.ToString();
                                        }
                                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                                        {
                                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Up";
                                            }
                                            else
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Down";
                                            }
                                        }
                                        else
                                        {
                                            num13 = 0;
                                            while (num13 < 7)
                                            {
                                                if (Input.GetKeyDown((KeyCode)(0x143 + num13)))
                                                {
                                                    flag4 = true;
                                                    str4 = "Mouse" + Convert.ToString(num13);
                                                }
                                                num13++;
                                            }
                                        }
                                        if (flag4)
                                        {
                                            for (num13 = 0; num13 < 14; num13++)
                                            {
                                                num23 = 0x65 + num13;
                                                if (((int)settings[100]) == num23)
                                                {
                                                    settings[num23] = str4;
                                                    settings[100] = 0;
                                                    inputRC.setInputTitan(num13, str4);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (((int)settings[190]) == 2)
                                {
                                    list7 = new List<string> { "Forward:", "Back:", "Left:", "Right:", "Walk:", "Jump:", "Mount:" };
                                    for (num13 = 0; num13 < list7.Count; num13++)
                                    {
                                        num18 = num13;
                                        num48 = 80f;
                                        if (num18 > 3)
                                        {
                                            num48 = 390f;
                                            num18 -= 4;
                                        }
                                        GUI.Label(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 145f, 22f), list7[num13], "Label");
                                    }
                                    for (num13 = 0; num13 < 7; num13++)
                                    {
                                        num23 = 0xed + num13;
                                        num18 = num13;
                                        num48 = 190f;
                                        if (num18 > 3)
                                        {
                                            num48 = 500f;
                                            num18 -= 4;
                                        }
                                        if (GUI.Button(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 120f, 20f), (string)settings[num23], "box"))
                                        {
                                            settings[num23] = "waiting...";
                                            settings[100] = num23;
                                        }
                                    }
                                    if (((int)settings[100]) != 0)
                                    {
                                        current = Event.current;
                                        flag4 = false;
                                        str4 = "waiting...";
                                        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                                        {
                                            flag4 = true;
                                            str4 = current.keyCode.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.LeftShift.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.RightShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.RightShift.ToString();
                                        }
                                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                                        {
                                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Up";
                                            }
                                            else
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Down";
                                            }
                                        }
                                        else
                                        {
                                            num13 = 0;
                                            while (num13 < 7)
                                            {
                                                if (Input.GetKeyDown((KeyCode)(0x143 + num13)))
                                                {
                                                    flag4 = true;
                                                    str4 = "Mouse" + Convert.ToString(num13);
                                                }
                                                num13++;
                                            }
                                        }
                                        if (flag4)
                                        {
                                            for (num13 = 0; num13 < 7; num13++)
                                            {
                                                num23 = 0xed + num13;
                                                if (((int)settings[100]) == num23)
                                                {
                                                    settings[num23] = str4;
                                                    settings[100] = 0;
                                                    inputRC.setInputHorse(num13, str4);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (((int)settings[190]) == 3)
                                {
                                    list7 = new List<string> { "Rotate Up:", "Rotate Down:", "Rotate Left:", "Rotate Right:", "Fire:", "Mount:", "Slow Rotate:" };
                                    for (num13 = 0; num13 < list7.Count; num13++)
                                    {
                                        num18 = num13;
                                        num48 = 80f;
                                        if (num18 > 3)
                                        {
                                            num48 = 390f;
                                            num18 -= 4;
                                        }
                                        GUI.Label(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 145f, 22f), list7[num13], "Label");
                                    }
                                    for (num13 = 0; num13 < 7; num13++)
                                    {
                                        num23 = 0xfe + num13;
                                        num18 = num13;
                                        num48 = 190f;
                                        if (num18 > 3)
                                        {
                                            num48 = 500f;
                                            num18 -= 4;
                                        }
                                        if (GUI.Button(new Rect(num7 + num48, (num8 + 86f) + (num18 * 25f), 120f, 20f), (string)settings[num23], "box"))
                                        {
                                            settings[num23] = "waiting...";
                                            settings[100] = num23;
                                        }
                                    }
                                    if (((int)settings[100]) != 0)
                                    {
                                        current = Event.current;
                                        flag4 = false;
                                        str4 = "waiting...";
                                        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
                                        {
                                            flag4 = true;
                                            str4 = current.keyCode.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.LeftShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.LeftShift.ToString();
                                        }
                                        else if (Input.GetKey(KeyCode.RightShift))
                                        {
                                            flag4 = true;
                                            str4 = KeyCode.RightShift.ToString();
                                        }
                                        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                                        {
                                            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Up";
                                            }
                                            else
                                            {
                                                flag4 = true;
                                                str4 = "Scroll Down";
                                            }
                                        }
                                        else
                                        {
                                            num13 = 0;
                                            while (num13 < 6)
                                            {
                                                if (Input.GetKeyDown((KeyCode)(0x143 + num13)))
                                                {
                                                    flag4 = true;
                                                    str4 = "Mouse" + Convert.ToString(num13);
                                                }
                                                num13++;
                                            }
                                        }
                                        if (flag4)
                                        {
                                            for (num13 = 0; num13 < 7; num13++)
                                            {
                                                num23 = 0xfe + num13;
                                                if (((int)settings[100]) == num23)
                                                {
                                                    settings[num23] = str4;
                                                    settings[100] = 0;
                                                    inputRC.setInputCannon(num13, str4);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else if (((int)settings[0x40]) == 8)
                            {
                                GUI.Label(new Rect(num7 + 150f, num8 + 51f, 120f, 22f), "Map Settings", "Label");
                                GUI.Label(new Rect(num7 + 50f, num8 + 81f, 140f, 20f), "Titan Spawn Cap:", "Label");
                                settings[0x55] = GUI.TextField(new Rect(num7 + 155f, num8 + 81f, 30f, 20f), (string)settings[0x55]);
                                strArray16 = new string[] { "1 Round", "Waves", "PVP", "Racing", "Custom" };
                                GameSettings.gameType = GUI.SelectionGrid(new Rect(num7 + 190f, num8 + 80f, 140f, 60f), GameSettings.gameType, strArray16, 2, GUI.skin.toggle);
                                GUI.Label(new Rect(num7 + 150f, num8 + 155f, 150f, 20f), "Level Script:", "Label");
                                currentScript = GUI.TextField(new Rect(num7 + 50f, num8 + 180f, 275f, 220f), currentScript);
                                if (GUI.Button(new Rect(num7 + 100f, num8 + 410f, 50f, 25f), "Copy"))
                                {
                                    editor = new TextEditor
                                    {
                                        content = new GUIContent(currentScript)
                                    };
                                    editor.SelectAll();
                                    editor.Copy();
                                }
                                else if (GUI.Button(new Rect(num7 + 225f, num8 + 410f, 50f, 25f), "Clear"))
                                {
                                    currentScript = string.Empty;
                                }
                                GUI.Label(new Rect(num7 + 455f, num8 + 51f, 180f, 20f), "Custom Textures", "Label");
                                GUI.Label(new Rect(num7 + 375f, num8 + 81f, 180f, 20f), "Ground Skin:", "Label");
                                settings[0xa2] = GUI.TextField(new Rect(num7 + 375f, num8 + 103f, 275f, 20f), (string)settings[0xa2]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 125f, 150f, 20f), "Skybox Front:", "Label");
                                settings[0xaf] = GUI.TextField(new Rect(num7 + 375f, num8 + 147f, 275f, 20f), (string)settings[0xaf]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 169f, 150f, 20f), "Skybox Back:", "Label");
                                settings[0xb0] = GUI.TextField(new Rect(num7 + 375f, num8 + 191f, 275f, 20f), (string)settings[0xb0]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 213f, 150f, 20f), "Skybox Left:", "Label");
                                settings[0xb1] = GUI.TextField(new Rect(num7 + 375f, num8 + 235f, 275f, 20f), (string)settings[0xb1]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 257f, 150f, 20f), "Skybox Right:", "Label");
                                settings[0xb2] = GUI.TextField(new Rect(num7 + 375f, num8 + 279f, 275f, 20f), (string)settings[0xb2]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 301f, 150f, 20f), "Skybox Up:", "Label");
                                settings[0xb3] = GUI.TextField(new Rect(num7 + 375f, num8 + 323f, 275f, 20f), (string)settings[0xb3]);
                                GUI.Label(new Rect(num7 + 375f, num8 + 345f, 150f, 20f), "Skybox Down:", "Label");
                                settings[180] = GUI.TextField(new Rect(num7 + 375f, num8 + 367f, 275f, 20f), (string)settings[180]);
                            }
                        }
                    }
                    if (GUI.Button(new Rect(num7 + 408f, num8 + 465f, 42f, 25f), "Save"))
                    {
                        PlayerPrefs.SetInt("human", (int)settings[0]);
                        PlayerPrefs.SetInt("titan", (int)settings[1]);
                        PlayerPrefs.SetInt("level", (int)settings[2]);
                        PlayerPrefs.SetString("horse", (string)settings[3]);
                        PlayerPrefs.SetString("hair", (string)settings[4]);
                        PlayerPrefs.SetString("eye", (string)settings[5]);
                        PlayerPrefs.SetString("glass", (string)settings[6]);
                        PlayerPrefs.SetString("face", (string)settings[7]);
                        PlayerPrefs.SetString("skin", (string)settings[8]);
                        PlayerPrefs.SetString("costume", (string)settings[9]);
                        PlayerPrefs.SetString("logo", (string)settings[10]);
                        PlayerPrefs.SetString("bladel", (string)settings[11]);
                        PlayerPrefs.SetString("blader", (string)settings[12]);
                        PlayerPrefs.SetString("gas", (string)settings[13]);
                        PlayerPrefs.SetString("haircolor", (string)settings[14]);
                        PlayerPrefs.SetInt("gasenable", (int)settings[15]);
                        PlayerPrefs.SetInt("titantype1", (int)settings[0x10]);
                        PlayerPrefs.SetInt("titantype2", (int)settings[0x11]);
                        PlayerPrefs.SetInt("titantype3", (int)settings[0x12]);
                        PlayerPrefs.SetInt("titantype4", (int)settings[0x13]);
                        PlayerPrefs.SetInt("titantype5", (int)settings[20]);
                        PlayerPrefs.SetString("titanhair1", (string)settings[0x15]);
                        PlayerPrefs.SetString("titanhair2", (string)settings[0x16]);
                        PlayerPrefs.SetString("titanhair3", (string)settings[0x17]);
                        PlayerPrefs.SetString("titanhair4", (string)settings[0x18]);
                        PlayerPrefs.SetString("titanhair5", (string)settings[0x19]);
                        PlayerPrefs.SetString("titaneye1", (string)settings[0x1a]);
                        PlayerPrefs.SetString("titaneye2", (string)settings[0x1b]);
                        PlayerPrefs.SetString("titaneye3", (string)settings[0x1c]);
                        PlayerPrefs.SetString("titaneye4", (string)settings[0x1d]);
                        PlayerPrefs.SetString("titaneye5", (string)settings[30]);
                        PlayerPrefs.SetInt("titanR", (int)settings[0x20]);
                        PlayerPrefs.SetString("tree1", (string)settings[0x21]);
                        PlayerPrefs.SetString("tree2", (string)settings[0x22]);
                        PlayerPrefs.SetString("tree3", (string)settings[0x23]);
                        PlayerPrefs.SetString("tree4", (string)settings[0x24]);
                        PlayerPrefs.SetString("tree5", (string)settings[0x25]);
                        PlayerPrefs.SetString("tree6", (string)settings[0x26]);
                        PlayerPrefs.SetString("tree7", (string)settings[0x27]);
                        PlayerPrefs.SetString("tree8", (string)settings[40]);
                        PlayerPrefs.SetString("leaf1", (string)settings[0x29]);
                        PlayerPrefs.SetString("leaf2", (string)settings[0x2a]);
                        PlayerPrefs.SetString("leaf3", (string)settings[0x2b]);
                        PlayerPrefs.SetString("leaf4", (string)settings[0x2c]);
                        PlayerPrefs.SetString("leaf5", (string)settings[0x2d]);
                        PlayerPrefs.SetString("leaf6", (string)settings[0x2e]);
                        PlayerPrefs.SetString("leaf7", (string)settings[0x2f]);
                        PlayerPrefs.SetString("leaf8", (string)settings[0x30]);
                        PlayerPrefs.SetString("forestG", (string)settings[0x31]);
                        PlayerPrefs.SetInt("forestR", (int)settings[50]);
                        PlayerPrefs.SetString("house1", (string)settings[0x33]);
                        PlayerPrefs.SetString("house2", (string)settings[0x34]);
                        PlayerPrefs.SetString("house3", (string)settings[0x35]);
                        PlayerPrefs.SetString("house4", (string)settings[0x36]);
                        PlayerPrefs.SetString("house5", (string)settings[0x37]);
                        PlayerPrefs.SetString("house6", (string)settings[0x38]);
                        PlayerPrefs.SetString("house7", (string)settings[0x39]);
                        PlayerPrefs.SetString("house8", (string)settings[0x3a]);
                        PlayerPrefs.SetString("cityG", (string)settings[0x3b]);
                        PlayerPrefs.SetString("cityW", (string)settings[60]);
                        PlayerPrefs.SetString("cityH", (string)settings[0x3d]);
                        PlayerPrefs.SetInt("skinQ", QualitySettings.masterTextureLimit);
                        PlayerPrefs.SetInt("skinQL", (int)settings[0x3f]);
                        PlayerPrefs.SetString("eren", (string)settings[0x41]);
                        PlayerPrefs.SetString("annie", (string)settings[0x42]);
                        PlayerPrefs.SetString("colossal", (string)settings[0x43]);
                        PlayerPrefs.SetString("hoodie", (string)settings[14]);
                        PlayerPrefs.SetString("cnumber", (string)settings[0x52]);
                        PlayerPrefs.SetString("cmax", (string)settings[0x55]);
                        PlayerPrefs.SetString("titanbody1", (string)settings[0x56]);
                        PlayerPrefs.SetString("titanbody2", (string)settings[0x57]);
                        PlayerPrefs.SetString("titanbody3", (string)settings[0x58]);
                        PlayerPrefs.SetString("titanbody4", (string)settings[0x59]);
                        PlayerPrefs.SetString("titanbody5", (string)settings[90]);
                        PlayerPrefs.SetInt("customlevel", (int)settings[0x5b]);
                        PlayerPrefs.SetInt("traildisable", (int)settings[0x5c]);
                        PlayerPrefs.SetInt("wind", (int)settings[0x5d]);
                        PlayerPrefs.SetString("trailskin", (string)settings[0x5e]);
                        PlayerPrefs.SetString("snapshot", (string)settings[0x5f]);
                        PlayerPrefs.SetString("trailskin2", (string)settings[0x60]);
                        PlayerPrefs.SetInt("reel", (int)settings[0x61]);
                        PlayerPrefs.SetString("reelin", (string)settings[0x62]);
                        PlayerPrefs.SetString("reelout", (string)settings[0x63]);
                        PlayerPrefs.SetFloat("vol", AudioListener.volume);
                        PlayerPrefs.SetString("tforward", (string)settings[0x65]);
                        PlayerPrefs.SetString("tback", (string)settings[0x66]);
                        PlayerPrefs.SetString("tleft", (string)settings[0x67]);
                        PlayerPrefs.SetString("tright", (string)settings[0x68]);
                        PlayerPrefs.SetString("twalk", (string)settings[0x69]);
                        PlayerPrefs.SetString("tjump", (string)settings[0x6a]);
                        PlayerPrefs.SetString("tpunch", (string)settings[0x6b]);
                        PlayerPrefs.SetString("tslam", (string)settings[0x6c]);
                        PlayerPrefs.SetString("tgrabfront", (string)settings[0x6d]);
                        PlayerPrefs.SetString("tgrabback", (string)settings[110]);
                        PlayerPrefs.SetString("tgrabnape", (string)settings[0x6f]);
                        PlayerPrefs.SetString("tantiae", (string)settings[0x70]);
                        PlayerPrefs.SetString("tbite", (string)settings[0x71]);
                        PlayerPrefs.SetString("tcover", (string)settings[0x72]);
                        PlayerPrefs.SetString("tsit", (string)settings[0x73]);
                        PlayerPrefs.SetInt("reel2", (int)settings[0x74]);
                        PlayerPrefs.SetInt("humangui", (int)settings[0x85]);
                        PlayerPrefs.SetString("horse2", (string)settings[0x86]);
                        PlayerPrefs.SetString("hair2", (string)settings[0x87]);
                        PlayerPrefs.SetString("eye2", (string)settings[0x88]);
                        PlayerPrefs.SetString("glass2", (string)settings[0x89]);
                        PlayerPrefs.SetString("face2", (string)settings[0x8a]);
                        PlayerPrefs.SetString("skin2", (string)settings[0x8b]);
                        PlayerPrefs.SetString("costume2", (string)settings[140]);
                        PlayerPrefs.SetString("logo2", (string)settings[0x8d]);
                        PlayerPrefs.SetString("bladel2", (string)settings[0x8e]);
                        PlayerPrefs.SetString("blader2", (string)settings[0x8f]);
                        PlayerPrefs.SetString("gas2", (string)settings[0x90]);
                        PlayerPrefs.SetString("hoodie2", (string)settings[0x91]);
                        PlayerPrefs.SetString("trail2", (string)settings[0x92]);
                        PlayerPrefs.SetString("horse3", (string)settings[0x93]);
                        PlayerPrefs.SetString("hair3", (string)settings[0x94]);
                        PlayerPrefs.SetString("eye3", (string)settings[0x95]);
                        PlayerPrefs.SetString("glass3", (string)settings[150]);
                        PlayerPrefs.SetString("face3", (string)settings[0x97]);
                        PlayerPrefs.SetString("skin3", (string)settings[0x98]);
                        PlayerPrefs.SetString("costume3", (string)settings[0x99]);
                        PlayerPrefs.SetString("logo3", (string)settings[0x9a]);
                        PlayerPrefs.SetString("bladel3", (string)settings[0x9b]);
                        PlayerPrefs.SetString("blader3", (string)settings[0x9c]);
                        PlayerPrefs.SetString("gas3", (string)settings[0x9d]);
                        PlayerPrefs.SetString("hoodie3", (string)settings[0x9e]);
                        PlayerPrefs.SetString("trail3", (string)settings[0x9f]);
                        PlayerPrefs.SetString("customGround", (string)settings[0xa2]);
                        PlayerPrefs.SetString("forestskyfront", (string)settings[0xa3]);
                        PlayerPrefs.SetString("forestskyback", (string)settings[0xa4]);
                        PlayerPrefs.SetString("forestskyleft", (string)settings[0xa5]);
                        PlayerPrefs.SetString("forestskyright", (string)settings[0xa6]);
                        PlayerPrefs.SetString("forestskyup", (string)settings[0xa7]);
                        PlayerPrefs.SetString("forestskydown", (string)settings[0xa8]);
                        PlayerPrefs.SetString("cityskyfront", (string)settings[0xa9]);
                        PlayerPrefs.SetString("cityskyback", (string)settings[170]);
                        PlayerPrefs.SetString("cityskyleft", (string)settings[0xab]);
                        PlayerPrefs.SetString("cityskyright", (string)settings[0xac]);
                        PlayerPrefs.SetString("cityskyup", (string)settings[0xad]);
                        PlayerPrefs.SetString("cityskydown", (string)settings[0xae]);
                        PlayerPrefs.SetString("customskyfront", (string)settings[0xaf]);
                        PlayerPrefs.SetString("customskyback", (string)settings[0xb0]);
                        PlayerPrefs.SetString("customskyleft", (string)settings[0xb1]);
                        PlayerPrefs.SetString("customskyright", (string)settings[0xb2]);
                        PlayerPrefs.SetString("customskyup", (string)settings[0xb3]);
                        PlayerPrefs.SetString("customskydown", (string)settings[180]);
                        PlayerPrefs.SetInt("dashenable", (int)settings[0xb5]);
                        PlayerPrefs.SetString("dashkey", (string)settings[0xb6]);
                        PlayerPrefs.SetInt("vsync", (int)settings[0xb7]);
                        PlayerPrefs.SetString("fpscap", (string)settings[0xb8]);
                        PlayerPrefs.SetInt("speedometer", (int)settings[0xbd]);
                        PlayerPrefs.SetInt("bombMode", (int)settings[0xc0]);
                        PlayerPrefs.SetInt("teamMode", (int)settings[0xc1]);
                        PlayerPrefs.SetInt("rockThrow", (int)settings[0xc2]);
                        PlayerPrefs.SetInt("explodeModeOn", (int)settings[0xc3]);
                        PlayerPrefs.SetString("explodeModeNum", (string)settings[0xc4]);
                        PlayerPrefs.SetInt("healthMode", (int)settings[0xc5]);
                        PlayerPrefs.SetString("healthLower", (string)settings[0xc6]);
                        PlayerPrefs.SetString("healthUpper", (string)settings[0xc7]);
                        PlayerPrefs.SetInt("infectionModeOn", (int)settings[200]);
                        PlayerPrefs.SetString("infectionModeNum", (string)settings[0xc9]);
                        PlayerPrefs.SetInt("banEren", (int)settings[0xca]);
                        PlayerPrefs.SetInt("moreTitanOn", (int)settings[0xcb]);
                        PlayerPrefs.SetString("moreTitanNum", (string)settings[0xcc]);
                        PlayerPrefs.SetInt("damageModeOn", (int)settings[0xcd]);
                        PlayerPrefs.SetString("damageModeNum", (string)settings[0xce]);
                        PlayerPrefs.SetInt("sizeMode", (int)settings[0xcf]);
                        PlayerPrefs.SetString("sizeLower", (string)settings[0xd0]);
                        PlayerPrefs.SetString("sizeUpper", (string)settings[0xd1]);
                        PlayerPrefs.SetInt("spawnModeOn", (int)settings[210]);
                        PlayerPrefs.SetString("nRate", (string)settings[0xd3]);
                        PlayerPrefs.SetString("aRate", (string)settings[0xd4]);
                        PlayerPrefs.SetString("jRate", (string)settings[0xd5]);
                        PlayerPrefs.SetString("cRate", (string)settings[0xd6]);
                        PlayerPrefs.SetString("pRate", (string)settings[0xd7]);
                        PlayerPrefs.SetInt("horseMode", (int)settings[0xd8]);
                        PlayerPrefs.SetInt("waveModeOn", (int)settings[0xd9]);
                        PlayerPrefs.SetString("waveModeNum", (string)settings[0xda]);
                        PlayerPrefs.SetInt("friendlyMode", (int)settings[0xdb]);
                        PlayerPrefs.SetInt("pvpMode", (int)settings[220]);
                        PlayerPrefs.SetInt("maxWaveOn", (int)settings[0xdd]);
                        PlayerPrefs.SetString("maxWaveNum", (string)settings[0xde]);
                        PlayerPrefs.SetInt("endlessModeOn", (int)settings[0xdf]);
                        PlayerPrefs.SetString("endlessModeNum", (string)settings[0xe0]);
                        PlayerPrefs.SetString("motd", (string)settings[0xe1]);
                        PlayerPrefs.SetInt("pointModeOn", (int)settings[0xe2]);
                        PlayerPrefs.SetString("pointModeNum", (string)settings[0xe3]);
                        PlayerPrefs.SetInt("ahssReload", (int)settings[0xe4]);
                        PlayerPrefs.SetInt("punkWaves", (int)settings[0xe5]);
                        PlayerPrefs.SetInt("mapOn", (int)settings[0xe7]);
                        PlayerPrefs.SetString("mapMaximize", (string)settings[0xe8]);
                        PlayerPrefs.SetString("mapToggle", (string)settings[0xe9]);
                        PlayerPrefs.SetString("mapReset", (string)settings[0xea]);
                        PlayerPrefs.SetInt("globalDisableMinimap", (int)settings[0xeb]);
                        PlayerPrefs.SetString("chatRebind", (string)settings[0xec]);
                        PlayerPrefs.SetString("hforward", (string)settings[0xed]);
                        PlayerPrefs.SetString("hback", (string)settings[0xee]);
                        PlayerPrefs.SetString("hleft", (string)settings[0xef]);
                        PlayerPrefs.SetString("hright", (string)settings[240]);
                        PlayerPrefs.SetString("hwalk", (string)settings[0xf1]);
                        PlayerPrefs.SetString("hjump", (string)settings[0xf2]);
                        PlayerPrefs.SetString("hmount", (string)settings[0xf3]);
                        PlayerPrefs.SetInt("chatfeed", (int)settings[0xf4]);
                        PlayerPrefs.SetFloat("bombR", (float)settings[0xf6]);
                        PlayerPrefs.SetFloat("bombG", (float)settings[0xf7]);
                        PlayerPrefs.SetFloat("bombB", (float)settings[0xf8]);
                        PlayerPrefs.SetFloat("bombA", (float)settings[0xf9]);
                        PlayerPrefs.SetInt("bombRadius", (int)settings[250]);
                        PlayerPrefs.SetInt("bombRange", (int)settings[0xfb]);
                        PlayerPrefs.SetInt("bombSpeed", (int)settings[0xfc]);
                        PlayerPrefs.SetInt("bombCD", (int)settings[0xfd]);
                        PlayerPrefs.SetString("cannonUp", (string)settings[0xfe]);
                        PlayerPrefs.SetString("cannonDown", (string)settings[0xff]);
                        PlayerPrefs.SetString("cannonLeft", (string)settings[0x100]);
                        PlayerPrefs.SetString("cannonRight", (string)settings[0x101]);
                        PlayerPrefs.SetString("cannonFire", (string)settings[0x102]);
                        PlayerPrefs.SetString("cannonMount", (string)settings[0x103]);
                        PlayerPrefs.SetString("cannonSlow", (string)settings[260]);
                        PlayerPrefs.SetInt("deadlyCannon", (int)settings[0x105]);
                        PlayerPrefs.SetString("liveCam", (string)settings[0x106]);
                        settings[0x40] = 4;
                        //PlayerPrefs.SetString("AnimatedName", (string)FengGameManagerMKII.settings[265]/* + (string)FengGameManagerMKII.settings[266]*/);
                        //FengGameManagerMKII.settings[267] = PlayerPrefs.GetString("Faded", "None");
                        //FengGameManagerMKII.settings[268] = PlayerPrefs.GetString("Linear", "None");
                        //FengGameManagerMKII.settings[269] = PlayerPrefs.GetString("Rebound", "None");
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[267]);
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[268]);
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[269]);
                        //FengGameManagerMKII.settings[270] = PlayerPrefs.GetString("AnimatedName", "None");
                        //StatsTab.AddLine("Save configurate... FengGameManagerMKII.settings[265] = " + FengGameManagerMKII.settings[265].ToString() + "\n FengGameManagerMKII.settings[266] = " + FengGameManagerMKII.settings[266].ToString(), StatsTab.DebugType.LOG);
                        //if (PhotonNetwork.connectionStateDetailed == PeerStates.Joined)
                        //{
                        //    this.StartNameAnim((string)FengGameManagerMKII.settings[270] != "None");
                        //}

                    }
                    else if (GUI.Button(new Rect(num7 + 455f, num8 + 465f, 40f, 25f), "Load"))
                    {
                        this.loadconfig();
                        settings[0x40] = 5;
                    }
                    else if (GUI.Button(new Rect(num7 + 500f, num8 + 465f, 60f, 25f), "Default"))
                    {
                        FengCustomInputs.Inputs.setToDefault();
                    }
                    else if (GUI.Button(new Rect(num7 + 565f, num8 + 465f, 75f, 25f), "Continue"))
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            Time.timeScale = 1f;
                        }
                        if (!Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().enabled)
                        {
                            Screen.showCursor = true;
                            Screen.lockCursor = true;
                            FengCustomInputs.Inputs.menuOn = false;
                            IN_GAME_MAIN_CAMERA.spectate.disable = false;
                            IN_GAME_MAIN_CAMERA.mouselook.disable = false;
                        }
                        else
                        {
                            IN_GAME_MAIN_CAMERA.isPausing = false;
                            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS)
                            {
                                Screen.showCursor = false;
                                Screen.lockCursor = true;
                            }
                            else
                            {
                                Screen.showCursor = false;
                                Screen.lockCursor = false;
                            }
                            FengCustomInputs.Inputs.menuOn = false;
                            FengCustomInputs.Inputs.justUPDATEME();
                        }
                    }
                    else if (GUI.Button(new Rect(num7 + 645f, num8 + 465f, 40f, 25f), "Quit"))
                    {
                        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            Time.timeScale = 1f;
                        }
                        else
                        {
                            PhotonNetwork.Disconnect();
                        }
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                        gameStart = false;
                        BRM.CacheGameObject.Find<FengCustomInputs>("InputManagerController").menuOn = false;
                        this.DestroyAllExistingCloths();
                        UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("MultiplayerManager"));
                        Application.LoadLevel("menu");
                    }
                }
            }
            else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
            {
                if (Time.timeScale <= 0.1f)
                {
                    num7 = ((float)Screen.width) / 2f;
                    num8 = ((float)Screen.height) / 2f;
                    GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
                    GUI.DrawTexture(new Rect(num7 - 98f, num8 - 48f, 196f, 96f), this.textureBackgroundBlue);
                    GUI.Box(new Rect(num7 - 100f, num8 - 50f, 200f, 100f), string.Empty);
                    if (this.pauseWaitTime <= 3f)
                    {
                        GUI.Label(new Rect(num7 - 43f, num8 - 15f, 200f, 22f), "Unpausing in:");
                        GUI.Label(new Rect(num7 - 8f, num8 + 5f, 200f, 22f), this.pauseWaitTime.ToString("F1"));
                    }
                    else
                    {
                        GUI.Label(new Rect(num7 - 43f, num8 - 10f, 200f, 22f), "Game Paused.");
                    }
                }
                else if (!logicLoaded || !customLevelLoaded)
                {
                    num7 = ((float)Screen.width) / 2f;
                    num8 = ((float)Screen.height) / 2f;
                    GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
                    GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.textureBackgroundBlack);
                    GUI.DrawTexture(new Rect(num7 - 98f, num8 - 48f, 196f, 146f), this.textureBackgroundBlue);
                    GUI.Box(new Rect(num7 - 100f, num8 - 50f, 200f, 150f), string.Empty);
                    int length = RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.currentLevel]).Length;
                    int num50 = RCextensions.returnStringFromObject(PhotonNetwork.masterClient.customProperties[PhotonPlayerProperty.currentLevel]).Length;
                    GUI.Label(new Rect(num7 - 60f, num8 - 30f, 200f, 22f), "Loading Level (" + length.ToString() + "/" + num50.ToString() + ")");
                    this.retryTime += Time.deltaTime;
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    if (GUI.Button(new Rect(num7 - 20f, num8 + 50f, 40f, 30f), "Quit"))
                    {
                        PhotonNetwork.Disconnect();
                        Screen.lockCursor = false;
                        Screen.showCursor = true;
                        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                        FengGameManagerMKII.gameStart = false;
                        CacheGameObject.Find<FengCustomInputs>("InputManagerController").menuOn = false;
                        this.DestroyAllExistingCloths();
                        UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("MultiplayerManager"));
                        Application.LoadLevel("menu");
                    }
                }
            }
        }
    }

    public void OnJoinedLobby()
    {
        NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiStart, false);

        NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().PanelMultiJoinPrivate, false);
        //UnityEngine.MonoBehaviour.print("OnJoinedLobby");
        //UIMainReferences uIRefer = UIMainReferences.UIRefer;
        //NGUITools.SetActive(uIRefer.panelMultiStart, false);
        //(string)FengGameManagerMKII.settings[263] == "yes" || (string)FengGameManagerMKII.settings[263] == "on"
        if (serverList)
        {
            ServerListGUI.serverlistGUI.enabled = true;
            //return;
        }
        else NGUITools.SetActive(BRM.CacheGameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMultiROOM, true);
        //NGUITools.SetActive(UIMainReferences.UIRefer.panelMultiROOM, true);
    }

    public void OnJoinedRoom()
    {
        ServerListGUI.serverlistGUI.enabled = false;
        Joined = true;
        ExtraGUI.enable = true;
        this.maxPlayers = PhotonNetwork.room.maxPlayers;
        this.playerList = string.Empty;
        char[] separator = new char[] { "`"[0] };
        UnityEngine.MonoBehaviour.print("OnJoinedRoom " + PhotonNetwork.room.name + "    >>>>   " + LevelInfo.getInfo(PhotonNetwork.room.name.Split(separator)[1]).mapName);
        this.gameTimesUp = false;
        char[] chArray3 = new char[] { "`"[0] };
        string[] strArray = PhotonNetwork.room.name.Split(chArray3);
        level = strArray[1];
        if (strArray[2] == "normal")
        {
            this.difficulty = 0;
        }
        else if (strArray[2] == "hard")
        {
            this.difficulty = 1;
        }
        else if (strArray[2] == "abnormal")
        {
            this.difficulty = 2;
        }
        IN_GAME_MAIN_CAMERA.difficulty = this.difficulty;
        this.time = int.Parse(strArray[3]);
        this.time *= 60;
        if (strArray[4] == "day")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Day;
        }
        else if (strArray[4] == "dawn")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Dawn;
        }
        else if (strArray[4] == "night")
        {
            IN_GAME_MAIN_CAMERA.dayLight = DayLight.Night;
        }
        levelinfo = LevelInfo.getInfo(level);
        IN_GAME_MAIN_CAMERA.gamemode = LevelInfo.getInfo(level).type;
        PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.name, LoginFengKAI.player.name);
        hashtable.Add(PhotonPlayerProperty.guildName, LoginFengKAI.player.guildname);
        hashtable.Add(PhotonPlayerProperty.kills, 0);
        hashtable.Add(PhotonPlayerProperty.max_dmg, 0);
        hashtable.Add(PhotonPlayerProperty.total_dmg, 0);
        hashtable.Add(PhotonPlayerProperty.deaths, 0);
        hashtable.Add(PhotonPlayerProperty.dead, true);
        hashtable.Add(PhotonPlayerProperty.isTitan, 0);
        hashtable.Add(PhotonPlayerProperty.RCteam, 0);
        hashtable.Add(PhotonPlayerProperty.currentLevel, string.Empty);
        //hashtable.Add(PhotonPlayerProperty.modrolling, ModRoll());
        PhotonNetwork.player.SetCustomProperties(hashtable);
        this.humanScore = 0;
        this.titanScore = 0;
        this.PVPtitanScore = 0;
        this.PVPhumanScore = 0;
        this.wave = 1;
        this.highestwave = 1;
        this.localRacingResult = string.Empty;
        this.needChooseSide = true;
        this.chatContent = new ArrayList();
        this.killInfoGO = new ArrayList();
        InRoomChat.messages = new List<string>();
        if (!PhotonNetwork.isMasterClient)
        {
            PView.RPC("RequireStatus", PhotonTargets.MasterClient, new object[0]);
        }
        this.assetCacheTextures = new Dictionary<string, Texture2D>();
        this.isFirstLoad = true;
        this.name = LoginFengKAI.player.name;
        //if (loginstate != 3)
        //{
        //    this.name = nameField;
        //    if ((!this.name.StartsWith("[") || (this.name.Length < 8)) || (this.name.Substring(7, 1) != "]"))
        //    {
        //        this.name = "[9999FF]" + this.name;
        //    }
        //    this.name = this.name.Replace("[-]", "");
        //    LoginFengKAI.player.name = this.name;
        //}
        //ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
        //hashtable3.Add(PhotonPlayerProperty.name, this.name);
        //PhotonNetwork.player.SetCustomProperties(hashtable3);
        //PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        //{
        //    {
        //    "name",
        //    LoginFengKAI.player.name
        //    },
        //    {
        //    "guildName",
        //    LoginFengKAI.player.guildname
        //    }
        //});
        if (OnPrivateServer)
        {
            ServerRequestAuthentication(PrivateServerAuthPass);
        }
        string masterServerAddress = PhotonNetwork.networkingPeer.MasterServerAddress;
        int num = masterServerAddress.IndexOf("-") + 1;
        FengGameManagerMKII.settings[275] = "\n" + masterServerAddress.Substring(num, masterServerAddress.IndexOf(".exit") - num).ToUpper();
        FengGameManagerMKII.settings[275] = (string)FengGameManagerMKII.settings[275] + " " + strArray[0].Limit(20, TextColor.Hexadecimal, "[FFFFFF]...[-]") + " ";
        base.StartCoroutine(ClearList());
        /*if (Rolling)*/ /*StartCoroutine(ModRoll(Rollingmodlist));*/

    }
    public void ClearCustomObjects(bool destroyImmediate = false)
    {
        while (customObjects.Count > 0)
        {
            GameObject gameObject = customObjects.Dequeue();
            if (gameObject != null)
            {
                if (destroyImmediate) UnityEngine.Object.DestroyImmediate(gameObject, false);
                else UnityEngine.Object.Destroy(gameObject);
            }
        }
    }

    public IEnumerator ClearList()
    {
        if (Agressive)
        {
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if (player.isDCMarked || disconnectBan.Contains(player.uiname)) DC.LightDC(player.ID);
                yield return new WaitForEndOfFrame();
            }
        }
        yield break;
    }

    public void OnLeftLobby()
    {
        UnityEngine.MonoBehaviour.print("OnLeftLobby");
    }

    public void OnLeftRoom()
    {
        if (Application.loadedLevel != 0)
        {
            Time.timeScale = 1f;
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Disconnect();
            }
            this.resetSettings(true);
            this.loadconfig();
            //this.StartNameAnim(false);
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            gameStart = false;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            FengCustomInputs.Inputs.menuOn = false;
            this.DestroyAllExistingCloths();
            UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("MultiplayerManager"));
            Application.LoadLevel("menu");
            ShowMenuButtonGUI = true;
            ExtraGUI.enable = false;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if ((level != 0) && ((Application.loadedLevelName != "characterCreation") && (Application.loadedLevelName != "SnapShot")))
        {
            ChangeQuality.setCurrentQuality();
            foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("titan"))
            {
                
                if (!((obj2.GetPhotonView() != null) && obj2.GetPhotonView().owner.isMasterClient))
                {
                    UnityEngine.Object.Destroy(obj2);
                }
            }
            this.isWinning = false;
            gameStart = true;
            this.ShowHUDInfoCenter(string.Empty);
            GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("MainCamera_mono"), BRM.CacheGameObject.Find("cameraDefaultPosition").transform.position, BRM.CacheGameObject.Find("cameraDefaultPosition").transform.rotation);
            UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("cameraDefaultPosition"));
            obj3.name = "MainCamera";
            Screen.lockCursor = true;
            Screen.showCursor = true;
            (this.uirefer = (UIReferArray)UnityEngine.Object.Instantiate(CacheResources.Load<UIReferArray>("UI_IN_GAME"))).name = "UI_IN_GAME";
            this.uirefer.gameObject.SetActive(true);
            NGUITools.SetActive(this.uirefer.panels[0], true);
            NGUITools.SetActive(this.uirefer.panels[1], false);
            NGUITools.SetActive(this.uirefer.panels[2], false);
            NGUITools.SetActive(this.uirefer.panels[3], false);
            levelinfo = LevelInfo.getInfo(FengGameManagerMKII.level);
            this.cache();
            this.loadskin();
            IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
            IN_GAME_MAIN_CAMERA.mainCamera.setDayLight(IN_GAME_MAIN_CAMERA.dayLight);
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                this.single_kills = 0;
                this.single_maxDamage = 0;
                this.single_totalDamage = 0;
                IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
                IN_GAME_MAIN_CAMERA.spectate.disable = true;
                IN_GAME_MAIN_CAMERA.mouselook.disable = true;
                IN_GAME_MAIN_CAMERA.gamemode = levelinfo.type;
                this.SpawnPlayer(IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper(), "playerRespawn");
                if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
                {
                    Screen.lockCursor = true;
                }
                else
                {
                    Screen.lockCursor = false;
                }
                Screen.showCursor = false;
                int abnormal = 90;
                if (this.difficulty == 1)
                {
                    abnormal = 70;
                }
                this.spawnTitanCustom("titanRespawn", abnormal, levelinfo.enemyNumber, false);
            }
            else
            {
                PVPcheckPoint.chkPts = new ArrayList();
                IN_GAME_MAIN_CAMERA.mainCamera.enabled = false;
                IN_GAME_MAIN_CAMERA.shake.enabled = false;
                IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.MULTIPLAYER;
                this.updatePlayerList();
                if (levelinfo.type == GAMEMODE.TROST)
                {
                    CacheGameObject.Find("playerRespawn").SetActive(false);
                    UnityEngine.Object.Destroy(CacheGameObject.Find("playerRespawn"));
                    CacheGameObject.Find("rock").animation["lift"].speed = 0f;
                    CacheGameObject.Find("door_fine").SetActive(false);
                    CacheGameObject.Find("door_broke").SetActive(true);
                    UnityEngine.Object.Destroy(CacheGameObject.Find("ppl"));
                }
                else if (levelinfo.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    BRM.CacheGameObject.Find("playerRespawnTrost").SetActive(false);
                    UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("playerRespawnTrost"));
                }
                if (this.needChooseSide)
                {
                    this.ShowHUDInfoTopCenterADD("\n\nPRESS 1 TO ENTER GAME");
                }
                else if (((int)settings[0xf5]) == 0)
                {
                    if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
                    {
                        Screen.lockCursor = true;
                    }
                    else
                    {
                        Screen.lockCursor = false;
                    }
                    if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                    {
                        if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                        {
                            this.checkpoint = BRM.CacheGameObject.Find("PVPchkPtT");
                        }
                        else
                        {
                            this.checkpoint = BRM.CacheGameObject.Find("PVPchkPtH");
                        }
                    }
                    if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
                    }
                    else
                    {
                        this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
                    }
                }
                if (levelinfo.type == GAMEMODE.BOSS_FIGHT_CT)
                {
                    UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("rock"));
                }
                if (PhotonNetwork.isMasterClient)
                {
                    if (levelinfo.type == GAMEMODE.TROST)
                    {
                        if (!this.isPlayerAllDead2())
                        {
                            PhotonNetwork.Instantiate("TITAN_EREN_trost", new Vector3(-200f, 0f, -194f), Quaternion.Euler(0f, 180f, 0f), 0).GetComponent<TITAN_EREN>().rockLift = true;
                            int rate = 90;
                            if (this.difficulty == 1)
                            {
                                rate = 70;
                            }
                            GameObject[] objArray2 = GameObject.FindGameObjectsWithTag("titanRespawn");
                            GameObject obj4 = BRM.CacheGameObject.Find("titanRespawnTrost");
                            if (obj4 != null)
                            {
                                foreach (GameObject obj5 in objArray2)
                                {
                                    if (obj5.transform.parent.gameObject == obj4)
                                    {
                                        this.spawnTitan(rate, obj5.transform.position, obj5.transform.rotation, false);
                                    }
                                }
                            }
                        }
                    }
                    else if (levelinfo.type == GAMEMODE.BOSS_FIGHT_CT)
                    {
                        if (!this.isPlayerAllDead2())
                        {
                            PhotonNetwork.Instantiate("COLOSSAL_TITAN", (Vector3)(-Vector3.up * 10000f), Quaternion.Euler(0f, 180f, 0f), 0);
                        }
                    }
                    else if (((levelinfo.type == GAMEMODE.KILL_TITAN) || (levelinfo.type == GAMEMODE.ENDLESS_TITAN)) || (levelinfo.type == GAMEMODE.SURVIVE_MODE))
                    {
                        if ((levelinfo.name == "Annie") || (levelinfo.name == "Annie II"))
                        {
                            PhotonNetwork.Instantiate("FEMALE_TITAN", BRM.CacheGameObject.Find("titanRespawn").transform.position, BRM.CacheGameObject.Find("titanRespawn").transform.rotation, 0);
                        }
                        else
                        {
                            int num4 = 90;
                            if (this.difficulty == 1)
                            {
                                num4 = 70;
                            }
                            this.spawnTitanCustom("titanRespawn", num4, levelinfo.enemyNumber, false);
                        }
                    }
                    else if ((levelinfo.type != GAMEMODE.TROST) && ((levelinfo.type == GAMEMODE.PVP_CAPTURE) && (levelinfo.mapName == "OutSide")))
                    {
                        GameObject[] objArray3 = GameObject.FindGameObjectsWithTag("titanRespawn");
                        if (objArray3.Length <= 0)
                        {
                            return;
                        }
                        for (int i = 0; i < objArray3.Length; i++)
                        {
                            this.spawnTitanRaw(objArray3[i].transform.position, objArray3[i].transform.rotation).GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                        }
                    }
                }
                if (!levelinfo.supply)
                {
                    UnityEngine.Object.Destroy(BRM.CacheGameObject.Find("aot_supply"));
                }
                if (!PhotonNetwork.isMasterClient)
                {
                    PView.RPC("RequireStatus", PhotonTargets.MasterClient, new object[0]);
                }
                if (levelinfo.lavaMode)
                {
                    UnityEngine.Object.Instantiate(BRM.CacheResources.Load("levelBottom"), new Vector3(0f, -29.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    BRM.CacheGameObject.Find("aot_supply").transform.position = BRM.CacheGameObject.Find("aot_supply_lava_position").transform.position;
                    BRM.CacheGameObject.Find("aot_supply").transform.rotation = BRM.CacheGameObject.Find("aot_supply_lava_position").transform.rotation;
                }
                if (((int)settings[0xf5]) == 1)
                {
                    this.EnterSpecMode(true);
                }
            }
        }
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!noRestart)
        {
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingMC = true;
                if (GameSettings.infectionMode > 0)
                {
                    this.restartingTitan = true;
                }
                if (GameSettings.bombMode > 0)
                {
                    this.restartingBomb = true;
                }
                if (GameSettings.horseMode > 0)
                {
                    this.restartingHorse = true;
                }
                if (GameSettings.banEren == 0)
                {
                    this.restartingEren = true;
                }
            }
            this.resetSettings(false);
            if (!levelinfo.teamTitan)
            {
                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 1);
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }
            if (!this.gameTimesUp && PhotonNetwork.isMasterClient)
            {
                this.restartGame2(true);
                PView.RPC("setMasterRC", PhotonTargets.All, new object[0]);
            }
        }
        noRestart = false;
    }

    public void OnPhotonCreateRoomFailed()
    {
        UnityEngine.MonoBehaviour.print("OnPhotonCreateRoomFailed");
    }

    public void OnPhotonCustomRoomPropertiesChanged()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (!PhotonNetwork.room.open)
            {
                PhotonNetwork.room.open = true;
            }
            if (!PhotonNetwork.room.visible)
            {
                PhotonNetwork.room.visible = true;
            }
            if (PhotonNetwork.room.maxPlayers != this.maxPlayers)
            {
                PhotonNetwork.room.maxPlayers = this.maxPlayers;
            }
        }
        else
        {
            this.maxPlayers = PhotonNetwork.room.maxPlayers;
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (info == null)
        {
            return;
        }
        PhotonView photonView = info.photonView;
        if (photonView == null)
        {
            return;
        }
        GameObject gameObject = photonView.gameObject;
        if (gameObject == null)
        {
            return;
        }
        if (gameObject.tag != null && gameObject.tag != "Untagged")
        {
            FengGameManagerMKII.PhotonObjectsText.First.Add(photonView.GetHashCode(), string.Format("[cfcfcf]~[000000][[ff1c1c]{0}[-]][-] {1}[000000]:[-] {2}[000000]:[-] {3}[-]", new object[]
            {
                photonView.ownerId,
                gameObject.name,
                gameObject.tag,
                photonView.viewID
            }));
        }
        else if (FengGameManagerMKII.ObjectListCheck(gameObject.name))
        {
            FengGameManagerMKII.PhotonObjectsText.First.Add(photonView.GetHashCode(), string.Format("[cfcfcf]~[000000][[ff1c1c]{0}[-]][-] {1}[000000]:[-] {2}[000000]:[-] {3}[-]", new object[]
            {
                photonView.ownerId,
                gameObject.name,
                "Untagged",
                photonView.viewID
            }));
        }
        FengGameManagerMKII.PhotonObjectsText.Second = true;
        UnityEngine.MonoBehaviour.print("OnPhotonInstantiate");

    }

    public void OnPhotonDestroy(int hashCode)
    {
        if (FengGameManagerMKII.PhotonObjectsText.First.Remove(hashCode))
        {
            FengGameManagerMKII.PhotonObjectsText.Second = true;
        }
    }

    public void OnPhotonJoinRoomFailed()
    {
        UnityEngine.MonoBehaviour.print("OnPhotonJoinRoomFailed");
    }

    public void OnPhotonMaxCccuReached()
    {
        UnityEngine.MonoBehaviour.print("OnPhotonMaxCccuReached");
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        if (disconnectBan.Contains(player.uiname) || player.isDCMarked)
        {
            StatsTab.AddLine("Player " + player.uiname.ToRGBA() + " in <color=yellow>DC List</color>", StatsTab.DebugType.LOG);
            DC.LightDC(player.ID);
        }
        if (PhotonNetwork.isMasterClient)
        {
            if (FengGameManagerMKII.PView == null)
            {
                FengGameManagerMKII.PView = base.photonView;
            }
            if (banHash.ContainsValue(RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name])))
            {
                this.kickPlayerRC(player, false, "banned.");
            }
            else
            {
                int num = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statACL]);
                int num2 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statBLA]);
                int num3 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statGAS]);
                int num4 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statSPD]);
                if ((((num > 150) || (num2 > 0x7d)) || (num3 > 150)) || (num4 > 140))
                {
                    this.kickPlayerRC(player, true, "excessive stats.");
                    return;
                }
                if (GameSettings.asoPreservekdr == 1)
                {
                    base.StartCoroutine(this.WaitAndReloadKDR(player));
                }
                if (level.StartsWith("Custom"))
                {
                    base.StartCoroutine(this.customlevelE(new List<PhotonPlayer> { player }));
                }
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                if (GameSettings.bombMode == 1)
                {
                    hashtable.Add("bomb", 1);
                }
                if (GameSettings.globalDisableMinimap == 1)
                {
                    hashtable.Add("globalDisableMinimap", 1);
                }
                if (GameSettings.teamMode > 0)
                {
                    hashtable.Add("team", GameSettings.teamMode);
                }
                if (GameSettings.pointMode > 0)
                {
                    hashtable.Add("point", GameSettings.pointMode);
                }
                if (GameSettings.disableRock > 0)
                {
                    hashtable.Add("rock", GameSettings.disableRock);
                }
                if (GameSettings.explodeMode > 0)
                {
                    hashtable.Add("explode", GameSettings.explodeMode);
                }
                //if (GameSettings.healthMode > 0)
                //{
                //    hashtable.Add("healthMode", GameSettings.healthMode);
                //    hashtable.Add("healthLower", GameSettings.healthLower);
                //    hashtable.Add("healthUpper", GameSettings.healthUpper);
                //}
                //if (GameSettings.infectionMode > 0)
                //{
                //    hashtable.Add("infection", GameSettings.infectionMode);
                //}
                //if (GameSettings.banEren == 1)
                //{
                //    hashtable.Add("eren", GameSettings.banEren);
                //}
                //if (GameSettings.moreTitans > 0)
                //{
                //    hashtable.Add("titanc", GameSettings.moreTitans);
                //}
                //if (GameSettings.damageMode > 0)
                //{
                //    hashtable.Add("damage", GameSettings.damageMode);
                //}
                //if (GameSettings.sizeMode > 0)
                //{
                //    hashtable.Add("sizeMode", GameSettings.sizeMode);
                //    hashtable.Add("sizeLower", GameSettings.sizeLower);
                //    hashtable.Add("sizeUpper", GameSettings.sizeUpper);
                //}
                //if (GameSettings.spawnMode > 0)
                //{
                //    hashtable.Add("spawnMode", GameSettings.spawnMode);
                //    hashtable.Add("nRate", GameSettings.nRate);
                //    hashtable.Add("aRate", GameSettings.aRate);
                //    hashtable.Add("jRate", GameSettings.jRate);
                //    hashtable.Add("cRate", GameSettings.cRate);
                //    hashtable.Add("pRate", GameSettings.pRate);
                //}
                //if (GameSettings.waveModeOn > 0)
                //{
                //    hashtable.Add("waveModeOn", 1);
                //    hashtable.Add("waveModeNum", GameSettings.waveModeNum);
                //}
                if (GameSettings.friendlyMode > 0)
                {
                    hashtable.Add("friendly", 1);
                }
                if (GameSettings.pvpMode > 0)
                {
                    hashtable.Add("pvp", GameSettings.pvpMode);
                }
                //if (GameSettings.maxWave > 0)
                //{
                //    hashtable.Add("maxwave", GameSettings.maxWave);
                //}
                if (GameSettings.endlessMode > 0)
                {
                    hashtable.Add("endless", GameSettings.endlessMode);
                }
                if (GameSettings.motd != string.Empty)
                {
                    hashtable.Add("motd", GameSettings.motd);
                }
                if (GameSettings.horseMode > 0)
                {
                    hashtable.Add("horse", GameSettings.horseMode);
                }
                if (GameSettings.ahssReload > 0)
                {
                    hashtable.Add("ahssReload", GameSettings.ahssReload);
                }
                if (GameSettings.punkWaves > 0)
                {
                    hashtable.Add("punkWaves", GameSettings.punkWaves);
                }
                if (GameSettings.deadlyCannons > 0)
                {
                    hashtable.Add("deadlycannons", GameSettings.deadlyCannons);
                }
                if (GameSettings.racingStatic > 0)
                {
                    hashtable.Add("asoracing", GameSettings.racingStatic);
                }
                if ((ignoreList != null) && (ignoreList.Count > 0))
                {
                    PView.RPC("ignorePlayerArray", player, new object[] { ignoreList.ToArray() });
                }
                PView.RPC("settingRPC", player, new object[] { hashtable });
                PView.RPC("setMasterRC", player, new object[0]);
                if ((Time.timeScale <= 0.1f) && (this.pauseWaitTime > 3f))
                {
                    PView.RPC("pauseRPC", player, new object[] { true });
                    object[] parameters = new object[] { "<color=#FFCC00>MasterClient has paused the game.</color>", "" };
                    PView.RPC("Chat", player, parameters);
                }
                string context = string.Empty;
                foreach (string Mode in ActiveGameModes)
                {
                    context += "\n" + Mode;
                }
                object[] parameter = new object[] { context, string.Empty };
                PView.RPC("Chat", player, parameter);

            }
        }
        this.updatePlayerList();
        base.StartCoroutine(this.OnDelayJoinedPlayer(player));
    }
    private void OnDestroy()
    {
        base.StopAllCoroutines();
        //FengGameManagerMKII.customPhoton.Clear();
        //FengGameManagerMKII.titanSpawnersCopy.Clear();
        //while (customObjects.Count > 0)
        //{
        //    GameObject gameObject = customObjects.Dequeue();
        //    if (gameObject != null)
        //    {
        //        UnityEngine.Object.DestroyImmediate(gameObject, false);
        //    }
        //}
        ClearCustomObjects(true);
        PView = null;
        instance = null;
    }
    private IEnumerator OnDelayJoinedPlayer(PhotonPlayer player)
    {
        //yield return new WaitForSeconds(2f);
        Stopwatch watch = Stopwatch.StartNew();
        while (player.customProperties["name"] == null || player == null)
        {
            yield return new WaitForSeconds(0.5f);
            if (!player.IsInPlayerList() || watch.ElapsedMilliseconds > 2000L)
            {
                watch.Reset();
                yield break;
            }
        }
        watch.Reset();
        Yield.Begin(new Action(this.updatePlayerList));
        StatsTab.AddLine("<i>" + player.uiname.ToRGBA() + "</i><color=black> [<color=white>" + player.ID + "</color>]</color><color=#7b001c> joined the server.</color>", StatsTab.DebugType.LOG);
        yield return new WaitForEndOfFrame();
        if (Agressive && (disconnectBan.Contains(player.uiname) || player.isDCMarked))
        {
            StatsTab.AddLine("Player " + player.uiname.ToRGBA() + " in <color=yellow>DC List</color>", StatsTab.DebugType.WARNING);
            DC.LightDC(player.ID);
        }
        yield break;
        //if (PhotonNetwork.isMasterClient)
        //{
        //    //for (int i = 0; i < 20; i++)
        //    //{
        //    //    object[] objArray3 = new object[] { "", string.Empty };
        //    //    photonView.photonView.RPC("Chat", player, objArray3);
        //    //}
        //    string context = string.Empty;
        //    foreach (string Mode in ActiveGameModes)
        //    {
        //        context += "\n" + Mode;
        //    }
        //    object[] parameters = new object[] { context, string.Empty };
        //    photonView.photonView.RPC("Chat", player, parameters);
        //}
    }
    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        if (!this.gameTimesUp)
        {
            this.oneTitanDown(string.Empty, true);
            this.someOneIsDead(0);
        }
        if (ignoreList.Contains(player.ID))
        {
            ignoreList.Remove(player.ID);
        }
        InstantiateTracker.instance.TryRemovePlayer(player.ID);
        if (PhotonNetwork.isMasterClient)
        {
            PView.RPC("verifyPlayerHasLeft", PhotonTargets.All, new object[] { player.ID });
        }
        if (GameSettings.asoPreservekdr == 1)
        {
            string key = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            if (this.PreservedPlayerKDR.ContainsKey(key))
            {
                this.PreservedPlayerKDR.Remove(key);
            }
            int[] numArray2 = new int[] { RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]), RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]) };
            this.PreservedPlayerKDR.Add(key, numArray2);
        }
        this.updatePlayerList();
    }

    public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        //string strprop = string.Empty;
        //foreach (object prop in playerAndUpdatedProps)
        //{
        //    strprop = prop.ToString();
        //    if ((strprop.Contains("name") || strprop.Contains("guildname")) && playerAndUpdatedProps[0] != PhotonNetwork.player)
        //        StatsTab.AddLine("trying change nickname or guildname ", StatsTab.DebugType.WARNING);
        //}

        //if ((GameSettings.teamMode == 0 && !info.sender.isMasterClient && !info.sender.isLocal) || (GameSettings.teamMode == 0 && info.sender.isMasterClient && !info.sender.isLocal))
        //{
        //    StatsTab.AddLine("Player" + info.sender.uiname.ToRGBA() + " try change your name...", StatsTab.DebugType.WARNING);
        //    string text = "Dont need try change my nickname...";
        //    FengGameManagerMKII.instance.photonView.RPC("Chat", info.sender, text, "<color=black>{<color=yellow>PM</color>}</color>" + FengGameManagerMKII.Chatname);
        //    InRoomChat.addLINE2(string.Concat(new object[]
        //    {
        //                                        "<color=black>{<color=yellow>PM<color=orange>",
        //                                        info.sender.ID,
        //                                        "</color></color>}</color>",
        //                                        FengGameManagerMKII.Chatname,
        //                                        ":",
        //                                        text
        //    }));
        //    ExitGames.Client.Photon.Hashtable hashtable;
        //    hashtable = new ExitGames.Client.Photon.Hashtable();
        //    hashtable.Add(PhotonPlayerProperty.name, this.name);
        //    hashtable.Add(PhotonPlayerProperty.guildName, LoginFengKAI.player.guildname);
        //    PhotonNetwork.player.SetCustomProperties(hashtable);
        //    return;
        //}
        this.updatePlayerList();
        if (((playerAndUpdatedProps != null) && (playerAndUpdatedProps.Length >= 2)) && (((PhotonPlayer)playerAndUpdatedProps[0]) == PhotonNetwork.player))
        {
            ExitGames.Client.Photon.Hashtable hashtable = (ExitGames.Client.Photon.Hashtable)playerAndUpdatedProps[1];
            if (/*(string)FengGameManagerMKII.settings[270] == "None" &&*/ RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name]) != LoginFengKAI.player.name)
            {
                ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                hashtable2.Add(PhotonPlayerProperty.name, LoginFengKAI.player.name);
                PhotonNetwork.player.SetCustomProperties(hashtable2);
                StatsTab.AddLine("trying change custom properties... (name)", StatsTab.DebugType.WARNING);
            }
            if (hashtable.ContainsKey("guildName") && RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.guildName]) != LoginFengKAI.player.guildname)
            {
                ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                hashtable2.Add(PhotonPlayerProperty.guildName, LoginFengKAI.player.guildname);
                PhotonNetwork.player.SetCustomProperties(hashtable2);
                StatsTab.AddLine("trying change custom properties... (guildname)", StatsTab.DebugType.WARNING);
            }
            //foreach (object obj in hashtable.Keys)
            //{
            //    string currkey = (string)obj;
            //    if (!FengGameManagerMKII.MyCurrPlayerPropsKeys.Contains(currkey))
            //    {
            //        StatsTab.AddLine("trying change custom properties... (Add any Key: " + currkey + ")", StatsTab.DebugType.WARNING);
            //        StatsTab.AddLine("Start remove that...", StatsTab.DebugType.LOG);
            //        PhotonNetwork.player.RemoveFromCustomProperties(hashtable);
            //    }
            //}
            if (((hashtable.ContainsKey("statACL") || hashtable.ContainsKey("statBLA")) || hashtable.ContainsKey("statGAS")) || hashtable.ContainsKey("statSPD"))
            {
                PhotonPlayer player = PhotonNetwork.player;
                int num = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statACL]);
                int num2 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statBLA]);
                int num3 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statGAS]);
                int num4 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.statSPD]);
                if (num > 150)
                {
                    ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statACL, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num = 100;
                }
                if (num2 > 0x7d)
                {
                    ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statBLA, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num2 = 100;
                }
                if (num3 > 150)
                {
                    ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statGAS, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num3 = 100;
                }
                if (num4 > 140)
                {
                    ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                    hashtable2.Add(PhotonPlayerProperty.statSPD, 100);
                    PhotonNetwork.player.SetCustomProperties(hashtable2);
                    num4 = 100;
                }
            }
        }
    }

    public void RemturnToCustomProperties(ExitGames.Client.Photon.Hashtable deletedProps)
    {

    }

    public void OnPhotonRandomJoinFailed()
    {
        UnityEngine.MonoBehaviour.print("OnPhotonRandomJoinFailed");
    }

    public void OnPhotonSerializeView()
    {
        UnityEngine.MonoBehaviour.print("OnPhotonSerializeView");
    }

    public void OnReceivedRoomListUpdate()
    {
    }

    public void OnUpdate()
    {
        if (RCEvents.ContainsKey("OnUpdate"))
        {
            if (this.updateTime > 0f)
            {
                this.updateTime -= Time.deltaTime;
            }
            else
            {
                ((RCEvent)RCEvents["OnUpdate"]).checkEvent();
                this.updateTime = 1f;
            }
        }
    }

    public void OnUpdatedFriendList()
    {
        UnityEngine.MonoBehaviour.print("OnUpdatedFriendList");
    }

    public int operantType(string str, int condition)
    {
        switch (condition)
        {
            case 0:
            case 3:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 5;
                    }
                    if (!str.StartsWith("LessThan"))
                    {
                        if (str.StartsWith("LessThanOrEquals"))
                        {
                            return 1;
                        }
                        if (str.StartsWith("GreaterThanOrEquals"))
                        {
                            return 3;
                        }
                        if (str.StartsWith("GreaterThan"))
                        {
                            return 4;
                        }
                    }
                    return 0;
                }
                return 2;

            case 1:
            case 4:
            case 5:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 5;
                    }
                    return 0;
                }
                return 2;

            case 2:
                if (!str.StartsWith("Equals"))
                {
                    if (str.StartsWith("NotEquals"))
                    {
                        return 1;
                    }
                    if (str.StartsWith("Contains"))
                    {
                        return 2;
                    }
                    if (str.StartsWith("NotContains"))
                    {
                        return 3;
                    }
                    if (str.StartsWith("StartsWith"))
                    {
                        return 4;
                    }
                    if (str.StartsWith("NotStartsWith"))
                    {
                        return 5;
                    }
                    if (str.StartsWith("EndsWith"))
                    {
                        return 6;
                    }
                    if (str.StartsWith("NotEndsWith"))
                    {
                        return 7;
                    }
                    return 0;
                }
                return 0;
        }
        return 0;
    }

    public RCEvent parseBlock(string[] stringArray, int eventClass, int eventType, RCCondition condition)
    {
        List<RCAction> sentTrueActions = new List<RCAction>();
        RCEvent event2 = new RCEvent(null, null, 0, 0);
        for (int i = 0; i < stringArray.Length; i++)
        {
            int num2;
            int num3;
            int num4;
            int length;
            string[] strArray;
            int num6;
            int num7;
            int index;
            int num9;
            string str;
            int num10;
            int num11;
            int num12;
            string[] strArray2;
            RCCondition condition2;
            RCEvent event3;
            RCAction action;
            if (stringArray[i].StartsWith("If") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                num10 = this.conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = this.operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                event3 = this.parseBlock(strArray, 1, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                event2 = event3;
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("While") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                num10 = this.conditionType(str);
                num11 = str.IndexOf('.');
                str = str.Substring(num11 + 1);
                num12 = this.operantType(str, num10);
                index = str.IndexOf('(');
                num9 = str.LastIndexOf(")");
                strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                event3 = this.parseBlock(strArray, 3, 0, condition2);
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachTitan") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, (num9 - index) - 3);
                num10 = 0;
                event3 = this.parseBlock(strArray, 2, num10, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("ForeachPlayer") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                num7 = num2;
                while (num7 <= num3)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                    num7++;
                }
                index = stringArray[i].IndexOf("(");
                num9 = stringArray[i].LastIndexOf(")");
                str = stringArray[i].Substring(index + 2, (num9 - index) - 3);
                num10 = 1;
                event3 = this.parseBlock(strArray, 2, num10, null);
                event3.foreachVariableName = str;
                action = new RCAction(0, 0, event3, null);
                sentTrueActions.Add(action);
                i = num3;
            }
            else if (stringArray[i].StartsWith("Else") && (stringArray[i + 1] == "{"))
            {
                num2 = i + 2;
                num3 = i + 2;
                num4 = 0;
                length = i + 2;
                while (length < stringArray.Length)
                {
                    if (stringArray[length] == "{")
                    {
                        num4++;
                    }
                    if (stringArray[length] == "}")
                    {
                        if (num4 > 0)
                        {
                            num4--;
                        }
                        else
                        {
                            num3 = length - 1;
                            length = stringArray.Length;
                        }
                    }
                    length++;
                }
                strArray = new string[(num3 - num2) + 1];
                num6 = 0;
                for (num7 = num2; num7 <= num3; num7++)
                {
                    strArray[num6] = stringArray[num7];
                    num6++;
                }
                if (stringArray[i] == "Else")
                {
                    event3 = this.parseBlock(strArray, 0, 0, null);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
                else if (stringArray[i].StartsWith("Else If"))
                {
                    index = stringArray[i].IndexOf("(");
                    num9 = stringArray[i].LastIndexOf(")");
                    str = stringArray[i].Substring(index + 1, (num9 - index) - 1);
                    num10 = this.conditionType(str);
                    num11 = str.IndexOf('.');
                    str = str.Substring(num11 + 1);
                    num12 = this.operantType(str, num10);
                    index = str.IndexOf('(');
                    num9 = str.LastIndexOf(")");
                    strArray2 = str.Substring(index + 1, (num9 - index) - 1).Split(new char[] { ',' });
                    condition2 = new RCCondition(num12, num10, this.returnHelper(strArray2[0]), this.returnHelper(strArray2[1]));
                    event3 = this.parseBlock(strArray, 1, 0, condition2);
                    action = new RCAction(0, 0, event3, null);
                    event2.setElse(action);
                    i = num3;
                }
            }
            else
            {
                int num13;
                int num14;
                int num15;
                int num16;
                string str2;
                string[] strArray3;
                RCActionHelper helper;
                RCActionHelper helper2;
                RCActionHelper helper3;
                if (stringArray[i].StartsWith("VariableInt"))
                {
                    num13 = 1;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableBool"))
                {
                    num13 = 2;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetToOpposite"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        action = new RCAction(num13, 11, null, new RCActionHelper[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableString"))
                {
                    num13 = 3;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Concat"))
                    {
                        RCActionHelper[] helpers = new RCActionHelper[strArray3.Length];
                        for (length = 0; length < strArray3.Length; length++)
                        {
                            helpers[length] = this.returnHelper(strArray3[length]);
                        }
                        action = new RCAction(num13, 7, null, helpers);
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Append"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 8, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Replace"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 10, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Remove"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 9, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableFloat"))
                {
                    num13 = 4;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("SetRandom"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        helper3 = this.returnHelper(strArray3[2]);
                        action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2, helper3 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Add"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Subtract"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Multiply"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Divide"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Modulo"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                    else if (str2.StartsWith("Power"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariablePlayer"))
                {
                    num13 = 5;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else if (stringArray[i].StartsWith("VariableTitan"))
                {
                    num13 = 6;
                    num14 = stringArray[i].IndexOf('.');
                    num15 = stringArray[i].IndexOf('(');
                    num16 = stringArray[i].LastIndexOf(')');
                    str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                    strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                    if (str2.StartsWith("Set"))
                    {
                        helper = this.returnHelper(strArray3[0]);
                        helper2 = this.returnHelper(strArray3[1]);
                        action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                        sentTrueActions.Add(action);
                    }
                }
                else
                {
                    RCActionHelper helper4;
                    if (stringArray[i].StartsWith("Player"))
                    {
                        num13 = 7;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("KillPlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayerAt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnPlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MovePlayer"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetKills"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetDeaths"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 5, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetMaxDmg"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 6, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTotalDmg"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 7, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetName"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 8, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetGuildName"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 9, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetTeam"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 10, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomInt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 11, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomBool"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 12, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomString"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 13, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetCustomFloat"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 14, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Titan"))
                    {
                        num13 = 8;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("KillTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper, helper2, helper3 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitanAt"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            RCActionHelper helper5 = this.returnHelper(strArray3[4]);
                            RCActionHelper helper6 = this.returnHelper(strArray3[5]);
                            RCActionHelper helper7 = this.returnHelper(strArray3[6]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper, helper2, helper3, helper4, helper5, helper6, helper7 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SpawnTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("SetHealth"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper, helper2 });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("MoveTitan"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            helper2 = this.returnHelper(strArray3[1]);
                            helper3 = this.returnHelper(strArray3[2]);
                            helper4 = this.returnHelper(strArray3[3]);
                            action = new RCAction(num13, 4, null, new RCActionHelper[] { helper, helper2, helper3, helper4 });
                            sentTrueActions.Add(action);
                        }
                    }
                    else if (stringArray[i].StartsWith("Game"))
                    {
                        num13 = 9;
                        num14 = stringArray[i].IndexOf('.');
                        num15 = stringArray[i].IndexOf('(');
                        num16 = stringArray[i].LastIndexOf(')');
                        str2 = stringArray[i].Substring(num14 + 1, (num15 - num14) - 1);
                        strArray3 = stringArray[i].Substring(num15 + 1, (num16 - num15) - 1).Split(new char[] { ',' });
                        if (str2.StartsWith("PrintMessage"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 0, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("LoseGame"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 2, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("WinGame"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 1, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                        else if (str2.StartsWith("Restart"))
                        {
                            helper = this.returnHelper(strArray3[0]);
                            action = new RCAction(num13, 3, null, new RCActionHelper[] { helper });
                            sentTrueActions.Add(action);
                        }
                    }
                }
            }
        }
        return new RCEvent(condition, sentTrueActions, eventClass, eventType);
    }

    [RPC]
    public void pauseRPC(bool pause, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            if (pause)
            {
                this.pauseWaitTime = 100000f;
                Time.timeScale = 1E-06f;
            }
            else
            {
                this.pauseWaitTime = 3f;
            }
        }
    }

    public void playerKillInfoSingleUpdate(int dmg)
    {
        this.single_kills++;
        this.single_maxDamage = Mathf.Max(dmg, this.single_maxDamage);
        this.single_totalDamage += dmg;
    }

    public void playerKillInfoUpdate(PhotonPlayer player, int dmg)
    {
        ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.kills, ((int)player.customProperties[PhotonPlayerProperty.kills]) + 1);
        player.SetCustomProperties(propertiesToSet);
        propertiesToSet = new ExitGames.Client.Photon.Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.max_dmg, Mathf.Max(dmg, (int)player.customProperties[PhotonPlayerProperty.max_dmg]));
        player.SetCustomProperties(propertiesToSet);
        propertiesToSet = new ExitGames.Client.Photon.Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.total_dmg, ((int)player.customProperties[PhotonPlayerProperty.total_dmg]) + dmg);
        player.SetCustomProperties(propertiesToSet);
    }

    public GameObject randomSpawnOneTitan(string place, int rate)
    {
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(place);
        int index = UnityEngine.Random.Range(0, objArray.Length);
        GameObject obj2 = objArray[index];
        while (objArray[index] == null)
        {
            index = UnityEngine.Random.Range(0, objArray.Length);
            obj2 = objArray[index];
        }
        objArray[index] = null;
        return this.spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, false);
    }

    public void randomSpawnTitan(string place, int rate, int num, bool punk)
    {
        if (num == -1)
        {
            num = 1;
        }
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(place);
        if (objArray.Length > 0)
        {
            for (int i = 0; i < num; i++)
            {
                int index = UnityEngine.Random.Range(0, objArray.Length);
                GameObject obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = UnityEngine.Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }
                objArray[index] = null;
                this.spawnTitan(rate, obj2.transform.position, obj2.transform.rotation, punk);
            }
        }
    }

    public Texture2D RCLoadTexture(string tex)
    {
        if (this.assetCacheTextures == null)
        {
            this.assetCacheTextures = new Dictionary<string, Texture2D>();
        }
        if (this.assetCacheTextures.ContainsKey(tex))
        {
            return this.assetCacheTextures[tex];
        }
        Texture2D textured2 = (Texture2D)RCassets.Load(tex);
        this.assetCacheTextures.Add(tex, textured2);
        return textured2;
    }

    public void RecompilePlayerList(float time)
    {
        if (!this.isRecompiling)
        {
            this.isRecompiling = true;
            base.StartCoroutine(this.WaitAndRecompilePlayerList(time));
        }
    }

    [RPC]
    private void refreshPVPStatus(int score1, int score2)
    {
        this.PVPhumanScore = score1;
        this.PVPtitanScore = score2;
    }

    [RPC]
    private void refreshPVPStatus_AHSS(int[] score1)
    {
        UnityEngine.MonoBehaviour.print(score1);
        this.teamScores = score1;
    }

    private void refreshRacingResult()
    {
        this.localRacingResult = "Result\n";
        IComparer comparer = new IComparerRacingResult();
        this.racingResult.Sort(comparer);
        int num = Mathf.Min(this.racingResult.Count, 6);
        for (int i = 0; i < num; i++)
        {
            string localRacingResult = this.localRacingResult;
            object[] objArray1 = new object[] { localRacingResult, "Rank ", i + 1, " : " };
            this.localRacingResult = string.Concat(objArray1);
            this.localRacingResult = this.localRacingResult + (this.racingResult[i] as RacingResult).name;
            this.localRacingResult = this.localRacingResult + "   " + ((((int)((this.racingResult[i] as RacingResult).time * 100f)) * 0.01f)).ToString() + "s";
            this.localRacingResult = this.localRacingResult + "\n";
        }
        object[] parameters = new object[] { this.localRacingResult };
        PView.RPC("netRefreshRacingResult", PhotonTargets.All, parameters);
    }

    private void refreshRacingResult2()
    {
        this.localRacingResult = "Result\n";
        IComparer comparer = new IComparerRacingResult();
        this.racingResult.Sort(comparer);
        int num = Mathf.Min(this.racingResult.Count, 10);
        for (int i = 0; i < num; i++)
        {
            string localRacingResult = this.localRacingResult;
            object[] objArray2 = new object[] { localRacingResult, "Rank ", i + 1, " : " };
            this.localRacingResult = string.Concat(objArray2);
            this.localRacingResult = this.localRacingResult + (this.racingResult[i] as RacingResult).name;
            this.localRacingResult = this.localRacingResult + "   " + ((((int)((this.racingResult[i] as RacingResult).time * 100f)) * 0.01f)).ToString() + "s";
            this.localRacingResult = this.localRacingResult + "\n";
        }
        object[] parameters = new object[] { this.localRacingResult };
        PView.RPC("netRefreshRacingResult", PhotonTargets.All, parameters);
    }

    [RPC]
    private void refreshStatus(int score1, int score2, int wav, int highestWav, float time1, float time2, bool startRacin, bool endRacin)
    {
        this.humanScore = score1;
        this.titanScore = score2;
        this.wave = wav;
        this.highestwave = highestWav;
        this.roundTime = time1;
        this.timeTotalServer = time2;
        this.startRacing = startRacin;
        this.endRacing = endRacin;
        if (this.startRacing)
        {
            GameObject door = BRM.CacheGameObject.Find("door");
            door.SetActive(false);
        }
    }

    public IEnumerator reloadSky()
    {
        yield return new WaitForSeconds(0.5f);
        if ((skyMaterial != null) && (IN_GAME_MAIN_CAMERA.skybox.material != skyMaterial))
        {
            IN_GAME_MAIN_CAMERA.skybox.material = skyMaterial;
        }
        Screen.lockCursor = !Screen.lockCursor;
    }

    public void removeCT(COLOSSAL_TITAN titan)
    {
        this.cT.Remove(titan);
    }

    public void removeET(TITAN_EREN hero)
    {
        this.eT.Remove(hero);
    }

    public void removeFT(FEMALE_TITAN titan)
    {
        this.fT.Remove(titan);
    }

    public void removeHero(HERO hero, GameObject GO)
    {
        heroes.Remove(hero);
        allheroes.Remove(GO);
    }

    public void removeHook(Bullet h)
    {
        this.hooks.Remove(h);
    }

    public void removeTitan(TITAN titan, GameObject GO)
    {
        titans.Remove(titan);
        alltitans.Remove(GO);
    }

    [RPC]
    private void RequireStatus()
    {
        object[] parameters = new object[] { this.humanScore, this.titanScore, this.wave, this.highestwave, this.roundTime, this.timeTotalServer, this.startRacing, this.endRacing };
        PView.RPC("refreshStatus", PhotonTargets.Others, parameters);
        object[] objArray2 = new object[] { this.PVPhumanScore, this.PVPtitanScore };
        PView.RPC("refreshPVPStatus", PhotonTargets.Others, objArray2);
        object[] objArray3 = new object[] { this.teamScores };
        PView.RPC("refreshPVPStatus_AHSS", PhotonTargets.Others, objArray3);
    }

    [RPC]
    private void RPCLoadLevel(PhotonMessageInfo info)
    {
        if (!info.sender.isMasterClient)
        {
            StatsTab.AddLine(string.Format("<color=orange><b>(ID:{0}) {1} has restarted the server.</b></color>", info.sender.ID, info.sender.uiname.ToRGBA()), StatsTab.DebugType.WARNING);
        }
        if (info.sender.isMasterClient)
        {
            this.DestroyAllExistingCloths();
            PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
        }
        else if (PhotonNetwork.isMasterClient)
        {
            this.kickPlayerRC(info.sender, true, "false restart.");
        }
        else if (!masterRC)
        {
            this.restartCount.Add(Time.time);
            foreach (float num in this.restartCount)
            {
                if ((Time.time - num) > 60f)
                {
                    this.restartCount.Remove(num);
                }
            }
            if (this.restartCount.Count < 6)
            {
                this.DestroyAllExistingCloths();
                PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
            }
        }
    }
    private void resetGameSettings()
    {
        GameSettings.bombMode = 0;
        GameSettings.teamMode = 0;
        GameSettings.pointMode = 0;
        GameSettings.disableRock = 0;
        GameSettings.explodeMode = 0;
        GameSettings.healthMode = 0;
        GameSettings.healthLower = 0;
        GameSettings.healthUpper = 0;
        GameSettings.infectionMode = 0;
        GameSettings.banEren = 0;
        GameSettings.moreTitans = 0;
        GameSettings.damageMode = 0;
        GameSettings.sizeMode = 0;
        GameSettings.sizeLower = 0f;
        GameSettings.sizeUpper = 0f;
        GameSettings.spawnMode = 0;
        GameSettings.nRate = 0f;
        GameSettings.aRate = 0f;
        GameSettings.jRate = 0f;
        GameSettings.cRate = 0f;
        GameSettings.pRate = 0f;
        GameSettings.horseMode = 0;
        GameSettings.waveModeOn = 0;
        GameSettings.waveModeNum = 0;
        GameSettings.friendlyMode = 0;
        GameSettings.pvpMode = 0;
        GameSettings.maxWave = 0;
        GameSettings.endlessMode = 0;
        GameSettings.ahssReload = 0;
        GameSettings.punkWaves = 0;
        GameSettings.globalDisableMinimap = 0;
        GameSettings.motd = string.Empty;
        GameSettings.deadlyCannons = 0;
        GameSettings.asoPreservekdr = 0;
        GameSettings.racingStatic = 0;
    }

    private void resetSettings(bool isLeave)
    {
        this.name = LoginFengKAI.player.name;
        masterRC = false;
        ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
        propertiesToSet.Add(PhotonPlayerProperty.RCteam, 0);
        if (isLeave)
        {
            currentLevel = string.Empty;
            scriptcache = string.Empty;
            propertiesToSet.Add(PhotonPlayerProperty.currentLevel, string.Empty);
            this.levelCache = new List<string[]>();
            this.titanSpawns.Clear();
            this.playerSpawnsC.Clear();
            this.playerSpawnsM.Clear();
            this.titanSpawners.Clear();
            intVariables.Clear();
            boolVariables.Clear();
            stringVariables.Clear();
            floatVariables.Clear();
            globalVariables.Clear();
            RCRegions.Clear();
            RCEvents.Clear();
            RCVariableNames.Clear();
            playerVariables.Clear();
            titanVariables.Clear();
            RCRegionTriggers.Clear();
            currentScriptLogic = string.Empty;
            propertiesToSet.Add(PhotonPlayerProperty.statACL, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statBLA, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statGAS, 100);
            propertiesToSet.Add(PhotonPlayerProperty.statSPD, 100);
            this.restartingTitan = false;
            this.restartingMC = false;
            this.restartingHorse = false;
            this.restartingEren = false;
            this.restartingBomb = false;
            //while (customObjects.Count > 0)
            //{
            //    GameObject gameObject = customObjects.Dequeue();
            //    if (gameObject != null)
            //    {
            //        UnityEngine.Object.Destroy(gameObject);
            //    }
            //}
            ClearCustomObjects();
        }
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        this.resetGameSettings();
        banHash = new ExitGames.Client.Photon.Hashtable();
        imatitan = new ExitGames.Client.Photon.Hashtable();
        oldScript = string.Empty;
        ignoreList = new List<int>();
        this.restartCount = new List<float>();
        heroHash = new ExitGames.Client.Photon.Hashtable();
    }

    private IEnumerator respawnE(float seconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            if (!this.isLosing && !this.isWinning)
            {
                for (int j = 0; j < PhotonNetwork.playerList.Length; j++)
                {
                    PhotonPlayer targetPlayer = PhotonNetwork.playerList[j];
                    if (((targetPlayer.customProperties[PhotonPlayerProperty.RCteam] == null) && RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead])) && (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2))
                    {
                        PView.RPC("respawnHeroInNewRound", targetPlayer, new object[0]);
                    }
                }
            }
        }
    }

    internal static string[] StarterExplaination()
    {
        string text = "\n<color=red><b>";
        string str = "<color=yellow><b>  ";
        string str2 = "<color=#d1e5ff><b><i>     ";
        string str3 = "</b></color>";
        string str4 = "</i></b></color>";
        return new string[]
        {
            //text.Substring(1) + "***WAY TO UPDATE." + str3 + " On game startup, it checks for updates. If one is available it'll inform you on the bottom center of the screen with two buttons that will be visible at the bottom right. One for viewing the changelog and one for downloading exe and one for downloading the dll. The dll method is more like how professional games do it. Just replacing the dll by itself.",
            //text + "***LEVI SKILL" + str3 + " altered by a tad. Instead I cut off the ending a bit so when you are in air it'll make him fade into that air animation instead of stance, and you can use your left hook near the end after a spin.",
            //text + "***IMPROVED" + str3 + " Changing Blades/ReloadGuns and blade throwing. Now you can change/reload while sliding on the ground. Same for blade throwing",
            //text + "***Image Uploading." + str3 + " The snapshots you get you can upload them in the snapshot review screen to imgur instead of downloading them then uploading.",
            //text + "***The name.cfg" + str3 + " is necessary or else u won't be able to play. You need to put ur name in the name.cfg and such. And it explains itself if you read it.",
            //str + "        ***HGamemode: and TGamemode" + str3 + " in the name.cfg. Just put in the chat gamemode commands after the : so that when you play as human (HGamemode) or titan (TGamemode) it will turn them on.",
            //text + "***FOR SERVERNAMES" + str3 + " Now you can set your servername in there instead. In the name.cfg do\n         servername:[the server name you want]",
            //text + "***INCLUDED" + str3 + " 'Excluding Full Servers' to the serverlist. So you can filter out full servers.",
            //text + "***INCLUDED" + str3 + " an indecator for how many bullets you have left with your guns at the gasUI.",
            //text + "THROWING ROCKS AS A PT" + str3 + " If you want to aim at a player that is high in the sky double tap otherwise itll go for the closest.",
            //text + "***THROWING HUMANS." + str3 + " If you are a PT then you can only throw a human if you grab them with your right hand. Easiest way is to hold forward to run and grab. Shifter titans or if you turn on all Punks. Will be able to throw humans if they grab you with their right hand. The titan will throw you at a random player if there is one. All players that get hit by the person thrown will die. If the person thrown hit a tree, ground, or invisible wall then he will die.",
            //text + "***THROWING BLADES" + str3 + " are possible in this mod. You can kill humans, kill titans, trip titans, and blind titans all by throwing blades. To throw blades you either assign HYPERCANNON as a special while being on the blades team. Not AHSS. Or you can just click salute while in air.",
            //str + "    ONE THING THO" + str3 + " You won't be able to kill boss titans aka Colossal Titan or Female Titan by throwing unless you in single mode or you throw them when ur near their neck/ankle/eye. You can only blind titans on other servers by being near their eye and same for ankles. Although not for killing regular titans in which cuts the neck.",
            //text + "***HYPERLINKS" + str3 + " You can also view picture links in game and open links through the chat. If someone sends the link, just press Hide/Show cursor button that you assigned in the pause menu and click on the link in the chat. If someone sends a picture link, it'll view it in game and you can download onto your pc or copy the url that you opened otherwise if it isn't a picture link it will open the url using your default browser.",
            //text + "***TWO TOPRIGHT HUDS" + str3 + " There are two top right HUD lists.",
            //str + "    OJECTLIST" + str3 + " (is when you use /objlist) is the list of objects revealing the objects name and its owner.",
            //str + "    ROOMLIST" + str3 + " (is when you use /roomlist) is the default top right hud.",
            //text + "***2 SPECIALS" + str3,
            //str + "   ASSIGNING" + str3 + " 2 specials (1 for air and 1 for ground). Jean special and Refill special must be assigned as ground to work.",
            //str + "   FIXED" + str3 + " armin special and marco special for air use when you assign it as airspecial.",
            //str + "   NEW SPECIALS" + str3 + " - Hypercannon, Afterimg, Radar, and Refill.",
            //str2 + "        HYPERCANNON" + str4 + " special allows u to have rapid fire with AHSS characters.Airspecial is one hand while groundspecial is both.",
            //str2 + "       AFTERIMG" + str4 + " allows you to teleport in the direction you decide to go in by using wsad or (for original users their mouse). This move is a teleporting dodge so that you can escape in the nick of time. After using 10, you will have to wait 50 seconds for a recharge and must be Air Special.",
            //str2 + "       RADAR" + str4 + " lets you find any titan on the map with a 3d view.",
            //str2 + "       REFILL" + str4 + " is a special where you earn 30 points of gas back. One use per life and must be Ground Special.",
            //str2 + "       TRAP" + str4 + " kills any titan within a 40 ft radius with an explosin. Only 5 traps can be existing at a time. It will recharge for 35 seconds after using 5 traps. Can be both ground and air and the trap can explode when a titan strikes it. If you spawn it right next to a titan then it will wait 5 seconds for it to either run or destroy it, same goes for you. If you are near 40 ft of it for more than 5 seconds it will kill you. If you get back into its radius it will kill you. It can only kill its owner and titans.",
            //text + "***RC FEATURES" + str3,
            //str + "   SKINS" + str3 + " are in the F1 menu when you press F1 or go into pause menu. IT WORKS EXACTLY LIKE RC MOD.",
            //str + "    XTRAS" + str3 + " like PVP Bomb & Damage Mode & Custom maps & Skins included from RC (Syncs with RC)",
            //text + "***F1 MENU" + str3,
            //str + "    GENERAL TAB" + str3 + " has preferences like show HUD, blue cross smoke, airspecial, groundspecial, etc. Basically preferences.",
            //str2 + "      ANIMATED NAME." + str4 + " In the General Tab, you can choose either Linear Gradient Name Animation, Rebound Name Animation, Faded Name Animation, etc. So your name can fade in to a different color or fade out depending on your hexes. Just have your name contain a fade in your hex and it will be able to work. For Example:[A5A5AE]L[8C8C97]I[727281]F[59596A]E[-][-][-][-]. It has a gradient from this silver to dark grey.",
            //str2 + "      FIRST PERSON CAMERA" + str4 + " First Person Camera is added in. Just turn it on in the F1 Menu.",
            //str + "    OTHER TABS" + str3 + " are ripped off of RC Mod and self explanitory. Like SKINS, MAPS, TITAN SKINS, etc.",
            //str2 + "       STEALING TITAN SKINS" + str4 + " from any player as long as the TITAN is spawned and not a boss titan. Go to the F1 Menu then click on Titan. And you will see at the near bottom player id to input and the button to start stealing.",
            //str + "    SINGLE GAMEMODES (only available if you are in single)" + str3,
            //str2 + "        WHILE IN SINGLE" + str4 + ", you can play gamemodes, change character, change daylight, and such all in game. Just click on SINGLE GAMEMODES in the General Tab of the F1 Menu when you are in Single Player, then you can do any gamemode you want.",
            //str2 + "        ALSO" + str4 + ", you can do a legit and fair speedrun in seconds. Just set it to 600 or whatever you want then when time is up it'll let you know how many you killed in that many seconds.",
            //text + "***F2 MENU" + str3,
            //str + "   STARTER" + str3 + " explains to new people who gets my mod.",
            //str + "   CHANGELOG" + str3 + " shows the recent changes",
            //str + "   COMMANDLIST" + str3 + " in the F2 menu to see all the commands.",
            //text + "***F3 MENU" + str3,
            //str + "   VIEWING SNAPSHOTS IN GAME." + str3 + " In F3 you will be able to upload the snapshot to imgur. Although it freezes the game for a short time until it's done since it's not the same as downloading."
            "put text here later with updating"
        };
    }

    internal static string ChangeLog()
    {
        string str = "<color=yellow><b>";
        string str2 = "</b></color>";
        //if ((bool)FengGameManagerMKII.settings[59] && IN_GAME_MAIN_CAMERA.mainCamera == null)
        //{
        //    return Regex.Replace(Regex.Replace((string)FengGameManagerMKII.settings[60], "(\\*+[^\\s]+)", "\n<color=red><b>$1</b></color>"), "(\\d*\\/\\d*\\/\\d*)", "<color=yellow><b>$1</b></color>");
        //}
        string[] value = new string[]
        {
            //str + "9/30/15" + str2 + "***ADDED two new commands. /immune and /setvice. Check commandlist for description.",
            //str + "9/1/15" + str2 + "***ADDED. A new layout for searching for servers. Turn on by putting in name.cfg 'serverlist:yes'.",
            //str + "8/20/15" + str2 + "***ADDED. Speed Meter. Turn on or off in F1 Menu.",
            //str + "8/17/15" + str2 + "***BUGFIXES. Only.",
            //str + "7/28/15" + str2 + "***BUGFIXES. Major ones.***FIXED Custom levels.***ADDED CUSTOM filter for the MAP button so you can search for custom maps both (CUSTOM and CUSTOM NOPT).",
            //str + "7/22/15" + str2 + "***FIXED /ban. Exe won't freeze.***ADDED to /kill a timer until they aren't playing anymore. So they are free to kill. ",
            //str + "7/10/15" + str2 + "***ADDED Respawn mode for Single Gamemodes.***UPDATED /kill cmd.***ADDED Minimaps. The rebinds are in Rebinds tab in F1 menu.***FIXED Skybox skins. Now it won't be mixed up.***CHANGED /timereset to /time for assigning server time.***ADDED /horserc for syncing rc horse mode with everyone else.***ADDED Cannons for custom maps.***ADDED a new FX for throwing blades.***UPDATED Bomb mode. To give your bomb a color or change its stats just go to\r\n\tF1 Menu > More > Abilities.\r\nStill syncs with RC.",
            //str + "6/30/15" + str2 + "***IMPROVED Custom map loading. Now it will only load once and not on each restart.***ADDED RC Blade/AHSS PVP mode.***ADDED All and No normals/aberrants/jumpers gamemode. Check commandlist for details.",
            //str + "6/19/15" + str2 + "***ADDED Maps and Levels filtering. Maps and levels are different. Maps are like the forest, the city, etc. Annie, F2, and F3 share the same map but are different levels.***FIXED Synchronizations with RC temporarily.***TOOLAZY to put in other shit from RC.***FIXED Throwing blades and spawning horses with rc user as host. Since rc just being uptight.",
            //str + "6/3/15" + str2 + "***NEWSPECIAL. Trap Skill. (Still in progress but go ahead and try it out) Kills any titan within a 50 ft radius with an explosin. Only 5 traps can be existing at a time. It will recharge after using 5 traps. 30 second recharge.***IMPROVED F2 Commandlist by ordering it in ABC order.***ADDED NEW COMMANDS. /mod or /modinfo to see the information of the current active gamemodes to everyone if you are mc. And added /horse for spawning and destroying your horse on switch. Also:RC & DC hosts autokicks those who spawn horses in maps where they don't belong. They are uptight what can I say.***ADDED 50-second recharge for afterimg.***IMPROVED Chat by adding a scrollbar (Didn't make it available to view all messages since it would just increase the lag of the game. Loading 1k messages each second would lag). Now there is a scrollbar for when the chat can't contain all the text. So it won't overflow. Although if you use original camera or wow camera, then it will only show the scroll bar when you click SHOW/HIDE CURSOR. Since the scrollbar would affect your gameplay with reeling.***FIXED custom maps since RC updated. I'll put in the new customs later.***FIXED original camera bug.",
            //str + "5/23/15" + str2 + "***FIXED custom maps.***FIXED survive mode (Waves).***IMPROVED F2 Menu for different screen resolutions. Bigger Text.***IMPROVED Commands. Now with most commands that require you to select. You can choose multiple by using a comma to seperate your selection. Check Commandlist for details.",
            //str + "5/12/15" + str2 + "***FIXED Afk kill. Now it can kill those who stand there.***ADDED Anti-RPCSpam although don't know if this improves anything. Just testing it.***ADDED HGamemode: and TGamemode in the name.cfg. Just put in the chat gamemode commands after the : so that when you play as human (HGamemode) or titan (TGamemode) it will turn them on.***IMPROVED Image Uploading. Won't freeze exe.***IMPROVED Annie Apocalypse. Smoother FPS. Less lag.",
            //str + "5/5/15" + str2 + "***ADDED Nape Health to single player gamemodes.***ADDED /info and /current. Check commandlist."
            "put text here later with updating"
        };
        return Regex.Replace(string.Join("\r\n\r\n", value), "(\\*+[^\\s]+)", "\n<color=red><b>$1</b></color>");
    }
    public static string SelectCMD;
    internal static string CmdDescription()
    {
        string key;
        switch (key = SelectCMD)
        {
            case "/slp":
                return "Blow away the player from map (added for test anti-slp)";
                //return "Group Private Chats.\n To select more than one player seperate the playerIDs with a comma ','\nTo make a group do: /create [GroupName] [playerID].\nTo flip back and forth from public chat to group chat do: /view or /v.\nTo switch groups do: /view [groupID].\nTo add people do: /group [GroupName or GroupID] [playerID].\nTo remove ppl do: /ungroup [GroupName or GroupID] [playerID].\nTo leave a group do: /ungroup [GroupName or GroupID] [yourPlayerID].\nTo leave all groups do: /cleargroups\nTo chat in groups press ENTER/TAB.\nThis can only work for people with the mod but it works with those without it too but then the person without the mod/updated deathmod will just be talking to everyone in the public chat and in ur pm as if he is talking to himself.";
                //case "/restart":
                //    return "/restart\n Restarts the game. HOST ONLY";
                //case "/setvice":
                //    return "/setvice [ID] [y/n]\n Set the ViceMC. The ViceMC can use MC commands as well as change the gamemodes for the server and also be able to kick people from the server.\n'y' for promoting them as ViceMC and 'n' for demoting them from ViceMC.";
                //case "/immune":
                //    return "/immune or /immune [y/n]\n Become immune to bullshit from other RS users. 'y' for turning on and 'n' for turning off. Or you can just type /immune for toggle.";
                //case "/modinfo":
                //    return "/modinfo or /mod\r\nShow the information of the current active gamemodes to everyone if you are MC. HOST ONLY";
                //case "/horse":
                //    return "/horse or /horse rc\r\nFor spawning and destroying your horse on switch. Works on any map. No need for MC. But since RC is uptight you will get kicked if you spawn a horse on maps other than where it is allowed if the RC player is host.\r\nBut if you are host you can use /horserc to enable horses for everyone if they have rc mod or this mod.";
                //case "/add":
                //    return "/add [ID] or /add [ID, ID, ID, etc]\r\n Add a name to the namelist.";
                //case "/crash":
                //    return "/crash [ID] or /crash [ID, ID, ID, etc]\n Crash a player's game to instantly kick them out of a server. Alternative for banning. HOST ONLY";
                //case "/dc":
                //    return "/dc [ID, ID, etc] [loop/bytes] or /dc [loop/bytes] [ID, ID, etc]\r\n Disconnect them from the game to instantly kick them out of a server. If 20k loop isn't enough then just raise the number, but the highest you can do is what your internet is capable of otherwise you will lag out and disconnect from the server. Alternative for kicking. HOST ONLY\nThe loop must have b, kb, mb, gb, tb, k or mil. the ones that end with B is byte size in which isn't a loop but instead sends the amount of bytes, K is thousand and mil is million. EX: /dc 9 20k /dc 20 15mil, /dc 5k 3 /dc 90mil 24, /dc 13 200kb, /dc 90 20gb, /dc 903 24tb\nHOST ONLY";
                //case "/crashlist":
                //    return "/crashlist\n Shows who is in the crashlist.";
                //case "/mute":
                //    return "/mute [ID] or /mute [ID, ID, ID, etc]\r\n Mute a player.";
                //case "/unmute":
                //    return "/unmute [ID] or /unmute [ID, ID, ID, etc]\r\n Unmute a player.";
                //case "/mutelist":
                //    return "/mutelist\n Shows who is in the mutelist.";
                //case "/erase":
                //    return "/erase [ID] or /erase [ID, ID, ID, etc]\r\n Erase a name out of the namelist.";
                //case "/rek":
                //    return "/rek [ID] or /rek [ID, ID, ID, etc]\r\n Destroy all game objects of the player.";
                //case "/kill":
                //    return "/kill [SPECIE] [ID] or /kill [SPECIE] [ID, ID, ID, ID, etc]\nKill a player. The specie is optional while the IDs you can input multiple by seperating each id with a comma. The specie you can input a for all species, h for all humans, t for titans, c for colossal, f for annie, or allt for all titans.\r\nExample:/kill h 380, 2, 3904, 204\r\nExample2:/kill a 9\r\nExample3:/kill t 14";
                //case "/current":
                //    return "/current \n Send the current GameModes to the players in the room.";
                //case "/info":
                //    return "/info [ID] or /info [name] or /info [ID, ID, ID, etc]\r\n Get information of the player.";
                //case "/setmc":
                //    return "/setmc [ID]\n Set the MasterClient player. HOST ONLY";
                //case "/time":
                //    return "/time [number] or /timereset\n Reset the server time or set the time to a certain value. HOST ONLY";
                //case "/roomlist":
                //    return "/roomlist\n View the room info in the top-right HUD.";
                //case "/kick":
                //    return "/kick [ID] or /kick [ID, ID, ID, etc]\r\n Kick a player. HOST ONLY";
                //case "/quit":
                //    return "/quit\n Close the game.";
                //case "/ban":
                //    return "/ban [ID] or /ban [ID, ID, ID, etc]\r\n Ban a player. (Player wont leave the game instantly but will be banned. Just wait for them to leave.)";
                //case "/unban":
                //    return "/unban [ID] or /unbam [ID, ID, ID, etc]\r\n Unban a player.";
                //case "/clear":
                //    return "/clear [ban/death/name/mute/crash/chat/all]\n Clear a List (banlist, deathlist, namelist, mutelist, crashlist, chat messages, or all).";
                //case "/mode":
                //    return "/mode [0/1/2/3]\nChange the difficulty. (0 = training, 1 = normal, 2 = hard, 3 = abnormal)";
                //case "/light":
                //    return "/light [1/2/3/4/5/6]\n Change the daylight. (1 = day, 2 = dawn, 3 = night, 4 = nightvision, 5 = limbo, 6 = sketch)";
                //case "/noguests":
                //    return "/noguests or /noguest\n Kicks all guests that join. If they are unkickable then it will crash them. If the guest isn't kicked then obviously it is not any ordinary guest. Like one with a colored name. HOST ONLY";
                //case "/revive":
                //    return "/revive\n Counters the /rek command. Revives your player and titans if all your game objects have been destroyed. Also has another use, if you died and a clone appears that is immobile then it will destroy your clones. Sometimes you may have to spam this command to get it to fully work to destroy a clone.";
                //case "/resetkd":
                //    return "/resetkd\n Resets your Kill/Death/MaxDmg/TotalDmg score.";
                //case "/maxplayers":
                //    return "/maxplayers [number]\n Change the maxplayers of the room you are in. HOST ONLY";
                //case "/spawn":
                //    return "/spawn [ID] or /spawn [ID, ID, ID, etc] or /spawn all\r\n Revive player(s) or all of them.";
                //case "/users":
                //    return "/users\r\n Grabs other the users using this mod in the server.";
                //case "/destroy":
                //    return "/destroy [ObjectName] or /destroy [OwnerID]:[ObjectName] or /destroy view[ViewID]:[ObjectName] or /destroy [OwnerID]:[ViewID]:[ObjectName]\n If you use the object list, you will find game objects and its viewID and its ownerID. Only one at a time though. Since the 'out of sync' error prevents a check for each game object. BTW THE OBJECT LIST WILL LAG YOUR GAME WHILE YOU PLAY.";
                //case "//s":
                //    return "//s [message]\n Send a message into chat without your name.";
                //case "//c":
                //    return "//c [message]\n Send a congrats message into chat without your name.";
                //case "//w":
                //    return "//w [message]\n Send a warning message into chat without your name.";
                //case "/me":
                //    return "/me [message]\n Send a third-person message into chat.";
                //case "/color":
                //    return "/color [color name/hex code]\n Change your chat color. If you want a hex value then just include the # before the hex.";
                //case "/pm":
                //    return "/pm[ID] [message] or /pm[ID, ID, ID, etc] [message]\r\n Send a private message to a player in the room.";
                //case "/namelist":
                //    return "/namelist\n View the list of saved names in the chat. The namelist is for preventing people from changing other's names.";
                //case "/banlist":
                //    return "/banlist\n View the list of banned players in the chat.";
                //case "/deathlist":
                //    return "/deathlist\n View the death list in the chat.";
                //case "/objlist":
                //    return "/objlist or /objectlist\n View the object list in the top-right HUD.";
                //case "/die":
                //    return "/die [ID] or /die [ID, ID, ID, etc]\r\n Keeps a player from respawning by adding them to the deathlist. HOST ONLY";
                //case "/live":
                //    return "/live [ID] or /live [ID, ID, ID, etc]\r\n Allows the player to respawn again by removing them from the deathlist. HOST ONLY";
                //case "/antisteal":
                //    return "/antisteal\n Prevents another player from stealing MC.";
                //case "/antiname":
                //    return "/antiname\n Prevents another player from changing another player's name.";
                //case "/afk":
                //    return "/afk\n Kills AFK or inactive players. It checks each player individually instead of one by one. Now it can check if the player is just spinning in circles too. 20 seconds if in same spot. 60 seconds if only spinning around an object. HOST ONLY";
                //case "/duelspd":
                //    return "/duelspd [number]\n Speedkilling Duels with Death Rule. No Need For Host. ONLY SURVIVE WAVES.";
                //case "/dueldmg":
                //    return "/dueldmg [time]\n Damage Duels with Death Rule. No Need For Host. Set the time for until the victor is decided.";
                //case "/dueltotal":
                //    return "/dueltotaldmg [number] or /dueltotal [number]\n Total Damage Duels with Death Rule. No Need For Host. Set the total damage needed to reach or surpass to win.";
                //case "/nocrawler":
                //    return "/nocrawler or /nocrawlers\n No Crawler Mode. HOST ONLY";
                //case "/heat":
                //    return "/heat\n Heatblaze. Titans let out lethal CT gas upon attack. Speedkill to win without letting a single titan attack. HOST ONLY";
                //case "/ice":
                //    return "/ice\n Icefields. Titans run faster and slide. HOST ONLY";
                //case "/zerog":
                //    return "/zerog\n Zero-Gravity Crawlers. Crawlers can fly. HOST ONLY";
                //case "/endless":
                //    return "/endless\n Endless Waves. Reach (7 + the wave number) of titans to hit next wave. HOST ONLY";
                //case "/lock":
                //    return "/lock [number]\n Number Lock. Locks titan count to one number. HOST ONLY";
                //case "/size":
                //    return "/size [number] [number] or /size [number]\n Change the titan size when spawned. IN METERS (If you do /size [number] then it will only spawn titans of that number and not in a range of between and between.)";
                //case "/waves":
                //    return "/[number] waves or /waves [number]\n Number of waves to clear. ONLY SURVIVE WAVES.";
                //case "/shifter":
                //    return "/shifter\n Shifter Titans. Titans can transform into either crawlers, punks, or jumpers. They all have randomized speeds so they dont run the same speed. They have annie grabs. They can attack while chasing. HOST ONLY";
                //case "/crawler":
                //    return "/crawler or /crawlers or /allcrawler or /allcrawlers\n All Crawlers. Only Crawlers will spawn. HOST ONLY";
                //case "/punks":
                //    return "/punks or /punk or /allpunk or /allpunks\n All Punks. Only Punks will spawn. HOST ONLY";
                //case "/normal":
                //    return "/norm or /norms or /normal or /normals or /allnorm or /allnormal \n All Punks. Only Normal Titans will spawn. HOST ONLY";
                //case "/abs":
                //    return "/abs or /ab or /allab or /allabs\n All Punks. Only Aberrants will spawn. HOST ONLY";
                //case "/jumpers":
                //    return "/jumper or /jumpers or /jump or /jumps or /alljumper or /alljumpers\n All Punks. Only Punks will spawn. HOST ONLY";
                //case "/teleport":
                //    return "/teleport or /tele\n Teleporting Titans. Titans will teleport to a human at random. HOST ONLY";
                //case "/nopunks":
                //    return "/nopunk or /nopunks\n No Punks will spawn.";
                //case "/noabs":
                //    return "/noab or /noabs\n No Abberants will spawn.";
                //case "/nojumpers":
                //    return "/nojumper or /nojumpers or /nojump or /nojumps\n No Abnormals will spawn.";
                //case "/nonormals":
                //    return "/nonorm or /nonorms or /nonormal or /nonormals\n No Normals will spawn.";
                //case "/untouch":
                //    return "/untouch or /untouchable\n Untouchable Titans. Touch a titan you die. Only safe spot is nape. Perfect for normal mode pros or damage players.";
                //case "/damage":
                //    return "/damage [number] or /dmg [number]\n RC Damage Mode. To kill a titan, you will have to aim for the damage specified or higher. Syncs with RC.";
                //case "/bomb":
                //    return "/bomb\n RC PVP Bombs. To give your bomb a color or change its stats just go to\r\n\tF1 Menu > More > Abilities.\r\nStill syncs with RC.";
                //case "/hvh":
                //    return "/hvh or /ffa or /ffa team or /hvh team\n RC Blade/AHSS PVP with all humans against each other\n. /hvh team or /hvh 1 to turn on Team FFA.\n /hvh to just do a normal free for all. Syncs With RC mod.";
                //case "/respawn":
                //    return "/respawn [seconds]\n Endless Respawn. Syncs With RC mod.";
                //case "/health":
                //    return "/health [HP] or /health [minHP]-[maxHP] or /health [fix/scale] [HP] or /health [fix/scale] [minHP]-[maxHP]\n Nape Health. If you choose scaled HP, then it will scale the HP based on the titan's size, if you choose fixed then it won't scale the titan's HP based on its size. If you input only one and not two numbers then it will make both numbers the same. Syncs With RC mod.";
                //case "/annie":
                //    return "/annie [number]\n Only annies will spawn for survive waves. Set the HP she will have as well.";
                //case "/pvp":
                //    return "/pvp or /friendly\n Turn pvp for AHSS and Humans off or on. They won't be able to kill you with guns. HOST ONLY";
        }
        return string.Empty;
    }

    [RPC]
    private void respawnHeroInNewRound()
    {
        if (!this.needChooseSide && IN_GAME_MAIN_CAMERA.mainCamera.gameOver)
        {
            this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    public IEnumerator restartE(float time)
    {
        yield return new WaitForSeconds(time);
        this.restartGame2(false);
    }

    public void restartGame(bool masterclientSwitched)
    {
        UnityEngine.MonoBehaviour.print("reset game :" + this.gameTimesUp);
        if (!this.gameTimesUp)
        {
            this.PVPtitanScore = 0;
            this.PVPhumanScore = 0;
            this.startRacing = false;
            this.endRacing = false;
            this.checkpoint = null;
            this.timeElapse = 0f;
            this.roundTime = 0f;
            this.isWinning = false;
            this.isLosing = false;
            this.isPlayer1Winning = false;
            this.isPlayer2Winning = false;
            this.wave = 1;
            this.myRespawnTime = 0f;
            this.kicklist = new ArrayList();
            this.killInfoGO = new ArrayList();
            this.racingResult = new ArrayList();
            this.ShowHUDInfoCenter(string.Empty);
            PhotonNetwork.DestroyAll();
            PView.RPC("RPCLoadLevel", PhotonTargets.All, new object[0]);
            if (masterclientSwitched)
            {
                this.sendChatContentInfo("<color=#A8FF24>MasterClient has switched to </color>" + PhotonNetwork.player.uiname.ToRGBA());
            }
        }
    }

    public void restartGame2(bool masterclientSwitched)
    {
        Photon.MonoBehaviour.print("reset game :" + this.gameTimesUp);
        if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP && !this.gameTimesUp)
        {
            this.PVPtitanScore = 0;
            this.PVPhumanScore = 0;
            this.startRacing = false;
            this.endRacing = false;
            this.checkpoint = null;
            this.timeElapse = 0f;
            this.roundTime = 0f;
            this.isWinning = false;
            this.isLosing = false;
            this.isPlayer1Winning = false;
            this.isPlayer2Winning = false;
            this.wave = 1;
            this.myRespawnTime = 0f;
            this.kicklist = new ArrayList();
            this.killInfoGO = new ArrayList();
            this.racingResult = new ArrayList();
            this.ShowHUDInfoCenter(string.Empty);
            this.isRestarting = true;
            this.DestroyAllExistingCloths();
            PhotonNetwork.DestroyAll();
            //ExitGames.Client.Photon.Hashtable hash = this.;
            PView.RPC("settingRPC", PhotonTargets.Others, new object[] { checkGameGUI("out") });
            PView.RPC("RPCLoadLevel", PhotonTargets.All, new object[0]);
            this.setGameSettings(checkGameGUI());
            if (masterclientSwitched)
            {
                this.sendChatContentInfo("<color=#b5ceff>MasterClient has switched to </color>" + PhotonNetwork.player.uiname.ToRGBA());
                //string context = string.Empty;
                //foreach (string Mode in ActiveGameModes)
                //{
                //    context += "\n" + Mode;
                //}
                //object[] parameters = new object[] { context, string.Empty };
                //PView.RPC("Chat", PhotonTargets.All, parameters);
            }
        }
    }

    [RPC]
    private void restartGameByClient()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.restartGame2(false);
        }
    }

    public void restartGameSingle()
    {
        this.startRacing = false;
        this.endRacing = false;
        this.checkpoint = null;
        this.single_kills = 0;
        this.single_maxDamage = 0;
        this.single_totalDamage = 0;
        this.timeElapse = 0f;
        this.roundTime = 0f;
        this.timeTotalServer = 0f;
        this.isWinning = false;
        this.isLosing = false;
        this.isPlayer1Winning = false;
        this.isPlayer2Winning = false;
        this.wave = 1;
        this.myRespawnTime = 0f;
        this.ShowHUDInfoCenter(string.Empty);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartGameSingle2()
    {
        this.startRacing = false;
        this.endRacing = false;
        this.checkpoint = null;
        this.single_kills = 0;
        this.single_maxDamage = 0;
        this.single_totalDamage = 0;
        this.timeElapse = 0f;
        this.roundTime = 0f;
        this.timeTotalServer = 0f;
        this.isWinning = false;
        this.isLosing = false;
        this.isPlayer1Winning = false;
        this.isPlayer2Winning = false;
        this.wave = 1;
        this.myRespawnTime = 0f;
        this.ShowHUDInfoCenter(string.Empty);
        this.DestroyAllExistingCloths();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void restartRC()
    {
        intVariables.Clear();
        boolVariables.Clear();
        stringVariables.Clear();
        floatVariables.Clear();
        playerVariables.Clear();
        titanVariables.Clear();
        if (GameSettings.infectionMode > 0)
        {
            this.endGameInfectionRC();
        }
        else
        {
            this.endGameRC();
        }
    }

    public RCActionHelper returnHelper(string str)
    {
        float num;
        int num3;
        string[] strArray = str.Split(new char[] { '.' });
        if (float.TryParse(str, out num))
        {
            strArray = new string[] { str };
        }
        List<RCActionHelper> list = new List<RCActionHelper>();
        int sentType = 0;
        for (num3 = 0; num3 < strArray.Length; num3++)
        {
            string str2;
            RCActionHelper helper;
            if (list.Count == 0)
            {
                str2 = strArray[num3];
                if (str2.StartsWith("\"") && str2.EndsWith("\""))
                {
                    helper = new RCActionHelper(0, 0, str2.Substring(1, str2.Length - 2));
                    list.Add(helper);
                    sentType = 2;
                }
                else
                {
                    int num4;
                    if (int.TryParse(str2, out num4))
                    {
                        helper = new RCActionHelper(0, 0, num4);
                        list.Add(helper);
                        sentType = 0;
                    }
                    else
                    {
                        float num5;
                        if (float.TryParse(str2, out num5))
                        {
                            helper = new RCActionHelper(0, 0, num5);
                            list.Add(helper);
                            sentType = 3;
                        }
                        else if ((str2.ToLower() == "true") || (str2.ToLower() == "false"))
                        {
                            helper = new RCActionHelper(0, 0, Convert.ToBoolean(str2.ToLower()));
                            list.Add(helper);
                            sentType = 1;
                        }
                        else
                        {
                            int index;
                            int num7;
                            if (str2.StartsWith("Variable"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("VariableInt"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 0, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 0;
                                }
                                else if (str2.StartsWith("VariableBool"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 1, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 1;
                                }
                                else if (str2.StartsWith("VariableString"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 2, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 2;
                                }
                                else if (str2.StartsWith("VariableFloat"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 3, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("VariablePlayer"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 4, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 4;
                                }
                                else if (str2.StartsWith("VariableTitan"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(1, 5, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 5;
                                }
                            }
                            else if (str2.StartsWith("Region"))
                            {
                                index = str2.IndexOf('(');
                                num7 = str2.LastIndexOf(')');
                                if (str2.StartsWith("RegionRandomX"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 0, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomY"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 1, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                                else if (str2.StartsWith("RegionRandomZ"))
                                {
                                    str2 = str2.Substring(index + 1, (num7 - index) - 1);
                                    helper = new RCActionHelper(4, 2, this.returnHelper(str2));
                                    list.Add(helper);
                                    sentType = 3;
                                }
                            }
                        }
                    }
                }
                continue;
            }
            if (list.Count <= 0)
            {
                continue;
            }
            str2 = strArray[num3];
            if (list[list.Count - 1].helperClass != 1)
            {
                goto Label_0AF5;
            }
            switch (list[list.Count - 1].helperType)
            {
                case 4:
                    {
                        if (!str2.StartsWith("GetTeam()"))
                        {
                            break;
                        }
                        helper = new RCActionHelper(2, 1, null);
                        list.Add(helper);
                        sentType = 0;
                        continue;
                    }
                case 5:
                    {
                        if (!str2.StartsWith("GetType()"))
                        {
                            goto Label_0918;
                        }
                        helper = new RCActionHelper(3, 0, null);
                        list.Add(helper);
                        sentType = 0;
                        continue;
                    }
                default:
                    goto Label_0A1C;
            }
            if (str2.StartsWith("GetType()"))
            {
                helper = new RCActionHelper(2, 0, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetIsAlive()"))
            {
                helper = new RCActionHelper(2, 2, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetTitan()"))
            {
                helper = new RCActionHelper(2, 3, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetKills()"))
            {
                helper = new RCActionHelper(2, 4, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetDeaths()"))
            {
                helper = new RCActionHelper(2, 5, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetMaxDmg()"))
            {
                helper = new RCActionHelper(2, 6, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetTotalDmg()"))
            {
                helper = new RCActionHelper(2, 7, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomInt()"))
            {
                helper = new RCActionHelper(2, 8, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetCustomBool()"))
            {
                helper = new RCActionHelper(2, 9, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("GetCustomString()"))
            {
                helper = new RCActionHelper(2, 10, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetCustomFloat()"))
            {
                helper = new RCActionHelper(2, 11, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(2, 14, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(2, 15, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(2, 0x10, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetName()"))
            {
                helper = new RCActionHelper(2, 12, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetGuildName()"))
            {
                helper = new RCActionHelper(2, 13, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("GetSpeed()"))
            {
                helper = new RCActionHelper(2, 0x11, null);
                list.Add(helper);
                sentType = 3;
            }
            continue;
        Label_0918:
            if (str2.StartsWith("GetSize()"))
            {
                helper = new RCActionHelper(3, 1, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetHealth()"))
            {
                helper = new RCActionHelper(3, 2, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("GetPositionX()"))
            {
                helper = new RCActionHelper(3, 3, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionY()"))
            {
                helper = new RCActionHelper(3, 4, null);
                list.Add(helper);
                sentType = 3;
            }
            else if (str2.StartsWith("GetPositionZ()"))
            {
                helper = new RCActionHelper(3, 5, null);
                list.Add(helper);
                sentType = 3;
            }
            continue;
        Label_0A1C:
            if (str2.StartsWith("ConvertToInt()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("ConvertToBool()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("ConvertToString()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("ConvertToFloat()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 3;
            }
            continue;
        Label_0AF5:
            if (str2.StartsWith("ConvertToInt()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 0;
            }
            else if (str2.StartsWith("ConvertToBool()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 1;
            }
            else if (str2.StartsWith("ConvertToString()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 2;
            }
            else if (str2.StartsWith("ConvertToFloat()"))
            {
                helper = new RCActionHelper(5, sentType, null);
                list.Add(helper);
                sentType = 3;
            }
        }
        for (num3 = list.Count - 1; num3 > 0; num3--)
        {
            list[num3 - 1].setNextHelper(list[num3]);
        }
        return list[0];
    }

    public static PeerStates returnPeerState(int peerstate)
    {
        switch (peerstate)
        {
            case 0:
                return PeerStates.Authenticated;

            case 1:
                return PeerStates.ConnectedToMaster;

            case 2:
                return PeerStates.DisconnectingFromMasterserver;

            case 3:
                return PeerStates.DisconnectingFromGameserver;

            case 4:
                return PeerStates.DisconnectingFromNameServer;
        }
        return PeerStates.ConnectingToMasterserver;
    }

    //[RPC]
    //private void RPCLoadLevel(PhotonMessageInfo info)
    //{
    //    if (info.sender.isMasterClient)
    //    {
    //        this.DestroyAllExistingCloths();
    //        PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
    //    }
    //    else if (PhotonNetwork.isMasterClient)
    //    {
    //        this.kickPlayerRC(info.sender, true, "false restart.");
    //    }
    //    else if (!masterRC)
    //    {
    //        this.restartCount.Add(Time.time);
    //        foreach (float num in this.restartCount)
    //        {
    //            if ((Time.time - num) > 60f)
    //            {
    //                this.restartCount.Remove(num);
    //            }
    //        }
    //        if (this.restartCount.Count < 6)
    //        {
    //            this.DestroyAllExistingCloths();
    //            PhotonNetwork.LoadLevel(LevelInfo.getInfo(level).mapName);
    //        }
    //    }
    //}

    public void sendChatContentInfo(string content)
    {
        object[] parameters = new object[] { content, string.Empty };
        PView.RPC("Chat", PhotonTargets.All, parameters);
    }

    public void sendKillInfo(bool t1, string killer, bool t2, string victim, int dmg)
    {
        object[] parameters = new object[] { t1, killer, t2, victim, dmg };
        PView.RPC("updateKillInfo", PhotonTargets.All, parameters);
    }

    public static void ServerCloseConnection(PhotonPlayer targetPlayer, bool requestIpBan, string inGameName)
    {
        RaiseEventOptions options = new RaiseEventOptions
        {
            TargetActors = new int[] { targetPlayer.ID }
        };
        if (requestIpBan)
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = true;
            if ((inGameName != null) && (inGameName.Length > 0))
            {
                eventContent[(byte)1] = inGameName;
            }
            PhotonNetwork.RaiseEvent(0xcb, eventContent, true, options);
        }
        else
        {
            PhotonNetwork.RaiseEvent(0xcb, null, true, options);
        }
    }

    public static void ServerRequestAuthentication(string authPassword)
    {
        if (!string.IsNullOrEmpty(authPassword))
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = authPassword;
            PhotonNetwork.RaiseEvent(0xc6, eventContent, true, new RaiseEventOptions());
        }
    }

    public static void ServerRequestUnban(string bannedAddress)
    {
        if (!string.IsNullOrEmpty(bannedAddress))
        {
            ExitGames.Client.Photon.Hashtable eventContent = new ExitGames.Client.Photon.Hashtable();
            eventContent[(byte)0] = bannedAddress;
            PhotonNetwork.RaiseEvent(0xc7, eventContent, true, new RaiseEventOptions());
        }
    }

    //public void setBackground()
    //{
    //    if (isAssetLoaded)
    //    {
    //        UnityEngine.Object.Instantiate(RCassets.Load("backgroundCamera"));
    //    }
    //}

    private void setGameSettings(ExitGames.Client.Photon.Hashtable hash)
    {
        string str;

        ExitGames.Client.Photon.Hashtable hashtable;
        this.restartingEren = false;
        this.restartingBomb = false;
        this.restartingHorse = false;
        this.restartingTitan = false;

        if (hash.ContainsKey("bomb"))
        {
            if (GameSettings.bombMode != ((int)hash["bomb"]))
            {
                GameSettings.bombMode = (int)hash["bomb"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">PVP Bomb Mode enabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>~RC PVP BOMB~</color></b> <color=#7b001c><i>is on.</i></color>");
            }
        }
        else if (GameSettings.bombMode != 0)
        {
            GameSettings.bombMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">PVP Bomb Mode disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>~RC PVP BOMB~</color></b> <color=#7b001c><i>is on.</i></color>");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingBomb = true;
            }
        }
        if (hash.ContainsKey("globalDisableMinimap"))
        {
            if (GameSettings.globalDisableMinimap != ((int)hash["globalDisableMinimap"]))
            {
                GameSettings.globalDisableMinimap = (int)hash["globalDisableMinimap"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Minimaps are not allowed.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Minimaps are </color></b> <color=#7b001c><i>not allowed.</i></color>");
                if ((int)hash["globalDisableMinimap"] == 0)
                {
                    GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopRight");
                    obj2.transform.localPosition = new Vector3((float)((int)(Screen.width * 0.35f)), (float)((int)(Screen.height * 0.5f)), 0f);
                }

            }
        }
        else if (GameSettings.globalDisableMinimap != 0)
        {
            GameSettings.globalDisableMinimap = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Minimaps are allowed.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>Minimaps are </color></b> <color=#7b001c><i>not allowed.</i></color>");
        }
        if (hash.ContainsKey("horse"))
        {
            if (GameSettings.horseMode != ((int)hash["horse"]))
            {
                GameSettings.horseMode = (int)hash["horse"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Horses enabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Horses are </color></b> <color=#7b001c><i>allowed.</i></color>");
            }
        }
        else if (GameSettings.horseMode != 0)
        {
            GameSettings.horseMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Horses disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Horses are </color></b> <color=#7b001c><i>allowed.</i></color>");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingHorse = true;
            }
        }
        //if (hash.ContainsKey("noguest"))
        //{
        //    if (GameSettings.noguest != ((int)hash["noguest"]))
        //    {
        //        GameSettings.noguest = (int)hash["noguest"];
        //        //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Horses enabled.</color></b>");
        //        //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Horses are </color></b> <color=#7b001c><i>allowed.</i></color>");
        //    }
        //}
        //else if (GameSettings.noguest != 0)
        //{
        //    GameSettings.noguest = 0;
        //    //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Horses disabled.</color></b>");
        //    //ActiveGameModes.Remove("<b><color=#b5ceff>RC Horses are </color></b> <color=#7b001c><i>allowed.</i></color>");           
        //}
        if (hash.ContainsKey("punkWaves"))
        {
            if (GameSettings.punkWaves != ((int)hash["punkWaves"]))
            {
                GameSettings.punkWaves = (int)hash["punkWaves"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Punk override every 5 waves enabled.</color></b>");
            }
        }
        else if (GameSettings.punkWaves != 0)
        {
            GameSettings.punkWaves = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Punk override every 5 waves disabled.</color></b>");
        }
        if (hash.ContainsKey("ahssReload"))
        {
            if (GameSettings.ahssReload != ((int)hash["ahssReload"]))
            {
                GameSettings.ahssReload = (int)hash["ahssReload"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">AHSS Air-Reload disabled.</color></b>");
                //ActiveGameModes.Remove("<b><color=#b5ceff>AHSS Air-Reload</color></b> <color=#7b001c><i> disabled.</i></color>");
            }
        }
        else if (GameSettings.ahssReload != 0)
        {
            GameSettings.ahssReload = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">AHSS Air-Reload allowed.</color></b>");
            //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>AHSS Air-Reload</color></b> <color=#7b001c><i> allowed.</i></color>");
        }
        if (hash.ContainsKey("team"))
        {
            if (GameSettings.teamMode != ((int)hash["team"]))
            {
                GameSettings.teamMode = (int)hash["team"];
                str = string.Empty;
                if (GameSettings.teamMode == 1)
                {
                    str = "no sort";
                }
                else if (GameSettings.teamMode == 2)
                {
                    str = "locked by size";
                }
                else if (GameSettings.teamMode == 3)
                {
                    str = "locked by skill";
                }
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Team Mode enabled (" + str + ").</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Team Mode (" + str + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
                if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
                {
                    this.setTeam(3);
                }
            }
        }
        else if (GameSettings.teamMode != 0)
        {
            GameSettings.teamMode = 0;
            this.setTeam(0);
            str = string.Empty;
            if (GameSettings.teamMode == 1)
            {
                str = "no sort";
            }
            else if (GameSettings.teamMode == 2)
            {
                str = "locked by size";
            }
            else if (GameSettings.teamMode == 3)
            {
                str = "locked by skill";
            }
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Team mode disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Team Mode (" + str + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (hash.ContainsKey("point"))
        {
            if (GameSettings.pointMode != ((int)hash["point"]))
            {
                GameSettings.pointMode = (int)hash["point"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Point limit enabled (" + Convert.ToString(GameSettings.pointMode) + ").</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Point limit (" + Convert.ToString(GameSettings.pointMode) + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
            }
        }
        else if (GameSettings.pointMode != 0)
        {
            GameSettings.pointMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Point limit disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Point limit (" + Convert.ToString(GameSettings.pointMode) + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (hash.ContainsKey("rock"))
        {
            if (GameSettings.disableRock != ((int)hash["rock"]))
            {
                GameSettings.disableRock = (int)hash["rock"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Punk rock throwing disabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Punk rock throwing</color></b> <color=#7b001c><i> disabled.</i></color>");
            }
        }
        else if (GameSettings.disableRock != 0)
        {
            GameSettings.disableRock = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Punk rock throwing enabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>Punk rock throwing</color></b> <color=#7b001c><i> disabled.</i></color>");
        }
        if (hash.ContainsKey("explode"))
        {
            if (GameSettings.explodeMode != ((int)hash["explode"]))
            {
                GameSettings.explodeMode = (int)hash["explode"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Titan Explode Mode enabled (Radius " + Convert.ToString(GameSettings.explodeMode) + ").</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Titan Explode Mode</color></b> <color=#7b001c><i> enabled.</i></color> <b><color=#b5ceff>(Radius " + Convert.ToString(GameSettings.explodeMode) + ")</color></b>");
            }
        }
        else if (GameSettings.explodeMode != 0)
        {
            GameSettings.explodeMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Titan Explode Mode disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>Titan Explode Mode</color></b> <color=#7b001c><i> enabled.</i></color> <b><color=#b5ceff>(Radius " + Convert.ToString(GameSettings.explodeMode) + ")</color></b>");
        }
        if ((hash.ContainsKey("healthMode") && hash.ContainsKey("healthLower")) && hash.ContainsKey("healthUpper"))
        {
            if (((GameSettings.healthMode != ((int)hash["healthMode"])) || (GameSettings.healthLower != ((int)hash["healthLower"]))) || (GameSettings.healthUpper != ((int)hash["healthUpper"])))
            {
                GameSettings.healthMode = (int)hash["healthMode"];
                GameSettings.healthLower = (int)hash["healthLower"];
                GameSettings.healthUpper = (int)hash["healthUpper"];
                str = "Static";
                if (GameSettings.healthMode == 2)
                {
                    str = "Scaled";
                }
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Titan Health (" + str + ", " + GameSettings.healthLower.ToString() + " to " + GameSettings.healthUpper.ToString() + ") enabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Nape Health </color> <color=#" + MainColor + ">(<color=#b5ceff>" + str + ", </color><color=#7b001c><i>" + GameSettings.healthLower.ToString() + "</i></color><color=#b5ceff> to </color><color=#7b001c><i>" + GameSettings.healthUpper.ToString() + "</i></color>)</color></b> ");
            }
        }
        else if (((GameSettings.healthMode != 0) || (GameSettings.healthLower != 0)) || (GameSettings.healthUpper != 0))
        {
            str = "Static";
            if (GameSettings.healthMode == 2)
            {
                str = "Scaled";
            }
            GameSettings.healthMode = 0;
            GameSettings.healthLower = 0;
            GameSettings.healthUpper = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Titan Health disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Nape Health </color> <color=#" + MainColor + ">(<color=#b5ceff>" + str + ", </color><color=#7b001c><i>" + GameSettings.healthLower.ToString() + "</i></color><color=#b5ceff> to </color><color=#7b001c><i>" + GameSettings.healthUpper.ToString() + "</i></color>)</color></b> ");
        }
        if (hash.ContainsKey("infection"))
        {
            if (GameSettings.infectionMode != ((int)hash["infection"]))
            {
                GameSettings.infectionMode = (int)hash["infection"];
                this.name = LoginFengKAI.player.name;
                hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable.Add(PhotonPlayerProperty.RCteam, 0);
                PhotonNetwork.player.SetCustomProperties(hashtable);
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Infection mode (" + Convert.ToString(GameSettings.infectionMode) + ") enabled. Make sure your first character is human.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Infection mode</color></b> <color=#7b001c><i> enabled.</i></color>");
            }
        }
        else if (GameSettings.infectionMode != 0)
        {
            GameSettings.infectionMode = 0;
            hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 1);
            PhotonNetwork.player.SetCustomProperties(hashtable);
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Infection Mode disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Infection mode</color></b> <color=#7b001c><i> enabled.</i></color>");
            if (PhotonNetwork.isMasterClient)
            {
                this.restartingTitan = true;
            }
        }
        if (hash.ContainsKey("eren"))
        {
            if (GameSettings.banEren != ((int)hash["eren"]))
            {
                GameSettings.banEren = (int)hash["eren"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Anti-Eren enabled. Using eren transform will get you kicked.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Anti-Eren</color></b> <color=#7b001c><i> enabled.</i></color>");
                if (PhotonNetwork.isMasterClient)
                {
                    this.restartingEren = true;
                }
            }
        }
        else if (GameSettings.banEren != 0)
        {
            GameSettings.banEren = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Anti-Eren disabled. Eren transform is allowed.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>Anti-Eren</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if (hash.ContainsKey("titanc"))
        {
            if (GameSettings.moreTitans != ((int)hash["titanc"]))
            {
                GameSettings.moreTitans = (int)hash["titanc"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">" + Convert.ToString(GameSettings.moreTitans) + " titans will spawn each round.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add(" <color=#7b001c><i>" + Convert.ToString(GameSettings.moreTitans) + "</i></color> <b><color=#b5ceff> titans will spawn each round.</color></b>");
            }
        }
        else if (GameSettings.moreTitans != 0)
        {
            GameSettings.moreTitans = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Default titans will spawn each round.</color></b>");
            //ActiveGameModes.Remove(" <color=#7b001c><i>" + Convert.ToString(GameSettings.moreTitans) + "</i></color> <b><color=#b5ceff> titans will spawn each round.</color></b>");
        }
        if (hash.ContainsKey("damage"))
        {
            if (GameSettings.damageMode != ((int)hash["damage"]))
            {
                GameSettings.damageMode = (int)hash["damage"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Nape minimum damage (" + Convert.ToString(GameSettings.damageMode) + ") enabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Nape minimum damage</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(GameSettings.damageMode) + "</i></color>" + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
            }
        }
        else if (GameSettings.damageMode != 0)
        {
            GameSettings.damageMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Nape minimum damage disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Nape minimum damage</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(GameSettings.damageMode) + "</i></color>" + ")</color></b> <color=#7b001c><i> enabled.</i></color>");
        }
        if ((hash.ContainsKey("sizeMode") && hash.ContainsKey("sizeLower")) && hash.ContainsKey("sizeUpper"))
        {
            if (((GameSettings.sizeMode != ((int)hash["sizeMode"])) || (GameSettings.sizeLower != ((float)hash["sizeLower"]))) || (GameSettings.sizeUpper != ((float)hash["sizeUpper"])))
            {
                GameSettings.sizeMode = (int)hash["sizeMode"];
                GameSettings.sizeLower = (float)hash["sizeLower"];
                GameSettings.sizeUpper = (float)hash["sizeUpper"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom titan size (" + GameSettings.sizeLower.ToString("F2") + "," + GameSettings.sizeUpper.ToString("F2") + ") enabled.</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>Titans of sizes between </color><color=#7b001c><i>" + GameSettings.sizeLower.ToString("F2") + "</i></color> <color=#b5ceff>and</color> <color=#7b001c><i>" + GameSettings.sizeUpper.ToString("F2") + "</i></color><color=#b5ceff> meter(s)</color></b>");
            }
        }
        else if (((GameSettings.sizeMode != 0) || (GameSettings.sizeLower != 0f)) || (GameSettings.sizeUpper != 0f))
        {
            GameSettings.sizeMode = 0;
            GameSettings.sizeLower = 0f;
            GameSettings.sizeUpper = 0f;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom titan size disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>Titans of sizes between </color><color=#7b001c><i>" + GameSettings.sizeLower.ToString("F2") + "</i></color> <color=#b5ceff>and</color> <color=#7b001c><i>" + GameSettings.sizeUpper.ToString("F2") + "</i></color><color=#b5ceff> meter(s)</color></b>");

        }
        if (((hash.ContainsKey("spawnMode") && hash.ContainsKey("nRate")) && (hash.ContainsKey("aRate") && hash.ContainsKey("jRate"))) && (hash.ContainsKey("cRate") && hash.ContainsKey("pRate")))
        {
            if ((((GameSettings.spawnMode != ((int)hash["spawnMode"])) || (GameSettings.nRate != ((float)hash["nRate"]))) || ((GameSettings.aRate != ((float)hash["aRate"])) || (GameSettings.jRate != ((float)hash["jRate"])))) || ((GameSettings.cRate != ((float)hash["cRate"])) || (GameSettings.pRate != ((float)hash["pRate"]))))
            {
                GameSettings.spawnMode = (int)hash["spawnMode"];
                GameSettings.nRate = (float)hash["nRate"];
                GameSettings.aRate = (float)hash["aRate"];
                GameSettings.jRate = (float)hash["jRate"];
                GameSettings.cRate = (float)hash["cRate"];
                GameSettings.pRate = (float)hash["pRate"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom spawn rate enabled (" + GameSettings.nRate.ToString("F2") + "% Normal, " + GameSettings.aRate.ToString("F2") + "% Abnormal, " + GameSettings.jRate.ToString("F2") + "% Jumper, " + GameSettings.cRate.ToString("F2") + "% Crawler, " + GameSettings.pRate.ToString("F2") + "% Punk </color></b>");
            }
        }
        else if ((((GameSettings.spawnMode != 0) || (GameSettings.nRate != 0f)) || ((GameSettings.aRate != 0f) || (GameSettings.jRate != 0f))) || ((GameSettings.cRate != 0f) || (GameSettings.pRate != 0f)))
        {
            GameSettings.spawnMode = 0;
            GameSettings.nRate = 0f;
            GameSettings.aRate = 0f;
            GameSettings.jRate = 0f;
            GameSettings.cRate = 0f;
            GameSettings.pRate = 0f;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom spawn rate disabled.</color></b>");
        }
        if (hash.ContainsKey("waveModeOn") && hash.ContainsKey("waveModeNum"))
        {
            if ((GameSettings.waveModeOn != ((int)hash["waveModeOn"])) || (GameSettings.waveModeNum != ((int)hash["waveModeNum"])))
            {
                GameSettings.waveModeOn = (int)hash["waveModeOn"];
                GameSettings.waveModeNum = (int)hash["waveModeNum"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom wave mode (" + GameSettings.waveModeNum.ToString() + ") enabled.</color></b>");
            }
        }
        else if ((GameSettings.waveModeOn != 0) || (GameSettings.waveModeNum != 0))
        {
            GameSettings.waveModeOn = 0;
            GameSettings.waveModeNum = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Custom wave mode disabled.</color></b>");
        }
        if (hash.ContainsKey("friendly"))
        {
            if (GameSettings.friendlyMode != ((int)hash["friendly"]))
            {
                GameSettings.friendlyMode = (int)hash["friendly"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">PVP is prohibited.</color></b>");
            }
        }
        else if (GameSettings.friendlyMode != 0)
        {
            GameSettings.friendlyMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">PVP is allowed.</color></b>");
        }
        if (hash.ContainsKey("pvp"))
        {
            if (GameSettings.pvpMode != ((int)hash["pvp"]))
            {
                GameSettings.pvpMode = (int)hash["pvp"];
                str = string.Empty;
                if (GameSettings.pvpMode == 1)
                {
                    str = "Team-Based";
                }
                else if (GameSettings.pvpMode == 2)
                {
                    str = "FFA";
                }
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Blade/AHSS PVP enabled (" + str + ").</color></b>");
            }
        }
        else if (GameSettings.pvpMode != 0)
        {
            GameSettings.pvpMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Blade/AHSS PVP disabled.</color></b>");
        }
        if (hash.ContainsKey("maxwave"))
        {
            if (GameSettings.maxWave != ((int)hash["maxwave"]))
            {
                GameSettings.maxWave = (int)hash["maxwave"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Max wave is " + GameSettings.maxWave.ToString() + ".</color></b>");
            }
        }
        else if (GameSettings.maxWave != 0)
        {
            GameSettings.maxWave = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Max wave set to default.</color></b>");
        }
        if (hash.ContainsKey("endless"))
        {
            if (GameSettings.endlessMode != ((int)hash["endless"]))
            {
                GameSettings.endlessMode = (int)hash["endless"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Endless respawn enabled (" + GameSettings.endlessMode.ToString() + " seconds).</color></b>");
                //if (PhotonNetwork.isMasterClient) ActiveGameModes.Add("<b><color=#b5ceff>RC Endless Respawn</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(GameSettings.endlessMode) + "</i></color> <color=#b5ceff>seconds</color>" + ")</color></b>");
            }
        }
        else if (GameSettings.endlessMode != 0)
        {
            GameSettings.endlessMode = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Endless respawn disabled.</color></b>");
            //ActiveGameModes.Remove("<b><color=#b5ceff>RC Endless Respawn</color> <color=#" + MainColor + ">(" + "<color=#7b001c><i>" + Convert.ToString(GameSettings.damageMode) + "</i></color> <color=#b5ceff>seconds</color>" + ")</color></b>");
        }
        if (hash.ContainsKey("motd"))
        {
            if (GameSettings.motd != ((string)hash["motd"]))
            {
                GameSettings.motd = (string)hash["motd"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">MOTD</color></b><b><color=#" + MainColor + "> | </color></b><b><color=#" + MainColor + ">" + GameSettings.motd.StripHex() + "</color></b>");
            }
        }
        else if (GameSettings.motd != string.Empty)
        {
            GameSettings.motd = string.Empty;
        }
        if (hash.ContainsKey("deadlycannons"))
        {
            if (GameSettings.deadlyCannons != ((int)hash["deadlycannons"]))
            {
                GameSettings.deadlyCannons = (int)hash["deadlycannons"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Cannons will now kill players.</color></b>");
            }
        }
        else if (GameSettings.deadlyCannons != 0)
        {
            GameSettings.deadlyCannons = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Cannons will no longer kill players.</color></b>");
        }
        if (hash.ContainsKey("asoracing"))
        {
            if (GameSettings.racingStatic != ((int)hash["asoracing"]))
            {
                GameSettings.racingStatic = (int)hash["asoracing"];
                //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Racing will not restart on win.</color></b>");
            }
        }
        else if (GameSettings.racingStatic != 0)
        {
            GameSettings.racingStatic = 0;
            //InRoomChat.addLINE2("<b><color=#" + MainColor + ">Racing will restart on win.</color></b>");
        }
    }

    //private void setGameSettings(ExitGames.Client.Photon.Hashtable hash)
    //{
    //    string str;
    //    ExitGames.Client.Photon.Hashtable hashtable;
    //    this.restartingEren = false;
    //    this.restartingBomb = false;
    //    this.restartingHorse = false;
    //    this.restartingTitan = false;
    //    if (hash.ContainsKey("bomb"))
    //    {
    //        if (GameSettings.bombMode != ((int) hash["bomb"]))
    //        {
    //            GameSettings.bombMode = (int) hash["bomb"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>PVP Bomb Mode enabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.bombMode != 0)
    //    {
    //        GameSettings.bombMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>PVP Bomb Mode disabled.</color>");
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            this.restartingBomb = true;
    //        }
    //    }
    //    if (hash.ContainsKey("globalDisableMinimap"))
    //    {
    //        if (GameSettings.globalDisableMinimap != ((int) hash["globalDisableMinimap"]))
    //        {
    //            GameSettings.globalDisableMinimap = (int) hash["globalDisableMinimap"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Minimaps are not allowed.</color>");
    //        }
    //    }
    //    else if (GameSettings.globalDisableMinimap != 0)
    //    {
    //        GameSettings.globalDisableMinimap = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Minimaps are allowed.</color>");
    //    }
    //    if (hash.ContainsKey("horse"))
    //    {
    //        if (GameSettings.horseMode != ((int) hash["horse"]))
    //        {
    //            GameSettings.horseMode = (int) hash["horse"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Horses enabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.horseMode != 0)
    //    {
    //        GameSettings.horseMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Horses disabled.</color>");
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            this.restartingHorse = true;
    //        }
    //    }
    //    if (hash.ContainsKey("punkWaves"))
    //    {
    //        if (GameSettings.punkWaves != ((int) hash["punkWaves"]))
    //        {
    //            GameSettings.punkWaves = (int) hash["punkWaves"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Punk override every 5 waves enabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.punkWaves != 0)
    //    {
    //        GameSettings.punkWaves = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Punk override every 5 waves disabled.</color>");
    //    }
    //    if (hash.ContainsKey("ahssReload"))
    //    {
    //        if (GameSettings.ahssReload != ((int) hash["ahssReload"]))
    //        {
    //            GameSettings.ahssReload = (int) hash["ahssReload"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>AHSS Air-Reload disabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.ahssReload != 0)
    //    {
    //        GameSettings.ahssReload = 0;
    //        InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>AHSS Air-Reload allowed.</color>");
    //    }
    //    if (hash.ContainsKey("team"))
    //    {
    //        if (GameSettings.teamMode != ((int) hash["team"]))
    //        {
    //            GameSettings.teamMode = (int) hash["team"];
    //            //str = string.Empty;
    //            //if (GameSettings.teamMode == 1)
    //            //{
    //            //    //str = "no sort";
    //            //}
    //            //else if (GameSettings.teamMode == 2)
    //            //{
    //            //    //str = "locked by size";
    //            //}
    //            //else if (GameSettings.teamMode == 3)
    //            //{
    //            //    //str = "locked by skill";
    //            //}
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Team Mode enabled (" + str + ").</color>");
    //            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
    //            {
    //                this.setTeam(3);
    //            }
    //        }
    //    }
    //    else if (GameSettings.teamMode != 0)
    //    {
    //        GameSettings.teamMode = 0;
    //        this.setTeam(0);
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Team mode disabled.</color>");
    //    }
    //    if (hash.ContainsKey("point"))
    //    {
    //        if (GameSettings.pointMode != ((int) hash["point"]))
    //        {
    //            GameSettings.pointMode = (int) hash["point"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Point limit enabled (" + Convert.ToString(GameSettings.pointMode) + ").</color>");
    //        }
    //    }
    //    else if (GameSettings.pointMode != 0)
    //    {
    //        GameSettings.pointMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Point limit disabled.</color>");
    //    }
    //    if (hash.ContainsKey("rock"))
    //    {
    //        if (GameSettings.disableRock != ((int) hash["rock"]))
    //        {
    //            GameSettings.disableRock = (int) hash["rock"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Punk rock throwing disabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.disableRock != 0)
    //    {
    //        GameSettings.disableRock = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Punk rock throwing enabled.</color>");
    //    }
    //    if (hash.ContainsKey("explode"))
    //    {
    //        if (GameSettings.explodeMode != ((int) hash["explode"]))
    //        {
    //            GameSettings.explodeMode = (int) hash["explode"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Titan Explode Mode enabled (Radius " + Convert.ToString(GameSettings.explodeMode) + ").</color>");
    //        }
    //    }
    //    else if (GameSettings.explodeMode != 0)
    //    {
    //        GameSettings.explodeMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Titan Explode Mode disabled.</color>");
    //    }
    //    if ((hash.ContainsKey("healthMode") && hash.ContainsKey("healthLower")) && hash.ContainsKey("healthUpper"))
    //    {
    //        if (((GameSettings.healthMode != ((int) hash["healthMode"])) || (GameSettings.healthLower != ((int) hash["healthLower"]))) || (GameSettings.healthUpper != ((int) hash["healthUpper"])))
    //        {
    //            GameSettings.healthMode = (int) hash["healthMode"];
    //            GameSettings.healthLower = (int) hash["healthLower"];
    //            GameSettings.healthUpper = (int) hash["healthUpper"];
    //            //str = "Static";
    //            //if (GameSettings.healthMode == 2)
    //            //{
    //            //    str = "Scaled";
    //            //}
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Titan Health (" + str + ", " + GameSettings.healthLower.ToString() + " to " + GameSettings.healthUpper.ToString() + ") enabled.</color>");
    //        }
    //    }
    //    else if (((GameSettings.healthMode != 0) || (GameSettings.healthLower != 0)) || (GameSettings.healthUpper != 0))
    //    {
    //        GameSettings.healthMode = 0;
    //        GameSettings.healthLower = 0;
    //        GameSettings.healthUpper = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Titan Health disabled.</color>");
    //    }
    //    if (hash.ContainsKey("infection"))
    //    {
    //        if (GameSettings.infectionMode != ((int) hash["infection"]))
    //        {
    //            GameSettings.infectionMode = (int) hash["infection"];
    //            this.name = LoginFengKAI.player.name;
    //            hashtable = new ExitGames.Client.Photon.Hashtable();
    //            hashtable.Add(PhotonPlayerProperty.RCteam, 0);
    //            PhotonNetwork.player.SetCustomProperties(hashtable);
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Infection mode (" + Convert.ToString(GameSettings.infectionMode) + ") enabled. Make sure your first character is human.</color>");
    //        }
    //    }
    //    else if (GameSettings.infectionMode != 0)
    //    {
    //        GameSettings.infectionMode = 0;
    //        hashtable = new ExitGames.Client.Photon.Hashtable();
    //        hashtable.Add(PhotonPlayerProperty.isTitan, 1);
    //        PhotonNetwork.player.SetCustomProperties(hashtable);
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Infection Mode disabled.</color>");
    //        if (PhotonNetwork.isMasterClient)
    //        {
    //            this.restartingTitan = true;
    //        }
    //    }
    //    if (hash.ContainsKey("eren"))
    //    {
    //        if (GameSettings.banEren != ((int) hash["eren"]))
    //        {
    //            GameSettings.banEren = (int) hash["eren"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Anti-Eren enabled. Using eren transform will get you kicked.</color>");
    //            if (PhotonNetwork.isMasterClient)
    //            {
    //                this.restartingEren = true;
    //            }
    //        }
    //    }
    //    else if (GameSettings.banEren != 0)
    //    {
    //        GameSettings.banEren = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Anti-Eren disabled. Eren transform is allowed.</color>");
    //    }
    //    if (hash.ContainsKey("titanc"))
    //    {
    //        if (GameSettings.moreTitans != ((int) hash["titanc"]))
    //        {
    //            GameSettings.moreTitans = (int) hash["titanc"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>" + Convert.ToString(GameSettings.moreTitans) + " titans will spawn each round.</color>");
    //        }
    //    }
    //    else if (GameSettings.moreTitans != 0)
    //    {
    //        GameSettings.moreTitans = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Default titans will spawn each round.</color>");
    //    }
    //    if (hash.ContainsKey("damage"))
    //    {
    //        if (GameSettings.damageMode != ((int) hash["damage"]))
    //        {
    //            GameSettings.damageMode = (int) hash["damage"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Nape minimum damage (" + Convert.ToString(GameSettings.damageMode) + ") enabled.</color>");
    //        }
    //    }
    //    else if (GameSettings.damageMode != 0)
    //    {
    //        GameSettings.damageMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Nape minimum damage disabled.</color>");
    //    }
    //    if ((hash.ContainsKey("sizeMode") && hash.ContainsKey("sizeLower")) && hash.ContainsKey("sizeUpper"))
    //    {
    //        if (((GameSettings.sizeMode != ((int) hash["sizeMode"])) || (GameSettings.sizeLower != ((float) hash["sizeLower"]))) || (GameSettings.sizeUpper != ((float) hash["sizeUpper"])))
    //        {
    //            GameSettings.sizeMode = (int) hash["sizeMode"];
    //            GameSettings.sizeLower = (float) hash["sizeLower"];
    //            GameSettings.sizeUpper = (float) hash["sizeUpper"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom titan size (" + GameSettings.sizeLower.ToString("F2") + "," + GameSettings.sizeUpper.ToString("F2") + ") enabled.</color>");
    //        }
    //    }
    //    else if (((GameSettings.sizeMode != 0) || (GameSettings.sizeLower != 0f)) || (GameSettings.sizeUpper != 0f))
    //    {
    //        GameSettings.sizeMode = 0;
    //        GameSettings.sizeLower = 0f;
    //        GameSettings.sizeUpper = 0f;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom titan size disabled.</color>");
    //    }
    //    if ((((hash.ContainsKey("spawnMode") && hash.ContainsKey("nRate")) && (hash.ContainsKey("aRate") && hash.ContainsKey("jRate"))) && hash.ContainsKey("cRate")) && hash.ContainsKey("pRate"))
    //    {
    //        if (((((GameSettings.spawnMode != ((int) hash["spawnMode"])) || (GameSettings.nRate != ((float) hash["nRate"]))) || ((GameSettings.aRate != ((float) hash["aRate"])) || (GameSettings.jRate != ((float) hash["jRate"])))) || (GameSettings.cRate != ((float) hash["cRate"]))) || (GameSettings.pRate != ((float) hash["pRate"])))
    //        {
    //            GameSettings.spawnMode = (int) hash["spawnMode"];
    //            GameSettings.nRate = (float) hash["nRate"];
    //            GameSettings.aRate = (float) hash["aRate"];
    //            GameSettings.jRate = (float) hash["jRate"];
    //            GameSettings.cRate = (float) hash["cRate"];
    //            GameSettings.pRate = (float) hash["pRate"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom spawn rate enabled (" + GameSettings.nRate.ToString("F2") + "% Normal, " + GameSettings.aRate.ToString("F2") + "% Abnormal, " + GameSettings.jRate.ToString("F2") + "% Jumper, " + GameSettings.cRate.ToString("F2") + "% Crawler, " + GameSettings.pRate.ToString("F2") + "% Punk </color>");
    //        }
    //    }
    //    else if (((((GameSettings.spawnMode != 0) || (GameSettings.nRate != 0f)) || ((GameSettings.aRate != 0f) || (GameSettings.jRate != 0f))) || (GameSettings.cRate != 0f)) || (GameSettings.pRate != 0f))
    //    {
    //        GameSettings.spawnMode = 0;
    //        GameSettings.nRate = 0f;
    //        GameSettings.aRate = 0f;
    //        GameSettings.jRate = 0f;
    //        GameSettings.cRate = 0f;
    //        GameSettings.pRate = 0f;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom spawn rate disabled.</color>");
    //    }
    //    if (hash.ContainsKey("waveModeOn") && hash.ContainsKey("waveModeNum"))
    //    {
    //        if ((GameSettings.waveModeOn != ((int) hash["waveModeOn"])) || (GameSettings.waveModeNum != ((int) hash["waveModeNum"])))
    //        {
    //            GameSettings.waveModeOn = (int) hash["waveModeOn"];
    //            GameSettings.waveModeNum = (int) hash["waveModeNum"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom wave mode (" + GameSettings.waveModeNum.ToString() + ") enabled.</color>");
    //        }
    //    }
    //    else if ((GameSettings.waveModeOn != 0) || (GameSettings.waveModeNum != 0))
    //    {
    //        GameSettings.waveModeOn = 0;
    //        GameSettings.waveModeNum = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Custom wave mode disabled.</color>");
    //    }
    //    if (hash.ContainsKey("friendly"))
    //    {
    //        if (GameSettings.friendlyMode != ((int) hash["friendly"]))
    //        {
    //            GameSettings.friendlyMode = (int) hash["friendly"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>PVP is prohibited.</color>");
    //        }
    //    }
    //    else if (GameSettings.friendlyMode != 0)
    //    {
    //        GameSettings.friendlyMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>PVP is allowed.</color>");
    //    }
    //    if (hash.ContainsKey("pvp"))
    //    {
    //        if (GameSettings.pvpMode != ((int) hash["pvp"]))
    //        {
    //            GameSettings.pvpMode = (int) hash["pvp"];
    //            //str = string.Empty;
    //            //if (GameSettings.pvpMode == 1)
    //            //{
    //            //    str = "Team-Based";
    //            //}
    //            //else if (GameSettings.pvpMode == 2)
    //            //{
    //            //    str = "FFA";
    //            //}
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Blade/AHSS PVP enabled (" + str + ").</color>");
    //        }
    //    }
    //    else if (GameSettings.pvpMode != 0)
    //    {
    //        GameSettings.pvpMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Blade/AHSS PVP disabled.</color>");
    //    }
    //    if (hash.ContainsKey("maxwave"))
    //    {
    //        if (GameSettings.maxWave != ((int) hash["maxwave"]))
    //        {
    //            GameSettings.maxWave = (int) hash["maxwave"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Max wave is " + GameSettings.maxWave.ToString() + ".</color>");
    //        }
    //    }
    //    else if (GameSettings.maxWave != 0)
    //    {
    //        GameSettings.maxWave = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Max wave set to default.</color>");
    //    }
    //    if (hash.ContainsKey("endless"))
    //    {
    //        if (GameSettings.endlessMode != ((int) hash["endless"]))
    //        {
    //            GameSettings.endlessMode = (int) hash["endless"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Endless respawn enabled (" + GameSettings.endlessMode.ToString() + " seconds).</color>");
    //        }
    //    }
    //    else if (GameSettings.endlessMode != 0)
    //    {
    //        GameSettings.endlessMode = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Endless respawn disabled.</color>");
    //    }
    //    if (hash.ContainsKey("motd"))
    //    {
    //        if (GameSettings.motd != ((string) hash["motd"]))
    //        {
    //            GameSettings.motd = (string) hash["motd"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>MOTD:" + GameSettings.motd + "</color>");
    //        }
    //    }
    //    else if (GameSettings.motd != string.Empty)
    //    {
    //        GameSettings.motd = string.Empty;
    //    }
    //    if (hash.ContainsKey("deadlycannons"))
    //    {
    //        if (GameSettings.deadlyCannons != ((int) hash["deadlycannons"]))
    //        {
    //            GameSettings.deadlyCannons = (int) hash["deadlycannons"];
    //            //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Cannons will now kill players.</color>");
    //        }
    //    }
    //    else if (GameSettings.deadlyCannons != 0)
    //    {
    //        GameSettings.deadlyCannons = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Cannons will no longer kill players.</color>");
    //    }
    //    if (hash.ContainsKey("asoracing"))
    //    {
    //        if (GameSettings.racingStatic != ((int) hash["asoracing"]))
    //        {
    //            GameSettings.racingStatic = (int) hash["asoracing"];
    //            InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Racing will not restart on win.</color>");
    //        }
    //    }
    //    else if (GameSettings.racingStatic != 0)
    //    {
    //        GameSettings.racingStatic = 0;
    //        //InRoomChat.ChatInstanse.addLINE("<color=#FFCC00>Racing will restart on win.</color>");
    //    }
    //}

    public string ModRoll()
    {
        //if (SelectMod == 0)
        //{
        //    System.Random randpeek = new System.Random();
        //    SelectMod = randpeek.Next(0, Rollingmodlist.Count);
        //    return Rollingmodlist[SelectMod];
        //}
        //foreach (string currientmod in mods)
        //{
        //    switch (currientmod)
        //    {
        //        case "Disconnect_mod":
        //            {  
        //                NetworkingPeer.instance.ResendInfo("Disconnect_mod");
        //                break;
        //            }
        //        case "CyanModNew":
        //            {                     
        //                NetworkingPeer.instance.ResendInfo("CyanModNew");
        //                break;
        //            }
        //    }
        //    //yield return new WaitForSeconds(1f);
        //}
        if (Rollingmodlist.Count <= 0)
        {
            SelectMod = 0;
            return string.Empty;
        }
        int num = SelectMod + 1;
        SelectMod = num;
        if (num >= Rollingmodlist.Count)
        {
            SelectMod = 0;
            return Rollingmodlist[0];
        }
        return Rollingmodlist[num];
    }

    public static IEnumerator SendPropModE(string modinfo, string modkey = "")
    {
        yield return new WaitForSeconds(1f);
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add(modkey, modinfo);
        PhotonNetwork.player.SetCustomProperties(hash);
    }

    private IEnumerator setGuildFeng()
    {
        WWW iteratorVariable1;
        WWWForm form = new WWWForm();
        form.AddField("name", LoginFengKAI.player.name);
        form.AddField("guildname", LoginFengKAI.player.guildname);
        if (Application.isWebPlayer)
        {
            iteratorVariable1 = new WWW("http://aotskins.com/version/guild.php", form);
        }
        else
        {
            iteratorVariable1 = new WWW("http://fenglee.com/game/aog/change_guild_name.php", form);
        }
        yield return iteratorVariable1;
    }

    [RPC]
    private void setMasterRC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            masterRC = true;
        }
    }

    private void setTeam(int setting)
    {
        if (setting == 0)
        {
            this.name = LoginFengKAI.player.name;
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.RCteam, 0);
            propertiesToSet.Add(PhotonPlayerProperty.name, this.name);
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else if (setting == 1)
        {
            ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
            hashtable2.Add(PhotonPlayerProperty.RCteam, 1);
            //string name = LoginFengKAI.player.name;
            //while (name.Contains("[") && (name.Length >= (name.IndexOf("[") + 8)))
            //{
            //    int index = name.IndexOf("[");
            //    name = name.Remove(index, 8);
            //}
            //if (!name.StartsWith("[00FFFF]"))
            //{
            //    name = "[00FFFF]" + name;
            //}
            //this.name = name;
            hashtable2.Add(PhotonPlayerProperty.name, this.name);
            PhotonNetwork.player.SetCustomProperties(hashtable2);
        }
        else if (setting == 2)
        {
            ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
            hashtable3.Add(PhotonPlayerProperty.RCteam, 2);
            //string str2 = LoginFengKAI.player.name;
            //while (str2.Contains("[") && (str2.Length >= (str2.IndexOf("[") + 8)))
            //{
            //    int startIndex = str2.IndexOf("[");
            //    str2 = str2.Remove(startIndex, 8);
            //}
            //if (!str2.StartsWith("[FF00FF]"))
            //{
            //    str2 = "[FF00FF]" + str2;
            //}
            //this.name = str2;
            hashtable3.Add(PhotonPlayerProperty.name, this.name);
            PhotonNetwork.player.SetCustomProperties(hashtable3);
        }
        else if (setting == 3)
        {
            int num3 = 0;
            int num4 = 0;
            int num5 = 1;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                int num7 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                if (num7 > 0)
                {
                    if (num7 == 1)
                    {
                        num3++;
                    }
                    else if (num7 == 2)
                    {
                        num4++;
                    }
                }
            }
            if (num3 > num4)
            {
                num5 = 2;
            }
            this.setTeam(num5);
        }
        if (((setting == 0) || (setting == 1)) || (setting == 2))
        {
            foreach (GameObject obj2 in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (obj2.GetPhotonView().isMine)
                {
                    PView.RPC("labelRPC", PhotonTargets.All, new object[] { obj2.GetPhotonView().viewID });
                }
            }
        }
    }

    [RPC]
    private void setTeamRPC(int setting, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient || info.sender.isLocal)
        {
            this.setTeam(setting);
        }
    }

    [RPC]
    private void settingRPC(ExitGames.Client.Photon.Hashtable hash, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            this.setGameSettings(hash);
        }
    }

    [RPC]
    private void showChatContent(string content)
    {
        this.chatContent.Add(content);
        if (this.chatContent.Count > 10)
        {
            this.chatContent.RemoveAt(0);
        }
        BRM.CacheGameObject.Find("LabelChatContent").GetComponent<UILabel>().text = string.Empty;
        for (int i = 0; i < this.chatContent.Count; i++)
        {
            UILabel component = BRM.CacheGameObject.Find("LabelChatContent").GetComponent<UILabel>();
            component.text = component.text + this.chatContent[i];
        }
    }

    public void ShowHUDInfoCenter(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoCenter");
        if (obj2 != null)
        {
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    public void ShowHUDInfoCenterADD(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoCenter");
        if (obj2 != null)
        {
            UILabel component = obj2.GetComponent<UILabel>();
            component.text = component.text + content;
        }
    }

    public void ShowHUDInfoTopCenter(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopCenter");
        if (obj2 != null)
        {
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    private void ShowHUDInfoTopCenterADD(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopCenter");
        if (obj2 != null)
        {
            UILabel component = obj2.GetComponent<UILabel>();
            component.text = component.text + content;
        }
    }

    private void ShowHUDInfoTopLeft(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopLeft");
        if (obj2 != null)
        {
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    private void ShowHUDInfoTopRight(string content)
    {
        if (this.LabelInfoTopRight == null)
        {
            UILabel uilabel = CacheGameObject.Find<UILabel>("LabelInfoTopRight");
            if (uilabel != null)
            {
                this.LabelInfoTopRight = uilabel;
            }
        }

        if (this.LabelInfoTopRight != null)
        {
            this.LabelInfoTopRight.text = content;
            //if ((((int)FengGameManagerMKII.settings[0xe7]) == 0) || (GameSettings.globalDisableMinimap == 1)) obj2.transform.localPosition = new Vector3((float)((int)(Screen.width * 0.5f)), (float)((int)(Screen.height * 0.5f)), 0f);
        }
    }

    private void ShowHUDInfoTopRightMAPNAME(string content)
    {
        if (this.LabelInfoTopRight == null)
        {
            UILabel uilabel = CacheGameObject.Find<UILabel>("LabelInfoTopRight");
            if (uilabel != null)
            {
                this.LabelInfoTopRight = uilabel;
            }
        }
        if (this.LabelInfoTopRight != null)
        {
            this.LabelInfoTopRight.text = this.LabelInfoTopRight.text + content;
        }
    }

    [RPC]
    private void showResult(string text0, string text1, string text2, string text3, string text4, string text6, PhotonMessageInfo t)
    {
        if ((this.gameTimesUp && t.sender.isMasterClient) || t.sender.isMasterClient)
        {
            this.gameTimesUp = true;
            GameObject obj2 = BRM.CacheGameObject.Find("UI_IN_GAME");
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[0], false);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[2], true);
            NGUITools.SetActive(obj2.GetComponent<UIReferArray>().panels[3], false);
            BRM.CacheGameObject.Find("LabelName").GetComponent<UILabel>().text = text0;
            BRM.CacheGameObject.Find("LabelKill").GetComponent<UILabel>().text = text1;
            BRM.CacheGameObject.Find("LabelDead").GetComponent<UILabel>().text = text2;
            BRM.CacheGameObject.Find("LabelMaxDmg").GetComponent<UILabel>().text = text3;
            BRM.CacheGameObject.Find("LabelTotalDmg").GetComponent<UILabel>().text = text4;
            BRM.CacheGameObject.Find("LabelResultTitle").GetComponent<UILabel>().text = text6;
            Screen.lockCursor = false;
            Screen.showCursor = true;
            IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
            gameStart = false;
        }
        else
        {
            //this.kickPlayerRC(t.sender, true, "false game end.");
            StatsTab.AddLine("Player " + t.sender.uiname.ToRGBA() + " send showResult as non mc.", StatsTab.DebugType.WARNING);
        }
    }

    private void SingleShowHUDInfoTopCenter(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopCenter");
        if (obj2 != null)
        {
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    private void SingleShowHUDInfoTopLeft(string content)
    {
        GameObject obj2 = BRM.CacheGameObject.Find("LabelInfoTopLeft");
        if (obj2 != null)
        {
            content = content.Replace("[0]", "[*^_^*]");
            obj2.GetComponent<UILabel>().text = content;
        }
    }

    [RPC]
    public void someOneIsDead(int id = -1)
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            if (id != 0)
            {
                this.PVPtitanScore += 2;
            }
            this.checkPVPpts();
            object[] parameters = new object[] { this.PVPhumanScore, this.PVPtitanScore };
            PView.RPC("refreshPVPStatus", PhotonTargets.Others, parameters);
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.ENDLESS_TITAN)
        {
            this.titanScore++;
        }
        else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.KILL_TITAN) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)) || ((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT) || (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST)))
        {
            if (this.isPlayerAllDead2())
            {
                this.gameLose2();
            }
        }
        else if (((IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS) && (GameSettings.pvpMode == 0)) && (GameSettings.bombMode == 0))
        {
            if (this.isPlayerAllDead2())
            {
                this.gameLose2();
                this.teamWinner = 0;
            }
            if (this.isTeamAllDead2(1))
            {
                this.teamWinner = 2;
                this.gameWin2();
            }
            if (this.isTeamAllDead2(2))
            {
                this.teamWinner = 1;
                this.gameWin2();
            }
        }
    }

    public void SpawnNonAITitan(string id, string tag = "titanRespawn")
    {
        GameObject obj3;
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
        GameObject obj2 = objArray[UnityEngine.Random.Range(0, objArray.Length)];
        this.myLastHero = id.ToUpper();
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", this.checkpoint.transform.position + new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20)), this.checkpoint.transform.rotation, 0);
        }
        else
        {
            obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", obj2.transform.position, obj2.transform.rotation, 0);
        }
        IN_GAME_MAIN_CAMERA.mainCamera.setMainObjectASTITAN(obj3);
        obj3.GetComponent<TITAN>().nonAI = true;
        obj3.GetComponent<TITAN>().speed = 30f;
        obj3.GetComponent<TITAN_CONTROLLER>().enabled = true;
        if ((id == "RANDOM") && (UnityEngine.Random.Range(0, 100) < 7))
        {
            obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
        }
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.spectate.disable = true;
        IN_GAME_MAIN_CAMERA.mouselook.disable = true;
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add("dead", false);
        ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable.Add(PhotonPlayerProperty.isTitan, 2);
        propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = true;
        this.ShowHUDInfoCenter(string.Empty);
    }

    public void SpawnNonAITitan2(string id, string tag = "titanRespawn")
    {
        if (logicLoaded && customLevelLoaded)
        {
            GameObject obj3;
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
            GameObject obj2 = objArray[UnityEngine.Random.Range(0, objArray.Length)];
            Vector3 position = obj2.transform.position;
            if (level.StartsWith("Custom") && (this.titanSpawns.Count > 0))
            {
                position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
            }
            this.myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", this.checkpoint.transform.position + new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20)), this.checkpoint.transform.rotation, 0);
            }
            else
            {
                obj3 = PhotonNetwork.Instantiate("TITAN_VER3.1", position, obj2.transform.rotation, 0);
            }
            IN_GAME_MAIN_CAMERA.mainCamera.setMainObjectASTITAN(obj3);
            obj3.GetComponent<TITAN>().nonAI = true;
            obj3.GetComponent<TITAN>().speed = 30f;
            obj3.GetComponent<TITAN_CONTROLLER>().enabled = true;
            if ((id == "RANDOM") && (UnityEngine.Random.Range(0, 100) < 7))
            {
                obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
            }
            IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
            IN_GAME_MAIN_CAMERA.spectate.disable = true;
            IN_GAME_MAIN_CAMERA.mouselook.disable = true;
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("dead", false);
            ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 2);
            propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = true;
            this.ShowHUDInfoCenter(string.Empty);
        }
        else
        {
            this.NOTSpawnNonAITitanRC(id);
        }
    }

    public void SpawnPlayer(string id, string tag = "playerRespawn")
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            this.SpawnPlayerAt2(id, this.checkpoint);
        }
        else
        {
            this.myLastRespawnTag = tag;
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);
            GameObject pos = objArray[UnityEngine.Random.Range(0, objArray.Length)];
            this.SpawnPlayerAt2(id, pos);
        }
    }

    public void SpawnPlayerAt(string id, GameObject pos)
    {
        this.myLastHero = id.ToUpper();
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            if (IN_GAME_MAIN_CAMERA.singleCharacter == "TITAN_EREN")
            {
                IN_GAME_MAIN_CAMERA.mainCamera.setMainObject((GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("TITAN_EREN"), pos.transform.position, pos.transform.rotation), true, false);
            }
            else
            {
                HERO component = IN_GAME_MAIN_CAMERA.mainCamera.setMainObject((GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("AOTTG_HERO 1"), pos.transform.position, pos.transform.rotation), true, false).GetComponent<HERO>();
                HERO_SETUP component2 = component.GetComponent<HERO_SETUP>();
                if (((IN_GAME_MAIN_CAMERA.singleCharacter == "SET 1") || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 2")) || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 3"))
                {
                    HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(IN_GAME_MAIN_CAMERA.singleCharacter);
                    costume.checkstat();
                    CostumeConeveter.HeroCostumeToLocalData(costume, IN_GAME_MAIN_CAMERA.singleCharacter);
                    component2.init();
                    if (costume != null)
                    {
                        component2.myCostume = costume;
                        component2.myCostume.stat = costume.stat;
                    }
                    else
                    {
                        costume = HeroCostume.costumeOption[3];
                        component2.myCostume = costume;
                        component2.myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                    }
                    component2.setCharacterComponent();
                    component.setStat2();
                    component.setSkillHUDPosition2();
                }
                else
                {
                    for (int i = 0; i < HeroCostume.costume.Length; i++)
                    {
                        if (HeroCostume.costume[i].name.ToUpper() == IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper())
                        {
                            int index = (HeroCostume.costume[i].id + CheckBoxCostume.costumeSet) - 1;
                            if (HeroCostume.costume[index].name != HeroCostume.costume[i].name)
                            {
                                index = HeroCostume.costume[i].id + 1;
                            }
                            component2.init();
                            component2.myCostume = HeroCostume.costume[index];
                            component2.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[index].name.ToUpper());
                            component2.setCharacterComponent();
                            component.setStat2();
                            component.setSkillHUDPosition2();
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            HERO component = IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", pos.transform.position, pos.transform.rotation, 0), true, false).GetComponent<HERO>();
            HERO_SETUP component2 = component.GetComponent<HERO_SETUP>();
            id = id.ToUpper();
            if (((id == "SET 1") || (id == "SET 2")) || (id == "SET 3"))
            {
                HeroCostume costume2 = CostumeConeveter.LocalDataToHeroCostume(id);
                costume2.checkstat();
                CostumeConeveter.HeroCostumeToLocalData(costume2, id);
                component2.init();
                if (costume2 != null)
                {
                    component2.myCostume = costume2;
                    component2.myCostume.stat = costume2.stat;
                }
                else
                {
                    costume2 = HeroCostume.costumeOption[3];
                    component2.myCostume = costume2;
                    component2.myCostume.stat = HeroStat.getInfo(costume2.name.ToUpper());
                }
                component2.setCharacterComponent();
                component.setStat2();
                component.setSkillHUDPosition2();
            }
            else
            {
                for (int j = 0; j < HeroCostume.costume.Length; j++)
                {
                    if (HeroCostume.costume[j].name.ToUpper() == id.ToUpper())
                    {
                        int num4 = HeroCostume.costume[j].id;
                        if (id.ToUpper() != "AHSS")
                        {
                            num4 += CheckBoxCostume.costumeSet - 1;
                        }
                        if (HeroCostume.costume[num4].name != HeroCostume.costume[j].name)
                        {
                            num4 = HeroCostume.costume[j].id + 1;
                        }
                        component2.init();
                        component2.myCostume = HeroCostume.costume[num4];
                        component2.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[num4].name.ToUpper());
                        component2.setCharacterComponent();
                        component.setStat2();
                        component.setSkillHUDPosition2();
                        break;
                    }
                }
            }
            CostumeConeveter.HeroCostumeToPhotonData2(component2.myCostume, PhotonNetwork.player);
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                IN_GAME_MAIN_CAMERA.main_objectT.position += new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20));
            }
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("dead", false);
            ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 1);
            propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
        IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
        IN_GAME_MAIN_CAMERA.spectate.disable = true;
        IN_GAME_MAIN_CAMERA.mouselook.disable = true;
        IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
        if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
        {
            Screen.lockCursor = true;
        }
        else
        {
            Screen.lockCursor = false;
        }
        Screen.showCursor = false;
        this.isLosing = false;
        this.ShowHUDInfoCenter(string.Empty);
    }

    public void SpawnPlayerAt2(string id, GameObject pos)
    {
        if (!logicLoaded || !customLevelLoaded)
        {
            BRM.StatsTab.AddLine("SpawnPlayerAt2, if (!logicLoaded(" + logicLoaded.ToString() + ")|| !customLevelLoaded(" + customLevelLoaded.ToString() + "))", StatsTab.DebugType.LOG);
            this.NOTSpawnPlayerRC(id);
        }
        else
        {
            Vector3 position = pos.transform.position;
            if (this.racingSpawnPointSet)
            {
                position = this.racingSpawnPoint;
            }
            else if (level.StartsWith("Custom"))
            {
                if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 0)
                {
                    List<Vector3> list = new List<Vector3>();
                    foreach (Vector3 vector2 in this.playerSpawnsC)
                    {
                        list.Add(vector2);
                    }
                    foreach (Vector3 vector2 in this.playerSpawnsM)
                    {
                        list.Add(vector2);
                    }
                    if (list.Count > 0)
                    {
                        position = list[UnityEngine.Random.Range(0, list.Count)];
                    }
                }
                else if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                {
                    if (this.playerSpawnsC.Count > 0)
                    {
                        position = this.playerSpawnsC[UnityEngine.Random.Range(0, this.playerSpawnsC.Count)];
                    }
                }
                else if ((RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam]) == 2) && (this.playerSpawnsM.Count > 0))
                {
                    position = this.playerSpawnsM[UnityEngine.Random.Range(0, this.playerSpawnsM.Count)];
                }
            }
            this.myLastHero = id.ToUpper();
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (IN_GAME_MAIN_CAMERA.singleCharacter == "TITAN_EREN")
                {
                    IN_GAME_MAIN_CAMERA.mainCamera.setMainObject((GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("TITAN_EREN"), pos.transform.position, pos.transform.rotation), true, false);
                }
                else
                {
                    HERO component = IN_GAME_MAIN_CAMERA.mainCamera.setMainObject((GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("AOTTG_HERO 1"), pos.transform.position, pos.transform.rotation), true, false).GetComponent<HERO>();
                    HERO_SETUP component2 = component.GetComponent<HERO_SETUP>();
                    if (((IN_GAME_MAIN_CAMERA.singleCharacter == "SET 1") || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 2")) || (IN_GAME_MAIN_CAMERA.singleCharacter == "SET 3"))
                    {
                        HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(IN_GAME_MAIN_CAMERA.singleCharacter);
                        costume.checkstat();

                        CostumeConeveter.HeroCostumeToLocalData(costume, IN_GAME_MAIN_CAMERA.singleCharacter);
                        component2.init();
                        if (costume != null)
                        {
                            component2.myCostume = costume;
                            component2.myCostume.stat = costume.stat;
                        }
                        else
                        {
                            costume = HeroCostume.costumeOption[3];
                            component2.myCostume = costume;
                            component2.myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                        }
                        component2.setCharacterComponent();
                        component.setStat2();
                        component.setSkillHUDPosition2();
                    }
                    else
                    {
                        for (int i = 0; i < HeroCostume.costume.Length; i++)
                        {
                            if (HeroCostume.costume[i].name.ToUpper() == IN_GAME_MAIN_CAMERA.singleCharacter.ToUpper())
                            {
                                int index = (HeroCostume.costume[i].id + CheckBoxCostume.costumeSet) - 1;
                                if (HeroCostume.costume[index].name != HeroCostume.costume[i].name)
                                {
                                    index = HeroCostume.costume[i].id + 1;
                                }
                                component2.init();
                                component2.myCostume = HeroCostume.costume[index];
                                component2.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[index].name.ToUpper());
                                component2.setCharacterComponent();
                                component.setStat2();
                                component.setSkillHUDPosition2();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                HERO component = IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, pos.transform.rotation, 0), true, false).GetComponent<HERO>();
                HERO_SETUP component2 = component.GetComponent<HERO_SETUP>();
                id = id.ToUpper();
                if (((id == "SET 1") || (id == "SET 2")) || (id == "SET 3"))
                {
                    HeroCostume costume2 = CostumeConeveter.LocalDataToHeroCostume(id);
                    costume2.checkstat();
                    CostumeConeveter.HeroCostumeToLocalData(costume2, id);
                    component2.init();
                    if (costume2 != null)
                    {
                        component2.myCostume = costume2;
                        component2.myCostume.stat = costume2.stat;
                    }
                    else
                    {
                        costume2 = HeroCostume.costumeOption[3];
                        component2.myCostume = costume2;
                        component2.myCostume.stat = HeroStat.getInfo(costume2.name.ToUpper());
                    }
                    component2.setCharacterComponent();
                    component.setStat2();
                    component.setSkillHUDPosition2();
                }
                else
                {
                    for (int j = 0; j < HeroCostume.costume.Length; j++)
                    {
                        if (HeroCostume.costume[j].name.ToUpper() == id.ToUpper())
                        {
                            int num4 = HeroCostume.costume[j].id;
                            if (id.ToUpper() != "AHSS")
                            {
                                num4 += CheckBoxCostume.costumeSet - 1;
                            }
                            if (HeroCostume.costume[num4].name != HeroCostume.costume[j].name)
                            {
                                num4 = HeroCostume.costume[j].id + 1;
                            }
                            component2.init();
                            component2.myCostume = HeroCostume.costume[num4];
                            component2.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[num4].name.ToUpper());
                            component2.setCharacterComponent();
                            component.setStat2();
                            component.setSkillHUDPosition2();
                            break;
                        }
                    }
                }
                CostumeConeveter.HeroCostumeToPhotonData2(component2.myCostume, PhotonNetwork.player);
                if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
                {
                    IN_GAME_MAIN_CAMERA.main_objectT.position += new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20));
                }
                ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable.Add("dead", false);
                ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
                hashtable = new ExitGames.Client.Photon.Hashtable();
                hashtable.Add(PhotonPlayerProperty.isTitan, 1);
                propertiesToSet = hashtable;
                PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            }
            IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
            IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
            IN_GAME_MAIN_CAMERA.spectate.disable = true;
            IN_GAME_MAIN_CAMERA.mouselook.disable = true;
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = false;
            this.isLosing = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    [RPC]
    public void spawnPlayerAtRPC(float posX, float posY, float posZ, PhotonMessageInfo info)
    {
        if (((info.sender.isMasterClient && logicLoaded) && (customLevelLoaded && !this.needChooseSide)) && IN_GAME_MAIN_CAMERA.mainCamera.gameOver)
        {
            Vector3 position = new Vector3(posX, posY, posZ);
            HERO component = IN_GAME_MAIN_CAMERA.mainCamera.setMainObject(PhotonNetwork.Instantiate("AOTTG_HERO 1", position, new Quaternion(0f, 0f, 0f, 1f), 0), true, false).GetComponent<HERO>();
            HERO_SETUP component2 = component.GetComponent<HERO_SETUP>();
            string slot = this.myLastHero.ToUpper();
            switch (slot)
            {
                case "SET 1":
                case "SET 2":
                case "SET 3":
                    {
                        HeroCostume costume = CostumeConeveter.LocalDataToHeroCostume(slot);
                        costume.checkstat();
                        CostumeConeveter.HeroCostumeToLocalData(costume, slot);
                        component2.init();
                        if (costume != null)
                        {
                            component2.myCostume = costume;
                            component2.myCostume.stat = costume.stat;
                        }
                        else
                        {
                            costume = HeroCostume.costumeOption[3];
                            component2.myCostume = costume;
                            component2.myCostume.stat = HeroStat.getInfo(costume.name.ToUpper());
                        }
                        component2.setCharacterComponent();
                        component.setStat2();
                        component.setSkillHUDPosition2();
                        break;
                    }
                default:
                    for (int i = 0; i < HeroCostume.costume.Length; i++)
                    {
                        if (HeroCostume.costume[i].name.ToUpper() == slot.ToUpper())
                        {
                            int id = HeroCostume.costume[i].id;
                            if (slot.ToUpper() != "AHSS")
                            {
                                id += CheckBoxCostume.costumeSet - 1;
                            }
                            if (HeroCostume.costume[id].name != HeroCostume.costume[i].name)
                            {
                                id = HeroCostume.costume[i].id + 1;
                            }
                            component2.init();
                            component2.myCostume = HeroCostume.costume[id];
                            component2.myCostume.stat = HeroStat.getInfo(HeroCostume.costume[id].name.ToUpper());
                            component2.setCharacterComponent();
                            component.setStat2();
                            component.setSkillHUDPosition2();
                            break;
                        }
                    }
                    break;
            }
            CostumeConeveter.HeroCostumeToPhotonData2(component2.myCostume, PhotonNetwork.player);
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                IN_GAME_MAIN_CAMERA.main_objectT.position += new Vector3((float)UnityEngine.Random.Range(-20, 20), 2f, (float)UnityEngine.Random.Range(-20, 20));
            }
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("dead", false);
            ExitGames.Client.Photon.Hashtable propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add(PhotonPlayerProperty.isTitan, 1);
            propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
            IN_GAME_MAIN_CAMERA.mainCamera.enabled = true;
            IN_GAME_MAIN_CAMERA.mainCamera.setHUDposition();
            IN_GAME_MAIN_CAMERA.spectate.disable = true;
            IN_GAME_MAIN_CAMERA.mouselook.disable = true;
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
            if (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS || IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.OldTPS)
            {
                Screen.lockCursor = true;
            }
            else
            {
                Screen.lockCursor = false;
            }
            Screen.showCursor = false;
            this.isLosing = false;
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    private void spawnPlayerCustomMap()
    {
        if (!this.needChooseSide && IN_GAME_MAIN_CAMERA.mainCamera.gameOver)
        {
            IN_GAME_MAIN_CAMERA.mainCamera.gameOver = false;
            if (RCextensions.returnIntFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.isTitan]) == 2)
            {
                this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
            }
            else
            {
                this.SpawnPlayer(this.myLastHero, this.myLastRespawnTag);
            }
            this.ShowHUDInfoCenter(string.Empty);
        }
    }

    public GameObject spawnTitan(int rate, Vector3 position, Quaternion rotation, bool punk)
    {
        GameObject obj3;
        GameObject obj2 = this.spawnTitanRaw(position, rotation);
        if (punk)
        {
            obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
        }
        else if (UnityEngine.Random.Range(0, 100) < rate)
        {
            if (IN_GAME_MAIN_CAMERA.difficulty == 2)
            {
                if ((UnityEngine.Random.Range((float)0f, (float)1f) < 0.7f) || LevelInfo.getInfo(level).noCrawler)
                {
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                }
                else
                {
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
                }
            }
        }
        else if (IN_GAME_MAIN_CAMERA.difficulty == 2)
        {
            if ((UnityEngine.Random.Range((float)0f, (float)1f) < 0.7f) || LevelInfo.getInfo(level).noCrawler)
            {
                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
            }
            else
            {
                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
            }
        }
        else if (UnityEngine.Random.Range(0, 100) < rate)
        {
            if ((UnityEngine.Random.Range((float)0f, (float)1f) < 0.8f) || LevelInfo.getInfo(level).noCrawler)
            {
                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
            }
            else
            {
                obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
            }
        }
        else if ((UnityEngine.Random.Range((float)0f, (float)1f) < 0.8f) || LevelInfo.getInfo(level).noCrawler)
        {
            obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
        }
        else
        {
            obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, false);
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            obj3 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("FX/FXtitanSpawn"), obj2.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        }
        else
        {
            obj3 = PhotonNetwork.Instantiate("FX/FXtitanSpawn", obj2.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
        }
        obj3.transform.localScale = obj2.transform.localScale;
        return obj2;
    }

    public void spawnTitanAction(int type, float size, int health, int number)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        if (this.titanSpawns.Count > 0)
        {
            position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
        }
        else
        {
            GameObject[] objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
            if (objArray.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, objArray.Length);
                GameObject obj2 = objArray[index];
                while (objArray[index] == null)
                {
                    index = UnityEngine.Random.Range(0, objArray.Length);
                    obj2 = objArray[index];
                }
                objArray[index] = null;
                position = obj2.transform.position;
                rotation = obj2.transform.rotation;
            }
        }
        for (int i = 0; i < number; i++)
        {
            GameObject obj3 = this.spawnTitanRaw(position, rotation);
            obj3.GetComponent<TITAN>().resetLevel(size);
            obj3.GetComponent<TITAN>().hasSetLevel = true;
            if (health > 0f)
            {
                obj3.GetComponent<TITAN>().currentHealth = health;
                obj3.GetComponent<TITAN>().maxHealth = health;
            }
            switch (type)
            {
                case 0:
                    obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanAtAction(int type, float size, int health, int number, float posX, float posY, float posZ)
    {
        Vector3 position = new Vector3(posX, posY, posZ);
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 1f);
        for (int i = 0; i < number; i++)
        {
            GameObject obj2 = this.spawnTitanRaw(position, rotation);
            obj2.GetComponent<TITAN>().resetLevel(size);
            obj2.GetComponent<TITAN>().hasSetLevel = true;
            if (health > 0f)
            {
                obj2.GetComponent<TITAN>().currentHealth = health;
                obj2.GetComponent<TITAN>().maxHealth = health;
            }
            switch (type)
            {
                case 0:
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.NORMAL, false);
                    break;

                case 1:
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
                    break;

                case 2:
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    break;

                case 3:
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    break;

                case 4:
                    obj2.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    break;
            }
        }
    }

    public void spawnTitanCustom(string type, int abnormal, int rate, bool punk)
    {
        int num8;
        Vector3 position;
        Quaternion rotation;
        GameObject[] objArray;
        int num9;
        GameObject obj2;
        int moreTitans = rate;
        if (level.StartsWith("Custom"))
        {
            moreTitans = 5;
            if (GameSettings.gameType == 1)
            {
                moreTitans = 3;
            }
            else if ((GameSettings.gameType == 2) || (GameSettings.gameType == 3))
            {
                moreTitans = 0;
            }
        }
        if ((GameSettings.moreTitans > 0) || (((GameSettings.moreTitans == 0) && level.StartsWith("Custom")) && (GameSettings.gameType >= 2)))
        {
            moreTitans = GameSettings.moreTitans;
        }
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.SURVIVE_MODE)
        {
            if (punk)
            {
                moreTitans = rate;
            }
            else
            {
                int waveModeNum;
                if (GameSettings.moreTitans == 0)
                {
                    waveModeNum = 1;
                    if (GameSettings.waveModeOn == 1)
                    {
                        waveModeNum = GameSettings.waveModeNum;
                    }
                    moreTitans += (this.wave - 1) * (waveModeNum - 1);
                }
                else if (GameSettings.moreTitans > 0)
                {
                    waveModeNum = 1;
                    if (GameSettings.waveModeOn == 1)
                    {
                        waveModeNum = GameSettings.waveModeNum;
                    }
                    moreTitans += (this.wave - 1) * waveModeNum;
                }
            }
        }
        moreTitans = Math.Min(50, moreTitans);
        if (GameSettings.spawnMode == 1)
        {
            float nRate = GameSettings.nRate;
            float aRate = GameSettings.aRate;
            float jRate = GameSettings.jRate;
            float cRate = GameSettings.cRate;
            float pRate = GameSettings.pRate;
            if (punk && (GameSettings.punkWaves == 1))
            {
                nRate = 0f;
                aRate = 0f;
                jRate = 0f;
                cRate = 0f;
                pRate = 100f;
                moreTitans = rate;
            }
            for (num8 = 0; num8 < moreTitans; num8++)
            {
                position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (this.titanSpawns.Count > 0)
                {
                    position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num9 = UnityEngine.Random.Range(0, objArray.Length);
                        obj2 = objArray[num9];
                        while (objArray[num9] == null)
                        {
                            num9 = UnityEngine.Random.Range(0, objArray.Length);
                            obj2 = objArray[num9];
                        }
                        objArray[num9] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }
                float num10 = UnityEngine.Random.Range((float)0f, (float)100f);
                if (num10 <= ((((nRate + aRate) + jRate) + cRate) + pRate))
                {
                    GameObject obj3 = this.spawnTitanRaw(position, rotation);
                    if (num10 < nRate)
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.NORMAL, false);
                    }
                    else if ((num10 >= nRate) && (num10 < (nRate + aRate)))
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_I, false);
                    }
                    else if ((num10 >= (nRate + aRate)) && (num10 < ((nRate + aRate) + jRate)))
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_JUMPER, false);
                    }
                    else if ((num10 >= ((nRate + aRate) + jRate)) && (num10 < (((nRate + aRate) + jRate) + cRate)))
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_CRAWLER, true);
                    }
                    else if ((num10 >= (((nRate + aRate) + jRate) + cRate)) && (num10 < ((((nRate + aRate) + jRate) + cRate) + pRate)))
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.TYPE_PUNK, false);
                    }
                    else
                    {
                        obj3.GetComponent<TITAN>().setAbnormalType2(AbnormalType.NORMAL, false);
                    }
                }
                else
                {
                    this.spawnTitan(abnormal, position, rotation, punk);
                }
            }
        }
        else if (level.StartsWith("Custom"))
        {
            for (num8 = 0; num8 < moreTitans; num8++)
            {
                position = new Vector3(UnityEngine.Random.Range((float)-400f, (float)400f), 0f, UnityEngine.Random.Range((float)-400f, (float)400f));
                rotation = new Quaternion(0f, 0f, 0f, 1f);
                if (this.titanSpawns.Count > 0)
                {
                    position = this.titanSpawns[UnityEngine.Random.Range(0, this.titanSpawns.Count)];
                }
                else
                {
                    objArray = GameObject.FindGameObjectsWithTag("titanRespawn");
                    if (objArray.Length > 0)
                    {
                        num9 = UnityEngine.Random.Range(0, objArray.Length);
                        obj2 = objArray[num9];
                        while (objArray[num9] == null)
                        {
                            num9 = UnityEngine.Random.Range(0, objArray.Length);
                            obj2 = objArray[num9];
                        }
                        objArray[num9] = null;
                        position = obj2.transform.position;
                        rotation = obj2.transform.rotation;
                    }
                }
                this.spawnTitan(abnormal, position, rotation, punk);
            }
        }
        else
        {
            this.randomSpawnTitan("titanRespawn", abnormal, moreTitans, punk);
        }
    }

    private GameObject spawnTitanRaw(Vector3 position, Quaternion rotation)
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            return (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("TITAN_VER3.1"), position, rotation);
        }
        return PhotonNetwork.Instantiate("TITAN_VER3.1", position, rotation, 0);
    }

    [RPC]
    private void spawnTitanRPC(PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient)
        {
            foreach (TITAN titan in titans)
            {
                if (titan.photonView.isMine && !(PhotonNetwork.isMasterClient && !titan.nonAI))
                {
                    PhotonNetwork.Destroy(titan.gameObject);
                }
            }
            this.SpawnNonAITitan2(this.myLastHero, "titanRespawn");
        }
    }

    public IEnumerator SkyTex()
    {

        WWW sky = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonLeft.png");
        yield return sky;
        ImageFront = sky.texture;
        Material sky1 = Camera.main.GetComponent<Skybox>().material;
        ImageFront.wrapMode = TextureWrapMode.Clamp;

        //WWW sky2 = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonBack.png");
        //yield return sky;
        //ImageBack = sky.texture;
        //Material sky3 = Camera.main.GetComponent<Skybox>().material;
        //ImageBack.wrapMode = TextureWrapMode.Clamp;

        //WWW sky4 = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonDown.png");
        //yield return sky;
        //ImageDown = sky.texture;
        //Material sky5 = Camera.main.GetComponent<Skybox>().material;
        //ImageDown.wrapMode = TextureWrapMode.Clamp;

        //WWW sky6 = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonFront.png"); 
        // yield return sky;
        //ImageLeft = sky.texture;
        //Material sky7 = Camera.main.GetComponent<Skybox>().material;
        //ImageLeft.wrapMode = TextureWrapMode.Clamp;

        //WWW sky8 = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonRight.png");
        //yield return sky;
        //ImageRight = sky.texture;
        //Material sky9 = Camera.main.GetComponent<Skybox>().material;
        //ImageRight.wrapMode = TextureWrapMode.Clamp;

        //WWW sky10 = new WWW("file:///" + Application.dataPath + "/Textures/FullMoonUp.png");
        //yield return sky;
        //ImageUp = sky.texture;
        //Material sky11 = Camera.main.GetComponent<Skybox>().material;
        //ImageUp.wrapMode = TextureWrapMode.Clamp;


        sky1.SetTexture("_FrontTex", ImageFront);
        sky1.SetTexture("_BackTex", ImageFront);
        sky1.SetTexture("_LeftTex", ImageFront);
        sky1.SetTexture("_RightTex", ImageFront);
        sky1.SetTexture("_UpTex", ImageFront);
        sky1.SetTexture("_DownTex", ImageFront);
        //sky1.SetTexture("_FrontTex", ImageFront);
        //sky3.SetTexture("_BackTex", ImageBack);
        //sky7.SetTexture("_LeftTex", ImageLeft);
        //sky9.SetTexture("_RightTex", ImageRight);
        //sky11.SetTexture("_UpTex", ImageUp);
        //sky5.SetTexture("_DownTex", ImageDown);
    }

    private void Start()
    {
        base.name = "MultiplayerManager";
        HeroCostume.init2();
        CharacterMaterials.init();
        DontDestroyOnLoad(base.gameObject);
        heroes.Clear();
        this.eT = new ArrayList();
        titans.Clear();
        this.fT = new ArrayList();
        this.cT = new ArrayList();
        this.hooks = new ArrayList();
        this.name = string.Empty;
        allheroes.Clear();
        //if (nameField == null)
        //{
        //    nameField = "GUEST" + UnityEngine.Random.Range(0, 0x186a0);
        //}
        if (privateServerField == null)
        {
            privateServerField = string.Empty;
        }
        usernameField = string.Empty;
        passwordField = string.Empty;
        //this.resetGameSettings();
        banHash = new ExitGames.Client.Photon.Hashtable();
        imatitan = new ExitGames.Client.Photon.Hashtable();
        oldScript = string.Empty;
        currentLevel = string.Empty;
        if (currentScript == null)
        {
            currentScript = string.Empty;
        }
        this.titanSpawns = new List<Vector3>();
        this.playerSpawnsC = new List<Vector3>();
        this.playerSpawnsM = new List<Vector3>();
        this.playersRPC = new List<PhotonPlayer>();
        this.levelCache = new List<string[]>();
        this.titanSpawners = new List<TitanSpawner>();
        this.restartCount = new List<float>();
        ignoreList = new List<int>();
        this.groundList = new List<GameObject>();
        noRestart = false;
        masterRC = false;
        this.isSpawning = false;
        intVariables = new ExitGames.Client.Photon.Hashtable();
        heroHash = new ExitGames.Client.Photon.Hashtable();
        boolVariables = new ExitGames.Client.Photon.Hashtable();
        stringVariables = new ExitGames.Client.Photon.Hashtable();
        floatVariables = new ExitGames.Client.Photon.Hashtable();
        globalVariables = new ExitGames.Client.Photon.Hashtable();
        RCRegions = new ExitGames.Client.Photon.Hashtable();
        RCEvents = new ExitGames.Client.Photon.Hashtable();
        RCVariableNames = new ExitGames.Client.Photon.Hashtable();
        RCRegionTriggers = new ExitGames.Client.Photon.Hashtable();
        playerVariables = new ExitGames.Client.Photon.Hashtable();
        titanVariables = new ExitGames.Client.Photon.Hashtable();
        logicLoaded = false;
        customLevelLoaded = false;
        oldScriptLogic = string.Empty;
        currentScriptLogic = string.Empty;
        this.retryTime = 0f;
        this.playerList = string.Empty;
        this.updateTime = 0f;
        if (this.textureBackgroundBlack == null)
        {
            this.textureBackgroundBlack = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundBlack.SetPixel(0, 0, Color.Lerp(Color.gray, Color.black, 0.8f));
            this.textureBackgroundBlack.Apply();
        }
        if (this.textureBackgroundPitchBlack == null)
        {
            this.textureBackgroundPitchBlack = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundPitchBlack.SetPixel(0, 0, Color.black);
            this.textureBackgroundPitchBlack.Apply();
        }
        if (this.textureBackgroundBlue == null)
        {
            this.textureBackgroundBlue = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundBlue.SetPixel(0, 0, new Color(0.08f, 0.3f, 0.4f, 1f));
            this.textureBackgroundBlue.Apply();
        }
        if (this.textureBackgroundYellow == null)
        {
            this.textureBackgroundYellow = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundYellow.SetPixel(0, 0, Color.yellow);
            this.textureBackgroundYellow.Apply();
        }
        if (this.textureBackgroundDarkGrey == null)
        {
            this.textureBackgroundDarkGrey = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundDarkGrey.SetPixel(0, 0, Color.Lerp(Color.gray, Color.black, 0.75f));
            this.textureBackgroundDarkGrey.Apply();
        }
        if (this.textureBackgroundGrey == null)
        {
            this.textureBackgroundGrey = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundGrey.SetPixel(0, 0, Color.gray);
            this.textureBackgroundGrey.Apply();
        }
        if (this.textureBackgroundWhite == null)
        {
            this.textureBackgroundWhite = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            this.textureBackgroundWhite.SetPixel(0, 0, Color.white);
            this.textureBackgroundWhite.Apply();
        }
        this.loadconfig();
        //StartCoroutine(SkyTex());
        List<string> list2 = new List<string> {
            //"AOTTG_HERO",
            //"Colossal",
            //"Icosphere",
            //"Cube",
            //"colossal",
            //"CITY",
            //"city",
            //"rock",
            "PanelLogin",
            "LOGIN",
            "BG_TITLE",
            "ButtonCREDITS",
            "ButtonSINGLE",
            "ButtonLAN",
            "ButtonOPTION"
        };
        Rollingmodlist = new List<string> {
            "Disconnect_mod",
            "CyanModNew",
            "RedSkies",
            //"DT",
            //"JakesMod",
            //"Robbie'sMod",
            //"not null",
            //"Destroy",
            //"Alpha",
            //"BRM",
            //"woahWtf",
            //"SRC",
            //"BRC",
            //"OhSoShO",
            //"Nathan",
            //"Arch",
            //"NRC",
            //"Doge",
            //"GHOST",
        };
        //if ((bool)FengGameManagerMKII.settings[263]) list2.Add();
        foreach (GameObject obj2 in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            foreach (string str in list2)
            {
                if ((obj2.name.Contains(str) /*|| (obj2.name == "Button")) || ((obj2.name == "Label") && obj2.GetComponent<UILabel>().text.Contains("Snap"))*/))
                {
                    UnityEngine.Object.Destroy(obj2);
                }
                //else if (obj2.name == "Checkbox")
                //{
                //    UnityEngine.Object.Destroy(obj2);
                //}

            }
        }

        //this.setBackground();
        PView = base.photonView;
        instance = this;
        ChangeQuality.setCurrentQuality();
        gameObject.AddComponent<StatsTab>();
        gameObject.AddComponent<ServerListGUI>().enabled = false;
        gameObject.AddComponent<F1GUI>();
        gameObject.AddComponent<F2GUI>();
        gameObject.AddComponent<ExtraGUI>();
        if (Agressive) disconnectBan = LoginFengKAI.LoadBanList();
        //StartCoroutine(SkyTex());
    }

    [RPC]
    public void titanGetKill(PhotonPlayer player, int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        object[] parameters = new object[] { Damage };
        PView.RPC("netShowDamage", player, parameters);
        object[] objArray2 = new object[] { name, false };
        PView.RPC("oneTitanDown", PhotonTargets.MasterClient, objArray2);
        this.sendKillInfo(false, (string)player.customProperties[PhotonPlayerProperty.name], true, name, Damage);
        this.playerKillInfoUpdate(player, Damage);
    }

    public void titanGetKillbyServer(int Damage, string name)
    {
        Damage = Mathf.Max(10, Damage);
        this.sendKillInfo(false, LoginFengKAI.player.name, true, name, Damage);
        this.netShowDamage(Damage);
        this.oneTitanDown(name, false);
        this.playerKillInfoUpdate(PhotonNetwork.player, Damage);
    }

    private void tryKick(KickState tmp)
    {
        this.sendChatContentInfo(string.Concat(new object[] { "kicking #", tmp.name, ", ", tmp.getKickCount(), "/", (int)(PhotonNetwork.playerList.Length * 0.5f), "vote" }));
        if (tmp.getKickCount() >= ((int)(PhotonNetwork.playerList.Length * 0.5f)))
        {
            this.kickPhotonPlayer(tmp.name.ToString());
        }
    }

    public void unloadAssets()
    {
        if (!this.isUnloading)
        {
            this.isUnloading = true;
            base.StartCoroutine(this.unloadAssetsE(10f));
        }
    }

    public IEnumerator unloadAssetsE(float time)
    {
        yield return new WaitForSeconds(time);
        Resources.UnloadUnusedAssets();
        this.isUnloading = false;
    }

    public void unloadAssetsEditor()
    {
        if (!this.isUnloading)
        {
            this.isUnloading = true;
            base.StartCoroutine(this.unloadAssetsE(30f));
        }
    }

    private void Update()
    {
        if ((IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE) && (GameObject.Find("LabelNetworkStatus") != null))
        {
            if (PhotonNetwork.networkingPeer.SocketImplementationConfig.ContainsKey(ExitGames.Client.Photon.ConnectionProtocol.WebSocket))
                PhotonNetwork.networkingPeer.SocketImplementationConfig[ExitGames.Client.Photon.ConnectionProtocol.WebSocket] = typeof(ExitGames.Client.Photon.SocketWebTcp);
            else
                PhotonNetwork.networkingPeer.SocketImplementationConfig.Add(ExitGames.Client.Photon.ConnectionProtocol.WebSocket, typeof(ExitGames.Client.Photon.SocketWebTcp));
            //string protocol = "none";
            //if (PhotonNetwork.networkingPeer.UsedProtocol == ConnectionProtocol.Tcp) protocol = "TCP";
            //else if (PhotonNetwork.networkingPeer.UsedProtocol == ConnectionProtocol.Udp) protocol = "UDP";
            //else if (PhotonNetwork.networkingPeer.UsedProtocol == ConnectionProtocol.WebSocket) protocol = "Web Socket";
            GameObject.Find("LabelNetworkStatus").GetComponent<UILabel>().text = PhotonNetwork.connectionStatesDetailed.ToString();
            UILabel component = GameObject.Find("LabelNetworkStatus").GetComponent<UILabel>();
            if (PhotonNetwork.connected)
            {
                component.text = component.text + " ping:[A8FF24]" + PhotonNetwork.GetPing() + ":[ffdc2e]" + PhotonNetwork.networkingPeer.UsedProtocol.ToString();
            }
            else component.text = "Welcome," + LoginFengKAI.player.name;
        }
        //if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        //{
        //    if (FengGameManagerMKII.LabelNetworkStatus != null)
        //    {
        //        FengGameManagerMKII.LabelNetworkStatus.text = PhotonNetwork.connectionStateDetailed.ToString();
        //        if (PhotonNetwork.connected)
        //        {
        //            UILabel expr_53 = FengGameManagerMKII.LabelNetworkStatus;
        //            expr_53.text = expr_53.text + " ping:" + PhotonNetwork.GetPing();
        //        }
        //    }
        //    else
        //    {
        //        FengGameManagerMKII.LabelNetworkStatus = LoginFengKAI.Log.output.GetComponent<UILabel>();
        //        //BRM.CacheGameObject.Find("LabelNetworkStatus").GetComponent<UILabel>()
        //    }
        //}
        if (gameStart)
        {
            foreach (HERO hERO in heroes)
            {
                hERO.update();
            }
            foreach (Bullet bullet in this.hooks)
            {
                bullet.update();
            }
            if (IN_GAME_MAIN_CAMERA.mainCamera != null)
            {
                IN_GAME_MAIN_CAMERA.mainCamera.snapShotUpdate();
            }
            foreach (TITAN_EREN tITAN_EREN in this.eT)
            {
                tITAN_EREN.update();
            }
            foreach (TITAN tITAN in titans)
            {
                tITAN.update();
            }
            foreach (FEMALE_TITAN fEMALE_TITAN in this.fT)
            {
                fEMALE_TITAN.update();
            }
            foreach (COLOSSAL_TITAN cOLOSSAL_TITAN in this.cT)
            {
                cOLOSSAL_TITAN.update();
            }
            if (IN_GAME_MAIN_CAMERA.isReady)
            {
                IN_GAME_MAIN_CAMERA.mainCamera.update();
            }
        }
    }


    [RPC]
    private void updateKillInfo(bool t1, string killer, bool t2, string victim, int dmg)
    {
        GameObject obj4;
        GameObject obj2 = BRM.CacheGameObject.Find("UI_IN_GAME");
        GameObject obj3 = (GameObject)UnityEngine.Object.Instantiate(BRM.CacheResources.Load("UI/KillInfo"));
        for (int i = 0; i < this.killInfoGO.Count; i++)
        {
            obj4 = (GameObject)this.killInfoGO[i];
            if (obj4 != null)
            {
                obj4.GetComponent<KillInfoComponent>().moveOn();
            }
        }
        if (this.killInfoGO.Count > 4)
        {
            obj4 = (GameObject)this.killInfoGO[0];
            if (obj4 != null)
            {
                obj4.GetComponent<KillInfoComponent>().destory();
            }
            this.killInfoGO.RemoveAt(0);
        }
        obj3.transform.parent = obj2.GetComponent<UIReferArray>().panels[0].transform;
        obj3.GetComponent<KillInfoComponent>().show(t1, killer, t2, victim, dmg);
        this.killInfoGO.Add(obj3);
        if (((int)settings[0xf4]) == 1)
        {
            string str2 = ("<color=#FFC000>(" + this.roundTime.ToString("F2") + ")</color> ") + killer.hexColor() + " killed ";
            string newLine = str2 + victim.hexColor() + " for " + dmg.ToString() + " damage.";
            InRoomChat.ChatInstanse.addLINE(newLine);
        }
    }

    [RPC]
    public void verifyPlayerHasLeft(int ID, PhotonMessageInfo info)
    {
        if (info.sender.isMasterClient && (PhotonPlayer.Find(ID) != null))
        {
            PhotonPlayer player = PhotonPlayer.Find(ID);
            string str = string.Empty;
            str = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
            banHash.Add(ID, str);
        }
    }


    private void updatePlayerList()
    {
        string text = string.Empty;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
        {
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                PhotonPlayer photonPlayer = PhotonNetwork.playerList[i];
                if (photonPlayer != null)
                {
                    ExitGames.Client.Photon.Hashtable customProperties = photonPlayer.customProperties;
                    if (customProperties != null && customProperties["dead"] != null)
                    {
                        object obj = text;
                        text = string.Concat(new object[]
                        {
                            obj,
                            "[" + MainColor + "][",
                            photonPlayer.ID,
                            "] "
                        });
                        if (photonPlayer.isDCMarked)
                        {
                            text += "[[000000]DC [ffdc2e]MARK[F0F0F0]]";
                        }
                        if (disconnectBan.Contains(photonPlayer.uiname))
                        {
                            text += "[1f0e11][DC BAN] ";
                        }
                        if (ignoreList.Contains(photonPlayer.ID))
                        {
                            text += "[FF0000][X] ";
                        }
                        if (photonPlayer.isLocal)
                        {
                            text += "[7d4d5d][YOU][FFFFFF]";
                        }
                        else
                        {
                            text += "[FFCC00]";
                        }
                        text += "[" + MainColor + "][[7b001c]" + Checkmod(photonPlayer) + "[" + MainColor + "]] ";
                        if (photonPlayer.isMasterClient)
                        {
                            text += "[000000][M][-] ";
                        }
                        if ((bool)customProperties["dead"])
                        {
                            text = text + "[" + ColorSet.color_red + "] [[000000]DEAD[" + ColorSet.color_red + "]] ";
                        }
                        if (customProperties["isTitan"] != null)
                        {
                            if ((int)customProperties["isTitan"] < 2 && customProperties["team"] != null)
                            {

                                if ((int)customProperties["team"] < 2)
                                {
                                    text = text + "[" + ColorSet.color_human + "] <H> ";
                                }
                                else if ((int)customProperties["team"] == 2)
                                {
                                    text = text + "[" + ColorSet.color_human_1 + "] <A> ";
                                }
                            }
                            else if ((int)customProperties["isTitan"] == 2)
                            {
                                text = text + "[" + ColorSet.color_titan_player + "] <T> ";
                            }
                        }
                        object str = text;
                        text = string.Concat(new object[]
                            {
                                str,
                                string.Empty,
                                customProperties["name"],
                                "[-][ffffff]: {",
                                customProperties["kills"],
                                "|",
                                customProperties["deaths"],
                                "|",
                                customProperties["max_dmg"],
                                "|",
                                customProperties["total_dmg"],
                                "}"
                            });
                        if ((bool)customProperties["dead"])
                        {
                            text += "[-][-]";
                        }
                        text += "\n";
                        //str2 = (player7.uiname.Length > 100) ? player7.uiname.Substring(0, 20) : player7.uiname;                        
                    }
                }
            }
            this.ShowHUDInfoTopLeft(text);
        }
    }

    public IEnumerator WaitAndRecompilePlayerList(float time)
    {
        int num16;
        string str2;
        int num17;
        int num18;
        int num19;
        int num20;
        object[] objArray2;
        yield return new WaitForSeconds(time);
        string iteratorVariable1 = string.Empty;
        if (GameSettings.teamMode == 0)
        {
            foreach (PhotonPlayer player7 in PhotonNetwork.playerList)
            {
                //iteratorVariable1 = string.Concat(new object[] {"[" + MainColor + "][", player7.ID, "] " });
                if (player7.customProperties[PhotonPlayerProperty.dead] != null)
                {
                    iteratorVariable1 += string.Concat(new object[] { "[" + MainColor + "][", player7.ID, "] " });
                    if (player7.isDCMarked)
                    {
                        iteratorVariable1 += "[[000000]DC [ffdc2e]MARK[F0F0F0]]";
                    }
                    if (disconnectBan.Contains(player7.uiname))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[1f0e11][DC BAN] ";
                    }
                    if (ignoreList.Contains(player7.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }
                    if (player7.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[7d4d5d][YOU][FFFFFF]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "[" + MainColor + "][[7b001c]" + Checkmod(player7) + "[" + MainColor + "]] ";
                    if (player7.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[000000][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] [[000000]DEAD[" + ColorSet.color_red + "]] ";
                    }
                    if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    string iteratorVariable0 = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = (player7.uiname.Length > 100) ? player7.uiname.Substring(0, 20) : player7.uiname;
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player7.customProperties[PhotonPlayerProperty.total_dmg]);
                    objArray2 = new object[] { iteratorVariable0, string.Empty, str2, "[ffffff]: {", num17, "|", num18, "|", num19, "|", num20, "}" };
                    iteratorVariable1 = string.Concat(objArray2);
                    if (RCextensions.returnBoolFromObject(player7.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
        }
        else
        {
            int num11;
            string str;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            Dictionary<int, PhotonPlayer> dictionary = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary2 = new Dictionary<int, PhotonPlayer>();
            Dictionary<int, PhotonPlayer> dictionary3 = new Dictionary<int, PhotonPlayer>();
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if ((player.customProperties[PhotonPlayerProperty.dead] != null) && !ignoreList.Contains(player.ID))
                {
                    num11 = RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.RCteam]);
                    switch (num11)
                    {
                        case 0:
                            dictionary3.Add(player.ID, player);
                            break;

                        case 1:
                            dictionary.Add(player.ID, player);
                            num2 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num4 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num6 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num8 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;

                        case 2:
                            dictionary2.Add(player.ID, player);
                            num3 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.kills]);
                            num5 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.deaths]);
                            num7 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.max_dmg]);
                            num9 += RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.total_dmg]);
                            break;
                    }
                }
            }
            this.cyanKills = num2;
            this.magentaKills = num3;
            if (PhotonNetwork.isMasterClient)
            {
                if (GameSettings.teamMode != 2)
                {
                    if (GameSettings.teamMode == 3)
                    {
                        foreach (PhotonPlayer player3 in PhotonNetwork.playerList)
                        {
                            int num13 = 0;
                            num11 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.RCteam]);
                            if (num11 > 0)
                            {
                                switch (num11)
                                {
                                    case 1:
                                        {
                                            int num14 = 0;
                                            num14 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                            if (((num3 + num14) + 7) < (num2 - num14))
                                            {
                                                num13 = 2;
                                                num3 += num14;
                                                num2 -= num14;
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            int num15 = 0;
                                            num15 = RCextensions.returnIntFromObject(player3.customProperties[PhotonPlayerProperty.kills]);
                                            if (((num2 + num15) + 7) < (num3 - num15))
                                            {
                                                num13 = 1;
                                                num2 += num15;
                                                num3 -= num15;
                                            }
                                            break;
                                        }
                                }
                                if (num13 > 0)
                                {
                                    PView.RPC("setTeamRPC", player3, new object[] { num13 });
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (PhotonPlayer player2 in PhotonNetwork.playerList)
                    {
                        int num12 = 0;
                        if (dictionary.Count > (dictionary2.Count + 1))
                        {
                            num12 = 2;
                            if (dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Remove(player2.ID);
                            }
                            if (!dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Add(player2.ID, player2);
                            }
                        }
                        else if (dictionary2.Count > (dictionary.Count + 1))
                        {
                            num12 = 1;
                            if (!dictionary.ContainsKey(player2.ID))
                            {
                                dictionary.Add(player2.ID, player2);
                            }
                            if (dictionary2.ContainsKey(player2.ID))
                            {
                                dictionary2.Remove(player2.ID);
                            }
                        }
                        if (num12 > 0)
                        {
                            PView.RPC("setTeamRPC", player2, new object[] { num12 });
                        }
                    }
                }
            }
            iteratorVariable1 = string.Concat(new object[] { iteratorVariable1, "[00FFFF]TEAM CYAN", "[ffffff]:", this.cyanKills, "/", num4, "/", num6, "/", num8, "\n" });
            foreach (PhotonPlayer player4 in dictionary.Values)
            {
                num11 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player4.customProperties[PhotonPlayerProperty.dead] != null) && (num11 == 1))
                {
                    if (ignoreList.Contains(player4.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }
                    if (player4.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player4.ID) + "] ";
                    if (player4.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = (player4.uiname.Length > 100) ? player4.uiname.Substring(0, 20) : player4.uiname;
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player4.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(new object[] { str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20 });
                    if (RCextensions.returnBoolFromObject(player4.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
            iteratorVariable1 = string.Concat(new object[] { iteratorVariable1, " \n", "[FF00FF]TEAM MAGENTA", "[ffffff]:", this.magentaKills, "/", num5, "/", num7, "/", num9, "\n" });
            foreach (PhotonPlayer player5 in dictionary2.Values)
            {
                num11 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player5.customProperties[PhotonPlayerProperty.dead] != null) && (num11 == 2))
                {
                    if (ignoreList.Contains(player5.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }
                    if (player5.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player5.ID) + "] ";
                    if (player5.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = (player5.uiname.Length > 100) ? player5.uiname.Substring(0, 20) : player5.uiname;
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player5.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(new object[] { str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20 });
                    if (RCextensions.returnBoolFromObject(player5.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
            iteratorVariable1 = string.Concat(new object[] { iteratorVariable1, " \n", "[00FF00]INDIVIDUAL\n" });
            foreach (PhotonPlayer player6 in dictionary3.Values)
            {
                num11 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.RCteam]);
                if ((player6.customProperties[PhotonPlayerProperty.dead] != null) && (num11 == 0))
                {
                    if (ignoreList.Contains(player6.ID))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FF0000][X] ";
                    }
                    if (player6.isLocal)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[00CC00]";
                    }
                    else
                    {
                        iteratorVariable1 = iteratorVariable1 + "[FFCC00]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "[" + Convert.ToString(player6.ID) + "] ";
                    if (player6.isMasterClient)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[ffffff][M] ";
                    }
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_red + "] *dead* ";
                    }
                    if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) < 2)
                    {
                        num16 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.team]);
                        if (num16 < 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human + "] <H> ";
                        }
                        else if (num16 == 2)
                        {
                            iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_human_1 + "] <A> ";
                        }
                    }
                    else if (RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.isTitan]) == 2)
                    {
                        iteratorVariable1 = iteratorVariable1 + "[" + ColorSet.color_titan_player + "] <T> ";
                    }
                    str = iteratorVariable1;
                    str2 = string.Empty;
                    str2 = (player6.uiname.Length > 100) ? player6.uiname.Substring(0, 20) : player6.uiname;
                    num17 = 0;
                    num17 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.kills]);
                    num18 = 0;
                    num18 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.deaths]);
                    num19 = 0;
                    num19 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.max_dmg]);
                    num20 = 0;
                    num20 = RCextensions.returnIntFromObject(player6.customProperties[PhotonPlayerProperty.total_dmg]);
                    iteratorVariable1 = string.Concat(new object[] { str, string.Empty, str2, "[ffffff]:", num17, "/", num18, "/", num19, "/", num20 });
                    if (RCextensions.returnBoolFromObject(player6.customProperties[PhotonPlayerProperty.dead]))
                    {
                        iteratorVariable1 = iteratorVariable1 + "[-]";
                    }
                    iteratorVariable1 = iteratorVariable1 + "\n";
                }
            }
        }
        this.playerList = iteratorVariable1;
        if (PhotonNetwork.isMasterClient && ((!this.isWinning && !this.isLosing) && (this.roundTime >= 5f)))
        {
            int num22;
            if (GameSettings.infectionMode > 0)
            {
                int num21 = 0;
                for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                {
                    PhotonPlayer targetPlayer = PhotonNetwork.playerList[num22];
                    if ((!ignoreList.Contains(targetPlayer.ID) && (targetPlayer.customProperties[PhotonPlayerProperty.dead] != null)) && (targetPlayer.customProperties[PhotonPlayerProperty.isTitan] != null))
                    {
                        if (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) == 1)
                        {
                            if (RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead]) && (RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.deaths]) > 0))
                            {
                                if (!imatitan.ContainsKey(targetPlayer.ID))
                                {
                                    imatitan.Add(targetPlayer.ID, 2);
                                }
                                ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
                                propertiesToSet.Add(PhotonPlayerProperty.isTitan, 2);
                                targetPlayer.SetCustomProperties(propertiesToSet);
                                PView.RPC("spawnTitanRPC", targetPlayer, new object[0]);
                            }
                            else if (imatitan.ContainsKey(targetPlayer.ID))
                            {
                                for (int j = 0; j < heroes.Count; j++)
                                {
                                    HERO hero = (HERO)heroes[j];
                                    if (hero.photonView.owner == targetPlayer)
                                    {
                                        hero.markDie();
                                        hero.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "noswitchingfagt" });
                                    }
                                }
                            }
                        }
                        else if (!((RCextensions.returnIntFromObject(targetPlayer.customProperties[PhotonPlayerProperty.isTitan]) != 2) || RCextensions.returnBoolFromObject(targetPlayer.customProperties[PhotonPlayerProperty.dead])))
                        {
                            num21++;
                        }
                    }
                }
                if ((num21 <= 0) && (IN_GAME_MAIN_CAMERA.gamemode != GAMEMODE.KILL_TITAN))
                {
                    this.gameWin2();
                }
            }
            else if (GameSettings.pointMode > 0)
            {
                if (GameSettings.teamMode > 0)
                {
                    if (this.cyanKills >= GameSettings.pointMode)
                    {
                        object[] parameters = new object[] { "<color=#00FFFF>Team Cyan wins! </color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, parameters);
                        this.gameWin2();
                    }
                    else if (this.magentaKills >= GameSettings.pointMode)
                    {
                        objArray2 = new object[] { "<color=#FF00FF>Team Magenta wins! </color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, objArray2);
                        this.gameWin2();
                    }
                }
                else if (GameSettings.teamMode == 0)
                {
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        PhotonPlayer player9 = PhotonNetwork.playerList[num22];
                        if (RCextensions.returnIntFromObject(player9.customProperties[PhotonPlayerProperty.kills]) >= GameSettings.pointMode)
                        {
                            object[] objArray4 = new object[] { "<color=#FFCC00>" + RCextensions.returnStringFromObject(player9.customProperties[PhotonPlayerProperty.name]).hexColor() + " wins!</color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray4);
                            this.gameWin2();
                        }
                    }
                }
            }
            else if ((GameSettings.pointMode <= 0) && ((GameSettings.bombMode == 1) || (GameSettings.pvpMode > 0)))
            {
                if ((GameSettings.teamMode > 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num24 = 0;
                    int num25 = 0;
                    int num26 = 0;
                    int num27 = 0;
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        PhotonPlayer player10 = PhotonNetwork.playerList[num22];
                        if ((!ignoreList.Contains(player10.ID) && (player10.customProperties[PhotonPlayerProperty.RCteam] != null)) && (player10.customProperties[PhotonPlayerProperty.dead] != null))
                        {
                            if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 1)
                            {
                                num26++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num24++;
                                }
                            }
                            else if (RCextensions.returnIntFromObject(player10.customProperties[PhotonPlayerProperty.RCteam]) == 2)
                            {
                                num27++;
                                if (!RCextensions.returnBoolFromObject(player10.customProperties[PhotonPlayerProperty.dead]))
                                {
                                    num25++;
                                }
                            }
                        }
                    }
                    if ((num26 > 0) && (num27 > 0))
                    {
                        if (num24 == 0)
                        {
                            object[] objArray5 = new object[] { "<color=#FF00FF>Team Magenta wins! </color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray5);
                            this.gameWin2();
                        }
                        else if (num25 == 0)
                        {
                            object[] objArray6 = new object[] { "<color=#00FFFF>Team Cyan wins! </color>", string.Empty };
                            PView.RPC("Chat", PhotonTargets.All, objArray6);
                            this.gameWin2();
                        }
                    }
                }
                else if ((GameSettings.teamMode == 0) && (PhotonNetwork.playerList.Length > 1))
                {
                    int num28 = 0;
                    string text = "Nobody";
                    PhotonPlayer player11 = PhotonNetwork.playerList[0];
                    for (num22 = 0; num22 < PhotonNetwork.playerList.Length; num22++)
                    {
                        PhotonPlayer player12 = PhotonNetwork.playerList[num22];
                        if (!((player12.customProperties[PhotonPlayerProperty.dead] == null) || RCextensions.returnBoolFromObject(player12.customProperties[PhotonPlayerProperty.dead])))
                        {
                            text = (player11 = player12).uiname;
                            //player11 = player12;
                            num28++;
                        }
                    }
                    if (num28 <= 1)
                    {
                        string str4 = " 5 points added.";
                        if (text == "Nobody")
                        {
                            str4 = string.Empty;
                        }
                        else
                        {
                            for (num22 = 0; num22 < 5; num22++)
                            {
                                this.playerKillInfoUpdate(player11, 0);
                            }
                        }
                        object[] objArray7 = new object[] { "<color=#FFCC00>" + text.hexColor() + " wins." + str4 + "</color>", string.Empty };
                        PView.RPC("Chat", PhotonTargets.All, objArray7);
                        this.gameWin2();
                    }
                }
            }
        }
        this.isRecompiling = false;
        this.ShowHUDInfoTopLeft(playerList);
    }

    public IEnumerator WaitAndReloadKDR(PhotonPlayer player)
    {
        yield return new WaitForSeconds(5f);
        string key = RCextensions.returnStringFromObject(player.customProperties[PhotonPlayerProperty.name]);
        if (this.PreservedPlayerKDR.ContainsKey(key))
        {
            int[] numArray = this.PreservedPlayerKDR[key];
            this.PreservedPlayerKDR.Remove(key);
            ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
            propertiesToSet.Add(PhotonPlayerProperty.kills, numArray[0]);
            propertiesToSet.Add(PhotonPlayerProperty.deaths, numArray[1]);
            propertiesToSet.Add(PhotonPlayerProperty.max_dmg, numArray[2]);
            propertiesToSet.Add(PhotonPlayerProperty.total_dmg, numArray[3]);
            player.SetCustomProperties(propertiesToSet);
        }
    }

    public IEnumerator WaitAndResetRestarts()
    {
        yield return new WaitForSeconds(10f);
        this.restartingBomb = false;
        this.restartingEren = false;
        this.restartingHorse = false;
        this.restartingMC = false;
        this.restartingTitan = false;
    }

    public IEnumerator WaitAndRespawn1(float time, string str)
    {
        yield return new WaitForSeconds(time);
        this.SpawnPlayer(this.myLastHero, str);
    }

    public IEnumerator WaitAndRespawn2(float time, GameObject pos)
    {
        yield return new WaitForSeconds(time);
        this.SpawnPlayerAt2(this.myLastHero, pos);
    }
















    private enum LoginStates
    {
        notlogged,
        loggingin,
        loginfailed,
        loggedin
    }
}

