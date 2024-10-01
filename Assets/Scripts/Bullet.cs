using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float initialPushForce = 10f;
    [SerializeField] private float despawnTime = 8f;
    public Quaternion rotation; 
    [SerializeField] private Rigidbody rb; 

    void Start()
    {
        Vector3 direction = PlayerMovement.instance.transform.forward + Camera.main.transform.forward;
        rb.AddForce(direction * initialPushForce, ForceMode.Impulse);  

        StartCoroutine(Timer(despawnTime)); 
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(20f);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(10f);
            Destroy(gameObject);
        }

    }

    private IEnumerator Timer(float duration)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(duration);

        // Execute code after the timer is done
        Destroy(gameObject);
    }
}

