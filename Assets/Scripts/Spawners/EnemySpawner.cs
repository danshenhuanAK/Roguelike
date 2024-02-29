using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyCurrentAttributes> enemyAttributes;
    public List<EnemyCurrentAttributes> enemyCurrentAttributes;

    public int currentFloor;                                //当前层数
    public int currentEnemyNum;                             //场景当前怪物数量

    [System.Serializable]
    public class LevelEnemy
    {
        public int startTime;
        public int endTime;

        public float spawnerSpeed;
        public float spawnerTime;
        public float maxEnemyNum;
        public float minEnemyNum;
    }

    public List<LevelEnemy> levelEnemy;
    private LevelEnemy currentLevelEnemy;

    public Transform minSpawn;
    public Transform maxSpawn;

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        enemyCurrentAttributes = enemyAttributes;
    }

    private void Start()
    {
        minSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("MinSpawn");
        maxSpawn = GameObject.FindGameObjectWithTag("Player").transform.Find("MaxSpawn");

        currentLevelEnemy = levelEnemy[0];

        int mosterKind = Random.Range(0, enemyCurrentAttributes.Count);

        for (int i = 0; i < 10; i++)
        {
            GameObject enemy = objectPool.CreateObject(enemyCurrentAttributes[mosterKind].enemyPrefab.name, enemyCurrentAttributes[mosterKind].enemyPrefab,
                                        gameObject, SelectSpawnPoint(), Quaternion.identity);

            SpawnerData(enemy.GetComponent<EnemyController>().enemyCurrentAttribute, enemyCurrentAttributes[mosterKind].enemyCurrentAttributes);

            currentEnemyNum++;
        }
    }

    public void SpawnerData(EnemyCurrentAttribute enemyCurrentAttribute, EnemyCurrentAttribute attribute)
    {
        enemyCurrentAttribute.maxHealth = attribute.maxHealth;
        enemyCurrentAttribute.currentHealth = attribute.currentHealth;
        enemyCurrentAttribute.defence = attribute.defence;
        enemyCurrentAttribute.moveSpeed = attribute.moveSpeed;
        enemyCurrentAttribute.attackDamage = attribute.attackDamage;
        enemyCurrentAttribute.coolDown = attribute.coolDown;
        enemyCurrentAttribute.isAttack = attribute.isAttack;
        enemyCurrentAttribute.isBoss = attribute.isBoss;
    }

    private void Update()
    {
        int mosterKind = Random.Range(0, enemyCurrentAttributes.Count);

        if(currentLevelEnemy.spawnerTime > 0)
        {
            currentLevelEnemy.spawnerTime -= Time.deltaTime;
        }
        else
        {
            int spawnerNum = Random.Range(1, 5);

            for(int i = 0; i < spawnerNum; i++)
            {
                GameObject enemy = objectPool.CreateObject(enemyCurrentAttributes[mosterKind].enemyPrefab.name, enemyCurrentAttributes[mosterKind].enemyPrefab,
                                        gameObject, SelectSpawnPoint(), Quaternion.identity);
                SpawnerData(enemy.GetComponent<EnemyController>().enemyCurrentAttribute, enemyCurrentAttributes[mosterKind].enemyCurrentAttributes);

                currentEnemyNum++;
            }

            if(currentEnemyNum < currentLevelEnemy.minEnemyNum)
            {
                currentLevelEnemy.spawnerTime = currentLevelEnemy.spawnerSpeed / 2;
            }
            else
            {
                currentLevelEnemy.spawnerTime = currentLevelEnemy.spawnerSpeed;
            }
        }
    }

    public void GetSpawnerData(int minute)
    {
        foreach(LevelEnemy level in levelEnemy)
        {
            if(level.startTime >= minute && level.endTime <= minute)
            {
                currentLevelEnemy = level;
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

    public void ClearMonster()
    {
        for(int i = 0; i < enemyCurrentAttributes.Count; i++)
        {
            objectPool.CollectObject(enemyCurrentAttributes[i].enemyPrefab.name);
        }
    }
}
