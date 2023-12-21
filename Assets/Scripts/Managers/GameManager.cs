using UnityEngine;

public class GameManager : Singleton<GameManager>, ISaveable
{
    public float timer;     
    public int second;                                            
    public int minute;
    public int gold;

    private void Start()
    {
        timer = 0;

        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        second = (int)timer;

        if (second > 59.0f)
        {
            second = (int)(timer - (minute * 60));
        }

        minute = (int)(timer / 60);
    }

    public void StartGame()                         //新游戏
    {
        Time.timeScale = 1;
    }

    public void PauseGame()                         //暂停游戏
    {
        Time.timeScale = 0;
    }

    public void SaveGame()                          //保存游戏进度
    {
        
    }

    public void ContinuingGame()                    //继续游戏
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
}
