using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class Zeppelin : MonoBehaviour
{
    public static Zeppelin Instance { get; private set; }


    private float life = 100;
    private int stage = 0;

    [SerializeField] private CinemachineSmoothPath smoothPath;
    [SerializeField] private CinemachineDollyCart dollyCart;

    [SerializeField] private List<GameObject> WeakSpots1; //9
    [SerializeField] private LifeBar lifeBar;
    [SerializeField] private GameObject bomber;

    //Commander
    [SerializeField] private AudioClip ILoveTheSmellOfOrangeJuiceInTheMorning;
    [SerializeField] private AudioClip ItsSlowingDown;
    [SerializeField] private AudioClip ThatsItKeepFiring;

    //Zeppelin
    [SerializeField] private AudioClip laugh1;
    [SerializeField] private AudioClip laugh2;
    [SerializeField] private AudioClip laugh3;
    [SerializeField] private AudioClip AhStopThat;
    [SerializeField] private AudioClip AllYourBaseAreBelongToUs;
    [SerializeField] private AudioClip Nooooo;
    [SerializeField] private AudioClip MyEmperorIveFailedYou;

    [SerializeField] private ZeppelinEnemyDamageable body;
    [SerializeField] private GameObject machineGun;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckResetPosition();
    }

    private void CheckResetPosition()
    {
        if (dollyCart.m_Position >= 28)
        {
            dollyCart.m_Position -= 16;
        }
    }

    public void Init()
    {
        foreach(GameObject weakspot in WeakSpots1)
        {
            EnemyManager.Instance.RegisterEnemy(weakspot);
        }
        lifeBar.SetMaxLifeAndCurrLife(life);
        body.SetVulnerability(false);
    }

    public void TakeDamage()
    {
        life -= 1;
        lifeBar.ReduceLife(1);
    }

    public void PartDestroyed(GameObject part)
    {
        WeakSpots1.Remove(part);

        if (stage == 0)
        {
            if (part.name == "Bomber")
            {
                part.gameObject.GetComponent<ZeppelinBomber>().StopCycle();
                AudioManager.Instance.PlayNarration(ILoveTheSmellOfOrangeJuiceInTheMorning);
            }
            else
            {
                if (WeakSpots1.Count == 13)
                {
                    AudioManager.Instance.PlayNarration(laugh1);
                }
                else if (WeakSpots1.Count == 11)
                {
                    AudioManager.Instance.PlayNarration(ThatsItKeepFiring);
                }
                else if (WeakSpots1.Count == 9)
                {
                    AudioManager.Instance.PlayNarration(AhStopThat);
                }
                else if (WeakSpots1.Count == 7)
                {
                    AudioManager.Instance.PlayNarration(ItsSlowingDown);
                }
                else if (WeakSpots1.Count == 5)
                {
                    AudioManager.Instance.PlayNarration(AllYourBaseAreBelongToUs);
                }
                else if (WeakSpots1.Count == 3)
                {
                    AudioManager.Instance.PlayNarration(Nooooo);
                }
                else if (WeakSpots1.Count == 1) //only body left
                {
                    stage = 1;
                    body.SetVulnerability(true);
                }
            }
        }
        else if (stage == 1)
        {
            if (WeakSpots1.Count == 0)
            {
                StartCoroutine(FinalDestroy());
            }
        }
    }

    IEnumerator FinalDestroy()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayNarration(MyEmperorIveFailedYou);
        machineGun.SetActive(false);
        body.SetVulnerability(false);

        for (int i = 0; i < 10; i++)
        {
            var explosion = ObjectPool.Instance.GetFromPoolInactive(Pools.YellowFireImpactV2);
            explosion.transform.localScale = Vector3.one * 30;
            explosion.GetComponent<Explosion>().Init(body.gameObject.transform.position + Random.insideUnitSphere * 50);
            explosion.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        var explosion2 = ObjectPool.Instance.GetFromPoolInactive(Pools.YellowFireImpactV2);
        explosion2.transform.localScale = Vector3.one * 100;
        explosion2.GetComponent<Explosion>().Init(body.gameObject.transform.position);
        explosion2.SetActive(true);

        Level1Manager.Instance.ZeppelinDestroyed();
        Destroy(gameObject);
    }
}