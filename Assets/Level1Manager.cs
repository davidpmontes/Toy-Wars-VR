using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Level1Manager : MonoBehaviour
{
    int state = 0;

    [SerializeField] GameObject[] enemySpawners;
    [SerializeField] GameObject[] timelines;

    void Start()
    {
        UpdateState();
    }

    public void UpdateState()
    {
        if (state == 0) //Opening scene, audio introduction
        {
            timelines[0].SetActive(true);
            NextState((float)timelines[0].GetComponent<PlayableDirector>().duration + 1);
        }
        else if (state == 1) //Spawn enemies
        {
            enemySpawners[0].SetActive(true);
            NextState(0);
        }
        else if (state == 2) //Waiting for the Player to defeat all the targets
        {
            if (EnemyManager.Instance.GetEnemyCount() < 0)
            {
                Debug.Log("you won!");
                NextState(0);
            }
        }
        else if (state == 3)
        {

        }
    }

    private void PlayAudio(AudioClip clip, float time)
    {
        StartCoroutine(PlayAudioInTime(clip, time));
    }

    IEnumerator PlayAudioInTime(AudioClip clip, float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void NextState(float time)
    {
        StartCoroutine(NextStateInTime(time));
    }

    IEnumerator NextStateInTime(float time)
    {
        yield return new WaitForSeconds(time);
        state++;
        UpdateState();
    }
}
