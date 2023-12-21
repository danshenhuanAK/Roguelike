using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject wayCanvas;                            //�ؿ�����UI

    public GameObject chooseBuffCanvas;                     //buffѡ��UI
        
    public GameObject fightCanvas;                          //ս������UI

    public GameObject pauseCanvas;                          //����UI

    public void displayWayCanvas(bool isDisplay)                          //��ʾ�ؿ�����UI
    {
        if(wayCanvas.activeSelf != isDisplay)
        {
            wayCanvas.SetActive(isDisplay);
        }
    }

    public void displayChooseBuffCanvas(bool isDisplay)                   //��ʾbuffѡ��UI
    {
        if(chooseBuffCanvas.activeSelf != isDisplay)
        {
            chooseBuffCanvas.SetActive(isDisplay);
        }
    }

    public void displayPauseCanvas(bool isDisplay)                        //��ʾ���ý���UI
    {
        if(pauseCanvas.activeSelf != isDisplay)
        {
            pauseCanvas.SetActive(isDisplay);
        }
    }
}
