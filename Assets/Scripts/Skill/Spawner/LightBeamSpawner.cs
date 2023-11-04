using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamSpawner : SkillSpawner
{
    private float playerScale;
    private float lightBeamScale;
    protected override void Awake()
    {
        base.Awake();

        playerScale = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        lightBeamScale = skillData.skillAttackData.currentScale.x;
    }

    private void Start()
    {
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
            exitCoroutine = null;
        }
        exitCoroutine = StartCoroutine(CoolTimeDown(skillData));
    }

    private void Update()
    {
        if(PrepareSkill(skillData))
        {
            exitCoroutine = StartCoroutine(CoolTimeDown(skillData));

            skill = objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject, 
                                            gameObject, skillPoint.position, Quaternion.identity);

            ChangeSkillSize(skill);
        }
    }
}
