using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlower : RelicFunction
{
    public float getHealthRegen;

    public override void AtGetStart()
    {
        attributeManager.HealthRegen(getHealthRegen);
    }
}
