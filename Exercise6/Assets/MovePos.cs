using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePos : MonoBehaviour
{
    public Transform cube;
    public Camera originCamera;
    public float moveSpeed;
    public float rotateSpeed;
    RaycastHit hit = new RaycastHit();
    
    bool isRotate = false;
    bool ifOneStepFinish; //获取一次旋转加移动是否完成
    Vector3 oldPos; //旧的点击点
    Vector3 newPos; //鼠标点击点
    Vector3 targetDirc;

    List<Vector3> targetPos; //鼠标点击点的集合
    int step = 0; //记录列表的第几项

    private void Start()
    {
        targetPos = new List<Vector3>();
        newPos = cube.position;
    }

    void Update ()
    {
        var forward = cube.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(cube.position, forward, Color.green);
        if (Input.GetMouseButtonDown(0))
        {
            GetPos();
            isRotate = true;
            targetPos.Add(targetDirc);
        }

        foreach(var pair in targetPos)
        {
            Debug.Log(pair);
        }

        if(!ifOneStepFinish)
        {

            if (Vector3.Dot(cube.forward, targetPos[0].normalized) < 0.999f)
            {
                if (isRotate)
                {
                    //Rotate1();
                    Rotate2(targetPos[0]);
                }
            }
            else
            {
                Move();
            }
        }
        else
        {
            targetPos.RemoveAt(0);
            ifOneStepFinish = false;
        }
        Debug.Log(Vector3.Dot(cube.forward, targetPos[0].normalized));
        
    }

    void GetPos()
    {
        oldPos = newPos;
        Ray ray = originCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 100);
        if (hit.transform != null && hit.collider.gameObject.tag == "Plane")
        {
            newPos = hit.point;
            newPos.y = 1;
        }
        targetDirc = newPos - oldPos; 
    }

    void Move()
    {
        var dirc = newPos - cube.position;
        if (dirc.magnitude > 0.1f)
        {
            cube.position += dirc.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            ifOneStepFinish = true;
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
