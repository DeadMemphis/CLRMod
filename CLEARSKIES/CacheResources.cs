using System;
using System.Collections.Generic;
using UnityEngine;

namespace CLEARSKIES
{
    // Token: 0x0200000C RID: 12
    internal static class CacheResources
    {
        // Token: 0x0600006A RID: 106 RVA: 0x00012EB8 File Offset: 0x000110B8
        internal static UnityEngine.Object Load(string path)
        {
            path = path.ToUpper();
            if (CacheResources.cacheLocal.ContainsKey(path) && CacheResources.cacheLocal[path] != null)
            {
                return CacheResources.cacheLocal[path];
            }
            return CacheResources.cacheLocal[path] = Resources.Load(path);
        }

        // Token: 0x0600006B RID: 107 RVA: 0x00012F10 File Offset: 0x00011110
        internal static UnityEngine.Object Load(string path, System.Type type)
        {
            string key = path.ToUpper() + type.FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null)
            {
                return CacheResources.cacheGO[key];
            }
            return CacheResources.cacheGO[key] = Resources.Load(path, type);
        }

        // Token: 0x0600006C RID: 108 RVA: 0x00012F70 File Offset: 0x00011170
        internal static T Load<T>(string path) where T : UnityEngine.Object
        {
            string key = path.ToUpper() + typeof(T).FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
            {
                return (T)((object)CacheResources.cacheGO[key]);
            }
            return (T)((object)(CacheResources.cacheGO[key] = Resources.Load<T>(path)));
        }

        // Token: 0x0600006D RID: 109 RVA: 0x00012FFC File Offset: 0x000111FC
        internal static bool Load<T>(string path, out T value) where T : UnityEngine.Object
        {
            string key = path.ToUpper() + typeof(T).FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
            {
                value = (T)((object)CacheResources.cacheGO[key]);
                return value != null;
            }
            CacheResources.cacheGO[key] = (value = Resources.Load<T>(path));
            return value != null;
        }

        // Token: 0x0600006E RID: 110 RVA: 0x000130B0 File Offset: 0x000112B0
        internal static UnityEngine.Object RCLoad(string path)
        {
            string key = "RC" + path.ToUpper();
            if (CacheResources.cacheLocal.ContainsKey(key) && CacheResources.cacheLocal[key] != null)
            {
                return CacheResources.cacheLocal[key];
            }
            return CacheResources.cacheLocal[key] = FengGameManagerMKII.RCassets.Load(path);
        }

        // Token: 0x0600006F RID: 111 RVA: 0x00013114 File Offset: 0x00011314
        internal static UnityEngine.Object RCLoad(string path, System.Type type)
        {
            string key = "RC" + path.ToUpper() + type.FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null)
            {
                return CacheResources.cacheGO[key];
            }
            return CacheResources.cacheGO[key] = FengGameManagerMKII.RCassets.Load(path, type);
        }

        // Token: 0x06000070 RID: 112 RVA: 0x00013180 File Offset: 0x00011380
        internal static T RCLoad<T>(string path) where T : UnityEngine.Object
        {
            string key = "RC" + path.ToUpper() + typeof(T).FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
            {
                return (T)((object)CacheResources.cacheGO[key]);
            }
            return (T)((object)(CacheResources.cacheGO[key] = (T)((object)FengGameManagerMKII.RCassets.Load(path, typeof(T)))));
        }

        // Token: 0x06000071 RID: 113 RVA: 0x00013224 File Offset: 0x00011424
        internal static bool RCLoad<T>(string path, out T value) where T : UnityEngine.Object
        {
            string key = "RC" + path.ToUpper() + typeof(T).FullName;
            if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
            {
                value = (T)((object)CacheResources.cacheGO[key]);
                return value != null;
            }
            CacheResources.cacheGO[key] = (value = (T)((object)FengGameManagerMKII.RCassets.Load(path, typeof(T))));
            return value != null;
        }

        // Token: 0x06000072 RID: 114 RVA: 0x000132F0 File Offset: 0x000114F0
        //internal static UnityEngine.Object RSLoad(string path)
        //{
        //    string key = "RS" + path.ToUpper();
        //    if (CacheResources.cacheLocal.ContainsKey(key) && CacheResources.cacheLocal[key] != null)
        //    {
        //        return CacheResources.cacheLocal[key];
        //    }
        //    return CacheResources.cacheLocal[key] = FengGameManagerMKII.RSAssets.Load(path);
        //}

        //// Token: 0x06000073 RID: 115 RVA: 0x00013354 File Offset: 0x00011554
        //internal static UnityEngine.Object RSLoad(string path, System.Type type)
        //{
        //    string key = "RS" + path.ToUpper() + type.FullName;
        //    if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null)
        //    {
        //        return CacheResources.cacheGO[key];
        //    }
        //    return CacheResources.cacheGO[key] = FengGameManagerMKII.RSAssets.Load(path, type);
        //}

        //// Token: 0x06000074 RID: 116 RVA: 0x000133C0 File Offset: 0x000115C0
        //internal static T RSLoad<T>(string path) where T : UnityEngine.Object
        //{
        //    string key = "RS" + path.ToUpper() + typeof(T).FullName;
        //    if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
        //    {
        //        return (T)((object)CacheResources.cacheGO[key]);
        //    }
        //    return (T)((object)(CacheResources.cacheGO[key] = (T)((object)FengGameManagerMKII.RSAssets.Load(path, typeof(T)))));
        //}

        //// Token: 0x06000075 RID: 117 RVA: 0x00013464 File Offset: 0x00011664
        //internal static bool RSLoad<T>(string path, out T value) where T : UnityEngine.Object
        //{
        //    string key = "RS" + path.ToUpper() + typeof(T).FullName;
        //    if (CacheResources.cacheGO.ContainsKey(key) && CacheResources.cacheGO[key] != null && CacheResources.cacheGO[key] is T)
        //    {
        //        value = (T)((object)CacheResources.cacheGO[key]);
        //        return value != null;
        //    }
        //    CacheResources.cacheGO[key] = (value = (T)((object)FengGameManagerMKII.RSAssets.Load(path, typeof(T))));
        //    return value != null;
        //}

        // Token: 0x04000033 RID: 51
        private static System.Collections.Generic.Dictionary<string, UnityEngine.Object> cacheLocal = new System.Collections.Generic.Dictionary<string, UnityEngine.Object>
        {
            {
                "ROPE",
                Resources.Load("ROPE")
            },
            {
                "HOOK",
                Resources.Load("HOOK")
            },
            {
                "HITMEAT",
                Resources.Load("HITMEAT")
            },
            {
                "REDCROSS",
                Resources.Load("REDCROSS")
            },
            {
                "REDCROSS1",
                Resources.Load("REDCROSS1")
            },
            {
                "UI_IN_GAME",
                Resources.Load("UI_IN_GAME")
            },
            {
                "FLASHLIGHT",
                Resources.Load("FLASHLIGHT")
            },
            {
                "AOT_SUPPLY",
                Resources.Load("AOT_SUPPLY")
            },
            {
                "FX/THUNDER",
                Resources.Load("FX/THUNDER")
            },
            {
                "TITAN_EREN",
                Resources.Load("TITAN_EREN")
            },
            {
                "UI/KILLINFO",
                Resources.Load("UI/KILLINFO")
            },
            {
                "FEMALE_TITAN",
                Resources.Load("FEMALE_TITAN")
            },
            {
                "TITANNAPEMEAT",
                Resources.Load("TITANNAPEMEAT")
            },
            {
                "FX/FXTITANDIE",
                Resources.Load("FX/FXTITANDIE")
            },
            {
                "FX/FXTITANDIE1",
                Resources.Load("FX/FXTITANDIE1")
            },
            {
                "COLOSSAL_TITAN",
                Resources.Load("COLOSSAL_TITAN")
            },
            {
                "MAINCAMERA_MONO",
                Resources.Load("MAINCAMERA_MONO")
            }
        };

        // Token: 0x04000034 RID: 52
        private static System.Collections.Generic.Dictionary<string, UnityEngine.Object> cacheGO = new System.Collections.Generic.Dictionary<string, UnityEngine.Object>
        {
            {
                "HOOKUnityEngine.GameObject",
                Resources.Load<GameObject>("HOOK")
            },
            {
                "ROPEUnityEngine.GameObject",
                Resources.Load<GameObject>("ROPE")
            },
            {
                "HORSEUnityEngine.GameObject",
                Resources.Load<GameObject>("HORSE")
            },
            {
                "HITMEATUnityEngine.GameObject",
                Resources.Load<GameObject>("HITMEAT")
            },
            {
                "HITMEAT2UnityEngine.GameObject",
                Resources.Load<GameObject>("HITMEAT2")
            },
            {
                "REDCROSSUnityEngine.GameObject",
                Resources.Load<GameObject>("REDCROSS")
            },
            {
                "REDCROSS1UnityEngine.GameObject",
                Resources.Load<GameObject>("REDCROSS1")
            },
            {
                "TITAN_ERENUnityEngine.GameObject",
                Resources.Load<GameObject>("TITAN_EREN")
            },
            {
                "FX/THUNDERUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/THUNDER")
            },
            {
                "UI_IN_GAMEUIReferArray",
                Resources.Load<UIReferArray>("UI_IN_GAME")
            },
            {
                "FEMALE_TITANUnityEngine.GameObject",
                Resources.Load<GameObject>("FEMALE_TITAN")
            },
            {
                "FX/ROCKTHROWUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/ROCKTHROW")
            },
            {
                "FX/JUSTSMOKEUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/JUSTSMOKE")
            },
            {
                "FX/FXTITANDIEUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/FXTITANDIE")
            },
            {
                "FX/FXTITANDIE1UnityEngine.GameObject",
                Resources.Load<GameObject>("FX/FXTITANDIE1")
            },
            {
                "FX/BOOST_SMOKEUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/BOOST_SMOKE")
            },
            {
                "COLOSSAL_TITANUnityEngine.GameObject",
                Resources.Load<GameObject>("COLOSSAL_TITAN")
            },
            {
                "FX/FLAREBULLETUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/FLAREBULLET")
            },
            {
                "UI/KILLINFOKillInfoComponent",
                Resources.Load<KillInfoComponent>("UI/KILLINFO")
            },
            {
                "FX/FXTITANSPAWNUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/FXTITANSPAWN")
            },
            {
                "TITAN_EREN_TROSTUnityEngine.GameObject",
                Resources.Load<GameObject>("TITAN_EREN_TROST")
            },
            {
                "FX/COLOSSAL_STEAMUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/COLOSSAL_STEAM")
            },
            {
                "FX/COLOSSAL_STEAM_DMGUnityEngine.GameObject",
                Resources.Load<GameObject>("FX/COLOSSAL_STEAM_DMG")
            }
        };
    }
}

