using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationPanelPanel : BasePanel
{
    protected override void Awake()
    {
        uiPanelName = UIPanelType.InformationPanel;
        gameObject.transform.localPosition = new Vector3(2000, 2000, 0);

        base.Awake();
    }

    public override void OnEnter()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Time.timeScale = 0;

        uiPanelManager.displayPanel = uiPanelName;
    }

    public override void OnExit()
    {
        gameObject.transform.localPosition = new Vector3(2000, 2000, 0);

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
