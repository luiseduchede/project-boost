using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] float crashDelay;
    [SerializeField] AudioClip shipCrash;
    [SerializeField] AudioClip shipLand;
    
    [SerializeField] ParticleSystem shipCrashParticle;
    [SerializeField] ParticleSystem shipLandParticle;

    int currSceneIdx;
    int qntdScenes;
    AudioSource source;

    bool isAlive = true;
    bool ignoreCollision = false;
    private void Start() {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        qntdScenes = SceneManager.sceneCountInBuildSettings;
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {

        if (!isAlive || ignoreCollision) { return; }

        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                Debug.Log("This is Finish");
                LandingSequence();
                break;
            case "Fuel":
                Debug.Log("This is fuel");
                break;
            default:
                Debug.Log("This is death");
                CrashSequence();
                break;
        }
    }

    private void LandingSequence() {
        GetComponent<Movement>().enabled = false;
        source.PlayOneShot(shipLand);
        shipLandParticle.Play();
        isAlive = false;
        Invoke("NextLevel", crashDelay);
    }

    private void CrashSequence() {
        GetComponent<Movement>().enabled = false;
        source.PlayOneShot(shipCrash);
        shipCrashParticle.Play();
        isAlive = false;
        Invoke("ReloadScene", crashDelay);
    }


    //-----------------------------------------------------INVOKES----------------------------------------------------------------
    private void NextLevel() {
        int nextScene = currSceneIdx + 1;
        if (nextScene == qntdScenes)
            SceneManager.LoadScene(0);
        else SceneManager.LoadScene(currSceneIdx + 1);
    }

    private void ReloadScene() {
        SceneManager.LoadScene(currSceneIdx);
    }

    public void DisableCollision() {
        ignoreCollision = !ignoreCollision;
    }
}
