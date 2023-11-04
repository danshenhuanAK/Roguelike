using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionLightningController : SkillController
{ 
    private GameObject electricFieldSpawner;

    private CharacterStats MonsterData;
    private CircleCollider2D skillCollider2D;
    private AudioSource skillAudio;

    public float openColliderTime;                          //打开触发器的时间帧
    public float endAnimationTime;                          //动画结束的时间帧

    private float time;
    private bool isSpawner;

    protected override void Awake()
    {
        skillCollider2D = GetComponent<CircleCollider2D>();
        electricFieldSpawner = GameObject.Find("ElectricFieldSpawner");
        skillAudio = GetComponentInParent<AudioSource>();
        base.Awake();
    }

    private void OnEnable()
    {
        isSpawner = true;
        skillAudio.Play();
        skillCollider2D.enabled = false;
        time = Time.time;
    }

    private void Update()
    {
        if(Time.time - time >= (openColliderTime / 60) && isSpawner)
        {
            isSpawner = false;
            electricFieldSpawner.GetComponent<ElectricFieldSpawner>().CreateElectricField(gameObject.transform);
            skillCollider2D.enabled = true;
        }
        if(Time.time - time >= (endAnimationTime / 60))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            MonsterData = collision.GetComponent<CharacterStats>();
            skillData.SkillDamage(MonsterData);
        }
    }
}
