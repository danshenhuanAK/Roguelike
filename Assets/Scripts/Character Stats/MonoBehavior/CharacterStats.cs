using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    public PlayerBaseAttributeData_SO playerBaseAttributeData;
    public List<MonsterAttributeData_SO> templateMonsterAttributeData;
    public MonsterAttributeData_SO monsterAttributeData;

    public SkillAttackData_SO skillAttackData;

    [HideInInspector]
    public bool isCritical;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void OnEnable()
    {
        if(templateMonsterAttributeData != null)
        {
            for(int i = 0; i < templateMonsterAttributeData.Count; i++)
            {
                if(gameManager.minute >= templateMonsterAttributeData[i].startTime && gameManager.minute < templateMonsterAttributeData[i].endTime)
                {
                    monsterAttributeData = Instantiate(templateMonsterAttributeData[i]);
                }
            }
        }
    }

    #region Characher Combat
    
    public void SkillDamage(CharacterStats targeterData)
    {
        //float damage = Mathf.Max(skillAttackData.currentAttackDamage - targeterData.attributeData.currentDefence, 0.1f);

        //targeterData.attributeData.currentHealth -= damage;
    }

    public void MonsterDamage(CharacterStats targeterData)
    {
        //float damage = Mathf.Max(monsterAttackData.currentAttackDamage - targeterData.attributeData.currentDefence, 0.1f);

        //targeterData.attributeData.currentHealth -= damage;
    }

    #endregion
}
