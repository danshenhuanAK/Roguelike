using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNumericalValueUI : MonoBehaviour
{
    private FightProgressAttributeManager attributeManager;

    [SerializeField]
    private List<TMP_Text> valueUI = new();

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
    }

    public void OnEnable()
    {
        valueUI[0].text = Mathf.RoundToInt((float)attributeManager.playerData.maxHealth).ToString();
        valueUI[1].text = Mathf.RoundToInt((float)attributeManager.playerData.healthRegen).ToString();
        valueUI[2].text = Mathf.RoundToInt((float)attributeManager.playerData.defence).ToString();
        valueUI[3].text = Mathf.RoundToInt((float)attributeManager.playerData.moveSpeed).ToString();
        valueUI[4].text = "+" + Mathf.RoundToInt((float)(attributeManager.playerData.attackPower * 100)).ToString() + "%";
        valueUI[5].text = "+" + Mathf.RoundToInt((float)(attributeManager.playerData.launchMoveSpeed * 100)).ToString() + "%";
        valueUI[6].text = "+" + Mathf.RoundToInt((float)(attributeManager.playerData.duration * 100)).ToString() + "%";
        valueUI[7].text = "+" + Mathf.RoundToInt((float)(attributeManager.playerData.attackRange * 100)).ToString() + "%";
        valueUI[8].text = "-" + Mathf.RoundToInt((float)(attributeManager.playerData.skillCoolDown * 100)).ToString() + "%";
        valueUI[9].text = "+" + attributeManager.playerData.projectileQuantity.ToString();
        valueUI[10].text = attributeManager.playerData.revival.ToString();
        valueUI[11].text = "+" + Mathf.RoundToInt((float)( (attributeManager.playerData.magnet - 1) * 100)).ToString() + "%";
        valueUI[12].text = "+" + Mathf.RoundToInt((float)(attributeManager.playerData.critical * 100)).ToString() + "%";
        valueUI[13].text = "+" + Mathf.RoundToInt((float)( (attributeManager.playerData.criticalDamage - 2) * 100)).ToString() + "%";
        valueUI[14].text = "+" + Mathf.RoundToInt((float)( (attributeManager.playerData.experienceCquisitionSpeed - 1) * 100)).ToString() + "%";
        valueUI[15].text = attributeManager.playerData.luck.ToString();
    }
}
