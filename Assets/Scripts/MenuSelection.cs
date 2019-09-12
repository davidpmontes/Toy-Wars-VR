using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayWhooshAndClang()
    {
        AudioManager.Instance.PlayOverlapping("WhooshAndClank");
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayOverlapping("Click");
        animator.SetBool("isHovering", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isHovering", false);
    }
}
