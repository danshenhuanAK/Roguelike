using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImpactEffect                     //攻击效果接口
{
    void Exeute(SkillDeployer deployer);
}

public interface ISkillSelector                    //范围选择算法
{
    Transform[] SelectTarget(CharacterStats data, Transform skillTF, LayerMask enemyMask);
}
