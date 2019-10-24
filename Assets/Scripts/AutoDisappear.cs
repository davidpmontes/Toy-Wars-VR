using UnityEngine;

public class AutoDisappear : MonoBehaviour
{
    public float disappearTime;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlayOneshot("explosion_larger_01", transform.position, occluding: true, spacial_blend: 0f);
        print("Fired");
    }

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
