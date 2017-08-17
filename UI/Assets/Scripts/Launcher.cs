using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建UIManager并添加最开始的菜单界面
/// </summary>
public class Launcher : MonoBehaviour
{
    public GameObject manager;

	public void Awake ()
    {
        manager = new GameObject("UIManager");
        DontDestroyOnLoad(manager);
        manager.AddComponent<UIManager>();

        var menuPage = UIManager.Instance.CreatePageUI<Page_OpenBag>();
    }
}
