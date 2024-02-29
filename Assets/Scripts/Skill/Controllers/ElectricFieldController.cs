using System.Collections;
using UnityEngine;

public class ElectricFieldController : SkillController
{
    public Animator skillAnimator;
    public AudioSource skillAudio;
    private CircleCollider2D skillColllider2D;

    private LightningSpawner lightningSpawner;

    public float closeColliderTime;                         //关闭触发器的时间帧
    public float openColliderTime;                          //打开触发器的时间帧
    public float endAnimationTime;                          //动画结束的时间帧

    private float lastTime;

    private int damageCount;                                //伤害次数
    protected override void Awake()
    {
        skillColllider2D = GetComponent<CircleCollider2D>();
        lightningSpawner = GetComponentInParent<LightningSpawner>();

        base.Awake();
    }

    private void OnEnable()
    {
        skillAttribute = lightningSpawner.evolutionSkill.skillAttribute[lightningSpawner.grade - 1];

        damageCount = (int)skillAttribute.duration / (int)skillAttribute.damageCoolDown;
        skillColllider2D.enabled = false;
        skillAnimator.speed = 0;
        lastTime = Time.time;

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
        skillAudio.Stop();
        skillColllider2D.enabled = false;

        if(damageCount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void IsAttack()                                  //事件帧:打开触发器，发出声音
    {
        skillColllider2D.enabled = true;
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
        yield return new WaitForSeconds(skillAttribute.damageCoolDown);

        skillAudio.Play();
        damageCount--;
        skillAnimator.speed = 1;
        lastTime = Time.time;
        skillColllider2D.enabled = false;
        
        StopCoroutine(exitCoroutine);
        OpenDamageCoolDown();
    }
}
