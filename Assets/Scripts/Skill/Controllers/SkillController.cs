using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] protected CharacterStats skillData;
    protected CharacterStats monsterData;
    protected Transform skillPoint;

    protected Coroutine exitCoroutine;

    protected virtual void Awake()
    {
        skillData = GetComponent<CharacterStats>();
    }

    protected virtual IEnumerator SkillDuration()                           //这个技能持续时间的协程
    {
        yield return new WaitForSeconds(skillData.skillAttackData.currentDuration);

        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
