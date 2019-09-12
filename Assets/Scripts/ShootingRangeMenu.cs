using UnityEngine;

public class ShootingRangeMenu : MonoBehaviour
{
    public GameObject FirstButton;

    private void OnEnable()
    {
        Stage0();
    }

    private void Stage0()
    {
        FirstButton.GetComponent<Animator>().Play("FlyInFromLeft");
    }

    public void Selection(Vector3 pos)
    {
        if (FirstButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        {
            FirstButton.GetComponent<Animator>().Play("BlastAway");
            MainMenu.Instance.MenuTransition(pos, "First");
        }
        //else if (ShootingRangeButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        //{
        //    ShootingRangeButton.GetComponent<Animator>().Play("BlastAway");
        //    StoryButton.GetComponent<Animator>().Play("FlyOutLeft");
        //}
        //else
        //{
        //    return;
        //}
    }   
}
