using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Status : MonoBehaviour
{
    public int initialLife;
    [HideInInspector] public int mLife;
    public float mSpeed;

    private void Awake()
    {
        mLife = initialLife;
    }
}
