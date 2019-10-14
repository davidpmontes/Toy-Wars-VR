using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class MenuSelector : MonoBehaviour
{
    public static MenuSelector Instance { get; private set; }

    public SteamVR_Action_Boolean selectMenu;

    private LayerMask layerMask;
    private string currentButtonName;
    private string buttonDownName;
    

    private void Start()
    {
        QualitySettings.shadowDistance = 10;
        currentButtonName = "None";
    }
    private void Awake()
    {
        Instance = this;
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
                    SceneManager.LoadScene("Level1");
                }
                else if (buttonDownName == "QuitButton")
                {
                    Debug.Log("Quit");
                }
            }
            buttonDownName = "None";
        }
    }

    private void CastRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 1000))
        {
            if (hitInfo.collider.gameObject.name == "PlayButton" || hitInfo.collider.gameObject.name == "QuitButton")
            {
                currentButtonName = hitInfo.collider.gameObject.name;
                return;
            }
        }

        currentButtonName = "None";
    }
}
