using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Vector2 scrrenRange;                            //屏幕视图xy大小

    public GameObject[] gameObjects;
    private GameObject player;

    public int currentFloor;                                //当前层数
    public float spawnerSpeed;                              //生成速度
    private float currentTime;                              //当前时间(根据帧数)
    public int maxEnemyNum;                                 //场景最大怪物数量
    public int minEnemyNum;                                 //场景最小怪物数量
    public int currentEnemyNum;                             //场景当前怪物数量

    public Transform minSpawn;
    public Transform maxSpawn;

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        //y值为主相机投影大小,x值为y值*屏幕宽高比
        scrrenRange.y = Camera.main.orthographicSize;
        scrrenRange.x = scrrenRange.y * (((float)Screen.width / (float)Screen.height));
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        int mosterKind = Random.Range(0, gameObjects.Length);

        for (int i = 0; i < 10; i++)
        {
            objectPool.CreateObject(gameObjects[mosterKind].name, gameObjects[mosterKind], gameObject, SelectSpawnPoint(), Quaternion.identity);
            currentEnemyNum++;
        }
    }

    private void Update()
    {
        int mosterKind = Random.Range(0, gameObjects.Length);

        if (currentEnemyNum < minEnemyNum)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = spawnerSpeed / 2;

                objectPool.CreateObject(gameObjects[mosterKind].name, gameObjects[mosterKind], gameObject, SelectSpawnPoint(), Quaternion.identity);
                currentEnemyNum++;
            }
        }
        else
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = spawnerSpeed;

                objectPool.CreateObject(gameObjects[mosterKind].name, gameObjects[mosterKind], gameObject, SelectSpawnPoint(), Quaternion.identity);
                currentEnemyNum++;
            }
        }
    }

    private Vector3 SelectSpawnPoint()
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
        for(int i = 0; i < gameObjects.Length; i++)
        {
            objectPool.CollectObject(gameObjects[i].name);
        }
    }
}
