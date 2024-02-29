using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private TMP_Text gradeText;                                  //����ȼ��ı�
    private Image experienceSlider;                              //������

    private UIPanelManager uIPanelManager;
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
        uIPanelManager = UIPanelManager.Instance;
        attributeManager = AttributeManager.Instance;

        Transform fightPanel = GameObject.FindGameObjectWithTag("Fight").transform.Find("Experience");
        gradeText = fightPanel.Find("Grade").GetComponent<TMP_Text>();
        experienceSlider = fightPanel.Find("ExperienceSlider").GetComponent<Image>();
    }

    private void Start()
    {
        attributeManager.currentAttribute.experience = 0;

        if (attributeManager.currentAttribute.experienceCap > 0)
        {
            experienceSlider.fillAmount = attributeManager.currentAttribute.experience / attributeManager.currentAttribute.experienceCap;
        }

        gradeText.text = "�ȼ���" + attributeManager.currentAttribute.level;
    }

    private int levelUpNum;
    public bool isPush;

    private void Update()
    {
        if(levelUpNum != 0 && !isPush)
        {
            uIPanelManager.PushPanel(UIPanelType.ChooseBuffPanel, UIPanelType.ChooseBuffPanelCanvas);
            levelUpNum--;
        }
    }

    public void IncreaseExperience(float amount)
    {
        attributeManager.currentAttribute.experience += amount;

        while (attributeManager.currentAttribute.experience >= attributeManager.currentAttribute.experienceCap)
        {
            LevelUp();
            levelUpNum++;
        }

        //���ľ�����
        if(attributeManager.currentAttribute.experienceCap > 0)
        {
            experienceSlider.fillAmount = attributeManager.currentAttribute.experience / attributeManager.currentAttribute.experienceCap;
        }
    }

    /// <summary>
    /// ���������������������ȼ��ı����޸ģ�����������Ϊ��ǰ����ֵ��������һ���������辭��
    /// </summary>
    private void LevelUp()
    {
        attributeManager.currentAttribute.level++;
        gradeText.text = "�ȼ���" + attributeManager.currentAttribute.level;
        attributeManager.currentAttribute.experience -= attributeManager.currentAttribute.experienceCap;

        int experienceCapIncrease = 0;
        foreach (LevelRange range in levelRanges)
        {
            if (attributeManager.currentAttribute.level >= range.startLevel && attributeManager.currentAttribute.level <= range.endLevel)
            {
                experienceCapIncrease = range.experienceCapIncrease;
                break;
            }
        }
        attributeManager.currentAttribute.experienceCap += experienceCapIncrease;
    }
}
