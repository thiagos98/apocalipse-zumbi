using UnityEngine;
using UnityEngine.Serialization;

public class ZombieGenerator : MonoBehaviour
{
	[FormerlySerializedAs("Zombie")] public GameObject zombie;
	private float _mTimeCounter = 0f;
	[FormerlySerializedAs("TimeToGenerateZombie")] public float timeToGenerateZombie = 1f;

	private void Update ()
	{
		_mTimeCounter += Time.deltaTime;

		if (!(_mTimeCounter >= timeToGenerateZombie)) return;
		
		Instantiate(zombie, transform.position, transform.rotation);
		_mTimeCounter = 0f;

	}
}
