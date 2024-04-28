using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightSaveable
{
    void RegisterFightData() => DataManager.Instance.RegisterFightData(this);

    void GetFightData();

    void LoadFightData(GameFightData_SO FightData);
}
