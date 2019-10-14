using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



public class MainMenuManager : MonoBehaviour
{

    public SteamVR_Action_Boolean selectMenu;    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.shadowDistance = 10;
        SetCameraToMainMenu();
    }

    void Update()
    {
        if(selectMenu.stateDown == true)
        {
           if(menuSelector.Instance.GetMenu() == "PlayButton")
            {
                Debug.Log("PlayButton");
            }
        }
    }

    private void SetCameraToMainMenu()
    {
        CameraRigSetPosition.Instance.Relocate(transform.position,
                                       transform.rotation.eulerAngles.y);
    }


}
