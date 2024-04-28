using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : RelicFunction, IRelicSaveable
{
    public float getDefence;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.Defence(getDefence);
    }
    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }

}
