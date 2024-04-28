using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEditor;

public class DataManager : Singleton<DataManager>
{
    private EventManager evenManager;

    private List<ILevelSaveable> levelSaveableList = new();
    private IEnemySpawnerSaveable enemySaveable;
    private ISkillSaveable skillSaveable;
    private List<IRelicSaveable> relicSaveableList = new();
    private IPlayerSaveable playerSaveable;
    private IFightSaveable fightSaveable;

    [HideInInspector] public PlayerData_SO playerData;
    [HideInInspector] public GameFightData_SO gameFightData;
    [HideInInspector] public PlayerSkillDataList skillDataList = new();
    [HideInInspector] public EnemyDataList enemyDatas = new();
    [HideInInspector] public List<LevelData> levelDatas = new();
    [HideInInspector] public List<RelicData> relicDatas = new();
    public int currentFloor;
    public int maxFloor;

    protected override void Awake()
    {
        base.Awake();

        evenManager = EventManager.Instance;
        evenManager.Regist("SaveGameData", SaveGameDataEvent);
        evenManager.Regist("LoadGameData", LoadGameDataEvent);
    }

    public bool IsSave()
    {
        if (PlayerPrefs.GetInt("Saved", 0) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void OnDestroy()
    {
        evenManager.ClearEvents("SaveGameData");
        evenManager.ClearEvents("LoadGameData");
    }
    #region ���ݽӿڵ���Ӻ�ɾ��

        #region �ؿ����ݽӿ��б������ɾ��
        public void RegisterLevelData(ILevelSaveable saveable)
        {
            if(!levelSaveableList.Contains(saveable))
            {
                levelSaveableList.Add(saveable); 
            }
        }

        public void UnRegisterLevelData(ILevelSaveable saveable)
        {
            if(levelSaveableList.Contains(saveable))
            {
                levelSaveableList.Remove(saveable);
            }
        }
        #endregion

        #region �����������ݽӿ��б������ɾ��
    
        public void RegisterEnemySpawnerData(IEnemySpawnerSaveable saveable)
        {
            enemySaveable = saveable;
        }

        public void UnRegisterEnemySpawnerData()
        {
            enemySaveable = null;
        }
        #endregion

        #region �������ݽӿ���Ӻ�ɾ��
        public void RegisterSkillData(ISkillSaveable saveable)
        {
            skillSaveable = saveable;
        }

        public void UnRegisterSkillData()
        {
            skillSaveable = null;
        }
        #endregion

        #region �������ݽӿڵ���Ӻ�ɾ��
        public void RegisterRelicData(IRelicSaveable saveable)
        {
            if (!relicSaveableList.Contains(saveable))
            {
                relicSaveableList.Add(saveable);
            }
        }

        public void UnRegisterRelicData(IRelicSaveable saveable)
        {
            if (relicSaveableList.Contains(saveable))
            {
                relicSaveableList.Remove(saveable);
            }
        }

    #endregion

        #region �������ݽӿڵ���Ӻ�ɾ��
    public void RegisterPlayerData(IPlayerSaveable saveable)
    {
        playerSaveable = saveable;
    }

    public void UnRegisterPlayerData()
    {
        playerSaveable = null;
    }

    #endregion

    #region ս�����ݽӿڵ���Ӻ�ɾ��
    public void RegisterFightData(IFightSaveable saveable)
    {
        fightSaveable = saveable;
    }

    public void UnRegisterFightData()
    {
        fightSaveable = null;
    }

    #endregion

    #endregion

    #region ���������¼�
    private void SaveGameDataEvent(params object[] args)
    {
        fightSaveable.GetFightData();

        playerSaveable.GetPlayerData();

        foreach (var saveable in levelSaveableList)
        {
            saveable.GetLevelData(levelDatas);
        }

        enemySaveable.GetEnemyData(enemyDatas);

        skillSaveable.GetSkillData(skillDataList);

        foreach(var saveable in relicSaveableList)
        {
            saveable.GetRelicData(relicDatas);
        }
    }

    private void LoadGameDataEvent(params object[] args)
    {
        fightSaveable.LoadFightData(gameFightData);

        playerSaveable.LoadPlayerData(playerData);

        enemySaveable.LoadEnemyData(enemyDatas);

        skillSaveable.LoadSkillData(skillDataList);
    }
    #endregion

    #region ��Ϸ���ݵı��棬���غ����
    public void SaveGameData()
    {
        evenManager.DispatchEvent("SaveGameData", this);

        PlayerPrefs.SetInt("CurrentFloor", currentFloor);
        WriteEnemyDataJson();
        WriteLevelDataJson();
        WritePlayerDataJson();
        WriteSkillDataJson();
        WriteRelicDataJson();
        WriteFightProgressDataJson();

        PlayerPrefs.SetInt("Saved", 1);
    }

    public void LoadGameData()
    {
        currentFloor = PlayerPrefs.GetInt("CurrentFloor", 0);
        ReadEnemyDataJson();
        ReadLevelDataJson();
        ReadPlayerDataJson();
        ReadSkillDataJson();
        ReadRelicDataJson();
        ReadFightProgressDataJson();

        evenManager.DispatchEvent("LoadGameData", this);
    }

    public void ClearGameData()
    {
        PlayerPrefs.SetInt("CurrentFloor", 0);
        PlayerPrefs.SetInt("Saved", 0);

        ClearEnemyDataJson();
        ClearLevelDataJson();
        ClearPlayerDataJson();
        ClearSkillDataJson();
        ClearRelicDataJson();
        ClearFightProgressDataJson();
    }
    #endregion

    #region ���������ļ��Ĳ���

    #region 1.�ؿ������ļ��Ĳ���
    private void WriteLevelDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json");
        }

        string js;

        if(levelDatas != null)
        {
            js = JsonMapper.ToJson(levelDatas);
        }
        else
        {
            return;
        }
        
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json", js);
    }

    private void ReadLevelDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json");
        levelDatas = JsonMapper.ToObject<List<LevelData>>(js);
    }

    private void ClearLevelDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json");
        }

        levelDatas = new();

        string js = JsonMapper.ToJson(levelDatas);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/LevelDataJson.json", js);
    }
    #endregion

    #region 2.�������������ļ��Ĳ���
    private void WriteEnemyDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json");
        }

        string js;

        if (enemySaveable != null)
        {
            js = JsonMapper.ToJson(enemyDatas);
        }
        else
        {
            return;
        }

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json", js);
    }

    private void ReadEnemyDataJson()
    {
        if(!File.Exists(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json");
        enemyDatas = JsonMapper.ToObject<EnemyDataList>(js);
    }

    private void ClearEnemyDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json");
        }

        enemyDatas = new();

        string js = JsonMapper.ToJson(enemyDatas);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/EnemyDataJson.json", js);
    }
    #endregion

    #region 3.���������ļ��Ĳ���
    private void WritePlayerDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json");
        }

        string js;

        if (playerData != null)
        {
            js = JsonMapper.ToJson(playerData);
        }
        else
        {
            return;
        }

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json", js);
    }

    private void ReadPlayerDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json");
        playerData = JsonMapper.ToObject<PlayerData_SO>(js);
    }

    private void ClearPlayerDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json");
        }

        playerData = ScriptableObject.CreateInstance<PlayerData_SO>();

        string js = JsonMapper.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/PlayerDataJson.json", js);
    }
    #endregion

    #region 4.���������ļ��Ĳ���
    private void WriteSkillDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json");
        }

        string js;

        if (skillDataList != null)
        {
            js = JsonMapper.ToJson(skillDataList);
        }
        else
        {
            return;
        }

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json", js);
    }

    private void ReadSkillDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json");
        skillDataList = JsonMapper.ToObject<PlayerSkillDataList>(js);
    }

    private void ClearSkillDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json");
        }

        skillDataList = new();

        string js = JsonMapper.ToJson(skillDataList);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/SkillDataJson.json", js);
    }
    #endregion

    #region 5.���������ļ��Ĳ���

    private void WriteRelicDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json");
        }

        string js;

        if (relicDatas != null)
        {
            js = JsonMapper.ToJson(relicDatas);
        }
        else
        {
            return;
        }

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json", js);
    }

    private void ReadRelicDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json");
        relicDatas = JsonMapper.ToObject<List<RelicData>>(js);
    }

    private void ClearRelicDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json");
        }

        relicDatas = new();

        string js = JsonMapper.ToJson(relicDatas);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/RelicDataJson.json", js);
    }

    #endregion

    #region 6.ս�����������ļ�����
    private void WriteFightProgressDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json");
        }

        string js;

        if (gameFightData != null)
        {
            js = JsonMapper.ToJson(gameFightData);
        }
        else
        {
            return;
        }

        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json", js);
    }

    private void ReadFightProgressDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json");
        }

        string js = File.ReadAllText(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json");

        gameFightData = JsonMapper.ToObject<GameFightData_SO>(js);
    }
    private void ClearFightProgressDataJson()
    {
        if (!File.Exists(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json"))
        {
            Debug.LogError("û�ҵ��ļ�=��" + Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json");
        }

        gameFightData = ScriptableObject.CreateInstance<GameFightData_SO>();
        
        string js = JsonMapper.ToJson(gameFightData);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/Json/FightProgressDataJson.json", js);
    }

    #endregion

    #endregion
}
