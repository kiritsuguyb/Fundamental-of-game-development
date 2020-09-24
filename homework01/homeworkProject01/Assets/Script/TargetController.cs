using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    Rigidbody rigidbody;
    public float controllSpeed =20;
    public int score;
    public delegate bool HandlePlayerInput();
    HandlePlayerInput handleInput;
    PlayerNO player;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("require rigidbody for the charactor");
        }
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponentInParent<WhichPlayer>().player;
        if (player == PlayerNO.PlayerA) handleInput = handleAInput;
        else handleInput = handleBInput;
        if (!handleInput()) rigidbody.velocity *= 0.9f;

    }
    bool handleAInput()
    {
        bool ret = false;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.velocity = controllSpeed  * Vector3.left;
            ret = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity = controllSpeed * Vector3.right;
            ret = true;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.velocity = controllSpeed  * Vector3.up * 1.5f;
            ret = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.velocity = controllSpeed * Vector3.down * 1.5f;
            ret = true;
        }
        return ret;
    }
    bool handleBInput()
    {
        bool ret = false;
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = controllSpeed * Vector3.left;
            ret = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = controllSpeed * Vector3.right;
            ret = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity = controllSpeed * Vector3.up * 1.5f;
            ret = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity = controllSpeed * Vector3.down * 1.5f;
            ret = true;
        }
        return ret;
    }
}
