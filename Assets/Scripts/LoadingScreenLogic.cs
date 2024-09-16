using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreenLogic : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI loadingText;
   private SceneLoader sceneLoader;

   private void Start()
   {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sceneLoader.LoadNextScene();
   }

   private void Update()
   {
      if(!sceneLoader.loadingComplete) {
         loadingText.SetText("Loading progress: " + (sceneLoader.loadingProgress * 100) + "%");
      }
      else {
         loadingText.SetText("Press any button to continue...");
      }
   }
}
