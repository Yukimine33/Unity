using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject manager;

	public void Awake ()
    {
        manager = new GameObject("UIManager");
        DontDestroyOnLoad(manager);
        manager.AddComponent<UIManager>();

        var menuPage = UIManager.Instance.CreatePage("Page_GetBag");
        menuPage.AddComponent<AddBagUI>();
    }
}
