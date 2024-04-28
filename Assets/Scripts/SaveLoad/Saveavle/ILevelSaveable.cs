using System.Collections.Generic;
public interface ILevelSaveable
{
    void RegisterLevelData() => DataManager.Instance.RegisterLevelData(this);

    void GetLevelData(List<LevelData> levelDatas);
}
