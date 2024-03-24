using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Linq;

public class SkillSpawnerController : MonoBehaviour, ISkillSaveable
{
    private DataManager dataManager;

    public PlayerSkillDataList playerSkillDataList = new();

    public List<AssetReference> creatSkillSpawners;

    private void Awake()
    {
        dataManager = DataManager.Instance;

        ISkillSaveable saveable = this;
        saveable.RegisterSkillData();
    }

    private void Start()
    {
        if(!dataManager.IsSave())
        {
            int random = Random.Range(0, creatSkillSpawners.Count);

            creatSkillSpawners[random].LoadAssetAsync<GameObject>().Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject skillSpawner = Instantiate(handle.Result, gameObject.transform);
                    skillSpawner.name = handle.Result.name;
                }
            };
            creatSkillSpawners[random].ReleaseAsset();
        }
    }

    private void OnDestroy()
    {
        ISkillSaveable saveable = this;
        saveable.UnRegisterSkillData();
    }

    public void GetSkillData(PlayerSkillDataList skillData)
    {
        skillData.playerSkillDatas = playerSkillDataList.playerSkillDatas;
    }

    public void LoadSkillData(PlayerSkillDataList skillData)
    {
        playerSkillDataList.playerSkillDatas = skillData.playerSkillDatas;

        for(int i = 0; i < skillData.playerSkillDatas.Count; i++)
        {
            string skillSpawnerName = skillData.playerSkillDatas[i].skillSpanwerName;

            Addressables.LoadAssetAsync<GameObject>(skillSpawnerName).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject skillSpawner = Instantiate(handle.Result, gameObject.transform);
                    skillSpawner.name = handle.Result.name;

                    Addressables.Release(handle);
                }
            };
        }
    }
}
