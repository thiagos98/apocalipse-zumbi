﻿using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ZombieController : MonoBehaviour, IKillable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mMedKitPrefab;
    private Vector3 _mDirection;
    private int _mDamage;
    private MovementCharacters _mMovement;
    private AnimationCharacters _mAnimation;
    private Status _mStatusZombie;
    private Vector3 _mRandomPosition;
    private float _mWanderCounter;
    private GameController _gameController;
    private float _mTimeBetweenRandomPosition = 4;
    private float _mGenerationPercentage = 0.1f;
    public ZombieGenerator ZombieGenerator;
    public AudioClip zombieDeathSound;
    

    private void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        _mMovement = GetComponent<MovementCharacters>();
        _mAnimation = GetComponent<AnimationCharacters>();
        _mStatusZombie = GetComponent<Status>();
        _gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
        RandomizeZombie();
    }

    private void FixedUpdate()
    {
        var positionPlayer = player.transform.position;
        var positionZombie = transform.position;
        var distance = Vector3.Distance(positionZombie, positionPlayer);
        
        _mMovement.Rotate(_mDirection);
        _mAnimation.Move(_mDirection.magnitude);
        if (distance > 15)
        {
            Wander();
        }
        else if (distance > 2.5f)
        {
            _mDirection = positionPlayer - positionZombie;
            _mMovement.Move(_mDirection, _mStatusZombie.mSpeed);
            _mAnimation.Attack(false);
        }
        else
        {
            _mDirection = positionPlayer - positionZombie;
            _mAnimation.Attack(true);
        }
    }

    private void Wander()
    {
        _mWanderCounter -= Time.deltaTime;

        if (_mWanderCounter <= 0)
        {
            _mRandomPosition = RandomizePosition();
            _mWanderCounter += _mTimeBetweenRandomPosition + Random.Range(-2f, 2f);
        }

        var itsClose = Vector3.Distance(transform.position, _mRandomPosition) <= 0.05f;
        if (itsClose) return;
        _mDirection = _mRandomPosition - transform.position;
        _mMovement.Move(_mDirection, _mStatusZombie.mSpeed);
    }

    private Vector3 RandomizePosition()
    {
        var position = Random.insideUnitSphere * Random.Range(10, 15);
        position += transform.position;
        position.y = transform.position.y;
        return position;
    }

    private void AttackPlayer()
    {
        _mDamage = Random.Range(20, 30);
        player.GetComponent<PlayerController>().TakeDamage(_mDamage);
    }

    private void RandomizeZombie()
    {
        var generateTypeZombie = Random.Range(1, transform.childCount);
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
        _mAnimation.Die();
        _mMovement.Die();
        RandomGenerationMedKit(_mGenerationPercentage);
        _gameController.UpdateAmountDeadZombies();
        ZombieGenerator.ReduceAmountLiveZombies();
        this.enabled = false;
        Destroy(gameObject, 2);
    }

    private void RandomGenerationMedKit(float generationPercentage)
    {
        if (Random.value <= generationPercentage)
        {
            Instantiate(mMedKitPrefab, transform.position, Quaternion.identity);
        }
    }
}