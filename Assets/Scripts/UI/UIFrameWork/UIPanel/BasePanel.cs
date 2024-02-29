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
    /// UI进入时执行，只执行一次
    /// </summary>
    public virtual void OnEnter()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// UI暂停时执行
    /// </summary>
    public virtual void OnPause() { }

    /// <summary>
    /// UI继续时执行
    /// </summary>
    public virtual void OnResume()
    {
        uiPanelManager.displayPanel = uiPanelName;
    }

    /// <summary>
    /// UI退出时执行
    /// </summary>
    public virtual void OnExit()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }

        //面板全部出栈后进行的OnExit操作，此时将游戏时间恢复
        if (uiPanelManager.PanelStackCount() == 0)
        {
            Time.timeScale = 1;
        }
    }
}
