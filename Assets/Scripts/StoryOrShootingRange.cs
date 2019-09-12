using UnityEngine;

public class StoryOrShootingRange : MonoBehaviour
{
    public GameObject StoryButton;
    public GameObject ShootingRangeButton;
    
    private void OnEnable()
    {
        Stage0();
    }

    private void Stage0()
    {
        StoryButton.GetComponent<Animator>().Play("FlyInFromLeft");
        Invoke("ShootingRangeButtonFly", .2f);
    }

    private void ShootingRangeButtonFly()
    {
        ShootingRangeButton.GetComponent<Animator>().Play("FlyInFromRight");
    }

    public void Selection(Vector3 pos)
    {
        if (StoryButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        {
            StoryButton.GetComponent<Animator>().Play("BlastAway");
            ShootingRangeButton.GetComponent<Animator>().Play("FlyOutLeft");
            MainMenu.Instance.MenuTransition(pos, "Story");
        }
        else if (ShootingRangeButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        {
            ShootingRangeButton.GetComponent<Animator>().Play("BlastAway");
            StoryButton.GetComponent<Animator>().Play("FlyOutLeft");
            MainMenu.Instance.MenuTransition(pos, "ShootingRange");
        }
    }
}
