using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : SkillSpawner
{
    public GameObject electricFieldSpawner;

    private CircleCollider2D lightCollider;

    protected override void Awake()
    {
        lightCollider = basicSkill.skillObject.GetComponent<CircleCollider2D>();

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
            GetRandomEnemys();

            if (enemys == null)
            {
                return;
            }

            for (int i = 0; i < enemys.Count; i++)
            {
                skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject, enemys[i].position, Quaternion.identity);

                skill.GetComponent<CircleCollider2D>().radius = lightCollider.radius * skillData.skillAttribute[grade - 1].skillScale;
                skill.GetComponent<LightningController>().skillAttribute = basicSkill.skillAttribute[grade - 1];
            }

            enemys.Clear();
        }
    }

    public void DisElectricFieldSpawner()
    {
        if(isEvolution)
        {
            electricFieldSpawner.SetActive(true);
        }
    }
}
