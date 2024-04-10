using System;
using System.Collections;
using Photon;
using UnityEngine;
using Utility;

public class BombExplode : Photon.MonoBehaviour
{
    public static float _sizeMultiplier = 1.1f;
    private float _minimumLuminance = 0.4f;

    private IEnumerator WaitAndDestroyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(base.gameObject);
        yield break;
    }

    public void Awake()
    {
        if (base.photonView != null)
        {
            PhotonPlayer owner = base.photonView.owner;
            float diameter;
            diameter = Mathf.Clamp(RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombRadius]), 20f, 60) * 2f/* * BombExplode._sizeMultiplier*/;

            ParticleSystem component = base.GetComponent<ParticleSystem>();
            if (GameSettings.UseOldBombEffect > 0)
            {
                component.Stop();
                component.Clear();
                component = base.transform.Find("OldExplodeEffect").GetComponent<ParticleSystem>();
                component.gameObject.SetActive(true);
            }
            if (GameSettings.ShowBombColor > 0)
                component.startColor = BombUtil.GetBombColor(owner, 0.5f);

            owner.BombHasExploded = true; //make sure others explode their bombs before sending their kills 
            component.startSize = diameter;
            if (base.photonView.isMine)
            {
                base.StartCoroutine(this.WaitAndDestroyCoroutine(1.5f));
            }
            else
            {
                // change bomb color here
                if (ThymesUtils.RelativeLuminance(BombUtil.GetBombColor(owner, 1f)) < _minimumLuminance)
                {
                    component.startColor = Color.white;
                }
            }
        }
    }

    //    public GameObject myExplosion;
    //    static Vector3 bombPos = new Vector3(0f, 0f, 0f);

    //    public void Start()
    //    {
    //        if (base.photonView != null)
    //        {
    //            float num2;
    //            float num3;
    //            float num4;
    //            float num5;
    //            PhotonPlayer owner = base.photonView.owner;
    //            if (GameSettings.teamMode > 0)
    //            {
    //                int num = RCextensions.returnIntFromObject(owner.customProperties[PhotonPlayerProperty.RCteam]);
    //                if (num == 1)
    //                {
    //                    base.GetComponent<ParticleSystem>().startColor = Color.cyan;
    //                }
    //                else if (num == 2)
    //                {
    //                    base.GetComponent<ParticleSystem>().startColor = Color.magenta;
    //                }
    //                else
    //                {
    //                    num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
    //                    num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
    //                    num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
    //                    num5 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombA]);
    //                    num5 = Mathf.Max(0.5f, num5);
    //                    base.GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, num5);
    //                }
    //            }
    //            else
    //            {
    //                num2 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombR]);
    //                num3 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombG]);
    //                num4 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombB]);
    //                num5 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombA]);
    //                num5 = Mathf.Max(0.5f, num5);
    //                base.GetComponent<ParticleSystem>().startColor = new Color(num2, num3, num4, num5);
    //            }
    //            float num6 = RCextensions.returnFloatFromObject(owner.customProperties[PhotonPlayerProperty.RCBombRadius]) * 2f;
    //            num6 = Mathf.Clamp(num6, 40f, 120f);
    //            base.GetComponent<ParticleSystem>().startSize = num6;

    //            Vector3 position = base.transform.position;
    //            if (bombPos.x == position.x || bombPos.z == position.z) InRoomChat.Write("<color=red>AUTOEXPLODE 100% FROM </color>:" + owner.customProperties[PhotonPlayerProperty.name].ToString().hexColor() + "(" + owner.ID + "). 2 explosions in same position" + position);
    //            bombPos = position;

    //        }
    //    }
}

