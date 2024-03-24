using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkillSpawner : MonoBehaviour
{
    public PlayerSkillData skillData;                           //技能数值

    protected GameObject skillPre;                              //加载出来的技能预制体
    protected GameObject skill;                                 //生成的技能

    private SkillSpawnerController skillSpawnerController;          //技能生成控制器

    [SerializeField] protected Transform skillPoint;

    //锁定的敌人位置
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
    /// 锁定技能范围内所有敌人后，选取最近的敌人
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
    /// 获得一个随机攻击对象
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
    /// 获得多个随机攻击对象
    /// </summary>
    public void GetRandomEnemys()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skillPoint.position, (float)skillData.playerCurrentSkillData.searchEnemyRange,
                                                           skillData.skillAttackMask);

        if (colliders == null)                           //没有检测出敌人直接return
        {
            enemys = null;
            Debug.Log(enemys.Count);
            return;
        }
        //检测出的敌人比此技能发射数量少则将colliders中的直接赋值给enemys然后return
        if (colliders.Length <= skillData.playerCurrentSkillData.skillProjectileQuantity)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                enemys.Add(colliders[i].transform);
            }
            return;
        }

        HashSet<int> nums = new();

        //用哈希表保证enemys不重复
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
    /// 判断技能是否冷却结束
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
    /// 改变技能视觉大小
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
