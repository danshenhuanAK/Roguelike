using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public Animator anim;
    
    private CharacterStats playerData;
    private HealthBarUI healthBarUI;

    private float size;

    private void Awake()
    {
        healthBarUI = GetComponent<HealthBarUI>();
        anim = GetComponent<Animator>();
        playerData = GetComponent<CharacterStats>();
        size = transform.localScale.x;                  //��¼��ɫ����x����ߴ�   
    }

    private void Start()
    {
        healthBarUI.UpdateHealthBar(playerData.CurrentHealth, playerData.MaxHealth);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");              //ˮƽ�ƶ�
        float v = Input.GetAxis("Vertical");                //��ֱ�ƶ�
        float facedirection = Input.GetAxisRaw("Horizontal");
        Vector2 playerMove = new Vector2(transform.position.x, transform.position.y);

        if (h != 0 || v != 0)
        {
            Vector2 moveDir = new Vector2(h, v).normalized;
            Vector2 movement = moveDir + playerMove;
            //���Բ�ֵ�ƶ�
            transform.position = Vector2.Lerp(playerMove, movement, playerData.attributeData.currentMoveSpeed * Time.deltaTime);
            SwitchAnimation(false);
        }
        else
        {
            SwitchAnimation(true);
        }

        if (facedirection != 0)         //��ɫת��
        {
            transform.localScale = new Vector3(facedirection * size, transform.localScale.y, 0);
        }
    }

    private void SwitchAnimation(bool mark)      //���������ƶ�����
    {
        anim.SetBool("run", mark);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            healthBarUI.UpdateHealthBar(playerData.CurrentHealth, playerData.MaxHealth);
        }
    }
}
