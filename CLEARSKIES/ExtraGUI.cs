using System;
using UnityEngine;

namespace CLEARSKIES
{
    // Token: 0x020000DD RID: 221
    internal class ExtraGUI : MonoBehaviour
    {
        private static bool isTitanNotDead(Animation baseA)
        {
            return baseA != null && !baseA.IsPlaying("sit_die") && !baseA.IsPlaying("crawler_die") && !baseA.IsPlaying("die_front") && !baseA.IsPlaying("die_ground") && !baseA.IsPlaying("die_back");
        }

        public void OnGUI()
        {
            if (enable)
            {
                
                //GUI.DrawTexture(new Rect(-5f, -5f, 210f, 40f), FengGameManagerMKII.instance.textureBackgroundBlack);
                //GUI.DrawTexture(new Rect((float)Screen.width - 200f, (float)Screen.height - 75f, 195f, 70f), FengGameManagerMKII.instance.textureBackgroundBlack);

                //GUI.Box(new Rect(-5f, -5f, 210f, 40f), string.Empty);
                //GUI.Box(new Rect((float)Screen.width - 200f, (float)Screen.height - 75f, 195f, 70f), string.Empty);
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP /*&& (bool)FengGameManagerMKII.settings[115]*/ && IN_GAME_MAIN_CAMERA.mainHERO != null && IN_GAME_MAIN_CAMERA.main_objectR != null)
                {
                    GUI.backgroundColor = Color.black;
                    rect = new Rect((float)Screen.width - 200f, (float)Screen.height - 95f, 195f, 70f);
                    GUI.Box(rect, string.Empty);
                    GUILayout.BeginArea(rect);
                    //GUI.DrawTexture(new Rect(5f, 5f, 185f, 27.5f), FengGameManagerMKII.instance.textureBackgroundBlack);
                    //GUI.DrawTexture(new Rect(5f, 37.5f, 185f, 27.5f), FengGameManagerMKII.instance.textureBackgroundBlack);
                    float num = IN_GAME_MAIN_CAMERA.main_objectR.velocity.magnitude.RoundTo(2);
                    GUILayout.BeginArea(new Rect(10f, 7f, 181f, 33.5f));
                    GUILayout.Label("<color=yellow>Speed:\t" + num.ToString() + "</color>", new GUILayoutOption[0]);
                    GUILayout.EndArea();
                    GUILayout.BeginArea(new Rect(10f, 39.5f, 181f, 33.5f));
                    GUILayout.Label("<color=yellow>Damage:\t" + (IN_GAME_MAIN_CAMERA.mainHERO.useGun ? Mathf.Max(10, (int)(num * 10f * 0.4f)) : Mathf.Max(10, (int)(num * 10f))).ToString() + "</color>", new GUILayoutOption[0]);
                    GUILayout.EndArea();
                    GUILayout.EndArea();
                }
            }
            
            //Rect rect = new Rect(-5f, -5f, 210f, 40f);
            //if (HERO.radarTimer > 0f)
            //{
            //    Rect rect2 = new Rect(0f, 0f, 200f, 30f);
            //    foreach (GameObject gameObject in FengGameManagerMKII.instance.titans)
            //    {
            //        Transform transform;
            //        if (IN_GAME_MAIN_CAMERA.main_objectT != null && ExtraGUI.isTitanNotDead(gameObject.animation) && !((transform = gameObject.transform) == null))
            //        {
            //            this.nullable = new Vector3?(Camera.main.WorldToScreenPoint(transform.position));
            //            if (this.nullable != null)
            //            {
            //                this.screenPoint = this.nullable.Value;
            //                if (this.screenPoint.z > 0f)
            //                {
            //                    this.label = GUIUtility.ScreenToGUIPoint(this.screenPoint);
            //                    this.label.y = (float)Screen.height - (this.label.y + 1f);
            //                    rect.xMin = this.label.x - 5f;
            //                    rect.yMin = this.label.y - 5f;
            //                    GUI.Box(rect, string.Empty);
            //                    rect2.xMin = this.label.x;
            //                    rect2.yMin = this.label.y;
            //                    GUI.Box(rect2, gameObject.name.NullFix().ToRGBA() + " : " + Mathf.Round(Vector3.Distance(transform.position, IN_GAME_MAIN_CAMERA.main_objectT.position)).ToString() + " ft");
            //                }
            //            }
            //        }
            //    }
            //}
           
        }
        Rect rect;
        public static bool enable = false;
        //private Vector3? nullable;
        //private Vector3 screenPoint;
        //private Vector2 label;
    }
}
