using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
	private PlayerController _playerController;
	[FormerlySerializedAs("SliderLifePlayer")] public Slider sliderLifePlayer; 
	private void Start ()
	{
		_playerController = GameObject.FindWithTag(Tags.Player).GetComponent<PlayerController>();
		sliderLifePlayer.maxValue = _playerController.statusPlayer.mLife;
		UpdateSliderLifePlayer();
	}

	public void UpdateSliderLifePlayer()
	{
		sliderLifePlayer.value = _playerController.statusPlayer.mLife;
	}
}
