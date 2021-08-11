using UnityEngine;

public class ZumbiController : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField]private float speed;
	private Rigidbody _mRig;
	private Vector3 _mDirection;

	private void Start ()
	{
		_mRig = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		var position = player.transform.position;
		_mDirection = position - transform.position;
		_mRig.MovePosition(_mRig.position + _mDirection.normalized * (Time.deltaTime * speed));
	}
}
