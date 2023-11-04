using System.Collections;
using UnityEngine;

public class ElectricFieldController : SkillController
{
    public Animator skillAnimator;
    public AudioSource skillAudio;
    private CircleCollider2D skillColllider2D;

    public float closeColliderTime;                         //�رմ�������ʱ��֡
    public float openColliderTime;                          //�򿪴�������ʱ��֡
    public float endAnimationTime;                          //����������ʱ��֡

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

    private void StopAnimation()                             //�¼�֡:��ͣ�������رմ�����
    {
        skillAnimator.speed = 0;
        skillColllider2D.enabled = false;
    }

    private void IsAttack()                                  //�¼�֡:�򿪴���������������
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

    public IEnumerator DamageCoolDown()                     //������ܶ��������Э��
    {
        yield return new WaitForSeconds(skillData.skillAttackData.currentCoolDown);

        skillAnimator.speed = 1;
        lastTime = Time.time;
        skillColllider2D.enabled = false;
        
        StopCoroutine(exitCoroutine);
        OpenDamageCoolDown();
    }
}
