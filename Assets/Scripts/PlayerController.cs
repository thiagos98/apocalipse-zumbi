using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _mSpeed;
    private Animator _mAnimator;
    private Rigidbody _mRigidbody;
    private Vector3 _mDirection;
    public LayerMask FloorMask;

    private void Start()
    {
        _mAnimator = GetComponent<Animator>();
        _mRigidbody = GetComponent<Rigidbody>();
    }

    private void Update ()
	{
		var axisX = Input.GetAxis("Horizontal");
		var axisZ = Input.GetAxis("Vertical");
		
		_mDirection = new Vector3(axisX, 0, axisZ);

        _mRigidbody.MovePosition
            (_mRigidbody.position + 
            (_mDirection * (Time.deltaTime * _mSpeed)));

        _mAnimator.SetBool("isMove", _mDirection != Vector3.zero);
    }

    private void FixedUpdate()
    {
        _mRigidbody.MovePosition
            (_mRigidbody.position +
            (_mDirection * (Time.deltaTime * _mSpeed)));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit impact;

        if(Physics.Raycast(raio, out impact, 50, FloorMask))
        {
            Vector3 playerAimPosition = impact.point - transform.position;

            playerAimPosition.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(playerAimPosition);

            _mRigidbody.MoveRotation(newRotation);
        }
    }
}
