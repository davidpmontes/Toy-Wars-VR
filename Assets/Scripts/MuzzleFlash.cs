using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    Light thelight;

    private void Awake()
    {
        thelight = GetComponent<Light>();
    }

    public void TurnOnLight()
    {
        thelight.intensity = 30;
        Invoke("TurnOffLight", 0.1f);
    }

    public void TurnOffLight()
    {
        thelight.intensity = 0;
    }
}
