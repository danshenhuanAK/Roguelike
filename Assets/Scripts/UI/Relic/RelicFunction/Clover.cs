using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover : RelicFunction
{
    public int getLuck;

    public override void AtGetStart()
    {
        attributeManager.Luck(getLuck);
    }
}
