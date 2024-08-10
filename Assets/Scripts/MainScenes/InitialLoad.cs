using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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

   /* float totalSize;

    public IEnumerator CheckContentUptade(List<Object> keys)
    {
        for(int i = 0; i < keys.Count; i++)
        {
            AsyncOperationHandle<long> downLoadSize = Addressables.GetDownloadSizeAsync(keys[i]);

            yield return downLoadSize;

            if(downLoadSize.Result <= 0)
            {
                Debug.Log("没有更新的资源标签：" + keys[i]);
                keys.Remove(keys[i]);
            }
            else
            {
                totalSize += downLoadSize.Result / Mathf.Pow(1024, 2);
            }
        }

        if(keys.Count <= 0)
        {
            yield return null;
        }


    }*/
}
