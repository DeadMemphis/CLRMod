using System;
using System.Collections.Generic;
using System.Linq;
using CLEARSKIES;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class PanelMultiJoin : MonoBehaviour
{
    // Token: 0x06000FA0 RID: 4000 RVA: 0x0000C619 File Offset: 0x0000A819
    public static void updateServerList()
    {
        PanelMultiJoin.panelJOIN.updateFilterRooms();
        PanelMultiJoin.panelJOIN.showlist();
    }

    // Token: 0x06000FA1 RID: 4001 RVA: 0x0000C62F File Offset: 0x0000A82F
    private void Awake()
    {
        PanelMultiJoin.panelJOIN = this;
    }

    // Token: 0x06000FA2 RID: 4002 RVA: 0x0000C637 File Offset: 0x0000A837
    private void OnDestroy()
    {
        PanelMultiJoin.panelJOIN = null;
    }

    // Token: 0x06000FA3 RID: 4003 RVA: 0x000A5CEC File Offset: 0x000A3EEC
    public void connectToIndex(int index, string roomName)
    {
        int i;
        for (i = 0; i < 10; i++)
        {
            this.items[i].SetActive(false);
        }
        i = 10 * (this.currentPage - 1) + index;
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

    // Token: 0x06000FA4 RID: 4004 RVA: 0x00014BD0 File Offset: 0x00012DD0
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

    // Token: 0x06000FA5 RID: 4005 RVA: 0x0000263F File Offset: 0x0000083F
    private void OnDisable()
    {
    }

    // Token: 0x06000FA6 RID: 4006 RVA: 0x0000C63F File Offset: 0x0000A83F
    private void OnEnable()
    {
        PanelMultiJoin.panelJOIN = this;
        this.currentPage = 1;
        this.totalPage = 0;
        this.refresh();
    }

    // Token: 0x06000FA7 RID: 4007 RVA: 0x0000C65B File Offset: 0x0000A85B
    public void OnFilterSubmit(string content)
    {
        this.filter = content;
        this.updateFilterRooms();
        this.refresh();
    }

    // Token: 0x06000FA8 RID: 4008 RVA: 0x0000C670 File Offset: 0x0000A870
    public void pageDown()
    {
        this.currentPage++;
        if (this.currentPage > this.totalPage)
        {
            this.currentPage = 1;
        }
        this.showServerList();
    }

    // Token: 0x06000FA9 RID: 4009 RVA: 0x0000C69B File Offset: 0x0000A89B
    public void pageUp()
    {
        this.currentPage--;
        if (this.currentPage < 1)
        {
            this.currentPage = this.totalPage;
        }
        this.showServerList();
    }

    // Token: 0x06000FAA RID: 4010 RVA: 0x0000C6C6 File Offset: 0x0000A8C6
    public void refresh()
    {
        this.showlist();
    }

    // Token: 0x06000FAB RID: 4011 RVA: 0x000A5D88 File Offset: 0x000A3F88
    private void showlist()
    {
        this.updateFilterRooms();
        if (this.filterRoom.Count > 0)
        {
            this.totalPage = (this.filterRoom.Count - 1) / 10 + 1;
        }
        if (this.currentPage < 1)
        {
            this.currentPage = this.totalPage;
        }
        if (this.currentPage > this.totalPage)
        {
            this.currentPage = 1;
        }
        this.showServerList();
    }

    // Token: 0x06000FAC RID: 4012 RVA: 0x000A5DF4 File Offset: 0x000A3FF4
    private void showServerList()
    {
        if (PhotonNetwork.GetRoomList().Length != 0)
        {
            for (int i = 0; i < 10; i++)
            {
                int num = 10 * (this.currentPage - 1) + i;
                if (num < this.filterRoom.Count)
                {
                    RoomInfo roomInfo = this.filterRoom[num];
                    this.items[i].SetActive(true);
                    this.items[i].GetComponentInChildren<UILabel>().text = PanelMultiJoin.getServerDataString(roomInfo);
                    this.items[i].GetComponentInChildren<BTN_Connect_To_Server_On_List>().roomName = roomInfo.name;
                }
                else
                {
                    this.items[i].SetActive(false);
                }
            }
            CacheGameObject.Find<UILabel>("LabelServerListPage").text = this.currentPage + "/" + this.totalPage;
        }
    }

    // Token: 0x06000FAD RID: 4013 RVA: 0x000A5EC4 File Offset: 0x000A40C4
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            this.items[i].SetActive(true);
            this.items[i].GetComponentInChildren<UILabel>().text = string.Empty;
            this.items[i].SetActive(false);
        }
        this.updateFilterRooms();
        this.showlist();
    }

    // Token: 0x06000FAE RID: 4014 RVA: 0x0000C6CE File Offset: 0x0000A8CE
    private void Update()
    {
        this.elapsedTime += Time.deltaTime;
        if (this.elapsedTime > 3f)
        {
            this.elapsedTime = 0f;
            this.showlist();
        }
    }

    // Token: 0x06000FAF RID: 4015 RVA: 0x000A5F20 File Offset: 0x000A4120
    private void updateFilterRooms()
    {
        this.filterRoom = new System.Collections.Generic.List<RoomInfo>();
        bool flag = !PanelMultiJoin.MAPNAME.First.IsNullOrEmpty() && PanelMultiJoin.MAPNAME.First != "Any";
        bool flag2 = !PanelMultiJoin.LEVELNAME.First.IsNullOrEmpty() && PanelMultiJoin.LEVELNAME.First != "Any";
        bool flag3 = !PanelMultiJoin.TYPENAME.First.IsNullOrEmpty() && PanelMultiJoin.TYPENAME.First != "Any";
        bool flag4 = !PanelMultiJoin.DIFFICULTY.First.IsNullOrEmpty() && PanelMultiJoin.DIFFICULTY.First != "Any";
        bool flag5 = !PanelMultiJoin.DAYLIGHT.First.IsNullOrEmpty() && PanelMultiJoin.DAYLIGHT.First != "Any";
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
                    if ((flag2 && array[1] != PanelMultiJoin.LEVELNAME.First) || (flag4 && array[2] != PanelMultiJoin.DIFFICULTY.First) || (flag5 && array[4] != PanelMultiJoin.DAYLIGHT.First))
                    {
                        goto IL_2D9;
                    }
                    if (flag)
                    {
                        if (PanelMultiJoin.MAPNAME.First == "CUSTOM")
                        {
                            if (array[1] != "Custom" && array[1] != "Custom (No PT)")
                            {
                                goto IL_2D9;
                            }
                            if (flag3)
                            {
                                LevelInfo info = LevelInfo.getInfo(array[1]);
                                if (info == null)
                                {
                                    goto IL_2D9;
                                }
                                if (info.type.ToString() != PanelMultiJoin.TYPENAME.First)
                                {
                                    goto IL_2D9;
                                }
                            }
                        }
                        else
                        {
                            LevelInfo info2 = LevelInfo.getInfo(array[1]);
                            if (info2 == null || info2.mapName != PanelMultiJoin.MAPNAME.First)
                            {
                                goto IL_2D9;
                            }
                            if (flag3 && info2.type.ToString() != PanelMultiJoin.TYPENAME.First)
                            {
                                goto IL_2D9;
                            }
                        }
                    }
                    else if (flag3)
                    {
                        LevelInfo info3 = LevelInfo.getInfo(array[1]);
                        if (info3 == null || info3.type.ToString() != PanelMultiJoin.TYPENAME.First)
                        {
                            goto IL_2D9;
                        }
                    }
                }
                if ((this.filter == string.Empty || roomInfo.name.ToUpper().Contains(this.filter.ToUpper())) && (!PanelMultiJoin.filterfull || roomInfo.playerCount != (int)roomInfo.maxPlayers))
                {
                    this.filterRoom.Add(roomInfo);
                }
            }
        IL_2D9:;
        }
        if (PanelMultiJoin.RoomCount)
        {
            this.filterRoom = new System.Collections.Generic.List<RoomInfo>((from room in this.filterRoom
                                                                             orderby room.playerCount descending
                                                                             select room).ToArray<RoomInfo>());
            return;
        }
        if (PanelMultiJoin.A2Z)
        {
            this.filterRoom = new System.Collections.Generic.List<RoomInfo>(this.filterRoom.OrderBy((RoomInfo x) => x, new FunctionComparer<RoomInfo>((RoomInfo x, RoomInfo y) => string.Compare(x.name.Split(new char[]
            {
                '`'
            })[0].NullFix().StripHex().Trim(), y.name.Split(new char[]
            {
                '`'
            })[0].NullFix().StripHex().Trim()))));
            return;
        }
        if (PanelMultiJoin.Z2A)
        {
            this.filterRoom = new System.Collections.Generic.List<RoomInfo>(this.filterRoom.OrderByDescending((RoomInfo x) => x, new FunctionComparer<RoomInfo>((RoomInfo x, RoomInfo y) => string.Compare(x.name.Split(new char[]
            {
                '`'
            })[0].NullFix().StripHex().Trim(), y.name.Split(new char[]
            {
                '`'
            })[0].NullFix().StripHex().Trim()))));
        }
    }

    // Token: 0x04000F97 RID: 3991
    public static PanelMultiJoin panelJOIN;

    // Token: 0x04000F98 RID: 3992
    public static bool filterfull = false;

    // Token: 0x04000F99 RID: 3993
    public static bool A2Z = false;

    // Token: 0x04000F9A RID: 3994
    public static bool Z2A = false;

    // Token: 0x04000F9B RID: 3995
    public static bool chooseMAP = false;

    // Token: 0x04000F9C RID: 3996
    public static bool chooseLVL = false;

    // Token: 0x04000F9D RID: 3997
    public static bool chooseTYPE = false;

    // Token: 0x04000F9E RID: 3998
    public static bool chooseDIFF = false;

    // Token: 0x04000F9F RID: 3999
    public static bool chooseLIGHT = false;

    // Token: 0x04000FA0 RID: 4000
    public static Pair<string, int> MAPNAME = new Pair<string, int>("Any", 0);

    // Token: 0x04000FA1 RID: 4001
    public static Pair<string, int> LEVELNAME = new Pair<string, int>("Any", 0);

    // Token: 0x04000FA2 RID: 4002
    public static Pair<string, int> TYPENAME = new Pair<string, int>("Any", 0);

    // Token: 0x04000FA3 RID: 4003
    public static Pair<string, int> DIFFICULTY = new Pair<string, int>("Any", 0);

    // Token: 0x04000FA4 RID: 4004
    public static Pair<string, int> DAYLIGHT = new Pair<string, int>("Any", 0);

    // Token: 0x04000FA5 RID: 4005
    public static bool RoomCount = false;

    // Token: 0x04000FA6 RID: 4006
    private int currentPage = 1;

    // Token: 0x04000FA7 RID: 4007
    private float elapsedTime = 5f;

    // Token: 0x04000FA8 RID: 4008
    public string filter = string.Empty;

    // Token: 0x04000FA9 RID: 4009
    private System.Collections.Generic.List<RoomInfo> filterRoom = new System.Collections.Generic.List<RoomInfo>();

    // Token: 0x04000FAA RID: 4010
    public GameObject[] items;

    // Token: 0x04000FAB RID: 4011
    private int totalPage = 1;
}
