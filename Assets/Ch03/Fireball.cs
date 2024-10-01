using UnityEngine;

public class Fireball : MonoBehaviour {
	public float speed = 10.0f;
	public int damage = 1;

	void Update() {
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(50f);
        }
		Destroy(gameObject);
	}
}
