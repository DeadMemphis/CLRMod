using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CLEARSKIES
{
    // Token: 0x02000019 RID: 25
    internal class ServerListGUI : MonoBehaviour/*, MenuGUI*/
    {
        // Token: 0x060000AD RID: 173 RVA: 0x00002A75 File Offset: 0x00000C75
        public static void updateServerList()
        {
            ServerListGUI.serverlistGUI.updateFilterRooms();
        }

        // Token: 0x060000AE RID: 174 RVA: 0x00002A81 File Offset: 0x00000C81
        private void Awake()
        {
            ServerListGUI.serverlistGUI = this;
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00002A89 File Offset: 0x00000C89
        private void OnDestroy()
        {
            ServerListGUI.serverlistGUI = null;
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x00002A91 File Offset: 0x00000C91
        public static void connectToIndex(RoomInfo roomInfo)
        {
            ServerListGUI.connectToIndex(roomInfo.name);
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x00014AE8 File Offset: 0x00012CE8
        public static void connectToIndex(string roomName)
        {
            string[] array = roomName.Split(new char[]
            {
                '`'
            });
            if (array[5] != string.Empty)
            {
                PanelMultiJoinPWD.Password = array[5];
                PanelMultiJoinPWD.roomName = roomName;
                NGUITools.SetActive(UIMainReferences.UIRefer.PanelMultiPWD, true);
                NGUITools.SetActive(UIMainReferences.UIRefer.panelMultiROOM, false);
                ServerListGUI.serverlistGUI.enabled = false;
                return;
            }
            PhotonNetwork.JoinRoom(roomName);
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00014B5C File Offset: 0x00012D5C
        public static void connectToIndex(RoomData room)
        {
            if (room.password != string.Empty)
            {
                PanelMultiJoinPWD.Password = room.password;
                PanelMultiJoinPWD.roomName = room.roomName;
                NGUITools.SetActive(UIMainReferences.UIRefer.PanelMultiPWD, true);
                NGUITools.SetActive(UIMainReferences.UIRefer.panelMultiROOM, false);
                ServerListGUI.serverlistGUI.enabled = false;
                return;
            }
            PhotonNetwork.JoinRoom(room.roomName);
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x00014BD0 File Offset: 0x00012DD0
        private static string getServerDataString(RoomInfo room)
        {
            string[] array = room.name.Split(new char[]
            {
                '`'
            });
            return string.Concat(new object[]
            {
                (array[5] == string.Empty) ? string.Empty : "[PWD]",
                array[0],
                "/",
                array[1],
                "/",
                array[2],
                "/",
                array[4],
                " ",
                room.playerCount,
                "/",
                room.maxPlayers
            });
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x00002A9E File Offset: 0x00000C9E
        private void OnEnable()
        {
            ServerListGUI.GUIMatrix = new Vector3(1024f / (float)Screen.width, (float)Screen.height / 768f, 1f);
            this.updateFilterRooms();
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x00014C84 File Offset: 0x00012E84
        private static float MatrixX(float value)
        {
            return (float)Screen.width / 2f - Mathf.Clamp(value * ServerListGUI.GUIMatrix.y, (value - 50f) * ServerListGUI.GUIMatrix.y, (value - 50f) * ServerListGUI.GUIMatrix.y + 300f);
        }

        // Token: 0x060000B6 RID: 182 RVA: 0x00002ACD File Offset: 0x00000CCD
        private static float MatrixY(float value)
        {
            return value * ServerListGUI.GUIMatrix.y;
        }

        // Token: 0x060000B7 RID: 183 RVA: 0x00014CDC File Offset: 0x00012EDC
        private static string DiffAsString(byte difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    return "normal";
                case 1:
                    return "hard";
                case 2:
                    return "abnormal";
                default:
                    return null;
            }
        }

        // Token: 0x060000B8 RID: 184 RVA: 0x00014D14 File Offset: 0x00012F14
        private static byte DiffAsByte(string difficulty)
        {
            if (difficulty != null)
            {
                if (difficulty == "normal")
                {
                    return 0;
                }
                if (difficulty == "hard")
                {
                    return 1;
                }
                if (difficulty == "abnormal")
                {
                    return 2;
                }
            }
            return 3;
        }

        // Token: 0x060000B9 RID: 185 RVA: 0x00002ADB File Offset: 0x00000CDB
        private void KeyUP()
        {
            this.keyup = true;
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00014D58 File Offset: 0x00012F58
        private void TurnOnNGUI()
        {
            /*FengGameManagerMKII.settings[82] = (*/this.flip = false/*)*/;
            base.StopAllCoroutines();
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00014D84 File Offset: 0x00012F84
        private void TurnOffNGUI()
        {
            /*FengGameManagerMKII.settings[82] = (*/this.flip = true/*)*/;
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00014DA8 File Offset: 0x00012FA8
        private void OnButtonPress(string choose)
        {
            if (choose != "diff")
            {
                ServerListGUI.chooseDIFF = false;
            }
            if (choose != "light")
            {
                ServerListGUI.chooseLIGHT = false;
            }
            if (choose != "lvl")
            {
                ServerListGUI.chooseLVL = false;
            }
            if (choose != "map")
            {
                ServerListGUI.chooseMAP = false;
            }
            if (choose != "type")
            {
                ServerListGUI.chooseTYPE = false;
            }
        }

        // Token: 0x060000BD RID: 189 RVA: 0x00014E14 File Offset: 0x00013014
        private System.Collections.IEnumerator HasSelected(int choice, string choose)
        {
            yield return new WaitForSeconds(0.5f);
            if (this.keyup && choose != null)
            {
                if (!(choose == "map"))
                {
                    if (!(choose == "level"))
                    {
                        if (!(choose == "type"))
                        {
                            if (!(choose == "diff"))
                            {
                                if (choose == "light")
                                {
                                    if (ServerListGUI.chooseLIGHT)
                                    {
                                        if (base.IsInvoking("KeyUP"))
                                        {
                                            base.CancelInvoke("KeyUP");
                                        }
                                        this.keyup = false;
                                        Pair<string, int> arg_327_0 = ServerListGUI.DAYLIGHT;
                                        string[] arg_326_0 = this.lights;
                                        ServerListGUI.DAYLIGHT.Second = choice;
                                        arg_327_0.First = arg_326_0[choice];
                                        ServerListGUI.chooseLIGHT = false;
                                        base.Invoke("TurnOnNGUI", 0.32f);
                                        ServerListGUI.updateServerList();
                                    }
                                }
                            }
                            else if (ServerListGUI.chooseDIFF)
                            {
                                if (base.IsInvoking("KeyUP"))
                                {
                                    base.CancelInvoke("KeyUP");
                                }
                                this.keyup = false;
                                Pair<string, int> arg_2A2_0 = ServerListGUI.DIFFICULTY;
                                string[] arg_2A1_0 = this.diffs;
                                ServerListGUI.DIFFICULTY.Second = choice;
                                arg_2A2_0.First = arg_2A1_0[choice];
                                ServerListGUI.chooseDIFF = false;
                                base.Invoke("TurnOnNGUI", 0.32f);
                                ServerListGUI.updateServerList();
                            }
                        }
                        else if (ServerListGUI.chooseTYPE)
                        {
                            if (base.IsInvoking("KeyUP"))
                            {
                                base.CancelInvoke("KeyUP");
                            }
                            this.keyup = false;
                            Pair<string, int> arg_21A_0 = ServerListGUI.TYPENAME;
                            string[] arg_219_0 = this.types;
                            ServerListGUI.TYPENAME.Second = choice;
                            arg_21A_0.First = arg_219_0[choice];
                            ServerListGUI.chooseTYPE = false;
                            base.Invoke("TurnOnNGUI", 0.32f);
                            ServerListGUI.updateServerList();
                        }
                    }
                    else if (ServerListGUI.chooseLVL)
                    {
                        if (base.IsInvoking("KeyUP"))
                        {
                            base.CancelInvoke("KeyUP");
                        }
                        this.keyup = false;
                        Pair<string, int> arg_192_0 = ServerListGUI.LEVELNAME;
                        string[] arg_191_0 = this.levels;
                        ServerListGUI.LEVELNAME.Second = choice;
                        arg_192_0.First = arg_191_0[choice];
                        ServerListGUI.chooseLVL = false;
                        base.Invoke("TurnOnNGUI", 0.32f);
                        ServerListGUI.updateServerList();
                    }
                }
                else if (ServerListGUI.chooseMAP)
                {
                    if (base.IsInvoking("KeyUP"))
                    {
                        base.CancelInvoke("KeyUP");
                    }
                    this.keyup = false;
                    Pair<string, int> arg_10C_0 = ServerListGUI.MAPNAME;
                    string[] arg_10B_0 = this.maps;
                    ServerListGUI.MAPNAME.Second = choice;
                    arg_10C_0.First = arg_10B_0[choice];
                    ServerListGUI.chooseMAP = false;
                    base.Invoke("TurnOnNGUI", 0.32f);
                    ServerListGUI.updateServerList();
                }
            }
            yield break;
        }

        // Token: 0x060000BE RID: 190 RVA: 0x00014E40 File Offset: 0x00013040
        private void Options()
        {
            GUILayout.Label("Order by:", new GUILayoutOption[0]);
            bool flag = ServerListGUI.RoomCount;
            if (ServerListGUI.RoomCount = GUILayout.Toggle(ServerListGUI.RoomCount, "Count", new GUILayoutOption[]
            {
                GUILayout.Width(70f),
                GUILayout.Height(15f)
            }))
            {
                ServerListGUI.A2Z = false;
                ServerListGUI.Z2A = false;
            }
            if (flag != ServerListGUI.RoomCount)
            {
                this.updateFilterRooms();
            }
            flag = ServerListGUI.A2Z;
            if (ServerListGUI.A2Z = GUILayout.Toggle(ServerListGUI.A2Z, "A-Z", new GUILayoutOption[]
            {
                GUILayout.Width(70f),
                GUILayout.Height(15f)
            }))
            {
                ServerListGUI.RoomCount = false;
                ServerListGUI.Z2A = false;
            }
            if (flag != ServerListGUI.A2Z)
            {
                this.updateFilterRooms();
            }
            flag = ServerListGUI.Z2A;
            if (ServerListGUI.Z2A = GUILayout.Toggle(ServerListGUI.Z2A, "Z-A", new GUILayoutOption[]
            {
                GUILayout.Width(70f),
                GUILayout.Height(15f)
            }))
            {
                ServerListGUI.RoomCount = false;
                ServerListGUI.A2Z = false;
            }
            if (flag != ServerListGUI.Z2A)
            {
                this.updateFilterRooms();
            }
            flag = ServerListGUI.chooseMAP;
            GUIStyle style = new GUIStyle("Button")
            {
                wordWrap = true,
                margin = new RectOffset(0, 0, 2, 0)
            };
            if (ServerListGUI.chooseMAP = GUILayout.Toggle(ServerListGUI.chooseMAP, "MAP:" + ServerListGUI.MAPNAME.First, style, new GUILayoutOption[]
            {
                GUILayout.Width(75f)
            }))
            {
                if (base.IsInvoking("TurnOnNGUI"))
                {
                    base.CancelInvoke("TurnOnNGUI");
                }
                /*FengGameManagerMKII.settings[82] = (*/this.flip = true/*)*/;
                Rect position = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 67f);
                GUI.DrawTexture(position, FengGameManagerMKII.instance.textureBackgroundPitchBlack);
                int num = GUILayout.SelectionGrid(ServerListGUI.MAPNAME.Second, this.maps, 3, style, new GUILayoutOption[]
                {
                    GUILayout.Width(position.width)
                });
                if (ServerListGUI.MAPNAME.Second != num)
                {
                    if (base.IsInvoking("KeyUP"))
                    {
                        base.CancelInvoke("KeyUP");
                    }
                    base.StopAllCoroutines();
                    this.keyup = false;
                    ServerListGUI.MAPNAME.First = this.maps[ServerListGUI.MAPNAME.Second = num];
                    ServerListGUI.chooseMAP = false;
                    base.Invoke("TurnOnNGUI", 0.32f);
                    this.updateFilterRooms();
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Rect lastRect = GUILayoutUtility.GetLastRect();
                    lastRect = new Rect(ServerListGUI.MatrixX(500f) + lastRect.xMin, ServerListGUI.MatrixY(305f) + lastRect.yMin + 2f, lastRect.width, lastRect.height);
                    if (this.keyup && lastRect.Contains(Input.mousePosition))
                    {
                        base.StartCoroutine(this.HasSelected(num, "map"));
                    }
                    else
                    {
                        base.Invoke("KeyUP", 0.5f);
                    }
                }
                if (flag != ServerListGUI.chooseMAP)
                {
                    this.OnButtonPress("map");
                    this.updateFilterRooms();
                    return;
                }
            }
            else
            {
                if (flag != ServerListGUI.chooseMAP)
                {
                    this.OnButtonPress("map");
                    this.updateFilterRooms();
                }
                flag = ServerListGUI.chooseLVL;
                if (ServerListGUI.chooseLVL = GUILayout.Toggle(ServerListGUI.chooseLVL, "LVL:" + ServerListGUI.LEVELNAME.First, style, new GUILayoutOption[]
                {
                    GUILayout.Width(75f)
                }))
                {
                    if (base.IsInvoking("TurnOnNGUI"))
                    {
                        base.CancelInvoke("TurnOnNGUI");
                    }
                    /*FengGameManagerMKII.settings[82] = (*/this.flip = true/*)*/;
                    Rect position2 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 159f);
                    GUI.DrawTexture(position2, FengGameManagerMKII.instance.textureBackgroundPitchBlack);
                    int num2 = GUILayout.SelectionGrid(ServerListGUI.LEVELNAME.Second, this.levels, 3, style, new GUILayoutOption[]
                    {
                        GUILayout.Width(position2.width)
                    });
                    if (ServerListGUI.LEVELNAME.Second != num2)
                    {
                        if (base.IsInvoking("KeyUP"))
                        {
                            base.CancelInvoke("KeyUP");
                        }
                        base.StopAllCoroutines();
                        this.keyup = false;
                        ServerListGUI.LEVELNAME.First = this.levels[ServerListGUI.LEVELNAME.Second = num2];
                        ServerListGUI.chooseLVL = false;
                        base.Invoke("TurnOnNGUI", 0.32f);
                        this.updateFilterRooms();
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        Rect lastRect2 = GUILayoutUtility.GetLastRect();
                        lastRect2 = new Rect(ServerListGUI.MatrixX(500f) + lastRect2.xMin, ServerListGUI.MatrixY(305f) + lastRect2.yMin + 45f, 500f, 115f);
                        if (this.keyup && lastRect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                        {
                            base.StartCoroutine(this.HasSelected(num2, "level"));
                        }
                        else
                        {
                            base.Invoke("KeyUP", 0.5f);
                        }
                    }
                    if (flag != ServerListGUI.chooseLVL)
                    {
                        this.OnButtonPress("lvl");
                        this.updateFilterRooms();
                        return;
                    }
                }
                else
                {
                    if (flag != ServerListGUI.chooseLVL)
                    {
                        this.OnButtonPress("lvl");
                        this.updateFilterRooms();
                    }
                    flag = ServerListGUI.chooseTYPE;
                    if (ServerListGUI.chooseTYPE = GUILayout.Toggle(ServerListGUI.chooseTYPE, "TYPE:" + ServerListGUI.TYPENAME.First, style, new GUILayoutOption[]
                    {
                        GUILayout.Width(75f)
                    }))
                    {
                        if (base.IsInvoking("TurnOnNGUI"))
                        {
                            base.CancelInvoke("TurnOnNGUI");
                        }
                        /*FengGameManagerMKII.settings[82] = (*/this.flip = true/*)*/;
                        Rect position3 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 90f);
                        GUI.DrawTexture(position3, FengGameManagerMKII.instance.textureBackgroundPitchBlack);
                        int num3 = GUILayout.SelectionGrid(ServerListGUI.TYPENAME.Second, this.types, 3, style, new GUILayoutOption[]
                        {
                            GUILayout.Width(position3.width)
                        });
                        if (ServerListGUI.TYPENAME.Second != num3)
                        {
                            if (base.IsInvoking("KeyUP"))
                            {
                                base.CancelInvoke("KeyUP");
                            }
                            base.StopAllCoroutines();
                            this.keyup = false;
                            ServerListGUI.TYPENAME.First = this.types[ServerListGUI.TYPENAME.Second = num3];
                            ServerListGUI.chooseTYPE = false;
                            base.Invoke("TurnOnNGUI", 0.32f);
                            this.updateFilterRooms();
                        }
                        else if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            Rect lastRect3 = GUILayoutUtility.GetLastRect();
                            lastRect3 = new Rect(ServerListGUI.MatrixX(500f) + lastRect3.xMin, ServerListGUI.MatrixY(305f) + lastRect3.yMin, lastRect3.width, lastRect3.height);
                            if (this.keyup && lastRect3.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                            {
                                base.StartCoroutine(this.HasSelected(num3, "type"));
                            }
                            else
                            {
                                base.Invoke("KeyUP", 0.5f);
                            }
                        }
                        if (flag != ServerListGUI.chooseTYPE)
                        {
                            this.OnButtonPress("type");
                            this.updateFilterRooms();
                            return;
                        }
                    }
                    else
                    {
                        if (flag != ServerListGUI.chooseTYPE)
                        {
                            this.OnButtonPress("type");
                            this.updateFilterRooms();
                        }
                        flag = ServerListGUI.chooseDIFF;
                        if (ServerListGUI.chooseDIFF = GUILayout.Toggle(ServerListGUI.chooseDIFF, "DIFF:" + ServerListGUI.DIFFICULTY.First, style, new GUILayoutOption[]
                        {
                            GUILayout.Width(75f)
                        }))
                        {
                            if (base.IsInvoking("TurnOnNGUI"))
                            {
                                base.CancelInvoke("TurnOnNGUI");
                            }
                            /*FengGameManagerMKII.settings[82] = (*/this.flip = true/*)*/;
                            Rect position4 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 44f);
                            GUI.DrawTexture(position4, FengGameManagerMKII.instance.textureBackgroundPitchBlack);
                            int num4 = GUILayout.SelectionGrid(ServerListGUI.DIFFICULTY.Second, this.diffs, 3, style, new GUILayoutOption[]
                            {
                                GUILayout.Width(position4.width)
                            });
                            if (ServerListGUI.DIFFICULTY.Second != num4)
                            {
                                if (base.IsInvoking("KeyUP"))
                                {
                                    base.CancelInvoke("KeyUP");
                                }
                                base.StopAllCoroutines();
                                this.keyup = false;
                                ServerListGUI.DIFFICULTY.First = this.diffs[ServerListGUI.DIFFICULTY.Second = num4];
                                ServerListGUI.chooseDIFF = false;
                                base.Invoke("TurnOnNGUI", 0.32f);
                                this.updateFilterRooms();
                            }
                            else if (Input.GetKeyUp(KeyCode.Mouse0))
                            {
                                Rect lastRect4 = GUILayoutUtility.GetLastRect();
                                lastRect4 = new Rect(ServerListGUI.MatrixX(500f) + lastRect4.xMin, ServerListGUI.MatrixY(305f) + lastRect4.yMin, lastRect4.width, lastRect4.height);
                                if (this.keyup && lastRect4.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                                {
                                    base.StartCoroutine(this.HasSelected(num4, "diff"));
                                }
                                else
                                {
                                    base.Invoke("KeyUP", 0.5f);
                                }
                            }
                            if (flag != ServerListGUI.chooseDIFF)
                            {
                                this.OnButtonPress("diff");
                                this.updateFilterRooms();
                                return;
                            }
                        }
                        else
                        {
                            if (flag != ServerListGUI.chooseDIFF)
                            {
                                this.OnButtonPress("diff");
                                this.updateFilterRooms();
                            }
                            flag = ServerListGUI.chooseLIGHT;
                            if (ServerListGUI.chooseLIGHT = GUILayout.Toggle(ServerListGUI.chooseLIGHT, "DAYLIGHT:" + ServerListGUI.DAYLIGHT.First, style, new GUILayoutOption[]
                            {
                                GUILayout.Width(75f)
                            }))
                            {
                                if (base.IsInvoking("TurnOnNGUI"))
                                {
                                    base.CancelInvoke("TurnOnNGUI");
                                }
                                /*FengGameManagerMKII.settings[82]*/ /*= (*/this.flip = true/*)*/;
                                Rect position5 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 44f);
                                GUI.DrawTexture(position5, FengGameManagerMKII.instance.textureBackgroundPitchBlack);
                                int num5 = GUILayout.SelectionGrid(ServerListGUI.DAYLIGHT.Second, this.lights, 3, style, new GUILayoutOption[]
                                {
                                    GUILayout.Width(position5.width)
                                });
                                if (ServerListGUI.DAYLIGHT.Second != num5)
                                {
                                    if (base.IsInvoking("KeyUP"))
                                    {
                                        base.CancelInvoke("KeyUP");
                                    }
                                    base.StopAllCoroutines();
                                    this.keyup = false;
                                    ServerListGUI.DAYLIGHT.First = this.lights[ServerListGUI.DAYLIGHT.Second = num5];
                                    ServerListGUI.chooseLIGHT = false;
                                    base.Invoke("TurnOnNGUI", 0.32f);
                                    this.updateFilterRooms();
                                }
                                else if (Input.GetKeyUp(KeyCode.Mouse0))
                                {
                                    Rect lastRect5 = GUILayoutUtility.GetLastRect();
                                    lastRect5 = new Rect(ServerListGUI.MatrixX(500f) + lastRect5.xMin, ServerListGUI.MatrixY(305f) + lastRect5.yMin, lastRect5.width, lastRect5.height);
                                    if (this.keyup && lastRect5.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                                    {
                                        base.StartCoroutine(this.HasSelected(num5, "light"));
                                    }
                                    else
                                    {
                                        base.Invoke("KeyUP", 0.5f);
                                    }
                                }
                            }
                            else if (/*(bool)FengGameManagerMKII.settings[82] &&*/ this.flip && !base.IsInvoking("TurnOnNGUI"))
                            {
                                base.Invoke("TurnOnNGUI", 0.32f);
                            }
                            if (flag != ServerListGUI.chooseLIGHT)
                            {
                                this.OnButtonPress("light");
                                this.updateFilterRooms();
                            }
                        }
                    }
                }
            }
        }

        // Token: 0x1700000D RID: 13
        // (get) Token: 0x060000BF RID: 191 RVA: 0x00002AE4 File Offset: 0x00000CE4
        private static float divider
        {
            get
            {
                if (Screen.width > 1024)
                {
                    return 1.15f;
                }
                return 1f;
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x060000C0 RID: 192 RVA: 0x00002AFD File Offset: 0x00000CFD
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

        // Token: 0x1700000F RID: 15
        // (get) Token: 0x060000C1 RID: 193 RVA: 0x00002B16 File Offset: 0x00000D16
        private static Matrix4x4 resize
        {
            get
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3((float)Screen.width / (2400f / ServerListGUI.divider), (float)Screen.height / (1800f / ServerListGUI.divider), 1f));
            }
        }

        // Token: 0x17000010 RID: 16
        // (get) Token: 0x060000C2 RID: 194 RVA: 0x00002B55 File Offset: 0x00000D55
        private static Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, 1f, 1f));
            }
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x00015AA8 File Offset: 0x00013CA8
        public void OnGUI()
        {
            if (Event.current.type == EventType.KeyDown && (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return))
            {
                this.updateFilterRooms();
                return;
            }
            GUI.Label(new Rect((float)Screen.width - 100f, ServerListGUI.MatrixY(743f), 80f, 20f), "Players: " + PhotonNetwork.countOfPlayersInRooms.ToString());
            GUI.Label(new Rect((float)Screen.width - 170f, ServerListGUI.MatrixY(743f), 80f, 20f), "Rooms: " + PhotonNetwork.countOfRooms.ToString());
            Texture2D tex = null;
            GUI.backgroundColor = Color.white;
            Rect rect = new Rect(ServerListGUI.MatrixX(400f), ServerListGUI.MatrixY(110f), 900f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 630f * ServerListGUI.GUIMatrix.y);
            GUI.DrawTexture(rect, FengGameManagerMKII.instance.textureBackgroundBlack);
            GUILayout.BeginArea(new Rect(ServerListGUI.MatrixX(390f), ServerListGUI.MatrixY(120f), 880f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 30f));
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            ServerListGUI.filterfull = GUILayout.Toggle(ServerListGUI.filterfull, "Exclude Full:", new GUILayoutOption[0]);
            GUILayout.FlexibleSpace();
            GUILayout.Label("Search:", new GUILayoutOption[]
            {
                GUILayout.Height(30f)
            });
            if (!/*(bool)FengGameManagerMKII.settings[82]*/flip)
            {
                ServerListGUI.filter = GUILayout.TextField(ServerListGUI.filter, new GUILayoutOption[]
                {
                    GUILayout.Width(300f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider))
                });
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            float num = ServerListGUI.Width["PWD"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            float num2 = ServerListGUI.Width["Count"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            float num3 = ServerListGUI.Width["Name"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            float num4 = ServerListGUI.Width["Level"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            float num5 = ServerListGUI.Width["Difficulty"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            float num6 = ServerListGUI.Width["Daylight"] / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider);
            rect = new Rect(ServerListGUI.MatrixX(313f), ServerListGUI.MatrixY(147f), 810f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 548f * ServerListGUI.GUIMatrix.y);
            GUI.DrawTexture(rect, FengGameManagerMKII.instance.textureBackgroundGrey);
            GUILayout.BeginArea(new Rect(ServerListGUI.MatrixX(310f), ServerListGUI.MatrixY(150f), 804f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 25f));
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUIStyle gUIStyle = new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter
            };
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num, 28f, new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter
            }), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "PWD", gUIStyle);
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num2, 28f, gUIStyle), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "Count", gUIStyle);
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num3, 28f, gUIStyle), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "Name", gUIStyle);
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num4, 28f, gUIStyle), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "Level", gUIStyle);
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num5, 28f, gUIStyle), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "Difficulty", gUIStyle);
            GUI.DrawTexture(rect = GUILayoutUtility.GetRect(num6, 28f, gUIStyle), FengGameManagerMKII.instance.textureBackgroundBlack);
            GUI.Label(rect, "Daylight", gUIStyle);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            Rect screenRect = new Rect(ServerListGUI.MatrixX(315f), ServerListGUI.MatrixY(180f), 804f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 515f * ServerListGUI.GUIMatrix.y);
            GUILayout.BeginArea(screenRect);
            gUIStyle.alignment = TextAnchor.MiddleLeft;
            ServerListGUI.scroll = GUILayout.BeginScrollView(ServerListGUI.scroll, new GUILayoutOption[]
            {
                GUILayout.Width(804f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider))
            });
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("\\#[a-fA-F0-9]{6}");
            Func<string, string> func = delegate (string z)
            {
                if (!(tex == FengGameManagerMKII.instance.textureBackgroundYellow))
                {
                    return z;
                }
                return "<color=black>" + (z.ContainsLightHex() ? z.StripRGBA() : z) + "</color>";
            };
            foreach (RoomData current in this.rooms)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                string arg;
                rect = GUILayoutUtility.GetRect(new GUIContent(arg = (current.password.IsNullOrEmpty() ? string.Empty : "Yes")), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(num)
                });
                Rect position = new Rect(rect.xMin, rect.yMin, 804f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), rect.height);
                tex = ((/*!(bool)FengGameManagerMKII.settings[82] &&*/ position.Contains(new Vector2(Input.mousePosition.x + ServerListGUI.scroll.x - screenRect.xMin, (float)Screen.height - Input.mousePosition.y + ServerListGUI.scroll.y - screenRect.yMin))) ? FengGameManagerMKII.instance.textureBackgroundYellow : FengGameManagerMKII.instance.textureBackgroundDarkGrey);
                GUI.DrawTexture(rect, tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                GUI.DrawTexture(rect = GUILayoutUtility.GetRect(new GUIContent("  " + (arg = current.playerCount + "/" + current.maxPlayers) + "  "), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(num2)
                }), tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                bool flag = false;
                if (current.name.Contains("#") && regex.IsMatch(current.name))
                {
                    foreach (System.Text.RegularExpressions.Match match in regex.Matches(current.name))
                    {
                        if (RCextensions.DarkHex(match.Value))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                GUI.DrawTexture(rect = GUILayoutUtility.GetRect(new GUIContent("  " + (arg = current.name.ToRGBA()) + "  "), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(num3)
                }), (tex == FengGameManagerMKII.instance.textureBackgroundDarkGrey && flag) ? FengGameManagerMKII.instance.textureBackgroundWhite : tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                GUI.DrawTexture(rect = GUILayoutUtility.GetRect(new GUIContent(arg = current.level), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(num4)
                }), tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                GUI.DrawTexture(rect = GUILayoutUtility.GetRect(new GUIContent(arg = ServerListGUI.DiffAsString(current.difficulty)), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth(num5)
                }), tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                GUI.DrawTexture(rect = GUILayoutUtility.GetRect(new GUIContent(arg = current.dayLight.ToString()), gUIStyle, new GUILayoutOption[]
                {
                    GUILayout.MinWidth((num6 * (ServerListGUI.GUIMatrix.x * ServerListGUI.divider) - 50f) / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider))
                }), tex);
                rect.xMin += 2f;
                rect.xMax -= 2f;
                GUI.Label(rect, func(arg), gUIStyle);
                GUILayout.EndHorizontal();
                if (/*!(bool)FengGameManagerMKII.settings[82] &&*/ GUI.Button(position, string.Empty, GUI.skin.label))
                {
                    ServerListGUI.connectToIndex(current);
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(ServerListGUI.MatrixX(390f), ServerListGUI.MatrixY(155f), 880f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 600f));
            this.Options();
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(ServerListGUI.MatrixX(390f), ServerListGUI.MatrixY(700f), 880f / (ServerListGUI.GUIMatrix.x * ServerListGUI.divider), 30f));
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button("CREATE GAME", new GUILayoutOption[]
            {
                GUILayout.Height(30f)
            }) /*&& !(bool)FengGameManagerMKII.settings[82]*/)
            {
                NGUITools.SetActive(UIMainReferences.UIRefer.panelMultiSet, true);
                base.enabled = false;
            }
            if (GUILayout.Button("BACK", new GUILayoutOption[]
            {
                GUILayout.Height(30f)
            }) /*&& !(bool)FengGameManagerMKII.settings[82]*/)
            {
                NGUITools.SetActive(UIMainReferences.UIRefer.panelMain, true);
                FengCustomInputs.Inputs.menuOn = false;
                PhotonNetwork.Disconnect();
                base.enabled = false;
                FengGameManagerMKII.ShowMenuButtonGUI = true;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        // Token: 0x060000C4 RID: 196 RVA: 0x00002B7A File Offset: 0x00000D7A
        private void Update()
        {
            this.elapsedTime += Time.deltaTime;
            if (this.elapsedTime > 3f)
            {
                this.elapsedTime = 0f;
                this.updateFilterRooms();
            }
        }

        // Token: 0x060000C5 RID: 197 RVA: 0x000165DC File Offset: 0x000147DC
        private void updateFilterRooms()
        {
            this.filterRoom.Clear();
            bool flag = !ServerListGUI.MAPNAME.First.IsNullOrEmpty() && ServerListGUI.MAPNAME.First != "Any";
            bool flag2 = !ServerListGUI.LEVELNAME.First.IsNullOrEmpty() && ServerListGUI.LEVELNAME.First != "Any";
            bool flag3 = !ServerListGUI.TYPENAME.First.IsNullOrEmpty() && ServerListGUI.TYPENAME.First != "Any";
            bool flag4 = !ServerListGUI.DIFFICULTY.First.IsNullOrEmpty() && ServerListGUI.DIFFICULTY.First != "Any";
            bool flag5 = !ServerListGUI.DAYLIGHT.First.IsNullOrEmpty() && ServerListGUI.DAYLIGHT.First != "Any";
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();
            for (int i = 0; i < roomList.Length; i++)
            {
                RoomInfo roomInfo = roomList[i];
                if (roomInfo != null)
                {
                    if (flag || flag2 || flag3 || flag4 || flag5)
                    {
                        string[] array = roomInfo.name.Split(new char[]
                        {
                            '`'
                        });
                        if ((flag2 && array[1] != ServerListGUI.LEVELNAME.First) || (flag4 && array[2] != ServerListGUI.DIFFICULTY.First) || (flag5 && array[4] != ServerListGUI.DAYLIGHT.First))
                        {
                            goto IL_2D7;
                        }
                        if (flag)
                        {
                            if (ServerListGUI.MAPNAME.First == "CUSTOM")
                            {
                                if (array[1] != "Custom" && array[1] != "Custom (No PT)")
                                {
                                    goto IL_2D7;
                                }
                                if (flag3)
                                {
                                    LevelInfo info = LevelInfo.getInfo(array[1]);
                                    if (info == null)
                                    {
                                        goto IL_2D7;
                                    }
                                    if (info.type.ToString() != ServerListGUI.TYPENAME.First)
                                    {
                                        goto IL_2D7;
                                    }
                                }
                            }
                            else
                            {
                                LevelInfo info2 = LevelInfo.getInfo(array[1]);
                                if (info2 == null || info2.mapName != ServerListGUI.MAPNAME.First)
                                {
                                    goto IL_2D7;
                                }
                                if (flag3 && info2.type.ToString() != ServerListGUI.TYPENAME.First)
                                {
                                    goto IL_2D7;
                                }
                            }
                        }
                        else if (flag3)
                        {
                            LevelInfo info3 = LevelInfo.getInfo(array[1]);
                            if (info3 == null || info3.type.ToString() != ServerListGUI.TYPENAME.First)
                            {
                                goto IL_2D7;
                            }
                        }
                    }
                    if ((ServerListGUI.filter == string.Empty || roomInfo.name.ToUpper().Contains(ServerListGUI.filter.ToUpper())) && (!ServerListGUI.filterfull || roomInfo.playerCount != (int)roomInfo.maxPlayers))
                    {
                        this.filterRoom.Add(roomInfo);
                    }
                }
            IL_2D7:;
            }
            if (ServerListGUI.RoomCount)
            {
                this.filterRoom = new System.Collections.Generic.List<RoomInfo>((from room in this.filterRoom
                                                                                 orderby room.playerCount descending
                                                                                 select room).ToArray<RoomInfo>());
            }
            else if (ServerListGUI.A2Z)
            {
                this.filterRoom = new System.Collections.Generic.List<RoomInfo>(this.filterRoom.OrderBy((RoomInfo x) => x, new FunctionComparer<RoomInfo>((RoomInfo x, RoomInfo y) => string.Compare(x.name.Split(new char[]
                {
                    '`'
                })[0].NullFix().StripHex().Trim(), y.name.Split(new char[]
                {
                    '`'
                })[0].NullFix().StripHex().Trim()))));
            }
            else if (ServerListGUI.Z2A)
            {
                this.filterRoom = new System.Collections.Generic.List<RoomInfo>(this.filterRoom.OrderByDescending((RoomInfo x) => x, new FunctionComparer<RoomInfo>((RoomInfo x, RoomInfo y) => string.Compare(x.name.Split(new char[]
                {
                    '`'
                })[0].NullFix().StripHex().Trim(), y.name.Split(new char[]
                {
                    '`'
                })[0].NullFix().StripHex().Trim()))));
            }
            this.rooms.Clear();
            foreach (RoomInfo current in this.filterRoom)
            {
                this.rooms.Add(new RoomData(current));
            }
        }

        // Token: 0x04000071 RID: 113
        public static ServerListGUI serverlistGUI;

        // Token: 0x04000072 RID: 114
        private static Vector3 GUIMatrix = Vector3.one;

        // Token: 0x04000073 RID: 115
        private static Vector2 scroll = Vector2.zero;

        // Token: 0x04000074 RID: 116
        public static bool filterfull = false;

        // Token: 0x04000075 RID: 117
        public static bool A2Z = false;

        // Token: 0x04000076 RID: 118
        public static bool Z2A = false;

        // Token: 0x04000077 RID: 119
        public static bool chooseMAP = false;

        // Token: 0x04000078 RID: 120
        public static bool chooseLVL = false;

        // Token: 0x04000079 RID: 121
        public static bool chooseTYPE = false;

        // Token: 0x0400007A RID: 122
        public static bool chooseDIFF = false;

        // Token: 0x0400007B RID: 123
        public static bool chooseLIGHT = false;

        // Token: 0x0400007C RID: 124
        public static Pair<string, int> MAPNAME = new Pair<string, int>("Any", 0);

        // Token: 0x0400007D RID: 125
        public static Pair<string, int> LEVELNAME = new Pair<string, int>("Any", 0);

        // Token: 0x0400007E RID: 126
        public static Pair<string, int> TYPENAME = new Pair<string, int>("Any", 0);

        // Token: 0x0400007F RID: 127
        public static Pair<string, int> DIFFICULTY = new Pair<string, int>("Any", 0);

        // Token: 0x04000080 RID: 128
        public static Pair<string, int> DAYLIGHT = new Pair<string, int>("Any", 0);

        // Token: 0x04000081 RID: 129
        public static bool RoomCount = false;

        // Token: 0x04000082 RID: 130
        private bool keyup;

        // Token: 0x04000083 RID: 131
        private bool flip;

        // Token: 0x04000084 RID: 132
        private readonly string[] maps = new string[]
        {
            "Any",
            "track - akina",
            "CaveFight",
            "The City I",
            "Colossal Titan",
            "The Forest",
            "HouseFight",
            "OutSide",
            "CUSTOM"
        };

        // Token: 0x04000085 RID: 133
        private readonly string[] levels = new string[]
        {
            "Any",
            "Racing - Akina",
            "Annie",
            "Annie II",
            "Cave Fight",
            "The City",
            "The City II",
            "The City III",
            "Colossal Titan",
            "Colossal Titan II",
            "Custom",
            "Custom (No PT)",
            "The Forest",
            "The Forest II",
            "The Forest III",
            "The Forest IV  - LAVA",
            "House Fight",
            "Outside The Walls",
            "Trost",
            "Trost II"
        };

        // Token: 0x04000086 RID: 134
        private readonly string[] types = new string[]
        {
            "Any",
            "BOSS_FIGHT_CT",
            "CAGE_FIGHT",
            "ENDLESS_TITAN",
            "KILL_TITAN",
            "PVP_AHSS",
            "PVP_CAPTURE",
            "RACING",
            "SURVIVE_MODE",
            "TROST"
        };

        // Token: 0x04000087 RID: 135
        private readonly string[] diffs = new string[]
        {
            "Any",
            "normal",
            "hard",
            "abnormal"
        };

        // Token: 0x04000088 RID: 136
        private readonly string[] lights = new string[]
        {
            "Any",
            "day",
            "dawn",
            "night"
        };

        // Token: 0x04000089 RID: 137
        private float elapsedTime = 5f;

        // Token: 0x0400008A RID: 138
        public static string filter = string.Empty;

        // Token: 0x0400008B RID: 139
        private System.Collections.Generic.List<RoomInfo> filterRoom = new System.Collections.Generic.List<RoomInfo>();

        // Token: 0x0400008C RID: 140
        private System.Collections.Generic.List<RoomData> rooms = new System.Collections.Generic.List<RoomData>();

        // Token: 0x0400008D RID: 141
        private static readonly System.Collections.Generic.Dictionary<string, float> Width = new System.Collections.Generic.Dictionary<string, float>(6)
        {
            {
                "PWD",
                44f
            },
            {
                "Name",
                259f
            },
            {
                "Level",
                209f
            },
            {
                "Difficulty",
                139f
            },
            {
                "Daylight",
                109f
            },
            {
                "Count",
                44f
            }
        };

        // Token: 0x0400008E RID: 142
        private static readonly System.Collections.Generic.Dictionary<string, float> alterWidth = new System.Collections.Generic.Dictionary<string, float>(7)
        {
            {
                "PWD",
                3f
            },
            {
                "Name",
                3f
            },
            {
                "Map",
                3f
            },
            {
                "Difficulty",
                3f
            },
            {
                "Daylight",
                3f
            },
            {
                "Count",
                3f
            },
            {
                "Max",
                3f
            }
        };
    }
}

