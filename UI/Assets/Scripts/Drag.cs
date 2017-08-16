using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
    public GameObject deleteArea;
    public Transform initParent;

	void Start ()
    {
        initParent = gameObject.transform.parent; //物体的父节点
        deleteArea = GameObject.Find("Img_DeleteArea");
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Img_DeleteArea")
        {
            Destroy(initParent.gameObject);
        }
        else
        {
            gameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
