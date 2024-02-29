using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RandomEvents : MonoBehaviour
{
    public List<GameObject> eventTemplates = new List<GameObject>();
    [SerializeField]
    private List<GameObject> events = new List<GameObject>();

    private AttributeManager attributeManager;
    private UIPanelManager uIPanelManager;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
        uIPanelManager = UIPanelManager.Instance;

        for(int i = 0; i < eventTemplates.Count; i++)
        {
            events.Add(eventTemplates[i]);
        }
    }

    private void OnEnable()
    {
        uIPanelManager.displayPanel = null;

        int eventRandom = Random.Range(0, events.Count);

        GameObject triggerEvent = events[eventRandom];

        triggerEvent.SetActive(true);
    }

    private void OnDisable()
    {
        uIPanelManager.displayPanel = UIPanelType.LevelPanel;
    }

    public void AddEvent(GameObject addEvent)
    {
        events.Add(addEvent);
    }

    public void DeleteEvent(GameObject deleteEvent)
    {
        events.Remove(deleteEvent);
    }
}
