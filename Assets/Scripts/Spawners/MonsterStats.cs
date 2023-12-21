using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    private CharacterStats monsterData;

    private void Awake()
    {
        monsterData = GetComponent<CharacterStats>();
    }

    public class MonsterValue
    {
        public float health;
        public float defence;
        public float moveSpeed;
        public float attackDamage;
    }

    public List<MonsterValue> monsterValues;
}
