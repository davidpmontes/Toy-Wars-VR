﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] GameObject innerParent;
    [SerializeField] GameObject innerBar;
    [SerializeField] Material redLifeBar;
    [SerializeField] Material yellowLifeBar;
    [SerializeField] Material greenLifeBar;

    private float MaxLife;
    private float CurrLife;

    public void SetMaxLifeAndCurrLife(float value)
    {
        MaxLife = value;
        CurrLife = value;
    }

    public void ReduceLife(float value)
    {
        if (CurrLife >= value)
        {
            CurrLife -= value;

            var percentage = CurrLife / MaxLife;
            if (percentage > .75f)
            {
                innerBar.GetComponent<MeshRenderer>().material = greenLifeBar;
            }
            else if (percentage > .5f)
            {
                innerBar.GetComponent<MeshRenderer>().material = yellowLifeBar;
            }
            else
            {
                innerBar.GetComponent<MeshRenderer>().material = redLifeBar;
            }
        }
        else
        {
            CurrLife = 0;
        }
        UpdateInnerBar();
    }

    private void UpdateInnerBar()
    {
        var scale = innerParent.transform.localScale;
        scale.y = CurrLife / MaxLife;
        innerParent.transform.localScale = scale;
    }
}
