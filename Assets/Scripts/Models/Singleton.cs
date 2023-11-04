using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static readonly object syslock = new object();

    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)      //双锁解决多线程冲突
            {
                lock (syslock) 
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if(instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        instance = (T)obj.AddComponent(typeof(T));
                    }
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    public static bool IsInitialized
    {
        get
        { 
            return instance != null;
        }
    }

    protected virtual void OnDestory()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

}
