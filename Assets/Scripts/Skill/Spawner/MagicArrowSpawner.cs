using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MagicArrowSpawner : SkillSpawner
{
    public float rotationAngle;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (gameManager.gameState == GameState.Fighting && PrepareSkill())
        {
            GetRandomEnemy();                               //获得随机敌人

            if (enemy == null)                               //没有敌人在攻击范围内则结束此次技能生成
            {
                return;
            }

            //先生成一个此技能，其他生成的此技能围绕第一个生成的
            skill = objectPool.CreateObject(skillPre.name, skillPre, gameObject, skillPoint.position, Quaternion.identity);
            ChangeSkillSize(skill);

            Vector3 v = enemy.position - skill.transform.position;
            v.z = 0;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, v);
            skill.transform.rotation = rotation;

            for (int i = 1; i < skillData.playerCurrentSkillData.skillProjectileQuantity; i++)
            {
                GameObject rotationObject = objectPool.CreateObject(skillPre.name, skillPre, gameObject, skill.transform.position, Quaternion.identity);
                ChangeSkillSize(rotationObject);
                rotationObject.transform.rotation = skill.transform.rotation;

                if (i % 2 != 0)
                {
                    rotationObject.transform.Rotate(new Vector3(0, 0, rotationAngle * (i / 3 + 1)));
                }
                else
                {
                    rotationObject.transform.Rotate(new Vector3(0, 0, rotationAngle * (-1 * (i / 3 + 1))));
                }
            }
            audioManager.PlaySound("MagciArrow");
        }
    }
}
