using UnityEngine;

public class ElectricFieldSpawner : SkillSpawner
{
    public void CreateElectricField(Transform skillTransform)
    {
        skill = objectPool.CreateObject(skillData.skillAttackData.skillObject.name, skillData.skillAttackData.skillObject,
                                        gameObject, skillTransform.position, Quaternion.identity);

        ChangeSkillSize(skill);
    }
}
