using UnityEngine;

[System.Serializable]
public class GameCurrentAttribute
{
    public float timer;

    public int second;

    public int minute;

    public int gold;
}

[System.Serializable]
public class CurrentAttribute
{
    public float health;                                                //生命值

    public float maxHealth;                                             //最大生命

    public float healthRegen;                                           //生命恢复

    public float defence;                                               //防御值

    public float moveSpeed;                                             //移动速度

    public float attackPower;                                           //攻击力

    public float launchMoveSpeed;                                       //发射技能移动速度

    public float duration;                                              //技能持续时间加成

    public float attackRange;                                           //技能攻击范围加成

    public float skillCoolDown;                                         //技能冷却时间加成

    public float projectileQuantity;                                    //技能数量加成

    public float revival;                                               //复活次数

    public float magnet;                                                //拾取范围

    public float critical;                                              //暴击率

    public float criticalDamage;                                        //暴击伤害

    public float level;                                                 //等级

    public int experienceCap;                                           //升级所需经验                                

    public float experience;                                            //经验

    public float experienceCquisitionSpeed;                             //经验增长速度

    public float luck;                                                  //幸运值

    public int reroll;                                                  //重选次数

    public int skip;                                                    //跳过次数

    public int banish;                                                  //排除次数
}

[System.Serializable]
public class EnemyLevelData
{
    public float maxHealth;

    public float defence;

    public float moveSpeed;

    public float attackDamage;
}

[System.Serializable]
public class EnemyCurrentAttribute
{
    public float maxHealth;                                 //最大生命值

    public float currentHealth;                             //当前生命值

    public float defence;                                   //防御值

    public float moveSpeed;                                 //移动速度

    public float attackDamage;                              //攻击伤害

    public float coolDown;                                  //攻击间隔

    public bool isAttack;                                   //是否攻击

    public bool isBoss;                                     //是否为精英怪
}

[System.Serializable]
public class EnemyCurrentAttributes
{
    public UnityEngine.GameObject enemyPrefab;

    public EnemyCurrentAttribute enemyCurrentAttributes;
}

[System.Serializable]
public class SkillAttribute
{
    public int skillGrade;                                          //技能等级

    public float duration;                                          //技能持续时间

    public float coolDown;                                          //技能冷却时间

    public float cdRemain;                                          //技能剩余冷却时间

    public float damageCoolDown;                                    //伤害冷却

    public float launchMoveSpeed;                                   //发射速度

    public float attackRange;                                       //攻击范围

    public float attackDamage;                                      //基础攻击力

    public int skillProjectileQuantity;                             //技能投射物数量

    public float skillScale;                                        //技能大小
}

[System.Serializable]
public class SkillData
{
    public UnityEngine.LayerMask skillAttackMask;                   //技能攻击图层

    public UnityEngine.GameObject skillObject;                      //技能预制体

    public SkillAttribute[] skillAttribute;
}