using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        QualitySettings.shadowDistance = 10;
        SetCameraToMainMenu();
    }

    public void PlayButtonClicked()
    {
        Invoke("PlayGameInTime", 1);
    }

    public void QuitButtonClicked()
    {
    }

    private void SetCameraToMainMenu()
    {
        CameraRigSetPosition.Instance.Relocate(transform.position,
                                       transform.rotation.eulerAngles.y);
    }

    private void PlayGameInTime()
    {
        SceneManager.LoadScene("Level1");
    }
}
