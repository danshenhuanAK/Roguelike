using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AttributeManager attributeManager;
    public EnemyCurrentAttribute enemyCurrentAttribute = new();
    private GameManager gameManager;
    
    private GameObject attackTarget;                                //����Ŀ��
    public GameObject experienceGemSpawner;

    private HealthBarUI healthBarUI;                                //Ѫ����UI

    private SpriteRenderer render;                                  //�����Renderer���        
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;                              //����������
    public Material deatfMaterial;                                  //������ɢ������
    public Material attacktedMaterial;                              //�ܻ����ײ�����

    private Vector2 sizeXY;                                         //��¼�����������泯����
    private float dieTime;                                          //��ɢ�̶�ʱ������
    private bool isDie;                                             //�Ƿ�����
    public float glowTime;                                          //�ܻ����׳���ʱ��
    private bool isAttacked;                                        //�Ƿ񱻹���
    private float glowTimeRecord;                                   //�ܻ����׼�¼ʱ��

    //��������λ��
    private Transform minAmend;                                      
    private Transform maxAmend;
    private float amendTime;

    private Coroutine exitCoroutine;                                //�������Я��
    private void Awake()
    {
        gameManager = GameManager.Instance;
        attributeManager = AttributeManager.Instance;

        enemyRigidbody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        deatfMaterial = Instantiate(deatfMaterial);
        attacktedMaterial = Instantiate(attacktedMaterial);

        minAmend = transform.parent.GetComponent<EnemySpawner>().minSpawn;
        maxAmend = transform.parent.GetComponent<EnemySpawner>().maxSpawn;
    }

    private void Start()
    {
        attackTarget = GameObject.FindGameObjectWithTag("Player");
        experienceGemSpawner = GameObject.FindGameObjectWithTag("Gem");

        healthBarUI = attackTarget.GetComponent<HealthBarUI>();

        //��ȡ��ǰ����
        sizeXY = new Vector2(transform.localScale.x, transform.localScale.y);

        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
            exitCoroutine = null;
        }
        exitCoroutine = StartCoroutine(AttackCoolTimeDown());
    }

    private void OnEnable()
    {
        amendTime = 2f;
        dieTime = 0;
        glowTimeRecord = glowTime;
        isDie = false;
        deatfMaterial.SetFloat("_dissolve", 0);
        attacktedMaterial.SetInt("_BeAttack", 0);
        GetComponent<Animator>().speed = 1;
    }

    public void GetAttribute(EnemyCurrentAttribute attribute)
    {
        enemyCurrentAttribute.maxHealth = attribute.maxHealth;
        enemyCurrentAttribute.currentHealth = attribute.currentHealth;
        enemyCurrentAttribute.defence = attribute.defence;
        enemyCurrentAttribute.moveSpeed = attribute.moveSpeed;
        enemyCurrentAttribute.attackDamage = attribute.attackDamage;
        enemyCurrentAttribute.coolDown = attribute.coolDown;
        enemyCurrentAttribute.isAttack = attribute.isAttack;
        enemyCurrentAttribute.isBoss = attribute.isBoss;
    }

    private void FixedUpdate()
    {
        if (enemyCurrentAttribute.currentHealth <= 0)                           //��������
        {
            MonsterDeath();
        }
        
        if(isAttacked)                                                          //�ܻ�����
        {
            EnemyAttacked();
        }

        amendTime -= Time.deltaTime;
        
        if(amendTime < 0 && (transform.position.x < Mathf.Min(minAmend.position.x, maxAmend.position.x) || transform.position.y < minAmend.position.y 
            || transform.position.x > Mathf.Max(minAmend.position.x, maxAmend.position.x) || transform.position.y > maxAmend.position.y))
        {
            transform.position = transform.parent.GetComponent<EnemySpawner>().SelectSpawnPoint();
            amendTime = 2f;
        }

        //�����ǵ����λ��
        float facedirection = transform.position.x - attackTarget.transform.position.x;
        
        //�仯����
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //λ��
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, enemyCurrentAttribute.moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �����ǵ���ײ�¼�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemyCurrentAttribute.isAttack == true)           //�����ײ���������ǣ������ǿ�Ѫ
        {
            enemyCurrentAttribute.isAttack = false;
            attributeManager.MonsterDamage(enemyCurrentAttribute);
            healthBarUI.UpdateHealthBar(attributeManager.currentAttribute.health, attributeManager.currentAttribute.maxHealth);

            if (exitCoroutine != null)
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }
            exitCoroutine = StartCoroutine(AttackCoolTimeDown());
        }
    }

    /// <summary>
    /// �뼼�ܵ���ײ�¼�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Skill" && enemyCurrentAttribute.currentHealth > 0)
        {
            isAttacked = true;
            glowTimeRecord = glowTime;
            render.material = attacktedMaterial;
            Vector3 dirVec = transform.position - gameManager.player.transform.position;
            enemyRigidbody.AddForce(dirVec.normalized * 23f, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// ������ʱ�������ʱ��
    /// </summary>
    private void EnemyAttacked()
    {
        glowTimeRecord -= Time.deltaTime;

        if (glowTimeRecord > 0)
        {
            attacktedMaterial.SetInt("_BeAttack", 1);
        }
        else
        {
            isAttacked = false;
            glowTimeRecord = glowTime;
            attacktedMaterial.SetInt("_BeAttack", 0);
        }
    }

    private void MonsterDeath()
    {
        dieTime += Time.deltaTime;
        render.material = deatfMaterial;
        deatfMaterial.SetFloat("_dissolve", dieTime);                   //������ɢ�̶�

        Vector3 dirVec = transform.position - gameManager.player.transform.position;
        enemyRigidbody.AddForce(dirVec.normalized * 0.6f, ForceMode2D.Impulse);

        if (!isDie)                                                     //������ֱ�ӽ��е���Ϊ
        {
            if (enemyCurrentAttribute.isBoss)                           //��Ӣ�ֻ���boss��Ҫ����Ѫ��
            {
                gameObject.GetComponent<HealthBarUI>().CloseUIbar();
            }

            experienceGemSpawner.GetComponent<ExperienceGemSpawner>().Destroy(transform);                  //���侭�鱦ʯ
            enemyCurrentAttribute.moveSpeed = 0;
            GetComponent<Animator>().speed = 0;
            gameManager.kills++;
        }
        else if (dieTime >= 1)                                          //������ɢ����е���Ϊ
        {
            StopAllCoroutines();
            gameObject.SetActive(false);                                //�������ؼ����������
        }

        isDie = true;
    }

    private IEnumerator AttackCoolTimeDown()
    {
        yield return new WaitForSeconds(enemyCurrentAttribute.coolDown);

        enemyCurrentAttribute.isAttack = true;
    }
}
