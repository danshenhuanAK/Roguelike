using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HaloSpanwer : SkillSpawner
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void LoadPre()
    {
        skillData.skillAsset.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                skillPre = handle.Result;

                objectPool.CreateObject(skillPre.name, skillPre, gameObject, Vector3.zero, Quaternion.identity);
            }
        };
        skillData.skillAsset.ReleaseAsset();
    }

    public override void UpdateSkillAttribute()
    {
        base.UpdateSkillAttribute();
    }
}
