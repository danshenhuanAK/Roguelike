using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "ScriptableObject/SkillData/PlayerSkillData")]
public class PlayerSkillData_SO : ScriptableObject
{
    public string skillSpanwerName;                                  //����������������

    public int grade;                                                //��ǰ���ܵȼ�

    public double duration;                                          //���ܳ���ʱ��

    public double coolDown;                                          //������ȴʱ��

    public double damageCoolDown;                                    //�˺���ȴ

    public double launchMoveSpeed;                                   //�����ٶ�

    public double searchEnemyRange;                                  //���з�Χ

    public int skillProjectileQuantity;                              //����Ͷ��������

    public double attackRange;                                       //������Χ

    public double attackDamage;                                      //����������

    public bool isRepel;                                             //�����ƿ�����

    public bool isRetard;                                            //���м��ٵ���

    public double repelPower;                                        //�ƿ�����

    public double retardPower;                                       //���ٳ̶�

    public double retardTime;                                        //����ʱ��
}

[System.Serializable]
public class PlayerSkillDataList
{
    public List<PlayerSkillData_SO> playerSkillDatas = new();
}

public enum SkillSearchEnemyPos                                 //��������λ�����ĵ�
{
    Center,         //��ɫ����

    Sole,           //��ɫ�ŵ�

    Top             //��ɫͷ��
}

[System.Serializable]
public class PlayerSkillData
{
    public LayerMask skillAttackMask;                                           //���ܿ��Թ�����ͼ��

    public AssetReference skillAsset;                                           //������Դ

    public bool isOnce;                                                         //�Ƿ�Ϊһ�����ɵ�

    public SkillSearchEnemyPos searchEnemyPos;                                  //��������λ�����ĵ�

    public PlayerSkillData_SO playerBaseSkillData;                              //���ܻ�����ֵ

    public PlayerSkillData_SO playerCurrentSkillData;                           //���ܵ�ǰ��ֵ

    public List<PlayerSkillBuffData_SO> playerSkillBuffDataList = new();        //����������ֵ�б�
}
