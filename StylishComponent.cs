using System;
using UnityEngine;

public class StylishComponent : MonoBehaviour
{
    public static StylishComponent stylebar;

    private Transform baseT;
    private UISprite UIBar;
    private UILabel UIChain;
    public UILabel UIHits;
    public UILabel UIS;
    public UILabel UIS1;
    public UILabel UIS2;
    public UILabel UIsub;
    public UILabel UITotal;
    
    public GameObject bar;
    private int chainKillRank;
    private float[] chainRankMultiplier;
    private float chainTime;
    private float duration;
    private Vector3 exitPosition;
    private bool flip;
    private bool hasLostRank;
    public GameObject labelChain;
    public GameObject labelHits;
    public GameObject labelS;
    public GameObject labelS1;
    public GameObject labelS2;
    public GameObject labelsub;
    public GameObject labelTotal;
    private Vector3 originalPosition;
    private float R;
    private int styleHits;
    private float stylePoints;
    private int styleRank;
    private int[] styleRankDepletions;
    private int[] styleRankPoints;
    private string[,] styleRankText;
    private int styleTotalDamage;


    private void OnAwake()
    {
        stylebar = this;
    }

    private void OnDestroy()
    {
        stylebar = null;
    }

    public StylishComponent()
    {
        string[,] textArray1 = { { "D", "eja Vu" }, { "C", "asual" }, { "B", "oppin!" }, { "A", "mazing!" }, { "S", "ensational!" }, { "S", "pectacular!!" }, { "S", "tylish!!!" }, { "X", "TREEME!!!" } };
        styleRankText = textArray1;
        this.chainRankMultiplier = new float[] { 1f, 1.1f, 1.2f, 1.3f, 1.5f, 1.7f, 2f, 2.3f, 2.5f };
        this.styleRankPoints = new int[] { 350, 950, 0x992, 0x11c6, 0x1b58, 0x3a98, 0x186a0 };
        this.styleRankDepletions = new int[] { 1, 2, 5, 10, 15, 20, 0x19, 0x19 };
    }

    private int GetRankPercentage()
    {
        if ((this.styleRank > 0) && (this.styleRank < this.styleRankPoints.Length))
        {
            return (int) (((this.stylePoints - this.styleRankPoints[this.styleRank - 1]) * 100f) / ((float) (this.styleRankPoints[this.styleRank] - this.styleRankPoints[this.styleRank - 1])));
        }
        if (this.styleRank == 0)
        {
            return (((int) (this.stylePoints * 100f)) / this.styleRankPoints[this.styleRank]);
        }
        return 100;
    }

    private int GetStyleDepletionRate()
    {
        return this.styleRankDepletions[this.styleRank];
    }

    public void reset()
    {
        this.styleTotalDamage = 0;
        this.chainKillRank = 0;
        this.chainTime = 0f;
        this.styleRank = 0;
        this.stylePoints = 0f;
        this.styleHits = 0;
    }

    private void setPosition()
    {
        this.originalPosition = new Vector3((float) ((int) ((Screen.width * 0.5f) - 2f)), (float) ((int) ((Screen.height * 0.5f) - 150f)), 0f);
        this.exitPosition = new Vector3((float) Screen.width, this.originalPosition.y, this.originalPosition.z);
    }

    private void SetRank()
    {
        int styleRank = this.styleRank;
        int index = 0;
        while ((index < this.styleRankPoints.Length) && (this.stylePoints > this.styleRankPoints[index]))
        {
            index++;
        }
        if (index < this.styleRankPoints.Length)
        {
            this.styleRank = index;
        }
        else
        {
            this.styleRank = this.styleRankPoints.Length;
        }
        if (this.styleRank < styleRank)
        {
            if (this.hasLostRank)
            {
                this.stylePoints = 0f;
                this.styleHits = 0;
                this.styleTotalDamage = 0;
                this.styleRank = 0;
            }
            else
            {
                this.hasLostRank = true;
            }
        }
        else if (this.styleRank > styleRank)
        {
            this.hasLostRank = false;
        }
    }

    private void setRankText()
    {
       UIS.text = this.styleRankText[this.styleRank, 0];
        if (this.styleRank == 5)
        {
            UIS2.text = "[" + ColorSet.color_SS + "]S";
        }
        else
        {
            UIS2.text = string.Empty;
        }
        if (this.styleRank == 6)
        {
            UIS2.text = "[" + ColorSet.color_SSS + "]S";
            UIS1.text = "[" + ColorSet.color_SSS + "]S";
        }
        else
        {
            UIS1.text = string.Empty;
        }
        if (this.styleRank == 0)
        {
           UIS.text = "[" + ColorSet.color_D + "]D";
        }
        if (this.styleRank == 1)
        {
           UIS.text = "[" + ColorSet.color_C + "]C";
        }
        if (this.styleRank == 2)
        {
           UIS.text = "[" + ColorSet.color_B + "]B";
        }
        if (this.styleRank == 3)
        {
           UIS.text = "[" + ColorSet.color_A + "]A";
        }
        if (this.styleRank == 4)
        {
           UIS.text = "[" + ColorSet.color_S + "]S";
        }
        if (this.styleRank == 5)
        {
           UIS.text = "[" + ColorSet.color_SS + "]S";
        }
        if (this.styleRank == 6)
        {
           UIS.text = "[" + ColorSet.color_SSS + "]S";
        }
        if (this.styleRank == 7)
        {
           UIS.text = "[" + ColorSet.color_X + "]X";
        }
        UIsub.text = this.styleRankText[this.styleRank, 1];
    }

    private void shakeUpdate()
    {
        if (this.duration > 0f)
        {
            this.duration -= Time.deltaTime;
            if (this.flip)
            {
                this.baseT.localPosition = this.originalPosition + ((Vector3) (Vector3.up * this.R));
            }
            else
            {
                this.baseT.localPosition = this.originalPosition - ((Vector3) (Vector3.up * this.R));
            }
            this.flip = !this.flip;
            if (this.duration <= 0f)
            {
                this.baseT.localPosition = this.originalPosition;
            }
        }
    }

    private void Start()
    {
        stylebar = this;
        UIBar = bar.GetComponent<UISprite>();
        UIChain = labelChain.GetComponent<UILabel>();
        UIHits = labelHits.GetComponent<UILabel>();
        UIS = labelS.GetComponent<UILabel>();
        UIS1 = labelS1.GetComponent<UILabel>();
        UIS2 = labelS2.GetComponent<UILabel>();
        UIsub = labelsub.GetComponent<UILabel>();
        UITotal = labelTotal.GetComponent<UILabel>();
        setPosition();
        (baseT = transform).localPosition = exitPosition;
    }

    public void startShake(int R, float duration)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
        }
    }

    public void Style(int damage)
    {
        if (damage != -1)
        {
            this.stylePoints += (int) ((damage + 200) * this.chainRankMultiplier[this.chainKillRank]);
            this.styleTotalDamage += damage;
            this.chainKillRank = (this.chainKillRank >= (this.chainRankMultiplier.Length - 1)) ? this.chainKillRank : (this.chainKillRank + 1);
            this.chainTime = 5f;
            this.styleHits++;
            this.SetRank();
        }
        else if (this.stylePoints == 0f)
        {
            this.stylePoints++;
            this.SetRank();
        }
        this.startShake(5, 0.3f);
        this.setPosition();
        UITotal.text = ((int) this.stylePoints).ToString();
        UIHits.text = this.styleHits.ToString() + ((this.styleHits <= 1) ? "Hit" : "Hits");
        if (this.chainKillRank == 0)
        {
            UIChain.text = string.Empty;
        }
        else
        {
            UIChain.text = "x" + this.chainRankMultiplier[this.chainKillRank].ToString() + "!";
        }
    }

    private void Update()
    {
        if (!IN_GAME_MAIN_CAMERA.isPausing)
        {
            if (this.stylePoints > 0f)
            {
                this.setRankText();
                UIBar.fillAmount = this.GetRankPercentage() * 0.01f;
                this.stylePoints -= (this.GetStyleDepletionRate() * Time.deltaTime) * 10f;
                this.SetRank();
            }
            else
            {
                base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.exitPosition, Time.deltaTime * 3f);
            }
            if (this.chainTime > 0f)
            {
                this.chainTime -= Time.deltaTime;
            }
            else
            {
                this.chainTime = 0f;
                this.chainKillRank = 0;
            }
            this.shakeUpdate();
        }
    }
}

