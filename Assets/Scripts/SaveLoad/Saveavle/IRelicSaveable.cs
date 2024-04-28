using System.Collections.Generic;

public interface IRelicSaveable
{
    void RegisterRelicData() => DataManager.Instance.RegisterRelicData(this);

    void GetRelicData(List<RelicData> relicDatas);
}
