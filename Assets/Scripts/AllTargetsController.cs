using UnityEngine;

public class AllTargetsController : MonoBehaviour
{
    public static AllTargetsController Instance;

    public GameObject ActiveTargets;

    void Awake()
    {
        Instance = this;
    }

    public void AddToActive(GameObject instance)
    {
        instance.transform.SetParent(ActiveTargets.transform);
    }
}
