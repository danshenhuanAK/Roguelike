using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : RelicFunction
{
    public float getDuration;

    public override void AtGetStart()
    {
        attributeManager.Duration(getDuration);
    }
}
