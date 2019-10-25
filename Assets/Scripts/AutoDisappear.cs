using UnityEngine;

public class AutoDisappear : MonoBehaviour
{
    public float disappearTime;


    private void OnEnable()
    {
        Invoke("Disappear", disappearTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Disappear()
    {
        ObjectPool.Instance.DeactivateAndAddToPool(gameObject);
    }
}
