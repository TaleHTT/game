using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 附在maincamera上的工具类
/// </summary>
public class Utils : MonoBehaviour
{
    static public Vector3 buttomLeft;
    static public Vector3 topRight;

    private void Awake()
    {
        GetCameraBounds();
    }

    static private Bounds _camBounds;

    static public Bounds camBounds
    {
        get
        {
            //未设置 _camBounds 变量
            if (_camBounds.size == Vector3.zero)
            {
                //使用默认摄像机设置调用 SetCameraBounds()
                GetCameraBounds();
            }
            return _camBounds;
        }
    }

    /// <summary>
    /// 通过获取camera视野范围,将左下和右上边界赋值到 buttomLeft 和 topRight中
    /// </summary>
    /// <returns></returns>
    static public void GetCameraBounds(Camera cam = null)
   {
        if (cam == null) cam = Camera.main;

        Vector3 sButtomLeft = Vector3.zero;
        Vector3 sTopRight = new Vector3(Screen.width, Screen.height, 0);

        Vector3 wButtomLeft = cam.ScreenToWorldPoint(sButtomLeft);
        Vector3 wTopRight = cam.ScreenToWorldPoint(sTopRight);

        wButtomLeft.z = 0;
        wTopRight.z = 0;

        Vector3 center = (wButtomLeft + wTopRight) / 2f;
        _camBounds = new Bounds(center, Vector3.zero);

        _camBounds.Encapsulate(wButtomLeft);
        _camBounds.Encapsulate(wTopRight);

        buttomLeft = new Vector3(_camBounds.center.x - _camBounds.extents.x, _camBounds.center.y - _camBounds.extents.y, 0);
        topRight = new Vector3(_camBounds.center.x + _camBounds.extents.x, _camBounds.center.y + _camBounds.extents.y, 0);
        /*print("buttomLeft " + buttomLeft);
        print("topRight " + topRight);*/
    }

    



    /// <summary>
    /// 打印精灵渲染器的边界
    /// </summary>
    /// <param name="go"></param>
    static public void PrintSpriteRendererBounds(GameObject go)
    {
        print("this is "+go.name+"'s Bounds:  "+go.GetComponent<SpriteRenderer>().bounds);
    }

    /// <summary>
    /// 将一切超出屏幕范围的摧毁
    /// </summary>
    static public void OutScreenDestroy(GameObject go)
    {
        float limit = 5f;
        if(go.transform.position.x <= buttomLeft.x - limit) 
        {
            Destroy(go);
            Debug.Log("GameObject " + go.name + " destroyed by method 'OutScreenDestroy'");
        }
        else if(go.transform.position.x >= topRight.x + limit)
        {
            Destroy(go);
            Debug.Log("GameObject " + go.name + " destroyed by method 'OutScreenDestroy'");
        } 
        else if(go.transform.position.y >= topRight.y + limit)
        {
            Destroy(go);
            Debug.Log("GameObject " + go.name + " destroyed by method 'OutScreenDestroy'");
        } 
        else if(go.transform.position.y <= buttomLeft.y - limit)
        {
            Destroy(go);
            Debug.Log("GameObject " + go.name + " destroyed by method 'OutScreenDestroy'");
        }
    }
}
