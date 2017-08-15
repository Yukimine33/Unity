using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; //静态单例
    public GameObject UIRoot;
    public GameObject targetPage;

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
        UIRoot = Resources.Load("UIRoot") as GameObject;
        if (UIRoot != null)
        {
            UIRoot = Instantiate(UIRoot);
            UIRoot.name = "UIRoot";

            DontDestroyOnLoad(UIRoot);
        }
    }

    public GameObject CreatePage(string name)
    {
        targetPage = Resources.Load(name) as GameObject;
        if(targetPage == null)
        {
            Debug.LogError("Error: no " + name + " exist");
            return null;
        }

        var targetPage_clone = Instantiate(targetPage, UIRoot.transform);
        targetPage_clone.name = name;

        targetPage_clone.transform.SetParent(UIRoot.transform);
        targetPage_clone.transform.localScale = Vector3.one;
        return targetPage_clone;
    }
}
