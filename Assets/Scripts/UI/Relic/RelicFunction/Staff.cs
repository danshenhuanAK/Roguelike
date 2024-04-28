using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : RelicFunction, IRelicSaveable
{
    public float getCritical;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Critical(getCritical);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
