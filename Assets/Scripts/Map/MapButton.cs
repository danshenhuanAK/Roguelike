using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private UIPanelManager uiPanelManager;
    private GameManager gameManager;

    public GameObject randomEvents;

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
        uiPanelManager = UIPanelManager.Instance;
        gameManager = GameManager.Instance;

        randomWay = GetComponentInParent<RandomWay>();
        size = GetComponent<RectTransform>().sizeDelta;

        //生成一个独立与原本Material的Material
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
        if (uiPanelManager.displayPanel != UIPanelType.LevelPanel)
        {
            return;
        }

        if (isButton)
        {
            UIFlase();
        }
    }

    private void UIFlase()
    {
        if (!isShrink)
        {
            Nowsize = new Vector2(Nowsize.x + flashSpeed * Time.fixedUnscaledDeltaTime, Nowsize.y + flashSpeed * Time.fixedUnscaledDeltaTime);
            GetComponent<RectTransform>().sizeDelta = Nowsize;
        }
        else
        {
            Nowsize = new Vector2(Nowsize.x - flashSpeed * Time.fixedUnscaledDeltaTime, Nowsize.y - flashSpeed * Time.fixedUnscaledDeltaTime);
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

    public void UpFloor()
    {
        gameManager.floor++;
        highLightMaterial.DisableKeyword("_ShowOutline");
    }

    public void SetActiveBoxEvent()
    {
        if(isButton)
        {
            uiPanelManager.PushPanel(UIPanelType.BoxEventPanel, UIPanelType.BoxEventPanelCanvas);
        }
    }

    public void SetActiveRandomEvents()
    {
        if(isButton)
        {
            uiPanelManager.PushPanel(UIPanelType.RandomEventsPanel, UIPanelType.RandomEventsPanelCanvas);
        }
    }

    public void MonsterOnMouseClick()
    {
        if(isButton)
        {
            randomWay.lastClickRoom = gameObject;
            uiPanelManager.PopPanel();
        }
    }
}
