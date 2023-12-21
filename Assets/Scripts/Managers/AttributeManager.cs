using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeManager : Singleton<AttributeManager>
{
    public GameBaseAttribute gameBaseAttribute = new();
    public GameCurrentAttribute gameCurrentAttribute = new();
    public BaseAttribute baseAttribute = new();
    public CurrentAttribute currentAttribute = new();

    protected override void Awake()
    {
        base.Awake();

        RestoreAttribute();
        RestoreGameAttribute();
    }

    public void RestoreAttribute()
    {
        currentAttribute.health = baseAttribute.health;
        currentAttribute.maxHealth = baseAttribute.maxHealth;
        currentAttribute.healthRegen = baseAttribute.healthRegen;
        currentAttribute.defence = baseAttribute.defence;
        currentAttribute.moveSpeed = baseAttribute.moveSpeed;
        currentAttribute.attackPower = baseAttribute.attackPower;
        currentAttribute.launchMoveSpeed = baseAttribute.launchMoveSpeed;
        currentAttribute.duration = baseAttribute.duration;
        currentAttribute.attackRange = baseAttribute.attackRange;
        currentAttribute.skillCoolDown = baseAttribute.skillCoolDown;
        currentAttribute.projectileQuantity = baseAttribute.projectileQuantity;
        currentAttribute.revival = baseAttribute.revival;
        currentAttribute.magnet = baseAttribute.magnet;
        currentAttribute.critical = baseAttribute.critical;
        currentAttribute.criticalDamage = baseAttribute.criticalDamage;
        currentAttribute.level = baseAttribute.level;
        currentAttribute.experience = baseAttribute.experience;
        currentAttribute.experienceCap = baseAttribute.experienceCap;
        currentAttribute.experienceCquisitionSpeed = baseAttribute.experienceCquisitionSpeed;
        currentAttribute.luck = baseAttribute.luck;
        currentAttribute.reroll = baseAttribute.reroll;
        currentAttribute.skip = baseAttribute.skip;
        currentAttribute.banish = baseAttribute.banish;
    }

    public void RestoreGameAttribute()
    {
        gameCurrentAttribute.timer = gameBaseAttribute.timer;
        gameCurrentAttribute.second = gameBaseAttribute.second;
        gameCurrentAttribute.minute = gameBaseAttribute.minute;
        gameCurrentAttribute.gold = gameBaseAttribute.gold;
    }

}
