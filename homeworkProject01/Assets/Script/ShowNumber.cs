using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNumber : MonoBehaviour
{
    public List<GameObject> objects;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = objects.Count.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("SpawnedObj"))
        {
            if (!objects.Contains(other.gameObject))
            {
                objects.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("SpawnedObj"))
        {
            if (objects.Contains(other.gameObject))
            {
                objects.Remove(other.gameObject);
            }
        }
    }

}
