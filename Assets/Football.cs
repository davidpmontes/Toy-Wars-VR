using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : Collectible
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    override public void Shot()
    {
        base.Shot();
        audioManager.PlayOneshot("impact_deep_thud_bounce_01", transform.position);
    }
}
