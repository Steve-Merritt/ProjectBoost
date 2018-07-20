using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    float thrust = 120;
    float rotationRate = 45;

    float audioStartVolume = 0.0f;
    float audioFadeRate = 0.55f;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioStartVolume = audioSource.volume;
    }
    
    // Update is called once per frame
    void Update ()
    {
        ProcessInput();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply thrust
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrust);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            audioSource.volume = audioStartVolume;
        }
        else
        {
            FadeAudioOut();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; // manual control of rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationRate);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * -rotationRate);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void FadeAudioOut()
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= audioFadeRate * Time.deltaTime;
            if (audioSource.volume <= 0)
            {
                audioSource.Stop();
            }
        }
    }

}
