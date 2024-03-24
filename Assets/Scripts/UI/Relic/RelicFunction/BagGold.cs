using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGold : RelicFunction
{
    public int getGold;

    public override void AtGetStart()
    {
        attributeManager.Gold(getGold);
    }
}
