using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] private float mTimeBetweenGenerations = 30;
    private float _mTimeNextGeneration = 0;
    private GameController _gameController;
    private Transform _player;
    public GameObject bossPrefab;
    public Transform[] positionsGenerateBoss;
    
    private void Start()
    {
        _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
        _player = GameObject.FindWithTag(Tags.Player).transform;
        _mTimeNextGeneration = mTimeBetweenGenerations;
    }

    private void Update()
    {
        if (!(Time.timeSinceLevelLoad > _mTimeNextGeneration)) return;
        var calculatedPosition = CalculateFarthestPositionFromPlayer();
        Instantiate(bossPrefab, calculatedPosition, Quaternion.identity);
        _gameController.ShowUpWarningBossCreated();
        _mTimeNextGeneration = Time.timeSinceLevelLoad + mTimeBetweenGenerations;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    private Vector3 CalculateFarthestPositionFromPlayer()
    {
        var farthestPosition = Vector3.zero;
        var greaterDistance = 0f;
        foreach (var positions in positionsGenerateBoss)
        {
            var distanceBetweenPlayer = Vector3.Distance(positions.position, _player.position);
            if (!(distanceBetweenPlayer > greaterDistance)) continue;
            greaterDistance = distanceBetweenPlayer;
            farthestPosition = positions.position;
        }
        return farthestPosition;
    }
}
