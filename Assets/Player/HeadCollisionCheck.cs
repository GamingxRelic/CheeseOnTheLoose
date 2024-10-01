using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionCheck : MonoBehaviour
{
    private int headColliderCount;

    public bool IsHeadColliding() {
        return headColliderCount > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) {
            headColliderCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) {
            headColliderCount--;
        }
    }
}
