using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private AttributeManager attributeManager;
    private GameManager gameManager;

    public CurrentAttribute playerAttribute;

    public Animator anim;

    private HealthBarUI healthBarUI;

    private float timer;
    private float size;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
        gameManager = GameManager.Instance;
        gameManager.player = gameObject;

        healthBarUI = GetComponent<HealthBarUI>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        size = transform.localScale.x;                  //��¼��ɫ����x����ߴ�   
        timer = 0;
        healthBarUI.UpdateHealthBar(attributeManager.currentAttribute.health, attributeManager.currentAttribute.maxHealth);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f && attributeManager.currentAttribute.health <= attributeManager.currentAttribute.maxHealth)
        {
            RestoreHealth();
        }
    }

    private void FixedUpdate()
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
            transform.position = Vector2.Lerp(playerMove, movement, attributeManager.currentAttribute.moveSpeed * Time.deltaTime);
            
            SwitchAnimation(true);
        }
        else
        {
            SwitchAnimation(false);
        }

        if (facedirection != 0)         //��ɫת��
        {
            transform.localScale = new Vector3(facedirection * size, transform.localScale.y, 0);
        }
    }

    private void RestoreHealth()
    {
        attributeManager.currentAttribute.health = Mathf.Min(attributeManager.currentAttribute.health + attributeManager.currentAttribute.healthRegen,
                                                                attributeManager.currentAttribute.maxHealth);
        
        timer = 0;
    }

    private void SwitchAnimation(bool mark)      //���������ƶ�����
    {
       if(mark)
       {
            anim.speed = 1;
       }
       else
       {
            anim.Play("Master", -1, 0f);
            anim.speed = 0;
        }
    }
}
