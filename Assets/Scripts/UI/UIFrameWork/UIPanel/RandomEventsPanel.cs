using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventsPanel : BasePanel
{
    public List<GameObject> eventTemplates = new();
    [SerializeField]
    private List<GameObject> events = new();

    protected override void Awake()
    {
        uiPanelName = UIPanelType.RandomEventsPanel;

        for (int i = 0; i < eventTemplates.Count; i++)
        {
            events.Add(eventTemplates[i]);
        }

        base.Awake();
    }

    public override void OnEnter()
    {
        Time.timeScale = 0;

        GetRandomEvent();
        uiPanelManager.displayPanel = uiPanelName;

        base.OnEnter();
    }

    public override void OnExit()
    {
        dataManager.currentFloor++;
        dataManager.SaveGameData();
        base.OnExit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    private void GetRandomEvent()
    {
        int eventRandom = Random.Range(0, events.Count);

        GameObject triggerEvent = events[eventRandom];

        triggerEvent.SetActive(true);
    }

    public void AddEvent(GameObject addEvent)
    {
        events.Add(addEvent);
    }

    public void DeleteEvent(GameObject deleteEvent)
    {
        events.Remove(deleteEvent);
    }

    public void GetRelic(int count)
    {
        GameObject.FindGameObjectWithTag("Relic").GetComponent<DisplayRelicUI>().GetRandomRelic(count);
    }

    public void ClosePanel()
    {
        uiPanelManager.PopPanel();
    }
}
