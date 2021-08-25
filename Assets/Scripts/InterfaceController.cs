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
	private int _mAmountZombiesDead;
	public Slider sliderLifePlayer; 
	public GameObject panelGameOver;
	public Text timeSurvived;
	public Text timeMaxSurvived;
	public Text amountZombiesDead;
	public Text textWarningBossBorn;
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

	public void UpdateAmountDeadZombies()
	{
		_mAmountZombiesDead++;
		amountZombiesDead.text = string.Format("x {0}", _mAmountZombiesDead);
	}

	public void GameOver()
	{
		var minutes = (int) (Time.timeSinceLevelLoad / 60);
		var seconds = (int) (Time.timeSinceLevelLoad % 60);
		timeSurvived.text = "You Lose!\n You survived for " + minutes + "min and " + seconds + 
		                    "s. Killed " + _mAmountZombiesDead + " zombies.";
		AdjustHighScore(minutes, seconds);
		sliderLifePlayer.gameObject.SetActive(false);
		amountZombiesDead.gameObject.SetActive(false);
		panelGameOver.SetActive(true);
	}
	
	public void ShowUpWarningBossCreated()
	{
		StartCoroutine(DisappearText(2, textWarningBossBorn));
	}

	private IEnumerator DisappearText(float time, Text disappearText)
	{
		disappearText.gameObject.SetActive(true);
		var textColor = disappearText.color;
		textColor.a = 1;
		disappearText.color = textColor;
		yield return new WaitForSeconds(time);
		float counter = 0f;
		while (disappearText.color.a > 0)
		{
			counter += Time.deltaTime / time;
			textColor.a = Mathf.Lerp(1, 0, counter);
			disappearText.color = textColor;
			if (disappearText.color.a <= 0)
			{
				disappearText.gameObject.SetActive(false);
			}
			yield return null;
		}
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
