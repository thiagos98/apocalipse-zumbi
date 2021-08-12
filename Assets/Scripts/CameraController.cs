using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private GameObject player;
    private Vector3 _mDistCompensar;

    private void Start () {
        _mDistCompensar = transform.position - player.transform.position;
	}

    private void Update () {
        transform.position = player.transform.position + _mDistCompensar;
	}
}