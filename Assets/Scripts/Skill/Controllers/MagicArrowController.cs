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
        transform.Translate(transform.right * Time.deltaTime * skillData.skillAttackData.launchMoveSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<CharacterStats>();
            skillData.SkillDamage(monsterData);
            gameObject.SetActive(false);
        }
    }
}
