using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Monster Data")]
public class MonsterAttributeData_SO : ScriptableObject
{
    public int startTime;                                   //难度开始时间

    public int endTime;                                     //难度结束时间

    public float maxHealth;                                 //最大生命值

    public float currentHealth;                             //当前生命值

    public float monsterDefence;                            //防御值
            
    public float monsterMoveSpeed;                          //移动速度

    public float monstetAttackDamage;                       //攻击伤害

    public float coolDown;                                  //攻击间隔

    public bool isAttack;                                   //是否攻击
}