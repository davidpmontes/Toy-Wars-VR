using UnityEngine;

public class menuSelector : MonoBehaviour
{
    public static menuSelector Instance { get; private set; }
    public string currentlySelectedObject;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PlayButton")
        {
            currentlySelectedObject = "PlayButton";
        }
        else if(other.gameObject.name == "QuitButton")
        {
            currentlySelectedObject = "QuitButton";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayButton")
        {
            currentlySelectedObject = "none";
        }
        else if (other.gameObject.name == "QuitButton")
        {
            currentlySelectedObject = "none";
        }
    }

    public string GetMenu()
    {
        return currentlySelectedObject;
    }

}
