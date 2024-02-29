using UnityEngine;

public class EvolutionMagicArrowController : SkillController
{
    private Vector3 startPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        startPosition = gameObject.transform.position;
        exitCoroutine = StartCoroutine(SkillDuration());
    }

    private void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * skillAttribute.launchMoveSpeed, Space.World);

        if(Vector3.Distance(startPosition, transform.position) >= skillAttribute.attackRange)
        {
            gameObject.SetActive(false);
            StopCoroutine(exitCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            monsterData = collision.GetComponent<EnemyController>().enemyCurrentAttribute;
            attributeManager.SkillDamage(skillAttribute, monsterData);
        }
    }
}
