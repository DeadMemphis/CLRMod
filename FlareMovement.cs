using System;
using UnityEngine;

public class FlareMovement : MonoBehaviour
{
    public string color;
    private GameObject hero;
    private GameObject hint;
    private bool nohint;
    private Vector3 offY;
    private float timer;


    public Transform baseT;
    public Rigidbody baseR;
    public Transform hintT;


    public void dontShowHint()
    {
        UnityEngine.Object.Destroy(this.hint);
        this.nohint = true;
    }

    private void Awake()
    {
        this.baseT = base.transform;
        this.baseR = base.rigidbody;
    }

    private void Start()
    {
        this.hero = IN_GAME_MAIN_CAMERA.main_object;
        if (!this.nohint && (this.hero != null))
        {
            this.hintT = ( this.hint = (GameObject) UnityEngine.Object.Instantiate(CLEARSKIES.CacheResources.Load("UI/" + this.color + "FlareHint"))).transform;
            if (this.color == "Black")
            {
                this.offY = (Vector3) (Vector3.up * 0.4f);
            }
            else
            {
                this.offY = (Vector3) (Vector3.up * 0.5f);
            }
            this.hintT.parent = this.baseT.root;
            this.hintT.position = this.hero.transform.position + this.offY;
            Vector3 vector = this.baseT.position - this.hintT.position;
            float num = Mathf.Atan2(-vector.z, vector.x) * 57.29578f;
            this.hintT.rotation = Quaternion.Euler(-90f, num + 180f, 0f);
            this.hintT.localScale = Vector3.zero;
            object[] args = new object[] { "x", 1f, "y", 1f, "z", 1f, "easetype", iTween.EaseType.easeOutElastic, "time", 1f };
            iTween.ScaleTo(this.hint, iTween.Hash(args));
            object[] objArray2 = new object[] { "x", 0, "y", 0, "z", 0, "easetype", iTween.EaseType.easeInBounce, "time", 0.5f, "delay", 2.5f };
            iTween.ScaleTo(this.hint, iTween.Hash(objArray2));
        }
    }

    private void Update()
    {
        this.timer += Time.deltaTime;
        if (this.hint != null)
        {
            if (this.timer < 3f)
            {
                this.hintT.position = this.hero.transform.position + this.offY;
                Vector3 vector = this.baseT.position - this.hintT.position;
                float num = Mathf.Atan2(-vector.z, vector.x) * 57.29578f;
                this.hintT.rotation = Quaternion.Euler(-90f, num + 180f, 0f);
            }
            else if (this.hint != null)
            {
                UnityEngine.Object.Destroy(this.hint);
            }
        }
        if (this.timer < 4f)
        {
            this.baseR.AddForce((Vector3) (((this.baseT.forward + (this.baseT.up * 5f)) * Time.deltaTime) * 5f), ForceMode.VelocityChange);
        }
        else
        {
            this.baseR.AddForce((Vector3) ((-this.baseT.up * Time.deltaTime) * 7f), ForceMode.Acceleration);
        }
    }
}

