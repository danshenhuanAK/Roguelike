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
            GetRandomEnemy();                               //����������

            if (enemy == null)                               //û�е����ڹ�����Χ��������˴μ�������
            {
                return;
            }

            //������һ���˼��ܣ��������ɵĴ˼���Χ�Ƶ�һ�����ɵ�
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
