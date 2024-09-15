using UnityEngine;

public class PlayerPushCoins : MonoBehaviour
{
    [SerializeField] private float pushForce = 15f;
    [SerializeField] private float maxDistance = 5f;
    private void Update()
    {
        PushAwayCoins();
    }

    private void PushAwayCoins() {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin"); // Get the coins in the scene.

        foreach (GameObject coin in coins)
        {   
            float distance = Vector3.Distance(transform.position, coin.transform.position); // Get the distance between player and coin
            if(distance < maxDistance) {
                Vector3 direction = transform.position - coin.transform.position; // Get the direction from player to coin
                direction.Normalize(); // Normalize the direction
                coin.GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 0, direction.z) * pushForce * -1f, ForceMode.Force); // Push the coin in the opposite direction
            }
        }
    }
}

