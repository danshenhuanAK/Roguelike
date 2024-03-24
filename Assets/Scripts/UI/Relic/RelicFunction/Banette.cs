using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banette : RelicFunction
{
    public int getLuck;
    public float getCritical;

    public override void AtGetStart()
    {
        attributeManager.playerData.luck = getLuck;
        attributeManager.Critical(getCritical);
    }
}
