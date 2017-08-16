using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBag : MonoBehaviour
{
    public GameObject getBagPage;
    public Button getBagButton;

    public Toggle bagToggleOne;
    public GameObject bag1;

    public Toggle bagToggleTwo;
    public GameObject bag2;

    void Start()
    {
        bag1 = UIManager.Instance.CreatePage("Page_Bag1");
        bag1.SetActive(false);
        bagToggleOne = gameObject.transform.Find("Tran_GetBags/Tog_GetBag1").GetComponent<Toggle>();
        bagToggleOne.onValueChanged.AddListener(OnActiveBagOne);

        bag2 = UIManager.Instance.CreatePage("Page_Bag2");
        bag2.SetActive(false);
        bagToggleTwo = gameObject.transform.Find("Tran_GetBags/Tog_GetBag2").GetComponent<Toggle>();
        bagToggleTwo.onValueChanged.AddListener(OnActiveBagTwo);

        getBagButton = gameObject.transform.Find("Btn_GetBag").GetComponent<Button>();
        getBagPage = UIManager.Instance.CreatePage("Page_Bag");
        getBagPage.AddComponent<PageBag>();
        getBagPage.SetActive(false);
        getBagButton.onClick.AddListener(ActiveBagUI);
    }

    /// <summary>
    /// 点击按钮后激活背包界面
    /// </summary>
    void ActiveBagUI()
    {
        getBagPage.SetActive(true);
    }

    void OnActiveBagOne(bool check)
    {
        bag1.SetActive(check);
    }

    void OnActiveBagTwo(bool check)
    {
        bag2.SetActive(check);
    }
}
