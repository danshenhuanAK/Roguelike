using UnityEngine;

[System.Serializable]
public class GameCurrentAttribute
{
    public float timer;

    public int second;

    public int minute;

    public int gold;
}

[System.Serializable]
public class CurrentAttribute
{
    public float health;                                                //����ֵ

    public float maxHealth;                                             //�������

    public float healthRegen;                                           //�����ָ�

    public float defence;                                               //����ֵ

    public float moveSpeed;                                             //�ƶ��ٶ�

    public float attackPower;                                           //������

    public float launchMoveSpeed;                                       //���似���ƶ��ٶ�

    public float duration;                                              //���ܳ���ʱ��ӳ�

    public float attackRange;                                           //���ܹ�����Χ�ӳ�

    public float skillCoolDown;                                         //������ȴʱ��ӳ�

    public float projectileQuantity;                                    //���������ӳ�

    public float revival;                                               //�������

    public float magnet;                                                //ʰȡ��Χ

    public float critical;                                              //������

    public float criticalDamage;                                        //�����˺�

    public float level;                                                 //�ȼ�

    public int experienceCap;                                           //�������辭��                                

    public float experience;                                            //����

    public float experienceCquisitionSpeed;                             //���������ٶ�

    public float luck;                                                  //����ֵ

    public int reroll;                                                  //��ѡ����

    public int skip;                                                    //��������

    public int banish;                                                  //�ų�����
}

[System.Serializable]
public class EnemyLevelData
{
    public float maxHealth;

    public float defence;

    public float moveSpeed;

    public float attackDamage;
}

[System.Serializable]
public class EnemyCurrentAttribute
{
    public float maxHealth;                                 //�������ֵ

    public float currentHealth;                             //��ǰ����ֵ

    public float defence;                                   //����ֵ

    public float moveSpeed;                                 //�ƶ��ٶ�

    public float attackDamage;                              //�����˺�

    public float coolDown;                                  //�������

    public bool isAttack;                                   //�Ƿ񹥻�

    public bool isBoss;                                     //�Ƿ�Ϊ��Ӣ��
}

[System.Serializable]
public class EnemyCurrentAttributes
{
    public UnityEngine.GameObject enemyPrefab;

    public EnemyCurrentAttribute enemyCurrentAttributes;
}

[System.Serializable]
public class SkillAttribute
{
    public int skillGrade;                                          //���ܵȼ�

    public float duration;                                          //���ܳ���ʱ��

    public float coolDown;                                          //������ȴʱ��

    public float cdRemain;                                          //����ʣ����ȴʱ��

    public float damageCoolDown;                                    //�˺���ȴ

    public float launchMoveSpeed;                                   //�����ٶ�

    public float attackRange;                                       //������Χ

    public float attackDamage;                                      //����������

    public int skillProjectileQuantity;                             //����Ͷ��������

    public float skillScale;                                        //���ܴ�С
}

[System.Serializable]
public class SkillData
{
    public UnityEngine.LayerMask skillAttackMask;                   //���ܹ���ͼ��

    public UnityEngine.GameObject skillObject;                      //����Ԥ����

    public SkillAttribute[] skillAttribute;
}