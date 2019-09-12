using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("HelicopterHover");
    }
}
