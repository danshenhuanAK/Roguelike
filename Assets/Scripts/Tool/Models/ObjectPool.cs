using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectPool : Singleton<ObjectPool>
{
    private ObjectPool()
    {

    }

    //�ֵ� ------> ��Ϊ���֣�ֵΪһ���б�
    private Dictionary<string, List<GameObject>> dir = new Dictionary<string, List<GameObject>>();

    public GameObject CreateObject(string key, GameObject gameObj, GameObject parentObject, Vector3 position, Quaternion quaternion)
    {
        GameObject gameObject = FindPoolkey(key);   //�ҵ���Ϊkey�Ķ���

        if(gameObject != null)             
        {
            gameObject.transform.SetPositionAndRotation(position, quaternion);
            gameObject.SetActive(true);
        }
        else                    //Ϊ���򴴽�
        {
            gameObject = Instantiate(gameObj, position, quaternion, parentObject.transform);
            gameObject.name = key;
            Add(key, gameObject);
        }

        return gameObject;
    }

    private GameObject FindPoolkey(string key)      //���Ҽ�Ϊkey�����ض���
    {
        if(dir.ContainsKey(key) && dir[key] != null)
        {
            return dir[key].Find(p => !p.activeSelf);
        }
        return null;
    }

    private GameObject FindActivePoolkey(string key)      //���Ҽ�Ϊkey�����ֶ���
    {
        if (dir.ContainsKey(key))
        {
            return dir[key].Find(p => p.activeSelf);
        }
        return null;
    }

    private void Add(string key, GameObject gameObject)     //�������Ķ�������ֵ�
    {
        if(!dir.ContainsKey(key))
        {
            dir.Add(key, new List<GameObject>());
        }
        dir[key].Add(gameObject);
    }

    private void Clear(string key)          //�����Ϊkey������ֵ
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

    public void ClearAll()                  //����������Ϸ����
    {
        dir = new();
    }

    public void CollectObject(string go)       //���ü�ֵΪgo����Ϸ����
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
    //��ʱ����
    public void CollectObject(string go, float delay)
    {
        StartCoroutine(CollectDelay(go, delay));            //����Я��
    }

    private IEnumerator CollectDelay(string go, float delay)
    {
        yield return new WaitForSeconds(delay);         //delay�����
        CollectObject(go);
    }
}
