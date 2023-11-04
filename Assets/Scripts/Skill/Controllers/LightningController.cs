using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : SkillController
{
    private CircleCollider2D skillCollider2D;
    private AudioSource skillAudio;

    public float openColliderTime;                          //打开触发器的时间帧
    public float endAnimationTime;                          //动画结束的时间帧

    private float time;

    protected override void Awake()
    {
        skillCollider2D = GetComponent<CircleCollider2D>();
        skillAudio = GetComponentInParent<AudioSource>();
        base.Awake();
    }

    private void OnEnable()
    {
        skillCollider2D.enabled = false;
        skillAudio.Play();
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
        if (collision.tag == "Monster")
        {
            
            monsterData = collision.GetComponent<CharacterStats>();
            skillData.SkillDamage(monsterData);
        }
    }
}
