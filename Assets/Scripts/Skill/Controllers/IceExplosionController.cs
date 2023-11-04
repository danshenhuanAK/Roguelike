using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosionController : SkillController
{
    public AudioSource skillAudio;

    public float closeSkillTime;
    private float time;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        time = Time.time;
        //skillAudio.Play();
    }

    private void Update()
    {
        if((Time.time - time) >= (closeSkillTime / 60))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<CharacterStats>();
            skillData.SkillDamage(monsterData);
        }
    }
}
