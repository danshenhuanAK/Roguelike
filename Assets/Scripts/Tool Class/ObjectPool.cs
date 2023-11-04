using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            gameObject.transform.position = position;
            gameObject.transform.rotation = quaternion;
            gameObject.SetActive(true);
        }
        else                    //Ϊ���򴴽�
        {
            gameObject = Instantiate(gameObj, position, quaternion, parentObject.transform);
            Add(key, gameObject);
        }

        return gameObject;
    }

    private GameObject FindPoolkey(string key)      //���Ҽ�Ϊkey�����ض���
    {
        if(dir.ContainsKey(key))
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

    public void ClearAll()              //�������(ѭ������Clear)
    {
        List<string> list = new List<string>(dir.Keys);

        for(int i = 0; i < dir.Keys.Count; i++)
        {
            Clear(list[i]);
        }
    }

    public void CollectObject(string go)       //���ü�ֵΪgo����Ϸ����
    {
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
