using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    //Unity中自动添加委托的方法
    public UnityAction onClick;
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onPointerUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClick != null)
        {
            onClick();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(onPointerUp != null)
        {
            onPointerUp(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(onDrag != null)
        {
            onDrag(eventData);
        }
    }
}
