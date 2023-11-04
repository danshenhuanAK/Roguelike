using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : SkillSpawner
{
    protected override void Awake()
    {
        base.Awake();
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
        if (PrepareSkill(skillData))
        {
            GetRandomEnemys();

            if (enemys == null)
            {
                return;
            }

            exitCoroutine = StartCoroutine(CoolTimeDown(skillData));

            for(int i = 0; i < enemys.Count; i++)
            {
                skill = objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject, 
                    gameObject, enemys[i].position, Quaternion.identity);

                ChangeSkillSize(skill);
            }

            enemys.Clear();
        }
    }
}