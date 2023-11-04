using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Player Attack Data")]
public class GainAttackData_SO : ScriptableObject
{
    public bool isAttack;                           //是否攻击

    public float baseLaunchMoveSpeed;               //基础速度

    public float launchMoveSpeedBuff;               //发射速度增益

    public float attackRangeBuff;                   //攻击范围增益

    public float baseAttackPower;                   //基础攻击力

    public float attackPowerBuff;                   //基础攻击力增益

    public float criticalBuff;                      //暴击率增益
        
    public float criticalDamageBuff;                //暴击伤害增益

    public float damageMultiplier;                  //当前伤害系数增益

    public float baseProjectileQuantity;            //基础投射物数量

    public float currentProjectileQuantity;         //当前投射物数量

    public int baseGrade;                           //基础等级

    public int currentGrade;                        //当前等级
}
