using UnityEngine;
using System.Collections;

public class TargetCollision : MonoBehaviour {

    [SerializeField]
    private GameObject impactParticles;

	[SerializeField]
	private TargetMovement targetMovement;


    private AudioSource impactSounds;



    void Start() {

        impactSounds = GetComponent<AudioSource>();

    }


    void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Arrow")) {

            // Play an impact sound.
            impactSounds.volume = other.relativeVelocity.normalized.magnitude;
            impactSounds.Play();

            // Spawn hit particles.
            Instantiate(impactParticles, transform.position, Quaternion.identity);

            // Parent the arrow to the target.
            other.transform.parent = gameObject.transform;

			//Disable target movement
			targetMovement.enabled = false;

            // Stop the arrow from moving so that it's "stuck" on the target.
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // Enable the rigidbody on the target to allow it to fall.
            GetComponent<Rigidbody>().isKinematic = false;

            // Turn off collisions to prevent double hits.
            GetComponent<Collider>().enabled = false;

			// Decrement the total number of targets.
			TargetSpawner.totalTargets--;

			// Add the target to the score.
			GameManager.score++;

            // Start to destroy this object.
            Destroy(gameObject, 1f);

        }

    }

}
