using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treads : MonoBehaviour
{
    private bool colliding;
    public bool Colliding {
        get
        {
            return colliding;
        } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Statics"))
        {
            colliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Statics"))
        {
            colliding = false;
        }
    }
}
