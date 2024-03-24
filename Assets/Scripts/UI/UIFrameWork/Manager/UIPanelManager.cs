using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : Singleton<UIPanelManager>
{
    private Dictionary<string, BasePanel> panelDict = new();
    private Stack<BasePanel> panelStack = new();

    public Dictionary<string, GameObject> uiPanelPre = new();
    public UIPanelInfoList uiPanelInfo;

    public string displayPanel;

    protected override void Awake()
    {
        base.Awake();
    }

    public void PushPanel(string panelType, string canvas)
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }

        //停止上一个界面
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = panelDict.GetValue(panelType);

        //如果没有实例化面板，寻找路径进行实例化，并且存储到已经实例化好的字典面板中
        if (panel == null)
        {
            GameObject loadPanel = uiPanelPre.GetValue(panelType);
            Transform canvasTransform = GameObject.Find(canvas).transform;

            GameObject panelGo = Instantiate(loadPanel, canvasTransform, false);
            panel = panelGo.GetComponent<BasePanel>();
            if(panelDict.ContainsKey(panelType))                //实例化过的删除再加入
            {
                panelDict.Remove(panelType);
            }
            panelDict.Add(panelType, panel);
            panelStack.Push(panel);
            panel.OnEnter();

            if (panelType == UIPanelType.InformationPanel)
            {
                PopPanel();
            }
        }
        else
        {
            panelStack.Push(panel);
            panel.OnEnter();
        }
    }

    public void PopPanel()
    {
        if (panelStack == null)
        {
            panelStack = new Stack<BasePanel>();
        }
        if (panelStack.Count <= 0)
        {
            return;
        }

        //退出栈顶面板
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        //恢复上一个面板
        if (panelStack.Count > 0)
        {
            BasePanel panel = panelStack.Peek();
            panel.OnResume();
        }

    }

    public void ClearAllPanel()
    {
        panelDict.Clear();
        panelStack.Clear();
    }

    public int PanelStackCount()
    {
        return panelStack.Count;
    }

    public int LoadPanelCount()
    {
        return panelDict.Count;
    }
}
