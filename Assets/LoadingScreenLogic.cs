using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenLogic : MonoBehaviour
{
   private void Start()
   {
        SceneLoader sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        sceneLoader.LoadNextScene();
   }
}
