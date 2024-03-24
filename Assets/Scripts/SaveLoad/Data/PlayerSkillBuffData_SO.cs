using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillData/PlayerSkillBuffData")]
public class PlayerSkillBuffData_SO : ScriptableObject
{
    public float durationBuff;                                          //技能持续时间

    public float coolDownBuff;                                          //技能冷却时间

    public float damageCoolDownBuff;                                    //伤害冷却

    public float launchMoveSpeedBuff;                                   //发射速度

    public float searchEnemyRangeBuff;                                  //索敌范围

    public float attackRangeBuff;                                       //攻击范围

    public float attackDamageBuff;                                      //基础攻击力

    public int skillProjectileQuantityBuff;                              //技能投射物数量

    public float repelPowerBuff;                                        //推开力度

    public float retardPowerBuff;                                       //减速程度

    public float retardTmieBuff;                                        //减速时间
}
