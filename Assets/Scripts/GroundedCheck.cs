using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    private int groundColliderCount;

    public int GetColliderCount() {
        return groundColliderCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") == false) {
            groundColliderCount += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Coin") == false) {
            groundColliderCount -= 1;
        }
    }
    
}