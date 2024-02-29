using System.Collections;
using UnityEngine;

public class ElectricFieldController : SkillController
{
    public Animator skillAnimator;
    public AudioSource skillAudio;
    private CircleCollider2D skillColllider2D;

    private LightningSpawner lightningSpawner;

    public float closeColliderTime;                         //�رմ�������ʱ��֡
    public float openColliderTime;                          //�򿪴�������ʱ��֡
    public float endAnimationTime;                          //����������ʱ��֡

    private float lastTime;

    private int damageCount;                                //�˺�����
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

    private void StopAnimation()                             //�¼�֡:��ͣ�������رմ�����
    {
        skillAnimator.speed = 0;
        skillAudio.Stop();
        skillColllider2D.enabled = false;

        if(damageCount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void IsAttack()                                  //�¼�֡:�򿪴���������������
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

    public IEnumerator DamageCoolDown()                     //������ܶ��������Э��
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
