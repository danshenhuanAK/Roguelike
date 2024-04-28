using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banette : RelicFunction, IRelicSaveable
{
    public int getLuck;
    public float getCritical;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.playerData.luck = getLuck;
        attributeManager.Critical(getCritical);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
