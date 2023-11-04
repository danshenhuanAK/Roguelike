using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Player Attack Data")]
public class GainAttackData_SO : ScriptableObject
{
    public bool isAttack;                           //�Ƿ񹥻�

    public float baseLaunchMoveSpeed;               //�����ٶ�

    public float launchMoveSpeedBuff;               //�����ٶ�����

    public float attackRangeBuff;                   //������Χ����

    public float baseAttackPower;                   //����������

    public float attackPowerBuff;                   //��������������

    public float criticalBuff;                      //����������
        
    public float criticalDamageBuff;                //�����˺�����

    public float damageMultiplier;                  //��ǰ�˺�ϵ������

    public float baseProjectileQuantity;            //����Ͷ��������

    public float currentProjectileQuantity;         //��ǰͶ��������

    public int baseGrade;                           //�����ȼ�

    public int currentGrade;                        //��ǰ�ȼ�
}
