using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionController : SkillController
{
    public float closeSkillTime;
    private float time;

    private new CapsuleCollider2D collider;

    protected override void Awake()
    {
        collider = GetComponent<CapsuleCollider2D>();
        base.Awake();
    }

    private void OnEnable()
    {
        GetSkillData();
        time = Time.time;
        collider.enabled = true;
    }

    private void Update()
    {
        if(Time.time - time >= 0.1f)
        {
            collider.enabled = false;
        }

        if((Time.time - time) >= (closeSkillTime / 60))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<EnemyController>().HitEnemy(skillData);
        }
    }
}
