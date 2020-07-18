using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CLEARSKIES.Pooling
{
    public class PoolGameObject : MonoBehaviour
    {
        #region Interface
        public void ReturnToPool()
        {
            base.gameObject.SetActive(false);
        }
        #endregion
    }
}
