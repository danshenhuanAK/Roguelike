using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public float expValue;

    [HideInInspector]
    public bool isPicked;                                //是否被拾取
    public double moveSpeed;
    public double magnet;                                //拾取范围
    private double magnetBuff;

    public float timeChecks;                             //每隔timechecks时间检测是否可被拾取
    private float checkCounter;

    private GameManager gameManager;
    private FightProgressAttributeManager attributeManager;
    private PlayerStats playerStats;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void OnEnable()
    {
        if(magnetBuff != attributeManager.playerData.magnet)
        {
            magnetBuff = attributeManager.playerData.magnet;
            magnet = magnetBuff;
        }
    }

    private void Start()
    {
        playerStats = gameManager.player.GetComponent<PlayerStats>();
        moveSpeed += attributeManager.playerData.moveSpeed;
    }

    private void Update()
    {
        if(isPicked)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameManager.player.transform.position, (float)moveSpeed * Time.deltaTime);

            //经验宝石被吸收后增加经验，经验条增加
            if (Vector3.Distance(transform.position, gameManager.player.transform.position) <= 0)        
            {
                playerStats.IncreaseExperience(expValue);
                isPicked = false;
                gameObject.SetActive(false);
                transform.parent.GetComponent<ExperienceGemSpawner>().generatedGem.Remove(gameObject);
            }
        }
        else
        {
            checkCounter -= Time.deltaTime;

            if(checkCounter <= 0)
            {
                checkCounter = timeChecks;

                if(Vector3.Distance(transform.position, gameManager.player.transform.position) < magnet * (1 + attributeManager.playerData.magnet))
                {
                    isPicked = true;
                }
            }
        }
    }
}
