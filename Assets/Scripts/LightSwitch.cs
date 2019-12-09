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
        AudioManager.Instance.PlayOneshot("switch_off", this.transform);
        room_light.SetActive(false);
        StartCoroutine(TurnLightOn());
    }

    private IEnumerator TurnLightOn()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayOneshot("switch_on", this.transform);
        room_light.SetActive(true);
    }

}
