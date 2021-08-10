using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] GameObject m_Player;
    private Vector3 m_DistCompensar;

	void Start () {
        m_DistCompensar = transform.position - m_Player.transform.position;
	}
	
	void Update () {
        transform.position = m_Player.transform.position + m_DistCompensar;
	}
}