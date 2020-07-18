

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon;

namespace BRM
{
    public class Pool
    {
        public static Dictionary<int, Pool> AllPools = new Dictionary<int, Pool>();
        public static Dictionary<int, List<GameObject>> AllObjects = new Dictionary<int, List<GameObject>>();
        public int ownerHashCode;
        public PhotonView ownerPV;
        public List<GameObject> Objects;
        public Dictionary<int, string> IDToName;
        public Dictionary<string, string> PathToName;
        public Dictionary<string, int> PathToID;
        public Dictionary<string, int> NameToID;
        public Dictionary<string, int> Instantiations;

        public Pool(int hashCode, PhotonView view, Dictionary<string, int> instantiations)
        {
            this.ownerPV = view;
            this.ownerHashCode = hashCode;
            this.Instantiations = instantiations;
            this.Objects = new List<GameObject>();
            this.IDToName = new Dictionary<int, string>();
            this.NameToID = new Dictionary<string, int>();
            this.PathToID = new Dictionary<string, int>();
            this.PathToName = new Dictionary<string, string>();
            foreach (KeyValuePair<string, int> keyValuePair in instantiations)
            {
                if (keyValuePair.Value != 0 && !keyValuePair.Key.IsNullOrEmpty())
                {
                    GameObject gameObject = CacheResources.Load<GameObject>(keyValuePair.Key);
                    if (!(gameObject == null))
                    {
                        if (keyValuePair.Value == 1)
                        {
                            GameObject gameObject2;
                            if ((gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject)) == null)
                            {
                                UnityEngine.Object.Destroy(gameObject2);
                                Debug.LogError(string.Format("Couldn't add {0} to the objectPool.", gameObject.name));
                            }
                            else
                            {
                                foreach (Photon.MonoBehaviour monoBehaviour in gameObject2.GetComponents<Photon.MonoBehaviour>())
                                {
                                    if (!(monoBehaviour == null))
                                    {
                                        monoBehaviour.homePool = this;
                                    }
                                }
                                this.Objects.Add(gameObject2);
                                string name = gameObject2.name;
                                int instanceID = gameObject2.GetInstanceID();
                                gameObject2.SetActive(false);
                                if (!this.PathToID.ContainsKey(keyValuePair.Key))
                                {
                                    this.PathToID.Add(keyValuePair.Key, instanceID);
                                }
                                if (!name.IsNullOrEmpty())
                                {
                                    if (!this.PathToName.ContainsKey(keyValuePair.Key))
                                    {
                                        this.PathToName.Add(keyValuePair.Key, name);
                                    }
                                    if (!this.IDToName.ContainsKey(instanceID))
                                    {
                                        this.IDToName.Add(instanceID, name);
                                    }
                                    if (!this.NameToID.ContainsKey(name))
                                    {
                                        this.NameToID.Add(name, instanceID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < keyValuePair.Value; j++)
                            {
                                GameObject gameObject2;
                                if ((gameObject2 = (GameObject)UnityEngine.Object.Instantiate(gameObject)) == null)
                                {
                                    UnityEngine.Object.Destroy(gameObject2);
                                    Debug.LogError(string.Format("Couldn't add {0} to the objectPool.", gameObject.name));
                                }
                                else
                                {
                                    foreach (Photon.MonoBehaviour monoBehaviour2 in gameObject2.GetComponents<Photon.MonoBehaviour>())
                                    {
                                        if (!(monoBehaviour2 == null))
                                        {
                                            monoBehaviour2.homePool = this;
                                        }
                                    }
                                    this.Objects.Add(gameObject2);
                                    string name2 = gameObject2.name;
                                    int instanceID2 = gameObject2.GetInstanceID();
                                    gameObject2.SetActive(false);
                                    if (!this.PathToID.ContainsKey(keyValuePair.Key))
                                    {
                                        this.PathToID.Add(keyValuePair.Key, instanceID2);
                                    }
                                    if (!name2.IsNullOrEmpty())
                                    {
                                        if (!this.PathToName.ContainsKey(keyValuePair.Key))
                                        {
                                            this.PathToName.Add(keyValuePair.Key, name2);
                                        }
                                        if (!this.IDToName.ContainsKey(instanceID2))
                                        {
                                            this.IDToName.Add(instanceID2, name2);
                                        }
                                        if (!this.NameToID.ContainsKey(name2))
                                        {
                                            this.NameToID.Add(name2, instanceID2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Pool.AllObjects[this.ownerHashCode] = this.Objects;
            Pool.AllPools[this.ownerHashCode] = this;
        }
        public Pool(Photon.MonoBehaviour mono, PhotonView view, Dictionary<string, int> instantiations) : this(mono.GetHashCode(), view, instantiations)
        {
        }
        public Pool(GameObject GO, PhotonView view, Dictionary<string, int> instantiations) : this(GO.GetHashCode(), view, instantiations)
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is Pool)
            {
                Pool pool = obj as Pool;
                return pool != null && pool.GetHashCode() == this.GetHashCode();
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ownerHashCode;
        }
        public int AmountOf(string objectName)
        {
            int result;
            if (this.Instantiations.TryGetValue(objectName, out result))
            {
                return result;
            }
            return 0;
        }
        public static int AmountOf(int hashCode, string objectName)
        {
            Pool pool;
            int result;
            if (Pool.AllPools.TryGetValue(hashCode, out pool) && pool.Instantiations.TryGetValue(objectName, out result))
            {
                return result;
            }
            return 0;
        }
        public bool Contains(string objectName)
        {
            return this.Instantiations.ContainsKey(objectName) || this.PathToID.ContainsKey(objectName);
        }
        public bool Contains(GameObject GO)
        {
            return this.Objects.Contains(GO);
        }
        public bool Contains(Photon.MonoBehaviour mono)
        {
            return !(mono == null) && this.Objects.Contains(mono.gameObject);
        }
        public void Clear()
        {
            foreach (GameObject gameObject in new Queue<GameObject>(this.Objects))
            {
                UnityEngine.Object.DestroyImmediate(gameObject, false);
            }
            Pool.AllObjects.Remove(this.ownerHashCode);
            Pool.AllPools.Remove(this.ownerHashCode);
        }
        public bool TryEnable(out GameObject GO, string path, Vector3 position, Quaternion rotation, short prefix, int instantiationId, int[] viewIDs, object[] instantiationData)
        {
            if (this.PathToName.ContainsKey(path))
            {
                string b = this.PathToName[path];
                int i = 0;
                while (i < this.Objects.Count)
                {
                    GameObject gameObject;
                    GO = (gameObject = this.Objects[i]);
                    if (!(gameObject == null) && !GO.name.IsNullOrEmpty() && !(GO.name != b) && !GO.activeInHierarchy)
                    {
                        PhotonView[] componentsInChildren = GO.GetComponentsInChildren<PhotonView>(true);
                        PhotonView photonView = componentsInChildren[0];
                        if (FengGameManagerMKII.instance != null)
                        {
                            PhotonView pview = FengGameManagerMKII.PView;
                            if (pview.viewID == instantiationId || pview.viewID == photonView.viewID)
                            {
                                GO = null;
                                return true;
                            }
                        }
                        if (componentsInChildren.Length != viewIDs.Length)
                        {
                            throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
                        }
                        int j;
                        for (j = 0; j < viewIDs.Length; j++)
                        {
                            componentsInChildren[j].viewID = 0;
                            componentsInChildren[j].prefix = -1;
                            componentsInChildren[j].prefixBackup = -1;
                            componentsInChildren[j].instantiationId = -1;
                        }
                        j = 0;
                        while (j < viewIDs.Length)
                        {
                            componentsInChildren[j].viewID = viewIDs[j];
                            componentsInChildren[j].prefix = (int)prefix;
                            componentsInChildren[j].instantiationId = instantiationId;
                            i++;
                        }
                        PhotonNetwork.networkingPeer.StoreInstantiationData(instantiationId, instantiationData);
                        Transform transform = GO.transform;
                        if (transform != null)
                        {
                            transform.position = position;
                            transform.rotation = rotation;
                        }
                        GO.SetActive(true);
                        PhotonNetwork.networkingPeer.RemoveInstantiationData(instantiationId);
                        return true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            GO = null;
            return false;
        }
        public bool TryEnable<T>(out T GO, string path, Vector3 position, Quaternion rotation, short prefix, int instantiationId, int[] viewIDs, object[] instantiationData) where T : Component
        {
            if (this.PathToName.ContainsKey(path))
            {
                string b = this.PathToName[path];
                int i = 0;
                while (i < this.Objects.Count)
                {
                    if (!(this.Objects[i] == null) && !this.Objects[i].name.IsNullOrEmpty() && !(this.Objects[i].name != b) && !((GO = this.Objects[i].GetComponent<T>()) == null) && !this.Objects[i].activeInHierarchy)
                    {
                        PhotonView[] componentsInChildren = GO.GetComponentsInChildren<PhotonView>(true);
                        PhotonView photonView = componentsInChildren[0];
                        if (FengGameManagerMKII.instance != null)
                        {
                            PhotonView pview = FengGameManagerMKII.PView;
                            if (pview.viewID == instantiationId || pview.viewID == photonView.viewID)
                            {
                                GO = default(T);
                                return true;
                            }
                        }
                        if (componentsInChildren.Length != viewIDs.Length)
                        {
                            throw new Exception("Error in Instantiation! The resource's PhotonView count is not the same as in incoming data.");
                        }
                        int j;
                        for (j = 0; j < viewIDs.Length; j++)
                        {
                            componentsInChildren[j].viewID = 0;
                            componentsInChildren[j].prefix = -1;
                            componentsInChildren[j].prefixBackup = -1;
                            componentsInChildren[j].instantiationId = -1;
                        }
                        j = 0;
                        while (j < viewIDs.Length)
                        {
                            componentsInChildren[j].viewID = viewIDs[j];
                            componentsInChildren[j].prefix = (int)prefix;
                            componentsInChildren[j].instantiationId = instantiationId;
                            i++;
                        }
                        PhotonNetwork.networkingPeer.StoreInstantiationData(instantiationId, instantiationData);
                        Transform transform = GO.transform;
                        if (transform != null)
                        {
                            transform.position = position;
                            transform.rotation = rotation;
                        }
                        this.Objects[i].SetActive(true);
                        PhotonNetwork.networkingPeer.RemoveInstantiationData(instantiationId);
                        return true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            GO = default(T);
            return false;
        }
        public GameObject Enable(string path, Vector3 position, Quaternion rotation, bool localOnly = false)
        {
            if (localOnly || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (this.PathToName.ContainsKey(path))
                {
                    string b = this.PathToName[path];
                    for (int i = 0; i < this.Objects.Count; i++)
                    {
                        if (!(this.Objects[i].name != b) && !this.Objects[i].activeInHierarchy)
                        {
                            Transform transform = this.Objects[i].transform;
                            if (transform != null)
                            {
                                transform.position = position;
                                transform.rotation = rotation;
                            }
                            this.Objects[i].SetActive(true);
                            return this.Objects[i];
                        }
                    }
                }
                return null;
            }
            return this.PoolEnable(path, position, rotation, 0, null);
        }
        public GameObject Enable(string path, Vector3 position, Quaternion rotation, RaiseEventOptions options)
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (this.PathToName.ContainsKey(path))
                {
                    string b = this.PathToName[path];
                    for (int i = 0; i < this.Objects.Count; i++)
                    {
                        if (!this.Objects[i].activeInHierarchy && !(this.Objects[i].name != b))
                        {
                            Transform transform = this.Objects[i].transform;
                            if (transform != null)
                            {
                                transform.position = position;
                                transform.rotation = rotation;
                            }
                            this.Objects[i].SetActive(true);
                            return this.Objects[i];
                        }
                    }
                }
                return null;
            }
            return this.PoolEventEnable(path, position, rotation, options);
        }
        public T Enable<T>(string path, Vector3 position, Quaternion rotation, bool localOnly = false) where T : Component
        {
            if (localOnly || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (this.PathToName.ContainsKey(path))
                {
                    string b = this.PathToName[path];
                    for (int i = 0; i < this.Objects.Count; i++)
                    {
                        if (!this.Objects[i].activeInHierarchy && !(this.Objects[i].GetComponent<T>() == null) && (!(this.Objects[i] != null) || !(this.Objects[i].name.NullFix() != b)))
                        {
                            Transform transform = this.Objects[i].transform;
                            if (transform != null)
                            {
                                transform.position = position;
                                transform.rotation = rotation;
                            }
                            this.Objects[i].SetActive(true);
                            return this.Objects[i].GetComponent<T>();
                        }
                    }
                }
                return default(T);
            }
            return this.PoolEnable<T>(path, position, rotation, 0, null);
        }
        public T Enable<T>(string path, Vector3 position, Quaternion rotation, RaiseEventOptions options) where T : Component
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (this.PathToName.ContainsKey(path))
                {
                    string b = this.PathToName[path];
                    for (int i = 0; i < this.Objects.Count; i++)
                    {
                        if (!this.Objects[i].activeInHierarchy && !(this.Objects[i].GetComponent<T>() == null) && (!(this.Objects[i] != null) || !(this.Objects[i].name.NullFix() != b)))
                        {
                            Transform transform = this.Objects[i].transform;
                            if (transform != null)
                            {
                                transform.position = position;
                                transform.rotation = rotation;
                            }
                            this.Objects[i].SetActive(true);
                            return this.Objects[i].GetComponent<T>();
                        }
                    }
                }
                return default(T);
            }
            return this.PoolEventEnable<T>(path, position, rotation, options);
        }
        public GameObject Enable(string path, Vector3 position, Quaternion rotation, PhotonView view)
        {
            if (this.PathToName.ContainsKey(path))
            {
                string b = this.PathToName[path];
                for (int i = 0; i < this.Objects.Count; i++)
                {
                    if (!this.Objects[i].activeInHierarchy && (!(this.Objects[i] != null) || !(this.Objects[i].name.NullFix() != b)))
                    {
                        Transform transform = this.Objects[i].transform;
                        if (transform != null)
                        {
                            transform.position = position;
                            transform.rotation = rotation;
                        }
                        PhotonView component = this.Objects[i].GetComponent<PhotonView>();
                        if (component != null && view != null)
                        {
                            component.viewID = view.viewID;
                        }
                        this.Objects[i].SetActive(true);
                        return this.Objects[i];
                    }
                }
            }
            return null;
        }
        public bool Disable(string objectName, bool localOnly = false)
        {
            if (this.NameToID.ContainsKey(objectName))
            {
                string b = this.IDToName[this.NameToID[objectName]];
                for (int i = 0; i < this.Objects.Count; i++)
                {
                    if (this.Objects[i].activeInHierarchy && (!(this.Objects[i] != null) || !(this.Objects[i].name.NullFix() != b)))
                    {
                        if (localOnly || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                        {
                            this.Objects[i].SetActive(false);
                        }
                        else
                        {
                            this.PoolDisable(this.Objects[i]);
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public bool Disable(GameObject GO, bool localOnly = false)
        {
            if (GO == null)
            {
                return false;
            }
            if (this.Objects.Contains(GO))
            {
                if (localOnly || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    GO.SetActive(false);
                }
                else
                {
                    this.PoolDisable(GO);
                }
                return true;
            }
            return false;
        }
        public bool Disable(Component mono, bool localOnly = false)
        {
            if (mono == null)
            {
                return false;
            }
            GameObject gameObject = mono.gameObject;
            if (gameObject == null)
            {
                return false;
            }
            if (this.Objects.Contains(gameObject))
            {
                if (localOnly || IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    this.PoolDisable(gameObject);
                }
                return true;
            }
            return false;
        }
        public static void WriteOutPoolObjects()
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(typeof(EnemyfxIDcontainer).FullName);
            foreach (EnemyfxIDcontainer enemyfxIDcontainer in Resources.LoadAll<EnemyfxIDcontainer>(""))
            {
                queue.Enqueue(enemyfxIDcontainer.name);
            }
            queue.Enqueue(string.Empty);
            queue.Enqueue(typeof(EnemyCheckCollider).FullName);
            foreach (EnemyCheckCollider enemyCheckCollider in Resources.LoadAll<EnemyCheckCollider>(""))
            {
                queue.Enqueue(enemyCheckCollider.name.NullFix());
            }
            queue.Enqueue(string.Empty);
            queue.Enqueue(typeof(UnityEngine.Object).FullName);
            foreach (UnityEngine.Object @object in Resources.LoadAll(""))
            {
                queue.Enqueue(@object.name.NullFix());
            }
            queue.Enqueue(string.Empty);
            queue.Enqueue(typeof(SelfDestroy).FullName);
            foreach (SelfDestroy selfDestroy in Resources.LoadAll<SelfDestroy>(""))
            {
                queue.Enqueue(selfDestroy.name.NullFix());
            }
            queue.Enqueue(string.Empty);
            queue.Enqueue(typeof(UnityEngine.Object).FullName);
            foreach (UnityEngine.Object object2 in Resources.LoadAll("FX"))
            {
                if (object2 is Component)
                {
                    queue.Enqueue(string.Format("{0}\r\n\tComponents:{1}\r\n", object2.name.NullFix(), ((Component)object2).GetComponents<Component>().ToEngFormat("and", (Component c) => c.GetType().FullName)));
                }
                else if (object2 is GameObject)
                {
                    queue.Enqueue(string.Format("{0}\r\n\tComponents:{1}\r\n", object2.name.NullFix(), ((GameObject)object2).GetComponents<Component>().ToEngFormat("and", (Component c) => c.GetType().FullName)));
                }
                else
                {
                    queue.Enqueue(object2.name.NullFix());
                }
            }
            File.WriteAllLines("fx.rtf", queue.ToArray());
        }
      
    }
}