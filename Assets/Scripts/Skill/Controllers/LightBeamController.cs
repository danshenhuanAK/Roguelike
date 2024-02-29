using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : SkillController
{
    private Transform player;

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
    }

    private void Update()
    {
        if((Time.time - time) >= (closeSkillTime / 60))
        {
            gameObject.SetActive(false);
        }

        gameObject.transform.position = skillPoint.position;

        if (player.localScale.x * transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }
    }
}
