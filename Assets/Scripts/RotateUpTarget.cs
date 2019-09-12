using UnityEngine;

public class RotateUpTarget : MonoBehaviour, IPopUpTarget
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Appear()
    {
        animator.SetBool("RotateUp", true);
        AllTargetsController.Instance.AddToActive(gameObject);
    }

    public void Hide()
    {
        animator.SetBool("RotateDown", true);
        AllTargetsController.Instance.AddToActive(gameObject);
    }
}
