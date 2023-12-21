using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Image experienceSlider;                      //经验条滑动

    private UIManager uiManager;
    private AttributeManager attributeManager;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    private void Awake()
    {
        uiManager = UIManager.Instance;
        attributeManager = AttributeManager.Instance;
    }

    private void Start()
    {
        attributeManager.currentAttribute.experience = 0;
        experienceSlider.fillAmount = attributeManager.currentAttribute.experience / attributeManager.currentAttribute.experienceCap;
    }

    public void IncreaseExperience(int amount)
    {
        attributeManager.currentAttribute.experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(attributeManager.currentAttribute.experience >= attributeManager.currentAttribute.experienceCap)
        {
            attributeManager.currentAttribute.level++;
            attributeManager.currentAttribute.experience -= attributeManager.currentAttribute.experienceCap;

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if(attributeManager.currentAttribute.level >= range.startLevel && attributeManager.currentAttribute.level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            attributeManager.currentAttribute.experienceCap += experienceCapIncrease;
        }
    }
}
