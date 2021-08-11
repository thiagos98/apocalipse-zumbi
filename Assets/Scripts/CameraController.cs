using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private GameObject player;
    private Vector3 _mDistCompensar;

	void Start () {
        _mDistCompensar = transform.position - player.transform.position;
	}
	
	void Update () {
        transform.position = player.transform.position + _mDistCompensar;
	}
}