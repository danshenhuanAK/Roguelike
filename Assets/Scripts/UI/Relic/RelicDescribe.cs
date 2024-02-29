using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RelicDescribe : MonoBehaviour
{
    public Color enterColor;
    public Color exitColor;

    public TMP_Text describeText;

    private void OnEnable()
    {
        
    }

    public void RelicEnter(GameObject relic, string describe)
    {
        relic.GetComponent<Image>().color = enterColor;

        describeText.transform.localPosition = new Vector3(relic.transform.localPosition.x + transform.localPosition.x,
            relic.transform.localPosition.y + transform.localPosition.y + 30, 0);
        describeText.text = describe;
        describeText.gameObject.SetActive(true);
    }

    public void RelicExit(GameObject relic)
    {
        relic.GetComponent<Image>().color = exitColor;
        describeText.gameObject.SetActive(false);
    }
}
