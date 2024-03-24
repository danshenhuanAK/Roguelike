using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RoleData/EnemyData")]
public class EnemyData_SO : ScriptableObject
{
    public string enemyName;                                 //��������

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
public class EnemyDataList
{
    public List<EnemyData_SO> enmeyDatas = new();
}
