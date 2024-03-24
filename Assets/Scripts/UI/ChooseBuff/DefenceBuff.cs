using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "∑¿”˘¡¶+ " + buffValue[rarityLevel];
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.defence += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
