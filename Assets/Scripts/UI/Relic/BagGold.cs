using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGold : RelicInterface
{
    private AttributeManager attributeManager;

    public int getGold;

    protected override void Awake()
    {
        attributeManager = AttributeManager.Instance;

        base.Awake();
    }

    private void Start()
    {
        AtGetStart();
    }

    public override void AtGetStart()
    {
        attributeManager.Gold(getGold);
    }
}
