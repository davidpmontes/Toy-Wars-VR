using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuPointer : MonoBehaviour, ICameraRelocate
{
    [SerializeField] private Transform cameraPosition = default;
    [SerializeField] private GameObject NormalAimerHorizontal = default;
    [SerializeField] private GameObject NormalAimerVertical = default;

    private float horizontalValue = 0;
    private float verticalValue = 0;

    private void Update()
    {
        GetGamepadInput();
    }

    private void GetGamepadInput()
    {
        if (!enabled)
            return;

        var horizontal = Gamepad.current.leftStick.x.ReadValue();
        var vertical = Gamepad.current.leftStick.y.ReadValue();

        horizontalValue += horizontal * 70 * Time.deltaTime;
        verticalValue += vertical * 70 * Time.deltaTime;

        horizontalValue = Mathf.Clamp(horizontalValue, -90, 90);
        verticalValue = Mathf.Clamp(verticalValue, -90, 90);

        NormalAimerHorizontal.transform.localRotation = Quaternion.Euler(0, horizontalValue, 0);
        NormalAimerVertical.transform.localRotation = Quaternion.Euler(verticalValue, 0, 0);

        if (Gamepad.current.buttonSouth.isPressed)
        {
            //animator.SetBool("firing", true);
        }
        else
        {
            //animator.SetBool("firing", false);
        }
    }

    public Vector3 GetRelocatePosition()
    {
        return cameraPosition.position;
    }

    public float GetRelocateRotation()
    {
        return transform.rotation.eulerAngles.y;
    }
}
