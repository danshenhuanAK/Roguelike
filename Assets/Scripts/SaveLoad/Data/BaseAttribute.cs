using UnityEngine.AddressableAssets;

[System.Serializable]
public class GameFightAttribute
{
    public double timer;

    public int second;

    public int minute;

    public int kills;

    public int gold;
}

[System.Serializable]
public class CurrentAttribute
{
    public double health;                                                //生命值

    public double maxHealth;                                             //最大生命

    public double healthRegen;                                           //生命恢复

    public double defence;                                               //防御值

    public double moveSpeed;                                             //移动速度

    public double attackPower;                                           //攻击力

    public double launchMoveSpeed;                                       //发射技能移动速度

    public double duration;                                              //技能持续时间加成

    public double attackRange;                                           //技能攻击范围加成

    public double skillCoolDown;                                         //技能冷却时间加成

    public int projectileQuantity;                                       //技能数量加成

    public int revival;                                                  //复活次数

    public double magnet;                                                //拾取范围

    public double critical;                                              //暴击率

    public double criticalDamage;                                        //暴击伤害

    public double level;                                                 //等级

    public int experienceCap;                                            //升级所需经验                                

    public double experience;                                            //经验

    public double experienceCquisitionSpeed;                             //经验增长速度

    public int luck;                                                     //幸运值
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
    public double maxHealth;                                 //最大生命值

    public double currentHealth;                             //当前生命值

    public double defence;                                   //防御值

    public double moveSpeed;                                 //移动速度

    public double attackDamage;                              //攻击伤害

    public double coolDown;                                  //攻击间隔

    public bool isAttack;                                    //是否攻击

    public bool isBoss;                                      //是否为精英怪
}

[System.Serializable]
public class EnemyCurrentAttributes
{
    public AssetReferenceGameObject enemyPre;

    public EnemyData_SO enmeyData;

    public EnemyCurrentAttribute enemyCurrentAttributes;
}

[System.Serializable]
public class SkillAttributeBuff
{
    public float durationBuff;                                          //技能持续时间

    public float coolDownBuff;                                          //技能冷却时间

    public float damageCoolDownBuff;                                    //伤害冷却

    public float launchMoveSpeedBuff;                                   //发射速度

    public float searchEnemyRangeBuff;                                  //索敌范围

    public float attackRangeBuff;                                       //攻击范围

    public float attackDamageBuff;                                      //基础攻击力

    public int skillProjectileQuantityBuff;                             //技能投射物数量

    public float repelPowerBuff;                                        //推开力度

    public float retardPowerBuff;                                       //减速程度

    public float retardTmieBuff;                                        //减速时间
}

[System.Serializable]
public class SkillAttribute
{
    public int skillNum;                                             //技能生成器的序号  

    public string skillSpanwerName;                                  //技能生成器的名字

    public int grade;                                                //当前技能等级

    public double duration;                                          //技能持续时间

    public double coolDown;                                          //技能冷却时间

    public double damageCoolDown;                                    //伤害冷却

    public double launchMoveSpeed;                                   //发射速度

    public double searchEnemyRange;                                  //索敌范围

    public int skillProjectileQuantity;                              //技能投射物数量

    public double attackRange;                                       //攻击范围

    public double attackDamage;                                      //基础攻击力

    public bool isRepel;                                             //击中推开敌人

    public bool isRetard;                                            //击中减速敌人

    public double repelPower;                                        //推开力度

    public double retardPower;                                       //减速程度

    public double retardTime;                                        //减速时间
}

[System.Serializable]
public class SkillData
{
    public UnityEngine.LayerMask skillAttackMask;                   //技能攻击图层

    public AssetReference skillObj;                                 //技能资源预制体

    public bool isOnce;                                             //是否为仅生成一次技能

    public bool isCenter;                                           //角色中心点生成

    public bool isSole;                                             //角色脚底生成

    public SkillAttribute skillAttribute;                           //基础数值

    public SkillAttributeBuff[] skillAttributeBuff;                 //每一级数值增益
}