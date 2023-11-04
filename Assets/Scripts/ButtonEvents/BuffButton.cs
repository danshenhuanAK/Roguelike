using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuffButton : MonoBehaviour
{
    private Text textUI;
    private CharacterStats skillCharacterStats;

    private GameObject chooseBuffCanvas;
    private ChooseBUFF chooseBuff;

    public int UIGrade;
    private int deleteFlag;
    private List<GameObject> buffUI = new List<GameObject>();

    private void Awake()
    {
        UIGrade = 1;

        textUI = transform.GetChild(1).GetComponent<Text>();
        skillCharacterStats = GetComponent<CharacterStats>();

        chooseBuffCanvas = GameObject.FindGameObjectWithTag("Buff");
        chooseBuff = (ChooseBUFF)chooseBuffCanvas.GetComponent(typeof(ChooseBUFF));
        buffUI = chooseBuff.showUI;

        for(int i = 0; i < buffUI.Count; i++)
        {
            if(gameObject.name == buffUI[i].name + "(Clone)")
            {
                deleteFlag = i;
            }
        }
    }

    private void OnEnable()
    {
        textUI.text = "等级：" + UIGrade + "\n";
        
        switch(this.name)
        {
            case "Magic Sword(Clone)":
                textUI.text += "基础攻击力 +4\n投射物速度 +10%";
                break;
            case "Cool Down(Clone)":
                textUI.text += "施法间隔 -10%\n伤害系数 +30%";
                break;
            case "Experience(Clone)":
                textUI.text += "经验获取速率 +10%\n伤害系数 +10%";
                break;
            case "Four Leaf Clover(Clone)":
                textUI.text += "暴击率 +3%\n暴击伤害 +50%";
                break;
            case "Blood Volume(Clone)":
                textUI.text += "最大生命值 +20\n每秒回复生命 +0.2";
                break;
            case "Range(Clone)":
                textUI.text += "攻击范围 +10%\n暴击率 +2%\n伤害系数 +10%";
                break;
            case "Shield(Clone)":
                textUI.text += "防御力 +2\n伤害减免 +2%";
                break;
        }
    }

    public void BloodVolume()
    {
        UIGrade += 1;

        skillCharacterStats.attributeData.maxHealth += 20f;
        skillCharacterStats.attributeData.currentHealth += 20f;
        skillCharacterStats.attributeData.currentHealthRegen += 0.2f;

        chooseBuff.CollectUI();

        if(UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void MagicSword()                
    {
        UIGrade += 1;

        

        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void CoolDwon()
    {
        UIGrade += 1;


        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void Experience()
    {
        UIGrade += 1;

        skillCharacterStats.attributeData.currentExperienceCquisitionSpeed += 0.1f;

        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void FourLeafClover()
    {
        UIGrade += 1;


        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void Range()
    {
        UIGrade += 1;


        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void Shield()
    {
        UIGrade += 1;

        skillCharacterStats.attributeData.currentDefence += 2f;
        skillCharacterStats.attributeData.damageReduction += 0.02f;

        chooseBuff.CollectUI();

        if (UIGrade == 8)
        {
            chooseBuff.ClearUI(deleteFlag);
        }
    }

    public void EndChooseUI()
    {
        chooseBuffCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
