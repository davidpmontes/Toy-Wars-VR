using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        TabletController.Instance.SetCameraToFollowObject(PlayerManager.Instance.CurrentVehicle().transform);
    }
}
