using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject spawnObject;
    float startTime;
    float lastTime;
    public int countDownTime = 100;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int countdown = countDownTime - (int)(Time.time - startTime);
        text.text = "Count down" + countdown.ToString();
        float randomf=Random.Range(-1, 1);
        if (Time.time - lastTime > 3 + randomf)
        {
            lastTime=Time.time;
            Vector3 randomPosition = new Vector3(Random.Range(-10, 5), Random.Range(3, 8), 3.5f);
            spawn(spawnObject, randomPosition);
        }
        
    }
    void spawn(GameObject obj, Vector3 pos) {
        var go=Instantiate(obj,pos,Quaternion.Euler(0,0,0));
        go.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
    }

}
