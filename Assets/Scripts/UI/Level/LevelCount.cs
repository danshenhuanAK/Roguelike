using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCount : MonoBehaviour
{
    public TMP_Text difGrade;
    public TMP_Text golds;

    private FightProgressAttributeManager attributeManager;

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void Update()
    {
        difGrade.text = attributeManager.gameFightData.minute.ToString();
        golds.text = attributeManager.gameFightData.gold.ToString();
    }
}
