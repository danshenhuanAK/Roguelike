using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HealthBarUI : MonoBehaviour
{
    private ObjectPool objectPool;

    public AssetReference healthUIPre;              //Ѫ��Ԥ������Դ
    public Transform barPoint;                      //Ѫ������ĵ�

    private Image healthSlider;                     //Ѫ������
    private GameObject healthBarPanel;              //Ѫ��UI���

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
        if(UIbar != null)               //Ѫ����һֱ����
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
