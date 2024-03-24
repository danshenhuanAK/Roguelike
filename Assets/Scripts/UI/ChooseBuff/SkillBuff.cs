using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class SkillBuff : MonoBehaviour
{
    public string skillSpawnerName;

    private GameObject skillSpawner;
    private GameObject spawnerObj;
    private GameObject chooseBuff;

    public List<PlayerSkillBuffData_SO> skillBuffDataList = new();

    private TMP_Text buffGrade;
    private TMP_Text buffBonus;

    public int grade;

    private void Awake()
    {
        chooseBuff = transform.parent.gameObject;
        skillSpawner = GameObject.Find("SkillSpawner");

        spawnerObj = GameObject.Find(skillSpawnerName);
        if (spawnerObj != null)
        {
            skillBuffDataList = spawnerObj.GetComponent<SkillSpawner>().skillData.playerSkillBuffDataList;
            grade = 1;
        }
        else
        {
            grade = 0;
            skillBuffDataList = null;
        }

        buffGrade = transform.GetChild(2).GetComponent<TMP_Text>();
        buffBonus = transform.GetChild(3).GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        int tableNum = 0;

        buffBonus.text = "";

        if (grade == 0)
        {
            buffGrade.text = "领悟技能";
            buffGrade.color = Color.red;
            return;
        }
        else if(grade != 0 && grade != 8)
        {
            buffGrade.text = "等级：" + (grade);
            buffGrade.color = Color.white;
        }
        
        if (skillBuffDataList[grade - 1].durationBuff != 0)
        {
            tableNum++;
            buffBonus.text += "持续时间 +" + (int)(skillBuffDataList[grade - 1].durationBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].coolDownBuff != 0)
        {
            tableNum++;
            buffBonus.text += "技能冷却 -" + (int)(skillBuffDataList[grade - 1].coolDownBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].damageCoolDownBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "伤害冷却 -" + (int)(skillBuffDataList[grade - 1].damageCoolDownBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].launchMoveSpeedBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "发射速度 +" + (int)(skillBuffDataList[grade - 1].launchMoveSpeedBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].searchEnemyRangeBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "索敌范围 +" + (int)(skillBuffDataList[grade - 1].searchEnemyRangeBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].attackDamageBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "攻击伤害 +" + (int)(skillBuffDataList[grade - 1].attackDamageBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].skillProjectileQuantityBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
            }
            tableNum++;
            buffBonus.text += "发射数量 +" + skillBuffDataList[grade - 1].skillProjectileQuantityBuff + "\t";
        }
        if (skillBuffDataList[grade - 1].attackRangeBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "攻击范围 +" + (int)(skillBuffDataList[grade - 1].attackRangeBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].repelPowerBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "击退力度 +" + (int)(skillBuffDataList[grade - 1].repelPowerBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].retardPowerBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "减速程度 +" + (int)(skillBuffDataList[grade - 1].retardPowerBuff * 100) + "%\t";
        }
        if (skillBuffDataList[grade - 1].retardTmieBuff != 0)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
            }
            buffBonus.text += "减速时间 +" + (int)(skillBuffDataList[grade - 1].retardTmieBuff * 100) + "%\t";
        }
    }

    public void SkillOnClick()
    {
        if(chooseBuff.GetComponent<ChooseBUFF>().isButtonCleanUp)
        {
            ClearBuff();
            chooseBuff.GetComponent<ChooseBUFF>().CleanUpSpend();
        }
        else
        {
            SkillUp();
        }
    }

    public void SkillUp()
    {
        if(grade == 0)
        {
            Addressables.LoadAssetAsync<GameObject>(skillSpawnerName).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    spawnerObj = Instantiate(handle.Result, skillSpawner.transform);
                    spawnerObj.name = handle.Result.name;
                    skillBuffDataList = spawnerObj.GetComponent<SkillSpawner>().skillData.playerSkillBuffDataList;
                    Addressables.Release(handle);
                }
            };
        }
        else
        {
            spawnerObj.GetComponent<SkillSpawner>().UpdateSkillAttribute();
        }
        grade++;
    }

    public void ClearBuff()
    {
        chooseBuff.GetComponent<ChooseBUFF>().ClearUI(gameObject.name);
    }
}
