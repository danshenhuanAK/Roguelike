using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBUFF : MonoBehaviour
{
    private ObjectPool objectPool;

    public Button reselectButton;
    public Button cleanUpButton;
    public Button skipButton;
    public GameObject buttonParent;
    public List<GameObject> showUI = new List<GameObject>();

    private HashSet<int> chooseBuffArray;

    private int screenWidth;            //屏幕宽

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        screenWidth = Screen.width;

        /*if(gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }*/
    }

    private void OnEnable()
    {
        Time.timeScale = 0;                     //游戏暂停

        ShowUI();                               //随机显示buff
    }

    private void ShowUI()
    {
        int v_x = 0;

        chooseBuffArray = GetRandomUI(showUI.Count, 3);         //在0~showUI.Count数量中选择三个不重复随机数

        foreach (var item in chooseBuffArray)
        {
            showUI[item].transform.localPosition = new Vector3(0, 270 - (210 * v_x), 0);
            showUI[item].SetActive(true);
            v_x += 1;
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
            objectPool.CollectObject(showUI[i].name);
        }
    }

    public void ClearUI(int clearObj)
    {
        showUI.Remove(showUI[clearObj]);
    }
}
