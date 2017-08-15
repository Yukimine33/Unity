using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBagUI : MonoBehaviour
{
    public GameObject getBagPage;
    public Button getBagButton;

    void Start()
    {
        getBagButton = gameObject.transform.Find("Btn_GetBag").GetComponent<Button>();
        getBagPage = UIManager.Instance.CreatePage("Page_Bag");
        getBagPage.AddComponent<ScrollViewControl>();
        getBagPage.SetActive(false);
        getBagButton.onClick.AddListener(GetBagUI);
    }

    /// <summary>
    /// 点击按钮后激活背包界面
    /// </summary>
    void GetBagUI()
    {
        getBagPage.SetActive(true);
    }
}
