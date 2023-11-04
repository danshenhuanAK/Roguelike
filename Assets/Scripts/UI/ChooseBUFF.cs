using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBUFF : MonoBehaviour
{
    private ObjectPool objectPool;

    public List<GameObject> showUI = new List<GameObject>();

    private HashSet<int> chooseBuffArray;

    private int screenWidth;            //��Ļ��

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        screenWidth = Screen.width;

        if(gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;                     //��Ϸ��ͣ

        ShowUI();                               //�����ʾbuff
    }

    private void ShowUI()
    {
        int v_x = -1;

        chooseBuffArray = GetRandomUI(showUI.Count, 3);         //��0~showUI.Count������ѡ���������ظ������

        foreach (var item in chooseBuffArray)
        {
            objectPool.CreateObject(showUI[item].name, showUI[item], gameObject,
                                    new Vector3(transform.position.x + screenWidth * 0.125f * v_x, transform.position.y, 0), Quaternion.identity);
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
