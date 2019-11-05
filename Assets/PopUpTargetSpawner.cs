using UnityEngine;

public class PopUpTargetSpawner : MonoBehaviour
{
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            EnemyManager.Instance.RegisterEnemy(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        
    }
}
