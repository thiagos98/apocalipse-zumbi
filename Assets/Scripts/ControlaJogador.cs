using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
	[SerializeField] float m_Speed;
	private void Update ()
	{
		var axisX = Input.GetAxis("Horizontal");
		var axisZ = Input.GetAxis("Vertical");
		
		var direction = new Vector3(axisX, 0, axisZ);
		transform.Translate(direction * (Time.deltaTime * m_Speed));
	}
}
