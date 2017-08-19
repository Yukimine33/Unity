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

    public Button addButton1; //添加物体1按钮
    public Button addButton2; //添加物体2按钮
    public Button addButton3; //添加物体3按钮
    public Button addButton4; //添加物体4按钮

    public Button deleteButton; //删除物体按钮

    public Button exitButton; //退出按钮

    public GameObject movePoint;
    int currentItemCount = 0;
    int count = 0; //用来记录已生成的Com_Item的数量

    public string itemID;

    void Start ()
    {
        //获取Com_Item
        item = Resources.Load("Prefabs/Com_Item") as GameObject;

        rectTran = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<RectTransform>();
        grid = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content").GetComponent<GridLayoutGroup>();
        movePoint = gameObject.transform.Find("Canvas/Tran_DeleteArea/Tran_MoveItem").gameObject;

        addButton1 = gameObject.transform.Find("Canvas/Tran_AddButtons/Btn_AddItem1").GetComponent<Button>();
        addButton2 = gameObject.transform.Find("Canvas/Tran_AddButtons/Btn_AddItem2").GetComponent<Button>();
        addButton3 = gameObject.transform.Find("Canvas/Tran_AddButtons/Btn_AddItem3").GetComponent<Button>();
        addButton4 = gameObject.transform.Find("Canvas/Tran_AddButtons/Btn_AddItem4").GetComponent<Button>();

        deleteButton = gameObject.transform.Find("Canvas/Btn_DeleteItem").GetComponent<Button>();

        exitButton = gameObject.transform.Find("Canvas/Btn_Exit").GetComponent<Button>();

        SetEventTriggerListener(addButton1.gameObject).onClick += AddItem;
        SetEventTriggerListener(addButton2.gameObject).onClick += AddItem;
        SetEventTriggerListener(addButton3.gameObject).onClick += AddItem;
        SetEventTriggerListener(addButton4.gameObject).onClick += AddItem;

        SetEventTriggerListener(deleteButton.gameObject).onClick = DeleteItem;

        SetEventTriggerListener(exitButton.gameObject).onClick = Exit;

        InitBagConfig();
    }

    /// <summary>
    /// 配置背包物品数据
    /// </summary>
    public void InitBagConfig()
    {
        foreach(var pair in BagData.Instance.curItemDict)
        {
            for (int i = 0; i <= pair.Value.count; i++)
            {
                var item = CreateItem();
                item.data = pair.Value;
            }
        }
    }


    public Com_Item CreateItem()
    {
        var com_Item = UIManager.Instance.CreateComUI<Com_Item>(rectTran);

        //currentItemCount += 1;

        count += 1;
        ChangeHeight();
        return com_Item;
    }

    /// <summary>
    /// Add one item to a bag
    /// </summary>
    public void AddItem(GameObject obj)
    {
        switch(obj.name)
        {
            case "Btn_AddItem1":
                itemID = "1001";
                break;
            case "Btn_AddItem2":
                itemID = "1002";
                break;
            case "Btn_AddItem3":
                itemID = "1003";
                break;
            case "Btn_AddItem4":
                itemID = "1004";
                break;
        }
        
        var item = CreateItem();
        item.data = BagData.Instance.curItemDict[itemID];
        BagData.Instance.curItemDict[itemID].count += 1;
    }

    /// <summary>
    /// Delete the last item in a bag
    /// </summary>
    public void DeleteItem(GameObject obj)
    {
        var items = gameObject.transform.Find("Canvas/Scrl_Bag/Viewport/Content");

        if (items.childCount > 0)
        {
            var deleteItem = items.GetChild(items.childCount - 1).gameObject;
            var deleteName = deleteItem.transform.Find("Drag/Text").GetComponent<Text>().text;
            string deleteId = "";

            //获取要删除物体的id
            switch(deleteName)
            {
                case "Book":
                    deleteId = "1001";
                    break;
                case "Cloth":
                    deleteId = "1002";
                    break;
                case "Hat":
                    deleteId = "1003";
                    break;
                case "Shoes":
                    deleteId = "1004";
                    break;
            }

            Destroy(deleteItem);
            BagData.Instance.curItemDict[deleteId].count -= 1;
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

    public void Exit(GameObject obj)
    {
        UIManager.Instance.ClosePageUI<Page_Bag>();
    }
}
