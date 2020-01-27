using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private int scoreValue;
    public Text score;

    void Start()
    {
        scoreValue = PlayerPrefs.GetInt("ScoreValue", scoreValue);
        score.text = "Score: " + scoreValue.ToString();
    }
}
