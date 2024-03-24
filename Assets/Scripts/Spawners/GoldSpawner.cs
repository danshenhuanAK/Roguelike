using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GoldSpawner : MonoBehaviour
{
    private ObjectPool objectPool;
    private FightProgressAttributeManager attributeManager;

    public int spawnerLuck;
    public AssetReference goldPre;

    private GameObject goldLoadPre;
    [HideInInspector]
    public List<GameObject> generatedGold;
    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        attributeManager = FightProgressAttributeManager.Instance;

        goldPre.LoadAssetAsync<GameObject>().Completed += (handle) =>
        { 
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                goldLoadPre = handle.Result;
            }
        };
    }

    public void CreatGold(Transform enemyPos)
    {
        int random = Random.Range(attributeManager.playerData.luck, 101);

        Vector3 creatPos = new(enemyPos.position.x + Random.Range(0, 0.5f), enemyPos.position.y + Random.Range(0, 0.5f), 0);

        if(random > spawnerLuck)
        {
            generatedGold.Add(objectPool.CreateObject(goldLoadPre.name, goldLoadPre, gameObject, creatPos, Quaternion.identity));
        }
    }

    public void CloseAllGold()
    {
        foreach(GameObject gold in generatedGold)
        {
            gold.SetActive(false);
        }
    }
}
