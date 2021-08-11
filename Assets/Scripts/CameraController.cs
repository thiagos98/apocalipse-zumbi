using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private GameObject Player;
    private Vector3 _mDistCompensar;

	void Start () {
        _mDistCompensar = transform.position - Player.transform.position;
	}
	
	void Update () {
        transform.position = Player.transform.position + _mDistCompensar;
	}
}