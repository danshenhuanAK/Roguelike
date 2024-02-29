using System.Collections;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected AttributeManager attributeManager;

    public SkillAttribute skillAttribute;
    
    protected EnemyCurrentAttribute monsterData;
    protected Transform skillPoint;

    protected Coroutine exitCoroutine;

    protected virtual void Awake()
    {
        attributeManager = AttributeManager.Instance;
    }

    protected virtual IEnumerator SkillDuration()                           //������ܳ���ʱ���Э��
    {
        yield return new WaitForSeconds(skillAttribute.duration);
        
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
