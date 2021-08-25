using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BossController : MonoBehaviour, IKillable {
    [SerializeField] private GameObject mMedKitPrefab;
    [SerializeField] private GameObject mBloodParticleBoss;
    private Transform _player;
    private NavMeshAgent _agent;
    private Status _mStatusBoss;
    private AnimationCharacters _mAnimation;
    private MovementCharacters _mMovement;
    public Slider sliderBossLife;
    public Color maxLife, minimumLife;
    public Image imageSlider;

    private void Start()
    {
        _player = GameObject.FindWithTag(Tags.Player).transform;
        _agent = GetComponent<NavMeshAgent>();
        _mStatusBoss = GetComponent<Status>();
        _mAnimation = GetComponent<AnimationCharacters>();
        _mMovement = GetComponent<MovementCharacters>();
        
        SetBossLifeInterface();
        SetSpeed();
    }

    private void Update()
    {
        _agent.SetDestination(_player.position);
        _mAnimation.Move(_agent.velocity.magnitude);

        if (!_agent.hasPath) return;
        var closeToPlayer = _agent.remainingDistance <= _agent.stoppingDistance;
        var direction = _player.position - transform.position;
        _mMovement.Rotate(direction);
        _mAnimation.Attack(closeToPlayer);
    }

    private void AttackPlayer()
    {
        var damage = Random.Range(30, 40);
        _player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    private void SetSpeed()
    {
        _agent.speed = _mStatusBoss.mSpeed;
    }

    public void TakeDamage(int damage)
    {
        _mStatusBoss.mLife -= damage;
        UpdateInterface();
        if (_mStatusBoss.mLife <= 0)
        {
            Die();
        }
    }
    
    public void BloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(mBloodParticleBoss, position, rotation);
    }

    public void Die()
    {
        _mAnimation.Die();
        _mMovement.Die();
        this.enabled = false;
        _agent.enabled = false;
        Instantiate(mMedKitPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 2);
    }

    private void UpdateInterface()
    {
        sliderBossLife.value = _mStatusBoss.mLife;
        var percentageLife = (float) _mStatusBoss.mLife / _mStatusBoss.initialLife;
        var lifeColor = Color.Lerp(minimumLife, maxLife, percentageLife);
        imageSlider.color = lifeColor;
    }

    private void SetBossLifeInterface()
    {
        sliderBossLife.maxValue = _mStatusBoss.initialLife;
        sliderBossLife.value = sliderBossLife.maxValue;
        imageSlider.color = maxLife;
    }
}
