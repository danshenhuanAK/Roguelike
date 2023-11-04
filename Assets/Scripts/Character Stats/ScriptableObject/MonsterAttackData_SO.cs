using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Player Attack Data")]
public class MonsterAttackData_SO : ScriptableObject
{
    public bool isAttack;                       //�Ƿ񹥻�

    public float coolDown;                      //�������

    public float moveSpeed;                     //�ƶ��ٶ�

    public float baseAttackDamage;              //����������

    public float currentAttackDamage;           //��ǰ������

    public int baseGrade;                       //�����ȼ�

    public int currentGrade;                    //��ǰ�ȼ�
}
