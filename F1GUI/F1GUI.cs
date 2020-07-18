using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BRM
{
    class F1GUI : MonoBehaviour
    {
        public static bool TestGUIWindowOn;
        public int ChooseSettings = 0;
        public int ChooseButt1;
        public int ChooseButt2;
        public int ChooseButt3;
        public bool Toggle;
        public Vector2 Scroll;
        public Vector2 Scroll1;
        private bool onAppledStyle;

        void Start()
        {
            //onAppledStyle = true;
        }

        void OnGUI()
        {
            if (TestGUIWindowOn)
            {
                TextEditor editor;
             
                Texture2D textured;
                Mesh mesh;
                string[] strArray15;
                string textoff = "<color=#b5ceff> has been turned</color><i><color=#7b001c> off.</color></i>";
                string texton = "<color=#b5ceff> has been turned</color><i><color=#7b001c> on.</color></i>";
                string textnextoff = "<color=#7b001c><i> disabled</i></color> <color=#b5ceff>upon restart.</color>";
                string textnexton = "<color=#7b001c><i> enabled</i></color> <color=#b5ceff>upon restart. </color>";
                string str;
                bool sended = true;
                Screen.showCursor = true;
                    Screen.lockCursor = false;               
                    if (((int)FengGameManagerMKII.settings[0x40]) != 6)
                    {
                        GUI.backgroundColor = Color.gray;
                        Rect position = new Rect(0f, 0f, 247f, 609f);
                        GUI.Box(position, string.Empty);
                        GUI.Box(position, string.Empty);
                        GUI.Box(position, string.Empty);
                        GUI.backgroundColor = Color.gray;
                        GUI.Box(new Rect(5f, 5f, 237f, 555f), string.Empty);
                        GUI.DrawTexture(new Rect(5f, 5f, 237f, 555f), FengGameManagerMKII.instance.textureBackgroundBlack);   
                        GUILayout.BeginArea(new Rect(10f, 10f, 227f, 90f));
                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                        if (GUILayout.Button("General")) ChooseSettings = 0;                    
                        else if (GUILayout.Button("Human")) ChooseSettings = 1;
                        else if (GUILayout.Button("Map")) ChooseSettings = 2;
                        else if (GUILayout.Button("Titan")) ChooseSettings = 3;
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                        if (GUILayout.Button("Levels")) ChooseSettings = 5;
                        else if (GUILayout.Button("Sky")) ChooseSettings = 6;
                        else if (GUILayout.Button("Rebinds")) ChooseSettings = 7;
                        else if (GUILayout.Button("UIName")) ChooseSettings = 8;
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                        if (GUILayout.Button("Abilities")) ChooseSettings = 4;
                        if (GUILayout.Button("More")) ChooseSettings = 9;
                        GUILayout.EndHorizontal();
                        GUILayout.EndArea();
                        if (GUI.Button(new Rect(5f, 585f, 116f, 18f), "Close"))
                        {
                            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                            {
                                Time.timeScale = 1f;
                            }
                            if (!IN_GAME_MAIN_CAMERA.mainCamera.enabled)
                            {
                                Screen.showCursor = true;
                                Screen.lockCursor = true;
                                GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                                Camera.main.GetComponent<SpectatorMovement>().disable = false;
                                Camera.main.GetComponent<MouseLook>().disable = false;
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
                                GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                                GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().justUPDATEME();
                            }
                        }
                        if (GUI.Button(new Rect(126f, 585f, 116f, 18f), "<color=red>Quit</color>"))
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
                            FengGameManagerMKII.instance.gameStart = false;
                            GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                            FengGameManagerMKII.instance.DestroyAllExistingCloths();
                            UnityEngine.Object.Destroy(GameObject.Find("MultiplayerManager"));
                            Application.LoadLevel("menu");
                        }
                        if (GUI.Button(new Rect(126f, 565f, 116f, 15f), "Load"))
                        {
                            FengGameManagerMKII.instance.loadconfig();
                            FengGameManagerMKII.settings[0x40] = 5;
                        }
                        else if (GUI.Button(new Rect(5f, 565f, 116f, 15f), "Save"))
                        {
                        PlayerPrefs.SetInt("human", (int)FengGameManagerMKII.settings[0]);
                        PlayerPrefs.SetInt("titan", (int)FengGameManagerMKII.settings[1]);
                        PlayerPrefs.SetInt("level", (int)FengGameManagerMKII.settings[2]);
                        PlayerPrefs.SetString("horse", (string)FengGameManagerMKII.settings[3]);
                        PlayerPrefs.SetString("hair", (string)FengGameManagerMKII.settings[4]);
                        PlayerPrefs.SetString("eye", (string)FengGameManagerMKII.settings[5]);
                        PlayerPrefs.SetString("glass", (string)FengGameManagerMKII.settings[6]);
                        PlayerPrefs.SetString("face", (string)FengGameManagerMKII.settings[7]);
                        PlayerPrefs.SetString("skin", (string)FengGameManagerMKII.settings[8]);
                        PlayerPrefs.SetString("costume", (string)FengGameManagerMKII.settings[9]);
                        PlayerPrefs.SetString("logo", (string)FengGameManagerMKII.settings[10]);
                        PlayerPrefs.SetString("bladel", (string)FengGameManagerMKII.settings[11]);
                        PlayerPrefs.SetString("blader", (string)FengGameManagerMKII.settings[12]);
                        PlayerPrefs.SetString("gas", (string)FengGameManagerMKII.settings[13]);
                        PlayerPrefs.SetString("haircolor", (string)FengGameManagerMKII.settings[14]);
                        PlayerPrefs.SetInt("gasenable", (int)FengGameManagerMKII.settings[15]);
                        PlayerPrefs.SetInt("titantype1", (int)FengGameManagerMKII.settings[0x10]);
                        PlayerPrefs.SetInt("titantype2", (int)FengGameManagerMKII.settings[0x11]);
                        PlayerPrefs.SetInt("titantype3", (int)FengGameManagerMKII.settings[0x12]);
                        PlayerPrefs.SetInt("titantype4", (int)FengGameManagerMKII.settings[0x13]);
                        PlayerPrefs.SetInt("titantype5", (int)FengGameManagerMKII.settings[20]);
                        PlayerPrefs.SetString("titanhair1", (string)FengGameManagerMKII.settings[0x15]);
                        PlayerPrefs.SetString("titanhair2", (string)FengGameManagerMKII.settings[0x16]);
                        PlayerPrefs.SetString("titanhair3", (string)FengGameManagerMKII.settings[0x17]);
                        PlayerPrefs.SetString("titanhair4", (string)FengGameManagerMKII.settings[0x18]);
                        PlayerPrefs.SetString("titanhair5", (string)FengGameManagerMKII.settings[0x19]);
                        PlayerPrefs.SetString("titaneye1", (string)FengGameManagerMKII.settings[0x1a]);
                        PlayerPrefs.SetString("titaneye2", (string)FengGameManagerMKII.settings[0x1b]);
                        PlayerPrefs.SetString("titaneye3", (string)FengGameManagerMKII.settings[0x1c]);
                        PlayerPrefs.SetString("titaneye4", (string)FengGameManagerMKII.settings[0x1d]);
                        PlayerPrefs.SetString("titaneye5", (string)FengGameManagerMKII.settings[30]);
                        PlayerPrefs.SetInt("titanR", (int)FengGameManagerMKII.settings[0x20]);
                        PlayerPrefs.SetString("tree1", (string)FengGameManagerMKII.settings[0x21]);
                        PlayerPrefs.SetString("tree2", (string)FengGameManagerMKII.settings[0x22]);
                        PlayerPrefs.SetString("tree3", (string)FengGameManagerMKII.settings[0x23]);
                        PlayerPrefs.SetString("tree4", (string)FengGameManagerMKII.settings[0x24]);
                        PlayerPrefs.SetString("tree5", (string)FengGameManagerMKII.settings[0x25]);
                        PlayerPrefs.SetString("tree6", (string)FengGameManagerMKII.settings[0x26]);
                        PlayerPrefs.SetString("tree7", (string)FengGameManagerMKII.settings[0x27]);
                        PlayerPrefs.SetString("tree8", (string)FengGameManagerMKII.settings[40]);
                        PlayerPrefs.SetString("leaf1", (string)FengGameManagerMKII.settings[0x29]);
                        PlayerPrefs.SetString("leaf2", (string)FengGameManagerMKII.settings[0x2a]);
                        PlayerPrefs.SetString("leaf3", (string)FengGameManagerMKII.settings[0x2b]);
                        PlayerPrefs.SetString("leaf4", (string)FengGameManagerMKII.settings[0x2c]);
                        PlayerPrefs.SetString("leaf5", (string)FengGameManagerMKII.settings[0x2d]);
                        PlayerPrefs.SetString("leaf6", (string)FengGameManagerMKII.settings[0x2e]);
                        PlayerPrefs.SetString("leaf7", (string)FengGameManagerMKII.settings[0x2f]);
                        PlayerPrefs.SetString("leaf8", (string)FengGameManagerMKII.settings[0x30]);
                        PlayerPrefs.SetString("forestG", (string)FengGameManagerMKII.settings[0x31]);
                        PlayerPrefs.SetInt("forestR", (int)FengGameManagerMKII.settings[50]);
                        PlayerPrefs.SetString("house1", (string)FengGameManagerMKII.settings[0x33]);
                        PlayerPrefs.SetString("house2", (string)FengGameManagerMKII.settings[0x34]);
                        PlayerPrefs.SetString("house3", (string)FengGameManagerMKII.settings[0x35]);
                        PlayerPrefs.SetString("house4", (string)FengGameManagerMKII.settings[0x36]);
                        PlayerPrefs.SetString("house5", (string)FengGameManagerMKII.settings[0x37]);
                        PlayerPrefs.SetString("house6", (string)FengGameManagerMKII.settings[0x38]);
                        PlayerPrefs.SetString("house7", (string)FengGameManagerMKII.settings[0x39]);
                        PlayerPrefs.SetString("house8", (string)FengGameManagerMKII.settings[0x3a]);
                        PlayerPrefs.SetString("cityG", (string)FengGameManagerMKII.settings[0x3b]);
                        PlayerPrefs.SetString("cityW", (string)FengGameManagerMKII.settings[60]);
                        PlayerPrefs.SetString("cityH", (string)FengGameManagerMKII.settings[0x3d]);
                        PlayerPrefs.SetInt("skinQ", QualitySettings.masterTextureLimit);
                        PlayerPrefs.SetInt("skinQL", (int)FengGameManagerMKII.settings[0x3f]);
                        PlayerPrefs.SetString("eren", (string)FengGameManagerMKII.settings[0x41]);
                        PlayerPrefs.SetString("annie", (string)FengGameManagerMKII.settings[0x42]);
                        PlayerPrefs.SetString("colossal", (string)FengGameManagerMKII.settings[0x43]);
                        PlayerPrefs.SetString("hoodie", (string)FengGameManagerMKII.settings[14]);
                        PlayerPrefs.SetString("cnumber", (string)FengGameManagerMKII.settings[0x52]);
                        PlayerPrefs.SetString("cmax", (string)FengGameManagerMKII.settings[0x55]);
                        PlayerPrefs.SetString("titanbody1", (string)FengGameManagerMKII.settings[0x56]);
                        PlayerPrefs.SetString("titanbody2", (string)FengGameManagerMKII.settings[0x57]);
                        PlayerPrefs.SetString("titanbody3", (string)FengGameManagerMKII.settings[0x58]);
                        PlayerPrefs.SetString("titanbody4", (string)FengGameManagerMKII.settings[0x59]);
                        PlayerPrefs.SetString("titanbody5", (string)FengGameManagerMKII.settings[90]);
                        PlayerPrefs.SetInt("customlevel", (int)FengGameManagerMKII.settings[0x5b]);
                        PlayerPrefs.SetInt("traildisable", (int)FengGameManagerMKII.settings[0x5c]);
                        PlayerPrefs.SetInt("wind", (int)FengGameManagerMKII.settings[0x5d]);
                        PlayerPrefs.SetString("trailskin", (string)FengGameManagerMKII.settings[0x5e]);
                        PlayerPrefs.SetString("snapshot", (string)FengGameManagerMKII.settings[0x5f]);
                        PlayerPrefs.SetString("trailskin2", (string)FengGameManagerMKII.settings[0x60]);
                        PlayerPrefs.SetInt("reel", (int)FengGameManagerMKII.settings[0x61]);
                        PlayerPrefs.SetString("reelin", (string)FengGameManagerMKII.settings[0x62]);
                        PlayerPrefs.SetString("reelout", (string)FengGameManagerMKII.settings[0x63]);
                        PlayerPrefs.SetFloat("vol", AudioListener.volume);
                        PlayerPrefs.SetString("tforward", (string)FengGameManagerMKII.settings[0x65]);
                        PlayerPrefs.SetString("tback", (string)FengGameManagerMKII.settings[0x66]);
                        PlayerPrefs.SetString("tleft", (string)FengGameManagerMKII.settings[0x67]);
                        PlayerPrefs.SetString("tright", (string)FengGameManagerMKII.settings[0x68]);
                        PlayerPrefs.SetString("twalk", (string)FengGameManagerMKII.settings[0x69]);
                        PlayerPrefs.SetString("tjump", (string)FengGameManagerMKII.settings[0x6a]);
                        PlayerPrefs.SetString("tpunch", (string)FengGameManagerMKII.settings[0x6b]);
                        PlayerPrefs.SetString("tslam", (string)FengGameManagerMKII.settings[0x6c]);
                        PlayerPrefs.SetString("tgrabfront", (string)FengGameManagerMKII.settings[0x6d]);
                        PlayerPrefs.SetString("tgrabback", (string)FengGameManagerMKII.settings[110]);
                        PlayerPrefs.SetString("tgrabnape", (string)FengGameManagerMKII.settings[0x6f]);
                        PlayerPrefs.SetString("tantiae", (string)FengGameManagerMKII.settings[0x70]);
                        PlayerPrefs.SetString("tbite", (string)FengGameManagerMKII.settings[0x71]);
                        PlayerPrefs.SetString("tcover", (string)FengGameManagerMKII.settings[0x72]);
                        PlayerPrefs.SetString("tsit", (string)FengGameManagerMKII.settings[0x73]);
                        PlayerPrefs.SetInt("reel2", (int)FengGameManagerMKII.settings[0x74]);
                        PlayerPrefs.SetInt("humangui", (int)FengGameManagerMKII.settings[0x85]);
                        PlayerPrefs.SetString("horse2", (string)FengGameManagerMKII.settings[0x86]);
                        PlayerPrefs.SetString("hair2", (string)FengGameManagerMKII.settings[0x87]);
                        PlayerPrefs.SetString("eye2", (string)FengGameManagerMKII.settings[0x88]);
                        PlayerPrefs.SetString("glass2", (string)FengGameManagerMKII.settings[0x89]);
                        PlayerPrefs.SetString("face2", (string)FengGameManagerMKII.settings[0x8a]);
                        PlayerPrefs.SetString("skin2", (string)FengGameManagerMKII.settings[0x8b]);
                        PlayerPrefs.SetString("costume2", (string)FengGameManagerMKII.settings[140]);
                        PlayerPrefs.SetString("logo2", (string)FengGameManagerMKII.settings[0x8d]);
                        PlayerPrefs.SetString("bladel2", (string)FengGameManagerMKII.settings[0x8e]);
                        PlayerPrefs.SetString("blader2", (string)FengGameManagerMKII.settings[0x8f]);
                        PlayerPrefs.SetString("gas2", (string)FengGameManagerMKII.settings[0x90]);
                        PlayerPrefs.SetString("hoodie2", (string)FengGameManagerMKII.settings[0x91]);
                        PlayerPrefs.SetString("trail2", (string)FengGameManagerMKII.settings[0x92]);
                        PlayerPrefs.SetString("horse3", (string)FengGameManagerMKII.settings[0x93]);
                        PlayerPrefs.SetString("hair3", (string)FengGameManagerMKII.settings[0x94]);
                        PlayerPrefs.SetString("eye3", (string)FengGameManagerMKII.settings[0x95]);
                        PlayerPrefs.SetString("glass3", (string)FengGameManagerMKII.settings[150]);
                        PlayerPrefs.SetString("face3", (string)FengGameManagerMKII.settings[0x97]);
                        PlayerPrefs.SetString("skin3", (string)FengGameManagerMKII.settings[0x98]);
                        PlayerPrefs.SetString("costume3", (string)FengGameManagerMKII.settings[0x99]);
                        PlayerPrefs.SetString("logo3", (string)FengGameManagerMKII.settings[0x9a]);
                        PlayerPrefs.SetString("bladel3", (string)FengGameManagerMKII.settings[0x9b]);
                        PlayerPrefs.SetString("blader3", (string)FengGameManagerMKII.settings[0x9c]);
                        PlayerPrefs.SetString("gas3", (string)FengGameManagerMKII.settings[0x9d]);
                        PlayerPrefs.SetString("hoodie3", (string)FengGameManagerMKII.settings[0x9e]);
                        PlayerPrefs.SetString("trail3", (string)FengGameManagerMKII.settings[0x9f]);
                        PlayerPrefs.SetString("customGround", (string)FengGameManagerMKII.settings[0xa2]);
                        PlayerPrefs.SetString("forestskyfront", (string)FengGameManagerMKII.settings[0xa3]);
                        PlayerPrefs.SetString("forestskyback", (string)FengGameManagerMKII.settings[0xa4]);
                        PlayerPrefs.SetString("forestskyleft", (string)FengGameManagerMKII.settings[0xa5]);
                        PlayerPrefs.SetString("forestskyright", (string)FengGameManagerMKII.settings[0xa6]);
                        PlayerPrefs.SetString("forestskyup", (string)FengGameManagerMKII.settings[0xa7]);
                        PlayerPrefs.SetString("forestskydown", (string)FengGameManagerMKII.settings[0xa8]);
                        PlayerPrefs.SetString("cityskyfront", (string)FengGameManagerMKII.settings[0xa9]);
                        PlayerPrefs.SetString("cityskyback", (string)FengGameManagerMKII.settings[170]);
                        PlayerPrefs.SetString("cityskyleft", (string)FengGameManagerMKII.settings[0xab]);
                        PlayerPrefs.SetString("cityskyright", (string)FengGameManagerMKII.settings[0xac]);
                        PlayerPrefs.SetString("cityskyup", (string)FengGameManagerMKII.settings[0xad]);
                        PlayerPrefs.SetString("cityskydown", (string)FengGameManagerMKII.settings[0xae]);
                        PlayerPrefs.SetString("customskyfront", (string)FengGameManagerMKII.settings[0xaf]);
                        PlayerPrefs.SetString("customskyback", (string)FengGameManagerMKII.settings[0xb0]);
                        PlayerPrefs.SetString("customskyleft", (string)FengGameManagerMKII.settings[0xb1]);
                        PlayerPrefs.SetString("customskyright", (string)FengGameManagerMKII.settings[0xb2]);
                        PlayerPrefs.SetString("customskyup", (string)FengGameManagerMKII.settings[0xb3]);
                        PlayerPrefs.SetString("customskydown", (string)FengGameManagerMKII.settings[180]);
                        PlayerPrefs.SetInt("dashenable", (int)FengGameManagerMKII.settings[0xb5]);
                        PlayerPrefs.SetString("dashkey", (string)FengGameManagerMKII.settings[0xb6]);
                        PlayerPrefs.SetInt("vsync", (int)FengGameManagerMKII.settings[0xb7]);
                        PlayerPrefs.SetString("fpscap", (string)FengGameManagerMKII.settings[0xb8]);
                        PlayerPrefs.SetInt("speedometer", (int)FengGameManagerMKII.settings[0xbd]);
                        PlayerPrefs.SetInt("bombMode", (int)FengGameManagerMKII.settings[0xc0]);
                        PlayerPrefs.SetInt("teamMode", (int)FengGameManagerMKII.settings[0xc1]);
                        PlayerPrefs.SetInt("rockThrow", (int)FengGameManagerMKII.settings[0xc2]);
                        PlayerPrefs.SetInt("explodeModeOn", (int)FengGameManagerMKII.settings[0xc3]);
                        PlayerPrefs.SetString("explodeModeNum", (string)FengGameManagerMKII.settings[0xc4]);
                        PlayerPrefs.SetInt("healthMode", (int)FengGameManagerMKII.settings[0xc5]);
                        PlayerPrefs.SetString("healthLower", (string)FengGameManagerMKII.settings[0xc6]);
                        PlayerPrefs.SetString("healthUpper", (string)FengGameManagerMKII.settings[0xc7]);
                        PlayerPrefs.SetInt("infectionModeOn", (int)FengGameManagerMKII.settings[200]);
                        PlayerPrefs.SetString("infectionModeNum", (string)FengGameManagerMKII.settings[0xc9]);
                        PlayerPrefs.SetInt("banEren", (int)FengGameManagerMKII.settings[0xca]);
                        PlayerPrefs.SetInt("moreTitanOn", (int)FengGameManagerMKII.settings[0xcb]);
                        PlayerPrefs.SetString("moreTitanNum", (string)FengGameManagerMKII.settings[0xcc]);
                        PlayerPrefs.SetInt("damageModeOn", (int)FengGameManagerMKII.settings[0xcd]);
                        PlayerPrefs.SetString("damageModeNum", (string)FengGameManagerMKII.settings[0xce]);
                        PlayerPrefs.SetInt("sizeMode", (int)FengGameManagerMKII.settings[0xcf]);
                        PlayerPrefs.SetString("sizeLower", (string)FengGameManagerMKII.settings[0xd0]);
                        PlayerPrefs.SetString("sizeUpper", (string)FengGameManagerMKII.settings[0xd1]);
                        PlayerPrefs.SetInt("spawnModeOn", (int)FengGameManagerMKII.settings[210]);
                        PlayerPrefs.SetString("nRate", (string)FengGameManagerMKII.settings[0xd3]);
                        PlayerPrefs.SetString("aRate", (string)FengGameManagerMKII.settings[0xd4]);
                        PlayerPrefs.SetString("jRate", (string)FengGameManagerMKII.settings[0xd5]);
                        PlayerPrefs.SetString("cRate", (string)FengGameManagerMKII.settings[0xd6]);
                        PlayerPrefs.SetString("pRate", (string)FengGameManagerMKII.settings[0xd7]);
                        PlayerPrefs.SetInt("horseMode", (int)FengGameManagerMKII.settings[0xd8]);
                        PlayerPrefs.SetInt("waveModeOn", (int)FengGameManagerMKII.settings[0xd9]);
                        PlayerPrefs.SetString("waveModeNum", (string)FengGameManagerMKII.settings[0xda]);
                        PlayerPrefs.SetInt("friendlyMode", (int)FengGameManagerMKII.settings[0xdb]);
                        PlayerPrefs.SetInt("pvpMode", (int)FengGameManagerMKII.settings[220]);
                        PlayerPrefs.SetInt("maxWaveOn", (int)FengGameManagerMKII.settings[0xdd]);
                        PlayerPrefs.SetString("maxWaveNum", (string)FengGameManagerMKII.settings[0xde]);
                        PlayerPrefs.SetInt("endlessModeOn", (int)FengGameManagerMKII.settings[0xdf]);
                        PlayerPrefs.SetString("endlessModeNum", (string)FengGameManagerMKII.settings[0xe0]);
                        PlayerPrefs.SetString("motd", (string)FengGameManagerMKII.settings[0xe1]);
                        PlayerPrefs.SetInt("pointModeOn", (int)FengGameManagerMKII.settings[0xe2]);
                        PlayerPrefs.SetString("pointModeNum", (string)FengGameManagerMKII.settings[0xe3]);
                        PlayerPrefs.SetInt("ahssReload", (int)FengGameManagerMKII.settings[0xe4]);
                        PlayerPrefs.SetInt("punkWaves", (int)FengGameManagerMKII.settings[0xe5]);
                        PlayerPrefs.SetInt("mapOn", (int)FengGameManagerMKII.settings[0xe7]);
                        PlayerPrefs.SetString("mapMaximize", (string)FengGameManagerMKII.settings[0xe8]);
                        PlayerPrefs.SetString("mapToggle", (string)FengGameManagerMKII.settings[0xe9]);
                        PlayerPrefs.SetString("mapReset", (string)FengGameManagerMKII.settings[0xea]);
                        PlayerPrefs.SetInt("globalDisableMinimap", (int)FengGameManagerMKII.settings[0xeb]);
                        PlayerPrefs.SetString("chatRebind", (string)FengGameManagerMKII.settings[0xec]);
                        PlayerPrefs.SetString("hforward", (string)FengGameManagerMKII.settings[0xed]);
                        PlayerPrefs.SetString("hback", (string)FengGameManagerMKII.settings[0xee]);
                        PlayerPrefs.SetString("hleft", (string)FengGameManagerMKII.settings[0xef]);
                        PlayerPrefs.SetString("hright", (string)FengGameManagerMKII.settings[240]);
                        PlayerPrefs.SetString("hwalk", (string)FengGameManagerMKII.settings[0xf1]);
                        PlayerPrefs.SetString("hjump", (string)FengGameManagerMKII.settings[0xf2]);
                        PlayerPrefs.SetString("hmount", (string)FengGameManagerMKII.settings[0xf3]);
                        PlayerPrefs.SetInt("chatfeed", (int)FengGameManagerMKII.settings[0xf4]);
                        PlayerPrefs.SetFloat("bombR", (float)FengGameManagerMKII.settings[0xf6]);
                        PlayerPrefs.SetFloat("bombG", (float)FengGameManagerMKII.settings[0xf7]);
                        PlayerPrefs.SetFloat("bombB", (float)FengGameManagerMKII.settings[0xf8]);
                        PlayerPrefs.SetFloat("bombA", (float)FengGameManagerMKII.settings[0xf9]);
                        PlayerPrefs.SetInt("bombRadius", (int)FengGameManagerMKII.settings[250]);
                        PlayerPrefs.SetInt("bombRange", (int)FengGameManagerMKII.settings[0xfb]);
                        PlayerPrefs.SetInt("bombSpeed", (int)FengGameManagerMKII.settings[0xfc]);
                        PlayerPrefs.SetInt("bombCD", (int)FengGameManagerMKII.settings[0xfd]);
                        PlayerPrefs.SetString("cannonUp", (string)FengGameManagerMKII.settings[0xfe]);
                        PlayerPrefs.SetString("cannonDown", (string)FengGameManagerMKII.settings[0xff]);
                        PlayerPrefs.SetString("cannonLeft", (string)FengGameManagerMKII.settings[0x100]);
                        PlayerPrefs.SetString("cannonRight", (string)FengGameManagerMKII.settings[0x101]);
                        PlayerPrefs.SetString("cannonFire", (string)FengGameManagerMKII.settings[0x102]);
                        PlayerPrefs.SetString("cannonMount", (string)FengGameManagerMKII.settings[0x103]);
                        PlayerPrefs.SetString("cannonSlow", (string)FengGameManagerMKII.settings[260]);
                        PlayerPrefs.SetInt("deadlyCannon", (int)FengGameManagerMKII.settings[0x105]);
                        PlayerPrefs.SetString("liveCam", (string)FengGameManagerMKII.settings[0x106]);
                        //PlayerPrefs.SetString("titname", (string)FengGameManagerMKII.TitName);
                        //PlayerPrefs.SetString("titabername", (string)FengGameManagerMKII.TitAberName);
                        //PlayerPrefs.SetString("titjumpname", (string)FengGameManagerMKII.TitJumpName);
                        //PlayerPrefs.SetString("titpunkname", (string)FengGameManagerMKII.TitPunkName);
                        //PlayerPrefs.SetString("titcrawname", (string)FengGameManagerMKII.TitCrawName);
                        //PlayerPrefs.SetFloat("bgcolorR", (float)FengGameManagerMKII.BackGRColor1);
                        //PlayerPrefs.SetFloat("bgcolorG", (float)FengGameManagerMKII.BackGRColor2);
                        //PlayerPrefs.SetFloat("bgcolorB", (float)FengGameManagerMKII.BackGRColor3);
                        //PlayerPrefs.SetFloat("bgcolorA", (float)FengGameManagerMKII.BackGRColor4);
                        FengGameManagerMKII.settings[0x40] = 4;
                        PlayerPrefs.SetString("AnimatedName", (string)FengGameManagerMKII.settings[265]/* + (string)FengGameManagerMKII.settings[266]*/);
                        FengGameManagerMKII.settings[267] = PlayerPrefs.GetString("Faded", "None");
                        FengGameManagerMKII.settings[268] = PlayerPrefs.GetString("Linear", "None");
                        FengGameManagerMKII.settings[269] = PlayerPrefs.GetString("Rebound", "None");
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[267]);
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[268]);
                        //FengGameManagerMKII.EncodeAnimation((string)FengGameManagerMKII.settings[269]);
                        FengGameManagerMKII.settings[270] = PlayerPrefs.GetString("AnimatedName", "None");
                        //StatsTab.AddLine("Save configurate... FengGameManagerMKII.settings[265] = " + FengGameManagerMKII.settings[265].ToString() + "\n FengGameManagerMKII.settings[266] = " + FengGameManagerMKII.settings[266].ToString(), StatsTab.DebugType.LOG);
                        //if (PhotonNetwork.connectionStateDetailed == PeerStates.Joined)
                        //{
                        //    FengGameManagerMKII.instance.StartNameAnim((string)FengGameManagerMKII.settings[270] != "None");
                        //}
                    }                      
                        switch (ChooseSettings)
                        {
                            case 0: //General
                            GeneralGUI();
                            break;
                            case 1: //Human
                                break;
                            case 2: //Map
                                break;
                            case 3:

                                break;
                            case 4:
                                GUILayout.BeginArea(new Rect(5f, 100f, 237f, 480f));
                                GUILayout.Label("Bomb Mode", GUILayout.MaxWidth(237f));
                                int num42 = (((20 - ((int)FengGameManagerMKII.settings[250])) - ((int)FengGameManagerMKII.settings[0xfb])) - ((int)FengGameManagerMKII.settings[0xfc])) - ((int)FengGameManagerMKII.settings[0xfd]);


                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Color:</color></b>", "Label");
                                GUILayout.BeginVertical();
                                textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                                textured.SetPixel(0, 0, new Color((float)FengGameManagerMKII.settings[0xf6], (float)FengGameManagerMKII.settings[0xf7], (float)FengGameManagerMKII.settings[0xf8], (float)FengGameManagerMKII.settings[0xf9]));
                                textured.Apply();
                                GUI.DrawTexture(new Rect(120f, 113f, 40f, 15f), textured, ScaleMode.StretchToFill);
                                UnityEngine.Object.Destroy(textured);

                            GUILayout.BeginHorizontal();
                                GUILayout.Label("R:");
                                FengGameManagerMKII.settings[0xf6] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[0xf6], 0f, 1f);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("G:");
                                FengGameManagerMKII.settings[0xf7] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[0xf7], 0f, 1f);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("B:");
                                FengGameManagerMKII.settings[0xf8] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[0xf8], 0f, 1f);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("A:");
                                FengGameManagerMKII.settings[0xf9] = GUILayout.HorizontalSlider((float)FengGameManagerMKII.settings[0xf9], 0.5f, 1f);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Bomb Radius:");
                                GUILayout.Label(FengGameManagerMKII.settings[250].ToString());
                                if (GUILayout.Button("-"))
                                {
                                    if (((int)FengGameManagerMKII.settings[250]) > 0)
                                    {
                                        FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) - 1;
                                    }
                                }
                                else if ((GUILayout.Button("+") && (((int)FengGameManagerMKII.settings[250]) < 10)) && (num42 > 0))
                                {
                                    FengGameManagerMKII.settings[250] = ((int)FengGameManagerMKII.settings[250]) + 1;
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Bomb Range:");
                                GUILayout.Label(FengGameManagerMKII.settings[0xfb].ToString());
                                if (GUILayout.Button("-"))
                                {
                                    if (((int)FengGameManagerMKII.settings[0xfb]) > 0)
                                    {
                                        FengGameManagerMKII.settings[0xfb] = ((int)FengGameManagerMKII.settings[0xfb]) - 1;
                                    }
                                }
                                else if ((GUILayout.Button("+") && (((int)FengGameManagerMKII.settings[0xfb]) < 10)) && (num42 > 0))
                                {
                                    FengGameManagerMKII.settings[0xfb] = ((int)FengGameManagerMKII.settings[0xfb]) + 1;
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Bomb Speed:");
                                GUILayout.Label(FengGameManagerMKII.settings[0xfc].ToString());
                                if (GUILayout.Button("-"))
                                {
                                    if (((int)FengGameManagerMKII.settings[0xfc]) > 0)
                                    {
                                        FengGameManagerMKII.settings[0xfc] = ((int)FengGameManagerMKII.settings[0xfc]) - 1;
                                    }
                                }
                                else if ((GUILayout.Button("+") && (((int)FengGameManagerMKII.settings[0xfc]) < 10)) && (num42 > 0))
                                {
                                    FengGameManagerMKII.settings[0xfc] = ((int)FengGameManagerMKII.settings[0xfc]) + 1;
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Bomb CD:");
                                GUILayout.Label(FengGameManagerMKII.settings[0xfd].ToString());
                                if (GUILayout.Button("-"))
                                {
                                    if (((int)FengGameManagerMKII.settings[0xfd]) > 0)
                                    {
                                        FengGameManagerMKII.settings[0xfd] = ((int)FengGameManagerMKII.settings[0xfd]) - 1;
                                    }
                                }
                                else if ((GUILayout.Button("+") && (((int)FengGameManagerMKII.settings[0xfd]) < 10)) && (num42 > 0))
                                {
                                    FengGameManagerMKII.settings[0xfd] = ((int)FengGameManagerMKII.settings[0xfd]) + 1;
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("Unused Points:");
                                GUILayout.Label(num42.ToString());
                                GUILayout.EndHorizontal();
                                GUILayout.EndVertical();
                            GUILayout.EndArea();
                            break;
                            case 5:

                                break;
                            case 6:

                                break;
                            case 7:

                                break;
                            case 8:
                                FengGameManagerMKII.instance.UIName();
                                break;
                            case 9:


                            bool flag34;
                            bool flag35;
                            GUILayout.BeginArea(new Rect(5f, 100f, 237f, 480f));
                            Scroll1 = GUILayout.BeginScrollView(Scroll1, GUILayout.Width(237f), GUILayout.Height(460f));
                            GUILayout.BeginVertical();
                            if (GUILayout.Button("<b><color= " + FengGameManagerMKII.MainColor + ">Titans</color></b>")) ChooseButt1 = ChooseButt1 != 4 ? 4 : 0;
                            if (ChooseButt1 == 4)
                            {
                                
                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Custom Titan Number:</color></b>");
                                str = "<b><color=#d3eef2>NUMBERLOCK</color></b>";
                                flag34 = false;
                                if (((int)FengGameManagerMKII.settings[0xcb]) == 1)
                                {
                                    flag34 = true;
                                }
                                flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                                if (flag34 != flag35)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[0xcb] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[0xcb] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Amount (Integer):</color></b>");
                                FengGameManagerMKII.settings[0xcc] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xcc]);
                                if ((int)FengGameManagerMKII.settings[0xcb] == 1 && !sended)                             
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Kill <i><color=#7b001c>" + FengGameManagerMKII.settings[0xcc] + "</color></i> <b><color=#d3eef2>TITAN</color></b> to win.</color>");
                                    sended = true;
                                }
                                else
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Kill <i><color=#7b001c>10 </color></i><b><color=#d3eef2>TITAN</color></b> to win.</color>");
                                    sended = true;
                                }
                                GUILayout.EndHorizontal();



                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Custom Titan Spawns:</color></b>");
                                str = "<b><color=#d3eef2>CUSTOM SPAWN MODE</color></b>";
                                flag34 = false;
                                if ((int)FengGameManagerMKII.settings[210] == 1)
                                {
                                    flag34 = true;
                                }
                                flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                                if (flag34 != flag35)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[210] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[210] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Normal (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd3] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd3]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Aberrant (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd4] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd4]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Jumper (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd5] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd5]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Crawler (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd6] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd6]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Punk (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd7] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd7]);
                                GUILayout.EndHorizontal();

                                string txt = string.Empty;
                                int pos = 0;
                                if ((float)FengGameManagerMKII.settings[0xd3] > 0)
                                {
                                    txt += "<b><color=#b5ceff>NORMAL: </color></b><color=#7b001c><i>" + FengGameManagerMKII.settings[0xd3].ToString() + "</i></color><b><color=#b5ceff>%";
                                    if ((float)FengGameManagerMKII.settings[0xd4] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd5] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd6] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd7] > 0) txt += ", </color></b>";
                                    else txt += "</color></b>";
                                }
                                if ((float)FengGameManagerMKII.settings[0xd4] > 0)
                                {
                                    txt += "<b><color=#b5ceff>ABNORMAL: </color></b><color=#7b001c><i>" + FengGameManagerMKII.settings[0xd4].ToString() + "</i></color><b><color=#b5ceff>%";
                                    if ((float)FengGameManagerMKII.settings[0xd5] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd6] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd7] > 0) txt += ", </color></b>";
                                    else txt += "</color></b>";
                                }
                                if ((float)FengGameManagerMKII.settings[0xd5] > 0)
                                {
                                    txt += "<b><color=#b5ceff>JUMPER: </color></b><color=#7b001c><i>" + FengGameManagerMKII.settings[0xd5].ToString() + "</i></color><b><color=#b5ceff>%";
                                    if ((float)FengGameManagerMKII.settings[0xd6] > 0 ||
                                        (float)FengGameManagerMKII.settings[0xd7] > 0) txt += ", </color></b>";
                                    else txt += "</color></b>";
                                }
                                if ((float)FengGameManagerMKII.settings[0xd6] > 0)
                                {
                                    txt += "<b><color=#b5ceff>CRAWLER: </color></b><color=#7b001c><i>" + FengGameManagerMKII.settings[0xd6].ToString() + "</i></color><b><color=#b5ceff>%";
                                    if ((int)FengGameManagerMKII.settings[0xd7] > 0) txt += ", </color></b>";
                                    else txt += "</color></b>";
                                }
                                if ((float)FengGameManagerMKII.settings[0xd7] > 0)
                                {
                                    txt += "<b><color=#b5ceff>PUNK: </color></b><color=#7b001c><i>" + FengGameManagerMKII.settings[0xd7].ToString() + "</i></color><b><color=#b5ceff>%</color></b>";
                                }
                                if ((int)FengGameManagerMKII.settings[210] == 1 && !sended)
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Rate <b><color=#" + FengGameManagerMKII.MainColor + ">(" + txt + ")</color></b> will be <color=#7b001c><i>applied</i></color> in the next round.</color>");
                                    sended = true;
                                }
                                else
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Spawn rate will set to <i><color=#7b001c>default</color></i> in next round.</color>");
                                    sended = true;
                                }

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Titan Size Mode:</color></b>");
                                str = "<b><color=#d3eef2>TITAN SIZE MODE</color></b>";
                                flag34 = false;
                                if (((int)FengGameManagerMKII.settings[0xcf]) == 1)
                                {
                                    flag34 = true;
                                }
                                if ((flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>")) != flag34)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[0xcf] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[0xcf] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Minimum (Decimal):</color></b>");
                                FengGameManagerMKII.settings[0xd0] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd0]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Maximum (Decimal):</color></b>");
                                GUILayout.EndHorizontal();
                                FengGameManagerMKII.settings[0xd1] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xd1]);

                                if ((int)FengGameManagerMKII.settings[0xcf] == 1 && !sended)
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Titan size has been <i><color=#7b001c>altered</color></i> between <i><color=#7b001c>" + FengGameManagerMKII.settings[0xd0].ToString() + " meters</color></i> and <i><color=#7b001c>" + FengGameManagerMKII.settings[0xd1].ToString() + "</color></i> meters upon next spawn.</color>");
                                    sended = true;
                                }
                                else
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Titan size is back to normal.</color>");
                                    sended = true;
                                }


                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Titan Health Mode:</color></b>");
                                strArray15 = new string[] { "<b><color=#" + FengGameManagerMKII.MainColor + ">Off</color></b>", "<b><color=#" + FengGameManagerMKII.MainColor + ">Fixed</color></b>", "<b><color=#" + FengGameManagerMKII.MainColor + ">Scaled</color></b>" };
                                FengGameManagerMKII.settings[0xc5] = GUILayout.SelectionGrid((int)FengGameManagerMKII.settings[0xc5], strArray15, 1, GUI.skin.toggle);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Minimum (Integer):</color></b>");
                                FengGameManagerMKII.settings[0xc6] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xc6]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Maximum (Integer):</color></b>");
                                FengGameManagerMKII.settings[0xc7] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xc7]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Titan Damage Mode:</color></b>");
                                str = "<b><color=#d3eef2>DAMAGE MODE</color></b>";
                                flag34 = false;
                                if (((int)FengGameManagerMKII.settings[0xcd]) == 1)
                                {
                                    flag34 = true;
                                }
                                flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                                if (flag34 != flag35)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[0xcd] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[0xcd] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Damage (Integer):</color></b>");
                                FengGameManagerMKII.settings[0xce] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xce]);
                                GUILayout.EndHorizontal();
                                if ((int)FengGameManagerMKII.settings[0xcd] == 1 && !sended)
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Hit <i><color=#7b001c>" + FengGameManagerMKII.settings[0xce].ToString() + "</color> </i><b><color=#d3eef2>DAMAGE</color></b> to kill titan.</color>");
                                    sended = true;
                                }
                                else
                                {
                                    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Hit <i><color=#7b001c> 10</color></i><b><color=#d3eef2>DAMAGE</color></b> to kill titan.</color>");
                                    sended = true;
                                }

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Titan Explode Mode:</color></b>");
                                str = "<b><color=#d3eef2>TITAN EXPLODE MODE</color></b>";
                                flag34 = false;
                                if (((int)FengGameManagerMKII.settings[0xc3]) == 1)
                                {
                                    flag34 = true;
                                }
                                flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                                if (flag34 != flag35)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[0xc3] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[0xc3] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Radius (Integer):</color></b>", "Label");
                                FengGameManagerMKII.settings[0xc4] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xc4]);
                                GUILayout.EndHorizontal();

                                GUILayout.BeginHorizontal();
                                GUILayout.Label("<b><color=#" + FengGameManagerMKII.MainColor + ">Disable Rock Throwing:</color></b>", "Label");
                                str = "<b><color=#d3eef2>NO PUNKS ROCK THROWING</color></b>";
                                flag34 = false;
                                if (((int)FengGameManagerMKII.settings[0xc2]) == 1)
                                {
                                    flag34 = true;
                                }
                                flag35 = GUILayout.Toggle(flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                                if (flag34 != flag35)
                                {
                                    if (flag35)
                                    {
                                        FengGameManagerMKII.settings[0xc2] = 1;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnexton);
                                        sended = false;
                                    }
                                    else
                                    {
                                        FengGameManagerMKII.settings[0xc2] = 0;
                                        if (PhotonNetwork.isMasterClient) FengGameManagerMKII.instance.sendChatContentInfo(str + textnextoff);
                                        sended = false;
                                    }
                                }
                                GUILayout.EndHorizontal();
                            }




                            if (GUILayout.Button("<b><color= " + FengGameManagerMKII.MainColor + ">PVP</color></b>")) ChooseButt1 = ChooseButt1 != 5 ? 5 : 0;
                            if (GUILayout.Button("<b><color= " + FengGameManagerMKII.MainColor + ">Misc</color></b>")) ChooseButt1 = ChooseButt1 != 6 ? 6 : 0;
                            if (GUILayout.Button("<b><color=#" + FengGameManagerMKII.MainColor + ">Reset</color></b>"))
                            {
                                FengGameManagerMKII.settings[0xc0] = 0;
                                FengGameManagerMKII.settings[0xc1] = 0;
                                FengGameManagerMKII.settings[0xc2] = 0;
                                FengGameManagerMKII.settings[0xc3] = 0;
                                FengGameManagerMKII.settings[0xc4] = "30";
                                FengGameManagerMKII.settings[0xc5] = 0;
                                FengGameManagerMKII.settings[0xc6] = "100";
                                FengGameManagerMKII.settings[0xc7] = "200";
                                FengGameManagerMKII.settings[200] = 0;
                                FengGameManagerMKII.settings[0xc9] = "1";
                                FengGameManagerMKII.settings[0xca] = 0;
                                FengGameManagerMKII.settings[0xcb] = 0;
                                FengGameManagerMKII.settings[0xcc] = "1";
                                FengGameManagerMKII.settings[0xcd] = 0;
                                FengGameManagerMKII.settings[0xce] = "1000";
                                FengGameManagerMKII.settings[0xcf] = 0;
                                FengGameManagerMKII.settings[0xd0] = "1.0";
                                FengGameManagerMKII.settings[0xd1] = "3.0";
                                FengGameManagerMKII.settings[210] = 0;
                                FengGameManagerMKII.settings[0xd3] = "20.0";
                                FengGameManagerMKII.settings[0xd4] = "20.0";
                                FengGameManagerMKII.settings[0xd5] = "20.0";
                                FengGameManagerMKII.settings[0xd6] = "20.0";
                                FengGameManagerMKII.settings[0xd7] = "20.0";
                                FengGameManagerMKII.settings[0xd8] = 0;
                                FengGameManagerMKII.settings[0xd9] = 0;
                                FengGameManagerMKII.settings[0xda] = "1";
                                FengGameManagerMKII.settings[0xdb] = 0;
                                FengGameManagerMKII.settings[220] = 0;
                                FengGameManagerMKII.settings[0xdd] = 0;
                                FengGameManagerMKII.settings[0xde] = "20";
                                FengGameManagerMKII.settings[0xdf] = 0;
                                FengGameManagerMKII.settings[0xe0] = "10";
                                FengGameManagerMKII.settings[0xe1] = string.Empty;
                                FengGameManagerMKII.settings[0xe2] = 0;
                                FengGameManagerMKII.settings[0xe3] = "50";
                                FengGameManagerMKII.settings[0xe4] = 0;
                                FengGameManagerMKII.settings[0xe5] = 0;
                                FengGameManagerMKII.settings[0xeb] = 0;
                            }

                            //if (ChooseButt1 == 5)
                            //{
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 90f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Point Mode:</color></b>", "Label");
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 112f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Max Points (Integer):</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xe3] = GUILayout.TextField(new Rect(num + 250f, num2 + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[0xe3]);
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xe2]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 90f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xe2] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xe2] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 152f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">PVP Bomb Mode:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xc0]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 152f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xc0] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xc0] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 182f, 100f, 66f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Team Mode:</color></b>", "Label");
                            //    strArray15 = new string[] { "<b><color=#" + MainColor + ">Off</color></b>", "<b><color=#" + FengGameManagerMKII.MainColor + ">No Sort</color></b>", "<b><color=#" + MainColor + ">Size-Lock</color></b>", "<b><color=#" + MainColor + ">Skill-Lock</color></b>" };
                            //    FengGameManagerMKII.settings[0xc1] = GUILayout.SelectionGrid(new Rect(num + 250f, num2 + 182f, 120f, 88f), (int)FengGameManagerMKII.settings[0xc1], strArray15, 1, GUILayout.skin.toggle);
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 278f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Infection Mode:</color></b>", "Label");
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 300f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Starting Titans (Integer):</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xc9] = GUILayout.TextField(new Rect(num + 250f, num2 + 300f, 50f, 22f), (string)FengGameManagerMKII.settings[0xc9]);
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[200]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 278f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[200] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[200] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 330f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Friendly Mode:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xdb]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 330f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xdb] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xdb] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 90f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Sword/AHSS PVP:</color></b>", "Label");
                            //    strArray15 = new string[] { "<b><color=#" + FengGameManagerMKII.MainColor + ">Off</color></b>", "<b><color=#" + FengGameManagerMKII.MainColor + ">Teams</color></b>", "<b><color=#" + FengGameManagerMKII.MainColor + ">FFA</color></b>" };
                            //    FengGameManagerMKII.settings[220] = GUILayout.SelectionGrid(new Rect(num + 550f, num2 + 90f, 100f, 66f), (int)FengGameManagerMKII.settings[220], strArray15, 1, GUILayout.skin.toggle);
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 164f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">No AHSS Air-Reloading:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xe4]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 550f, num2 + 164f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xe4] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xe4] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 194f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Cannons kill humans:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0x105]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 550f, num2 + 194f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0x105] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0x105] = 0;
                            //        }
                            //    }
                            //}
                            //else if (ChooseButt1 == 6)
                            //{
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 90f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Custom Titans/Wave:</color></b>", "Label");
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 112f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Amount (Integer):</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xda] = GUILayout.TextField(new Rect(num + 250f, num2 + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[0xda]);
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xd9]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 90f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xd9] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xd9] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 152f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Maximum Waves:</color></b>", "Label");
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 174f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Amount (Integer):</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xde] = GUILayout.TextField(new Rect(num + 250f, num2 + 174f, 50f, 22f), (string)FengGameManagerMKII.settings[0xde]);
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xdd]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 152f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xdd] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xdd] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 214f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Punks every 5 waves:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xe5]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 214f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xe5] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xe5] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 100f, num2 + 244f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Global Minimap Disable:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xeb]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 250f, num2 + 244f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xeb] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xeb] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 90f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Endless Respawn:</color></b>", "Label");
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 112f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Respawn Time (Integer):</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xe0] = GUILayout.TextField(new Rect(num + 550f, num2 + 112f, 50f, 22f), (string)FengGameManagerMKII.settings[0xe0]);
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xdf]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 550f, num2 + 90f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xdf] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xdf] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 152f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Kick Eren Titan:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xca]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 550f, num2 + 152f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xca] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xca] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 182f, 160f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Allow Horses:</color></b>", "Label");
                            //    flag34 = false;
                            //    if (((int)FengGameManagerMKII.settings[0xd8]) == 1)
                            //    {
                            //        flag34 = true;
                            //    }
                            //    flag35 = GUILayout.Toggle(new Rect(num + 550f, num2 + 182f, 40f, 20f), flag34, "<b><color=#" + FengGameManagerMKII.MainColor + ">On</color></b>");
                            //    if (flag34 != flag35)
                            //    {
                            //        if (flag35)
                            //        {
                            //            FengGameManagerMKII.settings[0xd8] = 1;
                            //        }
                            //        else
                            //        {
                            //            FengGameManagerMKII.settings[0xd8] = 0;
                            //        }
                            //    }
                            //    GUILayout.Label(new Rect(num + 400f, num2 + 212f, 180f, 22f), "<b><color=#" + FengGameManagerMKII.MainColor + ">Message of the day:</color></b>", "Label");
                            //    FengGameManagerMKII.settings[0xe1] = GUILayout.TextField(new Rect(num + 400f, num2 + 234f, 200f, 22f), (string)FengGameManagerMKII.settings[0xe1]);
                            //}
                            GUILayout.EndScrollView();
                            GUILayout.EndVertical();
                            GUILayout.EndArea();
                            break;
                          
                            case 10:

                                break;
                            case 11:

                                break;
                            case 12:

                                break;                           
                        }
                    
                }








                //GUILayout.Box(new Rect(num, num2, 700f, 500f), string.Empty);
                //if (GUILayout.Button(new Rect(num + 7f, num2 + 7f, 59f, 25f), "<b><color=#" + MainColor + ">General</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 0;
                //}
                //else if (GUILayout.Button(new Rect(num + 71f, num2 + 7f, 60f, 25f), "<b><color=#" + MainColor + ">Rebinds</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 1;
                //}
                //else if (GUILayout.Button(new Rect(num + 136f, num2 + 7f, 85f, 25f), "<b><color=#" + MainColor + ">Human Skins</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 2;
                //}
                //else if (GUILayout.Button(new Rect(num + 226f, num2 + 7f, 85f, 25f), "<b><color=#" + MainColor + ">Titan Skins</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 3;
                //}
                //else if (GUILayout.Button(new Rect(num + 316f, num2 + 7f, 85f, 25f), "<b><color=#" + MainColor + ">Level Skins</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 7;
                //}
                //else if (GUILayout.Button(new Rect(num + 406f, num2 + 7f, 85f, 25f), "<b><color=#" + MainColor + ">Custom Map</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 8;
                //}
                //else if (GUILayout.Button(new Rect(num + 496f, num2 + 7f, 88f, 25f), "<b><color=#" + MainColor + ">Custom Logic</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 9;
                //}
                //else if (GUILayout.Button(new Rect(num + 589f, num2 + 7f, 95f, 25f), "<b><color=#" + MainColor + ">Game FengGameManagerMKII.settings</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 10;
                //}
                //else if (GUILayout.Button(new Rect(num + 7f, num2 + 37f, 70f, 25f), "<b><color=#" + MainColor + ">Abilities</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 11;
                //}
                //else if (GUILayout.Button(new Rect(num + 75f, num2 + 37f, 100f, 25f), "<b><color=#" + MainColor + ">Color FengGameManagerMKII.settings</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 12;
                //}
                //else if (GUILayout.Button(new Rect(num + 170f, num2 + 37f, 50f, 25f), "<b><color=#" + MainColor + ">More</color></b>"))
                //{
                //    FengGameManagerMKII.settings[0x40] = 13;
                //}

                //if (((int)FengGameManagerMKII.settings[0x40]) == 9)
                //{
                //    currentScriptLogic = GUILayout.TextField(new Rect(num + 50f, num2 + 82f, 600f, 270f), currentScriptLogic);
                //    if (GUILayout.Button(new Rect(num + 250f, num2 + 365f, 50f, 20f), "<b><color=#" + MainColor + ">Copy</color></b>"))
                //    {
                //        editor = new TextEditor
                //        {
                //            content = new GUIContent(currentScriptLogic)
                //        };
                //        editor.SelectAll();
                //        editor.Copy();
                //    }
                //    else if (GUILayout.Button(new Rect(num + 400f, num2 + 365f, 50f, 20f), "<b><color=#" + MainColor + ">Clear</color></b>"))
                //    {
                //        currentScriptLogic = string.Empty;
                //    }
                //}
                //else if (((int)FengGameManagerMKII.settings[0x40]) == 11)
                //{

                //}
                //else if (((int)FengGameManagerMKII.settings[0x40]) == 12)
                //{
                //    GUILayout.Label(new Rect(num + 150f, num2 + 80f, 185f, 22f), "<b><color=#" + MainColor + ">Main Color</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 72f, num2 + 135f, 20f, 22f), "<b><color=#" + MainColor + ">R:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 72f, num2 + 160f, 20f, 22f), "<b><color=#" + MainColor + ">G:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 72f, num2 + 185f, 20f, 22f), "<b><color=#" + MainColor + ">B:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 72f, num2 + 210f, 20f, 22f), "<b><color=#" + MainColor + ">A:</color></b>", "Label");
                //    BackGRColor1 = GUILayout.HorizontalSlider(new Rect(num + 92f, num2 + 138f, 100f, 20f), (float)BackGRColor1, 0f, 1f);
                //    BackGRColor2 = GUILayout.HorizontalSlider(new Rect(num + 92f, num2 + 163f, 100f, 20f), (float)BackGRColor2, 0f, 1f);
                //    BackGRColor3 = GUILayout.HorizontalSlider(new Rect(num + 92f, num2 + 188f, 100f, 20f), (float)BackGRColor3, 0f, 1f);
                //    BackGRColor4 = GUILayout.HorizontalSlider(new Rect(num + 92f, num2 + 213f, 100f, 20f), (float)BackGRColor4, 0.5f, 1f);
                //}
                //else if (((int)FengGameManagerMKII.settings[0x40]) == 13)
                //{
                //    float num43;
                //    num43 = 44f;
                //    GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Normal:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 1f), 227f, 20f), "<b><color=#" + MainColor + ">Abberants:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Jumpers:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 3f), 240f, 20f), "<b><color=#" + MainColor + ">Crawlers:</color></b>", "Label");
                //    GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Punks:</color></b>", "Label");
                //    TitName = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)TitName);
                //    TitAberName = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)TitAberName);
                //    TitJumpName = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)TitJumpName);
                //    TitCrawName = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)TitCrawName);
                //    TitPunkName = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)TitPunkName);
                //}
                //else
                //{
                //    float num43;
                //    if (((int)FengGameManagerMKII.settings[0x40]) == 2)
                //    {
                //        GUILayout.Label(new Rect(num + 205f, num2 + 52f, 120f, 30f), "<b><color=#" + MainColor + ">Human Skin Mode:</color></b>", "Label");
                //        flag = false;
                //        if (((int)FengGameManagerMKII.settings[0]) == 1)
                //        {
                //            flag = true;
                //        }
                //        flag3 = GUILayout.Toggle(new Rect(num + 325f, num2 + 52f, 40f, 20f), flag, "<b><color=#" + MainColor + ">On</color></b>");
                //        if (flag != flag3)
                //        {
                //            if (flag3)
                //            {
                //                FengGameManagerMKII.settings[0] = 1;
                //            }
                //            else
                //            {
                //                FengGameManagerMKII.settings[0] = 0;
                //            }
                //        }
                //        num43 = 44f;
                //        if (((int)FengGameManagerMKII.settings[0x85]) == 0)
                //        {
                //            if (GUILayout.Button(new Rect(num + 375f, num2 + 51f, 120f, 22f), "<b><color=#" + MainColor + ">Human Set 1</color></b>"))
                //            {
                //                FengGameManagerMKII.settings[0x85] = 1;
                //            }
                //            FengGameManagerMKII.settings[3] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[3]);
                //            FengGameManagerMKII.settings[4] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[4]);
                //            FengGameManagerMKII.settings[5] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[5]);
                //            FengGameManagerMKII.settings[6] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[6]);
                //            FengGameManagerMKII.settings[7] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[7]);
                //            FengGameManagerMKII.settings[8] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[8]);
                //            FengGameManagerMKII.settings[14] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[14]);
                //            FengGameManagerMKII.settings[9] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[9]);
                //            FengGameManagerMKII.settings[10] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[10]);
                //            FengGameManagerMKII.settings[11] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[11]);
                //            FengGameManagerMKII.settings[12] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[12]);
                //            FengGameManagerMKII.settings[13] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[13]);
                //            FengGameManagerMKII.settings[0x5e] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x5e]);
                //        }
                //        else if (((int)FengGameManagerMKII.settings[0x85]) == 1)
                //        {
                //            if (GUILayout.Button(new Rect(num + 375f, num2 + 51f, 120f, 22f), "<b><color=#" + MainColor + ">Human Set 2</color></b>"))
                //            {
                //                FengGameManagerMKII.settings[0x85] = 2;
                //            }
                //            FengGameManagerMKII.settings[0x86] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x86]);
                //            FengGameManagerMKII.settings[0x87] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x87]);
                //            FengGameManagerMKII.settings[0x88] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x88]);
                //            FengGameManagerMKII.settings[0x89] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x89]);
                //            FengGameManagerMKII.settings[0x8a] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8a]);
                //            FengGameManagerMKII.settings[0x8b] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8b]);
                //            FengGameManagerMKII.settings[0x91] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[0x91]);
                //            FengGameManagerMKII.settings[140] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[140]);
                //            FengGameManagerMKII.settings[0x8d] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8d]);
                //            FengGameManagerMKII.settings[0x8e] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8e]);
                //            FengGameManagerMKII.settings[0x8f] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x8f]);
                //            FengGameManagerMKII.settings[0x90] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x90]);
                //            FengGameManagerMKII.settings[0x92] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x92]);
                //        }
                //        else if (((int)FengGameManagerMKII.settings[0x85]) == 2)
                //        {
                //            if (GUILayout.Button(new Rect(num + 375f, num2 + 51f, 120f, 22f), "<b><color=#" + MainColor + ">Human Set 3</color></b>"))
                //            {
                //                FengGameManagerMKII.settings[0x85] = 0;
                //            }
                //            FengGameManagerMKII.settings[0x93] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x93]);
                //            FengGameManagerMKII.settings[0x94] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x94]);
                //            FengGameManagerMKII.settings[0x95] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x95]);
                //            FengGameManagerMKII.settings[150] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[150]);
                //            FengGameManagerMKII.settings[0x97] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x97]);
                //            FengGameManagerMKII.settings[0x98] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x98]);
                //            FengGameManagerMKII.settings[0x9e] = GUILayout.TextField(new Rect(num + 80f, (num2 + 114f) + (num43 * 6f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9e]);
                //            FengGameManagerMKII.settings[0x99] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 0f), 230f, 20f), (string)FengGameManagerMKII.settings[0x99]);
                //            FengGameManagerMKII.settings[0x9a] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9a]);
                //            FengGameManagerMKII.settings[0x9b] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 2f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9b]);
                //            FengGameManagerMKII.settings[0x9c] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9c]);
                //            FengGameManagerMKII.settings[0x9d] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 4f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9d]);
                //            FengGameManagerMKII.settings[0x9f] = GUILayout.TextField(new Rect(num + 390f, (num2 + 114f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x9f]);
                //        }
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Horse:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 1f), 227f, 20f), "<b><color=#" + MainColor + ">Hair (model dependent):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Eyes:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 3f), 240f, 20f), "<b><color=#" + MainColor + ">Glass (must have a glass enabled):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Face:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 5f), 150f, 20f), "<b><color=#" + MainColor + ">Skin:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 6f), 240f, 20f), "<b><color=#" + MainColor + ">Hoodie (costume dependent):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 0f), 240f, 20f), "<b><color=#" + MainColor + ">Costume (model dependent):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 1f), 150f, 20f), "<b><color=#" + MainColor + ">Logo & Cape:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 2f), 240f, 20f), "<b><color=#" + MainColor + ">3DMG Center & 3DMG/Blade/Gun(left):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 3f), 227f, 20f), "<b><color=#" + MainColor + ">3DMG/Blade/Gun(right):</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Gas:</color></b>", "Label");
                //        GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 5f), 150f, 20f), "<b><color=#" + MainColor + ">Weapon Trail:</color></b>", "Label");
                //    }
                //    else if (((int)FengGameManagerMKII.settings[0x40]) == 3)
                //    {
                //        int num44;
                //        int num45;
                //        GUILayout.Label(new Rect(num + 270f, num2 + 52f, 120f, 30f), "<b><color=#" + MainColor + ">Titan Skin Mode:</color></b>", "Label");
                //        flag4 = false;
                //        if (((int)FengGameManagerMKII.settings[1]) == 1)
                //        {
                //            flag4 = true;
                //        }
                //        bool flag10 = GUILayout.Toggle(new Rect(num + 390f, num2 + 52f, 40f, 20f), flag4, "<b><color=#" + MainColor + ">On</color></b>");
                //        if (flag4 != flag10)
                //        {
                //            if (flag10)
                //            {
                //                FengGameManagerMKII.settings[1] = 1;
                //            }
                //            else
                //            {
                //                FengGameManagerMKII.settings[1] = 0;
                //            }
                //        }
                //        GUILayout.Label(new Rect(num + 270f, num2 + 77f, 120f, 30f), "<b><color=#" + MainColor + ">Randomized Pairs:</color></b>", "Label");
                //        flag4 = false;
                //        if (((int)FengGameManagerMKII.settings[0x20]) == 1)
                //        {
                //            flag4 = true;
                //        }
                //        flag10 = GUILayout.Toggle(new Rect(num + 390f, num2 + 77f, 40f, 20f), flag4, "<b><color=#" + MainColor + ">On</color></b>");
                //        if (flag4 != flag10)
                //        {
                //            if (flag10)
                //            {
                //                FengGameManagerMKII.settings[0x20] = 1;
                //            }
                //            else
                //            {
                //                FengGameManagerMKII.settings[0x20] = 0;
                //            }
                //        }
                //        GUILayout.Label(new Rect(num + 158f, num2 + 112f, 150f, 20f), "<b><color=#" + MainColor + ">Titan Hair:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x15] = GUILayout.TextField(new Rect(num + 80f, num2 + 134f, 165f, 20f), (string)FengGameManagerMKII.settings[0x15]);
                //        FengGameManagerMKII.settings[0x16] = GUILayout.TextField(new Rect(num + 80f, num2 + 156f, 165f, 20f), (string)FengGameManagerMKII.settings[0x16]);
                //        FengGameManagerMKII.settings[0x17] = GUILayout.TextField(new Rect(num + 80f, num2 + 178f, 165f, 20f), (string)FengGameManagerMKII.settings[0x17]);
                //        FengGameManagerMKII.settings[0x18] = GUILayout.TextField(new Rect(num + 80f, num2 + 200f, 165f, 20f), (string)FengGameManagerMKII.settings[0x18]);
                //        FengGameManagerMKII.settings[0x19] = GUILayout.TextField(new Rect(num + 80f, num2 + 222f, 165f, 20f), (string)FengGameManagerMKII.settings[0x19]);
                //        if (GUILayout.Button(new Rect(num + 250f, num2 + 134f, 60f, 20f), this.hairtype((int)FengGameManagerMKII.settings[0x10])))
                //        {
                //            num44 = 0x10;
                //            num45 = (int)FengGameManagerMKII.settings[0x10];
                //            if (num45 >= 9)
                //            {
                //                num45 = -1;
                //            }
                //            else
                //            {
                //                num45++;
                //            }
                //            FengGameManagerMKII.settings[num44] = num45;
                //        }
                //        else if (GUILayout.Button(new Rect(num + 250f, num2 + 156f, 60f, 20f), this.hairtype((int)FengGameManagerMKII.settings[0x11])))
                //        {
                //            num44 = 0x11;
                //            num45 = (int)FengGameManagerMKII.settings[0x11];
                //            if (num45 >= 9)
                //            {
                //                num45 = -1;
                //            }
                //            else
                //            {
                //                num45++;
                //            }
                //            FengGameManagerMKII.settings[num44] = num45;
                //        }
                //        else if (GUILayout.Button(new Rect(num + 250f, num2 + 178f, 60f, 20f), this.hairtype((int)FengGameManagerMKII.settings[0x12])))
                //        {
                //            num44 = 0x12;
                //            num45 = (int)FengGameManagerMKII.settings[0x12];
                //            if (num45 >= 9)
                //            {
                //                num45 = -1;
                //            }
                //            else
                //            {
                //                num45++;
                //            }
                //            FengGameManagerMKII.settings[num44] = num45;
                //        }
                //        else if (GUILayout.Button(new Rect(num + 250f, num2 + 200f, 60f, 20f), this.hairtype((int)FengGameManagerMKII.settings[0x13])))
                //        {
                //            num44 = 0x13;
                //            num45 = (int)FengGameManagerMKII.settings[0x13];
                //            if (num45 >= 9)
                //            {
                //                num45 = -1;
                //            }
                //            else
                //            {
                //                num45++;
                //            }
                //            FengGameManagerMKII.settings[num44] = num45;
                //        }
                //        else if (GUILayout.Button(new Rect(num + 250f, num2 + 222f, 60f, 20f), this.hairtype((int)FengGameManagerMKII.settings[20])))
                //        {
                //            num44 = 20;
                //            num45 = (int)FengGameManagerMKII.settings[20];
                //            if (num45 >= 9)
                //            {
                //                num45 = -1;
                //            }
                //            else
                //            {
                //                num45++;
                //            }
                //            FengGameManagerMKII.settings[num44] = num45;
                //        }
                //        GUILayout.Label(new Rect(num + 158f, num2 + 252f, 150f, 20f), "<b><color=#" + MainColor + ">Titan Eye:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x1a] = GUILayout.TextField(new Rect(num + 80f, num2 + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[0x1a]);
                //        FengGameManagerMKII.settings[0x1b] = GUILayout.TextField(new Rect(num + 80f, num2 + 296f, 230f, 20f), (string)FengGameManagerMKII.settings[0x1b]);
                //        FengGameManagerMKII.settings[0x1c] = GUILayout.TextField(new Rect(num + 80f, num2 + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[0x1c]);
                //        FengGameManagerMKII.settings[0x1d] = GUILayout.TextField(new Rect(num + 80f, num2 + 340f, 230f, 20f), (string)FengGameManagerMKII.settings[0x1d]);
                //        FengGameManagerMKII.settings[30] = GUILayout.TextField(new Rect(num + 80f, num2 + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[30]);
                //        GUILayout.Label(new Rect(num + 455f, num2 + 112f, 150f, 20f), "<b><color=#" + MainColor + ">Titan Body:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x56] = GUILayout.TextField(new Rect(num + 390f, num2 + 134f, 230f, 20f), (string)FengGameManagerMKII.settings[0x56]);
                //        FengGameManagerMKII.settings[0x57] = GUILayout.TextField(new Rect(num + 390f, num2 + 156f, 230f, 20f), (string)FengGameManagerMKII.settings[0x57]);
                //        FengGameManagerMKII.settings[0x58] = GUILayout.TextField(new Rect(num + 390f, num2 + 178f, 230f, 20f), (string)FengGameManagerMKII.settings[0x58]);
                //        FengGameManagerMKII.settings[0x59] = GUILayout.TextField(new Rect(num + 390f, num2 + 200f, 230f, 20f), (string)FengGameManagerMKII.settings[0x59]);
                //        FengGameManagerMKII.settings[90] = GUILayout.TextField(new Rect(num + 390f, num2 + 222f, 230f, 20f), (string)FengGameManagerMKII.settings[90]);
                //        GUILayout.Label(new Rect(num + 472f, num2 + 252f, 150f, 20f), "<b><color=#" + MainColor + ">Eren:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x41] = GUILayout.TextField(new Rect(num + 390f, num2 + 274f, 230f, 20f), (string)FengGameManagerMKII.settings[0x41]);
                //        GUILayout.Label(new Rect(num + 470f, num2 + 296f, 150f, 20f), "<b><color=#" + MainColor + ">Annie:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x42] = GUILayout.TextField(new Rect(num + 390f, num2 + 318f, 230f, 20f), (string)FengGameManagerMKII.settings[0x42]);
                //        GUILayout.Label(new Rect(num + 465f, num2 + 340f, 150f, 20f), "<b><color=#" + MainColor + ">Colossal:</color></b>", "Label");
                //        FengGameManagerMKII.settings[0x43] = GUILayout.TextField(new Rect(num + 390f, num2 + 362f, 230f, 20f), (string)FengGameManagerMKII.settings[0x43]);
                //    }
                //    else if (((int)FengGameManagerMKII.settings[0x40]) == 7)
                //    {
                //        num43 = 22f;
                //        GUILayout.Label(new Rect(num + 205f, num2 + 52f, 145f, 30f), "<b><color=#" + MainColor + ">Level Skin Mode:</color></b>", "Label");
                //        bool flag11 = false;
                //        if (((int)FengGameManagerMKII.settings[2]) == 1)
                //        {
                //            flag11 = true;
                //        }
                //        bool flag12 = GUILayout.Toggle(new Rect(num + 325f, num2 + 52f, 40f, 20f), flag11, "<b><color=#" + MainColor + ">On</color></b>");
                //        if (flag11 != flag12)
                //        {
                //            if (flag12)
                //            {
                //                FengGameManagerMKII.settings[2] = 1;
                //            }
                //            else
                //            {
                //                FengGameManagerMKII.settings[2] = 0;
                //            }
                //        }
                //        if (((int)FengGameManagerMKII.settings[0xbc]) == 0)
                //        {
                //            if (GUILayout.Button(new Rect(num + 375f, num2 + 51f, 120f, 22f), "<b><color=#" + MainColor + ">Forest Skins</color></b>"))
                //            {
                //                FengGameManagerMKII.settings[0xbc] = 1;
                //            }
                //            GUILayout.Label(new Rect(num + 205f, num2 + 77f, 145f, 30f), "<b><color=#" + MainColor + ">Randomized Pairs:</color></b>", "Label");
                //            flag11 = false;
                //            if (((int)FengGameManagerMKII.settings[50]) == 1)
                //            {
                //                flag11 = true;
                //            }
                //            flag12 = GUILayout.Toggle(new Rect(num + 325f, num2 + 77f, 40f, 20f), flag11, "<b><color=#" + MainColor + ">On</color></b>");
                //            if (flag11 != flag12)
                //            {
                //                if (flag12)
                //                {
                //                    FengGameManagerMKII.settings[50] = 1;
                //                }
                //                else
                //                {
                //                    FengGameManagerMKII.settings[50] = 0;
                //                }
                //            }
                //            this.scroll = GUILayout.BeginScrollView(new Rect(num, num2 + 115f, 712f, 340f), this.scroll, new Rect(num, num2 + 115f, 700f, 475f), true, true);
                //            GUILayout.Label(new Rect(num + 79f, (num2 + 117f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Ground:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x31] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 1f), 227f, 20f), (string)FengGameManagerMKII.settings[0x31]);
                //            GUILayout.Label(new Rect(num + 79f, (num2 + 117f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Forest Trunks</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x21] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 3f), 227f, 20f), (string)FengGameManagerMKII.settings[0x21]);
                //            FengGameManagerMKII.settings[0x22] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 4f), 227f, 20f), (string)FengGameManagerMKII.settings[0x22]);
                //            FengGameManagerMKII.settings[0x23] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 5f), 227f, 20f), (string)FengGameManagerMKII.settings[0x23]);
                //            FengGameManagerMKII.settings[0x24] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 6f), 227f, 20f), (string)FengGameManagerMKII.settings[0x24]);
                //            FengGameManagerMKII.settings[0x25] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 7f), 227f, 20f), (string)FengGameManagerMKII.settings[0x25]);
                //            FengGameManagerMKII.settings[0x26] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 8f), 227f, 20f), (string)FengGameManagerMKII.settings[0x26]);
                //            FengGameManagerMKII.settings[0x27] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 9f), 227f, 20f), (string)FengGameManagerMKII.settings[0x27]);
                //            FengGameManagerMKII.settings[40] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 10f), 227f, 20f), (string)FengGameManagerMKII.settings[40]);
                //            GUILayout.Label(new Rect(num + 79f, (num2 + 117f) + (num43 * 11f), 150f, 20f), "<b><color=#" + MainColor + ">Forest Leaves:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x29] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 12f), 227f, 20f), (string)FengGameManagerMKII.settings[0x29]);
                //            FengGameManagerMKII.settings[0x2a] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 13f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2a]);
                //            FengGameManagerMKII.settings[0x2b] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 14f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2b]);
                //            FengGameManagerMKII.settings[0x2c] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 15f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2c]);
                //            FengGameManagerMKII.settings[0x2d] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 16f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2d]);
                //            FengGameManagerMKII.settings[0x2e] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 17f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2e]);
                //            FengGameManagerMKII.settings[0x2f] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 18f), 227f, 20f), (string)FengGameManagerMKII.settings[0x2f]);
                //            FengGameManagerMKII.settings[0x30] = GUILayout.TextField(new Rect(num + 79f, (num2 + 117f) + (num43 * 19f), 227f, 20f), (string)FengGameManagerMKII.settings[0x30]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Front:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa3] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 1f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa3]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Back:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa4] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 3f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa4]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Left:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa5] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 5f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa5]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 6f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Right:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa6] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 7f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa6]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 8f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Up:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa7] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 9f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa7]);
                //            GUILayout.Label(new Rect(num + 379f, (num2 + 117f) + (num43 * 10f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Down:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa8] = GUILayout.TextField(new Rect(num + 379f, (num2 + 117f) + (num43 * 11f), 227f, 20f), (string)FengGameManagerMKII.settings[0xa8]);
                //            GUILayout.EndScrollView();
                //        }
                //        else if (((int)FengGameManagerMKII.settings[0xbc]) == 1)
                //        {
                //            if (GUILayout.Button(new Rect(num + 375f, num2 + 51f, 120f, 22f), "<b><color=#" + MainColor + ">City Skins</color></b>"))
                //            {
                //                FengGameManagerMKII.settings[0xbc] = 0;
                //            }
                //            GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Ground:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x3b] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0x3b]);
                //            GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Wall:</color></b>", "Label");
                //            FengGameManagerMKII.settings[60] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[60]);
                //            GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Gate:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x3d] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0x3d]);
                //            GUILayout.Label(new Rect(num + 80f, (num2 + 92f) + (num43 * 6f), 150f, 20f), "<b><color=#" + MainColor + ">Houses:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0x33] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 7f), 230f, 20f), (string)FengGameManagerMKII.settings[0x33]);
                //            FengGameManagerMKII.settings[0x34] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 8f), 230f, 20f), (string)FengGameManagerMKII.settings[0x34]);
                //            FengGameManagerMKII.settings[0x35] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 9f), 230f, 20f), (string)FengGameManagerMKII.settings[0x35]);
                //            FengGameManagerMKII.settings[0x36] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 10f), 230f, 20f), (string)FengGameManagerMKII.settings[0x36]);
                //            FengGameManagerMKII.settings[0x37] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 11f), 230f, 20f), (string)FengGameManagerMKII.settings[0x37]);
                //            FengGameManagerMKII.settings[0x38] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 12f), 230f, 20f), (string)FengGameManagerMKII.settings[0x38]);
                //            FengGameManagerMKII.settings[0x39] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 13f), 230f, 20f), (string)FengGameManagerMKII.settings[0x39]);
                //            FengGameManagerMKII.settings[0x3a] = GUILayout.TextField(new Rect(num + 80f, (num2 + 92f) + (num43 * 14f), 230f, 20f), (string)FengGameManagerMKII.settings[0x3a]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 0f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Front:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xa9] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 1f), 230f, 20f), (string)FengGameManagerMKII.settings[0xa9]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 2f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Back:</color></b>", "Label");
                //            FengGameManagerMKII.settings[170] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 3f), 230f, 20f), (string)FengGameManagerMKII.settings[170]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 4f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Left:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xab] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 5f), 230f, 20f), (string)FengGameManagerMKII.settings[0xab]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 6f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Right:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xac] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 7f), 230f, 20f), (string)FengGameManagerMKII.settings[0xac]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 8f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Up:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xad] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 9f), 230f, 20f), (string)FengGameManagerMKII.settings[0xad]);
                //            GUILayout.Label(new Rect(num + 390f, (num2 + 92f) + (num43 * 10f), 150f, 20f), "<b><color=#" + MainColor + ">Skybox Down:</color></b>", "Label");
                //            FengGameManagerMKII.settings[0xae] = GUILayout.TextField(new Rect(num + 390f, (num2 + 92f) + (num43 * 11f), 230f, 20f), (string)FengGameManagerMKII.settings[0xae]);
                //        }
                //    }
                //    else if (((int)FengGameManagerMKII.settings[0x40]) == 4)
                //    {
                //        GUILayout.TextArea(new Rect(num + 80f, num2 + 52f, 270f, 30f), "<b><color=#" + MainColor + ">FengGameManagerMKII.settings saved to playerprefs!</color></b>", 100, "Label");
                //    }
                //    else if (((int)FengGameManagerMKII.settings[0x40]) == 5)
                //    {
                //        GUILayout.TextArea(new Rect(num + 80f, num2 + 52f, 270f, 30f), "<b><color=#" + MainColor + ">FengGameManagerMKII.settings reloaded from playerprefs!</color></b>", 100, "Label");
                //    }
                //    else
                //    {
                //        string[] strArray15;
                //        if (((int)FengGameManagerMKII.settings[0x40]) == 0)
                //        
                //        else if (((int)FengGameManagerMKII.settings[0x40]) == 10)
                //        {
                            
                //        }
                //        
                //        else if (((int)FengGameManagerMKII.settings[0x40]) == 8)
                //        {
                //            GUILayout.Label(new Rect(num + 150f, num2 + 51f, 120f, 22f), "Map FengGameManagerMKII.settings", "Label");
                //            GUILayout.Label(new Rect(num + 50f, num2 + 81f, 140f, 20f), "Titan Spawn Cap:", "Label");
                //            FengGameManagerMKII.settings[0x55] = GUILayout.TextField(new Rect(num + 155f, num2 + 81f, 30f, 20f), (string)FengGameManagerMKII.settings[0x55]);
                //            strArray15 = new string[] { "1 Round", "Waves", "PVP", "Racing", "Custom" };
                //            GameSettings.gameType = GUILayout.SelectionGrid(new Rect(num + 190f, num2 + 80f, 140f, 60f), GameFengGameManagerMKII.settings.gameType, strArray15, 2, GUILayout.skin.toggle);
                //            GUILayout.Label(new Rect(num + 150f, num2 + 155f, 150f, 20f), "Level Script:", "Label");
                //            currentScript = GUILayout.TextField(new Rect(num + 50f, num2 + 180f, 275f, 220f), currentScript);
                //            if (GUILayout.Button(new Rect(num + 100f, num2 + 410f, 50f, 25f), "Copy"))
                //            {
                //                editor = new TextEditor
                //                {
                //                    content = new GUIContent(currentScript)
                //                };
                //                editor.SelectAll();
                //                editor.Copy();
                //            }
                //            else if (GUILayout.Button(new Rect(num + 225f, num2 + 410f, 50f, 25f), "Clear"))
                //            {
                //                currentScript = string.Empty;
                //            }
                //            GUILayout.Label(new Rect(num + 455f, num2 + 51f, 180f, 20f), "Custom Textures", "Label");
                //            GUILayout.Label(new Rect(num + 375f, num2 + 81f, 180f, 20f), "Ground Skin:", "Label");
                //            FengGameManagerMKII.settings[0xa2] = GUILayout.TextField(new Rect(num + 375f, num2 + 103f, 275f, 20f), (string)FengGameManagerMKII.settings[0xa2]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 125f, 150f, 20f), "Skybox Front:", "Label");
                //            FengGameManagerMKII.settings[0xaf] = GUILayout.TextField(new Rect(num + 375f, num2 + 147f, 275f, 20f), (string)FengGameManagerMKII.settings[0xaf]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 169f, 150f, 20f), "Skybox Back:", "Label");
                //            FengGameManagerMKII.settings[0xb0] = GUILayout.TextField(new Rect(num + 375f, num2 + 191f, 275f, 20f), (string)FengGameManagerMKII.settings[0xb0]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 213f, 150f, 20f), "Skybox Left:", "Label");
                //            FengGameManagerMKII.settings[0xb1] = GUILayout.TextField(new Rect(num + 375f, num2 + 235f, 275f, 20f), (string)FengGameManagerMKII.settings[0xb1]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 257f, 150f, 20f), "Skybox Right:", "Label");
                //            FengGameManagerMKII.settings[0xb2] = GUILayout.TextField(new Rect(num + 375f, num2 + 279f, 275f, 20f), (string)FengGameManagerMKII.settings[0xb2]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 301f, 150f, 20f), "Skybox Up:", "Label");
                //            FengGameManagerMKII.settings[0xb3] = GUILayout.TextField(new Rect(num + 375f, num2 + 323f, 275f, 20f), (string)FengGameManagerMKII.settings[0xb3]);
                //            GUILayout.Label(new Rect(num + 375f, num2 + 345f, 150f, 20f), "Skybox Down:", "Label");
                //            FengGameManagerMKII.settings[180] = GUILayout.TextField(new Rect(num + 375f, num2 + 367f, 275f, 20f), (string)FengGameManagerMKII.settings[180]);
                //        }
                //    }
                //}
                //if (GUILayout.Button(new Rect(num + 408f, num2 + 465f, 42f, 25f), "<b><color=#" + MainColor + ">Save</color></b>"))
                //                    else if (GUILayout.Button(new Rect(num + 455f, num2 + 465f, 40f, 25f), "<b><color=#" + MainColor + ">Load</color></b>"))
                //{
                //    this.loadconfig();
                //    FengGameManagerMKII.settings[0x40] = 5;
                //}
                //else if (GUILayout.Button(new Rect(num + 500f, num2 + 465f, 60f, 25f), "<b><color=#" + MainColor + ">Default</color></b>"))
                //{
                //    GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().setToDefault();
                //}
                //else if (GUILayout.Button(new Rect(num + 565f, num2 + 465f, 75f, 25f), "<b><color=#" + MainColor + ">Continue</color></b>"))
                //{

                //}
                //else if (GUILayout.Button(new Rect(num + 645f, num2 + 465f, 40f, 25f), "<b><color=#FF0000>Quit</color></b>"))
                //{
                //    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                //    {
                //        Time.timeScale = 1f;
                //    }
                //    else
                //    {
                //        PhotonNetwork.Disconnect();
                //    }
                //    Screen.lockCursor = false;
                //    Screen.showCursor = true;
                //    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                //    this.gameStart = false;
                //    GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
                //    this.DestroyAllExistingCloths();
                //    UnityEngine.Object.Destroy(GameObject.Find("MultiplayerManager"));
                //    Application.LoadLevel("menu");
                //}
            //}
        }
    }
                    //}
                





        void GeneralGUI()
        {
            GUILayout.BeginArea(new Rect(5f, 100f, 237f, 480f));
            Scroll = GUILayout.BeginScrollView(Scroll, GUILayout.Width(237f), GUILayout.Height(460f));
            GUILayout.BeginVertical();
            if (GUILayout.Button("Graphics")) ChooseButt1 = ChooseButt1 != 1 ? 1 : 0;
            if (ChooseButt1 == 1)
            {
                int num47;
                bool flag10;
                bool flag14 = false;
                bool flag15 = false;
                bool flag16 = false;
                bool flag17 = false;
                bool flag18 = false;
                if (((int)FengGameManagerMKII.settings[15]) == 1) flag14 = true;
                if (((int)FengGameManagerMKII.settings[0x5c]) == 1) flag15 = true;
                if (((int)FengGameManagerMKII.settings[0x5d]) == 1) flag16 = true;
                if (((int)FengGameManagerMKII.settings[0x3f]) == 1) flag17 = true;
                if (((int)FengGameManagerMKII.settings[0xb7]) == 1) flag18 = true;
                GUILayout.BeginHorizontal();
                GUILayout.Label("Disable custom gas textures:");
                bool flag19 = GUILayout.Toggle(flag14, "On");
                if (flag19 != flag14)
                {
                    if (flag19)
                    {
                        FengGameManagerMKII.settings[15] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[15] = 0;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Disable weapon trail:");
                flag10 = GUILayout.Toggle(flag15, "On");
                if (flag10 != flag15)
                {
                    if (flag10)
                    {
                        FengGameManagerMKII.settings[0x5c] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[0x5c] = 0;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Disable wind effect:");
                bool flag20 = GUILayout.Toggle(flag16, "On");
                if (flag20 != flag16)
                {
                    if (flag20)
                    {
                        FengGameManagerMKII.settings[0x5d] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[0x5d] = 0;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Enable vSync:");
                bool flag21 = GUILayout.Toggle(flag18, "On");
                if (flag21 != flag18)
                {
                    if (flag21)
                    {
                        FengGameManagerMKII.settings[0xb7] = 1;
                        QualitySettings.vSyncCount = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[0xb7] = 0;
                        QualitySettings.vSyncCount = 0;
                    }
                    Minimap.WaitAndTryRecaptureInstance(0.5f);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("FPS Cap (0 for disabled):");
                FengGameManagerMKII.settings[0xb8] = GUILayout.TextField((string)FengGameManagerMKII.settings[0xb8]);
                Application.targetFrameRate = -1;
                if (int.TryParse((string)FengGameManagerMKII.settings[0xb8], out num47) && (num47 > 0))
                {
                    Application.targetFrameRate = num47;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Texture Quality:");
                if (GUILayout.Button(FengGameManagerMKII.instance.mastertexturetype(QualitySettings.masterTextureLimit)))
                {
                    if (QualitySettings.masterTextureLimit <= 0)
                    {
                        QualitySettings.masterTextureLimit = 2;
                    }
                    else
                    {
                        QualitySettings.masterTextureLimit--;
                    }
                    FengGameManagerMKII.linkHash[0].Clear();
                    FengGameManagerMKII.linkHash[1].Clear();
                    FengGameManagerMKII.linkHash[2].Clear();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Overall Quality:");
                FengGameManagerMKII.instance.qualitySlider = GUILayout.HorizontalSlider(FengGameManagerMKII.instance.qualitySlider, 0f, 1f);
                PlayerPrefs.SetFloat("GameQuality", FengGameManagerMKII.instance.qualitySlider);
                if (FengGameManagerMKII.instance.qualitySlider < 0.167f)
                {
                    QualitySettings.SetQualityLevel(0, true);
                }
                else if (FengGameManagerMKII.instance.qualitySlider < 0.33f)
                {
                    QualitySettings.SetQualityLevel(1, true);
                }
                else if (FengGameManagerMKII.instance.qualitySlider < 0.5f)
                {
                    QualitySettings.SetQualityLevel(2, true);
                }
                else if (FengGameManagerMKII.instance.qualitySlider < 0.67f)
                {
                    QualitySettings.SetQualityLevel(3, true);
                }
                else if (FengGameManagerMKII.instance.qualitySlider < 0.83f)
                {
                    QualitySettings.SetQualityLevel(4, true);
                }
                else if (FengGameManagerMKII.instance.qualitySlider <= 1f)
                {
                    QualitySettings.SetQualityLevel(5, true);
                }
                if (!((FengGameManagerMKII.instance.qualitySlider < 0.9f) || FengGameManagerMKII.level.StartsWith("Custom")))
                {
                    Camera.main.GetComponent<TiltShift>().enabled = true;
                }
                else
                {
                    Camera.main.GetComponent<TiltShift>().enabled = false;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Disable Mipmapping:");
                GUILayout.Label("*Disabling mipmapping will increase custom texture quality at the cost of performance.");
                bool flag22 = GUILayout.Toggle(flag17, "On");
                if (flag22 != flag17)
                {
                    if (flag22)
                    {
                        FengGameManagerMKII.settings[0x3f] = 1;
                    }
                    else
                    {
                        FengGameManagerMKII.settings[0x3f] = 0;
                    }
                    FengGameManagerMKII.linkHash[0].Clear();
                    FengGameManagerMKII.linkHash[1].Clear();
                    FengGameManagerMKII.linkHash[2].Clear();
                }
                GUILayout.EndHorizontal();










                if (GUILayout.Button("Snapshots")) ChooseButt2 = ChooseButt2 != 2 ? 2 : 0;
                if (ChooseButt2 == 2)
                {
                    //GUI.Label(new Rect(num7 + 470f, num8 + 51f, 185f, 22f), "Snapshots", "Label");
                    //GUI.Label(new Rect(num7 + 386f, num8 + 81f, 185f, 22f), "Enable Snapshots:", "Label");
                    //GUI.Label(new Rect(num7 + 386f, num8 + 106f, 185f, 22f), "Show In Game:", "Label");
                    //GUI.Label(new Rect(num7 + 386f, num8 + 131f, 227f, 22f), "Snapshot Minimum Damage:", "Label");
                    //settings[0x5f] = GUI.TextField(new Rect(num7 + 563f, num8 + 131f, 65f, 20f), (string)settings[0x5f]);
                    //bool flag23 = false;
                    //bool flag24 = false;
                    //if (PlayerPrefs.GetInt("EnableSS", 0) == 1)
                    //{
                    //    flag23 = true;
                    //}
                    //if (PlayerPrefs.GetInt("showSSInGame", 0) == 1)
                    //{
                    //    flag24 = true;
                    //}
                    //bool flag25 = GUI.Toggle(new Rect(num7 + 588f, num8 + 81f, 40f, 20f), flag23, "On");
                    //if (flag25 != flag23)
                    //{
                    //    if (flag25)
                    //    {
                    //        PlayerPrefs.SetInt("EnableSS", 1);
                    //    }
                    //    else
                    //    {
                    //        PlayerPrefs.SetInt("EnableSS", 0);
                    //    }
                    //}
                    //bool flag26 = GUI.Toggle(new Rect(num7 + 588f, num8 + 106f, 40f, 20f), flag24, "On");
                    //if (flag24 != flag26)
                    //{
                    //    if (flag26)
                    //    {
                    //        PlayerPrefs.SetInt("showSSInGame", 1);
                    //    }
                    //    else
                    //    {
                    //        PlayerPrefs.SetInt("showSSInGame", 0);
                    //    }
                    //}
                }
            }
            if (GUILayout.Button("Other")) ChooseButt3 = ChooseButt3 != 3 ? 3 : 0;
            if (ChooseButt3 == 3)
            {
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        void RebindGUI()
        {
            
            //Event current;
            //List<string> list6;
            //float num47;
            //if (GUILayout.Button(new Rect(num + 233f, num2 + 51f, 55f, 25f), "<b><color=#" + MainColor + ">Human</color></b>"))
            //{
            //    FengGameManagerMKII.settings[190] = 0;
            //}
            //else if (GUILayout.Button(new Rect(num + 293f, num2 + 51f, 52f, 25f), "<b><color=#" + MainColor + ">Titan</color></b>"))
            //{
            //    FengGameManagerMKII.settings[190] = 1;
            //}
            //else if (GUILayout.Button(new Rect(num + 350f, num2 + 51f, 53f, 25f), "<b><color=#" + MainColor + ">Horse</color></b>"))
            //{
            //    FengGameManagerMKII.settings[190] = 2;
            //}
            //else if (GUILayout.Button(new Rect(num + 408f, num2 + 51f, 59f, 25f), "<b><color=#" + MainColor + ">Cannon</color></b>"))
            //{
            //    FengGameManagerMKII.settings[190] = 3;
            //}
            //if (((int)FengGameManagerMKII.settings[190]) == 0)
            //{
            //    list6 = new List<string> {
            //                                        "<b><color=#" + MainColor + ">Forward:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Backward:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Left:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Right:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Jump:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Dodge:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Left Hook:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Right Hook:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Both Hooks:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Lock:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Attack:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Special:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Salute:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Change Camera:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Reset:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Pause:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Show/Hide Cursor:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Fullscreen:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Change Blade:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Flare Green:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Flare Red:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Flare Black:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Reel in:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Reel out:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Gas Burst:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Minimap Max:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Minimap Toggle:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Minimap Reset:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Open Chat:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Live Spectate</color></b>"
            //                                    };
            //    for (num11 = 0; num11 < list6.Count; num11++)
            //    {
            //        num12 = num11;
            //        num47 = 80f;
            //        if (num12 > 14)
            //        {
            //            num47 = 390f;
            //            num12 -= 15;
            //        }
            //        GUILayout.Label(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 145f, 22f), list6[num11], "Label");
            //    }
            //    bool flag36 = false;
            //    if (((int)FengGameManagerMKII.settings[0x61]) == 1)
            //    {
            //        flag36 = true;
            //    }
            //    bool flag37 = false;
            //    if (((int)FengGameManagerMKII.settings[0x74]) == 1)
            //    {
            //        flag37 = true;
            //    }
            //    bool flag38 = false;
            //    if (((int)FengGameManagerMKII.settings[0xb5]) == 1)
            //    {
            //        flag38 = true;
            //    }
            //    bool flag39 = GUILayout.Toggle(new Rect(num + 457f, num2 + 261f, 40f, 20f), flag36, "<b><color=#" + MainColor + ">On</color></b>");
            //    if (flag36 != flag39)
            //    {
            //        if (flag39)
            //        {
            //            FengGameManagerMKII.settings[0x61] = 1;
            //        }
            //        else
            //        {
            //            FengGameManagerMKII.settings[0x61] = 0;
            //        }
            //    }
            //    bool flag40 = GUILayout.Toggle(new Rect(num + 457f, num2 + 286f, 40f, 20f), flag37, "<b><color=#" + MainColor + ">On</color></b>");
            //    if (flag37 != flag40)
            //    {
            //        if (flag40)
            //        {
            //            FengGameManagerMKII.settings[0x74] = 1;
            //        }
            //        else
            //        {
            //            FengGameManagerMKII.settings[0x74] = 0;
            //        }
            //    }
            //    bool flag41 = GUILayout.Toggle(new Rect(num + 457f, num2 + 311f, 40f, 20f), flag38, "<b><color=#" + MainColor + ">On</color></b>");
            //    if (flag38 != flag41)
            //    {
            //        if (flag41)
            //        {
            //            FengGameManagerMKII.settings[0xb5] = 1;
            //        }
            //        else
            //        {
            //            FengGameManagerMKII.settings[0xb5] = 0;
            //        }
            //    }
            //    for (num11 = 0; num11 < 0x16; num11++)
            //    {
            //        num12 = num11;
            //        num47 = 190f;
            //        if (num12 > 14)
            //        {
            //            num47 = 500f;
            //            num12 -= 15;
            //        }
            //        if (GUILayout.Button(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 120f, 20f), this.inputManager.getKeyRC(num11), "box"))
            //        {
            //            FengGameManagerMKII.settings[100] = num11 + 1;
            //            this.inputManager.setNameRC(num11, "waiting...");
            //        }
            //    }
            //    if (GUILayout.Button(new Rect(num + 500f, num2 + 261f, 120f, 20f), (string)FengGameManagerMKII.settings[0x62], "box"))
            //    {
            //        FengGameManagerMKII.settings[0x62] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0x62;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 286f, 120f, 20f), (string)FengGameManagerMKII.settings[0x63], "box"))
            //    {
            //        FengGameManagerMKII.settings[0x63] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0x63;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 311f, 120f, 20f), (string)FengGameManagerMKII.settings[0xb6], "box"))
            //    {
            //        FengGameManagerMKII.settings[0xb6] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0xb6;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 336f, 120f, 20f), (string)FengGameManagerMKII.settings[0xe8], "box"))
            //    {
            //        FengGameManagerMKII.settings[0xe8] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0xe8;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 361f, 120f, 20f), (string)FengGameManagerMKII.settings[0xe9], "box"))
            //    {
            //        FengGameManagerMKII.settings[0xe9] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0xe9;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 386f, 120f, 20f), (string)FengGameManagerMKII.settings[0xea], "box"))
            //    {
            //        FengGameManagerMKII.settings[0xea] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0xea;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 411f, 120f, 20f), (string)FengGameManagerMKII.settings[0xec], "box"))
            //    {
            //        FengGameManagerMKII.settings[0xec] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0xec;
            //    }
            //    else if (GUILayout.Button(new Rect(num + 500f, num2 + 436f, 120f, 20f), (string)FengGameManagerMKII.settings[0x106], "box"))
            //    {
            //        FengGameManagerMKII.settings[0x106] = "waiting...";
            //        FengGameManagerMKII.settings[100] = 0x106;
            //    }
            //    if (((int)FengGameManagerMKII.settings[100]) != 0)
            //    {
            //        current = Event.current;
            //        flag2 = false;
            //        str2 = "waiting...";
            //        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
            //        {
            //            flag2 = true;
            //            str2 = current.keyCode.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.LeftShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.LeftShift.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.RightShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.RightShift.ToString();
            //        }
            //        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            //        {
            //            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Up";
            //            }
            //            else
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Down";
            //            }
            //        }
            //        else
            //        {
            //            num11 = 0;
            //            while (num11 < 7)
            //            {
            //                if (Input.GetKeyDown((KeyCode)(0x143 + num11)))
            //                {
            //                    flag2 = true;
            //                    str2 = "Mouse" + Convert.ToString(num11);
            //                }
            //                num11++;
            //            }
            //        }
            //        if (flag2)
            //        {
            //            if (((int)FengGameManagerMKII.settings[100]) == 0x62)
            //            {
            //                FengGameManagerMKII.settings[0x62] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.reelin, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0x63)
            //            {
            //                FengGameManagerMKII.settings[0x63] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.reelout, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0xb6)
            //            {
            //                FengGameManagerMKII.settings[0xb6] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.dash, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0xe8)
            //            {
            //                FengGameManagerMKII.settings[0xe8] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.mapMaximize, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0xe9)
            //            {
            //                FengGameManagerMKII.settings[0xe9] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.mapToggle, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0xea)
            //            {
            //                FengGameManagerMKII.settings[0xea] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.mapReset, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0xec)
            //            {
            //                FengGameManagerMKII.settings[0xec] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.chat, str2);
            //            }
            //            else if (((int)FengGameManagerMKII.settings[100]) == 0x106)
            //            {
            //                FengGameManagerMKII.settings[0x106] = str2;
            //                FengGameManagerMKII.settings[100] = 0;
            //                inputRC.setInputHuman(InputCodeRC.liveCam, str2);
            //            }
            //            else
            //            {
            //                for (num11 = 0; num11 < 0x16; num11++)
            //                {
            //                    num13 = num11 + 1;
            //                    if (((int)FengGameManagerMKII.settings[100]) == num13)
            //                    {
            //                        this.inputManager.setKeyRC(num11, str2);
            //                        FengGameManagerMKII.settings[100] = 0;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (((int)FengGameManagerMKII.settings[190]) == 1)
            //{
            //    list6 = new List<string> {
            //                                        "<b><color=#" + MainColor + ">Forward:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Back:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Left:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Right:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Walk:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Jump:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Punch:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Slam:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Grab (front):</color></b>",
            //                                        "<b><color=#" + MainColor + ">Grab (back):</color></b>",
            //                                        "<b><color=#" + MainColor + ">Grab (nape):</color></b>",
            //                                        "<b><color=#" + MainColor + ">Slap:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Bite:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Cover Nape:</color></b>"
            //                                    };
            //    for (num11 = 0; num11 < list6.Count; num11++)
            //    {
            //        num12 = num11;
            //        num47 = 80f;
            //        if (num12 > 6)
            //        {
            //            num47 = 390f;
            //            num12 -= 7;
            //        }
            //        GUILayout.Label(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 145f, 22f), list6[num11], "Label");
            //    }
            //    for (num11 = 0; num11 < 14; num11++)
            //    {
            //        num13 = 0x65 + num11;
            //        num12 = num11;
            //        num47 = 190f;
            //        if (num12 > 6)
            //        {
            //            num47 = 500f;
            //            num12 -= 7;
            //        }
            //        if (GUILayout.Button(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num13], "box"))
            //        {
            //            FengGameManagerMKII.settings[num13] = "waiting...";
            //            FengGameManagerMKII.settings[100] = num13;
            //        }
            //    }
            //    if (((int)FengGameManagerMKII.settings[100]) != 0)
            //    {
            //        current = Event.current;
            //        flag2 = false;
            //        str2 = "waiting...";
            //        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
            //        {
            //            flag2 = true;
            //            str2 = current.keyCode.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.LeftShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.LeftShift.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.RightShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.RightShift.ToString();
            //        }
            //        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            //        {
            //            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Up";
            //            }
            //            else
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Down";
            //            }
            //        }
            //        else
            //        {
            //            num11 = 0;
            //            while (num11 < 7)
            //            {
            //                if (Input.GetKeyDown((KeyCode)(0x143 + num11)))
            //                {
            //                    flag2 = true;
            //                    str2 = "Mouse" + Convert.ToString(num11);
            //                }
            //                num11++;
            //            }
            //        }
            //        if (flag2)
            //        {
            //            for (num11 = 0; num11 < 14; num11++)
            //            {
            //                num13 = 0x65 + num11;
            //                if (((int)FengGameManagerMKII.settings[100]) == num13)
            //                {
            //                    FengGameManagerMKII.settings[num13] = str2;
            //                    FengGameManagerMKII.settings[100] = 0;
            //                    inputRC.setInputTitan(num11, str2);
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (((int)FengGameManagerMKII.settings[190]) == 2)
            //{
            //    list6 = new List<string> {
            //                                        "<b><color=#" + MainColor + ">Forward:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Back:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Left:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Right:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Walk:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Jump:</color></b>",
            //                                        "<b><color=#" + MainColor + ">Mount:</color></b>"
            //                                    };
            //    for (num11 = 0; num11 < list6.Count; num11++)
            //    {
            //        num12 = num11;
            //        num47 = 80f;
            //        if (num12 > 3)
            //        {
            //            num47 = 390f;
            //            num12 -= 4;
            //        }
            //        GUILayout.Label(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 145f, 22f), list6[num11], "Label");
            //    }
            //    for (num11 = 0; num11 < 7; num11++)
            //    {
            //        num13 = 0xed + num11;
            //        num12 = num11;
            //        num47 = 190f;
            //        if (num12 > 3)
            //        {
            //            num47 = 500f;
            //            num12 -= 4;
            //        }
            //        if (GUILayout.Button(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num13], "box"))
            //        {
            //            FengGameManagerMKII.settings[num13] = "waiting...";
            //            FengGameManagerMKII.settings[100] = num13;
            //        }
            //    }
            //    if (((int)FengGameManagerMKII.settings[100]) != 0)
            //    {
            //        current = Event.current;
            //        flag2 = false;
            //        str2 = "waiting...";
            //        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
            //        {
            //            flag2 = true;
            //            str2 = current.keyCode.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.LeftShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.LeftShift.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.RightShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.RightShift.ToString();
            //        }
            //        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            //        {
            //            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Up";
            //            }
            //            else
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Down";
            //            }
            //        }
            //        else
            //        {
            //            num11 = 0;
            //            while (num11 < 7)
            //            {
            //                if (Input.GetKeyDown((KeyCode)(0x143 + num11)))
            //                {
            //                    flag2 = true;
            //                    str2 = "Mouse" + Convert.ToString(num11);
            //                }
            //                num11++;
            //            }
            //        }
            //        if (flag2)
            //        {
            //            for (num11 = 0; num11 < 7; num11++)
            //            {
            //                num13 = 0xed + num11;
            //                if (((int)FengGameManagerMKII.settings[100]) == num13)
            //                {
            //                    FengGameManagerMKII.settings[num13] = str2;
            //                    FengGameManagerMKII.settings[100] = 0;
            //                    inputRC.setInputHorse(num11, str2);
            //                }
            //            }
            //        }
            //    }
            //}
            //else if (((int)FengGameManagerMKII.settings[190]) == 3)
            //{
            //    list6 = new List<string> {
            //                                        "Rotate Up:",
            //                                        "Rotate Down:",
            //                                        "Rotate Left:",
            //                                        "Rotate Right:",
            //                                        "Fire:",
            //                                        "Mount:",
            //                                        "Slow Rotate:"
            //                                    };
            //    for (num11 = 0; num11 < list6.Count; num11++)
            //    {
            //        num12 = num11;
            //        num47 = 80f;
            //        if (num12 > 3)
            //        {
            //            num47 = 390f;
            //            num12 -= 4;
            //        }
            //        GUILayout.Label(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 145f, 22f), list6[num11], "Label");
            //    }
            //    for (num11 = 0; num11 < 7; num11++)
            //    {
            //        num13 = 0xfe + num11;
            //        num12 = num11;
            //        num47 = 190f;
            //        if (num12 > 3)
            //        {
            //            num47 = 500f;
            //            num12 -= 4;
            //        }
            //        if (GUILayout.Button(new Rect(num + num47, (num2 + 86f) + (num12 * 25f), 120f, 20f), (string)FengGameManagerMKII.settings[num13], "box"))
            //        {
            //            FengGameManagerMKII.settings[num13] = "waiting...";
            //            FengGameManagerMKII.settings[100] = num13;
            //        }
            //    }
            //    if (((int)FengGameManagerMKII.settings[100]) != 0)
            //    {
            //        current = Event.current;
            //        flag2 = false;
            //        str2 = "waiting...";
            //        if ((current.type == EventType.KeyDown) && (current.keyCode != KeyCode.None))
            //        {
            //            flag2 = true;
            //            str2 = current.keyCode.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.LeftShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.LeftShift.ToString();
            //        }
            //        else if (Input.GetKey(KeyCode.RightShift))
            //        {
            //            flag2 = true;
            //            str2 = KeyCode.RightShift.ToString();
            //        }
            //        else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            //        {
            //            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Up";
            //            }
            //            else
            //            {
            //                flag2 = true;
            //                str2 = "Scroll Down";
            //            }
            //        }
            //        else
            //        {
            //            num11 = 0;
            //            while (num11 < 6)
            //            {
            //                if (Input.GetKeyDown((KeyCode)(0x143 + num11)))
            //                {
            //                    flag2 = true;
            //                    str2 = "Mouse" + Convert.ToString(num11);
            //                }
            //                num11++;
            //            }
            //        }
            //        if (flag2)
            //        {
            //            for (num11 = 0; num11 < 7; num11++)
            //            {
            //                num13 = 0xfe + num11;
            //                if (((int)FengGameManagerMKII.settings[100]) == num13)
            //                {
            //                    FengGameManagerMKII.settings[num13] = str2;
            //                    FengGameManagerMKII.settings[100] = 0;
            //                    inputRC.setInputCannon(num11, str2);
            //                }
            //            }
            //        }
            //    }
            //}
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                TestGUIWindowOn = !TestGUIWindowOn;
            }
            //if ((int)FengGameManagerMKII.settings[0xcb] == 1)
            //{
            //    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Kill <i><color=#7b001c>" + FengGameManagerMKII.settings[0xcc] + "</color></i> <b><color=#d3eef2>TITAN</color></b> to win.</color>");
            //}
            //else
            //{
            //    FengGameManagerMKII.instance.sendChatContentInfo("<color=#b5ceff>Kill <i><color=#7b001c> 10</color></i><b><color=#d3eef2>TITAN</color></b> to win.</color>");
            //}
        }







        }
    }
