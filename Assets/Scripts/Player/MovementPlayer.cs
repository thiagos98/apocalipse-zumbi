using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MovementCharacters 
{
	public void PlayerRotation(LayerMask floorMask)
	{
		var rayCam = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit impact;

		if (!Physics.Raycast(rayCam, out impact, 50, floorMask)) return;
		var playerAimPosition = impact.point - transform.position;

		playerAimPosition.y = transform.position.y;

		Rotate(playerAimPosition);
	}
}
