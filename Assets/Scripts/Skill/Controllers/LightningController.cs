using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : SkillController
{
    private CircleCollider2D skillCollider2D;

    public float openColliderTime;                          //打开触发器的时间帧
    public float endAnimationTime;                          //动画结束的时间帧
    public bool isEvolution;

    private float time;

    protected override void Awake()
    {
        skillCollider2D = GetComponent<CircleCollider2D>();

        base.Awake();
    }

    private void OnEnable()
    {
        GetSkillData();
        skillCollider2D.enabled = false;
        time = Time.time;
    }

    private void Update()
    {
        if (Time.time - time >= (openColliderTime / 60))
        {
            skillCollider2D.enabled = true;
        }
        if (Time.time - time >= (endAnimationTime / 60))
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
