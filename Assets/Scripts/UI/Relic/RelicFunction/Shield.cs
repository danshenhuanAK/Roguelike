using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : RelicFunction
{
    public float getDefence;

    public override void AtGetStart()
    {
        attributeManager.Defence(getDefence);
    }
}
