using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Skill Attack Data")]
public class SkillAttackData_SO : ScriptableObject
{
    public LayerMask attackMask;                //技能攻击图层

    public GameObject skillObject;              //技能预制体

    public float baseDuration;                  //技能基础持续时间

    public float currentDuration;               //技能当前持续时间

    public float baseCoolDown;                  //基础技能冷却时间

    public float currentCoolDown;               //当前技能冷却时间

    public float cdRemain;                      //技能剩余冷却时间

    public float baseDamageCoolDown;            //基础伤害冷却

    public float currentDamageCoolDown;         //当前伤害冷却

    public float baselaunchMoveSpeed;           //基础发射速度

    public float launchMoveSpeed;               //发射速度

    public float attackRange;                   //攻击范围

    public float baseAttackDamage;              //基础攻击力

    public float currentAttackDamage;           //当前攻击力

    public int baseProjectileQuantity;          //基础投射物数量

    public int currentProjectileQuantity;       //当前投射物数量

    public int baseGrade;                       //基础等级

    public int currentGrade;                    //当前等级

    public Vector2 baseScale;                   //基础大小

    public Vector2 currentScale;                //当前大小
}
