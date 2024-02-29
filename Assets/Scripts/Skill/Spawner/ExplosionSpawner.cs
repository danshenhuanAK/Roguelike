using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : SkillSpawner
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (PrepareSkill())
        {
            GetRandomEnemys();

            if (enemys == null)
            {
                return;
            }

            for(int i = 0; i < enemys.Count; i++)
            {
                skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject, enemys[i].position, Quaternion.identity);

                ChangeSkillSize(skill);
            }

            enemys.Clear();
        }
    }
}