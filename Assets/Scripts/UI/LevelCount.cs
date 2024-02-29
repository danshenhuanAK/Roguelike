using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCount : MonoBehaviour
{
    public TMP_Text difGrade;
    public TMP_Text golds;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        difGrade = transform.Find("Grade").GetChild(0).GetComponent<TMP_Text>();
        golds = transform.Find("Gold").GetChild(0).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        difGrade.text = gameManager.minute.ToString();
        golds.text = gameManager.gold.ToString();
    }
}
