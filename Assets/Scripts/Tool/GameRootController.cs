using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour
{
    private UIPanelManager uiPanelManager;
    private GameManager gameManager;
    private DataManager dataManager;
    private FightProgressAttributeManager fightManager;

    private void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
        gameManager = GameManager.Instance;
        dataManager = DataManager.Instance;
        fightManager = FightProgressAttributeManager.Instance;

        gameManager.gameState = GameState.ChooseLevel;

        UIPanelRoot();        
    }

    private void Start()
    {
        if (dataManager.IsSave())
        {
            dataManager.LoadGameData();
        }
    }

    private void Update()
    {
        if(gameManager.gameState == GameState.Fighting)
        {
            fightManager.gameFightData.timer += Time.deltaTime;

            fightManager.gameFightData.second = (int)(fightManager.gameFightData.timer - (fightManager.gameFightData.minute * 60));
            fightManager.gameFightData.minute = (int)(fightManager.gameFightData.timer / 60);
        }
    }

    public void UIPanelRoot()
    {
        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/UIPanelTypeJson.json");
        uiPanelManager.uiPanelInfo = JsonMapper.ToObject<UIPanelInfoList>(js);

        //加载UI面板
        for (int i = 0; i < uiPanelManager.uiPanelInfo.panelInfoList.Count; i++)
        {
            Addressables.LoadAssetAsync<GameObject>(uiPanelManager.uiPanelInfo.panelInfoList[i].panelType).Completed += (handle) =>
            {
                string name = handle.Result.name;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject uiPanel = uiPanelManager.uiPanelPre.GetValue(name);

                    //将加载好的面板放入预制体中
                    if (uiPanel == null)
                    {
                        uiPanelManager.uiPanelPre.Add(name, handle.Result);
                    }

                    //所有面板加载完后将关卡面板和信息面板入栈
                    if (name == uiPanelManager.uiPanelInfo.panelInfoList[uiPanelManager.uiPanelInfo.panelInfoList.Count - 1].panelType)
                    {
                        uiPanelManager.PushPanel(UIPanelType.LevelPanel, UIPanelType.LevelPanelCanvas);
                        uiPanelManager.PushPanel(UIPanelType.InformationPanel, UIPanelType.InformationPanelCanvas);
                    }
                    Addressables.Release(handle);
                }
            };
        }
    }

    public void GetRelic()
    {
        GameObject.FindGameObjectWithTag("Relic").GetComponent<DisplayRelicUI>().GetRandomRelic(1);
    }

    public void Save()
    {
        DataManager.Instance.SaveGameData();
    }
}
