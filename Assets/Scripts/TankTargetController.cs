using UnityEngine;
using UnityEngine.Events;

public class TankTargetController : MonoBehaviour
{
    public UnityAction action;

    public enum MovementType
    {
        STATIC,
        ONE,
        TWO
    }

    public MovementType movementType;
    public float speed;
    public float timeLimit;
    public int direction = 1;
    public float offsetX;
    public float offsetY;


    private void Start()
    {
        switch(movementType)
        {
            case MovementType.STATIC:
                action = Static;
                break;
            case MovementType.ONE:
                InvokeRepeating("SwitchDirection", timeLimit, timeLimit);
                action = One;
                break;
            case MovementType.TWO:
                InvokeRepeating("SwitchDirection", timeLimit, timeLimit);
                action = Two;
                break;
            default:
                break;
        }
    }
    void Update()
    {
        action.Invoke();
    }

    private void Static()
    {

    }

    private void One()
    {
        transform.Translate(direction * transform.forward * speed, Space.World);
    }

    private void Two()
    {
        transform.Translate(direction * transform.forward * speed, Space.World);

        transform.Translate(transform.up * speed * Mathf.Sin(Time.time * 4 + offsetY) * 1.5f, Space.World);
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
        EnemyManager.Instance.DeregisterEnemyWithPoints(gameObject);
        Destroy(gameObject);
    }
}
