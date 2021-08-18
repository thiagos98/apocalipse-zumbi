using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private GameObject player;
    private Vector3 _mDistanceOffset;

    private void Start () {
        _mDistanceOffset = transform.position - player.transform.position;
	}

    private void Update () {
        transform.position = player.transform.position + _mDistanceOffset;
	}
}