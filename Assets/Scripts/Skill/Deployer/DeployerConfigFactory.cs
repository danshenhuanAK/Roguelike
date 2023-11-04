using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DeployerConfigFactory
{
    public static ISkillSelector CreateSkillSelector(CharacterStats data)
    {
        string className = string.Format("MOBASkill.{0}Selector", data.skillAttackData.skillObject);
        return CreateObject<ISkillSelector>(className);
    }

    public static IImpactEffect CreateImpactEffect(CharacterStats data)
    {
        string className = string.Format("MOBASkill.{0}Impact", data.skillAttackData.skillObject);
        return CreateObject<IImpactEffect>(className);
    }

    private static T CreateObject<T>(string className) where T :class
    {
        Type type = Type.GetType(className);
        return Activator.CreateInstance(type) as T;
    }
}
