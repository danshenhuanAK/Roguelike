using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventsController : MonoBehaviour
{
    public List<int> eventsRequirements;

    public Button[] goldButtons;

    public TMP_Text[] goldTexts;

    private FightProgressAttributeManager attributeManager;

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void OnEnable()
    {
        for (int i = 0; i < goldButtons.Length; i++)
        {
            if (attributeManager.gameFightData.gold < eventsRequirements[i])
            {
                goldButtons[i].enabled = false;
                goldTexts[i].text = "½ð±Ò²»¹»";
                goldTexts[i].color = Color.red;
            }
        }
    }
}
