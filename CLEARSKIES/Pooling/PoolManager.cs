﻿using System;
using UnityEngine;

namespace CLEARSKIES.Pooling
{
    public class PoolManager
    {
        private static PoolPart[] pools;
        private static GameObject objectsParent;

        [Serializable]
        public struct PoolPart
        {
            public string name;
            public PoolGameObject prefab;
            public int count;
            public PoolingGameObject ferula;
        }

        public static void Initialize(PoolPart[] newPools)
        {
            pools = newPools;
            objectsParent = new GameObject();
            objectsParent.name = "Pool";
            for (int i = 0; i < pools.Length; i++)
            {
                if (pools[i].prefab != null)
                {
                    pools[i].ferula = new PoolingGameObject();
                    pools[i].ferula.Initialize(pools[i].count, pools[i].prefab, objectsParent.transform);
                }
            }
        }


        public static GameObject GetObject(string name, Vector3 position, Quaternion rotation)
        {
            GameObject result = null;
            if (pools != null)
            {
                for (int i = 0; i < pools.Length; i++)
                {
                    if (string.Compare(pools[i].name, name) == 0)
                    {
                        result = pools[i].ferula.GetObject().gameObject;
                        result.transform.position = position;
                        result.transform.rotation = rotation;
                        result.SetActive(true);
                        return result;
                    }
                }
            }
            return result;
        }
    }
}
