using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerBuff : MonoBehaviour
{
    protected AttributeManager attributeManager;

    protected GameObject chooseBuff;
    
    protected TMP_Text buffGrade;
    protected TMP_Text buffBonus;
    protected Image buffBack;

    [SerializeField]
    protected float[] buffValue;
    protected int rarityLevel;
    public int grade;
    protected Color[] rarityColor = { new Color(0.45f, 0.45f, 0.45f), new Color(0.8f, 0.3f, 0), 
                                      new Color(0.3f, 0.2f, 0.4f), new Color(0.7f, 0.6f, 0.2f)};

    protected virtual void Awake()
    {
        attributeManager = AttributeManager.Instance;
        chooseBuff = GameObject.FindGameObjectWithTag("Buff");

        buffGrade = transform.GetChild(2).GetComponent<TMP_Text>();
        buffBonus = transform.GetChild(3).GetComponent<TMP_Text>();
        buffBack = GetComponent<Image>();

        grade = 0;
    }

    protected virtual void OnEnable()
    {
        buffGrade.text = "µÈ¼¶£º" + (grade + 1);

        int rarity = Random.Range((int)attributeManager.currentAttribute.luck, 100);

        if (0 <= rarity && rarity < 70)
        {
            rarityLevel = 0;
        }
        else if (70 <= rarity && rarity < 85)
        {
            rarityLevel = 1;
        }
        else if (85 <= rarity && rarity < 95)
        {
            rarityLevel = 2;
        }
        else if (95 <= rarity && rarity < 100)
        {
            rarityLevel = 3;
        }
    }

    public void ClearBuff()
    {
        chooseBuff.GetComponent<ChooseBUFF>().ClearUI(gameObject.name);
    }
}
