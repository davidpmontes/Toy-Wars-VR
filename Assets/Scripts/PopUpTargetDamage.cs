using UnityEngine;

public class PopUpTargetDamage : MonoBehaviour //, IDamageable
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        if (animator.GetBool("FlipUp") == true)
        {
            animator.SetBool("FlipUp", false);
            AudioManager.Instance.PlayOverlapping("Explosion");
            PointsContainer.Instance.PointsScored(100, transform.position);
        }
    }
}
