using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
	public GameObject Zombie;
	private float _mTimeCounter = 0f;
	public float TimeToGenerateZombie = 1f;

	private void Update ()
	{
		_mTimeCounter += Time.deltaTime;

		if (!(_mTimeCounter >= TimeToGenerateZombie)) return;
		
		Instantiate(Zombie, transform.position, transform.rotation);
		_mTimeCounter = 0f;

	}
}
