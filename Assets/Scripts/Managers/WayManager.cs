using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    private void Awake()
    {
        var backGroup = transform.GetChild(0).gameObject;                       //����Ļ��
        var rawImage = transform.GetChild(1).gameObject;                        //����ͼƬ

        Vector2 wayCanvas = gameObject.GetComponent<RectTransform>().sizeDelta;

        backGroup.GetComponent<RectTransform>().sizeDelta = wayCanvas;          //Ļ��Ҫ����������Ļ

        //���ݵ�ǰĻ����heightȷ������ͼƬ��ʼyֵ
        rawImage.transform.localPosition = new Vector3(0, rawImage.GetComponent<RectTransform>().sizeDelta.y / 2 - 20 - wayCanvas.y / 2, 0); 
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
}
