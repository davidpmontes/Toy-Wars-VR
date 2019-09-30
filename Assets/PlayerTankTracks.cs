using UnityEngine;

public class PlayerTankTracks : MonoBehaviour
{
    void Update()
    {
        RotateTracksToCamera();
    }

    private void RotateTracksToCamera()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
    }
}
