using UnityEditor;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class Test : MonoBehaviour
{
    public Material material;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().material = material;
    }

    private void Update()
    {
        material.GetFloat("AblationValue");
    }
}
