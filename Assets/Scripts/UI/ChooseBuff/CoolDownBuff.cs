using UnityEngine;

public class CoolDownBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "ººƒ‹¿‰»¥- " + buffValue[rarityLevel] * 100 + "%";
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.skillCoolDown += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
