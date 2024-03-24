using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private EnemyDataController dataController;
    private FightProgressAttributeManager attributeManager;

    private int minute;

    private void Awake()
    {
        dataController = GetComponent<EnemyDataController>();
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void Start()
    {
        minute = attributeManager.gameFightData.minute;
    }

    [System.Serializable]
    public class AttributeLevel
    {
        public int startTime;
        public int endTime;

        public Vector2 maxHealthLevel;
        public Vector2 defenceLevel;
        public Vector2 moveSpeedLevel;
        public Vector2 attackDamageLevel;
    }

    [System.Serializable]
    public class AttributeLevels
    {
        public AttributeLevel[] attributeLevel;
    }

    public AttributeLevels attributeLevels;

    private void Update()
    {
        if(attributeManager.gameFightData.minute != minute)
        {
            minute = attributeManager.gameFightData.minute;
            StrengThenEnemy(minute);
        }
    }

    public void StrengThenEnemy(int minute)
    {
        foreach (AttributeLevel levelData in attributeLevels.attributeLevel)
        {
            if (minute >= levelData.startTime && minute <= levelData.endTime)
            {
                for(int i = 0; i < dataController.enemyCurrentDatas.enmeyDatas.Count; i++)
                {
                    dataController.enemyCurrentDatas.enmeyDatas[i].maxHealth += Random.Range(levelData.maxHealthLevel.x, levelData.maxHealthLevel.y);
                    dataController.enemyCurrentDatas.enmeyDatas[i].currentHealth = dataController.enemyCurrentDatas.enmeyDatas[i].maxHealth;
                    dataController.enemyCurrentDatas.enmeyDatas[i].defence += Random.Range(levelData.defenceLevel.x, levelData.defenceLevel.y);
                    dataController.enemyCurrentDatas.enmeyDatas[i].moveSpeed += Random.Range(levelData.moveSpeedLevel.x, levelData.moveSpeedLevel.y);
                    dataController.enemyCurrentDatas.enmeyDatas[i].attackDamage += Random.Range(levelData.attackDamageLevel.x, levelData.attackDamageLevel.y);
                }
                return;
            }
        }
    }
}
