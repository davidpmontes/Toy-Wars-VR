using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IRoomActivate
{
    [SerializeField] private GameObject arm = default;
    [SerializeField] private Transform joint = default;
    private bool rotating = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            arm.transform.RotateAround(joint.position, joint.forward, 180 * Time.deltaTime);
        }
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        rotating = true;
        StartCoroutine(StopRotation());
    }

    private IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(0.5f);
        rotating = false;
    }
}
