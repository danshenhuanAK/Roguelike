using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Data")]
public class AttributeData_SO : ScriptableObject
{
    public int baseGrade;                               //基础等级

    public int currentGrade;                            //当前等级

    public float maxHealth;                             //最大生命值

    public float currentHealth;                         //当前生命值

    public float baseDefence;                           //基础防御值

    public float currentDefence;                        //当前防御值

    public float baseHealthRegen;                       //基础生命回复

    public float currentHealthRegen;                    //当前生命回复

    public float baseMoveSpeed;                         //基础移动速度

    public float currentMoveSpeed;                      //当前移动速度

    public float maximumExperience;                     //最大经验

    public float currentExperience;                     //当前经验

    public float baseExperienceCquisitionSpeed;         //基础经验获取速率

    public float currentExperienceCquisitionSpeed;      //当前经验获取速率

    public float damageReduction;                       //伤害减免
}
