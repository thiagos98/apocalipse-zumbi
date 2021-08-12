using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
	private PlayerController _playerController;
	public Slider SliderLifePlayer; 
	private void Start ()
	{
		_playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		SliderLifePlayer.maxValue = _playerController.MLife;
		UpdateSliderLifePlayer();
	}

	public void UpdateSliderLifePlayer()
	{
		SliderLifePlayer.value = _playerController.MLife;
	}
}
