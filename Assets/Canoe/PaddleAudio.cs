using UnityEngine;

public class CanoeMovementAudio : MonoBehaviour
{
    public AudioSource paddleAudioSource; // Reference to the AudioSource component
    public Rigidbody canoeRigidbody;      // Reference to the Rigidbody component of the canoe
    public float movementThreshold = 0.1f; // Threshold to determine if the canoe is moving

    private bool isPlaying = false;

    void Start()
    {
        if (paddleAudioSource == null)
        {
            paddleAudioSource = GetComponent<AudioSource>();
        }

        if (canoeRigidbody == null)
        {
            canoeRigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        float speed = canoeRigidbody.velocity.magnitude;

        if (speed > movementThreshold && !isPlaying)
        {
            paddleAudioSource.Play();
            isPlaying = true;
        }
        else if (speed <= movementThreshold && isPlaying)
        {
            paddleAudioSource.Stop();
            isPlaying = false;
        }
    }
}
