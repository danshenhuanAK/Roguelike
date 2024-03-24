using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameProgress : MonoBehaviour
{
    private UIPanelManager uIPanelManager;
    private AudioManager audioManager;
    private DataManager dataManager;

    public AssetReference loadScene;
    public AssetReference currentScene;

    public string[] audioName;

    private void Awake()
    {
        uIPanelManager = UIPanelManager.Instance;
        audioManager = AudioManager.Instance;
        dataManager = DataManager.Instance;
    }

    public void GameSettingButton()
    {
        uIPanelManager.PushPanel(UIPanelType.SettingButtonPanel, UIPanelType.SettingButtonPanelCanvas);
        PlayAudio();
    }

    public void GameOverButton()
    {
        uIPanelManager.PushPanel(UIPanelType.GameOverButtonPanel, UIPanelType.GameOverButtonPanelCanvas);
        ObjectPool.Instance.ClearAll();
        PlayAudio();
    }

    public void ExitGameButton()
    {
        PlayAudio();

        Addressables.LoadSceneAsync(loadScene);
    }

    public void ContinueGame()
    {
        PlayAudio();

        uIPanelManager.PopPanel();
    }

    public void PlayAudio()
    {
        string name = audioName[Random.Range(0, audioName.Length)];
        audioManager.PlaySound(name);
    }
}
