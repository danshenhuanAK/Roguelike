using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNumericalValueUI : MonoBehaviour
{
    private AttributeManager attributeManager;

    public PlayerData playerData;

    [SerializeField]
    private List<TMP_Text> valueUI = new List<TMP_Text>();

    public TMP_Text UI;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
    }

    private void OnEnable()
    {
        valueUI[0].text = attributeManager.currentAttribute.maxHealth.ToString();
        valueUI[1].text = attributeManager.currentAttribute.healthRegen.ToString();
        valueUI[2].text = "+" + attributeManager.currentAttribute.defence.ToString();
        valueUI[3].text = "+" + attributeManager.currentAttribute.moveSpeed.ToString() + "%";
        valueUI[4].text = "+" + attributeManager.currentAttribute.attackPower.ToString() + "%";
        valueUI[5].text = "+" + attributeManager.currentAttribute.launchMoveSpeed.ToString() + "%";
        valueUI[6].text = "+" + attributeManager.currentAttribute.duration.ToString() + "%";
        valueUI[7].text = "+" + attributeManager.currentAttribute.attackRange.ToString() + "%";
        valueUI[8].text = "-" + attributeManager.currentAttribute.skillCoolDown.ToString() + "%";
        valueUI[9].text = attributeManager.currentAttribute.projectileQuantity.ToString();
        valueUI[10].text = attributeManager.currentAttribute.revival.ToString();
        valueUI[11].text = "+" + attributeManager.currentAttribute.magnet.ToString() + "%";
        valueUI[12].text = "+" + attributeManager.currentAttribute.critical.ToString() + "%";
        valueUI[13].text = "+" + attributeManager.currentAttribute.criticalDamage.ToString() + "%";
        valueUI[14].text = attributeManager.currentAttribute.experienceCquisitionSpeed.ToString();
        valueUI[15].text = "+" + attributeManager.currentAttribute.luck.ToString();
    }
}
