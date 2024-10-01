using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLogic : MonoBehaviour
{
    [SerializeField] Transform positionToTeleportPlayer;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            GameObject player = GameObject.Find("Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            Vector2 teleportRotation = new Vector2(positionToTeleportPlayer.rotation.x, positionToTeleportPlayer.rotation.y);
            playerMovement.TeleportPlayer(positionToTeleportPlayer.position, teleportRotation);
        }
    }
}
