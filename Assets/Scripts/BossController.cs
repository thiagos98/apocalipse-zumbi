using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agent;

    private void Start()
    {
        _player = GameObject.FindWithTag(Tags.Player).transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);
    }
}
