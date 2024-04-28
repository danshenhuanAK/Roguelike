using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBook : RelicFunction, IRelicSaveable
{
    public float getCritical;
    public float getCriticalDamage;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Critical(getCritical);
        attributeManager.CriticalDamage(getCriticalDamage);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
