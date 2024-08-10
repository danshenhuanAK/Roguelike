using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelPanel : BasePanel
{
    public GameObject[] fightGameObject;
    private GameManager gameManager;

    protected override void Awake()
    {
        uiPanelName = UIPanelType.LevelPanel;

        gameManager = GameManager.Instance;

        var rawImage = transform.GetChild(1).gameObject;                            //����ͼƬ

        //���ݵ�ǰĻ����heightȷ������ͼƬ��ʼyֵ
        rawImage.transform.localPosition = new Vector3(0, 270, 0);

        base.Awake();
    }

    public override void OnEnter()
    {
        gameManager.gameState = GameState.ChooseLevel;
        GameManager.Instance.OpenOcclusion();
        Time.timeScale = 0;
        audioManager.PlayMusic("Level");
        uiPanelManager.displayPanel = uiPanelName;
        base.OnEnter();
    }

    public override void OnExit()
    {
        Time.timeScale = 1;
        base.OnExit();
    }

    public override void OnPause()
    {
        //audioManager.musicSource.Stop();
        base.OnPause();
    }

    public override void OnResume()
    {
        //audioManager.musicSource.Play();
        gameManager.gameState = GameState.ChooseLevel;
        base.OnResume();
    }
}
