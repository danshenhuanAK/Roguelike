using UnityEngine;

public class LuckBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "����ֵ+ " + buffValue[rarityLevel];
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
