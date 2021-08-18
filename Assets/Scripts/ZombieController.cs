using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ZombieController : MonoBehaviour, IKillable
{
    [SerializeField] private GameObject player;
    private Vector3 _mDirection;
    private int _mDamage;
    private MovementCharacters _mMovement;
    private AnimationCharacters _mAnimation;
    private Status _mStatusZombie;
    public AudioClip zombieDeathSound;
    
    private void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        _mMovement = GetComponent<MovementCharacters>();
        _mAnimation = GetComponent<AnimationCharacters>();
        _mStatusZombie = GetComponent<Status>();
        RandomizeZombie();
    }

    private void FixedUpdate()
    {
        var positionPlayer = player.transform.position;
        var positionZombie = transform.position;
        var distance = Vector3.Distance(positionZombie, positionPlayer);

        _mDirection = positionPlayer - positionZombie;
        
        _mMovement.Rotate(_mDirection);

        if (distance > 2.5)
        {
            _mMovement.Move(_mDirection, _mStatusZombie.mSpeed);

            _mAnimation.Attack(false);
        }
        else
        {
            _mAnimation.Attack(true);
        }
    }

    private void AttackPlayer()
    {
        _mDamage = Random.Range(20, 30);
        player.GetComponent<PlayerController>().TakeDamage(_mDamage);
    }

    private void RandomizeZombie()
    {
        var generateTypeZombie = Random.Range(1, 28);
        transform.GetChild(generateTypeZombie).gameObject.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        _mStatusZombie.mLife -= damage;
        if (_mStatusZombie.mLife > 0) return;
        Die();
    }

    public void Die()
    {
        AudioController.Instance.PlayOneShot(zombieDeathSound);
        Destroy(gameObject);
    }
}