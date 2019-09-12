using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEngine : MonoBehaviour
{
    readonly float lowIdleVolume = 0.1f;
    readonly float maxIncreaseVolume = 0.2f;

    readonly float lowIdlePitch = 1.0f;
    readonly float maxIncreasePitch = 0.6f;

    private AudioSource driving;

    void Start()
    {
        //driving = AudioManager.Instance.getSource("tank", "idling");
        driving.loop = true;
        driving.priority = 0;
        driving.volume = lowIdleVolume;
        driving.pitch = lowIdlePitch;
        //AudioManager.Instance.Play("tank", "idling");
    }

    void Update()
    {
        ChangeVolumePitch();
    }

    private void ChangeVolumePitch()
    {
        driving.volume = lowIdleVolume + maxIncreaseVolume * ((Mathf.Abs(InputController.Horizontal()) * 0.4f) + (Mathf.Abs(InputController.Vertical()) * 0.6f));
        driving.pitch = lowIdlePitch + maxIncreasePitch * ((Mathf.Abs(InputController.Horizontal()) * 0.4f) + (Mathf.Abs(InputController.Vertical()) * 0.6f));
    }
}
