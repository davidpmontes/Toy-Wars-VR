using UnityEngine;

public class PlayerTankTracks : MonoBehaviour
{
    void Update()
    {
        RotateTracksToCamera();
    }

    private void RotateTracksToCamera()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, Camera.main.transform.rotation.eulerAngles.y, 0));
    }
}
