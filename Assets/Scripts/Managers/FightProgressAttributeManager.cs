using UnityEngine;

public class FightProgressAttributeManager : Singleton<FightProgressAttributeManager>, IFightSaveable, IPlayerSaveable
{
    public PlayerData_SO playerDataTemplate;
    public PlayerData_SO playerData;

    public GameFightData_SO gameFightDataTemplate;
    public GameFightData_SO gameFightData;

    private GameObject player;

    public bool isCreatDamageUi;

    private DataManager dataManager;

    protected override void Awake()
    {
        base.Awake();

        dataManager = DataManager.Instance;

        IFightSaveable fightsaveable = this;
        fightsaveable.RegisterFightData();

        IPlayerSaveable playersaveable = this;
        playersaveable.RegisterPlayerData();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("isDamagerNumber", 1) == 1)
        {
            isCreatDamageUi = true;
        }
        else
        {
            isCreatDamageUi = false;
        }
    }

    private void OnDestroy()
    {
        IFightSaveable fightsaveable = this;
        fightsaveable.UnRegisterFightData();

        IPlayerSaveable playersaveable = this;
        playersaveable.UnRegisterPlayerData();
    }

    public Vector2 SkillDamage(PlayerSkillData_SO skillData, EnemyData_SO enemyData)                    //���ܶԹ�����ɵ��˺�
    {
        float damage = Mathf.Max((float)(skillData.attackDamage * (1 + playerData.attackPower) - enemyData.defence) , 0);
        int isCritical = 0;

        if (Random.Range(0f,1f) <= playerData.critical)
        {
            damage *= (float)playerData.criticalDamage;
            isCritical = 1;
        }

        enemyData.currentHealth -= damage;

        return new Vector2(damage, isCritical);
    }

    public void MonsterDamage(EnemyData_SO enemyData)                  //�������������˺�
    {
        float damage = Mathf.Max((float)enemyData.attackDamage - (float)playerData.defence, 0.1f);

        if(gameFightData.Shield > 0 && Random.Range(0f, 1f) <= gameFightData.Shield)
        {
            damage *= (1 - (float)gameFightData.Shield);
        }

        if(gameFightData.KnightArmor > 0 && Random.Range(0f, 1f) <= gameFightData.KnightArmor)
        {
            damage = 0;
        }

        playerData.health -= damage;

        if(playerData.health <= 0 && playerData.revival!= 0)
        {
            playerData.health = 50f;
            playerData.revival--;
        }
    }

    public void GetFightData()
    {
        dataManager.gameFightData = Instantiate(gameFightData);
    }

    public void LoadFightData(GameFightData_SO FightData)
    {
        gameFightData = Instantiate(FightData);
    }

    public void GetPlayerData()
    {
        dataManager.playerData = Instantiate(playerData);
    }

    public void LoadPlayerData(PlayerData_SO playerData)
    {
        this.playerData = Instantiate(playerData);

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().playerCurrentData = this.playerData;
        player.GetComponent<PlayerStats>().UpdateSlider();
    }

    #region PlayerBuff
    public void HealthRegen(float value)                    //�����ظ�
    {
        playerData.healthRegen = Mathf.Max((float)playerData.healthRegen + value, 0);
    }

    public void Health(float value)                         //���ӻ�������ﵱǰ����ֵ
    {
        playerData.health = Mathf.Min((float)playerData.health + value, (float)playerData.maxHealth);
    }

    public void ChangeHealth(float value)                   //ֱ�ӱ仯���ﵱǰ����ֵ
    {
        playerData.health = value;
    }

    public void MaxHealth(float value)                      //���ӻ������������
    {
        playerData.maxHealth += value;

        if (playerData.health > playerData.maxHealth)
        {
            playerData.health = playerData.maxHealth;
        }
    }

    public void Defence(float value)                        //���ӻ���ٷ���ֵ
    {
        playerData.defence = Mathf.Max((float)playerData.defence + value, 0);
    }

    public void MoveSpeed(float value)                      //���ӻ�����ƶ��ٶ�
    {
        playerData.moveSpeed = Mathf.Max((float)playerData.moveSpeed + value, 0);
    }

    public void AttackPower(float value)                    //���ӻ���ٹ�����
    {
        playerData.attackPower = Mathf.Max((float)playerData.attackPower + value, 0);
    }

    public void LaunchMoveSpeed(float value)                //���ӻ���ټ��ܵ����ٶ�
    {
        playerData.launchMoveSpeed = Mathf.Max((float)playerData.launchMoveSpeed + value, 0);
    }

    public void Duration(float value)                       //���ӻ���ټ��ܳ���ʱ��
    {
        playerData.duration = Mathf.Max((float)playerData.duration + value, 0);
    }
    public void AttackRange(float value)                    //���ӻ���ټ��ܹ�����Χ
    {
        playerData.attackRange = Mathf.Max((float)playerData.attackRange + value, 0);
    }
    public void SkillCoolDown(float value)                  //���ӻ���ټ�����ȴ
    {
        playerData.skillCoolDown = Mathf.Max((float)playerData.skillCoolDown + value, 0);
    }
    public void ProjectileQuantity(int value)             //���ӻ���ټ��ܷ�������
    {
        playerData.projectileQuantity = Mathf.Max(playerData.projectileQuantity + value, 0);
    }
    public void Revival(int value)                          //���ӻ���ٸ������
    {
        playerData.revival = Mathf.Max(playerData.revival + value, 0);
    }
    public void Magnet(float value)                         //���ӻ����ʰȡ��Χ
    {
        playerData.magnet = Mathf.Max((float)playerData.magnet + value, 0.1f);
    }
    public void Critical(float value)                       //���ӻ���ٱ�����
    {
        playerData.critical = Mathf.Max((float)playerData.critical + value, 0);
    }
    public void CriticalDamage(float value)                 //���ӻ���ٱ����˺�
    {
        playerData.criticalDamage = Mathf.Max((float)playerData.criticalDamage + value, 0);
    }

    public void ExperienceCquisitionSpeed(float value)          //���ӻ���پ����ȡ����
    {
        playerData.experienceCquisitionSpeed = Mathf.Max((float)playerData.experienceCquisitionSpeed + value, 0);
    }

    public void Luck(int value)                             //���ӻ��������ֵ
    {
        playerData.luck = Mathf.Max(playerData.luck + value, 0);
    }

    public void Gold(int value)                             //���ӻ������Ϸ���
    {
        gameFightData.gold = Mathf.Max(gameFightData.gold + value, 0);
    }
    #endregion
}
