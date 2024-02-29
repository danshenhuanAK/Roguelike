using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AttributeManager attributeManager;
    public EnemyCurrentAttribute enemyCurrentAttribute = new();
    private GameManager gameManager;
    
    private GameObject attackTarget;                                //攻击目标
    public GameObject experienceGemSpawner;

    private HealthBarUI healthBarUI;                                //血量条UI

    private SpriteRenderer render;                                  //物体的Renderer组件        
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;                              //物体刚体组件
    public Material deatfMaterial;                                  //死亡消散材质球
    public Material attacktedMaterial;                              //受击发白材质球

    private Vector2 sizeXY;                                         //记录本来的物体面朝方向
    private float dieTime;                                          //消散程度时间增量
    private bool isDie;                                             //是否死亡
    public float glowTime;                                          //受击发白持续时间
    private bool isAttacked;                                        //是否被攻击
    private float glowTimeRecord;                                   //受击发白记录时间

    //怪物修正位置
    private Transform minAmend;                                      
    private Transform maxAmend;
    private float amendTime;

    private Coroutine exitCoroutine;                                //攻击间隔携程
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

        //获取当前朝向
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
        if (enemyCurrentAttribute.currentHealth <= 0)                           //死亡处理
        {
            MonsterDeath();
        }
        
        if(isAttacked)                                                          //受击处理
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

        //与主角的相对位置
        float facedirection = transform.position.x - attackTarget.transform.position.x;
        
        //变化朝向
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //位移
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, enemyCurrentAttribute.moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 与主角的碰撞事件
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemyCurrentAttribute.isAttack == true)           //如果碰撞到的是主角，让主角扣血
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
    /// 与技能的碰撞事件
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
    /// 被攻击时发光持续时间
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
        deatfMaterial.SetFloat("_dissolve", dieTime);                   //物体消散程度

        Vector3 dirVec = transform.position - gameManager.player.transform.position;
        enemyRigidbody.AddForce(dirVec.normalized * 0.6f, ForceMode2D.Impulse);

        if (!isDie)                                                     //死亡后直接进行的行为
        {
            if (enemyCurrentAttribute.isBoss)                           //精英怪或者boss需要隐藏血条
            {
                gameObject.GetComponent<HealthBarUI>().CloseUIbar();
            }

            experienceGemSpawner.GetComponent<ExperienceGemSpawner>().Destroy(transform);                  //掉落经验宝石
            enemyCurrentAttribute.moveSpeed = 0;
            GetComponent<Animator>().speed = 0;
            gameManager.kills++;
        }
        else if (dieTime >= 1)                                          //怪物消散后进行的行为
        {
            StopAllCoroutines();
            gameObject.SetActive(false);                                //物体隐藏减少性能损耗
        }

        isDie = true;
    }

    private IEnumerator AttackCoolTimeDown()
    {
        yield return new WaitForSeconds(enemyCurrentAttribute.coolDown);

        enemyCurrentAttribute.isAttack = true;
    }
}
