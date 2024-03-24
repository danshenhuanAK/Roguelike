using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class WindBladeSpawner : SkillSpawner
{
    private Transform playerScale;

    public AssetReference[] windBlades;
    private List<GameObject> windBladePres = new();

    protected override void Awake()
    {
        base.Awake();

        playerScale = skillPoint.parent.transform;
    }

    public override void LoadPre()
    {
        for(int i = 0; i < windBlades.Length; i++)
        {
            windBlades[i].LoadAssetAsync<GameObject>().Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    windBladePres.Add(handle.Result);
                }
            };
            windBlades[i].ReleaseAsset();
        }
    }

    private void Update()
    {
        if (gameManager.gameState == GameState.Fighting && PrepareSkill())
        {
            if (skillData.playerCurrentSkillData.grade > 8)
            {
                for (int i = 0; i < 4; i++)
                {
                    skill = objectPool.CreateObject(windBladePres[2].name, windBladePres[2], gameObject, skillPoint.position, Quaternion.identity);
                    skill.transform.Rotate(new Vector3(0, 0, 45 + (90 * i)));
                    ChangeSkillSize(skill);
                }
                audioManager.PlaySound("WindBlade");
            }
            else
            {
                if (skillData.playerCurrentSkillData.grade > 4)
                {
                    skill = objectPool.CreateObject(windBladePres[1].name, windBladePres[1], gameObject, skillPoint.position, Quaternion.identity);
                }
                else
                {
                    skill = objectPool.CreateObject(windBladePres[0].name, windBladePres[0], gameObject, skillPoint.position, Quaternion.identity);
                }

                ChangeSkillSize(skill);
                if (playerScale.localScale.x * skill.transform.localScale.x < 0)
                {
                    skill.transform.Rotate(new Vector3(0, 0, skill.transform.rotation.z + 180));
                }
                audioManager.PlaySound("WindBlade");
            }
        }
    }
}
