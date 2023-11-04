using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    public AttributeData_SO attributeData;
    public AttributeData_SO templateAttributeData;
    public MonsterAttackData_SO monsterAttackData;
    public MonsterAttackData_SO templateMonsterAttackData;
    public SkillAttackData_SO skillAttackData;
    public GainAttackData_SO gainAttackData;

    [HideInInspector]
    public bool isCritical;

    private void OnEnable()
    {
        if (templateAttributeData != null)
        {
            attributeData = Instantiate(templateAttributeData);
        }
        if (templateMonsterAttackData != null)
        {
            monsterAttackData = Instantiate(templateMonsterAttackData);
        }
    }

    #region Read from Data_SO
    public float MaxHealth
    {
        get => attributeData?.maxHealth ?? 0;
        set => attributeData.maxHealth = value;
    }

    public float CurrentHealth
    {
        get => attributeData?.currentHealth ?? 0;
        set => attributeData.currentHealth = value;
    }

    public float BaseDefence
    {
        get => attributeData?.baseDefence ?? 0;
        set => attributeData.baseDefence = value;
    }

    public float CurrentDefence
    {
        get => attributeData?.currentDefence ?? 0;
        set => attributeData.currentDefence = value;
    }
    #endregion

    #region Characher Combat
    
    public void SkillDamage(CharacterStats targeterData)
    {
        float damage = Mathf.Max(skillAttackData.currentAttackDamage - targeterData.attributeData.currentDefence, 0.1f);

        targeterData.attributeData.currentHealth -= damage;
    }

    public void MonsterDamage(CharacterStats targeterData)
    {
        float damage = Mathf.Max(monsterAttackData.currentAttackDamage - targeterData.attributeData.currentDefence, 0.1f);

        targeterData.attributeData.currentHealth -= damage;
    }

    #endregion
}
