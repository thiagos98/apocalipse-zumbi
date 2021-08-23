using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossGenerator : MonoBehaviour
{
    private float _mTimeNextGeneration = 0;
    [SerializeField] private float mTimeBetweenGenerations = 30;
    public GameObject bossPrefab;
    private void Start()
    {
        _mTimeNextGeneration = mTimeBetweenGenerations;
    }

    private void Update()
    {
        if (!(Time.timeSinceLevelLoad > _mTimeNextGeneration)) return;
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
        _mTimeNextGeneration = Time.timeSinceLevelLoad + mTimeBetweenGenerations;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
