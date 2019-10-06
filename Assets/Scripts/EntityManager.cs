using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] GameObject[] entities;
    private int currIndex = 0;

    void Start()
    {
        ActivateEntity();
    }

    public void ActivateEntity()
    {
        entities[currIndex].SetActive(true);
        currIndex++;
    }
}
