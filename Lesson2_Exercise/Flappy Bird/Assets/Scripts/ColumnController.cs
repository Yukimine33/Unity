using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    public GameObject originalColumn;

    GameObject[] columns;
    int currentColumn = 0;
    Vector2 originalPos = new Vector2(-10, -20);
    int maxColumn = 6; //同时存在的障碍的最大数量

    float timeSinceLastCreate; //距离上一个障碍出现后的时间
    float createTime = 3.5f; //产生新障碍的时间间隔

    float yMin = -1f;
    float yMax = 5f;
    float xPos = 5f;

	void Start ()
    {
        columns = new GameObject[maxColumn];

        for(int i = 0; i < maxColumn; i++)
        {
            columns[i] = Instantiate(originalColumn, originalPos, Quaternion.identity);
        }
	}
	
	void Update ()
    {
        timeSinceLastCreate += Time.deltaTime;

        //每隔一段时间间隔后刷新出新障碍
        if(GameMode.instance.gameOver == false && timeSinceLastCreate >= createTime) 
        {
            timeSinceLastCreate = 0f;

            float yPos = Random.Range(yMin, yMax);
            columns[currentColumn].transform.position = new Vector2(xPos, yPos);

            currentColumn += 1;

            if(currentColumn >= maxColumn)
            {
                currentColumn = 0;
            }
        }

        //new idea：可不可以在分数达到某个特定数值之后开始产生连续障碍？
	}
}
