using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVolumeBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "最大生命值+ " + buffValue[rarityLevel];
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.maxHealth += buffValue[rarityLevel];
        grade++;
        
        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
