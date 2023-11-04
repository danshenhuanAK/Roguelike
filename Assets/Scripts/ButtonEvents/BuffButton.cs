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
        textUI.text = "�ȼ���" + UIGrade + "\n";
        
        switch(this.name)
        {
            case "Magic Sword(Clone)":
                textUI.text += "���������� +4\nͶ�����ٶ� +10%";
                break;
            case "Cool Down(Clone)":
                textUI.text += "ʩ����� -10%\n�˺�ϵ�� +30%";
                break;
            case "Experience(Clone)":
                textUI.text += "�����ȡ���� +10%\n�˺�ϵ�� +10%";
                break;
            case "Four Leaf Clover(Clone)":
                textUI.text += "������ +3%\n�����˺� +50%";
                break;
            case "Blood Volume(Clone)":
                textUI.text += "�������ֵ +20\nÿ��ظ����� +0.2";
                break;
            case "Range(Clone)":
                textUI.text += "������Χ +10%\n������ +2%\n�˺�ϵ�� +10%";
                break;
            case "Shield(Clone)":
                textUI.text += "������ +2\n�˺����� +2%";
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
