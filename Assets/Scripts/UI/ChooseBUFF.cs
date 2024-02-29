using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBUFF : MonoBehaviour
{
    public List<GameObject> showUI = new List<GameObject>();
    private AttributeManager attributeManager;

    public float[] buffPoint;

    private HashSet<int> chooseBuffArray;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;                     //游戏暂停

        ShowUI();                               //随机显示buff
    }

    public void ShowUI()
    {
        int v_x = 0;

        if(Random.Range(0, 100) <= attributeManager.currentAttribute.luck)         //幸运值越高升级后可能多一个buff选择框
        {
            chooseBuffArray = GetRandomUI(showUI.Count, 4);
        }
        else
        {
            chooseBuffArray = GetRandomUI(showUI.Count, 3);
        }

        foreach (var item in chooseBuffArray)
        {
            showUI[item].transform.localPosition = new Vector3(0, buffPoint[v_x], 0);
            showUI[item].SetActive(true);
            v_x++;
        }
    }

    private HashSet<int> GetRandomUI(int length, int count)
    {
        HashSet<int> nums = new HashSet<int>();
        System.Random r = new System.Random();

        while(nums.Count != count)
        {
            nums.Add(r.Next(0, length));
        }

        return nums;
    }

    public void CollectUI()
    {
        if(chooseBuffArray == null)
        {
            return;
        }
        foreach (var i in chooseBuffArray)
        {
            showUI[i].SetActive(false);
        }
    }

    public void ClearUI(string clearObj)
    {
        for(int i = 0; i < showUI.Count; i++)
        {
            if(showUI[i].name == clearObj)
            {
                showUI.Remove(showUI[i]);
            }
        }
    }

    public int reselectSpend;

    public int buyAllSpend;

    public GameObject cleanUpButton;
    public int cleanUpSpend;

    public void ReselectBuff()
    {
        attributeManager.Gold(reselectSpend);
        CollectUI();
        ShowUI();
    }

    public bool isButtonCleanUp;

    public void CleanUp()
    {
        if(isButtonCleanUp == false)
        {
            isButtonCleanUp = true;

            cleanUpButton.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
        }
        else
        {
            cleanUpButton.GetComponent<Image>().color = Color.white;
            isButtonCleanUp = false;
        }
    }

    public void CleanUpSpend()
    {
        attributeManager.Gold(cleanUpSpend);
    }

    public void BuyAll()
    {
        foreach (var item in chooseBuffArray)
        {
            showUI[item].GetComponent<Button>().onClick.Invoke();
        }
        attributeManager.Gold(buyAllSpend);
    }
}
