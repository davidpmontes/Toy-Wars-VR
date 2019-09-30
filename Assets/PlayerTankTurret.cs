﻿using UnityEngine;

public class PlayerTankTurret : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform barrel;

    void Update()
    {
        RotateTurretToController();
        RotateBarrelToController();
    }



    private void RotateTurretToController()
    {
        //find the vector pointing from our position to the target
        var direction = (targetPoint.position - transform.position).normalized;
        direction.y = 0;

        //create the rotation we need to be in to look at the target
        var lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    private void RotateBarrelToController()
    {
        //find the vector pointing from our position to the target
        var direction = (targetPoint.position - barrel.position).normalized;

        //create the rotation we need to be in to look at the target
        var lookRotation = Quaternion.LookRotation(direction);

        //rotate us over time according to speed until we are in the required rotation
        barrel.rotation = Quaternion.Slerp(barrel.rotation, lookRotation, Time.deltaTime * 2);
    }
}
