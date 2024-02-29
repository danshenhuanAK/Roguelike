using UnityEngine;

public class ExperienceBuff : PlayerBuff
{
    protected override void OnEnable()
    {
        buffBonus.text = "�����ȡ����+ " + buffValue[rarityLevel] + "%";
        base.OnEnable();
    }

    public void strengthenPlayer()
    {
        attributeManager.currentAttribute.experienceCquisitionSpeed += buffValue[rarityLevel];
        grade++;

        if (grade == 99)
        {
            ClearBuff();
        }
    }
}
