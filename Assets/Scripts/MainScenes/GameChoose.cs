using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameChoose : MonoBehaviour
{
    public GameObject newGame;
    public GameObject continueGame;
    public GameObject quitGame;
    public GameObject setting;

    public Vector3[] objPosY;

    private DataManager dataManager;
    private AudioManager audioManager;
    private UIPanelManager uIPanelManager;
    private ObjectPool objectPool;

    public string buttonSound;

    public GameObject loadPanel;
    public Slider slider;
    public TMP_Text loadText;
    public string loadSceneName;

    public GameObject settingPanel;

    private void Awake()
    {
        dataManager = DataManager.Instance;
        audioManager = AudioManager.Instance;
        uIPanelManager = UIPanelManager.Instance;
        objectPool = ObjectPool.Instance;
    }

    private void OnEnable()
    {
        if (dataManager.IsSave())
        {
            continueGame.SetActive(true);
            continueGame.transform.localPosition = objPosY[1];
            setting.transform.localPosition = objPosY[2];
            quitGame.transform.localPosition = objPosY[3];
        }
        else
        {
            continueGame.SetActive(false);
            setting.transform.localPosition = objPosY[1];
            quitGame.transform.localPosition = objPosY[2];
        }
    }

    #region
    public void NewGame()                   //新游戏点击事件
    {
        dataManager.ClearGameData();
        audioManager.PlaySound(buttonSound);

        if (objectPool.ObjectNum() != 0)
        {
            objectPool.ClearAll();
        }

        if (uIPanelManager.PanelStackCount() != 0 && uIPanelManager.LoadPanelCount() != 0)
        {
            uIPanelManager.ClearAllPanel();
        }

        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        loadPanel.SetActive(true);

        var handle = Addressables.LoadSceneAsync(loadSceneName);

        while (!handle.IsDone)
        {
            slider.value = handle.GetDownloadStatus().Percent;
            loadText.text = ((int)(slider.value * 100)).ToString() + "%";
            yield return null;
        }
    }

    public void ContinueGame()              //继续游戏点击事件
    {
        objectPool.ClearAll();
        audioManager.PlaySound(buttonSound);

        StartCoroutine(loadScene());
    }

    public void Setting()
    {
        audioManager.PlaySound(buttonSound);

        if(settingPanel.activeSelf == true)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            settingPanel.SetActive(true);
        }
    }

    public void QuitGame()                  //退出点击事件
    {
        audioManager.PlaySound(buttonSound);

        Application.Quit();
    }
    #endregion
}
