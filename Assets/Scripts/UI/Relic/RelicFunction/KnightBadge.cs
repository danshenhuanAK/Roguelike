using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBadge : RelicFunction
{
    public float getCriticalDamage;

    public override void AtGetStart()
    {
        attributeManager.CriticalDamage(getCriticalDamage);
    }
}
