using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewControl : MonoBehaviour
{
    GameObject item;
    public RectTransform rectTran;
    public GridLayoutGroup grid;
    public Button clickButton;
    public Button deleteButton;
    public Button exitButton;
    int count = 0;

	void Start ()
    {
        item = Resources.Load("Con_Item") as GameObject;

        rectTran = gameObject.transform.Find("Canvas/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        grid = gameObject.transform.Find("Canvas/Scroll View/Viewport/Content").GetComponent<GridLayoutGroup>();
        
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
        var item_clone = Instantiate(item);
        item_clone.transform.SetParent(rectTran);
        item_clone.transform.localScale = Vector3.one;
        count += 1;

        ChangeHeight();
    }

    /// <summary>
    /// Delete the last item in a bag
    /// </summary>
    void DeleteItem()
    {
        var item_delete = gameObject.transform.Find("Canvas/Scroll View/Viewport/Content");
        if (item_delete.childCount > 0)
        { Destroy(item_delete.GetChild(item_delete.childCount - 1).gameObject); }
        count -= 1;

        ChangeHeight();
    }

    /// <summary>
    /// Change the height of Content
    /// </summary>
    void ChangeHeight()
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
