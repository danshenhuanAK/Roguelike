using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected UIPanelManager uiPanelManager;
    protected AttributeManager attributeManager;
    public string uiPanelName;

    protected virtual void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
        attributeManager = AttributeManager.Instance;
    }

    /// <summary>
    /// UI����ʱִ�У�ִֻ��һ��
    /// </summary>
    public virtual void OnEnter()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// UI��ͣʱִ��
    /// </summary>
    public virtual void OnPause() { }

    /// <summary>
    /// UI����ʱִ��
    /// </summary>
    public virtual void OnResume()
    {
        uiPanelManager.displayPanel = uiPanelName;
    }

    /// <summary>
    /// UI�˳�ʱִ��
    /// </summary>
    public virtual void OnExit()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        //���ȫ����ջ����е�OnExit��������ʱ����Ϸʱ��ָ�
        if (uiPanelManager.PanelStackCount() == 0)
        {
            Time.timeScale = 1;
        }
    }
}
