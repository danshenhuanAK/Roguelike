using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public PlayerData_SO playerCurrentData;

    private FightProgressAttributeManager attributeManager;
    private GameManager gameManager;
    private DataManager dataManager;

    public Animator anim;

    private HealthBarUI healthBarUI;

    private float timer;
    private float size;

    private bool isDie;
    private void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
        gameManager = GameManager.Instance;
        dataManager = DataManager.Instance;

        gameManager.player = gameObject;

        if (!dataManager.IsSave())
        {
            attributeManager.gameFightData = Instantiate(attributeManager.gameFightDataTemplate);
            attributeManager.playerData = Instantiate(attributeManager.playerDataTemplate);
        }
    }
    
    private void Start()
    { 
        playerCurrentData = attributeManager.playerData;

        healthBarUI = GetComponent<HealthBarUI>();

        size = transform.localScale.x;                  //��¼��ɫ����x����ߴ�   
        timer = 0;
        healthBarUI.CreateHealthBar();
    }

    private void Update()
    {
        if(gameManager.gameState == GameState.Fighting)
        {
            healthBarUI.UpdateHealthBar(playerCurrentData.health, playerCurrentData.maxHealth);
        }

        timer += Time.deltaTime;

        if (timer >= 1f && playerCurrentData.health <= playerCurrentData.maxHealth)
        {
            RestoreHealth();
        }

        if(playerCurrentData.health <= 0 && !isDie)
        {
            if(playerCurrentData.revival > 0)
            {
                playerCurrentData.health = playerCurrentData.maxHealth / 2;
                playerCurrentData.revival--;
            }
            else
            {
                DieEvent();
            }
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
        Vector2 playerMove = new(transform.position.x, transform.position.y);
        
        if (h != 0 || v != 0)
        {
            Vector2 moveDir = new Vector2(h, v).normalized;
            Vector2 movement = moveDir + playerMove;
            //���Բ�ֵ�ƶ�
            transform.position = Vector2.Lerp(playerMove, movement, (float)playerCurrentData.moveSpeed * Time.deltaTime);
            
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
        playerCurrentData.health = Mathf.Min((float)(playerCurrentData.health + playerCurrentData.healthRegen), 
                                            (float)playerCurrentData.maxHealth);
        
        timer = 0;
    }

    private void DieEvent()
    {
        isDie = true;
        UIPanelManager.Instance.PushPanel(UIPanelType.GameOverPanel, UIPanelType.GameOverPanelCanvas);
        dataManager.ClearGameData();
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
