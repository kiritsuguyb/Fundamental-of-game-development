using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionTest : MonoBehaviour
{
    public float force;
    public float friction;

    public GameObject other;
    public GameObject right;
    public GameObject left;
    public GameObject top;
    public GameObject bottom;
    //上一帧结束时的速度
    [SerializeField]
    private Vector3 preV;
    [SerializeField]
    private float FinalF;
    [SerializeField]
    private Vector3 deltaD;
    [SerializeField]
    private float T;

    void Start()
    {
        preV = Vector3.zero;
    }

    void Update()
    {
        //先计算合力
            //摩擦系数就等于friction，由于demo中不考虑接触力产生的摩擦力
            //摩擦力视为常量，其大小由friction系数控制,方向为前一时刻速度的反方向
            //摩擦力不能导致反向运动，那么限制摩擦力的模长为之前速度的模长
        var fric = preV.magnitude > friction*Time.deltaTime ? friction : preV.magnitude/Time.deltaTime;
        Vector3 frictionForce = fric * -preV.normalized;
        

            //用户输入力

                //计算用户用力方向
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 fDir = new Vector3(moveHorizontal, 0.0f, moveVertical);
        fDir.Normalize();
        //合力
        Vector3 finalForce = frictionForce+force*fDir;
        FinalF = finalForce.magnitude;
        //计算合力加速度
        Vector3 acceleration = finalForce*Time.deltaTime;

        Vector3 prePos = transform.position;
        //应用加速度
        Vector3 curV = preV +acceleration;
        //算一个位移变量吧，后面做连续碰撞检测好用
        Vector3 deltaDist = curV * Time.deltaTime;
        deltaD = deltaDist;
        transform.Translate(deltaDist);
        preV = curV;


        //检测是否与其他球相撞
        Vector3 pos = transform.position;
        if (other != null)
        {
            OtherSphere otherSphere = other.GetComponent<OtherSphere>();
            Vector3 otherPos = other.transform.position;

            //球体间碰撞检测，判断球心距离与两球半径之和即可
            if (Vector3.Distance(pos, otherPos) < 0.5 + otherSphere.radius) //简单起见，认为自己的半径为0.5
            {
                Debug.Log("碰撞发生!");
                Vector3 v1 = preV;
                float m1 = 1.0f; // 简单起见，认为自己的质量为1
                Vector3 v2 = otherSphere.currentV;
                float m2 = otherSphere.mass;

                preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
                //Vector3 normal = prePos - otherPos;
                //normal.y = 0;
                //preV = preV.magnitude*Vector3.Reflect(-preV, normal).normalized;
                otherSphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);
                //otherSphere.currentV = -otherSphere.currentV.magnitude*Vector3.Reflect(otherSphere.currentV, normal).normalized;

                //如果有碰撞，位置回退，防止穿透
                transform.position = prePos;
            }
        }
        //墙体碰撞检测
        if (right == null|| left == null||top == null|| bottom == null)
        {
            Debug.LogError("CollisionTest::Please do assign the wall objects references.");
        }
        float xmin = left.transform.position.x+left.GetComponent<BoxCollider>().size.x / 2+0.5f;//考虑墙体位置、墙体宽度、球体半径，其中球体半径为0.5f
        float xmax = right.transform.position.x- right.GetComponent<BoxCollider>().size.x/2-0.5f;
        float zmin = bottom.transform.position.z+bottom.GetComponent<BoxCollider>().size.z / 2+0.5f;
        float zmax = top.transform.position.z-top.GetComponent<BoxCollider>().size.z / 2-0.5f;
        //参数方程，求t最大值的最小值
        float t;
        Vector3 vfactor=new Vector3(-1,0,1);
        //如果x大于零，那么运动朝向右边的墙，t在x方向上的最大值存在于右边
        //设置阈值为-0.00001，避免初始状态下deltaDist==0产生的bug
        if (deltaDist.x >= -0.00001)
        {
            t =(xmax - prePos.x) / deltaDist.x;
        }//x小于0，则朝向左边
        else
        {
            t= (xmin - prePos.x) / deltaDist.x;
        }
        //同上，考虑上下两边，求最大值的最小值
        if (deltaDist.z >= -0.00001)
        {
            var temp = (zmax - prePos.z) / deltaDist.z;
            if (t > temp)
            {
                vfactor =new Vector3(1, 0, -1);
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
        T = t;
        if (t < 1.00001&&t>0.00001)
        {
            Debug.Log("碰撞墙面！！！");
            preV.x *= vfactor.x;
            preV.z *= vfactor.z;
            transform.position = prePos + t * deltaDist;
        }

    }
}
