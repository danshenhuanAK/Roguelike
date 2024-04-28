using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAmethyst : RelicFunction, IRelicSaveable
{
    public float getExperienceCquisitionSpeed;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.ExperienceCquisitionSpeed(getExperienceCquisitionSpeed);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
