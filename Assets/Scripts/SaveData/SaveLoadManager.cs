using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private List<ISaveable> saveableList = new List<ISaveable>();
    private Dictionary<string, PlayerData> saveDataDict = new Dictionary<string, PlayerData>();

    private string DataFolder;
 
    protected override void Awake()
    {
        base.Awake();
        DataFolder = Application.dataPath + "/Data";
    }

    private void OnEnable()
    {
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnDisable()
    {
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent()
    {
        var resultPath = DataFolder + "Game_Data.sav";

        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }

    public void Register(ISaveable saveable)
    {
        saveableList.Add(saveable);
    }

    public void Save()
    {
        saveDataDict.Clear();

        foreach(var saveable in saveableList)
        {
            saveDataDict.Add(saveable.GetType().Name, saveable.GeneratePlayerData());
        }

        var resultPath = DataFolder + "Game_Data.sav";

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream fs = File.Create(resultPath);

        binaryFormatter.Serialize(fs, saveDataDict);
        fs.Close();
    }

    public void Load()
    {
        var resultPath = DataFolder + "Game_Data.sav";

        if (!File.Exists(resultPath)) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Application.dataPath + "/Data" + "/Player Data" + "/Data", FileMode.Open);
        var playerData = (Dictionary<string, PlayerData>)bf.Deserialize(fs);

        foreach (var saveable in saveableList)
        {
            saveable.RestorePlayerData(playerData[saveable.GetType().Name]);
        }
    }
}
