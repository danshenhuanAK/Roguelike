using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public float expValue;

    [HideInInspector]
    public bool isPicked;                              //�Ƿ�ʰȡ
    public float moveSpeed;

    public float timeChecks;
    private float checkCounter;

    private GameManager gameManager;
    private AttributeManager attributeManager;
    private PlayerController playerController;
    private PlayerStats playerStats;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        attributeManager = AttributeManager.Instance;
    }

    private void Start()
    {
        playerController = gameManager.player.GetComponent<PlayerController>();
        playerStats = gameManager.player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if(isPicked)
        {
            transform.position = Vector3.MoveTowards(transform.position, gameManager.player.transform.position, moveSpeed * Time.deltaTime);

            //���鱦ʯ�����պ����Ӿ��飬����������
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

                if(Vector3.Distance(transform.position, gameManager.player.transform.position) < attributeManager.currentAttribute.magnet)
                {
                    isPicked = true;
                    moveSpeed += attributeManager.currentAttribute.moveSpeed;
                }
            }
        }
    }
}
