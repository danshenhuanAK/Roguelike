using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necklace : RelicFunction, IRelicSaveable
{
    public float getDuration;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Duration(getDuration);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
