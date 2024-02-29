using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Monster Data")]
public class MonsterAttributeData_SO : ScriptableObject
{
    public int startTime;                                   //�Ѷȿ�ʼʱ��

    public int endTime;                                     //�ѶȽ���ʱ��

    public float maxHealth;                                 //�������ֵ

    public float currentHealth;                             //��ǰ����ֵ

    public float defence;                                   //����ֵ

    public float moveSpeed;                                 //�ƶ��ٶ�

    public float attackDamage;                              //�����˺�

    public float coolDown;                                  //�������

    public bool isAttack;                                   //�Ƿ񹥻�

    public bool isBoss;                                     //�Ƿ�Ϊ��Ӣ��
}