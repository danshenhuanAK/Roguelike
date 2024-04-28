using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelicFunction : MonoBehaviour
{
    protected FightProgressAttributeManager attributeManager;
    public RelicData relicData;

    protected virtual void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;
    }

    public virtual void AtGetStart()                                //遗物生成时
    {

    }

    public virtual void AtStartFight()                              //战斗开始时
    {

    }

    public virtual void AtInFight()                                 //战斗进行时
    {

    }

    public virtual void AtEndFight()                                //战斗结束时
    {

    }

    public virtual void AtPlayerUpgrade()                           //角色升级时
    {

    }
}
