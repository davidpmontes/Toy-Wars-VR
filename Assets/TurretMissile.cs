using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    private GameObject target; 

    void Start()
    {
        Invoke("DestroySelf", 5);
    }

    void Update()
    {
        MoveToTarget();
    }

    public void Init(GameObject target, Vector3 position, Vector3 rotation)
    {
        this.target = target;
        transform.position = position;
    }

    private void MoveToTarget()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.Translate(transform.forward);
    }

    private void DestroySelf()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
