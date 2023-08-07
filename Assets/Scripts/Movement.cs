using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1;
    
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem mainBoosterParticles;

    Rigidbody rigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    private void StartThrusting()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    private void RotateLeft()
    {
        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
        ApplyRotation(rotationThrust);
    }

    private void RotateRight()
    {
        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
        ApplyRotation(-rotationThrust);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        //freezing rotation to manually rotate
        //In pratica disattiviamo la fisica per poter ruotare liberamente
        rigidbody.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidbody.freezeRotation = false;
    }
}
