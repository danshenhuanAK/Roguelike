using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillBuff : MonoBehaviour
{
    private ObjectPool objectPool;

    public GameObject skillSpawner;
    private GameObject chooseBuff;

    public SkillData skillData;

    private TMP_Text buffName;
    private TMP_Text buffGrade;
    private TMP_Text buffBonus;

    public int grade;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        chooseBuff = GameObject.FindGameObjectWithTag("Buff");

        skillData = skillSpawner.GetComponent<SkillSpawner>().basicSkill;
        
        grade = skillSpawner.GetComponent<SkillSpawner>().grade;

        buffName = transform.GetChild(1).GetComponent<TMP_Text>();
        buffGrade = transform.GetChild(2).GetComponent<TMP_Text>();
        buffBonus = transform.GetChild(3).GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        int tableNum = 0;

        if (skillSpawner.GetComponent<SkillSpawner>().isEvolution)
        {
            buffName.text = "进化" + buffName.text;
        }

        if (grade == 0)
        {
            buffGrade.text = "领悟技能";
            buffGrade.color = Color.red;
        }
        else if(grade != 0 && grade != 8)
        {
            buffGrade.text = "等级：" + (grade + 1);
            buffGrade.color = Color.white;
        }
        else
        {
            buffGrade.text = "进化技能";
            skillData = skillSpawner.GetComponent<SkillSpawner>().evolutionSkill;
            return;
        }

        buffBonus.text = "";
        if (skillData.skillAttribute[grade].duration != skillData.skillAttribute[grade + 1].duration)
        {
            tableNum++;
            buffBonus.text += "持续时间 +" + (skillData.skillAttribute[grade + 1].duration - skillData.skillAttribute[grade].duration) + "\t";
        }
        if (skillData.skillAttribute[grade].coolDown != skillData.skillAttribute[grade + 1].coolDown)
        {
            tableNum++;
            buffBonus.text += "冷却时间 -" + (skillData.skillAttribute[grade].coolDown - skillData.skillAttribute[grade + 1].coolDown) + "\t";
        }
        if (skillData.skillAttribute[grade].damageCoolDown != skillData.skillAttribute[grade + 1].damageCoolDown)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "伤害冷却 -" + (skillData.skillAttribute[grade].damageCoolDown - skillData.skillAttribute[grade + 1].damageCoolDown) + "\t";
        }
        if (skillData.skillAttribute[grade].launchMoveSpeed != skillData.skillAttribute[grade + 1].launchMoveSpeed)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "发射速度 +" + (skillData.skillAttribute[grade + 1].launchMoveSpeed - skillData.skillAttribute[grade].launchMoveSpeed) + "\t";
        }
        if (skillData.skillAttribute[grade].attackRange != skillData.skillAttribute[grade + 1].attackRange)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "索敌范围 +" + (skillData.skillAttribute[grade + 1].attackRange - skillData.skillAttribute[grade].attackRange) + "\t";
        }
        if (skillData.skillAttribute[grade].attackDamage != skillData.skillAttribute[grade + 1].attackDamage)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
                tableNum = 0;
            }
            tableNum++;
            buffBonus.text += "攻击伤害 +" + (skillData.skillAttribute[grade + 1].attackDamage - skillData.skillAttribute[grade].attackDamage) + "\t";
        }
        if (skillData.skillAttribute[grade].skillProjectileQuantity != skillData.skillAttribute[grade + 1].skillProjectileQuantity)
        {
            if (tableNum == 2)
            {
                buffBonus.text += "\n";
            }
            buffBonus.text += "发射数量 +" + (skillData.skillAttribute[grade + 1].skillProjectileQuantity - skillData.skillAttribute[grade].skillProjectileQuantity) + "\t";
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
            objectPool.CreateObject(skillSpawner.name, skillSpawner, GameObject.Find("SkillSpawner"), Vector3.zero, Quaternion.identity);
        }

        grade++;
        skillSpawner.GetComponent<SkillSpawner>().grade++;
        if(grade == skillSpawner.GetComponent<SkillSpawner>().basicSkill.skillAttribute.Length)
        {
            skillSpawner.GetComponent<SkillSpawner>().EvolutionSkill();
        }
    }

    public void ClearBuff()
    {
        chooseBuff.GetComponent<ChooseBUFF>().ClearUI(gameObject.name);
    }
}
