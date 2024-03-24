using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;              //地形块预制体
    private GameObject player;                          //主角

    public float checkerRadius;                         //检测原点
    public float CheckDistance;
    Vector3 noTerrainPosition;                          //生成位置
    public LayerMask terrainMask;                       //检测图层

    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;                             //必须大于tilemap的长度和宽度
    float opDist;                                       //主角到地形块中心的距离
    float optimizerCooldown;
    public float optimizerCooldownDur;                  //几帧检测一次

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
    }

    private void OnEnable()
    {
        SpawnChunk();
        currentChunk = spawnedChunks[0];
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        noTerrainPosition = player.transform.position;
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    private void ChunkChecker()                 //判定新地形块生成位置
    {
        if(!currentChunk)
        {
            return;
        }

        Vector3 positionDeviate = player.transform.position - currentChunk.transform.position;          //主角位置与所踩块位置差

        if(positionDeviate.x >= 0 && positionDeviate.y >= 0)              //右上角(检测右上，右，上)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }

            if(Vector3.Distance(player.transform.position, currentChunk.transform.position) >= CheckDistance)       //快脱离此块时再生成防止刚生成就隐藏
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                    SpawnChunk();
                }
            }
        }
        else if(positionDeviate.x >= 0 && positionDeviate.y <= 0)            //右下角(检测右下，右，下)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }

            if (Vector3.Distance(player.transform.position, currentChunk.transform.position) >= CheckDistance)    
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                    SpawnChunk();
                }
            }
        }
        else if (positionDeviate.x <= 0 && positionDeviate.y >= 0)            //左上角(检测左上，左，上)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }

            if (Vector3.Distance(player.transform.position, currentChunk.transform.position) >= CheckDistance)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                    SpawnChunk();
                }
            }
        }
        else if (positionDeviate.x <= 0 && positionDeviate.y <= 0)            //左下角(检测左下，左，下)
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }

            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }

            if (Vector3.Distance(player.transform.position, currentChunk.transform.position) >= CheckDistance)
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                    SpawnChunk();
                }
            }
        }
    }

    private void SpawnChunk()       //生成地形块
    {
        int rand = Random.Range(0, terrainChunks.Count);

        latestChunk = objectPool.CreateObject(terrainChunks[rand].name, terrainChunks[rand], gameObject, noTerrainPosition,Quaternion.identity);

        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimizer()               //优化程序
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)                    //几帧检测一次
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)         //离角色太远的地形块先隐藏起来
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);

            if(opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
