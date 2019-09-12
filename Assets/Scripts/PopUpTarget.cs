using UnityEngine;

public class PopUpTarget : MonoBehaviour, IPopUpTarget
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Appear()
    {
        animator.SetBool("FlipUp", true);
        AllTargetsController.Instance.AddToActive(gameObject);
    }

    public void Hide()
    {
        animator.SetBool("FlipDown", true);
        AllTargetsController.Instance.AddToActive(gameObject);
    }
}
