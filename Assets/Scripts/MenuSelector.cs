using UnityEngine;
using Valve.VR;

public class MenuSelector : MonoBehaviour
{
    public static MenuSelector Instance { get; private set; }

    public SteamVR_Action_Boolean selectMenu;

    private LayerMask layerMask;
    private string currentButtonName;
    private string buttonDownName;

    public Animator PlayButton;
    public Animator QuitButton;
    private AudioManager audioManager;

    private void Start()
    {
        currentButtonName = "None";
    }
    private void Awake()
    {
        Instance = this;
        audioManager = AudioManager.GetAudioManager();
    }

    void Update()
    {
        CastRay();
        CheckButtonClick();
    }

    private void CheckButtonClick()
    {
        if (selectMenu.stateDown)
        {
            if (currentButtonName == "PlayButton" || currentButtonName == "QuitButton")
            {
                buttonDownName = currentButtonName;
            }
        }

        if (selectMenu.stateUp)
        {
            if (buttonDownName == currentButtonName)
            {
                if (buttonDownName == "PlayButton")
                {
                    var explode = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
                    explode.transform.position = PlayButton.gameObject.transform.position;
                    explode.transform.localScale = Vector3.one * 0.5f;
                    explode.SetActive(true);
                    audioManager.PlayOneshot("explosion_large_01", PlayButton.transform.position);
                    PlayButton.gameObject.SetActive(false);
                    MainMenuManager.Instance.PlayButtonClicked();
                    enabled = false;
                }
                else if (buttonDownName == "QuitButton")
                {
                    var explode = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
                    explode.transform.position = QuitButton.gameObject.transform.position;
                    explode.transform.localScale = Vector3.one * 0.5f;
                    explode.SetActive(true);
                    audioManager.PlayOneshot("explosion_large_01", PlayButton.transform.position);
                    //QuitButton.gameObject.SetActive(false);
                    //MainMenuManager.Instance.QuitButtonClicked();
                    //enabled = false;
                }
            }
            buttonDownName = "None";
        }
    }

    private void CastRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1000))
        {
            var obj = hitInfo.collider.gameObject;

            if (obj.name == "PlayButton" || obj.name == "QuitButton")
            {
                currentButtonName = hitInfo.collider.gameObject.name;
                
                if (obj.name == "PlayButton")
                {
                    PlayButton.Play("Hover");
                }

                if (obj.name == "QuitButton")
                {
                    QuitButton.Play("Hover");
                }

                return;
            }
        }

        PlayButton.Play("Idle");
        QuitButton.Play("Idle");
        currentButtonName = "None";
    }
}
