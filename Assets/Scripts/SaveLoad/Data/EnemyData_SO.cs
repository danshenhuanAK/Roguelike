using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RoleData/EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    public string enemyName;                                 //怪物名字

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
public class EnemyDataList
{
    public List<EnemyData_SO> enmeyDatas = new();
}
