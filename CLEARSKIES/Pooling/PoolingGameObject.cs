using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine; 

namespace CLEARSKIES.Pooling
{
    public class PoolingGameObject : MonoBehaviour
    {
        #region Data
        List<PoolGameObject> objects;
        Transform objectsParent;
        #endregion

        #region Interface
        public void Initialize(int count, PoolGameObject sample, Transform objects_parent)
        {
            objects = new List<PoolGameObject>();
            objectsParent = objects_parent;
            for (int i = 0; i < count; i++)
            {
                AddObject(sample, objects_parent);
            }
        }

        public PoolGameObject GetObject()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].gameObject.activeInHierarchy == false)
                {
                    return objects[i];
                }
            }
            AddObject(objects[0], objectsParent);
            return objects[objects.Count - 1];
        }
        #endregion

        #region Methods
        void AddObject(PoolGameObject sample, Transform objects_parent)
        {
            GameObject temp;
            temp = (GameObject)GameObject.Instantiate(sample.gameObject);
            temp.name = sample.name;
            temp.transform.SetParent(objects_parent);
            objects.Add(temp.GetComponent<PoolGameObject>());
            if (temp.GetComponent<Animator>())
                temp.GetComponent<Animator>().StartPlayback();
            temp.SetActive(false);
        }
        #endregion
    }
}
