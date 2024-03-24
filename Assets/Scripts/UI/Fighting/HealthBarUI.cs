using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HealthBarUI : MonoBehaviour
{
    private ObjectPool objectPool;

    public AssetReference healthUIPre;              //血条预制体资源
    public Transform barPoint;                      //血条跟随的点

    private Image healthSlider;                     //血条滑动
    private GameObject healthBarPanel;              //血条UI面板

    private GameObject UIbar;

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        healthBarPanel = GameObject.Find("HealthBarPanel");
    }
    public void CreateHealthBar()
    {
        healthUIPre.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                UIbar = objectPool.CreateObject(handle.Result.name, handle.Result, healthBarPanel, barPoint.position, Quaternion.identity);
                healthSlider = UIbar.transform.GetChild(0).GetComponent<Image>();
                healthSlider.fillAmount = 1f;
            }
        };
        healthUIPre.ReleaseAsset();
    }

    public void UpdateHealthBar(double currentHealth, double maxHealth)
    {
        if(UIbar)
        {
            UIbar.SetActive(true);
        }

        if(maxHealth == 0)
        {
            return;
        }

        float sliderPercent = (float)currentHealth / (float)maxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    private void LateUpdate()
    {
        if(UIbar != null)               //血量条一直跟随
        {
            UIbar.transform.position = barPoint.position;
        }
    }

    public void CloseUIbar()
    {
        UIbar.SetActive(false);
        UIbar = null;
    }
}
