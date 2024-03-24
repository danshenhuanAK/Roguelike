using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    [HideInInspector]
    public bool isPicked;                                //是否被拾取
    public double moveSpeed;
    public double magnet;                                //拾取范围

    public float timeChecks;                             //每隔timechecks时间检测是否可被拾取
    private float checkCounter;

    private GameManager gameManager;
    private FightProgressAttributeManager attributeManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void Start()
    {
        moveSpeed += attributeManager.playerData.moveSpeed;
    }

    private void Update()
    {
        if (isPicked)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameManager.player.transform.position, (float)moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, gameManager.player.transform.position) <= 0)
            {
                attributeManager.gameFightData.gold++;
                isPicked = false;
                gameObject.SetActive(false);
                transform.parent.GetComponent<GoldSpawner>().generatedGold.Remove(gameObject);
            }
        }
        else
        {
            checkCounter -= Time.deltaTime;

            if (checkCounter <= 0)
            {
                checkCounter = timeChecks;

                if (Vector3.Distance(transform.position, gameManager.player.transform.position) < magnet * (1 + attributeManager.playerData.magnet))
                {
                    isPicked = true;
                }
            }
        }
    }
}
