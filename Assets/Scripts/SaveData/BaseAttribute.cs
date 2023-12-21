[System.Serializable]
public class GameBaseAttribute
{
    public float timer = 0;                                         //游戏时间

    public int second = 0;                                          //游戏时间（秒）

    public int minute = 0;                                          //游戏时间（分）

    public int gold = 0;                                            //金币
}

public class GameCurrentAttribute
{
    public float timer;

    public int second;

    public int minute;

    public int gold;
}

[System.Serializable]
public class BaseAttribute
{
    public float health = 100;                                  //生命值

    public float maxHealth = 100;                               //最大生命值

    public float healthRegen = 0;                               //生命回复

    public float defence = 1;                                   //防御值

    public float moveSpeed = 10;                                //移动速度

    public float attackPower = 0;                               //基础攻击力

    public float launchMoveSpeed = 0;                           //发射速度

    public float duration = 5;                                  //持续时间

    public float attackRange = 10;                              //攻击范围

    public float skillCoolDown = 10;                            //冷却时间

    public float projectileQuantity = 0;                        //投射物数量

    public float revival = 1;                                   //复活次数

    public float magnet = 0;                                    //拾取范围

    public float critical = 0;                                  //暴击率

    public float criticalDamage = 100;                          //暴击伤害

    public float level = 1;                                     //等级

    public int experienceCap = 100;                             //升级所需经验

    public float experience = 0;                                //经验值

    public float experienceCquisitionSpeed = 0;                 //经验获取速率

    public float luck = 0;                                      //幸运值

    public int reroll = 3;                                      //重选

    public int skip = 3;                                        //跳过

    public int banish = 3;                                      //排除
}

[System.Serializable]
public class CurrentAttribute
{
    public float health;

    public float maxHealth;

    public float healthRegen;

    public float defence;

    public float moveSpeed;

    public float attackPower;

    public float launchMoveSpeed;

    public float duration;

    public float attackRange;

    public float skillCoolDown;

    public float projectileQuantity;

    public float revival;

    public float magnet;

    public float critical;

    public float criticalDamage;

    public float level;

    public int experienceCap;

    public float experience;

    public float experienceCquisitionSpeed;

    public float luck;

    public int reroll;

    public int skip;

    public int banish;
}