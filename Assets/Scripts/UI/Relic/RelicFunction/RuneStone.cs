using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : RelicFunction, IRelicSaveable
{
    public int getRevival;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Revival(getRevival);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
