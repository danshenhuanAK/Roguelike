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

    public void HealthRegen(float value)                    //生命回复
    {
        fightManager.playerData.healthRegen = Mathf.Max((float)fightManager.playerData.healthRegen + value, 0);
    }

    public void Health(float value)                         //增加或减少人物当前生命值
    {
        fightManager.playerData.health = Mathf.Min((float)fightManager.playerData.health + value, (float)fightManager.playerData.maxHealth);
    }

    public void ChangeHealth(float value)                   //直接变化人物当前生命值
    {
        fightManager.playerData.health = value;
    }

    public void MaxHealth(float value)                      //增加或减少生命上限
    {
        fightManager.playerData.maxHealth += value;

        if (fightManager.playerData.health > fightManager.playerData.maxHealth)
        {
            fightManager.playerData.health = fightManager.playerData.maxHealth;
        }
    }

    public void Defence(float value)                        //增加或减少防御值
    {
        fightManager.playerData.defence = Mathf.Max((float)fightManager.playerData.defence + value, 0);
    }

    public void MoveSpeed(float value)                      //增加或减少移动速度
    {
        fightManager.playerData.moveSpeed = Mathf.Max((float)fightManager.playerData.moveSpeed + value, 0);
    }

    public void AttackPower(float value)                    //增加或减少攻击力
    {
        fightManager.playerData.attackPower = Mathf.Max((float)fightManager.playerData.attackPower + value, 0);
    }

    public void LaunchMoveSpeed(float value)                //增加或减少技能弹道速度
    {
        fightManager.playerData.launchMoveSpeed = Mathf.Max((float)fightManager.playerData.launchMoveSpeed + value, 0);
    }

    public void Duration(float value)                       //增加或减少技能持续时间
    {
        fightManager.playerData.duration = Mathf.Max((float)fightManager.playerData.duration + value, 0);
    }
    public void AttackRange(float value)                    //增加或减少技能攻击范围
    {
        fightManager.playerData.attackRange = Mathf.Max((float)fightManager.playerData.attackRange + value, 0);
    }
    public void SkillCoolDown(float value)                  //增加或减少技能冷却
    {
        fightManager.playerData.skillCoolDown = Mathf.Max((float)fightManager.playerData.skillCoolDown + value, 0);
    }
    public void ProjectileQuantity(int value)             //增加或减少技能发射数量
    {
        fightManager.playerData.projectileQuantity = Mathf.Max(fightManager.playerData.projectileQuantity + value, 0);
    }
    public void Revival(int value)                          //增加或减少复活次数
    {
        fightManager.playerData.revival = Mathf.Max(fightManager.playerData.revival + value, 0);
    }
    public void Magnet(float value)                         //增加或减少拾取范围
    {
        fightManager.playerData.magnet = Mathf.Max((float)fightManager.playerData.magnet + value, 0.1f);
    }
    public void Critical(float value)                       //增加或减少暴击率
    {
        fightManager.playerData.critical = Mathf.Max((float)fightManager.playerData.critical + value, 0);
    }
    public void CriticalDamage(float value)                 //增加或减少暴击伤害
    {
        fightManager.playerData.criticalDamage = Mathf.Max((float)fightManager.playerData.criticalDamage + value, 0);
    }

    public void ExperienceCquisitionSpeed(float value)          //增加或减少经验获取速率
    {
        fightManager.playerData.experienceCquisitionSpeed = Mathf.Max((float)fightManager.playerData.experienceCquisitionSpeed + value, 0);
    }

    public void Luck(int value)                             //增加或减少幸运值
    {
        fightManager.playerData.luck = Mathf.Max(fightManager.playerData.luck + value, 0);
    }

    public void Gold(int value)                             //增加或减少游戏金币
    {
        fightManager.gameFightData.gold = Mathf.Max(fightManager.gameFightData.gold + value, 0);
    }
}
