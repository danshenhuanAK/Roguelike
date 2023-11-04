using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImpactEffect                     //����Ч���ӿ�
{
    void Exeute(SkillDeployer deployer);
}

public interface ISkillSelector                    //��Χѡ���㷨
{
    Transform[] SelectTarget(CharacterStats data, Transform skillTF, LayerMask enemyMask);
}
