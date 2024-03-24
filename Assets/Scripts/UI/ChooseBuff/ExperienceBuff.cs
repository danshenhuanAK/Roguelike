using UnityEngine;

public class ExperienceBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        base.OnEnable();
        buffBonus.text = "经验获取速率+ " + buffValue[rarityLevel] * 100 + "%";
    }

    public void StrengthenPlayer()
    {
        attributeManager.playerData.experienceCquisitionSpeed += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
