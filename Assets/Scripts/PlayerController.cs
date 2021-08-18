using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private AudioClip damageSound;
    private Vector3 _mDirection;
    private GameController _gameController;
    private InterfaceController _interfaceController;
    private MovementPlayer _movementPlayer;
    private AnimationCharacters _animationCharacters;
    public Status statusPlayer;
    
    private void Awake()
    {
        statusPlayer = GetComponent<Status>();
        _movementPlayer = GetComponent<MovementPlayer>();
        _animationCharacters = GetComponent<AnimationCharacters>();
        _gameController = GameObject.FindWithTag(Tags.GameController).GetComponent<GameController>();
        _interfaceController = GameObject.FindWithTag(Tags.Canvas).GetComponent<InterfaceController>();
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
        _interfaceController.UpdateSliderLifePlayer();
        AudioController.Instance.PlayOneShot(damageSound);
        
        if (statusPlayer.mLife > 0) return;
        Die();
    }

    public void Die()
    {
        Time.timeScale = 0;
        _gameController.SetPanelGameOver(true);
    }

}