using System;

namespace CLEARSKIES
{
    // Token: 0x02000016 RID: 22
    public struct RoomData
    {
        // Token: 0x060000A4 RID: 164 RVA: 0x000146F4 File Offset: 0x000128F4
        public RoomData(RoomInfo roominfo)
        {
            this = new RoomData(roominfo.name.Split(new char[]
            {
                '`'
            }), (byte)roominfo.playerCount, roominfo.maxPlayers);
        }

        // Token: 0x060000A5 RID: 165 RVA: 0x0001472C File Offset: 0x0001292C
        public RoomData(string roomname, byte PlayerCount, byte MaxPlayers)
        {
            this = new RoomData(roomname.Split(new char[]
            {
                '`'
            }), PlayerCount, MaxPlayers);
        }

        // Token: 0x060000A6 RID: 166 RVA: 0x00014754 File Offset: 0x00012954
        public RoomData(string[] roominfo, byte PlayerCount, byte MaxPlayers)
        {
            if (roominfo.Length > 5)
            {
                this.roomName = string.Join("`", roominfo);
                this.password = roominfo[5];
                this.name = roominfo[0].ToRGBA();
                LevelInfo info = LevelInfo.getInfo(this.level = roominfo[1]);
                this.gamemode = ((info != null) ? info.type : GAMEMODE.None);
                string a;
                if ((a = roominfo[2]) != null)
                {
                    if (a == "normal")
                    {
                        this.difficulty = 0;
                        goto IL_9F;
                    }
                    if (a == "hard")
                    {
                        this.difficulty = 1;
                        goto IL_9F;
                    }
                    if (!(a == "abnormal"))
                    {
                    }
                }
                this.difficulty = 2;
            IL_9F:
                if (!int.TryParse(roominfo[3], out this.timeMinutes))
                {
                    this.timeMinutes = 20;
                }
                string a2;
                if ((a2 = roominfo[4]) != null)
                {
                    if (a2 == "dawn")
                    {
                        this.dayLight = DayLight.Dawn;
                        goto IL_100;
                    }
                    if (a2 == "night")
                    {
                        this.dayLight = DayLight.Night;
                        goto IL_100;
                    }
                    if (!(a2 == "day"))
                    {
                    }
                }
                this.dayLight = DayLight.Day;
            IL_100:
                this.playerCount = PlayerCount;
                this.maxPlayers = MaxPlayers;
                return;
            }
            throw new System.ArgumentException("The length of the array must be greater than 5.", "roominfo");
        }

        // Token: 0x060000A7 RID: 167 RVA: 0x00014880 File Offset: 0x00012A80
        public override string ToString()
        {
            return string.Concat(new object[]
            {
                (this.password == string.Empty) ? string.Empty : "[PWD]",
                this.name,
                "/",
                this.level,
                "/",
                this.difficulty,
                "/",
                this.dayLight,
                " ",
                this.playerCount,
                "/",
                this.maxPlayers
            });
        }

        // Token: 0x04000065 RID: 101
        public string roomName;

        // Token: 0x04000066 RID: 102
        public string name;

        // Token: 0x04000067 RID: 103
        public string level;

        // Token: 0x04000068 RID: 104
        public GAMEMODE gamemode;

        // Token: 0x04000069 RID: 105
        public DayLight dayLight;

        // Token: 0x0400006A RID: 106
        public byte difficulty;

        // Token: 0x0400006B RID: 107
        public string password;

        // Token: 0x0400006C RID: 108
        public byte playerCount;

        // Token: 0x0400006D RID: 109
        public byte maxPlayers;

        // Token: 0x0400006E RID: 110
        public int timeMinutes;
    }
}
