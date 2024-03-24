using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RoleData/PlayerData")]
public class PlayerData_SO : ScriptableObject
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
