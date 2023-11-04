using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterStats> haloSkillData;
    [SerializeField]
    private List<CharacterStats> launchSkillData;
    [SerializeField]
    private List<CharacterStats> explosionSkillData;

    private ObjectPool objectPool;

    public Transform haloSkillPoint;
    public Transform centerSkillPoint;

    private void Update()
    {
        for(int i = 0; i < haloSkillData.Count; i++)
        {
            if(PrepareSkill(haloSkillData[i]))
            {
                SkillCreate(haloSkillData[i], haloSkillPoint);
            }
        }

        for(int i = 0; i < launchSkillData.Count; i++)
        {
            if (PrepareSkill(launchSkillData[i]))
            {
                SkillCreate(launchSkillData[i], centerSkillPoint);
            }
        }

        for(int i = 0; i < explosionSkillData.Count; i++)
        {
            if (PrepareSkill(explosionSkillData[i]))
            {
                SkillCreate(explosionSkillData[i], centerSkillPoint);
            }
        }
    }

    private bool PrepareSkill(CharacterStats skillData)
    {
        if(skillData.skillAttackData.cdRemain <= 0)
        {
            StartCoroutine(CoolTimeDown(skillData));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SkillCreate(CharacterStats skillData, Transform skillPoint)
    {
        GameObject skillObject = objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject,
                                                            gameObject, skillPoint.position, Quaternion.identity);
        SkillDeployer deployer = skillObject.GetComponent<SkillDeployer>();
        deployer.SkillData = skillData;
        deployer.DeploySkill();
        StartCoroutine(CoolTimeDown(skillData));
    }

    private IEnumerator CoolTimeDown(CharacterStats skillData)
    {
        skillData.skillAttackData.cdRemain = skillData.skillAttackData.currentCoolDown;

        while(skillData.skillAttackData.cdRemain > 0)
        {
            yield return new WaitForSeconds(0.1f);

            skillData.skillAttackData.cdRemain -= Time.deltaTime;
        }
    }

    public void AddHaloSkillPrefab(GameObject newSkill)
    {
        haloSkillData.Add(newSkill.GetComponent<CharacterStats>());
    }

    public void AddlaunchSkillPrefab(GameObject newSkill)
    {
        launchSkillData.Add(newSkill.GetComponent<CharacterStats>());
    }

    public void AddExplosionSkillPrefab(GameObject newSkill)
    {
        explosionSkillData.Add(newSkill.GetComponent<CharacterStats>());
    }

    public void ClearHaloSkillPrefab(GameObject newSkill)
    {
        for (int i = 0; i < haloSkillData.Count; i++)
        {
            if (newSkill.name == haloSkillData[i].skillAttackData.skillObject.name)
            {
                haloSkillData.RemoveAt(i);
            }
        }
    }

    public void ClearlaunchSkillPrefab(GameObject newSkill)
    {
        for (int i = 0; i < launchSkillData.Count; i++)
        {
            if (newSkill.name == launchSkillData[i].skillAttackData.skillObject.name)
            {
                launchSkillData.RemoveAt(i);
            }
        }
    }

    public void ClearExplosionSkillPrefab(GameObject newSkill)
    {
        for (int i = 0; i < explosionSkillData.Count; i++)
        {
            if (newSkill.name == explosionSkillData[i].skillAttackData.skillObject.name)
            {
                explosionSkillData.RemoveAt(i);
            }
        }
    }
}
