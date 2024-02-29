using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "���ܹ�����Χ+ " + buffValue[rarityLevel] + "%";
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.attackRange += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
