using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGold : RelicFunction, IRelicSaveable
{
    public int getGold;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Gold(getGold);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
