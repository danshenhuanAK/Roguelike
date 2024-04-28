using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloController : SkillController
{
    private float range;
    public float startSize;

    protected override void Awake()
    {
        base.Awake();
        
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
            isAttack = false;
        }
        else
        {
            damageCoolDown = (float)skillData.damageCoolDown;
            isAttack = true;
        }
  
        gameObject.transform.position = skillPoint.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isAttack)
        {
            return;
        }

        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<EnemyController>().HitEnemy(skillData);
        }
    }
}
