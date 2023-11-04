using System.Collections;
using UnityEngine;

public class ElectricFieldController : SkillController
{
    public Animator skillAnimator;
    public AudioSource skillAudio;
    private CircleCollider2D skillColllider2D;

    public float closeColliderTime;                         //关闭触发器的时间帧
    public float openColliderTime;                          //打开触发器的时间帧
    public float endAnimationTime;                          //动画结束的时间帧

    private float lastTime;

    protected override void Awake()
    {
        skillColllider2D = GetComponent<CircleCollider2D>();
        base.Awake();
    }

    private void OnEnable()
    {
        skillColllider2D.enabled = false;
        skillAnimator.speed = 0;
        lastTime = 0;

        StartCoroutine(SkillDuration());

        if(exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
        }
        exitCoroutine = StartCoroutine(DamageCoolDown());
    }

    private void Update()
    {      
        if ((Time.time - lastTime) >= (openColliderTime / 60) && (Time.time - lastTime) <= (closeColliderTime / 60))
        {
            IsAttack();
        }

        if((Time.time - lastTime) >= (endAnimationTime / 60))
        {
            StopAnimation();
        }
    }

    private void StopAnimation()                             //事件帧:暂停动画，关闭触发器
    {
        skillAnimator.speed = 0;
        skillColllider2D.enabled = false;
    }

    private void IsAttack()                                  //事件帧:打开触发器，发出声音
    {
        skillColllider2D.enabled = true;
        skillAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            //Debug.Log(1);
            //Debug.Log(GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void OpenDamageCoolDown()
    {
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
        }
        exitCoroutine = StartCoroutine(DamageCoolDown());
    }

    public IEnumerator DamageCoolDown()                     //这个技能动画间隔的协程
    {
        yield return new WaitForSeconds(skillData.skillAttackData.currentCoolDown);

        skillAnimator.speed = 1;
        lastTime = Time.time;
        skillColllider2D.enabled = false;
        
        StopCoroutine(exitCoroutine);
        OpenDamageCoolDown();
    }
}
