using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    private void Awake()
    {
        var backGroup = transform.GetChild(0).gameObject;                       //背景幕底
        var rawImage = transform.GetChild(1).gameObject;                        //背景图片

        Vector2 wayCanvas = gameObject.GetComponent<RectTransform>().sizeDelta;

        backGroup.GetComponent<RectTransform>().sizeDelta = wayCanvas;          //幕底要充满整个屏幕

        //根据当前幕布的height确定背景图片初始y值
        rawImage.transform.localPosition = new Vector3(0, rawImage.GetComponent<RectTransform>().sizeDelta.y / 2 - 20 - wayCanvas.y / 2, 0); 
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
}
