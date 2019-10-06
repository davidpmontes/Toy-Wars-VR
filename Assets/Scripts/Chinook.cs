using System.Collections;
using UnityEngine;

public class Chinook : MonoBehaviour, IEnemy
{
    private float life = 300;
    public bool isShaking = false;

    public void DamageEnemy(Vector3 position)
    {
        life--;
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = position;
        explosion.SetActive(true);

        if (life <= 0)
        {
            DestroySelf();
        }
        else
        {
            if (!isShaking)
            {
                isShaking = true;
                StartCoroutine("DamageShake");
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator DamageShake()
    {
        Vector3 oldPosition = transform.localPosition;
        transform.localPosition = transform.localPosition + Random.insideUnitSphere * 2;

        while (Vector3.Distance(transform.localPosition, oldPosition) > 0.1)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, oldPosition, Time.deltaTime * 20);
            yield return null;
        }
        transform.localPosition = oldPosition;
        isShaking = false;


        //Quaternion oldRotation = transform.rotation;
        //Quaternion newRotation = transform.rotation;
        //newRotation.eulerAngles = new Vector3(newRotation.eulerAngles.x + Random.Range(-10, 10), newRotation.eulerAngles.y + Random.Range(-10, 10), newRotation.eulerAngles.z + Random.Range(-10, 10));

        //transform.rotation = newRotation;

        //while (Mathf.Abs(Quaternion.Dot(transform.rotation, oldRotation)) < 0.999f)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, oldRotation, Time.deltaTime * 10);
        //}
        //transform.rotation = oldRotation;
    }

        private void DestroySelf()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
