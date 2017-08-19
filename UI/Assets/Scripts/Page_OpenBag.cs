using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_OpenBag : UIBase
{
    public GameObject getBagPage;
    public Button getBagButton;

    void Start()
    {
        getBagButton = gameObject.transform.Find("Btn_GetBag").GetComponent<Button>(); //获取开启背包界面的按钮

        SetEventTriggerListener(getBagButton.gameObject).onClick = ActiveBagUI;
        //getBagPage = UIManager.Instance.CreatePageUI<Page_Bag>().gameObject;
        //getBagPage.SetActive(false);
        //getBagButton.onClick.AddListener(ActiveBagUI);
    }

    /// <summary>
    /// 点击按钮后激活背包界面
    /// </summary>
    void ActiveBagUI(GameObject obj)
    {
        UIManager.Instance.CreatePageUI<Page_Bag>();
        //getBagPage.SetActive(true);
    }
}
