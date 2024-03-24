using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner : MonoBehaviour
{
    public List<AssetReference> enemyPres;

    private List<AsyncOperationHandle<GameObject>> loadEnemyPres = new();

    private EnemyDataController dataController;

    public int currentFloor;                                //当前层数
    public int currentEnemyNum;                             //场景当前怪物数量

    private int minute;

    [System.Serializable]
    public class EnemySpawnerGrade
    {
        public int startTime;
        public int endTime;

        public float spawnerSpeed;
        public float spawnerTime;
        public float maxEnemyNum;
        public float minEnemyNum;
    }

    public List<EnemySpawnerGrade> enemySpawnerGrades;                         //怪物生成数据
    private EnemySpawnerGrade currentSpawnerGrade;                       //当前怪物生成数据

    public Transform minSpawn;
    public Transform maxSpawn;

    public AssetReferenceGameObject bossSeal;
    public Vector2 bossSealMinPosition;
    public Vector2 bossSealMaxPosition;

    private ObjectPool objectPool;
    private DataManager dataManager;
    private FightProgressAttributeManager attributeManager;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        dataManager = DataManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;

        dataController = gameObject.GetComponent<EnemyDataController>();
    }

    private void Start()
    {
        //生成范围
        minSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("MinSpawn");
        maxSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("MaxSpawn");

        //加载怪物资源并将资源放进enemyPres列表中
        if(!dataManager.IsSave())
        {
            EnemySpawnerRoot();
        }
    }

    public void EnemySpawnerRoot()
    {
        for (int i = 0; i < enemyPres.Count; i++)
        {
            LoadEnemyAsset(i);
        }

        UpGradeSpawnerData(attributeManager.gameFightData.minute);
    }

    private void Update()
    {
        if (attributeManager.gameFightData.minute != minute)
        {
            minute = attributeManager.gameFightData.minute;
            UpGradeSpawnerData(minute);
        }

        int mosterKind = Random.Range(0, enemyPres.Count);

        if(currentSpawnerGrade.spawnerTime > 0)
        {
            currentSpawnerGrade.spawnerTime -= Time.deltaTime;
        }
        else if(currentSpawnerGrade.maxEnemyNum > currentEnemyNum)
        {
            int spawnerNum = Random.Range(1, 5);

            for(int i = 0; i < spawnerNum; i++)
            {
                CreatEnemy(mosterKind);
            }

            if(currentEnemyNum < currentSpawnerGrade.minEnemyNum)
            {
                currentSpawnerGrade.spawnerTime = currentSpawnerGrade.spawnerSpeed / 2;
            }
            else
            {
                currentSpawnerGrade.spawnerTime = currentSpawnerGrade.spawnerSpeed;
            }
        }
    }

    private void LoadEnemyAsset(int kind)
    {
        enemyPres[kind].LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                loadEnemyPres.Add(handle);

                if(loadEnemyPres.Count == enemyPres.Count)
                {
                    int mosterKind = Random.Range(0, enemyPres.Count);

                    for (int i = 0; i < 10; i++)
                    {
                        CreatEnemy(mosterKind);
                    }
                }
            }
        };
    }

    private void CreatEnemy(int kind)
    { 
        EnemyData_SO data = dataController.enemyCurrentDatas.enmeyDatas.Find(enemyData => enemyData.enemyName == loadEnemyPres[kind].Result.name);
        GameObject enemy = objectPool.CreateObject(loadEnemyPres[kind].Result.name, loadEnemyPres[kind].Result, gameObject, SelectSpawnPoint(), Quaternion.identity);
        enemy.GetComponent<EnemyController>().enemyCurrentData = Instantiate(data);

        currentEnemyNum++;
    }

    public void UpGradeSpawnerData(int minute)
    {
        foreach(EnemySpawnerGrade level in enemySpawnerGrades)
        {
            if(level.startTime <= minute && level.endTime >= minute)
            {
                currentSpawnerGrade = level;
                break;
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;

        if(spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if(Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    public void CreateBossSeal()
    {
        bossSeal.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            objectPool.CreateObject(handle.Result.name, handle.Result, gameObject,
                new Vector3(Random.Range(bossSealMinPosition.x, bossSealMaxPosition.x), Random.Range(bossSealMinPosition.y, bossSealMaxPosition.y), 0), Quaternion.identity);
        };
        bossSeal.ReleaseAsset();
    }

    public void CreateBoss()
    {
        int mosterKind = Random.Range(0, enemyPres.Count);

        GameObject enemy = objectPool.CreateObject(loadEnemyPres[mosterKind].Result.name, loadEnemyPres[mosterKind].Result,
                                                    gameObject, SelectSpawnPoint(), Quaternion.identity);

        EnemyData_SO data = dataController.enemyCurrentDatas.enmeyDatas.Find(enemy => enemy.enemyName == loadEnemyPres[mosterKind].Result.name);

        EnemyData_SO getData = Instantiate(data);
        getData.isBoss = true;
        getData.isAttack = true;
        getData.maxHealth = data.maxHealth * 30;
        getData.currentHealth = getData.maxHealth;
        getData.defence = data.defence * 2;
        getData.attackDamage = data.attackDamage * 2;
        getData.coolDown = data.coolDown / 2;

        enemy.GetComponent<EnemyController>().enemyCurrentData = getData;
        enemy.GetComponent<HealthBarUI>().CreateHealthBar();
    }

    public void CloseAllEnemy()
    {
        for(int i = 0; i < loadEnemyPres.Count; i++)
        {
            objectPool.CollectObject(loadEnemyPres[i].Result.name);
        }
    }
}
