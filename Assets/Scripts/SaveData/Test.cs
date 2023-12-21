using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private AttributeManager attributeManager;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
        
    }

    private void Start()
    {
        attributeManager.currentAttribute.maxHealth = 150;
        attributeManager.RestoreAttribute();
        Debug.Log(attributeManager.currentAttribute.maxHealth);
    }
}
