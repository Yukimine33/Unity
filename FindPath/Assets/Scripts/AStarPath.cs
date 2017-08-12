using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

class APos
{
    public int x;
    public int z;
    
    public APos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

class ASearchData
{
    //public int step;
    public APos parent; //记录每一点的父节点
    public int G;
    public int H;
    public int F;
    public ASearchData(APos parent,int G,int H)
    {
        this.parent = parent;
        this.G = G;
        this.H = H;
        this.F = G + H;
    }
}

public class AStarPath : MonoBehaviour
{
    public Transform wall;
    public Transform wallParent;
    public Transform startAndEnd;
    public Transform searchItem;
    public Transform searchItemParent;
    public Transform findPathItem;
    public Transform findPathParent;
    public Camera mainCamera;

    int straightDistance = 10; // 直线距离
    int diagonalDistance = 14; //对角线距离
    List<APos> openList; //开启列表
    Stack<APos> closeList; //关闭列表

    ASearchData[,] searchMap; //用来记录每一个点的父节点的数组
    int[,] map; //用来记录地图及地图中墙壁的数组
    int length = 5; //地图长度
    int width = 6; //地图宽度
    APos start; //起点
    APos end; //终点
    APos[] direction; //用来存储八个方向
    int valueOfF; //用来存储某一点的F值
    APos nextPos; //下一个节点

    void Start ()
    {
        map = new int[length, width];
        searchMap = new ASearchData[length, width];
        direction = new APos[8];
        openList = new List<APos>();
        closeList = new Stack<APos>();

        ReadMapFile();
        DrawWall();
        SetSearchMap();
        SetDirection();
        start = new APos(0, 4);
        end = new APos(4, 3);
        Instantiate(startAndEnd, new Vector3(start.x, startAndEnd.position.y, start.z), Quaternion.identity);
        Instantiate(startAndEnd, new Vector3(end.x, startAndEnd.position.y, end.z), Quaternion.identity);
        nextPos = start;
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        { FindAStarPath(nextPos); }
    }

    void FindAStarPath(APos pos)
    {
        int minValueF = 999;

        closeList.Push(pos); //关闭列表加入pos

        for (int i = 0; i < direction.Length; i++)
        {
            APos newPos = new APos(pos.x + direction[i].x, pos.z + direction[i].z);
            //Debug.Log("newPos:" + newPos.x + "," + newPos.z);

            if (newPos.x >= 0 && newPos.x < length && newPos.z >= 0 && newPos.z < width) //拓展出的点应在地图范围内
            {
                if (newPos.x == end.x && newPos.z == end.z)
                {
                    return;
                }

                if (map[newPos.x, newPos.z] == 0 && !closeList.Contains(newPos)) //判断新拓展点是否在关闭列表中
                {
                    searchMap[newPos.x, newPos.z].G = CalculateValueOfG(pos, newPos);
                    searchMap[newPos.x, newPos.z].H = CalculateValueOfH(newPos, end);
                    searchMap[newPos.x, newPos.z].F = searchMap[newPos.x, newPos.z].G + searchMap[newPos.x, newPos.z].H;
                    valueOfF = searchMap[newPos.x, newPos.z].F;
                    searchMap[newPos.x, newPos.z].parent = pos; //将pos设为新拓展点的父节点

                    if (openList.Contains(newPos)) //如果open list中已经包含了newPos
                    {
                        var oldOpenPos = openList[openList.IndexOf(newPos)];
                        if (searchMap[oldOpenPos.x, oldOpenPos.z].F <= valueOfF) //如果该pos的F值小于新的F值的话，则将更小的F值赋给newPos并保持原来的父子关系
                        {
                            valueOfF = searchMap[oldOpenPos.x, oldOpenPos.z].F;
                            searchMap[newPos.x, newPos.z].parent = searchMap[oldOpenPos.x, oldOpenPos.z].parent;
                        }
                    }
                    else
                    {
                        openList.Add(newPos); //将新拓展的点加入open list中
                        var searchItem_clone = Instantiate(searchItem, new Vector3(newPos.x, searchItem.position.y, newPos.z), Quaternion.identity);
                        searchItem_clone.SetParent(searchItemParent);
                    }

                    //这里有问题！！！！
                    if (valueOfF <= minValueF) //如果F值小于设定的最小F值，则将新F值设为最小值
                    {
                        minValueF = valueOfF;
                        nextPos.x = newPos.x;
                        Debug.Log("Pos6:" + pos.x + "," + pos.z);
                        nextPos.z = newPos.z;
                        Debug.Log("Pos7:" + pos.x + "," + pos.z);
                    }
                }
            }
        }

        
        //var findPathItem_clone = Instantiate(findPathItem, new Vector3(pos.x, findPathItem.position.y, pos.z),Quaternion.identity);
        //findPathItem_clone.SetParent(findPathParent);

        openList.Remove(nextPos); //开放列表中删除下一个节点
    }

    int CalculateValueOfG(APos startPos,APos endPos)
    {
        var valueG = searchMap[startPos.x, startPos.z].G;
        var offSetX = Mathf.Abs(startPos.x - endPos.x); //两点的x的差值绝对值
        var offSetZ = Mathf.Abs(startPos.z - endPos.z); //两点的z的差值绝对值

        if (offSetX == 0 || offSetZ == 0)
        {
            valueG += straightDistance;
        }
        else
        {
            valueG += diagonalDistance;
        }
        return valueG;
    }

    /// <summary>
    /// 获取H值
    /// </summary>
    int CalculateValueOfH(APos startPos,APos endPos)
    {
        var valueH = searchMap[startPos.x, startPos.z].H;
        var offSetX = Mathf.Abs(startPos.x - endPos.x); //两点的x的差值绝对值
        var offSetZ = Mathf.Abs(startPos.z - endPos.z); //两点的z的差值绝对值
        var offSetXZ = Mathf.Abs(offSetX - offSetZ); //两点x和z的差值绝对值
        var maxOffSet = Mathf.Max(offSetX, offSetZ); //取差值绝对值中的最大值
        var minOffSet = Mathf.Min(offSetX, offSetZ); //取差值绝对值中的最小值

        if (offSetX == 0 || offSetZ == 0)
        {
            valueH = maxOffSet * straightDistance;
        }
        else
        {
            valueH = offSetXZ * straightDistance + minOffSet * diagonalDistance;
        }
        return valueH;
    }

    /// <summary>
    /// 设置方向
    /// </summary>
    void SetDirection()
    {
        direction[0] = new APos(0, 1); //前
        direction[1] = new APos(0, -1); //后
        direction[2] = new APos(1, 0); //右
        direction[3] = new APos(-1, 0); //左
        direction[4] = new APos(-1, 1); //前左
        direction[5] = new APos(1, 1); //前右
        direction[6] = new APos(-1, -1); //后左
        direction[7] = new APos(1, -1); //后右
    }

    /// <summary>
    /// 初始化search map
    /// </summary>
    void SetSearchMap()
    {
        for(int i = 0;i < length;i++)
        {
            for(int j = 0;j < width; j++)
            {
                searchMap[i, j] = new ASearchData(null, 0, 0);
            }
        }
    }

    /// <summary>
    /// 显示墙壁
    /// </summary>
    void DrawWall()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i, j] == 1)
                {
                    var wallCopy = Instantiate(wall, new Vector3((i), wall.position.y, j), wall.rotation);
                    wallCopy.transform.SetParent(wallParent);
                }
            }
        }
    }

    /// <summary>
    /// 读取地图
    /// </summary>
    public void ReadMapFile()
    {
        string path = Application.dataPath + "//" + "Test.txt";
        if (!File.Exists(path))
        {
            return;
        }

        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        StreamReader read = new StreamReader(fs, Encoding.Default);

        string strReadline;
        int y = 0;


        while ((strReadline = read.ReadLine()) != null)
        {
            for (int x = 0; x < strReadline.Length; ++x)
            {
                if (strReadline[x] == '*') //将地图中墙的部分设为1
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = 0;
                }
            }
            y += 1;
            // strReadline即为按照行读取的字符串
        }
    }
}
