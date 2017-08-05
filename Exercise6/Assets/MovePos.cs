using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePos : MonoBehaviour
{
    public Transform cube;
    public Camera originCamera;
    public float moveSpeed;
    public float rotateSpeed;
    
    
    bool isRotate = false;
    bool ifRotateFinish =false;
    bool ifOneStepFinish; //获取一次旋转加移动是否完成
    Vector3 oldPos; //旧的点击点
    Vector3 newPos; //鼠标点击点
    Vector3 targetDirc; //移动方向

    List<Vector3> newDirection; //物体移动方向的集合
    List<Vector3> newPositions; //鼠标点击点的集合

    void Start()
    {
        newDirection = new List<Vector3>();
        newPositions = new List<Vector3>();
        newPos = cube.position;
    }

    void Update ()
    {
        Debug.DrawRay(cube.position, cube.forward * 5, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            GetPos();
            isRotate = true; 
            newPositions.Add(newPos); //每点击一次鼠标就添加下一个要移动到的点
            newDirection.Add(targetDirc); //每点击一次鼠标就添加下一次要移动的方向
        }

        Debug.Log(Vector3.Dot(cube.forward, targetDirc.normalized));

        if (newDirection.Count != 0 && newPositions.Count != 0)
        {
            if (Vector3.Dot(cube.forward, newDirection[0].normalized) < 0.99999f)
            {
                if (isRotate) //点击鼠标才可以调用旋转函数
                {
                    //Rotate1();
                    Rotate2(newDirection[0]); //将移动方向列表中的第一项传进旋转函数中
                }
            }
            else
            {
                Move(newPositions[0]); //将移动目标列表中的第一项传进移动函数中
            }
        }
    }

    /// <summary>
    /// 获取鼠标点击点的坐标
    /// </summary>
    void GetPos()
    {
        oldPos = newPos;
        RaycastHit hit = new RaycastHit();
        Ray ray = originCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 100);
        if (hit.transform != null && hit.collider.gameObject.tag == "Plane")
        {
            newPos = hit.point;
            newPos.y = 1;
        }
        targetDirc = newPos - oldPos;
        Debug.DrawLine(oldPos, newPos, Color.blue, 10);
        
    }

    //物体移动
    void Move(Vector3 targetPos)
    {
        var dirc = targetPos - cube.position; //本次移动的终点与物体坐标的差向量
        if (dirc.magnitude > 0.1f)
        {
            cube.position += dirc.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            newDirection.RemoveAt(0); //本次旋转完毕后删除相关列表第一项
            newPositions.RemoveAt(0); //本次移动完毕后删除相关列表第一项
        }
    }
    
    //cos旋转
    void Rotate1()
    {
        var cos = Vector3.Dot(cube.forward, targetDirc.normalized);
        cos = Mathf.Min(cos, 1);
        cos = Mathf.Max(cos, -1); //限定cos的取值范围，以免出现溢值

        float rot = Mathf.Acos(cos);
        if (rot > 0.1f)
        {
            rot = 0.1f;
        }

        var rotateAngel = Mathf.Rad2Deg * rot;

        Debug.Log(rotateAngel);

        var directionAngel = Vector3.Dot(cube.right, targetDirc.normalized);
        
        
        if (directionAngel >= 0)
        {
            cube.Rotate(cube.up, rotateAngel * rotateSpeed * Time.deltaTime);
        }
        else
        {
            cube.Rotate(cube.up, -rotateAngel * rotateSpeed * Time.deltaTime);
        }
        
    }

    //插值旋转
    void Rotate2(Vector3 target)
    {
        Quaternion rotateAngel = new Quaternion();
        rotateAngel.SetLookRotation(target);

        cube.rotation = Quaternion.Lerp(cube.rotation, rotateAngel, 0.1f);
    }
}
