using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpurtFireController : SkillController
{
    private Transform player;
    private Animator skillAnimator;

    private bool isEndAnimation;
    private float endOpenTime;                          //结束动画开始的时间
    public float endDisTime;                            //结束动画结束的时间

    protected override void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        skillAnimator = GetComponent<Animator>();
        base.Awake();
    }

    private void OnEnable()
    {
        isEndAnimation = false;
        exitCoroutine = StartCoroutine(SkillDuration());
    }

    private void Update()
    {
        gameObject.transform.position = player.position;

        if (player.localScale.x * transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }

        if(isEndAnimation)
        {
            if((Time.time - endOpenTime) >= (endDisTime / 60))
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected override IEnumerator SkillDuration()       
    {
        yield return new WaitForSeconds(skillAttribute.duration);

        StopAllCoroutines();
        skillAnimator.SetBool("isEnd", true);
        isEndAnimation = true;
        endOpenTime = Time.time;
    }
}
