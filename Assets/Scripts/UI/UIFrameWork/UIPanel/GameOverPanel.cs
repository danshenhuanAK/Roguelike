using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameOverPanel : BasePanel
{
    public AssetReference loadScene;

    protected override void Awake()
    {
        uiPanelName = UIPanelType.SettingButtonPanel;

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

    public void Exit()
    {
        PlayRandomSound();
        GameManager.Instance.GameOver();
    }
}
