using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelicFunction : MonoBehaviour, IRelicSaveable
{
    protected FightProgressAttributeManager attributeManager;
    public RelicData relicData;

    protected virtual void Awake()
    {
        attributeManager = FightProgressAttributeManager.Instance;

        IRelicSaveable saveable = this;
        saveable.RegisterRelicData();
    }

    private void OnDestroy()
    {
        IRelicSaveable saveable = this;
        saveable.UnRegisterRelicData();
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

    public void GetRelicData(List<RelicData> relicDatas)
    {
        if (relicDatas.Contains(relicData))
        {
            relicDatas[relicData.relicNum] = relicData;
        }
        else
        {
            relicDatas.Add(relicData);
        }
    }
}
