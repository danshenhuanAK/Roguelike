using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeController : SkillController
{
    private bool isPierce;
    private Vector3 startTransform;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        isPierce = skillAttribute.skillGrade >= 4 ? true : false;
        startTransform = gameObject.transform.position;
    }

    private void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * skillAttribute.launchMoveSpeed, Space.World);

        if (Vector3.Distance(startTransform, gameObject.transform.position) >= skillAttribute.attackRange)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<EnemyController>().enemyCurrentAttribute;
            attributeManager.SkillDamage(skillAttribute, monsterData);

            if (!isPierce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
