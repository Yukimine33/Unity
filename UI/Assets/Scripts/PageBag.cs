using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageBag : MonoBehaviour
{
    GameObject item;
    public RectTransform rectTran;
    public GridLayoutGroup grid;
    public Button clickButton;
    public Button deleteButton;
    public Button exitButton;
    public Text itemCountText;
    int count = 0;
    int itemCount = 1;

	void Start ()
    {
        item = Resources.Load("Com_Item") as GameObject;

        rectTran = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<RectTransform>();
        grid = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<GridLayoutGroup>();

        clickButton = gameObject.transform.Find("Canvas/Btn_AddItem").GetComponent<Button>();
        deleteButton = gameObject.transform.Find("Canvas/Btn_DeleteItem").GetComponent<Button>();
        exitButton = gameObject.transform.Find("Canvas/Btn_Exit").GetComponent<Button>();

        clickButton.onClick.AddListener(AddItem);
        deleteButton.onClick.AddListener(DeleteItem);
        exitButton.onClick.AddListener(Exit);
    }

    /// <summary>
    /// Add one item to a bag
    /// </summary>
    void AddItem()
    {
        /*
        if (itemCount == 1)
        {
            item = Instantiate(item);
            item.transform.SetParent(rectTran);
            item.transform.localScale = Vector3.one;
            item.AddComponent<Drag>();
            count += 1;
        }

        itemCountText = item.transform.Find("Count").GetComponent<Text>();
        itemCountText.text = itemCount.ToString();
        itemCount += 1;

        if(itemCount > limitPerGrid)
        {
            itemCount = 1;
        }
        */

        var item_clone = Instantiate(item);
        item_clone.transform.SetParent(rectTran);
        item_clone.transform.localScale = Vector3.one;
        var item_child = item_clone.transform.Find("Item");
        item_child.gameObject.AddComponent<Drag>();
        count += 1;

        ChangeHeight();
    }

    /// <summary>
    /// Delete the last item in a bag
    /// </summary>
    void DeleteItem()
    {
        var items = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content");

        if (items.childCount > 0)
        {
            Destroy(items.GetChild(items.childCount - 1).gameObject);
            count -= 1;
        }

        ChangeHeight();
    }

    /// <summary>
    /// Change the height of Content
    /// </summary>
    public void ChangeHeight()
    {
        float itemHeight = grid.cellSize.y + grid.spacing.y;
        rectTran.sizeDelta = new Vector2(0, itemHeight * Mathf.Ceil((float)count / grid.constraintCount));
    }

    void Exit()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
