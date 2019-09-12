using UnityEngine;

public class Patrol : AlienBase
{
    int currentWP;

    private void Awake()
    {

    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWP = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ai.waypoints.Length == 0) return;
        if (Vector3.Distance(ai.waypoints[currentWP].transform.position, 
                             npc.transform.position) < accuracy)
        {
            currentWP++;
            if (currentWP >= ai.waypoints.Length)
            {
                currentWP = 0;
            }
        }

        var direction = ai.waypoints[currentWP].transform.position - npc.transform.position;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    rotSpeed * Time.deltaTime);
        npc.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
