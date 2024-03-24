using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : RelicFunction
{
    public float getAttackRange;

    public override void AtGetStart()
    {
        attributeManager.AttackRange(getAttackRange);
    }
}
