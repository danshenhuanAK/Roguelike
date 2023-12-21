using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Monster Attack Data")]
public class MonsterAttackData_SO : ScriptableObject
{
    public bool isAttack;                       //�Ƿ񹥻�

    public float coolDown;                      //�������

    public float baseAttackDamage;              //����������

    public float currentAttackDamage;           //��ǰ������
}
