using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowController : SkillController
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * skillAttribute.launchMoveSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<EnemyController>().enemyCurrentAttribute;
            attributeManager.SkillDamage(skillAttribute, monsterData);
            gameObject.SetActive(false);
        }
    }
}
