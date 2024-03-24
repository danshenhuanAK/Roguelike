using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalTeeth : RelicFunction
{
    public float getPower;

    public override void AtGetStart()
    {
        attributeManager.AttackPower(getPower);
    }
}
