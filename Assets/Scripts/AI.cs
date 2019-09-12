using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public StateMachine StateMachine { get; set; }

    public GameObject missile;
    protected GameObject NPC;

    public Transform Turret { get; set; }
    public Transform Spawn { get; set; }

    public GameObject Player { get; set; }

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
        Turret = gameObject.transform.Find("turretHead");
        Spawn = Turret.Find("spawn");
        StateMachine = new StateMachine(this.gameObject);
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void lookAt()
    {
        var lookPos = Player.transform.position - Turret.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Turret.transform.rotation = Quaternion.Slerp(Turret.transform.rotation, rotation, Time.deltaTime * LockOnDamping);
    }

    public void attack()
    {
        InvokeRepeating("launchMissile", 0.0f, 2.0f);
    }

    public void haltAttack()
    {
        CancelInvoke("launchMissile");
    }

    private void launchMissile()
    {
        //AudioManager.Instance.Play("weapons", "missile");
        Instantiate(missile, Spawn.position, Spawn.rotation);
    }
}
