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
    /// ��������
    /// </summary>


    /// <summary>
    /// ���ܽ�����ʹ�ý�����ļ�������
    /// </summary>
    public void EvolutionSkill()
    {
        isEvolution = true;
        grade = 0;
        skillData = evolutionSkill;
        skillSize = skillData.skillObject.transform.localScale;
    }

    /// <summary>
    /// �������ܷ�Χ�����е���
    /// </summary>
    /// <returns></returns>
    private Collider2D[] SkillSelectors()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(skillPoint.position, skillData.skillAttribute[grade - 1].attackRange,
                                                            skillData.skillAttackMask);
        return collider;
    }

    /// <summary>
    /// ����ڼ��ܷ�Χ������һ������
    /// </summary>
    /// <returns></returns>
    private Collider2D SkillSelector()
    {
        Collider2D collider = Physics2D.OverlapCircle(skillPoint.position, skillData.skillAttribute[grade - 1].attackRange,
                                                            skillData.skillAttackMask);
        return collider;
    }

    /// <summary>
    /// �������ܷ�Χ�����е��˺�ѡȡ����ĵ���
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
    /// ���һ�������������
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
    /// ��ö�������������
    /// </summary>
    public void GetRandomEnemys()
    {
        Collider2D[] colliders = SkillSelectors();

        if (colliders == null)                           //û�м�������ֱ��return
        {
            enemys = null;
            return;
        }
        //�����ĵ��˱ȴ˼��ܷ�����������colliders�е�ֱ�Ӹ�ֵ��enemysȻ��return
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

        //�ù�ϣ��֤enemys���ظ�
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
    /// �жϼ����Ƿ���ȴ����
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
    /// �ı似���Ӿ���С
    /// </summary>
    /// <param name="skill"></param>
    public void ChangeSkillSize(GameObject skill)
    {
        skillSize = new Vector2(skillData.skillAttribute[grade - 1].skillScale * skillSize.x, skillData.skillAttribute[grade - 1].skillScale * skillSize.y);
        skill.transform.localScale = skillSize;
    }
}
