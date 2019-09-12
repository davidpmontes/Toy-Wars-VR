using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour {

    public SoldierStateMachine StateMachine { get; set; }
    public GameObject[] waypoints;

    public GameObject bullet;
    //protected GameObject NPC;

    public Transform NPC { get; set; }
    public Transform Spawn { get; set; }

    public GameObject Player { get; set; }

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }

    [SerializeField]
    private float range;
    public float Range { get { return range; } }

    [SerializeField]
    private float turnRate;
    public float TurnRate { get { return turnRate; } }

    [SerializeField]
    private float lockOnDamping;
    public float LockOnDamping { get { return lockOnDamping; } }

    private void Start()
    {
        NPC = this.gameObject.transform;
        Spawn = NPC.Find("spawn");
        StateMachine = new SoldierStateMachine(this.gameObject);
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void lookAt()
    {
        var lookPos = Player.transform.position - NPC.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, rotation, Time.deltaTime * LockOnDamping);
    }

    public void attack()
    {
        InvokeRepeating("fireBullet", 0.0f, 2.0f);
    }

    public void haltAttack()
    {
        CancelInvoke("fireBullet");
    }

    private void fireBullet()
    {
        //AudioManager.Instance.Play("weapons", "missile");
        Instantiate(bullet, Spawn.position, Spawn.rotation);
    }
}
