using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGemSpawner : MonoBehaviour
{
    private ObjectPool objectPool;
    private FightProgressAttributeManager attributeManager;

    [HideInInspector]
    public List<GameObject> generatedGem;                                               //已生成的宝石

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        attributeManager = FightProgressAttributeManager.Instance;
    }

    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public Vector2 dropRate;
    }

    public List<Drops> drops;

    public void Destroy(Transform enemyPos)
    {
        if(attributeManager.playerData.luck < 0)
        {
            generatedGem.Add(objectPool.CreateObject(drops[0].name, drops[0].itemPrefab, gameObject, enemyPos.position, Quaternion.identity));
            return;
        }

        int randomNumber = Random.Range((int)attributeManager.playerData.luck, 100);

        for (int i = 0; i < drops.Count; i++)
        {
            if (randomNumber >= drops[i].dropRate.x && randomNumber < drops[i].dropRate.y)
            {
                generatedGem.Add(objectPool.CreateObject(drops[i].name, drops[i].itemPrefab, gameObject, enemyPos.position, Quaternion.identity));
                break;
            }
        }
    }

    public void GetAllGem()
    {
        for(int i = 0; i < generatedGem.Count; i++)
        {
            generatedGem[i].GetComponent<ExpPickup>().isPicked = true;
        }
    }

    public void CloseAllGem()
    {
        for(int i = 0; i < generatedGem.Count; i++)
        {
            generatedGem[i].SetActive(false);
        }
    }
}
