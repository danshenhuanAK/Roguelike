using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RoleData/PlayerData")]
public class PlayerData_SO : ScriptableObject
{
    public double health;                                                //����ֵ

    public double maxHealth;                                             //�������

    public double healthRegen;                                           //�����ָ�

    public double defence;                                               //����ֵ

    public double moveSpeed;                                             //�ƶ��ٶ�

    public double attackPower;                                           //������

    public double launchMoveSpeed;                                       //���似���ƶ��ٶ�

    public double duration;                                              //���ܳ���ʱ��ӳ�

    public double attackRange;                                           //���ܹ�����Χ�ӳ�

    public double skillCoolDown;                                         //������ȴʱ��ӳ�

    public int projectileQuantity;                                       //���������ӳ�

    public int revival;                                                  //�������

    public double magnet;                                                //ʰȡ��Χ

    public double critical;                                              //������

    public double criticalDamage;                                        //�����˺�

    public double level;                                                 //�ȼ�

    public int experienceCap;                                            //�������辭��                                

    public double experience;                                            //����

    public double experienceCquisitionSpeed;                             //���������ٶ�

    public int luck;                                                     //����ֵ
}
