using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacters : MonoBehaviour
{
	protected Rigidbody MRig;

	private void Awake()
	{
		MRig = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 direction, float speed)
	{
		MRig.MovePosition(MRig.position + direction.normalized * (Time.deltaTime * speed));
	}

	public void Rotate(Vector3 direction)
	{
		var newRotation = Quaternion.LookRotation(direction);
		MRig.MoveRotation(newRotation);
	}

	public void Die()
	{
		MRig.constraints = RigidbodyConstraints.None;
		MRig.velocity = Vector3.zero;
		GetComponent<Collider>().enabled = false;
	}
	
}
