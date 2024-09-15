using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    void OnTriggerEnter(Collider other)
    {
        if(nextSceneName == "") {
            Debug.Log("Teleporter Missing Scene Name");
            return;
        }

        if(other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}
