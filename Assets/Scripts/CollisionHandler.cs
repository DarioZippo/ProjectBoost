using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        RespondToDebugKeys();
    }

    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            EndLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
            Debug.Log("Collisions: " + !collisionDisabled);
        }
    }

    void OnCollisionEnter(Collision other){
        if(isTransitioning || collisionDisabled){
            return;
        }

        string tag = other.gameObject.tag;
        switch(tag){
            case "Friendly":
                Debug.Log("This thing is frienldy");
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence(){
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("EndLevel", levelLoadDelay);
    }

    void StartCrashSequence(){
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
        crashParticles.Play();
        //Disattivo il controllo dopo il crash
        GetComponent<Movement>().enabled = false;
        //Genera un ritardo prima di chiamare la funzione
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel(){
        //Così riavvio il livello corrente
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        isTransitioning = false;
    }

    void EndLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        //Se siamo nell'ultimo livello, caricherà il primo per un loop
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

        isTransitioning = false;
    }
}
