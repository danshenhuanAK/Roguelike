using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventDelegate(object[] args);
    private Dictionary<string, Dictionary<int, EventDelegate>> eventListeners = new();

    public void Regist(string eventName, EventDelegate handler)
    {
        if(handler == null)
        {
            return;
        }

        if(!eventListeners.ContainsKey(eventName))
        {
            eventListeners.Add(eventName, new Dictionary<int, EventDelegate>());
        }

        var handlerDic = eventListeners[eventName];
        var handlerHash = handler.GetHashCode();

        if(handlerDic.ContainsKey(handlerHash))
        {
            handlerDic.Remove(handlerHash);
        }

        eventListeners[eventName].Add(handler.GetHashCode(), handler);
    }

    public void UnRegist(string eventName, EventDelegate handLer)
    {
        if (handLer == null)
        {
            return;
        }

        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName].Remove(handLer.GetHashCode());
            
            if (eventListeners[eventName] == null || eventListeners[eventName].Count == 0)
            {
                eventListeners.Remove(eventName);
            }
        }
    }

    public void DispatchEvent(string eventName, params object[] objs)
    {
        // 如果包含eventName这个ID
        if (eventListeners.ContainsKey(eventName))
        {
            //获取eventName键对应的所有执行事件
            var handlerDic = eventListeners[eventName];
            if (handlerDic != null && handlerDic.Count > 0)
            {
                var dic = new Dictionary<int, EventDelegate>(handlerDic);
                //通过对eventName键对应的所有执行进行遍历，然后运行
                foreach (var f in dic.Values)
                {
                    try
                    {
                        //执行所有的委托事件，即EventDelegate(object[] args)
                        f(objs);
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }
                }
            }

        }
    }

    public void ClearEvents(string eventName)
    {
        //如果事件字典中包含eventName中的键，则移除触发事件对应的所有执行事件
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners.Remove(eventName);
        }
    }
}
