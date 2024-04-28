using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHat : RelicFunction, IRelicSaveable
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
        attributeManager.gameFightData.MageHat = value;
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
