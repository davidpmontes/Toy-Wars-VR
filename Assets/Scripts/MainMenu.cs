using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject AimingSelector;

    public GameObject StoryOrShootingRangeMenu;
    public GameObject ShootingRangeMenu;

    public static MainMenu Instance;

    private void Start()
    {
        Instance = this;

        AimingSelector.SetActive(false);

        StoryOrShootingRangeMenu.SetActive(true);

        Invoke("Stage1", 1.5f);
    }

    private void Stage1()
    {
        AudioManager.Instance.Play("MainMenu");
        AimingSelector.SetActive(true);
    }

    public void MenuClick(Vector3 position)
    {
        if (StoryOrShootingRangeMenu.activeSelf)
        {
            StoryOrShootingRangeMenu.GetComponent<StoryOrShootingRange>().Selection(position);
        }
        else if (ShootingRangeMenu.activeSelf)
        {
            ShootingRangeMenu.GetComponent<ShootingRangeMenu>().Selection(position);
        }
    }

    public void MenuTransition(Vector3 position, string name)
    {
        AudioManager.Instance.Play("Selection");

        var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.smallExplosion);
        explosion.transform.position = position;
        explosion.SetActive(true);

        AimingSelector.GetComponent<CapsuleCollider>().enabled = false;

        if (name == "Story")
            StartCoroutine(LoadMenu("ShootingStoryOrShootingRangeMenu", 1.25f));
        else if (name == "ShootingRange")
            StartCoroutine(LoadMenu("ShootingRangeMenu", 1.25f));
        else if (name == "First")
            StartCoroutine(LoadScene("ShootingRange1", 1.25f));
    }

    IEnumerator LoadMenu(string menuName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        StoryOrShootingRangeMenu.SetActive(false);
        ShootingRangeMenu.SetActive(false);

        if (menuName == "ShootingStoryOrShootingRangeMenu")
            StoryOrShootingRangeMenu.SetActive(true);
        else if (menuName == "ShootingRangeMenu")
            ShootingRangeMenu.SetActive(true);

        AimingSelector.GetComponent<CapsuleCollider>().enabled = true;
    }

    IEnumerator LoadScene(string scenename, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        StoryOrShootingRangeMenu.SetActive(false);
        ShootingRangeMenu.SetActive(false);

        AudioManager.Instance.Stop("MainMenu");

        if (scenename == "ShootingRange1")
            SceneManager.LoadScene("ShootingRangePopUpTargets");


    }
}