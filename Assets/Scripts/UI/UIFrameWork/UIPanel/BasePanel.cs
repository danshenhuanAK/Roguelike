using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected UIPanelManager uiPanelManager;
    protected AudioManager audioManager;
    protected DataManager dataManager;

    [SerializeField]
    protected string[] buttonAudioName;
    [SerializeField]
    protected string onEnterAudioName;

    public string uiPanelName;

    protected virtual void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
        audioManager = AudioManager.Instance;
        dataManager = DataManager.Instance;
    }

    /// <summary>
    /// UI����ʱִ�У�ִֻ��һ��
    /// </summary>
    public virtual void OnEnter()
    {
        if(onEnterAudioName.Length != 0)
        {
            audioManager.PlaySound(onEnterAudioName);
        }

        if (gameObject.activeSelf == false)
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

    public void PlayRandomSound()
    {
        audioManager.PlaySound(buttonAudioName[Random.Range(0, buttonAudioName.Length)]);
    }
}
