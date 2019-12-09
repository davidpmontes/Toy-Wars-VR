using UnityEngine;

public class TrainCar : MonoBehaviour, ICollectible
{
    [SerializeField] private GameObject train_engine = default;

    public void Init()
    {
        train_engine.GetComponent<ICollectible>().Init();
    }
}
