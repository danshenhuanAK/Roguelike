using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RelicInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RelicDescribe relicDescribe;

    public string describe;
    protected virtual void Awake()
    {
        relicDescribe = transform.parent.GetComponent<RelicDescribe>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        relicDescribe.RelicEnter(gameObject, describe);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        relicDescribe.RelicExit(gameObject);
    }
}
