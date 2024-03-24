using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> propSpawnPoints;
    [SerializeField]
    private List<GameObject> propPrefabs;

    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
    }

    private void Start()
    {
        SpawnProps();
    }

    private void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);

            objectPool.CreateObject(propPrefabs[rand].name, propPrefabs[rand], sp, sp.transform.position, Quaternion.identity);
        }
    }
}
