using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnBullet;
    public Rigidbody body;

    public float reloadTime;
    public float recoil;

    private float fireTime;

    void Awake()
    {
        body = transform.parent.GetComponent<Rigidbody>();
    }

    void Update ()
    {
        if (InputController.Button4())
        {
            if (fireTime < Time.time)
            {
                body.AddForce(-this.transform.forward * recoil);
                Fire();
                fireTime = Time.time + reloadTime;
            }
        }
	}

    void Fire()
    {
        Instantiate(bulletPrefab, spawnBullet.position, spawnBullet.rotation);
    }
}
