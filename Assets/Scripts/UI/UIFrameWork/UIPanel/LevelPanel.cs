using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : BasePanel
{
    protected override void Awake()
    {
        uiPanelName = UIPanelType.LevelPanel;

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
