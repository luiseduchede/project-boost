using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugTools : MonoBehaviour
{
    int currSceneIdx;
    int qntdScenes;

    private void Start() {
        currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        qntdScenes = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            DisableCollision();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void DisableCollision() {
        GetComponent<CollisionManager>().DisableCollision();
    }

    private void NextLevel() {
        int nextScene = currSceneIdx + 1;
        if (nextScene == qntdScenes)
            SceneManager.LoadScene(0);
        else SceneManager.LoadScene(currSceneIdx + 1);
    }
}
