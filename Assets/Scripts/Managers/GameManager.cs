using UnityEngine;

public class GameManager : Singleton<GameManager>, ISaveable
{
    [HideInInspector]
    public float timer;     
    public int second;                                            
    public int minute;

    public int kills;
    public int gold;
    public int floor;

    public GameObject enemySpawner;
    public GameObject player;

    protected override void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        timer = 0;
        
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        second = (int)(timer - (minute * 60));
        minute = (int)(timer / 60);

        if(second == 60)
        {
            enemySpawner.GetComponent<EnemyStats>().StrengThenEnemy(minute);
            enemySpawner.GetComponent<EnemySpawner>().GetSpawnerData(minute);
        }
    }

    public void StartGame()                         //����Ϸ
    {
        Time.timeScale = 1;
    }

    public void PauseGame()                         //��ͣ��Ϸ
    {
        Time.timeScale = 0;
    }

    public void SaveGame()                          //������Ϸ����
    {
        
    }

    public void ContinuingGame()                    //������Ϸ
    {

    }

    public void EndGame()
    {
        Application.Quit();
    }

    public PlayerData GeneratePlayerData()
    {
        PlayerData playerData = new PlayerData();

        playerData.gold = gold;

        return playerData;
    }

    public void RestorePlayerData(PlayerData playerData)
    {
        gold = playerData.gold;
    }

    public void CloseMusic(AudioSource audio)
    {
        audio.Stop();
    }

    public void OpenMusic(AudioSource audio)
    {
        audio.Play();
    }
}
