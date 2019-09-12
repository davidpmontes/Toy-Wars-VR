using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour
{
    void Start()
    {
        //FindObjectOfType<MainMenuController>().MainMenuExitingEvent += OnMainMenuExiting;
    }

    void OnMainMenuExiting()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }
}
