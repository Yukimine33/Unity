using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用来存储UI的通用功能
/// </summary>
public class UIBase : MonoBehaviour
{
    public void SetSprite(Image image, string icon)
    {
        image.sprite = Resources.Load<Sprite>("Icons/" + icon);
    }

    public void SetName(Text text, string name)
    {
        text.text = name;
    }

    /// <summary>
    /// 获取EventTrigger的监听器
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
	public EventTriggerListener SetEventTriggerListener(GameObject obj)
    {
        var eventTriggerListener = obj.GetComponent<EventTriggerListener>();

        if(eventTriggerListener == null)
        {
            eventTriggerListener = obj.AddComponent<EventTriggerListener>();
        }

        return eventTriggerListener;
    }
}
