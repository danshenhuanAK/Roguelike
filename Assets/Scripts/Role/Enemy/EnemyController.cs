using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private ObjectPool objectPool;
    private AudioManager audioManager;
    private FightProgressAttributeManager attributeManager;
    private DataManager dataManager;
    private GameManager gameManager;
    private UIPanelManager uIPanelManager;

    public EnemyData_SO enemyCurrentData;

    private GameObject attackTarget;                                //攻击目标
    public GameObject playerHitSpecialEffects;                      //主角受击特效
    public AudioClip playerAttackedSound;                           //主角受击音效
    private GameObject particleSpawner;

    private GameObject experienceGemSpawner;                        //经验宝石生成
    private GameObject goldSpawner;                                 //金币生成
    private EnemySpawner enmeySpawner;                              //怪物生成

    private HealthBarUI bossHealthBarUI;

    private SpriteRenderer render;                                  //物体的Renderer组件        
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;                              //物体刚体组件

    public Material deatfMaterial;                                  //死亡消散材质球
    private Vector2 sizeXY;                                         //记录本来的物体面朝方向
    private float dieTime;                                          //消散程度时间增量
    private bool isDie;                                             //是否死亡
    private BoxCollider2D boxCollider2D;                            //死亡关闭受击器

    public AudioClip attackedSound;                                 //怪物受击音效
    public float glowTime;                                          //受击发白持续时间
    private bool isAttacked;                                        //是否被攻击
    private float glowTimeRecord;                                   //受击发白记录时间
    public Material attacktedMaterial;                              //受击发白材质球
    public Transform damageNumberPoint;                             //受击数字UI生成点
    public GameObject attackedDamageNumberUI;                       //受击数字UI
    private GameObject damageNumberPanel;                           //受击数字UI面板

    //怪物修正位置
    private Transform minAmend;                                      
    private Transform maxAmend;
    private float amendTime;

    private Coroutine exitCoroutine;                                //攻击间隔携程

    private bool isRetard;
    private float retardTime;
    private float retardPower;
    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        audioManager = AudioManager.Instance;
        dataManager = DataManager.Instance;
        gameManager = GameManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;
        uIPanelManager = UIPanelManager.Instance;

        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        deatfMaterial = Instantiate(deatfMaterial);
        attacktedMaterial = Instantiate(attacktedMaterial);

        enmeySpawner = transform.parent.GetComponent<EnemySpawner>();
        minAmend = enmeySpawner.minSpawn;
        maxAmend = enmeySpawner.maxSpawn;
    }

    private void Start()
    {
        particleSpawner = GameObject.Find("ParticleSpawner");
        damageNumberPanel = GameObject.Find("DamageNumberPanel");
        attackTarget = GameObject.FindGameObjectWithTag("Player");
        experienceGemSpawner = GameObject.FindGameObjectWithTag("Gem");
        goldSpawner = GameObject.FindGameObjectWithTag("Gold");

        bossHealthBarUI = gameObject.GetComponent<HealthBarUI>();

        //获取当前朝向
        sizeXY = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    private void OnEnable()
    {
        boxCollider2D.enabled = true;
        isDie = false;
        amendTime = 2f;
        dieTime = 0;
        glowTimeRecord = glowTime;
        retardPower = 1;
        deatfMaterial.SetFloat("_dissolve", 0);
        attacktedMaterial.SetInt("_BeAttack", 0);
        GetComponent<Animator>().speed = 1;
    }

    private void FixedUpdate()
    {
        if (enemyCurrentData.currentHealth <= 0)                           //死亡处理
        {
            MonsterDeath();
            return;
        }

        if (isAttacked)                                                          //受击处理
        {
            EnemyAttacked();
        }

        //离主角太远以至于不在摄像机范围内时进行位置修正
        amendTime -= Time.deltaTime;
        if(amendTime < 0 && (transform.position.x < Mathf.Min(minAmend.position.x, maxAmend.position.x) || transform.position.y < minAmend.position.y 
            || transform.position.x > Mathf.Max(minAmend.position.x, maxAmend.position.x) || transform.position.y > maxAmend.position.y))
        {
            transform.position = transform.parent.GetComponent<EnemySpawner>().SelectSpawnPoint();
            amendTime = 2f;
        }

        //朝向主角
        float facedirection = transform.position.x - attackTarget.transform.position.x;             //与主角的相对位置
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //判断是否减速中，并进行减速计时
        if(isRetard)
        {
            retardTime -= Time.deltaTime;

            if(retardTime <= 0)
            {
                retardPower = 1;
                isRetard = false;
            }
        }
        //位移
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, (float)enemyCurrentData.moveSpeed * retardPower * Time.deltaTime);
    }

    /// <summary>
    /// 与主角的碰撞事件
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!enemyCurrentData.isAttack)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))           //如果碰撞到的是主角，让主角扣血
        {
            enemyCurrentData.isAttack = false;
            attributeManager.MonsterDamage(enemyCurrentData);
            objectPool.CreateObject(playerHitSpecialEffects.name, playerHitSpecialEffects, particleSpawner, attackTarget.transform.position, Quaternion.identity);

            audioManager.PlayAttackedSound(playerAttackedSound);

            if (exitCoroutine != null)
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }
            exitCoroutine = StartCoroutine(AttackCoolTimeDown());
        }
    }

    private void OnDisable()
    {
        if (exitCoroutine != null)
        {
            StopCoroutine(exitCoroutine);
            exitCoroutine = null;
        }
    }

    /// <summary>
    /// 技能命中处理
    /// </summary>
    public void HitEnemy(PlayerSkillData_SO skillData)
    {
        Vector2 damage = attributeManager.SkillDamage(skillData, enemyCurrentData);

        if(attributeManager.isCreatDamageUi)
        {
            GameObject damageNumberUI = objectPool.CreateObject(attackedDamageNumberUI.name, attackedDamageNumberUI, damageNumberPanel, damageNumberPoint.position, Quaternion.identity);
            damageNumberUI.GetComponent<TMP_Text>().text = ((int)damage.x).ToString();

            if(damage.y == 1)
            {
                damageNumberUI.GetComponent<TMP_Text>().color = Color.yellow;
            }
            else
            {
                damageNumberUI.GetComponent<TMP_Text>().color = Color.white;
            }
        }
        
        if(enemyCurrentData.isBoss)
        {
            bossHealthBarUI.UpdateHealthBar(enemyCurrentData.currentHealth, enemyCurrentData.maxHealth);
        }

        audioManager.PlayAttackedSound(attackedSound);

        if(enemyCurrentData.currentHealth <= 0)
        {
            return;
        }

        if (skillData.isRepel)
        {
            EnemyRepel((float)skillData.repelPower);
        }
        if (skillData.isRetard)
        {
            EnemyRetard((float)skillData.retardTime, (float)skillData.retardPower);
        }
        isAttacked = true;
        glowTimeRecord = glowTime;
        render.material = attacktedMaterial;
    }

    /// <summary>
    /// 技能命中击退
    /// </summary>
    /// <param name="repelPower"></param>
    public void EnemyRepel(float repelPower)
    {
        if(enemyCurrentData.currentHealth <= 0)
        {
            return;
        }

        Vector3 dirVec = transform.position - gameManager.player.transform.position;

        if(enemyCurrentData.isBoss)
        {
            enemyRigidbody.AddForce(dirVec.normalized * (repelPower / 2), ForceMode2D.Impulse);
        }
        else
        {
            enemyRigidbody.AddForce(dirVec.normalized * repelPower, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// 技能命中减速
    /// </summary>
    /// <param name="time">持续时间</param>
    /// <param name="power">减速程度</param>
    public void EnemyRetard(float time, float power)
    {
        if (enemyCurrentData.currentHealth <= 0)
        {
            return;
        }

        isRetard = true;
        
        if(enemyCurrentData.isBoss)
        {
            retardTime = time / 2;
            retardPower = power / 2;
        }
        else
        {
            retardTime = time;
            retardPower = power;
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

    //怪物死亡处理
    private void MonsterDeath()
    {
        dieTime += Time.deltaTime;
        render.material = deatfMaterial;
        deatfMaterial.SetFloat("_dissolve", dieTime);                   //物体消散程度

        if(!isDie)
        {
            Vector3 dirVec = transform.position - gameManager.player.transform.position;
            enemyRigidbody.AddForce(dirVec.normalized * 0.6f, ForceMode2D.Impulse);

            if (enemyCurrentData.isBoss)                           //精英怪或者boss需要隐藏血条并且进入选关
            {
                gameObject.GetComponent<HealthBarUI>().CloseUIbar();
            }

            boxCollider2D.enabled = false;
            goldSpawner.GetComponent<GoldSpawner>().CreatGold(transform);                       //掉落金币
            experienceGemSpawner.GetComponent<ExperienceGemSpawner>().Destroy(transform);                  //掉落经验宝石
            enemyCurrentData.moveSpeed = 0;
            GetComponent<Animator>().speed = 0;
            attributeManager.gameFightData.kills++;
            enmeySpawner.currentEnemyNum--;

            isDie = true;
        }

        if (dieTime >= 1)                                          //怪物消散后进行的行为
        {
            StopAllCoroutines();
            
            if(enemyCurrentData.isBoss)
            {
                dataManager.currentFloor++;
                if(dataManager.currentFloor == dataManager.maxFloor)
                {
                    uIPanelManager.PushPanel(UIPanelType.GameOverPanel, UIPanelType.GameOverPanelCanvas);
                    dataManager.ClearGameData();
                    return;
                }
                uIPanelManager.PushPanel(UIPanelType.LevelPanel, UIPanelType.LevelPanelCanvas);
                goldSpawner.GetComponent<GoldSpawner>().CloseAllGold();
                transform.parent.GetComponent<EnemySpawner>().CloseAllEnemy();
                experienceGemSpawner.GetComponent<ExperienceGemSpawner>().CloseAllGem();
                dataManager.SaveGameData();
            }

            gameObject.SetActive(false);                                //物体隐藏减少性能损耗
        }
    }

    private IEnumerator AttackCoolTimeDown()
    {
        yield return new WaitForSeconds((float)enemyCurrentData.coolDown);

        enemyCurrentData.isAttack = true;
    }
}
