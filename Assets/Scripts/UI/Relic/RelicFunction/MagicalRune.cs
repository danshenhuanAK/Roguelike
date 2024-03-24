using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalRune : RelicFunction
{
    public float getMagnet;

    public override void AtGetStart()
    {
        attributeManager.Magnet(getMagnet);
    }
}
