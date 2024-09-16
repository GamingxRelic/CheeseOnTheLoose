using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private static string nextScene;
    public float loadingProgress;
    public bool loadingComplete;
    private void Awake()
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

[Tooltip("Call when you want to exit the current scene and enter the next scene.")]
    public void LoadLoadingScene(string sceneToLoad) {
        nextScene = sceneToLoad;
        SceneManager.LoadScene("LoadingScene");
    }


[Tooltip("Should only be used in the LoadingScene.")]
    public void LoadNextScene() {
        if (!string.IsNullOrEmpty(nextScene))
        {
            StartCoroutine(LoadSceneCoroutine());
        }
        else
        {
            Debug.LogError("Next scene not set!");
        }
    }

    private IEnumerator LoadSceneCoroutine() {
        // Begin to load the scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        // Don't switch to the new scene immediately when done
        asyncLoad.allowSceneActivation = false;

        // While the scene is loading
        while (!asyncLoad.isDone)
        {
            // Set the loading progress values
            loadingProgress = asyncLoad.progress;

            // Check if the loading is complete (progress is always < 0.9 until the scene is fully loaded)
            if (asyncLoad.progress >= 0.9f)
            {
                loadingComplete = true;
                // Wait for a key press to activate the scene
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;  // Activate the scene
                }
            }

            yield return null;  // Continue the loop every frame until the scene is done loading
        }
    }
}
