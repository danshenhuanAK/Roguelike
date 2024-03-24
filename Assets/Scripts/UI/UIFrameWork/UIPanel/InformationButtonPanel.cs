using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationButtonPanel : BasePanel
{
    private GameManager gameManager;

    protected override void Awake()
    {
        gameManager = GameManager.Instance;
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PushOrPopSettingPanel();
        }
    }

    /// <summary>
    /// 按钮事件，InformationPanel入栈或出栈
    /// </summary>
    public void PushOrPopSettingPanel()
    {
        if (gameManager.gameState == GameState.Pause)
        {
            if (uiPanelManager.displayPanel != UIPanelType.InformationPanel)
            {
                return;
            }

            uiPanelManager.PopPanel();
        }
        else
        {
            uiPanelManager.PushPanel(UIPanelType.InformationPanel, UIPanelType.InformationPanelCanvas);
        }

        PlayRandomSound();
    }
}
