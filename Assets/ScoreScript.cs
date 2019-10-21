using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    private int score;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScoreText();
    }
}
