using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBook : RelicFunction
{
    public float getCritical;
    public float getCriticalDamage;

    public override void AtGetStart()
    {
        attributeManager.Critical(getCritical);
        attributeManager.CriticalDamage(getCriticalDamage);
    }
}
