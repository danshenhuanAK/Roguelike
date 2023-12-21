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
        size = transform.localScale.x;                  //��¼��ɫ����x����ߴ�   
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

    private void RestoreHealth()
    {
        attributeManager.currentAttribute.health += attributeManager.currentAttribute.healthRegen;
        timer = 0;
    }

    private void SwitchAnimation(bool mark)      //���������ƶ�����
    {
        spriteRenderer.sprite = sprite;
        anim.SetBool("run", mark);
    }
}
