using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rigidbody;
    public float controllSpeed = 1;
    public Transform target;
    Vector3 dir;
    // Start is called before the first frame update
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
        handleInput();
        dir = target.position - transform.position;
        dir.z = 0;
        dir = dir.normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,dir);
       transform.rotation= rotation;

    }
    void handleInput()
    {
        rigidbody.velocity += dir * (target.position - transform.position).sqrMagnitude * Time.deltaTime * controllSpeed;
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.left;
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.right;
        //}
        //if (transform.position.y >= 0f) return;
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.up * 1.5f;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rigidbody.velocity += controllSpeed * Time.deltaTime * Vector3.down * 1.5f;
        //}
    }
}
