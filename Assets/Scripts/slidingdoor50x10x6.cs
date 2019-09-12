using UnityEngine;

public class slidingdoor50x10x6 : MonoBehaviour, IDoor
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        AudioManager.Instance.Play("SlidingDoorBell");
        animator.SetTrigger("lower");
    }

    public void Close()
    {
        animator.SetTrigger("raise");
    }
}
