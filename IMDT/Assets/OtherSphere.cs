using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;
    public GameObject right;
    public GameObject left;
    public GameObject top;
    public GameObject bottom;

    [NonSerialized]
    public Vector3 currentV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //先计算合力
        //摩擦系数就等于friction，由于demo中不考虑接触力产生的摩擦力
        //摩擦力视为常量，其大小由friction系数控制,方向为前一时刻速度的反方向
        //摩擦力不能导致反向运动，那么限制摩擦力的模长为之前速度的模长
        var fric = currentV.magnitude > friction * Time.deltaTime ? friction : currentV.magnitude / Time.deltaTime;
        Vector3 frictionForce = fric * -currentV.normalized;

        //应用加速度
        Vector3 acceleration = frictionForce*Time.deltaTime;

        Vector3 prePos = transform.position;
        Vector3 curV = currentV + acceleration;
        Vector3 deltaDist = curV * Time.deltaTime;
        transform.Translate(deltaDist);
        currentV = curV;

        //墙体连续碰撞检测,基于AABB包围盒
        if (right == null || left == null || top == null || bottom == null)
        {
            Debug.LogError("CollisionTest::Please do assign the wall objects references.");
            return;
        }
        float xmin = left.transform.position.x + left.GetComponent<BoxCollider>().size.x / 2 + radius;
        float xmax = right.transform.position.x - right.GetComponent<BoxCollider>().size.x / 2 - radius;
        float zmin = bottom.transform.position.z + bottom.GetComponent<BoxCollider>().size.z / 2 + radius;
        float zmax = top.transform.position.z - top.GetComponent<BoxCollider>().size.z / 2 - radius;
        //参数方程，求t最大值的最小值
        float t;
        Vector3 vfactor = new Vector3(-1, 0, 1);
        //如果x大于零，那么运动朝向右边的墙，t在x方向上的最大值存在于右边
        if (deltaDist.x >= -0.00001)
        {
            t = (xmax - prePos.x) / deltaDist.x;
        }
        else//x小于0，则朝向左边
        {
            t = (xmin - prePos.x) / deltaDist.x;
        }
        //同上，考虑上下两边，求最大值的最小值
        if (deltaDist.z >= -0.00001)
        {
            var temp = (zmax - prePos.z) / deltaDist.z;
            if (t > temp)
            {
                vfactor = new Vector3(1, 0, -1);
                t = temp;
            }
        }
        else
        {
            var temp = (zmin - prePos.z) / deltaDist.z;
            if (t > temp)
            {
                vfactor = new Vector3(1, 0, -1);
                t = temp;
            }
        }
        if (t < 1.00001 && t > 0.00001)
        {
            Debug.Log("碰撞墙面！！！");
            currentV.x *= vfactor.x;
            currentV.z *= vfactor.z;
            transform.position = prePos + t * deltaDist;
        }
    }
}
