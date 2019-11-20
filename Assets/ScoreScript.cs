using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI finalScoreText = default;
    [SerializeField] private TextMeshProUGUI shotsFiredText = default;
    [SerializeField] private TextMeshProUGUI numberOfHitsText = default;
    [SerializeField] private TextMeshProUGUI hitMissRatioText = default;
    [SerializeField] private TextMeshProUGUI collectedCollectibles = default;

    [SerializeField] TextMesh CollectibleText = default;

    private int finalScore = 0;
    private int shotsFired = 0;
    private int numberOfHits = 0;
    private int collectibleCount = 0;
    private int collectibles = 5;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        finalScoreText.text = string.Format("Final Score: {0}", finalScore);
        shotsFiredText.text = string.Format("Shots Fired: {0}", shotsFired);
        numberOfHitsText.text = string.Format("Number of hits: {0}", numberOfHits);
        hitMissRatioText.text = string.Format("Hit-miss ratio: {0} %", System.Math.Round(((double)numberOfHits / shotsFired), 2) * 100);
        collectedCollectibles.text = string.Format("{0} / 5", collectibleCount);
    }

    public void AddFinalScore(int newScoreValue)
    {
        finalScore += newScoreValue;
        UpdateScoreText();
    }

    public void AddShotsFired()
    {
        shotsFired += 1;
    }

    public void AddNumberOfHits()
    {
        numberOfHits += 1;
    }

    public void AddCollectiblesCount()
    {
        collectibleCount += 1;
    }

    public void showCollectibleCount()
    {
        CollectibleText.text = collectibleCount.ToString() + " / 5";
    }
}
