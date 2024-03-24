using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberUI : MonoBehaviour
{ 
    public float changeSizeAnimationSpeed;
    public Vector2 changeSizeRange; 

    private void Update()
    {
        if(transform.localScale.x < changeSizeRange.y)
        {
            transform.localScale = transform.localScale * (1 + changeSizeAnimationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.localScale = new Vector3(changeSizeRange.x, changeSizeRange.x, 0);
    }
}
