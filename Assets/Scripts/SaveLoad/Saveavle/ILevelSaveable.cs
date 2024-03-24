using System.Collections.Generic;
public interface ILevelSaveable
{
    void RegisterLevelData() => DataManager.Instance.RegisterLevelData(this);
    void UnRegisterLevelData() => DataManager.Instance.UnRegisterLevelData(this);
    void GetLevelData(List<LevelData> levelDatas);
}
