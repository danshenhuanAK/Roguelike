using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour
{
    private FightProgressAttributeManager fightManager;

    private void Awake()
    {
        fightManager = FightProgressAttributeManager.Instance;
    }

    public void HealthRegen(float value)                    //�����ظ�
    {
        fightManager.playerData.healthRegen = Mathf.Max((float)fightManager.playerData.healthRegen + value, 0);
    }

    public void Health(float value)                         //���ӻ�������ﵱǰ����ֵ
    {
        fightManager.playerData.health = Mathf.Min((float)fightManager.playerData.health + value, (float)fightManager.playerData.maxHealth);
    }

    public void ChangeHealth(float value)                   //ֱ�ӱ仯���ﵱǰ����ֵ
    {
        fightManager.playerData.health = value;
    }

    public void MaxHealth(float value)                      //���ӻ������������
    {
        fightManager.playerData.maxHealth += value;

        if (fightManager.playerData.health > fightManager.playerData.maxHealth)
        {
            fightManager.playerData.health = fightManager.playerData.maxHealth;
        }
    }

    public void Defence(float value)                        //���ӻ���ٷ���ֵ
    {
        fightManager.playerData.defence = Mathf.Max((float)fightManager.playerData.defence + value, 0);
    }

    public void MoveSpeed(float value)                      //���ӻ�����ƶ��ٶ�
    {
        fightManager.playerData.moveSpeed = Mathf.Max((float)fightManager.playerData.moveSpeed + value, 0);
    }

    public void AttackPower(float value)                    //���ӻ���ٹ�����
    {
        fightManager.playerData.attackPower = Mathf.Max((float)fightManager.playerData.attackPower + value, 0);
    }

    public void LaunchMoveSpeed(float value)                //���ӻ���ټ��ܵ����ٶ�
    {
        fightManager.playerData.launchMoveSpeed = Mathf.Max((float)fightManager.playerData.launchMoveSpeed + value, 0);
    }

    public void Duration(float value)                       //���ӻ���ټ��ܳ���ʱ��
    {
        fightManager.playerData.duration = Mathf.Max((float)fightManager.playerData.duration + value, 0);
    }
    public void AttackRange(float value)                    //���ӻ���ټ��ܹ�����Χ
    {
        fightManager.playerData.attackRange = Mathf.Max((float)fightManager.playerData.attackRange + value, 0);
    }
    public void SkillCoolDown(float value)                  //���ӻ���ټ�����ȴ
    {
        fightManager.playerData.skillCoolDown = Mathf.Max((float)fightManager.playerData.skillCoolDown + value, 0);
    }
    public void ProjectileQuantity(int value)             //���ӻ���ټ��ܷ�������
    {
        fightManager.playerData.projectileQuantity = Mathf.Max(fightManager.playerData.projectileQuantity + value, 0);
    }
    public void Revival(int value)                          //���ӻ���ٸ������
    {
        fightManager.playerData.revival = Mathf.Max(fightManager.playerData.revival + value, 0);
    }
    public void Magnet(float value)                         //���ӻ����ʰȡ��Χ
    {
        fightManager.playerData.magnet = Mathf.Max((float)fightManager.playerData.magnet + value, 0.1f);
    }
    public void Critical(float value)                       //���ӻ���ٱ�����
    {
        fightManager.playerData.critical = Mathf.Max((float)fightManager.playerData.critical + value, 0);
    }
    public void CriticalDamage(float value)                 //���ӻ���ٱ����˺�
    {
        fightManager.playerData.criticalDamage = Mathf.Max((float)fightManager.playerData.criticalDamage + value, 0);
    }

    public void ExperienceCquisitionSpeed(float value)          //���ӻ���پ����ȡ����
    {
        fightManager.playerData.experienceCquisitionSpeed = Mathf.Max((float)fightManager.playerData.experienceCquisitionSpeed + value, 0);
    }

    public void Luck(int value)                             //���ӻ��������ֵ
    {
        fightManager.playerData.luck = Mathf.Max(fightManager.playerData.luck + value, 0);
    }

    public void Gold(int value)                             //���ӻ������Ϸ���
    {
        fightManager.gameFightData.gold = Mathf.Max(fightManager.gameFightData.gold + value, 0);
    }
}
