using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightCount : MonoBehaviour
{
    public TMP_Text kills;
    public TMP_Text golds;

    private FightProgressAttributeManager attributeManager;

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void Update()
    {
        kills.text = attributeManager.gameFightData.kills.ToString();
        golds.text = attributeManager.gameFightData.gold.ToString();
    }
}
