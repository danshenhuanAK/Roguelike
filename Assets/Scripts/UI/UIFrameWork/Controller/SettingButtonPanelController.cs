using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonPanelController : BasePanel
{
    private Button settingButton;

    private bool isOnClick;

    protected override void Awake()
    {
        OnEnter();
        base.Awake();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PushOrPopSettingPanel();
        } 
    }

    public override void OnEnter()
    {
        settingButton = transform.Find("Setting").gameObject.GetComponent<Button>();
        settingButton.onClick.AddListener(PushOrPopSettingPanel);
    }

    /// <summary>
    /// ��ť�¼�������isOnClick��SettingPanel��ջ���ջ
    /// </summary>
    public void PushOrPopSettingPanel()
    {
        if(isOnClick)
        {
            uiPanelManager.PopPanel();
            isOnClick = false;
        }
        else
        {
            uiPanelManager.PushPanel(UIPanelType.InformationPanel, UIPanelType.InformationPanelCanvas);
            isOnClick = true;
        }
    }
}
