using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float speed;
    public float timeLimit;
    public int direction = 1;

    private void Start()
    {
        InvokeRepeating("SwitchDirection", Random.Range(0, timeLimit), timeLimit);
    }
    void Update()
    {
        transform.Translate(direction * transform.forward * speed, Space.World);
    }

    private void SwitchDirection()
    {
        direction *= -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.Large_CFX_Explosion_B_Smoke_Text);
        explosion.transform.position = transform.position;
        explosion.transform.GetComponent<Explosion>().Init();
        explosion.SetActive(true);
        EnemyManager.Instance.DeregisterEnemy(gameObject);
        Destroy(gameObject);
    }
}
