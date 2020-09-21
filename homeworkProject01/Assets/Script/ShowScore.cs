using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public PlayerNO player;
    Score score;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        score = FindObjectOfType<Score>();
        if (player == PlayerNO.PlayerA)
            GetComponent<TextMeshProUGUI>().text = score.scoreA.ToString();
        else
            GetComponent<TextMeshProUGUI>().text = score.scoreB.ToString();
    }
}
