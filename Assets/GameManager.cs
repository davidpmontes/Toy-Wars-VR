using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject[] entities;

    private int currIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActivateEntity();
    }

    public void ActivateEntity()
    {
        entities[currIndex].SetActive(true);
        currIndex++;
    }
}
