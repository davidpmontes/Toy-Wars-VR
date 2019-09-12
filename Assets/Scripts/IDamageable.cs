using UnityEngine;

public enum PlayerWeapon
{
    TurretBullet
}

public interface IDamageable
{
    void TakeDamage(int value);
}
