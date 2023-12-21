using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Monster Data")]
public class MonsterAttributeData_SO : ScriptableObject
{
    public int startTime;                                   //�Ѷȿ�ʼʱ��

    public int endTime;                                     //�ѶȽ���ʱ��

    public float maxHealth;                                 //�������ֵ

    public float currentHealth;                             //��ǰ����ֵ

    public float monsterDefence;                            //����ֵ
            
    public float monsterMoveSpeed;                          //�ƶ��ٶ�

    public float monstetAttackDamage;                       //�����˺�

    public float coolDown;                                  //�������

    public bool isAttack;                                   //�Ƿ񹥻�
}