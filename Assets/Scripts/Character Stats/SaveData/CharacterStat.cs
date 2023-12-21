using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public PlayerData playerData;

    private void Awake()
    {
        playerData = new PlayerData();
    }
}
    
