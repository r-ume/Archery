using UnityEngine;
using System.Collections;

public class TargetMovement : MonoBehaviour {

	// attached to target prehab.
	[SerializeField]
	private float amplitude = 1f;

	[SerializeField]
	private float timePeriod = 1f;

	private Vector3 startPosition;

	[SerializeField]
	private float chanceOfMovement = 0.5f;

	// Use this for initialization
	void Start () { 

		// Determine whether or not this target should be moving
		if (Random.Range (0f, 1f) >= chanceOfMovement) {
			this.enabled = false;
		}
 
		//store the start position
		startPosition = transform.localPosition;

	}
	
	void LateUpdate () {

		// caluculate the data
		// this variable theta can have negative value in it, which means the target moves in both right and left direction.
		float theta = Mathf.Sin(Time.timeSinceLevelLoad / timePeriod);

		// create the new delta position
		Vector3 deltaPosition = new Vector3(amplitude, 0f, 0f) * theta;

		// Update the position by adding the start position and the delta position
		transform.localPosition = startPosition + deltaPosition;
	
	}
}
