using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillSpawner : MonoBehaviour
{
    public PlayerSkillData skillData;                           //������ֵ

    protected GameObject skillPre;                              //���س����ļ���Ԥ����
    protected GameObject skill;                                 //���ɵļ���

    private SkillSpawnerController skillSpawnerController;          //�������ɿ�����

    [SerializeField] protected Transform skillPoint;

    //�����ĵ���λ��
    protected Transform enemy;                                  
    protected List<Transform> enemys = new();                   

    protected int grade;

    protected GameManager gameManager;
    protected AudioManager audioManager;
    protected ObjectPool objectPool;
    protected DataManager dataManager;

    protected float cdRemain;
    private Vector3 skillSize;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        objectPool = ObjectPool.Instance;
        audioManager = AudioManager.Instance;
        dataManager = DataManager.Instance;

        if (!dataManager.IsSave())
        {
            skillData.playerCurrentSkillData = Instantiate(skillData.playerBaseSkillData);
        }

        LoadPre();

        if (skillData.searchEnemyPos == SkillSearchEnemyPos.Center)
        {
            skillPoint = GameObject.FindGameObjectWithTag("Center").transform;
        }
        if (skillData.searchEnemyPos == SkillSearchEnemyPos.Sole)
        {
            skillPoint = GameObject.FindGameObjectWithTag("Sole").transform;
        }
    }

    public virtual void LoadPre()
    {
        skillData.skillAsset.LoadAssetAsync<GameObject>().Completed += (handle) =>
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                skillPre = handle.Result;
            }
        };
        skillData.skillAsset.ReleaseAsset();
    }

    public void LoadData()
    {
        skillSpawnerController.playerSkillDataList.playerSkillDatas.Add(skillData.playerCurrentSkillData);
        cdRemain = (float)skillData.playerCurrentSkillData.coolDown;
    }

    private void Start()
    {
        skillSpawnerController = transform.parent.GetComponent<SkillSpawnerController>();

        if (!dataManager.IsSave())
        {
            LoadData();
        }
        else
        {
            PlayerSkillData_SO data = skillSpawnerController.playerSkillDataList.playerSkillDatas.Find(skillData => skillData.skillSpanwerName == gameObject.name);
            skillData.playerCurrentSkillData = Instantiate(data);
            cdRemain = (float)skillData.playerCurrentSkillData.coolDown;
        }
    }

    public virtual void UpdateSkillAttribute()
    {
        grade = skillData.playerCurrentSkillData.grade;

        skillData.playerCurrentSkillData.duration *= 1 + skillData.playerSkillBuffDataList[grade].durationBuff;
        skillData.playerCurrentSkillData.coolDown *= 1 + skillData.playerSkillBuffDataList[grade].coolDownBuff;
        skillData.playerCurrentSkillData.damageCoolDown *= 1 + skillData.playerSkillBuffDataList[grade].damageCoolDownBuff;
        skillData.playerCurrentSkillData.launchMoveSpeed *= 1 + skillData.playerSkillBuffDataList[grade].launchMoveSpeedBuff;
        skillData.playerCurrentSkillData.searchEnemyRange *= 1 + skillData.playerSkillBuffDataList[grade].searchEnemyRangeBuff;
        skillData.playerCurrentSkillData.skillProjectileQuantity += skillData.playerSkillBuffDataList[grade].skillProjectileQuantityBuff;
        skillData.playerCurrentSkillData.attackRange += skillData.playerSkillBuffDataList[grade].attackRangeBuff;
        skillData.playerCurrentSkillData.attackDamage *= 1 + skillData.playerSkillBuffDataList[grade].attackDamageBuff;
        skillData.playerCurrentSkillData.repelPower *= 1 + skillData.playerSkillBuffDataList[grade].repelPowerBuff;
        skillData.playerCurrentSkillData.retardPower *= 1 + skillData.playerSkillBuffDataList[grade].retardPowerBuff;
        skillData.playerCurrentSkillData.grade++;

        skillSize = new Vector3(skillSize.x * (1 + skillData.playerSkillBuffDataList[grade].attackRangeBuff),
                skillSize.y * (1 + skillData.playerSkillBuffDataList[grade].attackRangeBuff), 0);

        if (!skillSpawnerController.playerSkillDataList.playerSkillDatas.Contains(skillData.playerCurrentSkillData))
        {
            skillSpawnerController.playerSkillDataList.playerSkillDatas.Add(skillData.playerCurrentSkillData);
        }
    }

    /// <summary>
    /// �������ܷ�Χ�����е��˺�ѡȡ����ĵ���
    /// </summary>
    public void GetNearestEnemy()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(skillPoint.position, (float)skillData.playerCurrentSkillData.searchEnemyRange,
                                                           skillData.skillAttackMask);

        if (collider == null)
        {
            enemy = null;
            return;
        }

        Transform nearestEnemy = collider[0].transform;
        float firstDistance = Vector3.Distance(skillPoint.position, nearestEnemy.position);

        for (int i = 0; i < collider.Length; i++)
        {
            float distance = Vector3.Distance(skillPoint.position, collider[i].transform.position);

            if (firstDistance > distance)
            {
                firstDistance = distance;
                nearestEnemy = collider[i].transform;
            }
        }

        enemy = nearestEnemy;
    }

    /// <summary>
    /// ���һ�������������
    /// </summary>
    public void GetRandomEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(skillPoint.position, (float)skillData.playerCurrentSkillData.searchEnemyRange,
                                                    skillData.skillAttackMask);

        if (collider == null)
        {
            enemy = null;
            return;
        }

        enemy = collider.transform;
    }

    /// <summary>
    /// ��ö�������������
    /// </summary>
    public void GetRandomEnemys()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skillPoint.position, (float)skillData.playerCurrentSkillData.searchEnemyRange,
                                                           skillData.skillAttackMask);

        if (colliders == null)                           //û�м�������ֱ��return
        {
            enemys = null;
            Debug.Log(enemys.Count);
            return;
        }
        //�����ĵ��˱ȴ˼��ܷ�����������colliders�е�ֱ�Ӹ�ֵ��enemysȻ��return
        if (colliders.Length <= skillData.playerCurrentSkillData.skillProjectileQuantity)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                enemys.Add(colliders[i].transform);
            }
            return;
        }

        HashSet<int> nums = new();

        //�ù�ϣ��֤enemys���ظ�
        while (nums.Count != skillData.playerCurrentSkillData.skillProjectileQuantity)
        {
            nums.Add(Random.Range(0, colliders.Length));
        }

        int length = 0;

        foreach (var i in nums)
        {
            enemys.Add(colliders[i].transform);

            length++;
        }
    }

    /// <summary>
    /// �жϼ����Ƿ���ȴ����
    /// </summary>
    /// <returns></returns>
    public bool PrepareSkill()
    {
        if (cdRemain <= 0)
        {
            cdRemain = (float)skillData.playerCurrentSkillData.coolDown;
            return true;
        }
        else
        {
            cdRemain -= Time.deltaTime;
            return false;
        }
    }

    /// <summary>
    /// �ı似���Ӿ���С
    /// </summary>
    /// <param name="skill"></param>
    public void ChangeSkillSize(GameObject skill)
    {
        if(grade == 0)
        {
            return;
        }

        if(skillData.playerCurrentSkillData.attackRange != 0)
        {
            skill.transform.localScale = skillSize;
        }
    }
}
