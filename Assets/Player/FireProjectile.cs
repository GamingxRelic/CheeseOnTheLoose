using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnTransform;
    [SerializeField] private float firingCooldown;
    private float timePassed; 

    private void Update()
    {
        if(timePassed >= firingCooldown) {
            if(Input.GetAxisRaw("Fire1") > 0f) {
                timePassed = 0f;
                Fire();
            }
        }
        else {
            timePassed += Time.deltaTime; 
        }
    }
    

    private void Fire() {
        Instantiate(bullet, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
    }
}