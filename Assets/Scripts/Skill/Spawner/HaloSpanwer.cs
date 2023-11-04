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
        skill = objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject,
                                         gameObject, skillPoint.position, Quaternion.identity);

        ChangeSkillSize(skill);
    }
}
