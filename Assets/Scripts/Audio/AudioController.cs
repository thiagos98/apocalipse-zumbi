using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _mAudioSource;
    public static AudioSource Instance;

    private void Awake()
    {
        _mAudioSource = GetComponent<AudioSource>();
        Instance = _mAudioSource;
    }
    
    
}
