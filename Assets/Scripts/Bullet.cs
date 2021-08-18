using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour {
    private Rigidbody _mRig;
    public int damage;
    public float speed;


    private void Start()
    {
        _mRig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate () {
        _mRig.MovePosition(_mRig.position + (transform.forward * (speed * Time.deltaTime)));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Inimigo"))
        {
            other.GetComponent<ZombieController>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
