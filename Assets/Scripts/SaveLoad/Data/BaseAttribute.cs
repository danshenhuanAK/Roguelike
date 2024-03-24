using UnityEngine.AddressableAssets;

[System.Serializable]
public class GameFightAttribute
{
    public double timer;

    public int second;

    public int minute;

    public int kills;

    public int gold;
}

[System.Serializable]
public class CurrentAttribute
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
    public double maxHealth;                                 //�������ֵ

    public double currentHealth;                             //��ǰ����ֵ

    public double defence;                                   //����ֵ

    public double moveSpeed;                                 //�ƶ��ٶ�

    public double attackDamage;                              //�����˺�

    public double coolDown;                                  //�������

    public bool isAttack;                                    //�Ƿ񹥻�

    public bool isBoss;                                      //�Ƿ�Ϊ��Ӣ��
}

[System.Serializable]
public class EnemyCurrentAttributes
{
    public AssetReferenceGameObject enemyPre;

    public EnemyData_SO enmeyData;

    public EnemyCurrentAttribute enemyCurrentAttributes;
}

[System.Serializable]
public class SkillAttributeBuff
{
    public float durationBuff;                                          //���ܳ���ʱ��

    public float coolDownBuff;                                          //������ȴʱ��

    public float damageCoolDownBuff;                                    //�˺���ȴ

    public float launchMoveSpeedBuff;                                   //�����ٶ�

    public float searchEnemyRangeBuff;                                  //���з�Χ

    public float attackRangeBuff;                                       //������Χ

    public float attackDamageBuff;                                      //����������

    public int skillProjectileQuantityBuff;                             //����Ͷ��������

    public float repelPowerBuff;                                        //�ƿ�����

    public float retardPowerBuff;                                       //���ٳ̶�

    public float retardTmieBuff;                                        //����ʱ��
}

[System.Serializable]
public class SkillAttribute
{
    public int skillNum;                                             //���������������  

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
public class SkillData
{
    public UnityEngine.LayerMask skillAttackMask;                   //���ܹ���ͼ��

    public AssetReference skillObj;                                 //������ԴԤ����

    public bool isOnce;                                             //�Ƿ�Ϊ������һ�μ���

    public bool isCenter;                                           //��ɫ���ĵ�����

    public bool isSole;                                             //��ɫ�ŵ�����

    public SkillAttribute skillAttribute;                           //������ֵ

    public SkillAttributeBuff[] skillAttributeBuff;                 //ÿһ����ֵ����
}