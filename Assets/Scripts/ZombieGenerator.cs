using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ZombieGenerator : MonoBehaviour
{
	private float _mTimeCounter = 0f;
	public GameObject zombie;
	public float timeToGenerateZombie = 1f;
	public LayerMask layerZombie;
	private const float MGenerateDistance = 3;
	private const float MDistanceFromPlayerToGeneration = 20;
	private GameObject _player;

	private void Start()
	{
		_player = GameObject.FindWithTag(Tags.Player);
	}

	private void Update ()
	{
		if (!(Vector3.Distance(transform.position, _player.transform.position) >
		      MDistanceFromPlayerToGeneration)) return;
		_mTimeCounter += Time.deltaTime;
		
		if (!(_mTimeCounter >= timeToGenerateZombie)) return;
		StartCoroutine(GenerateNewZombie());
		_mTimeCounter = 0f;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, MGenerateDistance);
	}

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
		
		Instantiate(zombie, creatingPosition, transform.rotation);
	}

	private Vector3 RandomizePosition()
	{
		var position = Random.insideUnitSphere * MGenerateDistance;
		position += transform.position;
		position.y = 0;
		return position;
	}
}
