using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField]
    private readonly float DELAY = 0.1f;

    private float lastTimeFired;

    void Update()
    {
        if (InputController.Button5() && Time.time - lastTimeFired > DELAY)
        {
            lastTimeFired = Time.time;
            //AudioManager.instance.PlayOverlapping("HelicopterMissile");
            SpawnFromPool();
        }
    }

    void SpawnFromPool()
    {
        var rocket = ObjectPool.Instance.GetFromPoolInactive(Pools.Rocket);
        //rocket.GetComponent<Rocket>().Init(transform, EnemyManager.Instance.GetNearestTarget());
        rocket.SetActive(true);
    }
}
