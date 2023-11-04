using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonFlash : MonoBehaviour
{
    private GameObject wayCanvas;

    public bool isButton;                       //是否可进入关卡
    private bool isShrink;                      //是否变化大小

    [SerializeField]
    private float flashSpeed;                   //动画速度

    private Vector2 size;
    private Vector2 Nowsize;

    private Material highLightMaterial;

    public RandomWay randomWay;

    private void Awake()
    {
        randomWay = GetComponentInParent<RandomWay>();
        wayCanvas = GameObject.FindGameObjectWithTag("Map");
        size = GetComponent<RectTransform>().sizeDelta;
        highLightMaterial = GetComponent<Image>().material;
        highLightMaterial = Instantiate(highLightMaterial);
        GetComponent<Image>().material = highLightMaterial;
    }

    private void Start()
    {
        Nowsize = size;
    }

    private void Update()
    {
        if (isButton)
        {
            UIFlase();
        }
    }

    private void UIFlase()
    {
        if (!isShrink)
        {
            Nowsize = new Vector2(Nowsize.x + flashSpeed, Nowsize.y + flashSpeed);
            GetComponent<RectTransform>().sizeDelta = Nowsize;
        }
        else
        {
            Nowsize = new Vector2(Nowsize.x - flashSpeed, Nowsize.y - flashSpeed);
            GetComponent<RectTransform>().sizeDelta = Nowsize;
        }
        if (Nowsize.x >= size.x * 2)
        {
            isShrink = true;
        }
        else if (Nowsize.x <= size.x)
        {
            isShrink = false;
        }
    }

    public void OnMouseEnter()
    {     
        if(isButton)
        {
            highLightMaterial.EnableKeyword("_ShowOutline");
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = size * 2;
        }
    }

    public void OnMouseExit()
    {
        if(isButton)
        {
            highLightMaterial.DisableKeyword("_ShowOutline");
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = size;
        }
    }

    public void MonsterOnMouseClick()
    {
        if(isButton)
        {
            Time.timeScale = 1;
            randomWay.lastClickRoom = gameObject;
            wayCanvas.SetActive(false);
        }
    }
}
