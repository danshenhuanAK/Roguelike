[System.Serializable]
public class GameBaseAttribute
{
    public float timer = 0;                                         //��Ϸʱ��

    public int second = 0;                                          //��Ϸʱ�䣨�룩

    public int minute = 0;                                          //��Ϸʱ�䣨�֣�

    public int gold = 0;                                            //���
}

public class GameCurrentAttribute
{
    public float timer;

    public int second;

    public int minute;

    public int gold;
}

[System.Serializable]
public class BaseAttribute
{
    public float health = 100;                                  //����ֵ

    public float maxHealth = 100;                               //�������ֵ

    public float healthRegen = 0;                               //�����ظ�

    public float defence = 1;                                   //����ֵ

    public float moveSpeed = 10;                                //�ƶ��ٶ�

    public float attackPower = 0;                               //����������

    public float launchMoveSpeed = 0;                           //�����ٶ�

    public float duration = 5;                                  //����ʱ��

    public float attackRange = 10;                              //������Χ

    public float skillCoolDown = 10;                            //��ȴʱ��

    public float projectileQuantity = 0;                        //Ͷ��������

    public float revival = 1;                                   //�������

    public float magnet = 0;                                    //ʰȡ��Χ

    public float critical = 0;                                  //������

    public float criticalDamage = 100;                          //�����˺�

    public float level = 1;                                     //�ȼ�

    public int experienceCap = 100;                             //�������辭��

    public float experience = 0;                                //����ֵ

    public float experienceCquisitionSpeed = 0;                 //�����ȡ����

    public float luck = 0;                                      //����ֵ

    public int reroll = 3;                                      //��ѡ

    public int skip = 3;                                        //����

    public int banish = 3;                                      //�ų�
}

[System.Serializable]
public class CurrentAttribute
{
    public float health;

    public float maxHealth;

    public float healthRegen;

    public float defence;

    public float moveSpeed;

    public float attackPower;

    public float launchMoveSpeed;

    public float duration;

    public float attackRange;

    public float skillCoolDown;

    public float projectileQuantity;

    public float revival;

    public float magnet;

    public float critical;

    public float criticalDamage;

    public float level;

    public int experienceCap;

    public float experience;

    public float experienceCquisitionSpeed;

    public float luck;

    public int reroll;

    public int skip;

    public int banish;
}