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

    public virtual void AtGetStart()                                //��������ʱ
    {

    }

    public virtual void AtStartFight()                              //ս����ʼʱ
    {

    }

    public virtual void AtInFight()                                 //ս������ʱ
    {

    }

    public virtual void AtEndFight()                                //ս������ʱ
    {

    }

    public virtual void AtPlayerUpgrade()                           //��ɫ����ʱ
    {

    }
}
