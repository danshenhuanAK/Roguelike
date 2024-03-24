using System.Collections.Generic;

public interface ISkillSaveable
{
    void RegisterSkillData() => DataManager.Instance.RegisterSkillData(this);
    void UnRegisterSkillData() => DataManager.Instance.UnRegisterSkillData();
    void GetSkillData(PlayerSkillDataList skillData);
    void LoadSkillData(PlayerSkillDataList skillData);
}
