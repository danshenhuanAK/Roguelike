using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] gameObjects;
    private GameObject player;

    public int currentFloor;                                //��ǰ����
    public float spawnerSpeed;                              //�����ٶ�
    private float currentTime;                              //��ǰʱ��(����֡��)
    public int maxEnemyNum;                                 //��������������
    public int minEnemyNum;                                 //������С��������
    public int currentEnemyNum;                             //������ǰ��������

    public Transform minSpawn;
    public Transform maxSpawn;

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
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

        if(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            objectPool.CreateObject(gameObjects[mosterKind].name, gameObjects[mosterKind], gameObject, SelectSpawnPoint(), Quaternion.identity);
            currentEnemyNum++;

            if(currentEnemyNum < minEnemyNum)
            {
                currentTime = spawnerSpeed / 2;
            }
            else
            {
                currentTime = spawnerSpeed;
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
