using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [Header("Coin Collections")]
    private int coinsCollected;
    public static int totalCoins;

    [Tooltip("GameObject used for leaving this scene. E.g. a teleporter.")]
    [SerializeField] private GameObject sceneExitObject; 

    private void Awake()
    {
        totalCoins = 0;
    }
    public void CoinCollected() {
        coinsCollected++;
        if(coinsCollected == totalCoins) {
            sceneExitObject.SetActive(true);
        }
    }
}
