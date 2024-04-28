using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : RelicFunction, IRelicSaveable
{
    public float getCoolDown;

    protected override void Awake()
    {
        base.Awake();

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    public override void AtGetStart()
    {
        attributeManager.SkillCoolDown(getCoolDown);
    }

    public void GetRelicData(List<RelicData> relicDatas)
    {
        relicDatas.Add(relicData);
    }
}
