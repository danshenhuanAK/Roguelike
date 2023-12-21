using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Skill Gain Data")]
public class GainAttackData_SO : ScriptableObject
{
    public float launchMoveSpeedBuff;                       //发射速度增益

    public float attackRangeBuff;                           //攻击范围增益

    public float attackPowerBuff;                           //基础攻击力增益

    public float criticalBuff;                              //暴击率增益
        
    public float criticalDamageBuff;                        //暴击伤害增益

    public float projectileQuantity;                        //当前投射物数量
}
