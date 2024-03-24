using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "ScriptableObject/SkillData/PlayerSkillData")]
public class PlayerSkillData_SO : ScriptableObject
{
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
public class PlayerSkillDataList
{
    public List<PlayerSkillData_SO> playerSkillDatas = new();
}

public enum SkillSearchEnemyPos                                 //技能索敌位置中心点
{
    Center,         //角色中心

    Sole,           //角色脚底

    Top             //角色头顶
}

[System.Serializable]
public class PlayerSkillData
{
    public LayerMask skillAttackMask;                                           //技能可以攻击的图层

    public AssetReference skillAsset;                                           //技能资源

    public bool isOnce;                                                         //是否为一次生成的

    public SkillSearchEnemyPos searchEnemyPos;                                  //技能索敌位置中心点

    public PlayerSkillData_SO playerBaseSkillData;                              //技能基础数值

    public PlayerSkillData_SO playerCurrentSkillData;                           //技能当前数值

    public List<PlayerSkillBuffData_SO> playerSkillBuffDataList = new();        //技能增益数值列表
}
