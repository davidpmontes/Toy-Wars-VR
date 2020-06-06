using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBounce : MonoBehaviour
{
    Vector3 start_scale;
    Vector3 max_scale = new Vector3(1.1f, 1.1f, 1.1f);
    Vector3 min_scale = new Vector3(0.9f, 0.9f, 0.9f);
    Vector3 target_scale;

    // Start is called before the first frame update
    void Start()
    {
        start_scale = this.transform.localScale;
        target_scale = max_scale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.Lerp(this.transform.localScale, target_scale, 0.2f * Time.deltaTime);
        if(transform.localScale == max_scale)
        {
            target_scale = min_scale;
        }
        else if (transform.localScale == min_scale)
        {
            target_scale = max_scale;
        }
    }
}
