using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHat : RelicFunction
{
    public float value;

    private void Start()
    {
        AtGetStart();
    }

    public override void AtGetStart()
    {
        attributeManager.gameFightData.MageHat = value;
    }
}
