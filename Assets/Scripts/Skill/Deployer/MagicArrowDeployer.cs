using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowDeployer : SkillDeployer
{
    public override void DeploySkill()
    {
        CalculateTargets();
        SkillImpact();
    }
}
