using System;
using UnityEngine;

namespace CLEARSKIES
{
    internal static class RespawnPositions
    {
        public static void Dispose()
        {
            RespawnPositions.titanpos = null;
        }


        public static Vector3 RandomTitanPos
        {
            get
            {
                if (RespawnPositions.titanpos != null)
                {
                    return RespawnPositions.titanpos[UnityEngine.Random.Range(0, RespawnPositions.titanpos.Length)];
                }
                GameObject[] array = GameObject.FindGameObjectsWithTag("titanRespawn");
                if (array != null)
                {
                    RespawnPositions.titanpos = new Vector3[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        RespawnPositions.titanpos[i] = array[i].transform.position;
                    }
                }
                if (RespawnPositions.titanpos != null)
                {
                    return RespawnPositions.titanpos[UnityEngine.Random.Range(0, RespawnPositions.titanpos.Length)];
                }
                return Vector3.zero;
            }
        }

        public static Vector3[] TitanPositions
        {
            get
            {
                if (RespawnPositions.titanpos != null)
                {
                    return RespawnPositions.titanpos;
                }
                GameObject[] array = GameObject.FindGameObjectsWithTag("titanRespawn");
                if (array != null)
                {
                    RespawnPositions.titanpos = new Vector3[array.Length];
                    for (int i = 0; i < array.Length; i++)
                    {
                        RespawnPositions.titanpos[i] = array[i].transform.position;
                    }
                    return RespawnPositions.titanpos;
                }
                return new Vector3[0];
            }
        }

        private static Vector3[] titanpos;
    }
}