using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "���ܹ�����Χ+ " + buffValue[rarityLevel] + "%";
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.attackRange += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
