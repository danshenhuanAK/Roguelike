using UnityEngine;
using TMPro;

public class ProcessController : MonoBehaviour
{
    public GameObject degreeDifficulty;                         //难度
    public TMP_Text completelyInsaneText;                       //彻底疯狂文本
    public TMP_Text timeText;

    [Header("各难度区间 x:最大时间域 y:难度框宽度")]
    public Vector2 easyRange;
    public Vector2 normalRange;
    public Vector2 difficultRange;
    public Vector2 madnessRange;
    public Vector2 completeInsaneRange;

    private GameManager gameManager;

    private int lastSecond;

    private float posX;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        timeText.text = string.Format("{0:d2}:{1:d2}", 0, 0);
    }

    private void Update()
    {
        timeText.text = string.Format("{0:d2}:{1:d2}",gameManager.minute, gameManager.second);

        if(gameManager.second - lastSecond == 1)
        {
            if (gameManager.minute <= easyRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - easyRange.y / (easyRange.x * 60);
                degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);
            }
            else if (gameManager.minute <= normalRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - normalRange.y / ((normalRange.x - easyRange.x) * 60);
                degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);
            }
            else if (gameManager.minute <= difficultRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - difficultRange.y / ((difficultRange.x - normalRange.x) * 60);
                degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);
            }
            else if(gameManager.minute <= madnessRange.x)
            {
                posX = degreeDifficulty.transform.localPosition.x - madnessRange.y / ((madnessRange.x - difficultRange.x) * 60);
                degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);
            }
            else
            {
                posX = degreeDifficulty.transform.localPosition.x - completeInsaneRange.y / (completeInsaneRange.x * 60);
                degreeDifficulty.transform.localPosition = new Vector2(posX, degreeDifficulty.transform.localPosition.y);

                if(gameManager.minute % 5 == 0)
                {
                    completelyInsaneText.margin = new Vector4(0, 0, completelyInsaneText.margin.w - completeInsaneRange.y / 2, 0);
                    completelyInsaneText.text += "哈哈哈哈哈";
                }
            }
        }

        lastSecond = gameManager.second;
    }
}
