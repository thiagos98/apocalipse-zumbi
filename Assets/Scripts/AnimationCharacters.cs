using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacters : MonoBehaviour
{
	private Animator _mAnimator;
	private static readonly int Atacando = Animator.StringToHash("Atacando");

	private void Awake()
	{
		_mAnimator = GetComponent<Animator>();
	}

	public void Attack(bool value)
	{
		_mAnimator.SetBool(Atacando, value);
	}

	public void Move(float value)
	{
		_mAnimator.SetFloat("Move", value);
	}
}
