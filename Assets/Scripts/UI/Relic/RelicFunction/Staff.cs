using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : RelicFunction
{
    public float getCritical;

    public override void AtGetStart()
    {
        attributeManager.Critical(getCritical);
    }
}
