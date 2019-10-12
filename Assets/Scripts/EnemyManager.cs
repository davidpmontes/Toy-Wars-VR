using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_NAMES
{
	RedTank, RedHelicopter
}

public class EnemyManager : MonoBehaviour
{
	public static EnemyManager Instance { get; private set; }

    public Dictionary<GameObject, GameObject> enemyToRedTarget;
    private GameObject nearestTarget;
    public int enemyCount;

    private readonly int MAX_LOCKON_DISTANCE = 100;
    [SerializeField] private GameObject AllEnemies;

    void Awake()
    {
        Instance = this;
        enemyToRedTarget = new Dictionary<GameObject, GameObject>();
    }

    void Update()
    {
        CheckEnemiesBeingTargeted();
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void DeregisterEnemy()
    {
        enemyCount--;
        Level1Manager.Instance.UpdateState();
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    private void CheckEnemiesBeingTargeted()
    {
        //foreach (GameObject enemy in enemyToRedTarget.Keys)
        //{
        //    var viewportPoint = Camera.main.WorldToViewportPoint(enemy.transform.position);
        //    viewportPoint.x = (viewportPoint.x * Screen.width) - Screen.width / 2;
        //    viewportPoint.y = (viewportPoint.y * Screen.height) - Screen.height / 2;

        //    var distance = Vector2.Distance(viewportPoint, AimingCircle.Instance.rtPosition());

        //    if (enemyToRedTarget[enemy] == null)
        //    {
        //        if (distance < MAX_LOCKON_DISTANCE)
        //        {
        //            var redTarget = ObjectPool.Instance.GetFromPoolInactive(Pools.RedTarget);
        //            redTarget.transform.SetParent(CanvasHUD.Instance.transform);
        //            redTarget.SetActive(true);
        //            enemyToRedTarget[enemy] = redTarget;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        if (distance > MAX_LOCKON_DISTANCE)
        //        {
        //            ObjectPool.Instance.DeactivateAndAddToPool(enemyToRedTarget[enemy]);
        //            enemyToRedTarget[enemy] = null;
        //            break;
        //        }

        //        enemyToRedTarget[enemy].GetComponent<RectTransform>().anchoredPosition = viewportPoint;
        //    }
        //}
    }

    public GameObject GetNearestTarget()
    {
        float distanceToNearestEnemy = float.MaxValue;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemyToRedTarget.Keys)
        {
            if (enemyToRedTarget[enemy] != null)
            {
                float distance = Vector3.Distance(enemy.transform.position, PlayerManager.Instance.CurrentVehicle().transform.position);
                if (distance < distanceToNearestEnemy)
                {
                    distanceToNearestEnemy = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    public void AddToAllEnemies(GameObject enemy)
    {
        enemy.transform.parent = AllEnemies.transform;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        //WaveManager.Instance.EnemyDestroyed();
        ObjectPool.Instance.DeactivateAndAddToPool(enemy);
        //enemyToRedTarget.Remove(enemy);
    }
}
