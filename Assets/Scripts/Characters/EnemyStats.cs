using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private List<EnemyCurrentAttributes> enemyCurrentAttributes;

    private void Awake()
    {
        enemyCurrentAttributes = GetComponent<EnemySpawner>().enemyCurrentAttributes;
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

    public void StrengThenEnemy(int minute)
    {
        for(int j = 0; j < enemyCurrentAttributes.Count; j++)
        {
            foreach(AttributeLevel levelData in attributeLevels.attributeLevel)
            {
                if(minute >= levelData.startTime && minute <= levelData.endTime)
                {
                    enemyCurrentAttributes[j].enemyCurrentAttributes.maxHealth += Random.Range(levelData.maxHealthLevel.x, levelData.maxHealthLevel.y);
                    enemyCurrentAttributes[j].enemyCurrentAttributes.defence += Random.Range(levelData.defenceLevel.x, levelData.defenceLevel.y);
                    enemyCurrentAttributes[j].enemyCurrentAttributes.moveSpeed += Random.Range(levelData.moveSpeedLevel.x, levelData.moveSpeedLevel.y);
                    enemyCurrentAttributes[j].enemyCurrentAttributes.attackDamage += Random.Range(levelData.attackDamageLevel.x, levelData.attackDamageLevel.y);
                    break;
                }
            }
            
        }
        
    }
}
