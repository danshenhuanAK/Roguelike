using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRelicUI : MonoBehaviour
{
    public List<GameObject> commonRelic;                                        //可获得的普通遗物
    public List<GameObject> rareRelic;                                          //稀有遗物
    public List<GameObject> legendRelic;                                        //传奇遗物

    private List<GameObject> existingRelics = new List<GameObject>();           //已获得的遗物

    public int[] luckyRange;

    private AttributeManager attributeManager;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
    }

    public void GetRandomRelic(int randomNumber)
    {
        while(randomNumber != 0)
        {
            int luckyValue = Random.Range((int)attributeManager.currentAttribute.luck, 100);

            if (luckyValue <= luckyRange[0] && commonRelic.Count > 0)
            {
                GetCommonRelic(1);
                randomNumber--;
            }
            else if (luckyRange[0] < luckyValue && luckyValue <= luckyRange[1] && rareRelic.Count > 0)
            {
                GetRareRelic(1);
                randomNumber--;
            }
            else if (luckyRange[1] < luckyValue && luckyValue <= luckyRange[2] && legendRelic.Count > 0)
            {
                GetLegendRelic(1);
                randomNumber--;
            }

            /*getRelic.transform.localPosition = new Vector3(startPoint.x + (existingRelics.Count % 3) * intervalValue.x,
                startPoint.y - (existingRelics.Count / 3) * intervalValue.y, 0);*/
        }
    }

    public void GetCommonRelic(int randomNumber)
    {
        for (int i = 0; i < randomNumber; i++)
        {
            int random = Random.Range(0, commonRelic.Count);
            commonRelic.RemoveAt(random);
            existingRelics.Add(commonRelic[random]);
        }
    }

    public void GetRareRelic(int randomNumber)
    {
        for(int i = 0; i < randomNumber; i++)
        {
            int random = Random.Range(0, rareRelic.Count);
            rareRelic.RemoveAt(random);
            existingRelics.Add(rareRelic[random]);
        }
    }

    public void GetLegendRelic(int randomNumber)
    {
        for (int i = 0; i < randomNumber; i++)
        {
            int random = Random.Range(0, legendRelic.Count);

            legendRelic.RemoveAt(random);
            existingRelics.Add(legendRelic[random]);
        }
    }
}
