using System.Collections.Generic;

public interface ISkillSaveable
{
    void RegisterSkillData() => DataManager.Instance.RegisterSkillData(this);

    void GetSkillData(PlayerSkillDataList skillData);
    void LoadSkillData(PlayerSkillDataList skillData);
}
