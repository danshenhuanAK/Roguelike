using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MapButton : MonoBehaviour, ILevelSaveable
{
    private UIPanelManager uiPanelManager;
    private GameManager gameManager;
    private AudioManager audioManager;
    private DataManager dataManager;

    private GameObject enemySpawner;

    public GameObject randomEvents;
    //[HideInInspector]
    public LevelData levelData;

    private bool isShrink;                                  //是否变化大小
    [SerializeField]
    private float flashSpeed;                               //动画速度
    public float flashValue;                                //动画增值

    private Vector2 size;
    private Vector2 Nowsize;

    private Image image;
    public Shader highLightMaterialShader;
    public Material highLightMaterial;

    private RandomWay randomWay;

    private void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        dataManager = DataManager.Instance;

        enemySpawner = GameObject.Find("EnemySpawner");

        image = GetComponent<Image>();
        randomWay = GetComponentInParent<RandomWay>();
        size = GetComponent<RectTransform>().sizeDelta;

        //生成一个独立与原本Material的Material
        highLightMaterial = Instantiate(highLightMaterial);
        highLightMaterial.shader = highLightMaterialShader;
        image.material = highLightMaterial;
        highLightMaterial.DisableKeyword("_ShowOutline");

        ILevelSaveable saveable = this;
        saveable.RegisterLevelData();
    }

    private void OnEnable()
    {
        GetComponent<RectTransform>().sizeDelta = size;
        Nowsize = size;

        if(dataManager.currentFloor != levelData.roomFloor)
        {
            levelData.isButton = false;
        }
    }

    private void Update()
    {
        if (uiPanelManager.displayPanel != UIPanelType.LevelPanel)
        {
            return;
        }

        if (levelData.isButton)
        {
            UIFlase();
        }
    }

    private void UIFlase()
    {
        if (!isShrink)
        {
            Nowsize = new Vector2(Nowsize.x + flashSpeed * flashValue, Nowsize.y + flashSpeed * flashValue);
            GetComponent<RectTransform>().sizeDelta = Nowsize;
        }
        else
        {
            Nowsize = new Vector2(Nowsize.x - flashSpeed * flashValue, Nowsize.y - flashSpeed * flashValue);
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
        if (levelData.isButton)
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
        if(levelData.isButton)
        {
            highLightMaterial.DisableKeyword("_ShowOutline");
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = size;
        }
    }

    public void SetActiveBoxEvent()
    {
        if(levelData.isButton)
        {
            randomWay.ChangeLevelIsButton(levelData);
            highLightMaterial.DisableKeyword("_ShowOutline");
            dataManager.currentFloor = levelData.roomFloor;
            uiPanelManager.PushPanel(UIPanelType.BoxEventPanel, UIPanelType.BoxEventPanelCanvas);
        }
    }

    public void SetActiveRandomEvents()
    {
        if(levelData.isButton)
        {
            randomWay.ChangeLevelIsButton(levelData);
            highLightMaterial.DisableKeyword("_ShowOutline");
            dataManager.currentFloor = levelData.roomFloor;
            uiPanelManager.PushPanel(UIPanelType.RandomEventsPanel, UIPanelType.RandomEventsPanelCanvas);
        }
    }

    public void MonsterOnMouseClick()
    {
        if(levelData.isButton)
        {
            gameManager.gameState = GameState.Fighting;
            enemySpawner.GetComponent<EnemySpawner>().CreateBossSeal();
            dataManager.currentFloor = levelData.roomFloor;
            audioManager.PlayMusic("Fight");
            randomWay.ChangeLevelIsButton(levelData);
            highLightMaterial.DisableKeyword("_ShowOutline");
            uiPanelManager.PopPanel();
        }
    }

    public void GetLevelData(List<LevelData> levelDatas)
    {
        if (!levelDatas.Contains(levelData))
        {
            levelDatas.Add(levelData);
        }
        else
        {
            levelDatas[levelData.roomNum] = levelData;
        }
    }
}
