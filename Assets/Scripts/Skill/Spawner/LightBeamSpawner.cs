using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamSpawner : SkillSpawner
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
            skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject, skillPoint.position, Quaternion.identity);

            ChangeSkillSize(skill);
        }
    }
}
