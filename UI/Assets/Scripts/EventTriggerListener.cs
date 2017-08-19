using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class EventTriggerListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IBeginDragHandler, IDragHandler
{
    //Unity中自动添加委托的方法
    public UnityAction<GameObject> onClick;
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onPointerUp;
    public UnityAction<PointerEventData> onPointerDown;
    public UnityAction<PointerEventData> onPointerEnter;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClick != null)
        {
            onClick(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null)
        {
            onPointerEnter(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(onPointerUp != null)
        {
            onPointerUp(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
        {
            onPointerDown(eventData);
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
