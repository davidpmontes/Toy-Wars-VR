using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    void Start()
    {
        QualitySettings.shadowDistance = 10;
        SetCameraToMainMenu();
    }

    void Update()
    {

    }

    private void SetCameraToMainMenu()
    {
        CameraRigSetPosition.Instance.Relocate(transform.position,
                                       transform.rotation.eulerAngles.y);
    }


}
