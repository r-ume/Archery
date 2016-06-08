using UnityEngine;
using System.Collections;

public class CloneDestruction : MonoBehaviour {

    [SerializeField]
    private float duration = 5f;

    void Start () {

		// Destroys the clone after a period of time
		Destroy(gameObject, duration);

    }

}
