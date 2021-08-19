using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IKillable, ICurable
{
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private AudioClip damageSound;
    private Vector3 _mDirection;
    private GameController _gameController;
    private MovementPlayer _movementPlayer;
    private AnimationCharacters _animationCharacters;
    [HideInInspector] public Status statusPlayer;
    
    private void Start()
    {
        statusPlayer = GetComponent<Status>();
        _movementPlayer = GetComponent<MovementPlayer>();
        _animationCharacters = GetComponent<AnimationCharacters>();
        _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
    }

    private void Update()
    {
        var axisX = Input.GetAxis("Horizontal");
        var axisZ = Input.GetAxis("Vertical");

        _mDirection = new Vector3(axisX, 0, axisZ);
        
        _animationCharacters.Move(_mDirection.magnitude);
    }

    private void FixedUpdate()
    {
        _movementPlayer.Move(_mDirection, statusPlayer.mSpeed);

        _movementPlayer.PlayerRotation(floorMask);
    }

    public void TakeDamage(int damageEnemy)
    {
        statusPlayer.mLife -= damageEnemy;
        _gameController.UpdateLifePlayer();
        AudioController.Instance.PlayOneShot(damageSound);
        
        if (statusPlayer.mLife > 0) return;
        Die();
    }
    
    public void Heal(int cureAmount)
    {
        statusPlayer.mLife += cureAmount;
        if (statusPlayer.mLife > statusPlayer.initialLife)
        {
            statusPlayer.mLife = statusPlayer.initialLife;
        }
        _gameController.UpdateLifePlayer();
    }
    
    public void Die()
    {
        GameController.SetTimeScale(0);
        _gameController.GameOver();
    }
}