using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloSpanwer : SkillSpawner
{
    protected override void Awake()
    {
        base.Awake();
    }
    
    private void OnEnable()
    {
        skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject, skillPoint.position, Quaternion.identity);

        ChangeSkillSize(skill);
    }
}
