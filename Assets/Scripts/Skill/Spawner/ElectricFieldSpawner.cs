using UnityEngine;

public class ElectricFieldSpawner : SkillSpawner
{
    private void OnEnable()
    {
        EvolutionSkill();
    }

    public void CreateElectricField(Transform skillTransform)
    {
        skill = objectPool.CreateObject(skillData.skillObject.name, skillData.skillObject, gameObject, skillTransform.position, Quaternion.identity);

        skill.GetComponent<ElectricFieldController>().skillAttribute = basicSkill.skillAttribute[grade - 1];

        ChangeSkillSize(skill);
    }
}
