using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using CLEARSKIES;

public class PhotonPlayer
{
    private int actorID;
    public readonly bool isLocal;
    public bool BombHasExploded { get; set; }

    private string nameField;
    public object TagObject;


    public static void CleanProperties()
    {
        if (PhotonNetwork.player == null) return;
        PhotonNetwork.player.customProperties.Clear();

        // PhotonNetwork.playerName = FengGameManagerMKII.RandomNameGenerator(5, 8); //friendname
        string str = FengGameManagerMKII.RandomNameGenerator(5, 8);
        PhotonNetwork.player.SetCustomProperties(new Hashtable { { PhotonPlayerProperty.name, str } });
    }


    protected internal string chatname
    {
        get;
        set;
    }


    protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
    {
        this.actorID = -1;
        this.nameField = string.Empty;
        this.customProperties = new Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        this.InternalCacheProperties(properties);
    }

    public PhotonPlayer(bool isLocal, int actorID, string name)
    {
        this.actorID = -1;
        this.nameField = string.Empty;
        this.customProperties = new Hashtable();
        this.isLocal = isLocal;
        this.actorID = actorID;
        this.nameField = name;
    }

    public PhotonPlayer()
    {

    }
    //protected internal PhotonPlayer(bool isLocal, int actorID, Hashtable properties)
    //{
    //    this.actorID = -1;
    //    this.nameField = string.Empty;
    //    this.customProperties = new Hashtable();
    //    this.isLocal = isLocal;
    //    this.actorID = actorID;
    //    this.InternalCacheProperties(properties);
    //}

    //public PhotonPlayer(bool isLocal, int actorID, string name)
    //{
    //    this.actorID = -1;
    //    this.nameField = string.Empty;
    //    this.customProperties = new Hashtable();
    //    this.isLocal = isLocal;
    //    this.actorID = actorID;
    //    this.nameField = name;
    //}
    protected internal string guildname
    {
        get
        {
            object obj = this.customProperties["guildName"];
            if (obj != null && obj is string)
            {
                return (string)obj;
            }
            return string.Empty;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "guildName",
                    value
                }
            });
        }
    }
    protected internal bool isTitan
    {
        get
        {
            object obj = this.customProperties["isTitan"];
            return obj != null && obj is int && (int)obj == 2;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "isTitan",
                    value
                }
            });
        }
    }
    public static bool Find(int ID, out PhotonPlayer player)
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            PhotonPlayer photonPlayer = playerList[i];
            if (photonPlayer != null && photonPlayer.ID == ID)
            {
                player = photonPlayer;
                return true;
            }
        }
        player = null;
        return false;
    }
    protected internal string Chatname
    {
        get
        {
            if (!this.chatname.IsNullOrEmpty())
            {
                return this.chatname;
            }
            if (this.customProperties.ContainsKey("chatname") && this.customProperties["chatname"] != null)
            {
                return (string)this.customProperties["chatname"];
            }
            return string.Empty;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
        {
            {
                "chatname",
                value
            }
        });
        }
    }
    
    
    public static bool IsInPlayerList(int ID)
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            PhotonPlayer photonPlayer = playerList[i];
            if (photonPlayer != null && photonPlayer.ID == ID)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsInPlayerList()
    {
        if (this == null)
        {
            return false;
        }
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            PhotonPlayer photonPlayer = playerList[i];
            if (photonPlayer != null && this == photonPlayer)
            {
                return true;
            }
        }
        return false;
    }
    public override bool Equals(object p)
    {
        PhotonPlayer player = p as PhotonPlayer;
        return ((player != null) && (this.GetHashCode() == player.GetHashCode()));
    }

    public static PhotonPlayer Find(int ID)
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            PhotonPlayer player = PhotonNetwork.playerList[i];
            if (player.ID == ID)
            {
                return player;
            }
        }
        return null;
    }

    public PhotonPlayer Get(int id)
    {
        return Find(id);
    }

    public override int GetHashCode()
    {
        return this.ID;
    }

    public PhotonPlayer GetNext()
    {
        return this.GetNextFor(this.ID);
    }

    public PhotonPlayer GetNextFor(PhotonPlayer currentPlayer)
    {
        if (currentPlayer == null)
        {
            return null;
        }
        return this.GetNextFor(currentPlayer.ID);
    }

    public PhotonPlayer GetNextFor(int currentPlayerId)
    {
        if (((PhotonNetwork.networkingPeer == null) || (NetworkingPeer.mActors == null)) || (NetworkingPeer.mActors.Count < 2))
        {
            return null;
        }
        Dictionary<int, PhotonPlayer> mActors = NetworkingPeer.mActors;
        int num = 0x7fffffff;
        int num2 = currentPlayerId;
        foreach (int num3 in mActors.Keys)
        {
            if (num3 < num2)
            {
                num2 = num3;
            }
            else if ((num3 > currentPlayerId) && (num3 < num))
            {
                num = num3;
            }
        }
        return ((num == 0x7fffffff) ? mActors[num2] : mActors[num]);
    }

    internal void InternalCacheProperties(Hashtable properties)
    {
        if (((properties != null) && (properties.Count != 0)) && !this.customProperties.Equals(properties))
        {
            if (properties.ContainsKey((byte) 0xff))
            {
                this.nameField = (string) properties[(byte) 0xff];
            }
            this.customProperties.MergeStringKeys(properties);
            this.customProperties.StripKeysWithNullValues();
        }
    }

    internal void InternalChangeLocalID(int newID)
    {
        if (!this.isLocal)
        {
            Debug.LogError("ERROR You should never change PhotonPlayer IDs!");
        }
        else
        {
            this.actorID = newID;
        }
    }

    public void SetCustomProperties(Hashtable propertiesToSet)
    {
        if (propertiesToSet != null)
        {
            
            this.customProperties.MergeStringKeys(propertiesToSet);
            this.customProperties.StripKeysWithNullValues();
            Hashtable actorProperties = propertiesToSet.StripToStringKeys();
            if ((this.actorID > 0) && !PhotonNetwork.offlineMode)
            {
                PhotonNetwork.networkingPeer.OpSetCustomPropertiesOfActor(this.actorID, actorProperties, true, 0);
            }
            object[] parameters = new object[] { this, propertiesToSet };
            NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonPlayerPropertiesChanged, parameters);
        }
    }
    
    public override string ToString()
    {
        if (string.IsNullOrEmpty(this.name))
        {
            return string.Format("#{0:00}{1}", this.ID, !this.isMasterClient ? string.Empty : "(master)");
        }
        return string.Format("'{0}'{1}", this.name, !this.isMasterClient ? string.Empty : "(master)");
    }

    public string ToStringFull()
    {
        return string.Format("#{0:00} '{1}' {2}", this.ID, this.name, this.customProperties.ToStringFull());
    }

    public Hashtable allProperties
    {
        get
        {
            Hashtable target = new Hashtable();
            target.Merge(this.customProperties);
            target[(byte) 0xff] = this.name;
            return target;
        }
    }

    public Hashtable customProperties { get; private set; }
  
    
    #region DM FEATURES
    protected internal bool isDead
    {
        get
        {
            object obj = this.customProperties["dead"];
            return obj != null && obj is bool && (bool)obj;
        }
        private set
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("dead", value);
            this.SetCustomProperties(hashtable);
        }
    }
    public string uiname
    {
        get
        {
            object obj = this.customProperties["name"];
            if (obj != null && obj is string)
            {
                return (string)obj;
            }
            return string.Empty;
        }
        set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "name",
                    value
                }
            });
        }
    }
    public short ActorID
    {
        get
        {
            return (short)this.actorID;
        }
    }


    protected internal int RCteam
    {
        get
        {
            object obj = this.customProperties["RCteam"];
            if (obj != null && obj is int)
            {
                return (int)obj;
            }
            return 0;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCteam",
                    value
                }
            });
        }
    }


    protected internal float RCBombA
    {
        get
        {
            object obj = this.customProperties["RCBombA"];
            if (obj != null && obj is float)
            {
                return (float)obj;
            }
            return 0f;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCBombA",
                    value
                }
            });
        }
    }

    protected internal float RCBombB
    {
        get
        {
            object obj = this.customProperties["RCBombB"];
            if (obj != null && obj is float)
            {
                return (float)obj;
            }
            return 0f;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCBombB",
                    value
                }
            });
        }
    }

    protected internal float RCBombG
    {
        get
        {
            object obj = this.customProperties["RCBombG"];
            if (obj != null && obj is float)
            {
                return (float)obj;
            }
            return 0f;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCBombG",
                    value
                }
            });
        }
    }

    protected internal float RCBombR
    {
        get
        {
            object obj = this.customProperties["RCBombR"];
            if (obj != null && obj is float)
            {
                return (float)obj;
            }
            return 0f;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCBombR",
                    value
                }
            });
        }
    }

    protected internal float RCBombRadius
    {
        get
        {
            object obj = this.customProperties["RCBombRadius"];
            if (obj != null && obj is float)
            {
                return (float)obj;
            }
            return 0f;
        }
        private set
        {
            this.SetCustomProperties(new Hashtable
            {
                {
                    "RCBombRadius",
                    value
                }
            });
        }
    }
    #endregion


    public void RemoveFromCustomProperties(ExitGames.Client.Photon.Hashtable removeProps)
    {
        ExitGames.Client.Photon.Hashtable revHashTable = this.customProperties;
        foreach (System.Collections.DictionaryEntry props in removeProps)
        {
            revHashTable.Remove(props.Key);
        }
        this.SetCustomProperties(revHashTable);
    }
    
    public int ID
    {
        get
        {
            return this.actorID;
        }
    }

    public bool isMasterClient
    {
        get
        {
            return (PhotonNetwork.networkingPeer.mMasterClient == this);
        }
    }

    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            if (!this.isLocal)
            {
                Debug.LogError("Error: Cannot change the name of a remote player!");
            }
            else
            {
                this.nameField = value;
            }
        }
    }
}

