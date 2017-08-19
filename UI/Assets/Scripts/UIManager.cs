using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //静态单例
    public GameObject UIRoot;

    private Dictionary<string, UIBase> uiPageDict = new Dictionary<string, UIBase>(); //建立存储page的字典

    void Awake ()
    {
        Instance = this;
        Init();
	}

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        UIRoot = Resources.Load("Prefabs/UIRoot") as GameObject;
        if (UIRoot != null)
        {
            UIRoot = Instantiate(UIRoot);
            UIRoot.name = "UIRoot";

            DontDestroyOnLoad(UIRoot);
        }
    }

    /// <summary>
    /// 获取page UI的类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPageUI<T>() where T : UIBase
    {
        string name = typeof(T).Name;
        if(!uiPageDict.ContainsKey(name))
        {
            Debug.LogError(name + " not found");
        }
        return uiPageDict[name] as T;
    }

    /// <summary>
    /// 创建page组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T CreatePageUI<T>() where T : UIBase
    {
        string name = typeof(T).Name;

        var targetPage = Resources.Load<GameObject>("Prefabs/" + name);

        if(targetPage == null)
        {
            Debug.LogError("Error: no " + name + " exist");
            return null;
        }

        targetPage = Instantiate(targetPage, UIRoot.transform);
        targetPage.name = name;

        targetPage.transform.localScale = Vector3.one;
        var menuScript = targetPage.AddComponent<T>();

        uiPageDict.Add(name, menuScript);

        return menuScript as T;
    }

    /// <summary>
    /// 创建com组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <returns></returns>
    public T CreateComUI<T>(Transform parent) where T : UIBase
    {
        string name = typeof(T).Name;

        var comUI = Resources.Load<GameObject>("Prefabs/" + name);
        if (comUI == null)
        {
            Debug.LogError("Error: no " + name + " exist");
            return null;
        }

        comUI = Instantiate(comUI, parent);
        comUI.name = name;
        comUI.transform.localScale = Vector3.one;

        var script = comUI.AddComponent<T>();

        return script;
    }

    /// <summary>
    /// 关闭page页面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ClosePageUI<T>()
    {
        var name = typeof(T).Name;
        Destroy(uiPageDict[name].gameObject);
        uiPageDict.Remove(name);
        ConfigBagData.Instance.RefreshExportJson(); //关闭背包页面时将更新后的数据输出到存档中
        //uiPageDict[name].gameObject.SetActive(false);
    }
}
