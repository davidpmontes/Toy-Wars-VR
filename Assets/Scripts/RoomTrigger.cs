using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] GameObject obj = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (obj != null)
        {
            IRoomActivate obj2;
            obj.TryGetComponent<IRoomActivate>(out obj2);
            if(obj2 != null)
            {
                obj2.Activate();
            }
        }
    }
}
