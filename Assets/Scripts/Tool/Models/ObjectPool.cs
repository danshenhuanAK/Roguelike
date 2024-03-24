using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectPool : Singleton<ObjectPool>
{
    private ObjectPool()
    {

    }

    //字典 ------> 键为名字，值为一个列表
    private Dictionary<string, List<GameObject>> dir = new Dictionary<string, List<GameObject>>();

    public GameObject CreateObject(string key, GameObject gameObj, GameObject parentObject, Vector3 position, Quaternion quaternion)
    {
        GameObject gameObject = FindPoolkey(key);   //找到键为key的对象

        if(gameObject != null)             
        {
            gameObject.transform.SetPositionAndRotation(position, quaternion);
            gameObject.SetActive(true);
        }
        else                    //为空则创建
        {
            gameObject = Instantiate(gameObj, position, quaternion, parentObject.transform);
            gameObject.name = key;
            Add(key, gameObject);
        }

        return gameObject;
    }

    private GameObject FindPoolkey(string key)      //查找键为key的隐藏对象
    {
        if(dir.ContainsKey(key) && dir[key] != null)
        {
            return dir[key].Find(p => !p.activeSelf);
        }
        return null;
    }

    private GameObject FindActivePoolkey(string key)      //查找键为key的显现对象
    {
        if (dir.ContainsKey(key))
        {
            return dir[key].Find(p => p.activeSelf);
        }
        return null;
    }

    private void Add(string key, GameObject gameObject)     //将创建的对象放入字典
    {
        if(!dir.ContainsKey(key))
        {
            dir.Add(key, new List<GameObject>());
        }
        dir[key].Add(gameObject);
    }

    private void Clear(string key)          //清除键为key的所有值
    {
        if(dir.ContainsKey(key))
        {
            for(int i = 0; i < dir[key].Count; i++)
            {
                Destroy(dir[key][i]);
            }
            dir.Remove(key);
        }
    }

    public void ClearAll()                  //清除保存的游戏物体
    {
        dir = new();
    }

    public void CollectObject(string go)       //禁用键值为go的游戏物体
    {
        if(!dir.ContainsKey(go))
        {
            return;
        }

        for(int i = 0; i < dir[go].Count; i++)
        {
            GameObject uiActive = FindActivePoolkey(go);

            if (uiActive == null)
            {
                return;
            }

            uiActive.SetActive(false);          
        }
    }
    public int ObjectNum()
    {
        return dir.Count;
    }
    //延时回收
    public void CollectObject(string go, float delay)
    {
        StartCoroutine(CollectDelay(go, delay));            //开启携程
    }

    private IEnumerator CollectDelay(string go, float delay)
    {
        yield return new WaitForSeconds(delay);         //delay后禁用
        CollectObject(go);
    }
}
