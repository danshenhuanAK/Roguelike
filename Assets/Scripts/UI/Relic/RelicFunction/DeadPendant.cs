using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPendant : RelicFunction, IRelicSaveable
{
    public float getPower;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.playerData.maxHealth /= 2;
        attributeManager.AttackPower(getPower);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
