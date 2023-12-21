using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Monster Attack Data")]
public class MonsterAttackData_SO : ScriptableObject
{
    public bool isAttack;                       //ÊÇ·ñ¹¥»÷

    public float coolDown;                      //¹¥»÷¼ä¸ô

    public float baseAttackDamage;              //»ù´¡¹¥»÷Á¦

    public float currentAttackDamage;           //µ±Ç°¹¥»÷Á¦
}
