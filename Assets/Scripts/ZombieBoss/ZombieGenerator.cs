using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ZombieGenerator : MonoBehaviour
{
	private float _mTimeCounter = 0f;
	private const float MGenerateDistance = 3;
	private const float MDistanceFromPlayerToGeneration = 20;
	private GameObject _player;
	private int _mMaximumAmountLiveZombies = 3;
	private int _mCurrentAmountZombiesLive;
	private const int MTimeForNextDifficultyIncrement = 15;
	private int _counterIncreaseDifficulty;
	public GameObject zombie;
	public float timeToGenerateZombie = 1f;
	public LayerMask layerZombie;
	private void Start()
	{
		_player = GameObject.FindWithTag(Tags.Player);
		_counterIncreaseDifficulty = MTimeForNextDifficultyIncrement;
		for (var i = 0; i < _mMaximumAmountLiveZombies; i++)
		{
			StartCoroutine(GenerateNewZombie());
		}
	}

	private void Update ()
	{
		var canSpawnZombies = Vector3.Distance(transform.position, _player.transform.position) >
		                      MDistanceFromPlayerToGeneration;
		
		if (canSpawnZombies && _mCurrentAmountZombiesLive < _mMaximumAmountLiveZombies)
		{
			_mTimeCounter += Time.deltaTime;
		
			if (!(_mTimeCounter >= timeToGenerateZombie)) return;
			StartCoroutine(GenerateNewZombie());
			_mTimeCounter = 0f;
		}

		if (Time.timeSinceLevelLoad > _counterIncreaseDifficulty)
		{
			_mMaximumAmountLiveZombies++;
			_counterIncreaseDifficulty = (int)Time.timeSinceLevelLoad + MTimeForNextDifficultyIncrement;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, MGenerateDistance);
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private IEnumerator GenerateNewZombie()
	{
		var creatingPosition = RandomizePosition();
		Collider[] colliders = Physics.OverlapSphere(creatingPosition, 1, layerZombie);

		while (colliders.Length > 0)
		{
			creatingPosition = RandomizePosition();
			colliders = Physics.OverlapSphere(creatingPosition, 1, layerZombie);
			yield return null;
		}
		
		var zombieGenerated = Instantiate(zombie, creatingPosition, transform.rotation)
			.GetComponent<ZombieController>();
		zombieGenerated.ZombieGenerator = this;
		_mCurrentAmountZombiesLive++;
	}

	private Vector3 RandomizePosition()
	{
		var position = Random.insideUnitSphere * MGenerateDistance;
		position += transform.position;
		position.y = 0;
		return position;
	}

	public void ReduceAmountLiveZombies()
	{
		_mCurrentAmountZombiesLive--;
	}
}
