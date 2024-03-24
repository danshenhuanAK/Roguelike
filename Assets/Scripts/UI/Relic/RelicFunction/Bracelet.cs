using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : RelicFunction
{
    public float getCoolDown;

    public override void AtGetStart()
    {
        attributeManager.SkillCoolDown(getCoolDown);
    }
}
