using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : SkillController
{
    private Transform player;

    private Vector2 lightBeamScale;

    private float closeSkillTime;
    private float time;

    protected override void Awake()
    {
        skillPoint = GameObject.FindGameObjectWithTag("Center").transform;
        player = skillPoint.parent.transform;
        base.Awake();
    }

    private void OnEnable()
    {
        time = Time.time;
        lightBeamScale = skillData.skillAttackData.currentScale;
    }

    private void Update()
    {
        if((Time.time - time) >= (closeSkillTime / 60))
        {
            gameObject.SetActive(false);
        }

        gameObject.transform.position = skillPoint.position;

        if (player.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1 * lightBeamScale.x, lightBeamScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(lightBeamScale.x, lightBeamScale.y, transform.localScale.z);
        }
    }
}
