using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private TMP_Text gradeText;                                  //����ȼ��ı�
    private Image experienceSlider;                              //������

    private UIPanelManager uIPanelManager;
    private FightProgressAttributeManager attributeManager;

    private PlayerController playerController;

    [System.Serializable]
    public class UpGradeRange
    {
        public int startUpGrade;
        public int endUpGrade;
        public int experienceCapIncrease;
    }

    public List<UpGradeRange> upGradeRanges;

    private void Awake()
    {
        uIPanelManager = UIPanelManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;

        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        Transform fightPanel = GameObject.FindGameObjectWithTag("Fight").transform.Find("Experience");
        gradeText = fightPanel.Find("Grade").GetComponent<TMP_Text>();
        experienceSlider = fightPanel.Find("ExperienceSlider").GetComponent<Image>();
    }

    public void UpdateSlider()
    {
        Transform fightPanel = GameObject.FindGameObjectWithTag("Fight").transform.Find("Experience");
        gradeText = fightPanel.Find("Grade").GetComponent<TMP_Text>();
        experienceSlider = fightPanel.Find("ExperienceSlider").GetComponent<Image>();

        if (attributeManager.playerData.experienceCap > 0)
        {
            experienceSlider.fillAmount = (float)attributeManager.playerData.experience / attributeManager.playerData.experienceCap;
        }

        gradeText.text = "�ȼ���" + attributeManager.playerData.level;
    }

    private int upGradeNum;
    [HideInInspector] public bool isPush;

    private void Update()
    {
        if(upGradeNum != 0 && !isPush)
        {
            uIPanelManager.PushPanel(UIPanelType.ChooseBuffPanel, UIPanelType.ChooseBuffPanelCanvas);
            isPush = true;
            upGradeNum--;
        }
    }

    public void IncreaseExperience(float amount)
    {
        attributeManager.playerData.experience += amount * attributeManager.playerData.experienceCquisitionSpeed;

        while (attributeManager.playerData.experience >= attributeManager.playerData.experienceCap)
        {
            int isAgainUpGrade = 1;

            if(attributeManager.gameFightData.MageHat > 0)
            {
                float againUpGrade = Random.Range(0f, 1f);

                if(againUpGrade >= attributeManager.gameFightData.MageHat)
                {
                    isAgainUpGrade = 2;
                }
            }

            for(int i = 0; i < isAgainUpGrade; i++)
            {
                UpGrade();
                upGradeNum++;

                if(attributeManager.gameFightData.BlueScroll > 0)
                {
                    attributeManager.SkillCoolDown((float)attributeManager.gameFightData.BlueScroll);
                }

                if(attributeManager.gameFightData.GreenScroll > 0)
                {
                    attributeManager.MaxHealth((float)attributeManager.gameFightData.GreenScroll);
                }

                if(attributeManager.gameFightData.PurpleScroll > 0)
                {
                    attributeManager.AttackPower((float)attributeManager.gameFightData.PurpleScroll);
                }
            }
        }

        //���ľ�����
        if(attributeManager.playerData.experienceCap > 0)
        {
            experienceSlider.fillAmount = (float)attributeManager.playerData.experience / attributeManager.playerData.experienceCap;
        }
    }

    /// <summary>
    /// ���������������������ȼ��ı����޸ģ�����������Ϊ��ǰ����ֵ��������һ���������辭��
    /// </summary>
    private void UpGrade()
    {
        attributeManager.playerData.level++;
        gradeText.text = "�ȼ���" + attributeManager.playerData.level;
        attributeManager.playerData.experience -= attributeManager.playerData.experienceCap;

        int experienceCapIncrease = 0;
        foreach (UpGradeRange range in upGradeRanges)
        {
            if (attributeManager.playerData.level >= range.startUpGrade && attributeManager.playerData.level < range.endUpGrade)
            {
                experienceCapIncrease = range.experienceCapIncrease;
                break;
            }
        }
        attributeManager.playerData.experienceCap += experienceCapIncrease;
    }
}
