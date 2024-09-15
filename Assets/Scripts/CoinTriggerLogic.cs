using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinTriggerLogic : MonoBehaviour
{
    private void Start() {
        LevelHandler.totalCoins++;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            LevelHandler scoreHandler = GameObject.Find("LevelHandler").GetComponent<LevelHandler>();
            scoreHandler.CoinCollected();
            Destroy(gameObject);
        }
    }
}
