using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
    }

    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    private void OnDestroy()
    {
        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach(Drops rate in drops)
        {
            if(randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        if(possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
            objectPool.CreateObject(drops.itemPrefab.name, drops.itemPrefab, transform.parent.gameObject, transform.position, Quaternion.identity);
        }

        
    }
}
