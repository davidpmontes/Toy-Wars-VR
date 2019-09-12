using UnityEngine;

public class RotateUpTargetDamage : MonoBehaviour //, IDamageable
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        if (animator.GetBool("RotateUp") == true)
        {
            animator.SetBool("RotateUp", false);
            AudioManager.Instance.PlayOverlapping("Explosion");
            PointsContainer.Instance.PointsScored(100, transform.position);
        }
    }
}
