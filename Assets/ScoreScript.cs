using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public int score;
    public static ScoreScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateScoreText()
    {
        scoreText.text = "Score " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScoreText();
    }
}
