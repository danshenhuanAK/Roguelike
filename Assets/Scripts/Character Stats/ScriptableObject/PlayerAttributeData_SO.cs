using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Player Data")]
public class PlayerAttributeData_SO : ScriptableObject
{
    public float health;                                    //生命值

    public float maxHealth;                                 //最大生命值

    public float healthRegen;                               //生命回复

    public float defence;                                   //防御值

    public float moveSpeed;                                 //移动速度

    public float attackPowerBuff;                           //基础攻击力

    public float launchMoveSpeedBuff;                       //发射速度

    public float duration;                                  //持续时间

    public float attackRangeBuff;                           //攻击范围

    public float skillCoolDown;                             //冷却时间

    public float projectileQuantity;                        //投射物数量

    public float revival;                                   //复活次数

    public float magnet;                                    //拾取范围

    public float criticalBuff;                              //暴击率

    public float criticalDamageBuff;                        //暴击伤害

    public float experienceCquisitionSpeed;                 //经验获取速率

    public float luck;                                      //幸运值

    public int reroll;                                      //重选

    public int skip;                                        //跳过

    public int banish;                                      //排除
}
