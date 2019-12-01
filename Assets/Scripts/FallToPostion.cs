using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallToPostion : Collectible
{
    [SerializeField] Vector3 end_position = default;
    [SerializeField] float speed = default;
    [SerializeField] string audio_clip = default;
    [SerializeField] Transform anchor = default;
    [SerializeField] bool shootable;

    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    override public void Init()
    {
        if (shootable)
        {
            triggered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            float step = speed * Time.deltaTime;
            transform.RotateAround(anchor.position, -anchor.right, (end_position.x - transform.rotation.x) * step);
        }

    }
}
