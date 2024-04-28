using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : RelicFunction, IRelicSaveable
{
    public int getMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.MoveSpeed(getMoveSpeed);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
