using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Com_Item : UIBase
{
    public Transform targDrag;

	void Start ()
    {
        targDrag = gameObject.transform.Find("Drag");

        SetEventTriggerListener(targDrag.gameObject).onBeginDrag = OnBeginDrag;
        SetEventTriggerListener(targDrag.gameObject).onDrag = OnDrag;
        SetEventTriggerListener(targDrag.gameObject).onPointerUp = OnPointerUp;
    }

    /// <summary>
    /// 进入拖拽时将物体父节点设为Tran_MoveItem
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var point = UIManager.Instance.GetPageUI<Page_Bag>().movePoint;
        targDrag.SetParent(point.transform);
    }

    /// <summary>
    /// 拖拽物体时调整物体坐标
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        targDrag.position = eventData.position;
    }

    /// <summary>
    /// 点击完毕，抬起鼠标时将物体父节点设为原父节点并检查是否在DeleteArea中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        targDrag.SetParent(gameObject.transform);
        if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Img_DeleteArea")
        {
            Destroy(gameObject);
        }
        else
        {
            targDrag.localPosition = Vector3.zero;
        }
    }
}
