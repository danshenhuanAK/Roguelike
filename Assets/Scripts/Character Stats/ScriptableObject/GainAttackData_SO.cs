using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Skill Gain Data")]
public class GainAttackData_SO : ScriptableObject
{
    public float launchMoveSpeedBuff;                       //�����ٶ�����

    public float attackRangeBuff;                           //������Χ����

    public float attackPowerBuff;                           //��������������

    public float criticalBuff;                              //����������
        
    public float criticalDamageBuff;                        //�����˺�����

    public float projectileQuantity;                        //��ǰͶ��������
}
