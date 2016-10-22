using UnityEngine;
using System.Collections;

public class ShootArrow : MonoBehaviour {

    [SerializeField]
	private GameObject arrowPrefab;

    [SerializeField]
    private Transform arrowPosition;

    [SerializeField]
    private float maximumArrowForce = 200f;

    [SerializeField]
    private AudioSource bowSounds;

    private Animator bowAnimator;
    private float arrowForce = 0f;
    float lerpTime = 1f;
    float currentLerpTime;


    void Start() {

        bowAnimator = GetComponent<Animator>();

    }


    void Update() {

		// If the game has started, then allow player input.
		if (GameManager.gameStarted) {

			// If the player has started to draw the bow...
			if (Input.GetMouseButtonDown (0)) {

				DrawBow ();
            }

			// If the player is continuing to draw the bow...
			if (Input.GetMouseButton (0)) {

				PowerUpBow ();

            }

			// If the player releases the bow...
			if (Input.GetMouseButtonUp (0)) {

				ReleaseBow ();

            }

		} else {

			// Reset the drawing animation to the idle state.
			bowAnimator.SetBool("drawing", false);

            // Stop sounds.
            bowSounds.Stop();

		}        

    }

	private void DrawBow(){
		// Play the bow stretching sound.
		bowSounds.Play();

		// Start the bow drawing animation.
		bowAnimator.SetBool("drawing", true);

	}

	private void PowerUpBow(){
		// Increment timer once per frame...
		currentLerpTime += Time.deltaTime;
		// To set he maximum amount of lerp as 1f. lerp = Linear Interpolation
		if (currentLerpTime > lerpTime)
			currentLerpTime = lerpTime;

		// Lerp the arrow force the longer the player holds down the button.
		float perc = currentLerpTime / lerpTime;

		// distance from the bow to the target
		arrowForce = Mathf.Lerp(0f, maximumArrowForce, perc);
	}

	private void ReleaseBow(){
		// Reset the drawing animation to the idle state.
		bowAnimator.SetBool ("drawing", false);

		// Launch the arrow.
		// instantiate - returns the copy
		//Instantiate(Object original, Vector3 position, Quaternion rotation);
		GameObject arrowInstance = Instantiate (arrowPrefab, arrowPosition.position, transform.rotation) as GameObject;
		arrowInstance.GetComponent<Rigidbody> ().AddForce (transform.forward * arrowForce);

		// increment the number of the arrow.
		GameManager.arrowsFired++;

		// Stop the stretch sound.
		bowSounds.Stop ();

		// Reset the arrow force and timer.
		arrowForce = 0f;
		currentLerpTime = 0f;
	}
}
