using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody _mRig;
    public float Speed;

    private void Start()
    {
        _mRig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate () {
        _mRig.MovePosition(_mRig.position + (transform.forward * (Speed * Time.deltaTime)));
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Inimigo"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
