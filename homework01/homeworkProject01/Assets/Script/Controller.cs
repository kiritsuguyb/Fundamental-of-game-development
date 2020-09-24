using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rigidbody;
    public float controllSpeed = 20;
    public Transform target;
    Vector3 dir;
    public delegate void HandlePlayerInput();
    HandlePlayerInput handleInput;
    // Start is called before the first frame update
    PlayerNO player;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError("require rigidbody for the charactor");
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponentInParent<WhichPlayer>().player;
        if (player == PlayerNO.PlayerA) handleInput = handleAInput;
        else handleInput = handleBInput;
        handleInput();
        dir = target.position - transform.position;
        dir.z = 0;
        dir = dir.normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,dir);
       transform.rotation= rotation;

    }
    void handleAInput()
    {
        rigidbody.velocity += dir * (target.position - transform.position).sqrMagnitude * Time.deltaTime * controllSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.right;
        }
        //if (transform.position.y >= 0f) return;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.up * 1.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.down * 1.5f;
        }
    }
    void handleBInput()
    {
        rigidbody.velocity += dir * (target.position - transform.position).sqrMagnitude * Time.deltaTime * controllSpeed;
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.right;
        }
        //if (transform.position.y >= 0f) return;
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.up * 1.5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.down * 1.5f;
        }
    }
}
