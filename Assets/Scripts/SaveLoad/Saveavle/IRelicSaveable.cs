using System.Collections.Generic;

public interface IRelicSaveable
{
    void RegisterRelicData() => DataManager.Instance.RegisterRelicData(this);
    void UnRegisterRelicData() => DataManager.Instance.UnRegisterRelicData(this);
    void GetRelicData(List<RelicData> relicDatas);
}
