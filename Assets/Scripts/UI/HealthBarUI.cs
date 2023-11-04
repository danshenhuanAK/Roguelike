using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private ObjectPool objectPool;

    public GameObject healthUIPrefab;               //���Ѫ��Ԥ����
    public Transform barPoint;                      //Ѫ������ĵ�

    public bool alwaysVisible;                      //�Ƿ�������ʾ
    public float visibleTime;                       //Ѫ������ʱ��

    private Image healthSlider;                     //Ѫ������
    private GameObject healthBarCanvas;             //Ѫ��Canvas

    private GameObject UIbar;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        healthBarCanvas = GameObject.Find("HealthBarCanvas");
    }
    private void OnEnable()
    {
        UIbar = objectPool.CreateObject(healthUIPrefab.name, healthUIPrefab, healthBarCanvas, new Vector3(0, 0, 0), Quaternion.identity);
        healthSlider = UIbar.transform.GetChild(0).GetComponent<Image>();
        UIbar.SetActive(alwaysVisible);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if(UIbar)
        {
            UIbar.SetActive(true);
        }
        
        float sliderPercent = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    private void LateUpdate()
    {
        if(UIbar != null)               //Ѫ����һֱ����
        {
            UIbar.transform.position = barPoint.position;
        }
    }

    public void CloseUIbar()
    {
        UIbar.SetActive(false);
    }
}
