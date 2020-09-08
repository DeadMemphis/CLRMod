using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CLEARSKIES
{
    // Token: 0x02000087 RID: 135
    internal class F2GUI : MonoBehaviour
    {
        // Token: 0x170000A1 RID: 161
        // (get) Token: 0x06000454 RID: 1108 RVA: 0x00004E5B File Offset: 0x0000305B
        private int select;
        private int cmdmenuselect;
        private bool disable = true;
        private static float divider
        {
            get
            {
                if (Screen.width < 1024)
                {
                    return 3f;
                }
                return 1f;
            }
        }
        // Token: 0x170000A2 RID: 162
        // (get) Token: 0x06000455 RID: 1109 RVA: 0x00002AFD File Offset: 0x00000CFD
        private static float adder
        {
            get
            {
                if (Screen.width < 1024)
                {
                    return 10f;
                }
                return -25f;
            }
        }

        // Token: 0x170000A3 RID: 163
        // (get) Token: 0x06000456 RID: 1110 RVA: 0x00004E74 File Offset: 0x00003074
        private static Matrix4x4 resize
        {
            get
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3((float)Screen.width / (2400f / F2GUI.divider), (float)Screen.height / (1800f / F2GUI.divider), 1f));
            }
        }

        // Token: 0x170000A4 RID: 164
        // (get) Token: 0x06000457 RID: 1111 RVA: 0x00002B55 File Offset: 0x00000D55
        private static Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, 1f, 1f));
            }
        }

        // Token: 0x06000458 RID: 1112 RVA: 0x000342E0 File Offset: 0x000324E0
        private static GUIStyle formatStyle(GUIStyle other)
        {
            GUIStyle gUIStyle = new GUIStyle(other);
            gUIStyle.wordWrap = true;
            gUIStyle.alignment = TextAnchor.MiddleCenter;
            gUIStyle.fontSize = ((F2GUI.divider != 1f) ? 14 : 15);
            return gUIStyle;
        }

        // Token: 0x06000459 RID: 1113 RVA: 0x0003431C File Offset: 0x0003251C
        static F2GUI()
        {
            Array.Sort<string>(F2GUI.commandsReg, (string x, string y) => string.Compare(x, y));
            F2GUI.commandsMode = new string[]
            {
                "/add later )))0"
                //"/nocrawler",
                //"/heat",
                //"/ice",
                //"/zerog",
                //"/endless",
                //"/size",
                //"/shifter",
                //"/crawler",
                //"/punks",
                //"/teleport",
                //"/nopunks",
                //"/untouch",
                //"/waves",
                //"/damage",
                //"/bomb",
                //"/annie",
                //"/dueldmg",
                //"/dueltotal",
                //"/lock",
                //"/hvh",
                //"/respawn",
                //"/health",
                //"/noabs",
                //"/nojumpers",
                //"/nonormals"
            };
            Array.Sort<string>(F2GUI.commandsMode, (string x, string y) => string.Compare(x, y));
        }

        // Token: 0x0600045A RID: 1114 RVA: 0x0003464C File Offset: 0x0003284C
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            float num = 1024f / (float)Screen.width;
            float num2 = 768f / (float)Screen.height;
            F2GUI.GUIMatrix = new Vector3(num, num2, 1f);
        }

        // Token: 0x0600045B RID: 1115 RVA: 0x0002724C File Offset: 0x0002544C
        private void OnEnable()
        {
            if (FengGameManagerMKII.settings != null && FengGameManagerMKII.settings.Length > 0)
            {
                if (!Screen.showCursor)
                {
                    Screen.showCursor = true;
                }
                if (Screen.lockCursor)
                {
                    Screen.lockCursor = false;
                }               
                if (!IN_GAME_MAIN_CAMERA.isPausing)
                {
                    IN_GAME_MAIN_CAMERA.isPausing = true;
                    IN_GAME_MAIN_CAMERA.isTyping = true;
                }
            }
        }

        // Token: 0x0600045C RID: 1116 RVA: 0x00034690 File Offset: 0x00032890
        private void OnDisable()
        {
            if (FengGameManagerMKII.settings != null && FengGameManagerMKII.settings.Length > 0)
            {            
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    Time.timeScale = 1f;
                }
                if (FengGameManagerMKII.instance != null && FengGameManagerMKII.gameStart)
                {
                    Screen.showCursor = false;
                    Screen.lockCursor = (IN_GAME_MAIN_CAMERA.cameraMode == CAMERA_TYPE.TPS);
                }
                else
                {
                    Screen.showCursor = true;
                    Screen.lockCursor = false;
                }
                //FengGameManagerMKII.settings[35] = (IN_GAME_MAIN_CAMERA.isTyping = (IN_GAME_MAIN_CAMERA.isPausing = false));
            }
        }

        // Token: 0x0600045D RID: 1117 RVA: 0x0003472C File Offset: 0x0003292C
        internal void SelectionGrid(int id, Matrix4x4 resize, Matrix4x4 matrix, float divider, float adder)
        {
            if (id == 4)
            {
                int i;
                for (i = 0; i < F2GUI.commandsReg.Length; i++)
                {
                    if ((FengGameManagerMKII.SelectCMD).IsNullOrEmpty())
                    {
                        i = -1;
                        break;
                    }
                    if (FengGameManagerMKII.SelectCMD == F2GUI.commandsReg[i])
                    {
                        break;
                    }
                }
                GUI.contentColor = Color.white;
                GUI.matrix = resize;
                GUILayout.BeginArea(new Rect(613f / divider, 555f / divider + adder, 1060f / divider, 1800f / divider));
                int arg_E1_0 = i;
                string[] arg_E1_1 = F2GUI.commandsReg;
                int arg_E1_2 = 4;
                GUIStyle gUIStyle = new GUIStyle("Button");
                gUIStyle.wordWrap = true;
                gUIStyle.alignment = TextAnchor.MiddleCenter;
                gUIStyle.fontSize = ((divider != 1f) ? 13 : 35);
                int num = GUILayout.SelectionGrid(arg_E1_0, arg_E1_1, arg_E1_2, gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.Width(1060f / divider)
                });
                GUILayout.EndArea();
                GUI.matrix = matrix;
                GUI.contentColor = Color.white;
                GUI.backgroundColor = Color.clear;
                if (i != num)
                {
                    FengGameManagerMKII.SelectCMD = F2GUI.commandsReg[num];
                    cmdmenuselect = 0;
                    return;
                }
            }
            else if (id == 5)
            {
                GUI.contentColor = Color.white;
                int i;
                for (i = 0; i < F2GUI.commandsMode.Length; i++)
                {
                    if ((FengGameManagerMKII.SelectCMD).IsNullOrEmpty())
                    {
                        i = -1;
                        break;
                    }
                    if (FengGameManagerMKII.SelectCMD == F2GUI.commandsMode[i])
                    {
                        break;
                    }
                }
                GUI.matrix = resize;
                GUILayout.BeginArea(new Rect(613f / divider, 555f / divider + adder, 1060f / divider, 1800f / divider));
                int arg_20F_0 = i;
                string[] arg_20F_1 = F2GUI.commandsMode;
                int arg_20F_2 = 4;
                GUIStyle gUIStyle2 = new GUIStyle("Button");
                gUIStyle2.wordWrap = true;
                gUIStyle2.alignment = TextAnchor.MiddleCenter;
                gUIStyle2.fontSize = ((divider != 1f) ? 13 : 35);
                int num2 = GUILayout.SelectionGrid(arg_20F_0, arg_20F_1, arg_20F_2, gUIStyle2, new GUILayoutOption[]
                {
                    GUILayout.Width(1060f / divider)
                });
                GUILayout.EndArea();
                GUI.matrix = matrix;
                GUI.contentColor = Color.white;
                GUI.backgroundColor = Color.clear;
                if (i != num2)
                {
                    FengGameManagerMKII.SelectCMD = F2GUI.commandsMode[num2];
                    cmdmenuselect = 0;
                }
            }
        }

        // Token: 0x0600045E RID: 1118 RVA: 0x00034990 File Offset: 0x00032B90
        public void OnGUI()
        {
            GUI.depth = -100;          
            if (!disable)
            {
                if (!Screen.showCursor)
                {
                    Screen.showCursor = true;
                }
                if (Screen.lockCursor)
                {
                    Screen.lockCursor = false;
                }
                if (!IN_GAME_MAIN_CAMERA.isPausing)
                {
                    IN_GAME_MAIN_CAMERA.isPausing = true;
                    IN_GAME_MAIN_CAMERA.isTyping = true;
                }
                float num = (float)Screen.width / 4f;
                float num2 = (float)Screen.height / 4f;
                float num3 = (float)Screen.width / 2f;
                float num4 = (float)Screen.height / 2f;
                Rect rect;
                rect = new Rect(num, num2, num3, num4);
                GUI.backgroundColor = Color.black;
                GUI.Box(new Rect(num - 9f, num2 - 39f, num3 + 18f, num4 + 48f), string.Empty);
                GUI.Box(new Rect(num - 5f, num2 - 35f, num3 + 10f, num4 + 40f), string.Empty);
                GUI.Box(new Rect(num - 5f, num2 - 35f, num3 + 10f, num4 + 40f), string.Empty);
                GUI.backgroundColor = new Color(0f, 1f, 1f, 1f);
                GUI.Box(rect, string.Empty);
                GUI.Box(rect, string.Empty);
                GUI.Box(rect, string.Empty);
                if (select == 2)
                {
                    GUI.backgroundColor = Color.grey;
                    string text = "<b>STARTER</b>  <size=12><i>These are the few things in this mod that makes it unique.</i></size>";
                    Rect rect2 = GUILayoutUtility.GetRect(new GUIContent(text), "Button");
                    Rect rect3;
                    rect3 = new Rect(num + 5f, num2 + 5f, num3 - 20f, 25f);
                    if (rect3.width < rect2.width)
                    {
                        GUI.Label(rect3, string.Empty, "Button");
                        GUI.Label(new Rect(num + 10f, num2 + 7f, num3 - 20f, 25f), "<b>STARTER</b>", "Label");
                    }
                    else
                    {
                        GUI.Label(rect3, text, "Button");
                    }
                    string text2 = string.Join("\r\n", FengGameManagerMKII.StarterExplaination());
                    GUILayout.BeginArea(new Rect(num + 5f, num2 + 35f, num3 - 5f, num4 - 35f));
                    this.scroll = GUILayout.BeginScrollView(this.scroll, new GUILayoutOption[0]);
                    GUILayout.Label(text2, F2GUI.formatStyle("Label"), new GUILayoutOption[0]);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();
                }
                else if (select == 1)
                {
                    GUI.backgroundColor = Color.grey;
                    string text3 = "<size=11><i><b>COMMAND LIST</b></i></size>\r\n<size=11><i>Choose a command.</i></size>";
                    GUIStyle gUIStyle = new GUIStyle("Button");
                    Rect rect4 = GUILayoutUtility.GetRect(new GUIContent(text3), gUIStyle);
                    GUI.Label(new Rect(num + num3 - rect4.width, num2 + 4f, rect4.width, 50f), text3);
                    GUI.Label(new Rect(num + 5f, num2 + 5f, num3 - 10f, 30f), string.Empty, gUIStyle);
                    GUI.backgroundColor = Color.Lerp(Color.grey, Color.white, 0.95f);
                    if ((int)cmdmenuselect == 1)
                    {
                        GUI.backgroundColor = Color.black;
                    }
                    Rect rect5 = GUILayoutUtility.GetRect(new GUIContent("REGULAR"), gUIStyle);
                    Rect rect6 = GUILayoutUtility.GetRect(new GUIContent("GAMEMODE"), gUIStyle);
                    if (GUI.Button(new Rect(num + 10f, num2 + 10f, rect5.width, 20f), "REGULAR", gUIStyle))
                    {
                        cmdmenuselect = (((int)cmdmenuselect != 1) ? 1 : 0);
                    }
                    GUI.backgroundColor = Color.Lerp(Color.grey, Color.white, 0.95f);
                    if ((int)cmdmenuselect == 2)
                    {
                        GUI.backgroundColor = Color.black;
                    }
                    if (GUI.Button(new Rect(num + 15f + rect5.width, num2 + 10f, rect6.width, 20f), "GAMEMODE", gUIStyle))
                    {
                        cmdmenuselect = (((int)cmdmenuselect != 2) ? 2 : 0);
                    }
                    if ((int)cmdmenuselect == 1 || (int)cmdmenuselect == 2)
                    {
                        GUI.backgroundColor = Color.red;
                        if ((int)cmdmenuselect == 1)
                        {
                            GUI.backgroundColor = Color.red;
                            this.SelectionGrid(4, F2GUI.resize, F2GUI.matrix, F2GUI.divider, F2GUI.adder);
                        }
                        else if ((int)cmdmenuselect == 2)
                        {
                            GUI.backgroundColor = Color.red;
                            this.SelectionGrid(5, F2GUI.resize, F2GUI.matrix, F2GUI.divider, F2GUI.adder);
                        }
                    }
                    else
                    {
                        GUI.matrix = F2GUI.resize;
                        GUI.backgroundColor = Color.white;
                        Rect arg_5D0_0 = new Rect(610f / F2GUI.divider, 555f / F2GUI.divider + F2GUI.adder, 1180f / F2GUI.divider, 60f / F2GUI.divider);
                        string arg_5D0_1 = FengGameManagerMKII.SelectCMD;
                        GUIStyle gUIStyle2 = new GUIStyle("Button");
                        gUIStyle2.wordWrap = (true);
                        gUIStyle2.alignment = TextAnchor.MiddleLeft;
                        gUIStyle2.fontSize = ((F2GUI.divider != 1f) ? 15 : 35);
                        gUIStyle2.fontStyle = FontStyle.BoldAndItalic;
                        GUI.Label(arg_5D0_0, arg_5D0_1, gUIStyle2);
                        string[] array = new string[]
                        {
                        string.Empty,
                        FengGameManagerMKII.CmdDescription()
                        };
                        if (!array[1].IsNullOrEmpty() && array[1].Contains("\n"))
                        {
                            int startIndex = array[1].IndexOf("\n");
                            array = new string[]
                            {
                            array[1].Remove(startIndex),
                            array[1].Substring(startIndex)
                            };
                            array[0] = Regex.Replace(array[0], "\\s(or)\\s", " </i></color>$1<color=yellow><i> ");
                            array[0] = Regex.Replace(array[0], "(\\[)(.+?)(\\])", "<color=yellow><b>$1</b></color><color=white>$2</color><color=yellow><b>$3</b></color>");
                            array[0] = "<color=yellow><i>" + array[0] + "</i></color>";
                            array[1] = "\r\n" + array[1].TrimStart(new char[0]);
                        }
                        bool flag = F2GUI.divider != 1f;
                        GUI.backgroundColor = Color.grey;
                        GUILayout.BeginArea(new Rect(610f / F2GUI.divider, 665f / F2GUI.divider + F2GUI.adder, 1190f / F2GUI.divider, 825f / F2GUI.divider));
                        if (flag)
                        {
                            this.scroll = GUILayout.BeginScrollView(this.scroll, new GUILayoutOption[]
                            {
                            GUILayout.MaxHeight(620f / F2GUI.divider + F2GUI.adder)
                            });
                        }
                        else
                        {
                            this.scroll = GUILayout.BeginScrollView(this.scroll, new GUILayoutOption[]
                            {
                            GUILayout.MaxHeight(700f)
                            });
                        }
                        string arg_7B9_0 = array[0];
                        GUIStyle gUIStyle3 = new GUIStyle("Box");
                        gUIStyle3.wordWrap = true;
                        gUIStyle3.alignment = TextAnchor.MiddleLeft;
                        gUIStyle3.fontSize = (flag ? 14 : ((Screen.height > 1000) ? 28 : 33));
                        GUILayout.Label(arg_7B9_0, gUIStyle3, new GUILayoutOption[0]);
                        string arg_80C_0 = array[1];
                        GUIStyle gUIStyle4 = new GUIStyle("Label");
                        gUIStyle4.wordWrap = true;
                        gUIStyle4.alignment = 0;
                        gUIStyle4.fontSize = (flag ? 14 : ((Screen.height > 1000) ? 24 : 29));
                        GUILayout.Label(arg_80C_0, gUIStyle4, new GUILayoutOption[0]);
                        GUILayout.EndScrollView();
                        GUILayout.EndArea();
                        GUI.matrix = F2GUI.matrix;
                    }
                }
                else if (select == 0)
                {
                    GUI.backgroundColor = Color.grey;
                    string text4 = "<b>CHANGELOG</b>  <size=12><i>For viewing the recent changes of this mod or the next newest update.</i></size>";
                    Rect rect7 = GUILayoutUtility.GetRect(new GUIContent(text4), "Button");
                    Rect rect8;
                    rect8 = new Rect(num + 5f, num2 + 5f, num3 - 20f, 25f);
                    if (rect8.width < rect7.width)
                    {
                        GUI.Label(rect8, string.Empty, "Button");
                        GUI.Label(new Rect(num + 10f, num2 + 7f, num3 - 20f, 25f), "<b>CHANGELOG</b>", "Label");
                    }
                    else
                    {
                        GUI.Label(rect8, text4, "Button");
                    }
                    string text5 = FengGameManagerMKII.ChangeLog();
                    GUILayout.BeginArea(new Rect(num + 5f, num2 + 35f, num3, num4 - 35f));
                    this.scroll = GUILayout.BeginScrollView(this.scroll, new GUILayoutOption[0]);
                    GUILayout.Label(text5, F2GUI.formatStyle("Label"), new GUILayoutOption[0]);
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();
                }
                GUI.backgroundColor = Color.red;
                if (GUI.Button(new Rect(num + num3 / 3f + 2.5f, num2 - 35f, num3 / 3f - 5f, 30f), "Command List"))
                {
                    select = 1;
                    return;
                }
                if (GUI.Button(new Rect(num + num3 / 3f * 2f + 2.5f, num2 - 35f, num3 / 3f - 2.5f, 30f), "ChangeLog"))
                {
                    select = 0;
                    return;
                }
                if (GUI.Button(new Rect(num, num2 - 35f, num3 / 3f - 2.5f, 30f), "Starter"))
                {
                    select = 2;
                }
            }
        }

        // Token: 0x0400037B RID: 891
        private static readonly string[] commandsReg = new string[]
        {
            "/slp",
            //"/setmc",
            //"/time",
            //"/kick",
            //"/ban",
            //"/unban",
            //"/add",
            //"/erase",
            //"/die",
            //"/live",
            //"/deathlist",
            //"/antisteal",
            //"/antiname",
            //"/afk",
            //"/banlist",
            //"/cmdlist",
            //"/roomlist",
            //"/objlist",
            //"/namelist",
            //"/crashlist",
            //"/mutelist",
            //"/kill",
            //"/rek",
            //"/clear",
            //"/mode",
            //"/light",
            //"/revive",
            //"/resetkd",
            //"/destroy",
            //"/maxplayers",
            //"//s",
            //"//w",
            //"//c",
            //"/me",
            //"/pm",
            //"/color",
            //"/quit",
            //"/pvp",
            //"/crash",
            //"/mute",
            //"/unmute",
            //"/users",
            //"/noguests",
            //"GroupChats",
            //"/dc",
            //"/info",
            //"/current",
            //"/modinfo",
            //"/horse",
            //"/spawn",
            //"/immune",
            //"/setvice"
        };

        // Token: 0x0400037C RID: 892
        private static readonly string[] commandsMode;

        // Token: 0x0400037D RID: 893
        private Vector2 scroll = Vector3.zero;

        // Token: 0x0400037E RID: 894
        private static Vector3 GUIMatrix = Vector3.zero;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2)) disable = !disable;
        }
    }
}

