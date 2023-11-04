using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpawner : MonoBehaviour
{
    [SerializeField] protected CharacterStats skillData;
    [SerializeField] protected Transform skillPoint;
    [SerializeField] protected AudioSource skillAudio;
    protected GameObject skill;
    protected Transform enemy;
    protected List<Transform> enemys = new List<Transform>();

    protected ObjectPool objectPool;

    protected Coroutine exitCoroutine;
    protected virtual void Awake()
    {
        objectPool = ObjectPool.Instance;
        skillPoint = GameObject.FindGameObjectWithTag("Center").transform;
    }

    private Collider2D[] SkillSelectors()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(skillPoint.position, skillData.skillAttackData.attackRange, 
                                                            skillData.skillAttackData.attackMask);
        return collider;
    }

    private Collider2D SkillSelector()
    {
        Collider2D collider = Physics2D.OverlapCircle(skillPoint.position, skillData.skillAttackData.attackRange,
                                                            skillData.skillAttackData.attackMask);
        return collider;
    }

    public void GetNearestEnemy()
    {
        Collider2D[] collider = SkillSelectors();

        if(collider == null)
        {
            enemy = null;
            return;
        }

        Transform nearestEnemy = collider[0].transform;
        float firstDistance = Vector3.Distance(skillPoint.position, nearestEnemy.position);

        for(int i = 0; i < collider.Length; i++)
        {
            float distance = Vector3.Distance(skillPoint.position, collider[i].transform.position);

            if(firstDistance > distance)
            {
                firstDistance = distance;
                nearestEnemy = collider[i].transform;
            }    
        }

        enemy = nearestEnemy;
    }

    public void GetRandomEnemy()
    {
        Collider2D collider = SkillSelector();

        if(collider == null)
        {
            enemy = null;
            return;
        }

        enemy = collider.transform;
    }

    public void GetRandomEnemys()
    {
        Collider2D[] colliders = SkillSelectors();

        if(colliders == null)                           //没有检测出敌人直接return
        {
            enemys = null;
            return;
        }
        //检测出的敌人比此技能发射数量少则将colliders中的直接赋值给enemys然后return
        if(colliders.Length <= skillData.skillAttackData.currentProjectileQuantity)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                enemys.Add(colliders[i].transform);
            }
            return;
        }

        HashSet<int> nums = new HashSet<int>();
        System.Random r = new System.Random();

        //用哈希表保证enemys不重复
        while (nums.Count != skillData.skillAttackData.currentProjectileQuantity)
        {
            nums.Add(r.Next(0, colliders.Length));
        }

        int length = 0;

        foreach(var i in nums)
        {
            enemys.Add(colliders[i].transform);
            
            length++;
        }
    }

    public bool PrepareSkill(CharacterStats skillData)
    {
        if (skillData.skillAttackData.cdRemain <= 0)
        {
            skillData.skillAttackData.cdRemain = skillData.skillAttackData.currentCoolDown;
            if (exitCoroutine != null)                      //已经开启携程则关闭上一个开启的协程
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }
            return true;
        }
        else
        {
            return false;
        } 
    }

    public void AlterSkill(CharacterStats alterSkilData)
    {
        skillData = alterSkilData;
    }

    public void ChangeSkillSize(GameObject skill)
    {
        skill.transform.localScale = new Vector3(skillData.skillAttackData.currentScale.x, skillData.skillAttackData.currentScale.y, 0);
    }

    public IEnumerator CoolTimeDown(CharacterStats skillData)
    {
        yield return new WaitForSeconds(skillData.skillAttackData.cdRemain);

        skillData.skillAttackData.cdRemain = 0;
    }
}
