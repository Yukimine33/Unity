using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Com_Item : UIBase
{
    public Transform targDrag;

    public configData data;
    public Image sourceImage;
    public Text nameText;

    void Start ()
    {
        targDrag = gameObject.transform.Find("Drag");

        sourceImage = gameObject.transform.Find("Drag/Image").GetComponent<Image>();
        nameText = gameObject.transform.Find("Drag/Text").GetComponent<Text>();

        SetEventTriggerListener(targDrag.gameObject).onBeginDrag = OnBeginDrag;
        SetEventTriggerListener(targDrag.gameObject).onDrag = OnDrag;
        SetEventTriggerListener(targDrag.gameObject).onPointerUp = OnPointerUp;

        SetIcon();
        SetText();
    }

    public void SetIcon()
    {
        SetSprite(sourceImage, data.source);
    }

    public void SetText()
    {
        SetName(nameText, data.name);
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
            BagData.Instance.curItemDict[data.id.ToString()].count -= 1;
            Destroy(gameObject);
        }
        else
        {
            targDrag.localPosition = Vector3.zero;
        }
    }
}
