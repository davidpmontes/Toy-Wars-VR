﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoy : MonoBehaviour, IRoomActivate
{
    public void Activate()
    {
        this.GetComponent<Rigidbody>().AddForce(-400 * this.transform.forward, ForceMode.Impulse);
    }
}
