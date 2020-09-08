using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLEARSKIES
{
    public static class CacheGameObject
    {
        private static Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();
        private static Dictionary<string, Component> cacheType = new Dictionary<string, Component>();

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
    }
}
