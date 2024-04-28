using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlower : RelicFunction, IRelicSaveable
{
    public float getHealthRegen;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.HealthRegen(getHealthRegen);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
