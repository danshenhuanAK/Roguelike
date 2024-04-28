using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalRune : RelicFunction, IRelicSaveable
{
    public float getMagnet;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Magnet(getMagnet);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
