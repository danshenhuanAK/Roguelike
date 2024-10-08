using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnerSaveable
{
    void RegisterEnemySpawnerData() => DataManager.Instance.RegisterEnemySpawnerData(this);

    void GetEnemyData(EnemyDataList enemyDataList);
    void LoadEnemyData(EnemyDataList enemyDataList);
}
