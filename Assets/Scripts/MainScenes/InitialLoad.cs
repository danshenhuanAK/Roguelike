using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitialLoad : MonoBehaviour
{
    public AssetReference mainScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(mainScene).Completed += (handle) =>
        {
            if(handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed)
            {
                Debug.Log(1);
            }
        };
    }
}
