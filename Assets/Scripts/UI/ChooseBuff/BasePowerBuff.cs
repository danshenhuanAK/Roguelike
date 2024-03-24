using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "»ù´¡¹¥»÷Á¦+ " + buffValue[rarityLevel] * 100 + "%";
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.attackPower += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
