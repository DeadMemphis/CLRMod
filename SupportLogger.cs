using System;
using UnityEngine;

public class SupportLogger : MonoBehaviour
{
    public bool LogTrafficStats = true;

    public void Start()
    {
        if (CLEARSKIES.CacheGameObject.Find("PunSupportLogger") == null)
        {
            GameObject target = new GameObject("PunSupportLogger");
            UnityEngine.Object.DontDestroyOnLoad(target);
            target.AddComponent<SupportLogging>().LogTrafficStats = this.LogTrafficStats;
        }
    }
}

