using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSaveable
{
    void RegisterPlayerData() => DataManager.Instance.RegisterPlayerData(this);
    void UnRegisterPlayerData() => DataManager.Instance.UnRegisterPlayerData();
    void GetPlayerData();

    void LoadPlayerData(PlayerData_SO playerData);
}
