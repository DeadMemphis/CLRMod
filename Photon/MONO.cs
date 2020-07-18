using System;
using Photon;
using UnityEngine;

public abstract class MONO : Photon.MonoBehaviour
{
    public readonly SPECIES species;
    public Animation baseA;
    public Transform baseT;
    public Rigidbody baseR;
    public GameObject baseG;
    public PhotonView basePV;
    public Transform Amarture;
    public Transform Controller_Body;
    public Transform Core;
    public Transform head;
    public Transform neck;
    public Transform thigh_R;
    public Transform thigh_L;
    public Transform foot_R;
    public Transform foot_L;
    public Transform chest;
    public Transform shoulder_R;
    public Transform shoulder_L;
    public Transform hip;
    public Transform spine;
    public Transform upper_arm_L;
    public Transform upper_arm_R;
    public Transform forearm_L;
    public Transform forearm_R;
    public Transform hand_L;
    public Transform hand_R;
    public Transform hand_R_001;
    public Transform hand_L_001;
    public Transform AABB;
    public SphereCollider hand_R_001Sphere;
    public SphereCollider hand_L_001Sphere;
    public Transform hand_R_001SphereT;
    public Transform hand_L_001SphereT;
    public Transform snd_titan_foot;
    public Transform snd_eren_foot;
    public AudioSource snd_titan_footAudio;
    public AudioSource snd_eren_footAudio;
    //titon
    public Transform ap_front_ground;
    public Transform chkAeLeft;
    public Transform chkAeLLeft;
    public Transform chkAeRight;
    public Transform chkAeLRight;

    protected MONO(SPECIES species)
    {
        this.species = species;
    }
    protected void CacheComponnents()
    {
        this.baseA = base.GetComponent<Animation>();
        this.baseT = base.GetComponent<Transform>();
        this.baseR = base.GetComponent<Rigidbody>();
        this.basePV = base.GetComponent<PhotonView>();
        this.baseG = base.gameObject;
    }
    public void CacheTransforms()
    {
        if (this.baseT == null && (this.baseT = base.GetComponent<Transform>()) == null)
        {
            return;
        }
        this.Amarture = this.baseT.Find("Amarture");
        string str;
        if (this.species == SPECIES.Hero || this.species == SPECIES.Horse)
        {
            this.AABB = null;
            this.Core = null;
            str = "Amarture/Controller_Body/";
            this.Controller_Body = this.baseT.Find("Amarture/Controller_Body");
        }
        else
        {
            str = "Amarture/Core/Controller_Body/";
            this.AABB = this.baseT.Find("AABB");
            this.Core = this.baseT.Find("Amarture/Core");
            if (this.species == SPECIES.TitanEren)
            {
                if (this.snd_eren_foot = this.baseT.Find("snd_eren_foot"))
                {
                    this.snd_eren_footAudio = this.snd_eren_foot.GetComponent<AudioSource>();
                }
            }
            else if (this.snd_titan_foot = this.baseT.Find("snd_titan_foot"))
            {
                this.snd_titan_footAudio = this.snd_titan_foot.GetComponent<AudioSource>();
            }
            this.Controller_Body = this.baseT.Find("Amarture/Core/Controller_Body");
        }
        this.head = this.baseT.Find(str + "hip/spine/chest/neck/head");
        this.neck = this.baseT.Find(str + "hip/spine/chest/neck");
        this.thigh_L = this.baseT.Find(str + "hip/thigh_L");
        this.thigh_R = this.baseT.Find(str + "hip/thigh_R");
        this.foot_L = this.baseT.Find(str + "hip/thigh_L/shin_L/foot_L");
        this.foot_R = this.baseT.Find(str + "hip/thigh_R/shin_R/foot_R");
        this.chest = this.baseT.Find(str + "hip/spine/chest");
        this.shoulder_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L");
        this.shoulder_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R");
        this.hip = this.baseT.Find(str + "hip");
        this.spine = this.baseT.Find(str + "hip/spine");
        this.upper_arm_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L");
        this.upper_arm_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R");
        this.forearm_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L");
        this.forearm_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        this.hand_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
        this.hand_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
        if ((this.hand_L_001 = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001")) && (this.hand_L_001Sphere = this.hand_L_001.GetComponent<SphereCollider>()))
        {
            this.hand_L_001SphereT = this.hand_L_001Sphere.transform;
        }
        if ((this.hand_R_001 = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001")) && (this.hand_R_001Sphere = this.hand_R_001.GetComponent<SphereCollider>()))
        {
            this.hand_R_001SphereT = this.hand_R_001Sphere.transform;
        }
        if (species == SPECIES.Titan)
        {
            this.chkAeLeft = this.baseT.Find("chkAeLeft");
            this.chkAeLLeft = this.baseT.Find("chkAeLLeft");
            this.chkAeRight = this.baseT.Find("chkAeRight");
            this.chkAeLRight = this.baseT.Find("chkAeLRight");
            this.ap_front_ground = this.baseT.Find("ap_front_ground");
        }
    }
    public void RecheckTransforms()
    {
        if (this.baseT == null && (this.baseT = base.GetComponent<Transform>()) == null)
        {
            return;
        }
        string str;
        if (this.species == SPECIES.Hero || this.species == SPECIES.Horse)
        {
            str = "Amarture/Controller_Body/";
            this.Core = null;
            if (this.Controller_Body == null)
            {
                this.Controller_Body = this.baseT.Find("Amarture/Controller_Body");
            }
        }
        else
        {
            str = "Amarture/Core/Controller_Body/";
            if (this.AABB == null)
            {
                this.AABB = this.baseT.Find("AABB");
            }
            if (this.Core == null)
            {
                this.Core = this.baseT.Find("Amarture/Core");
            }
            if (this.snd_titan_foot == null)
            {
                this.snd_titan_foot = this.baseT.Find("snd_titan_foot");
            }
            if (this.Controller_Body == null)
            {
                this.Controller_Body = this.baseT.Find("Amarture/Core/Controller_Body");
            }
        }
        if (this.Amarture == null)
        {
            this.Amarture = this.baseT.Find("Amarture");
        }
        if (this.head == null)
        {
            this.head = this.baseT.Find(str + "hip/spine/chest/neck/head");
        }
        if (this.neck == null)
        {
            this.neck = this.baseT.Find(str + "hip/spine/chest/neck");
        }
        if (this.thigh_L == null)
        {
            this.thigh_L = this.baseT.Find(str + "hip/thigh_L");
        }
        if (this.thigh_R == null)
        {
            this.thigh_R = this.baseT.Find(str + "hip/thigh_R");
        }
        if (this.foot_L == null)
        {
            this.foot_L = this.baseT.Find(str + "hip/thigh_L/shin_L/foot_L");
        }
        if (this.foot_R == null)
        {
            this.foot_R = this.baseT.Find(str + "hip/thigh_R/shin_R/foot_R");
        }
        if (this.chest == null)
        {
            this.chest = this.baseT.Find(str + "hip/spine/chest");
        }
        if (this.shoulder_L == null)
        {
            this.shoulder_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L");
        }
        if (this.shoulder_R == null)
        {
            this.shoulder_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R");
        }
        if (this.hip == null)
        {
            this.hip = this.baseT.Find(str + "hip");
        }
        if (this.spine == null)
        {
            this.spine = this.baseT.Find(str + "hip/spine");
        }
        if (this.upper_arm_L == null)
        {
            this.upper_arm_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L");
        }
        if (this.upper_arm_R == null)
        {
            this.upper_arm_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R");
        }
        if (this.forearm_L == null)
        {
            this.forearm_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L");
        }
        if (this.forearm_R == null)
        {
            this.forearm_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R");
        }
        if (this.hand_L == null)
        {
            this.hand_L = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L");
        }
        if (this.hand_R == null)
        {
            this.hand_R = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R");
        }
        if (this.hand_L_001 == null && (this.hand_L_001 = this.baseT.Find(str + "hip/spine/chest/shoulder_L/upper_arm_L/forearm_L/hand_L/hand_L_001")) && this.hand_L_001Sphere == null && (this.hand_L_001Sphere = this.hand_L_001.GetComponent<SphereCollider>()) && this.hand_L_001SphereT == null)
        {
            this.hand_L_001SphereT = this.hand_L_001Sphere.transform;
        }
        if (this.hand_R_001 == null && (this.hand_R_001 = this.baseT.Find(str + "hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001")) && this.hand_R_001Sphere == null && (this.hand_R_001Sphere = this.hand_R_001.GetComponent<SphereCollider>()) && this.hand_R_001SphereT == null)
        {
            this.hand_R_001SphereT = this.hand_R_001Sphere.transform;
        }
        if (this.ap_front_ground == null) this.ap_front_ground = this.ap_front_ground = this.baseT.Find("ap_front_ground");
    }
}
