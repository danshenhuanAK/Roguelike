using UnityEngine;

public class LuckBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "–“‘À÷µ+ " + buffValue[rarityLevel];
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.luck += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
