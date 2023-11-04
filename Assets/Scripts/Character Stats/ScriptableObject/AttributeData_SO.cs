using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Data")]
public class AttributeData_SO : ScriptableObject
{
    public int baseGrade;                               //�����ȼ�

    public int currentGrade;                            //��ǰ�ȼ�

    public float maxHealth;                             //�������ֵ

    public float currentHealth;                         //��ǰ����ֵ

    public float baseDefence;                           //��������ֵ

    public float currentDefence;                        //��ǰ����ֵ

    public float baseHealthRegen;                       //���������ظ�

    public float currentHealthRegen;                    //��ǰ�����ظ�

    public float baseMoveSpeed;                         //�����ƶ��ٶ�

    public float currentMoveSpeed;                      //��ǰ�ƶ��ٶ�

    public float maximumExperience;                     //�����

    public float currentExperience;                     //��ǰ����

    public float baseExperienceCquisitionSpeed;         //���������ȡ����

    public float currentExperienceCquisitionSpeed;      //��ǰ�����ȡ����

    public float damageReduction;                       //�˺�����
}
