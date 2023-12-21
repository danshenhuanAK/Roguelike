using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject wayCanvas;                            //关卡生成UI

    public GameObject chooseBuffCanvas;                     //buff选择UI
        
    public GameObject fightCanvas;                          //战斗界面UI

    public GameObject pauseCanvas;                          //设置UI

    public void displayWayCanvas(bool isDisplay)                          //显示关卡生成UI
    {
        if(wayCanvas.activeSelf != isDisplay)
        {
            wayCanvas.SetActive(isDisplay);
        }
    }

    public void displayChooseBuffCanvas(bool isDisplay)                   //显示buff选择UI
    {
        if(chooseBuffCanvas.activeSelf != isDisplay)
        {
            chooseBuffCanvas.SetActive(isDisplay);
        }
    }

    public void displayPauseCanvas(bool isDisplay)                        //显示设置界面UI
    {
        if(pauseCanvas.activeSelf != isDisplay)
        {
            pauseCanvas.SetActive(isDisplay);
        }
    }
}
