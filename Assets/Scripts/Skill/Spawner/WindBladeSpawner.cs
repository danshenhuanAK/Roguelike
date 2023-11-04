using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBladeSpawner : SkillSpawner
{
    private Transform playerScale;
    private float windBladeScale;

    public GameObject[] windBlade;
    protected override void Awake()
    {
        base.Awake();

        playerScale = GameObject.FindGameObjectWithTag("Player").transform;
        windBladeScale = skillData.skillAttackData.currentScale.x;
    }

    private void Start()
    {
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
            exitCoroutine = null;
        }
        exitCoroutine = StartCoroutine(CoolTimeDown(skillData));
    }

    private void Update()
    {
        if (PrepareSkill(skillData))
        {
            exitCoroutine = StartCoroutine(CoolTimeDown(skillData));

            if(skillData.skillAttackData.currentGrade == 8)
            {
                for(int i = 0; i < 4; i++)
                {
                    skill = objectPool.CreateObject(windBlade[2].name, windBlade[2], gameObject, skillPoint.position, Quaternion.identity);

                    skill.transform.Rotate(new Vector3(0, 0, 45 + (90 * i)));
                    ChangeSkillSize(skill);
                }
            }
            else
            {
                if(skillData.skillAttackData.currentGrade >= 4)
                {
                    skill = objectPool.CreateObject(windBlade[1].name, windBlade[1], gameObject, skillPoint.position, Quaternion.identity);
                }
                else
                {
                    skill = objectPool.CreateObject(windBlade[0].name, windBlade[0], gameObject, skillPoint.position, Quaternion.identity);
                }

                if (playerScale.localScale.x < 0)
                {
                    skill.transform.localScale = new Vector3(-1 * windBladeScale, skillData.skillAttackData.currentScale.y, 0);
                }
                else
                {
                    skill.transform.localScale = new Vector3(windBladeScale, skillData.skillAttackData.currentScale.y, 0);
                }

                ChangeSkillSize(skill);
            }         

            skillAudio.Play();
        }
    }
}
