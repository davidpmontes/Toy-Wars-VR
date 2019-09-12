using UnityEngine;

public class AlienBase : StateMachineBehaviour
{
    protected GameObject npc;
    protected GameObject opponent;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float rotSpeed;

    [SerializeField]
    protected float accuracy;

    protected AlienAI ai;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.gameObject;
        ai = npc.GetComponent<AlienAI>();
    }
}
