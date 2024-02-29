using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Attribute/Player Data")]
public class PlayerBaseAttributeData_SO : ScriptableObject
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