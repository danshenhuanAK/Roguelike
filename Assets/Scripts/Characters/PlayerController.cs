using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private AttributeManager attributeManager;

    public Animator anim;

    private SpriteRenderer spriteRenderer;
    private CharacterStats playerData;
    private HealthBarUI healthBarUI;

    public Sprite sprite;

    private float timer;
    private float size;

    private void Awake()
    {
        attributeManager = AttributeManager.Instance;
        healthBarUI = GetComponent<HealthBarUI>();
        anim = GetComponent<Animator>();
        playerData = GetComponent<CharacterStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        size = transform.localScale.x;                  //记录角色本身x方向尺寸   
        timer = 0;
        healthBarUI.UpdateHealthBar(attributeManager.currentAttribute.health, attributeManager.currentAttribute.maxHealth);
    }

    private void Update()
    {
        Movement();

        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            RestoreHealth();
        }
    }

    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");              //水平移动
        float v = Input.GetAxis("Vertical");                //竖直移动
        float facedirection = Input.GetAxisRaw("Horizontal");
        Vector2 playerMove = new Vector2(transform.position.x, transform.position.y);

        if (h != 0 || v != 0)
        {
            Vector2 moveDir = new Vector2(h, v).normalized;
            Vector2 movement = moveDir + playerMove;
            //线性插值移动
            transform.position = Vector2.Lerp(playerMove, movement, attributeManager.currentAttribute.moveSpeed * Time.deltaTime);
            SwitchAnimation(false);
        }
        else
        {
            SwitchAnimation(true);
        }

        if (facedirection != 0)         //角色转向
        {
            transform.localScale = new Vector3(facedirection * size, transform.localScale.y, 0);
        }
    }

    private void RestoreHealth()
    {
        attributeManager.currentAttribute.health += attributeManager.currentAttribute.healthRegen;
        timer = 0;
    }

    private void SwitchAnimation(bool mark)      //控制主角移动动画
    {
        spriteRenderer.sprite = sprite;
        anim.SetBool("run", mark);
    }
}
