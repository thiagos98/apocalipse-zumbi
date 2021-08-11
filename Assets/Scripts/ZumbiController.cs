using UnityEngine;

public class ZumbiController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public float Speed;
	private Rigidbody _mRig;
    private Animator _mAnim;
	private Vector3 _mDirection;
    public GameObject GameController;

	private void Start ()
	{
		_mRig = GetComponent<Rigidbody>();
        _mAnim = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        _mDirection = Player.transform.position - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(_mDirection);
        _mRig.MoveRotation(newRotation);

        if (distance > 2.5)
        {
            _mRig.MovePosition(_mRig.position + _mDirection.normalized * (Time.deltaTime * Speed));

            _mAnim.SetBool("Atacando", false);
        }
        else
        {
            _mAnim.SetBool("Atacando", true);
        }
    }

    private void AttackPlayer()
    {
        Time.timeScale = 0;

        GameController.GetComponent<GameController>().SetPanelGameOver(true);
    }
}
