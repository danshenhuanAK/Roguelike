using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "∑¿”˘¡¶+ " + buffValue[rarityLevel];
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.defence += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
