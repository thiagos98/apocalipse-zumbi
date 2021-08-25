using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private Rigidbody _mRig;
    public int damage;
    public float speed;


    private void Start()
    {
        _mRig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _mRig.MovePosition(_mRig.position + (transform.forward * (speed * Time.deltaTime)));
    }

    private void OnTriggerEnter(Collider other)
    {
        var oppositeRotation = Quaternion.LookRotation(-transform.forward);
        switch (other.tag)
        {
            case Tags.Enemy:
                var zombie = other.GetComponent<ZombieController>();
                zombie.TakeDamage(damage);
                zombie.BloodParticle(transform.position, oppositeRotation);
                break;
            case Tags.Boss:
                var boss = other.GetComponent<BossController>();
                boss.TakeDamage(damage);
                boss.BloodParticle(transform.position, oppositeRotation);
                break;
        }

        Destroy(gameObject);
    }
}