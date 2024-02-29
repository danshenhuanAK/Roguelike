using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    private ObjectPool objectPool;
    private AttributeManager attributeManager;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        attributeManager = AttributeManager.Instance;
    }

    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public Vector2 dropRate;
    }

    public List<Drops> drops;

    private void OnDestroy()
    {
        int randomNumber = Random.Range((int)attributeManager.currentAttribute.luck, 100);

        for(int i = 0; i < drops.Count; i++)
        {
            if(randomNumber > drops[i].dropRate.x && randomNumber < drops[i].dropRate.y)
            {
                objectPool.CreateObject(drops[i].name, drops[i].itemPrefab, transform.parent.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
