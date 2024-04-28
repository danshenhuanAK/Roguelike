using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : RelicFunction, IRelicSaveable
{
    public float getAttackRange;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.AttackRange(getAttackRange);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
