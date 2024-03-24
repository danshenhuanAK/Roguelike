using UnityEngine;

public class LuckBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "����ֵ+ " + buffValue[rarityLevel];
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.luck += (int)buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
