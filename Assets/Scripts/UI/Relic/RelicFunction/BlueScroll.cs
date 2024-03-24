using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScroll : RelicFunction
{
    public float value;

    private void Start()
    {
        AtGetStart();
    }

    public override void AtGetStart()
    {
        attributeManager.gameFightData.BlueScroll = value;
    }
}
