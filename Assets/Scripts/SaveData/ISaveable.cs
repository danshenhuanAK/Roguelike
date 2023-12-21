public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    PlayerData GeneratePlayerData();

    void RestorePlayerData(PlayerData playerData);
}
