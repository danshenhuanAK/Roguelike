using UnityEngine;

public class CoolDownBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "������ȴ+ " + buffValue[rarityLevel] + "%";
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.skillCoolDown += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
