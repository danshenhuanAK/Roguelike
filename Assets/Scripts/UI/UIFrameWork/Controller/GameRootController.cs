using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private UIPanelManager uiPanelManager;
    private ObjectPool objectPool;

    public GameObject skillSpawner;
    public GameObject skillSpawnerParent;
    private void Awake()
    {
        uiPanelManager = UIPanelManager.Instance;
        objectPool = ObjectPool.Instance;

        uiPanelManager.PushPanel(UIPanelType.LevelPanel, UIPanelType.LevelPanelCanvas);

        GameObject skillSpawnerObject = objectPool.CreateObject(skillSpawner.name, skillSpawner, skillSpawnerParent, skillSpawner.transform.position, Quaternion.identity);
        skillSpawnerObject.GetComponent<SkillSpawner>().grade++;
    }
}
