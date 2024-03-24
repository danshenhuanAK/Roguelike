using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleScroll : RelicFunction
{
    public float value;

    private void Start()
    {
        AtGetStart();
    }

    public override void AtGetStart()
    {
        attributeManager.gameFightData.PurpleScroll = value;
    }
}
