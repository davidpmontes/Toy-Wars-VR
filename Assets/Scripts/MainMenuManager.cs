using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour, ILevelManager
{

    private AudioManager audioManager;
    [SerializeField] private AudioClip[] sound_effects = default;
    [SerializeField] AudioClip BGM_MainMenu = default;

    public static MainMenuManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        audioManager = AudioManager.GetAudioManager();
    }

    public void GetSoundEffects(out AudioClip[] fx)
    {
        fx = sound_effects;
    }

    void Start()
    {
        QualitySettings.shadowDistance = 10;
        SetCameraToMainMenu();
        AudioManager.Instance.ChangeBGM(BGM_MainMenu);
        AudioManager.Instance.StartBGM();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("TankTest");
        }
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
