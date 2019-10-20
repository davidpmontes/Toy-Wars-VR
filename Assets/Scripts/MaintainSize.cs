using UnityEngine;

public class MaintainSize : MonoBehaviour
{
    public float SPEED = 100;
    public float PERCENT = .8f;
    public float MAX_SIZE = 2;
    public float MIN_SIZE = .5f;
        
    void Update()
    {
        var distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        //transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * Mathf.Clamp(distance / 70, MIN_SIZE, MAX_SIZE), Time.deltaTime * SPEED);
        transform.localScale = Vector3.one * Mathf.Clamp(distance / 70, MIN_SIZE, MAX_SIZE);
    }


}
