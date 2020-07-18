using System;
using System.Collections.Generic;
using UnityEngine;

namespace BRM
{
    // Token: 0x02000078 RID: 120
    public static class CacheGameObject
    {
        // Token: 0x060003C8 RID: 968 RVA: 0x00027FD8 File Offset: 0x000261D8
        public static T Find<T>(string name) where T : Component
        {
            string key = name + typeof(T).FullName;
            if (CacheGameObject.cacheType.ContainsKey(key))
            {
                Component component = CacheGameObject.cacheType[key];
                if (component != null)
                {
                    T t = component as T;
                    if (t != null)
                    {
                        return t;
                    }
                    return (T)((object)(CacheGameObject.cacheType[key] = component.GetComponent<T>()));
                }
            }
            GameObject gameObject = CacheGameObject.Find(name);
            if (gameObject != null)
            {
                return (T)((object)(CacheGameObject.cacheType[key] = gameObject.GetComponent<T>()));
            }
            if (gameObject = GameObject.Find(name))
            {
                return (T)((object)(CacheGameObject.cacheType[key] = gameObject.GetComponent<T>()));
            }
            return default(T);
        }

        // Token: 0x060003C9 RID: 969 RVA: 0x000280C8 File Offset: 0x000262C8
        public static GameObject Find(string name)
        {
            string text = name.ToLower().Trim();
            string key;
            switch (key = text)
            {
                case "maincamera":
                    if (CacheGameObject.cache.ContainsKey(name) && CacheGameObject.cache[name] != null)
                    {
                        return CacheGameObject.cache[name];
                    }
                    return CacheGameObject.cache[name] = GameObject.Find(name);
                case "aottg_hero1":
                case "aottg_hero1(clone)":
                case "colossal_titan":
                case "femaletitan":
                case "female_titan":
                case "crawler":
                case "punk":
                case "abberant":
                case "jumper":
                case "titan":
                case "tree":
                case "cube001":
                    return GameObject.Find(name);
            }
            GameObject gameObject;
            if (CacheGameObject.cache.ContainsKey(name) && (gameObject = CacheGameObject.cache[name]) && (gameObject.activeInHierarchy || text.StartsWith("ui") || text.StartsWith("label") || text.StartsWith("ngui")))
            {
                return gameObject;
            }
            if (gameObject = GameObject.Find(name))
            {
                return CacheGameObject.cache[name] = gameObject;
            }
            return gameObject;
        }

        // Token: 0x0400030F RID: 783
        private static System.Collections.Generic.Dictionary<string, GameObject> cache = new System.Collections.Generic.Dictionary<string, GameObject>();

        // Token: 0x04000310 RID: 784
        private static System.Collections.Generic.Dictionary<string, Component> cacheType = new System.Collections.Generic.Dictionary<string, Component>();
    }
}
