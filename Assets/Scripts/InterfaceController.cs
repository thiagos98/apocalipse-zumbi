using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
	private PlayerController _playerController;
	private float _mMaximumScore;
	public Slider sliderLifePlayer; 
	public GameObject panelGameOver;
	public Text timeSurvived;
	public Text timeMaxSurvived;
	private void Start ()
	{
		_playerController = GameObject.FindWithTag(Tags.Player).GetComponent<PlayerController>();
		sliderLifePlayer.maxValue = _playerController.statusPlayer.mLife;
		UpdateSliderLifePlayer();
		_mMaximumScore = PlayerPrefs.GetFloat("MaxScore");
	}

	public void UpdateSliderLifePlayer()
	{
		sliderLifePlayer.value = _playerController.statusPlayer.mLife;
	}

	public void GameOver()
	{
		var minutes = (int) (Time.timeSinceLevelLoad / 60);
		var seconds = (int) (Time.timeSinceLevelLoad % 60);
		timeSurvived.text = "You Lose!\n You survived for " + minutes + "min and \n" + seconds + "s.";
		AdjustHighScore(minutes, seconds);
		sliderLifePlayer.gameObject.SetActive(false);
		panelGameOver.SetActive(true);
	}

	private void AdjustHighScore(int min, int sec)
	{
		if (Time.timeSinceLevelLoad > _mMaximumScore)
		{
			_mMaximumScore = Time.timeSinceLevelLoad;
			timeMaxSurvived.text = string.Format("Your best time was {0}min and {1}s.", min, sec);
			PlayerPrefs.SetFloat("MaxScore", _mMaximumScore);
		}

		if (timeMaxSurvived.text == "")
		{
			min = (int) _mMaximumScore / 60;
			sec = (int) _mMaximumScore % 60;
			timeMaxSurvived.text = string.Format("Your best time was {0}min and {1}s.", min, sec);
		}
	}
}
