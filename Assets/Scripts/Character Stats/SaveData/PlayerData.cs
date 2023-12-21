using System;

[Serializable]
public class PlayerData
{
    internal float timer;                                         //��Ϸʱ��

    internal int second;                                          //��Ϸʱ�䣨�룩

    internal int minute;                                          //��Ϸʱ�䣨�֣�

    internal int gold;                                            //���

    internal float health = 100;                                  //����ֵ

    internal float maxHealth = 100;                               //�������ֵ

    internal float healthRegen = 0;                               //�����ظ�

    internal float defence = 1;                                   //����ֵ

    internal float moveSpeed = 10;                                //�ƶ��ٶ�

    internal float attackPower = 0;                               //����������

    internal float launchMoveSpeed = 0;                           //�����ٶ�

    internal float duration = 5;                                  //����ʱ��

    internal float attackRange = 10;                              //������Χ

    internal float skillCoolDown = 10;                            //��ȴʱ��

    internal float projectileQuantity = 0;                        //Ͷ��������

    internal float revival = 1;                                   //�������

    internal float magnet = 0;                                    //ʰȡ��Χ

    internal float critical = 0;                                  //������

    internal float criticalDamage = 100;                          //�����˺�

    internal float level = 1;                                     //�ȼ�

    internal int experienceCap = 100;                             //�������辭��

    internal float experience = 0;                                //����ֵ

    internal float experienceCquisitionSpeed = 0;                 //�����ȡ����

    internal float luck = 0;                                      //����ֵ

    internal int reroll = 3;                                      //��ѡ

    internal int skip = 3;                                        //����

    internal int banish = 3;                                      //�ų�
}
