using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FightCount : MonoBehaviour
{
    public TMP_Text kills;
    public TMP_Text golds;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        kills.text = gameManager.kills.ToString();
        golds.text = gameManager.gold.ToString();
    }
}
