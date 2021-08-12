using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private GameObject player;
	private Rigidbody _mRig;
    private Animator _mAnim;
	private Vector3 _mDirection;
    public float Speed;

	private void Start ()
    {
        player = GameObject.FindWithTag("Player");
		_mRig = GetComponent<Rigidbody>();
        _mAnim = GetComponent<Animator>();

        var generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
    }

	private void FixedUpdate()
	{
        var distance = Vector3.Distance(transform.position, player.transform.position);

        _mDirection = player.transform.position - transform.position;

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
        player.GetComponent<PlayerController>().TakeDamage();
    }
}
