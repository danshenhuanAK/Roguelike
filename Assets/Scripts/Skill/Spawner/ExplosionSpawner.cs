using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ExplosionSpawner : SkillSpawner
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (gameManager.gameState == GameState.Fighting && PrepareSkill())
        {
            GetRandomEnemys();
            
            if (enemys.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < enemys.Count; i++)
            {
                skill = objectPool.CreateObject(skillPre.name, skillPre, gameObject, enemys[i].position, Quaternion.identity);
                ChangeSkillSize(skill);
            }

            audioManager.PlaySound("IceExplosion");
            enemys.Clear();
        }
    }
}