using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloController : SkillController
{
    protected override void Awake()
    {
        skillPoint = GameObject.FindGameObjectWithTag("Sole").transform;
        base.Awake();
    }

    private void Update()
    {
        gameObject.transform.position = skillPoint.position;
    }
}
