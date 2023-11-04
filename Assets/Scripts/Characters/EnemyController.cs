using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject attackTarget;            //攻击目标

    private CharacterStats enemyData;           //敌人属性
    private CharacterStats playerData;          //主角属性
    private HealthBarUI healthBarUI;            //血量条UI

    private Vector2 sizeXY;
    public bool isBoss;

    private Coroutine exitCoroutine;
    private void Awake()
    {
        healthBarUI = GetComponent<HealthBarUI>();
        enemyData = GetComponent<CharacterStats>();
        attackTarget = GameObject.FindGameObjectWithTag("Player");
        playerData = attackTarget.GetComponent<CharacterStats>();        //获得攻击目标属性

        //获取当前朝向
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
        //与主角的相对位置
        float facedirection = transform.position.x - attackTarget.transform.position.x;

        //变化朝向
        if (facedirection < 0f)
        {
            transform.localScale = new Vector3(sizeXY.x * -1, sizeXY.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(sizeXY.x, sizeXY.y, 0);
        }

        //位移
        transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, enemyData.attributeData.currentMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemyData.monsterAttackData.isAttack == true)           //如果碰撞到的是主角，让主角扣血
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
