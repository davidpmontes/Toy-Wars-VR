using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
        if (dollyCart.m_Position >= 26)
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
    }

    public void TakeDamage()
    {
        life -= 1;
        lifeBar.ReduceLife(1);
    }

    public void PartDestroyed(GameObject part)
    {
        if (stage == 0)
        {
            WeakSpots1.Remove(part);

            if (part.name == "Bomber")
            {
                part.gameObject.GetComponent<ZeppelinBomber>().StopCycle();
                AudioManager.Instance.PlayNarration(ILoveTheSmellOfOrangeJuiceInTheMorning);
            }
            else
            {
                if (WeakSpots1.Count == 9)
                {
                    AudioManager.Instance.PlayNarration(laugh1);
                }
                else if (WeakSpots1.Count == 8)
                {
                    AudioManager.Instance.PlayNarration(ThatsItKeepFiring);
                }
                else if (WeakSpots1.Count == 7)
                {
                    AudioManager.Instance.PlayNarration(AhStopThat);
                }
                else if (WeakSpots1.Count == 6)
                {
                    AudioManager.Instance.PlayNarration(ItsSlowingDown);
                }
                else if (WeakSpots1.Count == 4)
                {
                    AudioManager.Instance.PlayNarration(AllYourBaseAreBelongToUs);
                }
                else if (WeakSpots1.Count == 2)
                {
                    AudioManager.Instance.PlayNarration(Nooooo);
                }
            }

            if (WeakSpots1.Count == 0)
            {
                AudioManager.Instance.PlayNarration(MyEmperorIveFailedYou);
                stage = 1;
            }
        }
    }

}