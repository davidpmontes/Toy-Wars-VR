using UnityEngine;

public class PopUpTargetSpawner : MonoBehaviour, IEnemySpawner
{
    public void Init()
    {
        Spawn();
    }

    private void Spawn()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            EnemyManager.Instance.RegisterEnemy(transform.GetChild(i).gameObject);
        }
    }
}
