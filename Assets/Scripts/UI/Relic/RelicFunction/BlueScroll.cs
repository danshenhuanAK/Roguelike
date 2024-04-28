using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueScroll : RelicFunction, IRelicSaveable
{
    public float value;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    private void Start()
    {
        AtGetStart();
    }

    public override void AtGetStart()
    {
        attributeManager.gameFightData.BlueScroll = value;
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
