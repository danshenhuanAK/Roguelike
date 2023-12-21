using System;

[Serializable]
public class PlayerData
{
    internal float timer;                                         //游戏时间

    internal int second;                                          //游戏时间（秒）

    internal int minute;                                          //游戏时间（分）

    internal int gold;                                            //金币

    internal float health = 100;                                  //生命值

    internal float maxHealth = 100;                               //最大生命值

    internal float healthRegen = 0;                               //生命回复

    internal float defence = 1;                                   //防御值

    internal float moveSpeed = 10;                                //移动速度

    internal float attackPower = 0;                               //基础攻击力

    internal float launchMoveSpeed = 0;                           //发射速度

    internal float duration = 5;                                  //持续时间

    internal float attackRange = 10;                              //攻击范围

    internal float skillCoolDown = 10;                            //冷却时间

    internal float projectileQuantity = 0;                        //投射物数量

    internal float revival = 1;                                   //复活次数

    internal float magnet = 0;                                    //拾取范围

    internal float critical = 0;                                  //暴击率

    internal float criticalDamage = 100;                          //暴击伤害

    internal float level = 1;                                     //等级

    internal int experienceCap = 100;                             //升级所需经验

    internal float experience = 0;                                //经验值

    internal float experienceCquisitionSpeed = 0;                 //经验获取速率

    internal float luck = 0;                                      //幸运值

    internal int reroll = 3;                                      //重选

    internal int skip = 3;                                        //跳过

    internal int banish = 3;                                      //排除
}
