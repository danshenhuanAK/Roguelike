using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public enum GameState
{
    NewGame,
    ContinueGame,
    Fighting,
    ChooseLevel,
    Pause,
    Menu
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public GameObject player;

    private DataManager dataManager;

    public AssetReference mainScene;

    public RawImage occlusionPanel;
    public float occlusionSpeed;

    protected override void Awake()
    {
        dataManager = DataManager.Instance;
        Application.targetFrameRate = 60;
    }

    public void GameOver()                          //游戏结束退回菜单
    {
        dataManager.ClearGameData();

        Addressables.LoadSceneAsync(mainScene);
    }

    public void GameQuit()                          //退出游戏
    {
        Application.Quit();
    }

    public void OpenOcclusion()
    {
        StartCoroutine(Occlusion());
    }

    private IEnumerator Occlusion()
    {
        occlusionPanel.gameObject.SetActive(true);
        occlusionPanel.color = Color.black;

        while (occlusionPanel.color.a != 0)
        {
            occlusionPanel.color = new Color(0, 0, 0, Mathf.Max(occlusionPanel.color.a - occlusionSpeed, 0));
            if(occlusionPanel.color.a == 0)
            {
                StopCoroutine(Occlusion());
            }
            yield return null;
        }
    }
}
