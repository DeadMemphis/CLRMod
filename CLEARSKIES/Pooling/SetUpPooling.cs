using System;
using System.Collections.Generic;
using UnityEngine;


namespace CLEARSKIES.Pooling
{
    class SetUpPooling : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField]
        private PoolManager.PoolPart[] pools = new PoolManager.PoolPart[2]; //Default
        #endregion

        #region Methods
        void OnValidate()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].name = pools[i].prefab.name;
            }
        }

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            PoolManager.Initialize(pools);
        }
        #endregion
    }
}
