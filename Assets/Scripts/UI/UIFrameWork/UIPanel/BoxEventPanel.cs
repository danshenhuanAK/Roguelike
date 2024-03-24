using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxEventPanel : BasePanel
{
    public List<Button> boxButton;
    public TMP_Text goldText;

    public Vector2 randomGoldRange;
    private int gold;
    private int ButtonNum;

    protected override void Awake()
    {
        uiPanelName = UIPanelType.BoxEventPanel;

        base.Awake();
    }

    public override void OnEnter()
    {
        Time.timeScale = 0;

        GetRandomGold();
        uiPanelManager.displayPanel = uiPanelName;

        base.OnEnter();
    }

    public override void OnExit()
    {
        dataManager.currentFloor++;
        dataManager.SaveGameData();
        base.OnExit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    private void GetRandomGold()
    {
        ButtonNum = boxButton.Count;

        for (int i = 0; i < ButtonNum; i++)
        {
            boxButton[i].gameObject.SetActive(true);
        }

        gold = (int)Random.Range(Mathf.Max(0, randomGoldRange.x + attributeManager.playerData.luck * 2),
                            Mathf.Max(0, randomGoldRange.y));

        goldText.text = "»ñµÃ" + gold + "½ð±Ò";
    }

    public void GetGold()
    {
        attributeManager.gameFightData.gold += gold;
    }

    public void GetRelic(int count)
    {
        GameObject.FindGameObjectWithTag("Relic").GetComponent<DisplayRelicUI>().GetRandomRelic(count);
    }

    public void IsClosePanel(Button closeButton)
    {
        closeButton.gameObject.SetActive(false);
        ButtonNum--;

        if (ButtonNum == 0)
        {
            uiPanelManager.PopPanel();
        }
    }
}
