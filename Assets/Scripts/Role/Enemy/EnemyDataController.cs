using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataController : MonoBehaviour, IEnemySpawnerSaveable
{
    public EnemyDataList enemyBaseDatas;
    public EnemyDataList enemyCurrentDatas;

    private DataManager dataManager;

    private void Awake()
    {
        dataManager = DataManager.Instance;

        IEnemySpawnerSaveable saveable = this;
        saveable.RegisterEnemySpawnerData();
    }

    private void Start()
    {
        if (!dataManager.IsSave())
        {
            for (int i = 0; i < enemyBaseDatas.enmeyDatas.Count; i++)
            {
                enemyCurrentDatas.enmeyDatas.Add(Instantiate(enemyBaseDatas.enmeyDatas[i]));
            }
        }
    }

    public void GetEnemyData(EnemyDataList enemyDataList)
    {
        if (enemyDataList == null)
        {
            enemyDataList = enemyCurrentDatas;
        }
        else
        {
            for(int i = 0; i < enemyCurrentDatas.enmeyDatas.Count; i++)
            {
                EnemyData_SO saveData = enemyDataList.enmeyDatas.Find(data => data.enemyName == enemyCurrentDatas.enmeyDatas[i].enemyName);

                if(saveData != null)
                {
                    enemyDataList.enmeyDatas.Remove(saveData);
                }
                int dataCount = enemyDataList.enmeyDatas.Count;

                enemyDataList.enmeyDatas.Add(ScriptableObject.CreateInstance<EnemyData_SO>());
                enemyDataList.enmeyDatas[dataCount] = Instantiate(enemyCurrentDatas.enmeyDatas[i]);
            }
        }
    }

    public void LoadEnemyData(EnemyDataList enemyDataList)
    {
        for (int i = 0; i < enemyDataList.enmeyDatas.Count; i++)
        {
            enemyCurrentDatas.enmeyDatas.Add(Instantiate(enemyDataList.enmeyDatas[i]));
        }

        GetComponent<EnemySpawner>().EnemySpawnerRoot();
    }
}
