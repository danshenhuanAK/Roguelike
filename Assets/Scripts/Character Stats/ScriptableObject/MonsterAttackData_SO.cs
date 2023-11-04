using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Player Attack Data")]
public class MonsterAttackData_SO : ScriptableObject
{
    public bool isAttack;                       //是否攻击

    public float coolDown;                      //攻击间隔

    public float moveSpeed;                     //移动速度

    public float baseAttackDamage;              //基础攻击力

    public float currentAttackDamage;           //当前攻击力

    public int baseGrade;                       //基础等级

    public int currentGrade;                    //当前等级
}
