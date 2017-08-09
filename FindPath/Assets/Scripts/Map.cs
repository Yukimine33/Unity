using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class Map : MonoBehaviour
{
    public Transform parent;
    public Transform startAndEnd;
    public Transform searchItem;

    SearchData[,] searchMap; //用来记录寻路步数的数组
    int[,]map; //用来记录地图及地图中墙壁的数组
    List<Pos> posList; //用来记录寻步时的坐标
    int length = 20;
    int width = 20;
    Pos start;
    Pos end;

    void Start()
    {
        map = new int[length , width];
        searchMap = new SearchData[length, width];
        posList = new List<Pos>();

        ResetSearchMap();
        ReadMapFile();
        StartCoroutine(DrawWall()); //协程
        SetStartAndEnd();
        posList.Add(start);
        searchMap[start.x, start.z] = new SearchData(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { SearchPoint(posList[0]); }
    }

    /// <summary>
    /// 用迭代器来动态显示墙壁的生成过程
    /// </summary>
    /// <returns></returns>
    IEnumerator DrawWall() 
    {
        for(int i = 0;i < length;i++)
        {
            for(int j = 0;j < width;j++)
            {
                if(map[i,j] == 1)
                {
                    Instantiate(parent, new Vector3((i), parent.position.y, j), parent.rotation);
                    yield return 0;
                    //yield return new WaitForSeconds(0.1f); //每执行到此处则跳出，WaitForSeconds用来调整等待时间，然后继续执行
                }
            }
        }
        yield return null; //结束迭代器
    }

    /// <summary>
    /// 初始化search map，使每一格的step均为-1
    /// </summary>
    void ResetSearchMap()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                searchMap[i, j] = new SearchData(-1);
            }
        }
    }

    /// <summary>
    /// 设置起点和终点
    /// </summary>
    void SetStartAndEnd()
    {
        start = new Pos(2, 2);
        end = new Pos(1, 19);
        Instantiate(startAndEnd, new Vector3(start.x, startAndEnd.position.y, start.z), Quaternion.identity);
        Instantiate(startAndEnd, new Vector3(end.x, startAndEnd.position.y, end.z), Quaternion.identity);
    }

    /// <summary>
    /// 以pos为起点开始向四周搜索
    /// </summary>
    /// <param name="pos"></param>
    void SearchPoint(Pos pos)
    {
        int oldStep = searchMap[pos.x, pos.z].step; //传入的pos的step
        int newStep = oldStep + 1; //新的step
        Pos[] direction = new Pos[4];
        direction[0] = new Pos(0, 1); //向前
        direction[1] = new Pos(0, -1); //向后
        direction[2] = new Pos(1, 0); //向右
        direction[3] = new Pos(-1, 0); //向左

        for(int i = 0;i < direction.Length;i++)
        {
            Pos newPos = new Pos(pos.x + direction[i].x, pos.z + direction[i].z);
            if (newPos.x >= 0 && newPos.x <= length && newPos.z >= 0 && newPos.z <= width) //如果拓展出的点上没有墙且不是上一个点的话
            {
                if (map[newPos.x, newPos.z] == 0 && searchMap[newPos.x, newPos.z].step == -1)
                {
                    posList.Add(newPos); //posList加入新位置
                    searchMap[newPos.x, newPos.z] = new SearchData(newStep); //设置新步数
                    Instantiate(searchItem, new Vector3(newPos.x, searchItem.position.y, newPos.z), Quaternion.identity);
                    Debug.Log("posList.Count:" + posList.Count);
                    Debug.Log("newPos.x:" + newPos.x);
                    Debug.Log("newPos.z:" + newPos.z);
                }
            }
        }
        posList.RemoveAt(0);
    }

    

    public void ReadMapFile()
    {
        string path = Application.dataPath + "//" + "Map.txt";
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
                if (strReadline[x] == '*')
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

class Pos
{
    public int x;
    public int z;
    public Pos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

class SearchData
{
    public int step;
    public SearchData(int step)
    {
        this.step = step;
    }
}