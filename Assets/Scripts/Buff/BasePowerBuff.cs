using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "»ù´¡¹¥»÷Á¦+ " + buffValue[rarityLevel];
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.attackPower += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
