using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IKillable
{
    private Transform _player;
    private NavMeshAgent _agent;
    private Status _mStatusBoss;
    private AnimationCharacters _mAnimation;
    private MovementCharacters _mMovement;

    private void Start()
    {
        _player = GameObject.FindWithTag(Tags.Player).transform;
        _agent = GetComponent<NavMeshAgent>();
        _mStatusBoss = GetComponent<Status>();
        _mAnimation = GetComponent<AnimationCharacters>();
        _mMovement = GetComponent<MovementCharacters>();
        
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
        if (_mStatusBoss.mLife <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _mAnimation.Die();
        _mMovement.Die();
        this.enabled = false;
        _agent.enabled = false;
        Destroy(gameObject, 2);
    }
}
