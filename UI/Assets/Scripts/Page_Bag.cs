using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Page_Bag : UIBase
{
    GameObject item; //Com_Item
    public RectTransform rectTran;
    public GridLayoutGroup grid;

    public Button addButton; //添加物体按钮
    public Button deleteButton; //删除物体按钮
    public Button exitButton; //退出按钮

    public GameObject movePoint;
    int count = 0; //用来记录已生成的Com_Item的数量

    void Start ()
    {
        //获取Com_Item
        item = Resources.Load("Com_Item") as GameObject;

        rectTran = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<RectTransform>();
        grid = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<GridLayoutGroup>();
        movePoint = gameObject.transform.Find("Canvas/Tran_DeleteArea/Tran_MoveItem").gameObject;

        addButton = gameObject.transform.Find("Canvas/Btn_AddItem").GetComponent<Button>();
        deleteButton = gameObject.transform.Find("Canvas/Btn_DeleteItem").GetComponent<Button>();
        exitButton = gameObject.transform.Find("Canvas/Btn_Exit").GetComponent<Button>();

        SetEventTriggerListener(addButton.gameObject).onClick = AddItem;
        SetEventTriggerListener(deleteButton.gameObject).onClick = DeleteItem;
        SetEventTriggerListener(exitButton.gameObject).onClick = Exit;
    }

    /// <summary>
    /// Add one item to a bag
    /// </summary>
    void AddItem()
    {
        UIManager.Instance.CreateComUI<Com_Item>(rectTran);
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
        UIManager.Instance.ClosePageUI<Page_Bag>();
    }
}
