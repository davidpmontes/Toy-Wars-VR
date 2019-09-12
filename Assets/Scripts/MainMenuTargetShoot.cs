using UnityEngine;
using System;

public class MainMenuTargetShoot : MonoBehaviour
{
    private bool select;
    
    void Update()
    {
        GetInput();
        ProcessInput();
    }

    void GetInput()
    {
        select = InputController.Button4();
    }

    void ProcessInput()
    {
        if (select)
        {
            MainMenu.Instance.MenuClick(transform.position);
        }
    }
}
