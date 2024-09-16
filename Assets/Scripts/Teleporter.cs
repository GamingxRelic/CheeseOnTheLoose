using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    void OnTriggerEnter(Collider other)
    {
        if(string.IsNullOrEmpty(nextSceneName)) {
            Debug.Log("Teleporter - Invalid Next Scene Name");
            return;
        }

        if(other.gameObject.CompareTag("Player")) {
            SceneLoader sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
            sceneLoader.LoadLoadingScene(nextSceneName);
        }
    }

}
