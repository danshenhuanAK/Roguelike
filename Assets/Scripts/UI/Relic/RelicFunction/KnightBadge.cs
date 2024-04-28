using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBadge : RelicFunction, IRelicSaveable
{
    public float getCriticalDamage;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.CriticalDamage(getCriticalDamage);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
