using UnityEngine;

public class InitializeInOrder : MonoBehaviour
{
    [SerializeField] private GameObject[] orderedGameObjects = default;

    public void InitializeObjects()
    {
        foreach (GameObject go in orderedGameObjects)
        {
            go.SetActive(true);
        }
    }
}
