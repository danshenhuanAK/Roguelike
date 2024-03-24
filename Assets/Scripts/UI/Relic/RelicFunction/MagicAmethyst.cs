using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAmethyst : RelicFunction
{
    public float getExperienceCquisitionSpeed;

    public override void AtGetStart()
    {
        attributeManager.ExperienceCquisitionSpeed(getExperienceCquisitionSpeed);
    }
}
