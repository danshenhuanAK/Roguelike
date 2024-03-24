using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;              //���ο�Ԥ����
    private GameObject player;                          //����

    public float checkerRadius;                         //���ԭ��
    public float CheckDistance;
    Vector3 noTerrainPosition;                          //����λ��
    public LayerMask terrainMask;                       //���ͼ��

    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;                             //�������tilemap�ĳ��ȺͿ��
    float opDist;                                       //���ǵ����ο����ĵľ���
    float optimizerCooldown;
    public float optimizerCooldownDur;                  //��֡���һ��

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

    private void ChunkChecker()                 //�ж��µ��ο�����λ��
    {
        if(!currentChunk)
        {
            return;
        }

        Vector3 positionDeviate = player.transform.position - currentChunk.transform.position;          //����λ�������ȿ�λ�ò�

        if(positionDeviate.x >= 0 && positionDeviate.y >= 0)              //���Ͻ�(������ϣ��ң���)
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

            if(Vector3.Distance(player.transform.position, currentChunk.transform.position) >= CheckDistance)       //������˿�ʱ�����ɷ�ֹ�����ɾ�����
            {
                if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
                {
                    noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                    SpawnChunk();
                }
            }
        }
        else if(positionDeviate.x >= 0 && positionDeviate.y <= 0)            //���½�(������£��ң���)
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
        else if (positionDeviate.x <= 0 && positionDeviate.y >= 0)            //���Ͻ�(������ϣ�����)
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
        else if (positionDeviate.x <= 0 && positionDeviate.y <= 0)            //���½�(������£�����)
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

    private void SpawnChunk()       //���ɵ��ο�
    {
        int rand = Random.Range(0, terrainChunks.Count);

        latestChunk = objectPool.CreateObject(terrainChunks[rand].name, terrainChunks[rand], gameObject, noTerrainPosition,Quaternion.identity);

        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimizer()               //�Ż�����
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)                    //��֡���һ��
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach (GameObject chunk in spawnedChunks)         //���ɫ̫Զ�ĵ��ο�����������
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
