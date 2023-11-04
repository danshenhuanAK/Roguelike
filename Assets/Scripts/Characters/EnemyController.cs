using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject attackTarget;            //����Ŀ��

    private CharacterStats enemyData;           //��������
    private CharacterStats playerData;          //��������
    private HealthBarUI healthBarUI;            //Ѫ����UI

    private Vector2 sizeXY;
    public bool isBoss;

    private Coroutine exitCoroutine;
    private void Awake()
    {
        healthBarUI = GetComponent<HealthBarUI>();
        enemyData = GetComponent<CharacterStats>();
        attackTarget = GameObject.FindGameObjectWithTag("Player");
        playerData = attackTarget.GetComponent<CharacterStats>();        //��ù���Ŀ������

        //��ȡ��ǰ����
        sizeXY = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    private void FixedUpdate()
    {
        if (enemyData.attributeData.currentHealth <= 0)
        {
            if(isBoss)
            {
                gameObject.GetComponent<HealthBarUI>().CloseUIbar();
            }
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        //�����ǵ����λ��
        float facedirection = transform.position.x - attackTarget.transform.position.x;

        //�仯����
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //λ��
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, enemyData.attributeData.currentMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemyData.monsterAttackData.isAttack == true)           //�����ײ���������ǣ������ǿ�Ѫ
        {
            enemyData.monsterAttackData.isAttack = false;
            healthBarUI.UpdateHealthBar(enemyData.attributeData.currentHealth, enemyData.attributeData.maxHealth);
            enemyData.MonsterDamage(playerData);

            if (exitCoroutine != null)
            {
                StopCoroutine(exitCoroutine);
                exitCoroutine = null;
            }
            exitCoroutine = StartCoroutine(AttackCoolTimeDown());
        }
    }

    private IEnumerator AttackCoolTimeDown()
    {
        yield return new WaitForSeconds(enemyData.monsterAttackData.coolDown);

        enemyData.monsterAttackData.isAttack = true;
    }
}
