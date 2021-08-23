using UnityEngine;

public class Billboard : MonoBehaviour 
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!ReferenceEquals(_camera, null)) transform.LookAt(transform.position - _camera.transform.forward);
    }
}
