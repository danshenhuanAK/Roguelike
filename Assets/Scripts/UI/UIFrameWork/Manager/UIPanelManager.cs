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

        //ֹͣ��һ������
        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }

        BasePanel panel = panelDict.GetValue(panelType);

        //���û��ʵ������壬Ѱ��·������ʵ���������Ҵ洢���Ѿ�ʵ�����õ��ֵ������
        if (panel == null)
        {
            GameObject loadPanel = uiPanelPre.GetValue(panelType);
            Transform canvasTransform = GameObject.Find(canvas).transform;

            GameObject panelGo = Instantiate(loadPanel, canvasTransform, false);
            panel = panelGo.GetComponent<BasePanel>();
            if(panelDict.ContainsKey(panelType))                //ʵ��������ɾ���ټ���
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

        //�˳�ջ�����
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        //�ָ���һ�����
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
