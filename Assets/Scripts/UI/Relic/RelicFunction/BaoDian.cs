using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaoDian : RelicFunction
{
    public int getProjectileQuantity;

    public override void AtGetStart()
    {
        attributeManager.ProjectileQuantity(getProjectileQuantity);
    }
}
