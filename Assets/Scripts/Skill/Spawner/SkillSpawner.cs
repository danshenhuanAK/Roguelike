using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    public SkillData basicSkill;
    public SkillData evolutionSkill;
    [HideInInspector] public SkillData skillData;
    [SerializeField] protected Transform skillPoint;
    [SerializeField] protected AudioSource skillAudio;

    protected GameObject skill;
    protected Transform enemy;
    protected List<Transform> enemys = new List<Transform>();

    public int grade;
    public bool isEvolution;
    protected Vector2 skillSize;

    protected ObjectPool objectPool;

    protected Coroutine exitCoroutine;
    protected virtual void Awake()
    {
        objectPool = ObjectPool.Instance;

        skillData = basicSkill;
        skillSize = skillData.skillObject.transform.localScale;
    }

    protected virtual void Start()
    {
        skillPoint = GameObject.FindGameObjectWithTag("Center").transform;
    }

    /// <summary>
    /// 技能升级
    /// </summary>


    /// <summary>
    /// 技能进化后使用进化后的技能数据
    /// </summary>
    public void EvolutionSkill()
    {
        isEvolution = true;
        grade = 0;
        skillData = evolutionSkill;
        skillSize = skillData.skillObject.transform.localScale;
    }

    /// <summary>
    /// 锁定技能范围内所有敌人
    /// </summary>
    /// <returns></returns>
    private Collider2D[] SkillSelectors()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(skillPoint.position, skillData.skillAttribute[grade - 1].attackRange,
                                                            skillData.skillAttackMask);
        return collider;
    }

    /// <summary>
    /// 随机在技能范围内锁定一个敌人
    /// </summary>
    /// <returns></returns>
    private Collider2D SkillSelector()
    {
        Collider2D collider = Physics2D.OverlapCircle(skillPoint.position, skillData.skillAttribute[grade - 1].attackRange,
                                                            skillData.skillAttackMask);
        return collider;
    }

    /// <summary>
    /// 锁定技能范围内所有敌人后，选取最近的敌人
    /// </summary>
    public void GetNearestEnemy()
    {
        Collider2D[] collider = SkillSelectors();

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
        Collider2D collider = SkillSelector();

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
        Collider2D[] colliders = SkillSelectors();

        if (colliders == null)                           //没有检测出敌人直接return
        {
            enemys = null;
            return;
        }
        //检测出的敌人比此技能发射数量少则将colliders中的直接赋值给enemys然后return
        if (colliders.Length <= skillData.skillAttribute[grade - 1].skillProjectileQuantity)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                enemys.Add(colliders[i].transform);
            }
            return;
        }

        HashSet<int> nums = new HashSet<int>();
        System.Random r = new System.Random();

        //用哈希表保证enemys不重复
        while (nums.Count != skillData.skillAttribute[grade - 1].skillProjectileQuantity)
        {
            nums.Add(r.Next(0, colliders.Length));
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
        skillData.skillAttribute[grade - 1].cdRemain -= Time.deltaTime;

        if (skillData.skillAttribute[grade - 1].cdRemain <= 0)
        {
            skillData.skillAttribute[grade - 1].cdRemain = skillData.skillAttribute[grade - 1].coolDown;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 改变技能视觉大小
    /// </summary>
    /// <param name="skill"></param>
    public void ChangeSkillSize(GameObject skill)
    {
        skillSize = new Vector2(skillData.skillAttribute[grade - 1].skillScale * skillSize.x, skillData.skillAttribute[grade - 1].skillScale * skillSize.y);
        skill.transform.localScale = skillSize;
    }
}
