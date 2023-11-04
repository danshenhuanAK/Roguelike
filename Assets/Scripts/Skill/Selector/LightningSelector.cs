using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSelector : ISkillSelector
{
    public Transform[] SelectTarget(CharacterStats data, Transform skillTF, LayerMask enemyMask)
    {
        Transform[] enemy = { };

        Collider2D[] collider = Physics2D.OverlapCircleAll(skillTF.position, data.skillAttackData.attackRange, enemyMask);
        //HashSet<int> targetEnemy;

        if(collider.Length < data.skillAttackData.currentProjectileQuantity)
        {
            for(int i = 0; i < collider.Length; i++)
            {
                enemy[i] = collider[i].transform;
            }
        }
        else
        {

        }

        throw new System.NotImplementedException();
    }

    private HashSet<int> GetRandomEnemy(int length, int count)
    {
        HashSet<int> nums = new HashSet<int>();
        System.Random r = new System.Random();

        while (nums.Count != count)
        {
            nums.Add(r.Next(0, length));
        }

        return nums;
    }
}
