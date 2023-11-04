using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public GameObject chooseBuffCanvas;
    public GameObject wayCanvas;
    public GameObject pauseCanvas;

    public void ShowChooseBuff(bool isShow)
    {
        if(isShow == true)
        {
            chooseBuffCanvas.SetActive(true);
        }
        else
        {
            chooseBuffCanvas.SetActive(false);
        }
    }

    public void ShowWay(bool isShow)
    {
        if (isShow == true)
        {
            wayCanvas.SetActive(true);
        }
        else
        {
            wayCanvas.SetActive(false);
        }
    }

    public void ShowPause(bool isShow)
    {
        if (isShow == true)
        {
            pauseCanvas.SetActive(true);
        }
        else
        {
            pauseCanvas.SetActive(false);
        }
    }
}
