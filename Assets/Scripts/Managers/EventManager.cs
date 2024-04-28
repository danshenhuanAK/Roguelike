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
        // �������eventName���ID
        if (eventListeners.ContainsKey(eventName))
        {
            //��ȡeventName����Ӧ������ִ���¼�
            var handlerDic = eventListeners[eventName];
            if (handlerDic != null && handlerDic.Count > 0)
            {
                var dic = new Dictionary<int, EventDelegate>(handlerDic);
                //ͨ����eventName����Ӧ������ִ�н��б�����Ȼ������
                foreach (var f in dic.Values)
                {
                    try
                    {
                        //ִ�����е�ί���¼�����EventDelegate(object[] args)
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
        //����¼��ֵ��а���eventName�еļ������Ƴ������¼���Ӧ������ִ���¼�
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners.Remove(eventName);
        }
    }
}
