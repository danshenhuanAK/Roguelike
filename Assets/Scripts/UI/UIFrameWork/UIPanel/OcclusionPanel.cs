using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OcclusionPanel : MonoBehaviour
{
    private void Update()
    {
        if(gameObject.GetComponent<RawImage>().color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
