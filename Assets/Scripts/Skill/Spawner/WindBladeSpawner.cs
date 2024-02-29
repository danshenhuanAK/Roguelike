using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeSpawner : SkillSpawner
{
    private Transform playerScale;

    public GameObject[] windBlade;
    protected override void Awake()
    {
        base.Awake();

        playerScale = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (PrepareSkill())
        {
            if (skillData.skillAttribute[grade - 1].skillGrade == 8)
            {
                for (int i = 0; i < 4; i++)
                {
                    skill = objectPool.CreateObject(windBlade[2].name, windBlade[2], gameObject, skillPoint.position, Quaternion.identity);

                    skill.GetComponent<WindBladeController>().skillAttribute = basicSkill.skillAttribute[grade - 1];
                    skill.transform.Rotate(new Vector3(0, 0, 45 + (90 * i)));
                    ChangeSkillSize(skill);
                }
            }
            else
            {
                if (skillData.skillAttribute[grade - 1].skillGrade >= 4)
                {
                    skill = objectPool.CreateObject(windBlade[1].name, windBlade[1], gameObject, skillPoint.position, Quaternion.identity);
                    skill.GetComponent<WindBladeController>().skillAttribute = basicSkill.skillAttribute[grade - 1];
                }
                else
                {
                    skill = objectPool.CreateObject(windBlade[0].name, windBlade[0], gameObject, skillPoint.position, Quaternion.identity);
                    skill.GetComponent<WindBladeController>().skillAttribute = basicSkill.skillAttribute[grade - 1];
                }

                if (playerScale.localScale.x < 0)
                {
                    skill.transform.localScale = new Vector3(-1 * skillSize.x, skillSize.y, 0);
                }
                else
                {
                    skill.transform.localScale = new Vector3(skillSize.x, skillSize.y, 0);
                }

                ChangeSkillSize(skill);
            }

            skillAudio.Play();
        }
    }
}
