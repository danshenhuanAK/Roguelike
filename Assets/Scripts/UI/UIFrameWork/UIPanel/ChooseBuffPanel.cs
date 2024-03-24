using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBuffPanel : BasePanel
{
    private GameManager gameManager;
    private FightProgressAttributeManager fightManager;

    private GameObject player;

    public GameObject[] spendGoldGames;
    public int[] spendGolds;

    protected override void Awake()
    {
        gameManager = GameManager.Instance;
        fightManager = FightProgressAttributeManager.Instance;
        player = gameManager.player;

        uiPanelName = UIPanelType.ChooseBuffPanel;

        base.Awake();
    }

    public override void OnEnter()
    {
        Time.timeScale = 0;

        uiPanelManager.displayPanel = uiPanelName;
        DisplaySpendGame();

        base.OnEnter();
    }

    public override void OnExit()
    {
        player.GetComponent<PlayerStats>().isPush = false;

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

    public void PopPanel()
    {
        if(uiPanelManager.displayPanel == UIPanelType.ChooseBuffPanel)
        {
            uiPanelManager.PopPanel();
        }
    }

    public void DisplaySpendGame()
    {
        for (int i = 0; i < spendGoldGames.Length; i++)
        {
            if (spendGolds[i] < fightManager.gameFightData.gold)
            {
                spendGoldGames[i].SetActive(true);
            }
            else
            {
                spendGoldGames[i].SetActive(false);
            }
        }
    }
}
