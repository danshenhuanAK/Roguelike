using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class RelicInterface : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private RelicDescribe relicDescribe;

    public string describe;
    protected virtual void Awake()
    {
        eventTrigger = gameObject.GetComponent<EventTrigger>();
        relicDescribe = transform.parent.GetComponent<RelicDescribe>();

        AddPointerEnterEvent();
        AddPointerExitEvent();
    }

    private void AddPointerEnterEvent()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(PointerEnter);
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }

    private void AddPointerExitEvent()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(PointerExit);
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }

    private void PointerEnter(BaseEventData baseEventData)
    {
        relicDescribe.RelicEnter(gameObject, describe);
    }

    private void PointerExit(BaseEventData baseEventData)
    {
        relicDescribe.RelicExit(gameObject);
    }

    public virtual void AtGetStart()                                //遗物生成时
    {

    }

    public virtual void AtStartFight()                              //战斗开始时
    {

    }

    public virtual void AtInFight()                                 //战斗进行时
    {

    }

    public virtual void AtEndFight()                                //战斗结束时
    {

    }

    public virtual void AtPlayerUpgrade()                           //角色升级时
    {

    }
}
