using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack Data", menuName = "Attack/Skill Attack Data")]
public class SkillAttackData_SO : ScriptableObject
{
    public LayerMask attackMask;                //���ܹ���ͼ��

    public GameObject skillObject;              //����Ԥ����

    public float baseDuration;                  //���ܻ�������ʱ��

    public float currentDuration;               //���ܵ�ǰ����ʱ��

    public float baseCoolDown;                  //����������ȴʱ��

    public float currentCoolDown;               //��ǰ������ȴʱ��

    public float cdRemain;                      //����ʣ����ȴʱ��

    public float baseDamageCoolDown;            //�����˺���ȴ

    public float currentDamageCoolDown;         //��ǰ�˺���ȴ

    public float baselaunchMoveSpeed;           //���������ٶ�

    public float launchMoveSpeed;               //�����ٶ�

    public float attackRange;                   //������Χ

    public float baseAttackDamage;              //����������

    public float currentAttackDamage;           //��ǰ������

    public int baseProjectileQuantity;          //����Ͷ��������

    public int currentProjectileQuantity;       //��ǰͶ��������

    public int baseGrade;                       //�����ȼ�

    public int currentGrade;                    //��ǰ�ȼ�

    public Vector2 baseScale;                   //������С

    public Vector2 currentScale;                //��ǰ��С
}
