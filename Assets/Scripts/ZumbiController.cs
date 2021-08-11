using UnityEngine;

public class ZumbiController : MonoBehaviour
{
	[SerializeField] private GameObject Player;
	public float Speed;
	private Rigidbody _mRig;
	private Vector3 _mDirection;

	private void Start ()
	{
		_mRig = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
        var position = Player.transform.position;

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if(distance > 2)
        {
            _mDirection = position - transform.position;
            _mRig.MovePosition(_mRig.position + _mDirection.normalized * (Time.deltaTime * Speed));

            Quaternion newRotation = Quaternion.LookRotation(_mDirection);
            _mRig.MoveRotation(newRotation);
        }
	}
}
