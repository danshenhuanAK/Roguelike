using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class UIPanelManager : Singleton<UIPanelManager>
{
    private Transform canvasTransform;

    private Dictionary<string, string> panelPathDict = new Dictionary<string, string>();
    private Dictionary<string, BasePanel> panelDict = new Dictionary<string, BasePanel>();
    private Stack<BasePanel> panelStack;

    public string displayPanel;

    protected override void Awake()
    {
        ParseUIPanelTypeJson();
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

        BasePanel panel = GetPanel(panelType, canvas);
        panelStack.Push(panel);
        panel.OnEnter();
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

    public int PanelStackCount()
    {
        return panelStack.Count;
    }

    private BasePanel GetPanel(string panelType, string canvas)
    {
        BasePanel panel = panelDict.GetValue(panelType);

        //如果没有实例化面板，寻找路径进行实例化，并且存储到已经实例化好的字典面板中
        if (panel == null)
        {
            string path = panelPathDict.GetValue(panelType);
            canvasTransform = GameObject.Find(canvas).transform;
            GameObject panelGo = Instantiate(Resources.Load<GameObject>(path), canvasTransform, false);
            panel = panelGo.GetComponent<BasePanel>();
            panelDict.Add(panelType, panel);
        }
        return panel;
    }
    
    /// <summary>
    /// 解析Json文件
    /// </summary>
    private void ParseUIPanelTypeJson()
    {
        TextAsset textUIPanelType = Resources.Load<TextAsset>("Json/UIPanelTypeJson");
        if(!textUIPanelType)
        {
            Debug.LogWarning("没有找到Json文件");
        }
        UIPanelInfoList panelInfoList = JsonMapper.ToObject<UIPanelInfoList>(textUIPanelType.text);

        foreach (UIPanelInfo panelInfo in panelInfoList.panelInfoList)
        {
            panelPathDict.Add(panelInfo.panelType, panelInfo.path);
            //Debug.Log(panelInfo.panelType + ":" + panelInfo.path);
        }
    }
}
