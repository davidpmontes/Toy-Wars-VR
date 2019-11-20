using UnityEngine;

public class TrainCar : MonoBehaviour, ICollectible
{
    [SerializeField] private GameObject train_engine = default;

    public void Shot()
    {
        train_engine.GetComponent<Train>().Shot();
    }
}
