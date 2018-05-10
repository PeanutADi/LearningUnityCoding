using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder : MonoBehaviour
{
    public Text ScoreText;
    int Score = 0;
    void GetScore()
    {
        Score++;
    }
    // Use this for initialization  
    void Start()
    {
        Gate.addScore += GetScore;//订阅事件  
    }

    // Update is called once per frame  
    void Update()
    {

    }
}