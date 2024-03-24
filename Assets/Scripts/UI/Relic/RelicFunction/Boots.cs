using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : RelicFunction
{
    public int getMoveSpeed;

    public override void AtGetStart()
    {
        attributeManager.MoveSpeed(getMoveSpeed);
    }

}
