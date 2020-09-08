using Photon;
using System;
using UnityEngine;

public class RockThrow : Photon.MonoBehaviour
{
    private bool launched;
    private Vector3 oldP;
    private Vector3 r;
    private Vector3 v;

    private Transform baseT;
    private PhotonView basePV;
    private int viewID = -1;
    private string titanName = string.Empty;


    private void Awake()
    {
        this.baseT = base.transform;
        this.basePV = base.photonView;
    }


    private void explore()
    {
        GameObject obj2;
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && (PhotonNetwork.isMasterClient || this.basePV.isMine))
        {
            obj2 = PhotonNetwork.Instantiate("FX/boom6", this.baseT.position, this.baseT.rotation, 0);
            EnemyfxIDcontainer component = gameObject.GetComponent<EnemyfxIDcontainer>();
            if (component != null)
            {
                component.myOwnerViewID = this.viewID;
                component.titanName = this.titanName;
            }
        }
        else
        {
            obj2 = (GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("FX/boom6"), this.baseT.position, this.baseT.rotation);
        }
        obj2.transform.localScale = this.baseT.localScale;
        float b = 1f - (Vector3.Distance(IN_GAME_MAIN_CAMERA.mainT.position, obj2.transform.position) * 0.05f);
        b = Mathf.Min(1f, b);
        IN_GAME_MAIN_CAMERA.mainCamera.startShake(b, b, 0.95f);
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            UnityEngine.Object.Destroy(base.gameObject);
            return;
        }
        PhotonNetwork.Destroy(basePV);
    }

    private void hitPlayer(GameObject hero, HERO component)
    {
        if (((hero != null) && !component.HasDied()) && !component.isInvincible())
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                if (!component.isGrabbed)
                {
                    component.die((Vector3)((this.v.normalized * 1000f) + (Vector3.up * 50f)), false);
                }
            }
            else if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && !component.HasDied()) && !component.isGrabbed)
            {
                component.markDie();
                object[] parameters = new object[] { (Vector3)((this.v.normalized * 1000f) + (Vector3.up * 50f)), false, viewID, titanName, true };
                component.photonView.RPC("netDie", PhotonTargets.All, parameters);
            }
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && this.basePV.isMine)
            {
                GameObject gameObject;
                gameObject = PhotonNetwork.Instantiate("FX/boom6", this.baseT.position, this.baseT.rotation, 0);
                gameObject.transform.localScale = this.baseT.localScale;
                EnemyfxIDcontainer component2 = gameObject.GetComponent<EnemyfxIDcontainer>();
                if (component2 != null)
                {
                    component2.myOwnerViewID = viewID;
                    component2.titanName = titanName;
                    return;
                }
            }
            else
            {
                ((GameObject)UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("FX/boom6"), this.baseT.position, this.baseT.rotation)).transform.localScale = this.baseT.localScale;
            }
        }
    }

    [RPC]
    private void initRPC(int viewID, Vector3 scale, Vector3 pos, float level)
    {
        PhotonView photonView = PhotonView.Find(viewID);
        if (photonView != null)
        {
            Transform gameObjectT = photonView.transform;
            if (gameObjectT != null)
            {
                Transform transform = gameObjectT.Find("Amarture/Core/Controller_Body/hip/spine/chest/shoulder_R/upper_arm_R/forearm_R/hand_R/hand_R_001");
                if (transform != null)
                {
                    this.baseT.localScale = gameObjectT.localScale;
                    this.baseT.parent = transform;
                    this.baseT.localPosition = pos;
                }
            }
        }
    }

    public void launch(Vector3 v1, string titanName = null, int viewID = -1)
    {
        this.launched = true;
        this.oldP = this.baseT.position;
        this.v = v1;
        if (!string.IsNullOrEmpty(titanName))
        {
            this.titanName = titanName;
        }
        if (viewID != -1)
        {
            this.viewID = viewID;
        }
        EnemyfxIDcontainer component = base.GetComponent<EnemyfxIDcontainer>();
        if (component != null)
        {
            component.titanName = titanName;
            component.myOwnerViewID = viewID;
        }
        if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && this.basePV.isMine)
        {
            object[] parameters = new object[] { this.v, this.oldP };
            base.photonView.RPC("launchRPC", PhotonTargets.Others, parameters);
        }
    }

    [RPC]
    private void launchRPC(Vector3 v, Vector3 p)
    {
        if (this.basePV != null && !this.basePV.isMine)
        {
            this.launched = true;
            Vector3 vector = p;
            this.baseT.position = vector;
            this.oldP = vector;
            this.baseT.parent = null;
            this.launch(v, null, -1);
        }
    }

    private void Start()
    {
        this.r = new Vector3(UnityEngine.Random.Range((float)-5f, (float)5f), UnityEngine.Random.Range((float)-5f, (float)5f), UnityEngine.Random.Range((float)-5f, (float)5f));
    }

    private void Update()
    {
        if (this.launched)
        {
            this.baseT.Rotate(this.r);
            this.v -= (Vector3)((20f * Vector3.up) * Time.deltaTime);
            this.baseT.position += this.v * Time.deltaTime;
            if ((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE) || this.basePV.isMine)
            {
                LayerMask mask = ((int)1) << LayerMask.NameToLayer("Ground");
                LayerMask mask2 = ((int)1) << LayerMask.NameToLayer("Players");
                LayerMask mask3 = ((int)1) << LayerMask.NameToLayer("EnemyAABB");
                LayerMask mask4 = (mask2 | mask) | mask3;
                foreach (RaycastHit hit in Physics.SphereCastAll(this.baseT.position, 2.5f * this.baseT.lossyScale.x,
                    this.baseT.position - this.oldP, Vector3.Distance(this.baseT.position, this.oldP), mask4))
                {
                    Collider collider = hit.collider;
                    if (!(collider == null))
                    {
                        GameObject gameObject = collider.gameObject;
                        if (!(gameObject == null))
                        {
                            GameObject gameObject2 = gameObject.transform.root.gameObject;
                            if (!(gameObject2 == null))
                            {
                                switch (gameObject.layer)
                                {
                                    case 8:
                                    case 13:
                                        if (gameObject2.tag.NullFix() == "Player")
                                        {
                                            MONO component = gameObject2.GetComponent<MONO>();
                                            if (!(component == null))
                                            {
                                                if (component.species == SPECIES.Hero)
                                                {
                                                    HERO hero = component as HERO;
                                                    if (!hero.HasDied() && !hero.isInvincible())
                                                    {
                                                        this.hitPlayer(gameObject2, hero);
                                                    }
                                                }
                                                else if (component.species == SPECIES.TitanEren)
                                                {
                                                    TITAN_EREN titan_EREN = component as TITAN_EREN;
                                                    if (!titan_EREN.isHit)
                                                    {
                                                        titan_EREN.hitByTitan();
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 9:
                                        this.explore();
                                        break;
                                    case 11:
                                        {
                                            TITAN component2 = gameObject2.GetComponent<TITAN>();
                                            if (component2 != null && !component2.hasDie)
                                            {
                                                Vector3 position = this.baseT.position;
                                                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                                                {
                                                    component2.hitAnkle();
                                                }
                                                else
                                                {
                                                    PhotonView photonView = PhotonView.Find(this.viewID);
                                                    if (photonView != null)
                                                    {
                                                        Vector3 position2 = photonView.transform.position;
                                                    }
                                                    component2.photonView.RPC("hitAnkleRPC", PhotonTargets.All, new object[0]);
                                                }
                                            }
                                            this.explore();
                                            break;
                                        }
                                }
                            }
                        }
                    } 
                }
                this.oldP = this.baseT.position;
                //    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "EnemyAABB")
                //    {
                //        GameObject gameObject = hit.collider.gameObject.transform.root.gameObject;
                //        if ((gameObject.GetComponent<TITAN>() != null) && !gameObject.GetComponent<TITAN>().hasDie)
                //        {
                //            gameObject.GetComponent<TITAN>().hitAnkle();
                //            Vector3 position = this.baseT.position;
                //            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                //            {
                //                gameObject.GetComponent<TITAN>().hitAnkle();
                //            }
                //            else
                //            {
                //                if ((this.baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>() != null) && (PhotonView.Find(this.baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID) != null))
                //                {
                //                    position = PhotonView.Find(this.baseT.root.gameObject.GetComponent<EnemyfxIDcontainer>().myOwnerViewID).transform.position;
                //                }
                //                gameObject.GetComponent<HERO>().photonView.RPC("hitAnkleRPC", PhotonTargets.All, new object[0]);
                //            }
                //        }
                //        this.explore();
                //    }
                //    else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Players")
                //    {
                //        GameObject hero = hit.collider.gameObject.transform.root.gameObject;
                //        if (hero.GetComponent<TITAN_EREN>() != null)
                //        {
                //            if (!hero.GetComponent<TITAN_EREN>().isHit)
                //            {
                //                hero.GetComponent<TITAN_EREN>().hitByTitan();
                //            }
                //        }
                //        else if ((hero.GetComponent<HERO>() != null) && !hero.GetComponent<HERO>().isInvincible())
                //        {
                //            this.hitPlayer(hero);
                //        }
                //    }
                //    else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Ground")
                //    {
                //        this.explore();
                //    }
                //}
                //this.oldP = this.baseT.position;

            }
        }
    }
}

