using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private ObjectPool objectPool;

    public GameObject healthUIPrefab;               //获得血条预制体
    public Transform barPoint;                      //血条跟随的点

    public bool alwaysVisible;                      //是否总是显示
    public float visibleTime;                       //血条持续时间

    private Image healthSlider;                     //血条滑动
    private GameObject healthBarCanvas;             //血条Canvas

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
        if(UIbar != null)               //血量条一直跟随
        {
            UIbar.transform.position = barPoint.position;
        }
    }

    public void CloseUIbar()
    {
        UIbar.SetActive(false);
    }
}
