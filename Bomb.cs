using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using CLEARSKIES;

public class Bomb : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool disabled;
    public GameObject myExplosion;
    public float SmoothingDelay = 10f;

    private Transform baseT;
    private Rigidbody baseR;
    private PhotonView basePV;
    
    private bool isMine
    {
        get
        {
            if (this.basePV == null)
            {
                this.basePV = base.photonView;
                //this.basePV = base.GetComponent<PhotonView>(); //wutahell
            }
            return this.basePV != null && this.basePV.isMine;
        }
    }

    public void Awake()
    {
        baseT = base.transform;
        baseR = base.rigidbody;
        basePV = base.photonView;
    }

    public void Start()
    {
        if (basePV == null)
        {
            basePV = base.photonView;
        }
        if (basePV != null)
        {
            float Red;
            float Green;
            float Blue;
            basePV.observed = this;
            this.correctPlayerPos = base.transform.position;
            this.correctPlayerRot = Quaternion.identity;
            PhotonPlayer owner = base.photonView.owner;
            ParticleSystem particle = base.GetComponent<ParticleSystem>();
            if (GameSettings.teamMode > 0)
            {
                int team = owner.RCteam;
                if (team == 1)
                {
                    particle.startColor = Color.cyan;
                }
                else if (team == 2)
                {
                    particle.startColor = Color.magenta;
                }
                else
                {
                    Red = owner.RCBombR;
                    Green = owner.RCBombG;
                    Blue = owner.RCBombB; 
                    particle.startColor = new Color(Red, Green, Blue, 1f);
                }
            }
            else
            {
                Red = owner.RCBombR;
                Green = owner.RCBombG;
                Blue = owner.RCBombB;
                particle.startColor = new Color(Red, Green, Blue, 1f);
            }
        }
    }

    public void destroyMe()
    {
        if (isMine)
        {
            if (this.myExplosion != null)
            {
                PhotonNetwork.Destroy(this.myExplosion);
            }
            PhotonNetwork.Destroy(base.gameObject);
        }
    }

    public void Explode(float radius)
    {
        this.disabled = true;
        baseR.velocity = Vector3.zero;
        Vector3 position = baseT.position;
        this.myExplosion = PhotonNetwork.Instantiate("RCAsset/BombExplodeMain", position, Quaternion.Euler(0f, 0f, 0f), 0);
        foreach (HERO hero in FengGameManagerMKII.heroes)
        {
            if(hero != null)
            {
                PhotonView pview = hero.photonView;
                if (((Vector3.Distance(hero.transform.position, position) < radius) && pview != null && !pview.isMine) && !hero.bombImmune)
                {
                    PhotonPlayer owner = pview.owner;
                    if (((GameSettings.teamMode > 0) && (PhotonNetwork.player.customProperties[PhotonPlayerProperty.RCteam] != null)) && (owner.customProperties[PhotonPlayerProperty.RCteam] != null))
                    {
                        int rcteam = PhotonNetwork.player.RCteam;
                        int rcteam2 = owner.RCteam;
                        if ((rcteam == 0) || (rcteam != rcteam2))
                        {
                            hero.markDie();
                            pview.RPC("netDie2", PhotonTargets.All, new object[] { -1, "bemb"/*RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name])*/ + " " });
                            FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                        }
                    }
                    else
                    {
                        hero.markDie();
                        pview.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "bemb"/*RCextensions.returnStringFromObject(PhotonNetwork.player.customProperties[PhotonPlayerProperty.name])*/ + " " });
                        FengGameManagerMKII.instance.playerKillInfoUpdate(PhotonNetwork.player, 0);
                        PhotonNetwork.Instantiate("FX/FXtitanDie", hero.transform.position, Quaternion.Euler(-90f, 0f, 0f), 0);
                        //PhotonNetwork.Instantiate("FX/boom6", gameObject.GetComponent<HERO>().transform.position, gameObject.GetComponent<HERO>().transform.rotation, 0);
                    }
                }
            }
        }
        //base.StartCoroutine(this.WaitAndFade(1.5f));
        Yield.Begin(new WaitForSeconds(1.5f), new Action (() => 
        {
            UnityEngine.MonoBehaviour.print("==> test yield instructions");
            PhotonNetwork.Destroy(this.myExplosion);
            PhotonNetwork.Destroy(this.gameObject);
        }));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(baseT.position);
            stream.SendNext(baseT.rotation);
            stream.SendNext(baseR.velocity);
        }
        else
        {
            this.correctPlayerPos = (Vector3) stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
            this.correctPlayerVelocity = (Vector3) stream.ReceiveNext();
        }
    }

    public void Update()
    {
        if (!(this.disabled || isMine))
        {
            if (this.baseT == null)
            {
                this.baseT = base.transform;
            }
            if (this.baseR == null)
            {
                this.baseR = base.rigidbody;
            }
            if (this.baseT != null)
            {
                baseT.position = Vector3.Lerp(baseT.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
                baseT.rotation = Quaternion.Lerp(baseT.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
            }
            if (this.baseR != null)
                baseR.velocity = this.correctPlayerVelocity;
        }
    }

    private IEnumerator WaitAndFade(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(this.myExplosion);
        PhotonNetwork.Destroy(this.gameObject);
    }

}

