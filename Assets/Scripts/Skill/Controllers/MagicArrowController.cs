using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowController : SkillController
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        GetSkillData();
        skillDuration = (float)skillData.duration;
    }

    private void Update()
    {
        transform.Translate((float)skillData.launchMoveSpeed * Time.deltaTime * transform.right, Space.World);

        skillDuration -= Time.deltaTime;

        if (skillDuration <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<EnemyController>().HitEnemy(skillData);
            gameObject.SetActive(false);
        }
    }
}
