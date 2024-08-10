using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloController : SkillController
{
    private float range;
    public float startSize;

    private CircleCollider2D haloCollider;

    protected override void Awake()
    {
        base.Awake();

        haloCollider = GetComponent<CircleCollider2D>();
        skillPoint = GameObject.FindGameObjectWithTag("Sole").transform;
    }

    private void OnEnable()
    {
        GetSkillData();
        damageCoolDown = (float)skillData.damageCoolDown;
        range = (float)skillData.attackRange;
    }

    private void Update()
    {
        GetSkillData();
        float nowRange = (float)skillData.attackRange;

        if(nowRange != range)
        {
            transform.localScale = new Vector3(startSize * (float)skillData.attackRange, startSize * (float)skillData.attackRange);
            range = nowRange;
        }

        if (damageCoolDown > 0)
        {
            damageCoolDown -= Time.deltaTime;
        }
        else
        {
            damageCoolDown = (float)skillData.damageCoolDown;
        }
  
        gameObject.transform.position = skillPoint.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && damageCoolDown <= 0)
        {
            collision.GetComponent<EnemyController>().HitEnemy(skillData);
        }
    }
}
