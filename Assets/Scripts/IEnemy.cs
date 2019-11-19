using UnityEngine;

public interface IEnemy
{
    void Init();
    void DamageEnemy(Vector3 position);
    void DestroyEnemy();
}
