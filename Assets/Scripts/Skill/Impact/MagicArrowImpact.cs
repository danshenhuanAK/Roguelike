using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicArrowImpact: IImpactEffect
{
    private ObjectPool objectPool;

    private CharacterStats skillData;
    private Transform maginArrow;

    public void Exeute(SkillDeployer deployer)
    {
        objectPool = ObjectPool.Instance;
        maginArrow = skillData.GetComponent<GameObject>().transform;
        skillData = deployer.SkillData;
        CreatMagicArrow(deployer);
        deployer.StartCoroutine(ContinuousEffect(deployer));
    }

    private void CreatMagicArrow(SkillDeployer deployer)
    {
        Transform creatTransform = maginArrow;

        float rotationAngle = 40 / (int)(skillData.skillAttackData.currentProjectileQuantity / 2);

        for(int i = 1; i < skillData.skillAttackData.currentProjectileQuantity; i++)
        {
            if(i % 2 != 0)
            {
                creatTransform.RotateAround(deployer.skillPoint.position, Vector3.right, rotationAngle * (i / 2 + 1));
            }
            else
            {
                creatTransform.RotateAround(deployer.skillPoint.position, Vector3.right, rotationAngle * (-1 * (i / 2 + 1)));
            }

            objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject, deployer.createParent
                                        , creatTransform.position, Quaternion.identity);
            creatTransform = maginArrow;
        }
    }

    IEnumerator ContinuousEffect(SkillDeployer deployer)
    {
        //float time = skillData.skillAttackData.currentDamageRemain;

        //do
        //{
            yield return new WaitForSeconds(skillData.skillAttackData.currentDuration);
        //    skillData.skillAttackData.isDamage =  true;
        //} while (true);
    }
}
