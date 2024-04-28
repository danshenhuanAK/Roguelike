using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaoDian : RelicFunction, IRelicSaveable
{
    public int getProjectileQuantity;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.ProjectileQuantity(getProjectileQuantity);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
