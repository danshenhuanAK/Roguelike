using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowSelector : ISkillSelector
{
    public Transform[] SelectTarget(CharacterStats data, Transform skillTF, LayerMask enemyMask)
    {
        Transform[] enemy = { };

        GameObject thisObject = data.GetComponent<GameObject>();

        Collider2D collider = Physics2D.OverlapCircle(thisObject.transform.position, data.skillAttackData.attackRange, enemyMask);

        enemy[0] = collider.transform;

        Vector3 v = enemy[0].position - thisObject.transform.position;
        v.z = 0;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, v);
        thisObject.transform.rotation = rotation;
        
        return enemy;
    }
}
