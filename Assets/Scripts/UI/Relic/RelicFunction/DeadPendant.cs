using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPendant : RelicFunction
{
    public float getPower;

    public override void AtGetStart()
    {
        attributeManager.playerData.maxHealth /= 2;
        attributeManager.AttackPower(getPower);
    }
}
