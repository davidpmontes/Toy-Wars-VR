using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance { get; private set; }

    [SerializeField] private TextMesh finalScoreText = default;
    [SerializeField] private TextMesh collectiblesPercentage = default;
    [SerializeField] private TextMesh basePercentage = default;

    [SerializeField] private TextMesh currentCollectedCollectibles = default;

    private int finalScore = 0;
    private float collectibleCount = 0;
    private float collectibleTimer;
    private float COLLECTIBLE_COUNT_DURATION = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void UpdateScoreText()
    {
        finalScoreText.text = string.Format("Final Score: {0}", finalScore);
        collectiblesPercentage.text = string.Format("Collectibles Found %: {0}", collectibleCount / 5);
        basePercentage.text = string.Format("Remaining Base %: {0}", 100);
    }

    public float GetCollectibleCountDuration()
    {
        return COLLECTIBLE_COUNT_DURATION;
    }

    public void AddFinalScore(int newScoreValue)
    {
        finalScore += newScoreValue;
        UpdateScoreText();
    }

    public void AddCollectiblesCount()
    {
        collectibleCount += 1;
        currentCollectedCollectibles.text = string.Format("{0}", collectibleCount);

        CancelInvoke("HideCollectiblesCount");
        currentCollectedCollectibles.gameObject.SetActive(true);
        Invoke("HideCollectiblesCount", COLLECTIBLE_COUNT_DURATION);
    }

    private void HideCollectiblesCount()
    {
        currentCollectedCollectibles.gameObject.SetActive(false);
    }
}
