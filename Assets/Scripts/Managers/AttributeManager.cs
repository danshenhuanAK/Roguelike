using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class AttributeManager : Singleton<AttributeManager>
{
    public GameCurrentAttribute gameCurrentAttribute = new ();
    public CurrentAttribute currentAttribute = new ();
    public EnemyLevelData enemyLevelData = new ();

    protected override void Awake()
    {
        RestoreGameAttribute();
        RestoreAttribute();
        base.Awake();
    }

    public string GetJsonForAssets(string fileName)
    {
        string jsonPath;

        if(Application.platform == RuntimePlatform.Android)
        {
            jsonPath = Application.persistentDataPath + "/" + fileName;
        }
        else
        {
            jsonPath = Application.dataPath + "/Resources" + "/Json" + "/" + fileName;
        }

        string data = File.ReadAllText(jsonPath);

        return data;
    }

    public void RestoreAttribute()
    {
        currentAttribute = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerAttribute;
    }

    public void RestoreGameAttribute()
    {
        gameCurrentAttribute.timer = 0;
        gameCurrentAttribute.second = 0;
        gameCurrentAttribute.minute = 0;
        gameCurrentAttribute.gold = 0;
    }

    public void RestoreEnemyLevel()
    {
        enemyLevelData.maxHealth = 0;
        enemyLevelData.defence = 0;
        enemyLevelData.attackDamage = 0;
        enemyLevelData.moveSpeed = 0;
    }

    public void SkillDamage(SkillAttribute skillAttribute, EnemyCurrentAttribute enemyAttribute)                    //技能对怪物造成的伤害
    {
        float damage = Mathf.Max(skillAttribute.attackDamage + currentAttribute.attackPower - enemyAttribute.defence);

        if(Random.Range(0f,1f) >= currentAttribute.critical)
        {
            damage *= 2;
        }

        enemyAttribute.currentHealth -= damage;
    }

    public void MonsterDamage(EnemyCurrentAttribute enemyData)                  //怪物对主角造成伤害
    {
        float damage = Mathf.Max(enemyData.attackDamage + enemyLevelData.attackDamage - currentAttribute.defence, 0.1f);

        currentAttribute.health -= damage;
    }

    #region PlayerBuff
    public void HealthRegen(float value)
    {
        currentAttribute.healthRegen = Mathf.Max(currentAttribute.healthRegen + value, 0);
    }

    public void Health(float value)
    {
        currentAttribute.health = Mathf.Min(currentAttribute.health + value, currentAttribute.maxHealth);
    }

    public void ChangeHealth(float value)
    {
        currentAttribute.health = value;
    }

    public void MaxHealth(float value)
    {
        currentAttribute.maxHealth += value;

        if (currentAttribute.health > currentAttribute.maxHealth)
        {
            currentAttribute.health = currentAttribute.maxHealth;
        }
    }

    public void Defence(float value)
    {
        currentAttribute.defence = Mathf.Max(currentAttribute.defence + value, 0);
    }

    public void MoveSpeed(float value)
    {
        currentAttribute.moveSpeed = Mathf.Max(currentAttribute.moveSpeed + value, 0);
    }

    public void AttackPower(float value)
    {
        currentAttribute.attackPower = Mathf.Max(currentAttribute.attackPower + value, 0);
    }

    public void LaunchMoveSpeed(float value)
    {
        currentAttribute.launchMoveSpeed = Mathf.Max(currentAttribute.launchMoveSpeed + value, 0);
    }

    public void Duration(float value)
    {
        currentAttribute.duration = Mathf.Max(currentAttribute.duration + value, 0);
    }
    public void AttackRange(float value)
    {
        currentAttribute.attackRange = Mathf.Max(currentAttribute.attackRange + value, 0);
    }
    public void SkillCoolDown(float value)
    {
        currentAttribute.skillCoolDown = Mathf.Max(currentAttribute.skillCoolDown + value, 0);
    }
    public void ProjectileQuantity(float value)
    {
        currentAttribute.projectileQuantity = Mathf.Max(currentAttribute.projectileQuantity + value, 0);
    }
    public void Revival(float value)
    {
        currentAttribute.revival = Mathf.Max(currentAttribute.revival + value, 0);
    }
    public void Magnet(float value)
    {
        currentAttribute.magnet = Mathf.Max(currentAttribute.magnet + value, 0.1f);
    }
    public void Critical(float value)
    {
        currentAttribute.critical = Mathf.Max(currentAttribute.critical + value, 0);
    }
    public void CriticalDamage(float value)
    {
        currentAttribute.criticalDamage = Mathf.Max(currentAttribute.criticalDamage + value, 0);
    }

    public void Luck(float value)
    {
        currentAttribute.luck = Mathf.Max(currentAttribute.luck + value, 0);
    }

    public void Gold(int value)
    {
        gameCurrentAttribute.gold = Mathf.Max(gameCurrentAttribute.gold + value, 0);
    }
    #endregion
}
