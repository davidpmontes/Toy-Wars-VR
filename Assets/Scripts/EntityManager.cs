using UnityEngine;
using UnityEngine.Playables;

public class EntityManager : MonoBehaviour
{
    public GameObject[] entities;
    private int currEntity;

    private void OnEnable()
    {
        currEntity = 0;
        GetComponent<PlayableDirector>().Play();
        foreach (GameObject entity in entities)
        {
            entity.SetActive(false);
        }
    }

    public void NextEntity()
    {
        if (currEntity < entities.Length)
        {
            entities[currEntity].SetActive(true);
        }
    }
}
