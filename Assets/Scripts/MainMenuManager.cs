using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class MainMenuManager : MonoBehaviour, ILevelManager
{

    private AudioManager audioManager;
    [SerializeField] private AudioClip[] sound_effects = default;

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

    public void UpdateState()
    {

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
        audioManager.StopAll();
        audioManager.StopAllCoroutines();
    }
}
