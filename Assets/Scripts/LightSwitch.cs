using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IRoomActivate
{
    [SerializeField] GameObject room_light = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        room_light.SetActive(false);
        StartCoroutine(TurnLightOn());
    }

    private IEnumerator TurnLightOn()
    {
        yield return new WaitForSeconds(1f);
        room_light.SetActive(true);
    }

}
