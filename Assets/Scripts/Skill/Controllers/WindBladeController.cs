using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeController : SkillController
{
    private bool isPierce;
    private Transform startTransform;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        isPierce = skillData.skillAttackData.baseGrade >= 4 ? true : false;
        startTransform = gameObject.transform;
    }

    private void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * skillData.skillAttackData.launchMoveSpeed, Space.World);

        if (Vector3.Distance(startTransform.position, gameObject.transform.position) >= skillData.skillAttackData.attackRange)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<CharacterStats>();
            skillData.SkillDamage(monsterData);

            if(!isPierce)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
