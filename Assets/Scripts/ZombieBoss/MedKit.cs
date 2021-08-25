using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MedKit : MonoBehaviour
{
    private int _mAmountCure;
    private const int MDestructionTime = 10;

    private void Start()
    {
        Destroy(gameObject, MDestructionTime);
    }

    private int RandomizeCure()
    {
        _mAmountCure = Random.Range(15, 25);
        return _mAmountCure;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var cure = RandomizeCure();
        if (!other.CompareTag(Tags.Player)) return;
        other.GetComponent<PlayerController>().Heal(cure);
        Destroy(gameObject);
    }
}
