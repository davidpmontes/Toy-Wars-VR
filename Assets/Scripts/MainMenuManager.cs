using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, ILevelManager
{
    public static MainMenuManager Instance { get; private set; }

    [SerializeField] private AudioClip[] sound_effects = default;
    [SerializeField] private AudioClip BGM_MainMenu = default;

    private void Awake()
    {
        Instance = this;
        GetComponent<InitializeInOrder>().InitializeObjects();
        PlayerManager.Instance.EnableVehicle(PlayerVehicles.MENU_SELECTOR);
        QualitySettings.shadowDistance = 10;
        SetCameraToMainMenu();
        AudioManager.Instance.ChangeBGM(BGM_MainMenu);
        AudioManager.Instance.StartBGM();
    }

    public void GetSoundEffects(out AudioClip[] fx)
    {
        fx = sound_effects;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Application.Quit();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene("MainMenu");
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SceneManager.LoadScene("TankTest");
        //}
    }
    public void PlayButtonClicked()
    {
        Invoke("PlayGameInTime", 1);
    }

    public void TankButtonClicked()
    {
        Invoke("PlayTankInTime", 1);
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

    private void PlayTankInTime()
    {
        SceneManager.LoadScene("TankTest");
    }

    public void UpdateState()
    {
    }
}
