using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private Rigidbody _mRig;
    public float Speed;

    private void Start()
    {
        _mRig = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        _mRig.MovePosition(_mRig.position + (transform.forward * Speed * Time.deltaTime));
	}
}
