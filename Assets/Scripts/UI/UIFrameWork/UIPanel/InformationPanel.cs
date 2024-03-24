using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPanel : BasePanel
{
    public GameObject numericalValueUI;

    private GameManager gameManager;

    GameState beforeState;
    protected override void Awake()
    {
        uiPanelName = UIPanelType.InformationPanel;
        gameManager = GameManager.Instance;
        base.Awake();
    }

    public override void OnEnter()
    {
        numericalValueUI.SetActive(true);
        beforeState = gameManager.gameState;
        gameManager.gameState = GameState.Pause;
        audioManager.musicSource.Stop();

        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Time.timeScale = 0;

        uiPanelManager.displayPanel = uiPanelName;
    }

    public override void OnExit()
    {
        numericalValueUI.SetActive(false);
        gameObject.transform.localPosition = new Vector3(2000, 2000, 0);
        audioManager.musicSource.Play();
        gameManager.gameState = beforeState;

        if (uiPanelManager.PanelStackCount() == 0)
        {
            Time.timeScale = 1;
        }
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
