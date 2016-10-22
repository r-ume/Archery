using UnityEngine;
using System.Collections;

// removes the target from the hierachy. 
public class CloneDestruction : MonoBehaviour {

	// attached to the particle for the target
    [SerializeField]
    private float duration = 5f;

    void Start () {

		// Destroys the clone after a period of time
		Destroy(gameObject, duration);

    }

}
