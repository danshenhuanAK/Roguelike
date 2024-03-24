using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected AudioManager audioManager;
    protected FightProgressAttributeManager attributeManager;

    public PlayerSkillData_SO skillData;
    private SkillSpawner parentSpawner;
    
    protected Transform skillPoint;

    protected Coroutine exitCoroutine;

    protected bool isAttack;
    protected float damageCoolDown;
    protected float skillDuration;

    protected virtual void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
        audioManager = AudioManager.Instance;

        parentSpawner = transform.parent.gameObject.GetComponent<SkillSpawner>();
    }

    protected void GetSkillData()
    {
        skillData = Instantiate(parentSpawner.skillData.playerCurrentSkillData);

        skillData.duration *= 1 + attributeManager.playerData.duration;
        skillData.coolDown *= 1 + attributeManager.playerData.skillCoolDown;
        skillData.launchMoveSpeed *= 1 + attributeManager.playerData.launchMoveSpeed;
        skillData.attackRange *= 1 + attributeManager.playerData.attackRange;
        skillData.attackDamage *= 1 + attributeManager.playerData.attackPower;
        skillData.skillProjectileQuantity += attributeManager.playerData.projectileQuantity;
    }
}
