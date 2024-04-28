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

    private GameObject attackTarget;                                //����Ŀ��
    public GameObject playerHitSpecialEffects;                      //�����ܻ���Ч
    public AudioClip playerAttackedSound;                           //�����ܻ���Ч
    private GameObject particleSpawner;

    private GameObject experienceGemSpawner;                        //���鱦ʯ����
    private GameObject goldSpawner;                                 //�������
    private EnemySpawner enmeySpawner;                              //��������

    private HealthBarUI bossHealthBarUI;

    private SpriteRenderer render;                                  //�����Renderer���        
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;                              //����������

    public Material deatfMaterial;                                  //������ɢ������
    private Vector2 sizeXY;                                         //��¼�����������泯����
    private float dieTime;                                          //��ɢ�̶�ʱ������
    private bool isDie;                                             //�Ƿ�����
    private BoxCollider2D boxCollider2D;                            //�����ر��ܻ���

    public AudioClip attackedSound;                                 //�����ܻ���Ч
    public float glowTime;                                          //�ܻ����׳���ʱ��
    private bool isAttacked;                                        //�Ƿ񱻹���
    private float glowTimeRecord;                                   //�ܻ����׼�¼ʱ��
    public Material attacktedMaterial;                              //�ܻ����ײ�����
    public Transform damageNumberPoint;                             //�ܻ�����UI���ɵ�
    public GameObject attackedDamageNumberUI;                       //�ܻ�����UI
    private GameObject damageNumberPanel;                           //�ܻ�����UI���

    //��������λ��
    private Transform minAmend;                                      
    private Transform maxAmend;
    private float amendTime;

    private Coroutine exitCoroutine;                                //�������Я��

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

        //��ȡ��ǰ����
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
        if (enemyCurrentData.currentHealth <= 0)                           //��������
        {
            MonsterDeath();
            return;
        }

        if (isAttacked)                                                          //�ܻ�����
        {
            EnemyAttacked();
        }

        //������̫Զ�����ڲ����������Χ��ʱ����λ������
        amendTime -= Time.deltaTime;
        if(amendTime < 0 && (transform.position.x < Mathf.Min(minAmend.position.x, maxAmend.position.x) || transform.position.y < minAmend.position.y 
            || transform.position.x > Mathf.Max(minAmend.position.x, maxAmend.position.x) || transform.position.y > maxAmend.position.y))
        {
            transform.position = transform.parent.GetComponent<EnemySpawner>().SelectSpawnPoint();
            amendTime = 2f;
        }

        //��������
        float facedirection = transform.position.x - attackTarget.transform.position.x;             //�����ǵ����λ��
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //�ж��Ƿ�����У������м��ټ�ʱ
        if(isRetard)
        {
            retardTime -= Time.deltaTime;

            if(retardTime <= 0)
            {
                retardPower = 1;
                isRetard = false;
            }
        }
        //λ��
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, (float)enemyCurrentData.moveSpeed * retardPower * Time.deltaTime);
    }

    /// <summary>
    /// �����ǵ���ײ�¼�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!enemyCurrentData.isAttack)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))           //�����ײ���������ǣ������ǿ�Ѫ
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
    /// �������д���
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
    /// �������л���
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
    /// �������м���
    /// </summary>
    /// <param name="time">����ʱ��</param>
    /// <param name="power">���ٳ̶�</param>
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

    //������������
    private void MonsterDeath()
    {
        dieTime += Time.deltaTime;
        render.material = deatfMaterial;
        deatfMaterial.SetFloat("_dissolve", dieTime);                   //������ɢ�̶�

        if(!isDie)
        {
            Vector3 dirVec = transform.position - gameManager.player.transform.position;
            enemyRigidbody.AddForce(dirVec.normalized * 0.6f, ForceMode2D.Impulse);

            if (enemyCurrentData.isBoss)                           //��Ӣ�ֻ���boss��Ҫ����Ѫ�����ҽ���ѡ��
            {
                gameObject.GetComponent<HealthBarUI>().CloseUIbar();
            }

            boxCollider2D.enabled = false;
            goldSpawner.GetComponent<GoldSpawner>().CreatGold(transform);                       //������
            experienceGemSpawner.GetComponent<ExperienceGemSpawner>().Destroy(transform);                  //���侭�鱦ʯ
            enemyCurrentData.moveSpeed = 0;
            GetComponent<Animator>().speed = 0;
            attributeManager.gameFightData.kills++;
            enmeySpawner.currentEnemyNum--;

            isDie = true;
        }

        if (dieTime >= 1)                                          //������ɢ����е���Ϊ
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

            gameObject.SetActive(false);                                //�������ؼ����������
        }
    }

    private IEnumerator AttackCoolTimeDown()
    {
        yield return new WaitForSeconds((float)enemyCurrentData.coolDown);

        enemyCurrentData.isAttack = true;
    }
}
