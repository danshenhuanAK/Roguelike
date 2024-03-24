using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillData/PlayerSkillBuffData")]
public class PlayerSkillBuffData_SO : ScriptableObject
{
    public float durationBuff;                                          //���ܳ���ʱ��

    public float coolDownBuff;                                          //������ȴʱ��

    public float damageCoolDownBuff;                                    //�˺���ȴ

    public float launchMoveSpeedBuff;                                   //�����ٶ�

    public float searchEnemyRangeBuff;                                  //���з�Χ

    public float attackRangeBuff;                                       //������Χ

    public float attackDamageBuff;                                      //����������

    public int skillProjectileQuantityBuff;                              //����Ͷ��������

    public float repelPowerBuff;                                        //�ƿ�����

    public float retardPowerBuff;                                       //���ٳ̶�

    public float retardTmieBuff;                                        //����ʱ��
}
