using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButtonPanel : BasePanel
{
    protected override void Awake()
    {
        uiPanelName = UIPanelType.GameOverButtonPanel;

        base.Awake();
    }

    public override void OnEnter()
    {
        Time.timeScale = 0;

        uiPanelManager.displayPanel = uiPanelName;

        base.OnEnter();
    }

    public override void OnExit()
    {
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
}
