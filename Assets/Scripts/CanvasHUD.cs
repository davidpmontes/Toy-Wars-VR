using UnityEngine;

public class CanvasHUD : MonoBehaviour
{
    public static CanvasHUD Instance { get; private set; }

    void Awake()
    {
        Instance = this;   
    }
}
