using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : RelicFunction
{
    public int getRevival;

    public override void AtGetStart()
    {
        attributeManager.Revival(getRevival);
    }
}
