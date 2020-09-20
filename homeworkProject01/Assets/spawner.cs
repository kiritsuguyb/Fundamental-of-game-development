using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject spawnObject;
    float startTime;
    float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float randomf=Random.Range(-1, 1);
        if (Time.time - lastTime > 4 + randomf)
        {
            lastTime=Time.time;
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), Random.Range(15, 25), 3.5f);
            spawn(spawnObject, randomPosition);
        }
        
    }
    void spawn(GameObject obj, Vector3 pos) {
        Instantiate(obj,pos,Quaternion.Euler(0,0,0));
    }

}
