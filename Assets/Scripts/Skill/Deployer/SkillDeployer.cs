using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillDeployer : MonoBehaviour
{
    public Transform skillPoint;
    public GameObject createParent;

    private CharacterStats skillData;

    public CharacterStats SkillData
    {
        get { return skillData; }
        set { skillData = value; InitDeplopye(); }
    }

    private ISkillSelector selector;

    private IImpactEffect imapct;

    private Transform[] enemy;

    private void InitDeplopye()
    {
        selector = DeployerConfigFactory.CreateSkillSelector(skillData);

        imapct = DeployerConfigFactory.CreateImpactEffect(skillData);
    }

    public void CalculateTargets()
    {
        enemy = selector.SelectTarget(skillData, skillPoint, skillData.skillAttackData.attackMask);
    }

    public void SkillImpact()
    {
        imapct.Exeute(this);
    }

    public abstract void DeploySkill();
}
