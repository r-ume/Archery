using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour {

	[SerializeField]
	private Vector3 spawnArea = new Vector3(1, 1, 1);

	[SerializeField]
	private int targetsToSpawn = 3;

	public static int totalTargets;

	[SerializeField]
	private GameObject targetPrefab;


	// Update is called once per frame
	void Update () {
		
		// If the game has started AND there are zero targets...
		if (GameManager.gameStarted && totalTargets == 0) {

			// ... spawn new targets.
			SpawnTargets();

		}

		// If the game has not started AND there are more than 0 targets...
		if (!GameManager.gameStarted && totalTargets > 0) {

			// destroy all targets
			DestroyTargets();

		}
	
	}

	private void SpawnTargets(){

		float xMin = transform.position.x - spawnArea.x / 2f;
		float yMin = transform.position.y - spawnArea.y / 2f;
		float zMin = transform.position.z - spawnArea.z / 2f;
		float xMax = transform.position.x + spawnArea.x / 2f;
		float yMax = transform.position.y + spawnArea.y / 2f;
		float zMax = transform.position.z + spawnArea.z / 2f;

		for (int i = 0; i < targetsToSpawn; i++) {

			float positionX = Random.Range(xMin, xMax);
			float positionY = Random.Range(yMin, yMax);
			float positionZ = Random.Range(zMin, zMax);

			GameObject targetInstance = Instantiate(targetPrefab, new Vector3(positionX, positionY, positionZ), Quaternion.identity) as GameObject;

			totalTargets++;

		}
	}

	private void DestroyTargets(){

		totalTargets = 0;

		GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
		for (int i = 0; i < targets.Length; i++) {

			//Disable target movement
			TargetMovement targetMovement = targets[i].GetComponent<TargetMovement>();
			targetMovement.enabled = false;

			// Enable the rigidbody on the target to allow it to fall.
			Rigidbody rigidyBody = targets[i].GetComponent<Rigidbody>();
			rigidyBody.isKinematic = false;

			// Turn off collisions to prevent double hits.
			targets[i].GetComponent<Collider>().enabled = false;

			// Start to destroy this object.
			Destroy(targets[i], 1f);

		}
	}

	void OnDrawGizmos() {

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, spawnArea);

	}
}
