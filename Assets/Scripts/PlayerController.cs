using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float mSpeed;
    private Animator _mAnimator;
    private Rigidbody _mRigidbody;
    private Vector3 _direction;

    private void Start()
    {
        _mAnimator = GetComponent<Animator>();
        _mRigidbody = GetComponent<Rigidbody>();
    }

    private void Update ()
	{
		var axisX = Input.GetAxis("Horizontal");
		var axisZ = Input.GetAxis("Vertical");
		
		_direction = new Vector3(axisX, 0, axisZ);

        _mRigidbody.MovePosition
            (_mRigidbody.position + 
            (_direction * (Time.deltaTime * mSpeed)));

        _mAnimator.SetBool("isMove", _direction != Vector3.zero);
    }

    private void FixedUpdate()
    {
        _mRigidbody.MovePosition
            (_mRigidbody.position +
            (_direction * (Time.deltaTime * mSpeed)));
    }
}
