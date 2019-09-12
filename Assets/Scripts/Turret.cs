using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Vector3 turretDirection;
    private float angle;
    private bool isTurning;
    private bool isPlaying;

    void Start()
    {
        isPlaying = false;
    }

    void Update()
    {
        isTurning = false;

        if (Input.GetKey("j"))
        {
            isTurning = true;
            angle--;
            if (angle <= 0)
                angle += 360;
            transform.localEulerAngles = new Vector3(0, angle, 0);
        }

        if (Input.GetKey("l"))
        {
            isTurning = true;
            angle++;
            if (angle >= 360)
                angle -= 360;
            transform.localEulerAngles = new Vector3(0, angle, 0);
        }

        if (isTurning && !isPlaying)
        {
            //AudioManager.Instance.Play("tank", "turretRotating");
            isPlaying = true;
        }
        else if (!isTurning && isPlaying)
        {
            //AudioManager.Instance.Stop("tank", "turretRotating");
            isPlaying = false;
        }
    }
}
