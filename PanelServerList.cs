using System;
using System.Collections;
using BRM;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class PanelServerList : MonoBehaviour
{
    // Token: 0x06000FCD RID: 4045 RVA: 0x0000C7F5 File Offset: 0x0000A9F5
    private void KeyUP()
    {
        this.keyup = true;
    }

    // Token: 0x06000FCE RID: 4046 RVA: 0x000A6878 File Offset: 0x000A4A78
    private void OnButtonPress(string choose)
    {
        if (choose != "diff")
        {
            PanelMultiJoin.chooseDIFF = false;
        }
        if (choose != "light")
        {
            PanelMultiJoin.chooseLIGHT = false;
        }
        if (choose != "lvl")
        {
            PanelMultiJoin.chooseLVL = false;
        }
        if (choose != "map")
        {
            PanelMultiJoin.chooseMAP = false;
        }
        if (choose != "type")
        {
            PanelMultiJoin.chooseTYPE = false;
        }
    }

    // Token: 0x06000FCF RID: 4047 RVA: 0x000A68E4 File Offset: 0x000A4AE4
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
                                if (PanelMultiJoin.chooseLIGHT)
                                {
                                    if (base.IsInvoking("KeyUP"))
                                    {
                                        base.CancelInvoke("KeyUP");
                                    }
                                    this.keyup = false;
                                    Pair<string, int> arg_327_0 = PanelMultiJoin.DAYLIGHT;
                                    string[] arg_326_0 = this.lights;
                                    PanelMultiJoin.DAYLIGHT.Second = choice;
                                    arg_327_0.First = arg_326_0[choice];
                                    PanelMultiJoin.chooseLIGHT = false;
                                    base.Invoke("TurnOnNGUI", 0.32f);
                                    PanelMultiJoin.updateServerList();
                                }
                            }
                        }
                        else if (PanelMultiJoin.chooseDIFF)
                        {
                            if (base.IsInvoking("KeyUP"))
                            {
                                base.CancelInvoke("KeyUP");
                            }
                            this.keyup = false;
                            Pair<string, int> arg_2A2_0 = PanelMultiJoin.DIFFICULTY;
                            string[] arg_2A1_0 = this.diffs;
                            PanelMultiJoin.DIFFICULTY.Second = choice;
                            arg_2A2_0.First = arg_2A1_0[choice];
                            PanelMultiJoin.chooseDIFF = false;
                            base.Invoke("TurnOnNGUI", 0.32f);
                            PanelMultiJoin.updateServerList();
                        }
                    }
                    else if (PanelMultiJoin.chooseTYPE)
                    {
                        if (base.IsInvoking("KeyUP"))
                        {
                            base.CancelInvoke("KeyUP");
                        }
                        this.keyup = false;
                        Pair<string, int> arg_21A_0 = PanelMultiJoin.TYPENAME;
                        string[] arg_219_0 = this.types;
                        PanelMultiJoin.TYPENAME.Second = choice;
                        arg_21A_0.First = arg_219_0[choice];
                        PanelMultiJoin.chooseTYPE = false;
                        base.Invoke("TurnOnNGUI", 0.32f);
                        PanelMultiJoin.updateServerList();
                    }
                }
                else if (PanelMultiJoin.chooseLVL)
                {
                    if (base.IsInvoking("KeyUP"))
                    {
                        base.CancelInvoke("KeyUP");
                    }
                    this.keyup = false;
                    Pair<string, int> arg_192_0 = PanelMultiJoin.LEVELNAME;
                    string[] arg_191_0 = this.levels;
                    PanelMultiJoin.LEVELNAME.Second = choice;
                    arg_192_0.First = arg_191_0[choice];
                    PanelMultiJoin.chooseLVL = false;
                    base.Invoke("TurnOnNGUI", 0.32f);
                    PanelMultiJoin.updateServerList();
                }
            }
            else if (PanelMultiJoin.chooseMAP)
            {
                if (base.IsInvoking("KeyUP"))
                {
                    base.CancelInvoke("KeyUP");
                }
                this.keyup = false;
                Pair<string, int> arg_10C_0 = PanelMultiJoin.MAPNAME;
                string[] arg_10B_0 = this.maps;
                PanelMultiJoin.MAPNAME.Second = choice;
                arg_10C_0.First = arg_10B_0[choice];
                PanelMultiJoin.chooseMAP = false;
                base.Invoke("TurnOnNGUI", 0.32f);
                PanelMultiJoin.updateServerList();
            }
        }
        yield break;
    }

    // Token: 0x06000FD0 RID: 4048 RVA: 0x000A6910 File Offset: 0x000A4B10
    private void Start()
    {
        float x = 1024f / (float)Screen.width;
        float y = (float)Screen.height / 768f;
        PanelServerList.GUIMatrix = new Vector3(x, y, 1f);
    }

    // Token: 0x06000FD1 RID: 4049 RVA: 0x000A6948 File Offset: 0x000A4B48
    private static float MatrixX(float value)
    {
        return (float)Screen.width / 2f - Mathf.Clamp(value * PanelServerList.GUIMatrix.y, (value - 50f) * PanelServerList.GUIMatrix.y, (value - 50f) * PanelServerList.GUIMatrix.y + 300f);
    }

    // Token: 0x06000FD2 RID: 4050 RVA: 0x0000C7FE File Offset: 0x0000A9FE
    private static float MatrixY(float value)
    {
        return value * PanelServerList.GUIMatrix.y;
    }

    // Token: 0x06000FD3 RID: 4051 RVA: 0x000A69A0 File Offset: 0x000A4BA0
    private void OnGUI()
    {
        //if ((string)FengGameManagerMKII.settings[263] != "yes" || (string)FengGameManagerMKII.settings[263] != "on")
        if (!FengGameManagerMKII.serverList)
        {
            GUI.backgroundColor = Color.black;
            GUI.Label(new Rect((float)Screen.width - 100f, PanelServerList.MatrixY(743f), 80f, 20f), "Players: " + PhotonNetwork.countOfPlayersInRooms.ToString());
            PanelMultiJoin.filterfull = GUI.Toggle(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(144f), 70f, 30f), PanelMultiJoin.filterfull, "Exclude\n full");
            GUI.Label(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(210f), 70f, 20f), "Order by:");
            bool flag = PanelMultiJoin.RoomCount;
            if (PanelMultiJoin.RoomCount = GUI.Toggle(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(230f), 70f, 15f), PanelMultiJoin.RoomCount, "Count"))
            {
                PanelMultiJoin.A2Z = false;
                PanelMultiJoin.Z2A = false;
            }
            if (flag != PanelMultiJoin.RoomCount)
            {
                PanelMultiJoin.updateServerList();
            }
            flag = PanelMultiJoin.A2Z;
            if (PanelMultiJoin.A2Z = GUI.Toggle(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(255f), 70f, 15f), PanelMultiJoin.A2Z, "A-Z"))
            {
                PanelMultiJoin.RoomCount = false;
                PanelMultiJoin.Z2A = false;
            }
            if (flag != PanelMultiJoin.A2Z)
            {
                PanelMultiJoin.updateServerList();
            }
            flag = PanelMultiJoin.Z2A;
            if (PanelMultiJoin.Z2A = GUI.Toggle(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(280f), 70f, 15f), PanelMultiJoin.Z2A, "Z-A"))
            {
                PanelMultiJoin.RoomCount = false;
                PanelMultiJoin.A2Z = false;
            }
            if (flag != PanelMultiJoin.Z2A)
            {
                PanelMultiJoin.updateServerList();
            }
            flag = PanelMultiJoin.chooseMAP;
            GUILayout.BeginArea(new Rect(PanelServerList.MatrixX(500f), PanelServerList.MatrixY(305f), 500f, 700f));
            GUIStyle style = new GUIStyle("Button")
            {
                wordWrap = true,
                margin = new RectOffset(0, 0, 2, 0)
            };
            if (PanelMultiJoin.chooseMAP = GUILayout.Toggle(PanelMultiJoin.chooseMAP, "MAP:" + PanelMultiJoin.MAPNAME.First, style, new GUILayoutOption[]
            {
            GUILayout.Width(75f)
            }))
            {
                if (base.IsInvoking("TurnOnNGUI"))
                {
                    base.CancelInvoke("TurnOnNGUI");
                }
                /*FengGameManagerMKII.settings[82] = */
                flip = true;
                Rect position = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 67f);
                GUI.Box(position, string.Empty);
                int num = GUILayout.SelectionGrid(PanelMultiJoin.MAPNAME.Second, this.maps, 3, style, new GUILayoutOption[0]);
                if (PanelMultiJoin.MAPNAME.Second != num)
                {
                    if (base.IsInvoking("KeyUP"))
                    {
                        base.CancelInvoke("KeyUP");
                    }
                    base.StopAllCoroutines();
                    this.keyup = false;
                    PanelMultiJoin.MAPNAME.First = this.maps[PanelMultiJoin.MAPNAME.Second = num];
                    PanelMultiJoin.chooseMAP = false;
                    base.Invoke("TurnOnNGUI", 0.32f);
                    PanelMultiJoin.updateServerList();
                }
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Rect lastRect = GUILayoutUtility.GetLastRect();
                    lastRect = new Rect(PanelServerList.MatrixX(500f) + lastRect.xMin, PanelServerList.MatrixY(305f) + lastRect.yMin + 2f, lastRect.width, lastRect.height);
                    if (this.keyup && lastRect.Contains(Input.mousePosition))
                    {
                        base.StartCoroutine(this.HasSelected(num, "map"));
                    }
                    else
                    {
                        base.Invoke("KeyUP", 0.5f);
                    }
                }
                if (flag != PanelMultiJoin.chooseMAP)
                {
                    this.OnButtonPress("map");
                    PanelMultiJoin.updateServerList();
                }
            }
            else
            {
                if (flag != PanelMultiJoin.chooseMAP)
                {
                    this.OnButtonPress("map");
                    PanelMultiJoin.updateServerList();
                }
                flag = PanelMultiJoin.chooseLVL;
                if (PanelMultiJoin.chooseLVL = GUILayout.Toggle(PanelMultiJoin.chooseLVL, "LVL:" + PanelMultiJoin.LEVELNAME.First, style, new GUILayoutOption[]
                {
                GUILayout.Width(75f)
                }))
                {
                    if (base.IsInvoking("TurnOnNGUI"))
                    {
                        base.CancelInvoke("TurnOnNGUI");
                    }
                    /*FengGameManagerMKII.settings[82] = (*/
                    this.flip = true;
                    Rect position2 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 159f);
                    GUI.Box(position2, string.Empty);
                    int num2 = GUILayout.SelectionGrid(PanelMultiJoin.LEVELNAME.Second, this.levels, 3, style, new GUILayoutOption[0]);
                    if (PanelMultiJoin.LEVELNAME.Second != num2)
                    {
                        if (base.IsInvoking("KeyUP"))
                        {
                            base.CancelInvoke("KeyUP");
                        }
                        base.StopAllCoroutines();
                        this.keyup = false;
                        PanelMultiJoin.LEVELNAME.First = this.levels[PanelMultiJoin.LEVELNAME.Second = num2];
                        PanelMultiJoin.chooseLVL = false;
                        base.Invoke("TurnOnNGUI", 0.32f);
                        PanelMultiJoin.updateServerList();
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        Rect lastRect2 = GUILayoutUtility.GetLastRect();
                        lastRect2 = new Rect(PanelServerList.MatrixX(500f) + lastRect2.xMin, PanelServerList.MatrixY(305f) + lastRect2.yMin + 45f, 500f, 115f);
                        if (this.keyup && lastRect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                        {
                            base.StartCoroutine(this.HasSelected(num2, "level"));
                        }
                        else
                        {
                            base.Invoke("KeyUP", 0.5f);
                        }
                    }
                    if (flag != PanelMultiJoin.chooseLVL)
                    {
                        this.OnButtonPress("lvl");
                        PanelMultiJoin.updateServerList();
                    }
                }
                else
                {
                    if (flag != PanelMultiJoin.chooseLVL)
                    {
                        this.OnButtonPress("lvl");
                        PanelMultiJoin.updateServerList();
                    }
                    flag = PanelMultiJoin.chooseTYPE;
                    if (PanelMultiJoin.chooseTYPE = GUILayout.Toggle(PanelMultiJoin.chooseTYPE, "TYPE:" + PanelMultiJoin.TYPENAME.First, style, new GUILayoutOption[]
                    {
                    GUILayout.Width(75f)
                    }))
                    {
                        if (base.IsInvoking("TurnOnNGUI"))
                        {
                            base.CancelInvoke("TurnOnNGUI");
                        }
                        /*FengGameManagerMKII.settings[82] = (*/
                        this.flip = true;
                        Rect position3 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 90f);
                        GUI.Box(position3, string.Empty);
                        int num3 = GUILayout.SelectionGrid(PanelMultiJoin.TYPENAME.Second, this.types, 3, style, new GUILayoutOption[0]);
                        if (PanelMultiJoin.TYPENAME.Second != num3)
                        {
                            if (base.IsInvoking("KeyUP"))
                            {
                                base.CancelInvoke("KeyUP");
                            }
                            base.StopAllCoroutines();
                            this.keyup = false;
                            PanelMultiJoin.TYPENAME.First = this.types[PanelMultiJoin.TYPENAME.Second = num3];
                            PanelMultiJoin.chooseTYPE = false;
                            base.Invoke("TurnOnNGUI", 0.32f);
                            PanelMultiJoin.updateServerList();
                        }
                        else if (Input.GetKeyUp(KeyCode.Mouse0))
                        {
                            Rect lastRect3 = GUILayoutUtility.GetLastRect();
                            lastRect3 = new Rect(PanelServerList.MatrixX(500f) + lastRect3.xMin, PanelServerList.MatrixY(305f) + lastRect3.yMin, lastRect3.width, lastRect3.height);
                            if (this.keyup && lastRect3.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                            {
                                base.StartCoroutine(this.HasSelected(num3, "type"));
                            }
                            else
                            {
                                base.Invoke("KeyUP", 0.5f);
                            }
                        }
                        if (flag != PanelMultiJoin.chooseTYPE)
                        {
                            this.OnButtonPress("type");
                            PanelMultiJoin.updateServerList();
                        }
                    }
                    else
                    {
                        if (flag != PanelMultiJoin.chooseTYPE)
                        {
                            this.OnButtonPress("type");
                            PanelMultiJoin.updateServerList();
                        }
                        flag = PanelMultiJoin.chooseDIFF;
                        if (PanelMultiJoin.chooseDIFF = GUILayout.Toggle(PanelMultiJoin.chooseDIFF, "DIFF:" + PanelMultiJoin.DIFFICULTY.First, style, new GUILayoutOption[]
                        {
                        GUILayout.Width(75f)
                        }))
                        {
                            if (base.IsInvoking("TurnOnNGUI"))
                            {
                                base.CancelInvoke("TurnOnNGUI");
                            }
                            /*FengGameManagerMKII.settings[82] = (*/
                            this.flip = true;
                            Rect position4 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 44f);
                            GUI.Box(position4, string.Empty);
                            int num4 = GUILayout.SelectionGrid(PanelMultiJoin.DIFFICULTY.Second, this.diffs, 3, style, new GUILayoutOption[0]);
                            if (PanelMultiJoin.DIFFICULTY.Second != num4)
                            {
                                if (base.IsInvoking("KeyUP"))
                                {
                                    base.CancelInvoke("KeyUP");
                                }
                                base.StopAllCoroutines();
                                this.keyup = false;
                                PanelMultiJoin.DIFFICULTY.First = this.diffs[PanelMultiJoin.DIFFICULTY.Second = num4];
                                PanelMultiJoin.chooseDIFF = false;
                                base.Invoke("TurnOnNGUI", 0.32f);
                                PanelMultiJoin.updateServerList();
                            }
                            else if (Input.GetKeyUp(KeyCode.Mouse0))
                            {
                                Rect lastRect4 = GUILayoutUtility.GetLastRect();
                                lastRect4 = new Rect(PanelServerList.MatrixX(500f) + lastRect4.xMin, PanelServerList.MatrixY(305f) + lastRect4.yMin, lastRect4.width, lastRect4.height);
                                if (this.keyup && lastRect4.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
                                {
                                    base.StartCoroutine(this.HasSelected(num4, "diff"));
                                }
                                else
                                {
                                    base.Invoke("KeyUP", 0.5f);
                                }
                            }
                            if (flag != PanelMultiJoin.chooseDIFF)
                            {
                                this.OnButtonPress("diff");
                                PanelMultiJoin.updateServerList();
                            }
                        }
                        else
                        {
                            if (flag != PanelMultiJoin.chooseDIFF)
                            {
                                this.OnButtonPress("diff");
                                PanelMultiJoin.updateServerList();
                            }
                            flag = PanelMultiJoin.chooseLIGHT;
                            if (PanelMultiJoin.chooseLIGHT = GUILayout.Toggle(PanelMultiJoin.chooseLIGHT, "DAYLIGHT:" + PanelMultiJoin.DAYLIGHT.First, style, new GUILayoutOption[]
                            {
                            GUILayout.Width(75f)
                            }))
                            {
                                if (base.IsInvoking("TurnOnNGUI"))
                                {
                                    base.CancelInvoke("TurnOnNGUI");
                                }
                                /* FengGameManagerMKII.settings[82] = (*/
                                this.flip = true;
                                Rect position5 = new Rect(0f, GUILayoutUtility.GetLastRect().yMax + 2f, 500f, 44f);
                                GUI.Box(position5, string.Empty);
                                int num5 = GUILayout.SelectionGrid(PanelMultiJoin.DAYLIGHT.Second, this.lights, 3, style, new GUILayoutOption[0]);
                                if (PanelMultiJoin.DAYLIGHT.Second != num5)
                                {
                                    if (base.IsInvoking("KeyUP"))
                                    {
                                        base.CancelInvoke("KeyUP");
                                    }
                                    base.StopAllCoroutines();
                                    this.keyup = false;
                                    PanelMultiJoin.DAYLIGHT.First = this.lights[PanelMultiJoin.DAYLIGHT.Second = num5];
                                    PanelMultiJoin.chooseLIGHT = false;
                                    base.Invoke("TurnOnNGUI", 0.32f);
                                    PanelMultiJoin.updateServerList();
                                }
                                else if (Input.GetKeyUp(KeyCode.Mouse0))
                                {
                                    Rect lastRect5 = GUILayoutUtility.GetLastRect();
                                    lastRect5 = new Rect(PanelServerList.MatrixX(500f) + lastRect5.xMin, PanelServerList.MatrixY(305f) + lastRect5.yMin, lastRect5.width, lastRect5.height);
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
                            else if (/*(bool)FengGameManagerMKII.settings[82] && */this.flip && !base.IsInvoking("TurnOnNGUI"))
                            {
                                base.Invoke("TurnOnNGUI", 0.32f);
                            }
                            if (flag != PanelMultiJoin.chooseLIGHT)
                            {
                                this.OnButtonPress("light");
                                PanelMultiJoin.updateServerList();
                            }
                        }
                    }
                }
            }
            GUILayout.EndArea();
        }
        else
        {
            ServerListGUI.serverlistGUI.enabled = true;
        }
    }

    // Token: 0x06000FD4 RID: 4052 RVA: 0x000A7664 File Offset: 0x000A5864
    private void TurnOnNGUI()
    {
        /*FengGameManagerMKII.settings[82] = (*/this.flip = false;
        base.StopAllCoroutines();
    }

    // Token: 0x06000FD5 RID: 4053 RVA: 0x000A7690 File Offset: 0x000A5890
    private void TurnOffNGUI()
    {
        /*FengGameManagerMKII.settings[82] = (*/this.flip = true;
    }

    // Token: 0x06000FD6 RID: 4054 RVA: 0x0000263F File Offset: 0x0000083F
    private void OnEnable()
    {
    }

    // Token: 0x06000FD7 RID: 4055 RVA: 0x000A76B4 File Offset: 0x000A58B4
    private void showTxt()
    {
        if (this.lang != Language.type)
        {
            this.lang = Language.type;
            this.label_name.GetComponent<UILabel>().text = Language.server_name[Language.type];
            this.label_refresh.GetComponent<UILabel>().text = Language.btn_refresh[Language.type];
            this.label_back.GetComponent<UILabel>().text = Language.btn_back[Language.type];
            this.label_create.GetComponent<UILabel>().text = Language.btn_create_game[Language.type];
        }
    }

    // Token: 0x06000FD8 RID: 4056 RVA: 0x0000C80C File Offset: 0x0000AA0C
    private void Update()
    {
        this.showTxt();
    }

    // Token: 0x04000FDE RID: 4062
    private bool keyup;

    // Token: 0x04000FDF RID: 4063
    private bool flip;

    // Token: 0x04000FE0 RID: 4064
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

    // Token: 0x04000FE1 RID: 4065
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

    // Token: 0x04000FE2 RID: 4066
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

    // Token: 0x04000FE3 RID: 4067
    private readonly string[] diffs = new string[]
    {
        "Any",
        "normal",
        "hard",
        "abnormal"
    };

    // Token: 0x04000FE4 RID: 4068
    private readonly string[] lights = new string[]
    {
        "Any",
        "day",
        "dawn",
        "night"
    };

    // Token: 0x04000FE5 RID: 4069
    private static Vector3 GUIMatrix = Vector3.zero;

    // Token: 0x04000FE6 RID: 4070
    public GameObject label_back;

    // Token: 0x04000FE7 RID: 4071
    public GameObject label_create;

    // Token: 0x04000FE8 RID: 4072
    public GameObject label_name;

    // Token: 0x04000FE9 RID: 4073
    public GameObject label_refresh;

    // Token: 0x04000FEA RID: 4074
    private int lang = -1;
}
