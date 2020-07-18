using Photon;
using System;
using UnityEngine;

public class SmoothSyncMovement : Photon.MonoBehaviour
{
    //private Vector3 correctCameraPos;
    public Quaternion correctCameraRot;
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool disabled;
    public bool noVelocity;
    public bool PhotonCamera;
    public float SmoothingDelay = 5f;

    private Transform baseT;
    private Rigidbody baseR;
    private PhotonView basePV;
    private bool isMine
    {
        get
        {
            if (this.basePV == null)
            {
                return (this.basePV = base.GetComponent<PhotonView>()) != null && this.basePV.isMine;
            }
            return this.basePV != null && this.basePV.isMine;
        }
    }

    public void Awake()
    {
        this.baseT = base.transform;
        this.baseR = base.rigidbody;
        this.basePV = base.GetComponent<PhotonView>();
        if (this.basePV == null || this.basePV.observed != this)
        {
            Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
        }
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            base.enabled = false;
        }
        if (this.baseT == null)
        {
            this.baseT = base.transform;
        }
        if (this.baseR == null)
        {
            this.baseR = base.rigidbody;
        }
        if (this.basePV == null)
        {
            this.basePV = base.GetComponent<PhotonView>();
        }
        this.correctPlayerPos = base.transform.position;
        this.correctPlayerRot = base.transform.rotation;
        if (base.rigidbody == null)
        {
            this.noVelocity = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.baseT.position);
            stream.SendNext(this.baseT.rotation);
            if (!this.noVelocity)
            {
                stream.SendNext(this.baseR.velocity);
            }
            if (this.PhotonCamera)
            {
                stream.SendNext(Camera.main.transform.rotation);
            }
        }
        else
        {
            this.correctPlayerPos = (Vector3) stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion) stream.ReceiveNext();
            if (!this.noVelocity)
            {
                this.correctPlayerVelocity = (Vector3) stream.ReceiveNext();
            }
            if (this.PhotonCamera)
            {
                this.correctCameraRot = (Quaternion) stream.ReceiveNext();
            }
        }
    }

    public void Update()
    {
        if (!this.disabled && IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE && this.basePV != null && !this.basePV.isMine)
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
                if (this.correctPlayerPos != this.baseT.position)
                {
                    this.baseT.position = Vector3.Lerp(this.baseT.position, this.correctPlayerPos, 5f * Time.deltaTime);
                }
                if (this.correctPlayerRot != this.baseT.rotation)
                {
                    this.baseT.rotation = Quaternion.Lerp(this.baseT.rotation, this.correctPlayerRot, 5f * Time.deltaTime);
                }
            }
            if (this.baseR != null)
            {
                this.baseR.velocity = this.correctPlayerVelocity;
            }
        }
    }
}

