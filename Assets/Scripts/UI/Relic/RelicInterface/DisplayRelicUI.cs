using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DisplayRelicUI : MonoBehaviour
{
    public List<AssetReference> commonRelics;                                   //普通遗物
    public List<AssetReference> rareRelics;                                     //稀有遗物
    public List<AssetReference> legendRelics;                                   //传奇遗物

    private List<GameObject> existingRelics = new();                            //已获得的遗物

    public int[] luckyRange;                                                    //遗物品质几率

    public Vector2 startPoint;
    public Vector2 intervalValue;

    private FightProgressAttributeManager attributeManager;
    private DataManager dataManager;

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
        dataManager = DataManager.Instance;
    }

    private void Start()
    {
        if(dataManager.relicDatas.Count != 0)
        {
            foreach(RelicData data in dataManager.relicDatas)
            {
                if(data.relicQuality == 0)
                {
                    LoadRelic(commonRelics, data);
                }
                else if(data.relicQuality == 1)
                {
                    LoadRelic(rareRelics, data);
                }
                else
                {
                    LoadRelic(legendRelics, data);
                }
            }
        }
    }

    private void LoadRelic(List<AssetReference> relicList, RelicData data)
    {
        relicList[data.relicNum].LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject getRelic = Instantiate(handle.Result, gameObject.transform);

                getRelic.GetComponent<RelicFunction>().relicData = data;
                getRelic.transform.localPosition = new Vector3((float)data.relicPosX, (float)data.relicPosY, 0);

                existingRelics.Add(handle.Result);
            }
        };
        relicList[data.relicNum].ReleaseAsset();
    }

    public void GetRandomRelic(int randomNumber)
    {
        while (randomNumber != 0)
        {
            int luckyValue = Random.Range(attributeManager.playerData.luck, 101);

            if (luckyValue <= luckyRange[0])
            {
                GetCommonRelic(1);
            }
            else if (luckyRange[0] < luckyValue && luckyValue <= luckyRange[1])
            {
                GetRareRelic(1);
            }
            else if (luckyRange[1] < luckyValue && luckyValue <= luckyRange[2])
            {
                GetLegendRelic(1);
            }
            randomNumber--;
        }
    }

    private HashSet<int> commonNum = new();

    public void GetCommonRelic(int randomNumber)
    {
        for (int i = 0; i < randomNumber; i++)
        {
            int num = commonNum.Count;

            if (num == commonRelics.Count)
            {
                return;
            }

            int random = 0;
            while (num == commonNum.Count)
            {
                random = Random.Range(0, commonRelics.Count);
                commonNum.Add(random);
            }

            commonRelics[random].LoadAssetAsync<GameObject>().Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject getRelic = Instantiate(handle.Result, gameObject.transform);

                    getRelic.GetComponent<RelicFunction>().AtGetStart();
                    getRelic.GetComponent<RelicFunction>().relicData.relicNum = random;
                    getRelic.GetComponent<RelicFunction>().relicData.relicQuality = 0;
                    getRelic.transform.localPosition = new Vector3(startPoint.x + (existingRelics.Count % 3) * intervalValue.x,
                        startPoint.y - (existingRelics.Count / 3) * intervalValue.y, 0);
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosX = getRelic.transform.localPosition.x;
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosY = getRelic.transform.localPosition.y;

                    existingRelics.Add(handle.Result);
                }
            };
            commonRelics[random].ReleaseAsset();
        }
    }

    private HashSet<int> rareNum = new();

    public void GetRareRelic(int randomNumber)
    {
        for(int i = 0; i < randomNumber; i++)
        {
            int num = rareNum.Count;

            if(num == rareRelics.Count)
            {
                return;
            }

            int random = 0;
            while (num == rareNum.Count)
            {
                random = Random.Range(0, rareRelics.Count);
                rareNum.Add(random);
            }

            rareRelics[random].LoadAssetAsync<GameObject>().Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject getRelic = Instantiate(handle.Result, gameObject.transform);

                    getRelic.GetComponent<RelicFunction>().relicData.relicNum = random;
                    getRelic.GetComponent<RelicFunction>().relicData.relicQuality = 1;
                    getRelic.transform.localPosition = new Vector3(startPoint.x + (existingRelics.Count % 3) * intervalValue.x,
                        startPoint.y - (existingRelics.Count / 3) * intervalValue.y, 0);
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosX = getRelic.transform.localPosition.x;
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosY = getRelic.transform.localPosition.y;

                    existingRelics.Add(handle.Result);
                }
            };
            rareRelics[random].ReleaseAsset();
        }
    }

    private HashSet<int> legendNum = new();

    public void GetLegendRelic(int randomNumber)
    {
        for (int i = 0; i < randomNumber; i++)
        {
            int num = legendNum.Count;

            if (num == legendRelics.Count)
            {
                return;
            }

            int random = 0;
            while (legendNum.Count == num)
            {
                random = Random.Range(0, legendRelics.Count);
                legendNum.Add(random);
            }

            legendRelics[random].LoadAssetAsync<GameObject>().Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject getRelic = Instantiate(handle.Result, gameObject.transform);

                    getRelic.GetComponent<RelicFunction>().relicData.relicNum = random;
                    getRelic.GetComponent<RelicFunction>().relicData.relicQuality = 2;
                    getRelic.transform.localPosition = new Vector3(startPoint.x + (existingRelics.Count % 3) * intervalValue.x,
                        startPoint.y - (existingRelics.Count / 3) * intervalValue.y, 0);
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosX = getRelic.transform.localPosition.x;
                    getRelic.GetComponent<RelicFunction>().relicData.relicPosY = getRelic.transform.localPosition.y;

                    existingRelics.Add(handle.Result);
                }
            };
            legendRelics[random].ReleaseAsset();
        }
    }
}
