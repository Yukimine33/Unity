using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建UIManager并添加最开始的菜单界面
/// </summary>
public class Launcher : MonoBehaviour
{

	public void Awake ()
    {
        RegistryManager();
        InitConfig();
        CreateMenu();
    }

    /// <summary>
    /// 用来创建管理器并添加相关脚本
    /// </summary>
    public void RegistryManager()
    {
        GameObject manager = new GameObject("Manager");
        DontDestroyOnLoad(manager);
        manager.AddComponent<UIManager>();
        manager.AddComponent<ConfigBagData>();
    }

    /// <summary>
    /// 预加载相关资源
    /// </summary>
    public void InitConfig()
    {
        ConfigBagData.Instance.Init();
    }

    public void CreateMenu()
    {
        UIManager.Instance.CreatePageUI<Page_OpenBag>();
    }
}
