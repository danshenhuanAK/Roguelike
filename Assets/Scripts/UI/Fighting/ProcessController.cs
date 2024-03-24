using UnityEngine;
using TMPro;

public class ProcessController : MonoBehaviour
{
    public GameObject degreeDifficulty;                         //�Ѷ�
    public TMP_Text completelyInsaneText;                       //���׷���ı�
    public TMP_Text timeText;

    [Header("���Ѷ����� x:���ʱ���� y:�Ѷȿ���")]
    public Vector2 easyRange;
    public Vector2 normalRange;
    public Vector2 difficultRange;
    public Vector2 madnessRange;
    public Vector2 completeInsaneRange;

    private FightProgressAttributeManager attributeManager;
    private DataManager dataManager;

    private int lastSecond;

    private float posX;

    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
        dataManager = DataManager.Instance;
    }

    private void Start()
    {
        timeText.text = string.Format("{0:d2}:{1:d2}", attributeManager.gameFightData.minute, attributeManager.gameFightData.second);
        
        if(dataManager.IsSave())
        {
            degreeDifficulty.transform.localPosition = new Vector2((float)attributeManager.gameFightData.DifficultyBoxPosX,
                                                                    degreeDifficulty.transform.localPosition.y);
        }
    }

    private void Update()
    {
        timeText.text = string.Format("{0:d2}:{1:d2}", attributeManager.gameFightData.minute, attributeManager.gameFightData.second);

        if(attributeManager.gameFightData.second - lastSecond == 1)
        {
            if (attributeManager.gameFightData.minute <= easyRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - easyRange.y / (easyRange.x * 60);
            }
            else if (attributeManager.gameFightData.minute <= normalRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - normalRange.y / ((normalRange.x - easyRange.x) * 60);
            }
            else if (attributeManager.gameFightData.minute <= difficultRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - difficultRange.y / ((difficultRange.x - normalRange.x) * 60);
            }
            else if(attributeManager.gameFightData.minute <= madnessRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - madnessRange.y / ((madnessRange.x - difficultRange.x) * 60);
            }
            else
            {
                posX = degreeDifficulty.transform.localPosition.x - completeInsaneRange.y / (completeInsaneRange.x * 60);

                if(attributeManager.gameFightData.minute % 5 == 0)
                {
                    completelyInsaneText.margin = new Vector4(0, 0, completelyInsaneText.margin.w - completeInsaneRange.y / 2, 0);
                    completelyInsaneText.text += "����������";
                }
            }

            degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);
            attributeManager.gameFightData.DifficultyBoxPosX = posX;
        }

        lastSecond = attributeManager.gameFightData.second;
    }
}
