using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVolumeBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "�������ֵ+ " + buffValue[rarityLevel];
    }

    public void strengthenPlayer()
    {
        attributeManager.playerData.maxHealth += buffValue[rarityLevel];
        grade++;
        
        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
