using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    [SerializeField] float deathSuccessVol = 0.5f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource getAudioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        getAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("dost");
                break;
            case "Finish":
                 SuccessSequence();
                break;
            default:
                CrashSquence();
                break;
        }
    }

    void Update()
    {
        RespondToDebugKey();
    }

    void RespondToDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;   //toggle collision
        }
    }

    void CrashSquence()
    {
        isTransitioning = true;
        getAudioSource.Stop();
        getAudioSource.PlayOneShot(crashSound, deathSuccessVol);
        crashParticle.Play();
        GetComponent<movement>().enabled = false;
        Invoke("ReloadLevel", sceneLoadDelay);
    }

    void SuccessSequence()
    {
        isTransitioning = true;
        getAudioSource.Stop();
        getAudioSource.PlayOneShot(successSound, deathSuccessVol);
        successParticle.Play();
        GetComponent<movement>().enabled = false;
        Invoke("LoadNextLevel", sceneLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}