using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover : RelicFunction, IRelicSaveable
{
    public int getLuck;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Luck(getLuck);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
