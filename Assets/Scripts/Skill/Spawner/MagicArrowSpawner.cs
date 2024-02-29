using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowSpawner : SkillSpawner
{
    public float rotationAngle;

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
            GetRandomEnemy();                               //����������

            if (enemy == null)                               //û�е����ڹ�����Χ��������˴μ�������
            {
                return;
            }

            //������һ���˼��ܣ��������ɵĴ˼���Χ�Ƶ�һ�����ɵ�
            skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject,
                                                            gameObject, skillPoint.position, Quaternion.identity);

            skill.GetComponent<SkillController>().skillAttribute = skillData.skillAttribute[grade - 1];
            ChangeSkillSize(skill);

            Vector3 v = enemy.position - skill.transform.position;
            v.z = 0;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.right, v);
            skill.transform.rotation = rotation;

            for (int i = 1; i < skillData.skillAttribute[grade - 1].skillProjectileQuantity; i++)
            {
                GameObject rotationObject = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject
                                                , skill.transform.position, Quaternion.identity);

                rotationObject.GetComponent<SkillController>().skillAttribute = skillData.skillAttribute[grade - 1];
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

            skillAudio.Play();
        }
    }
}
