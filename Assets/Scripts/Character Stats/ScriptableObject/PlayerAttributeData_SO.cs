using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Player Data")]
public class PlayerAttributeData_SO : ScriptableObject
{
    public float health;                                    //����ֵ

    public float maxHealth;                                 //�������ֵ

    public float healthRegen;                               //�����ظ�

    public float defence;                                   //����ֵ

    public float moveSpeed;                                 //�ƶ��ٶ�

    public float attackPowerBuff;                           //����������

    public float launchMoveSpeedBuff;                       //�����ٶ�

    public float duration;                                  //����ʱ��

    public float attackRangeBuff;                           //������Χ

    public float skillCoolDown;                             //��ȴʱ��

    public float projectileQuantity;                        //Ͷ��������

    public float revival;                                   //�������

    public float magnet;                                    //ʰȡ��Χ

    public float criticalBuff;                              //������

    public float criticalDamageBuff;                        //�����˺�

    public float experienceCquisitionSpeed;                 //�����ȡ����

    public float luck;                                      //����ֵ

    public int reroll;                                      //��ѡ

    public int skip;                                        //����

    public int banish;                                      //�ų�
}
