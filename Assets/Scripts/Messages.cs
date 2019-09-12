using UnityEngine;

public class Messages : MonoBehaviour
{
    public static Messages Instance { get; private set; }
    [SerializeField] private RectTransform IncomingEnemyTMP;

    void Awake()
    {
        Instance = this;
    }

    public void IncomingEnemy()
    {
        IncomingEnemyTMP.GetComponent<Animator>().Play("NewMessage", 1, 0f);
    }
}
