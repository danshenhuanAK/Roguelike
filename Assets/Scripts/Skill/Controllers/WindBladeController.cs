using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeController : SkillController
{
    private bool isPierce;                                  //4级之后可以穿刺敌人
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        GetSkillData();
        isPierce = skillData.grade >= 4;
        skillDuration = (float)skillData.duration;
    }

    private void Update()
    {
        transform.Translate((float)skillData.launchMoveSpeed * Time.deltaTime * transform.right, Space.World);

        skillDuration -= Time.deltaTime;

        if(skillDuration <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<EnemyController>().HitEnemy(skillData);

            if (!isPierce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
