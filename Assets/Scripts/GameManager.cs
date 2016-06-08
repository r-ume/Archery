using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private float gameLengthInSeconds;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text accuracyText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameStateUI;

    [SerializeField]
    private AudioSource gameStateSounds;

    [SerializeField]
    private AudioClip gameStartSound;

    [SerializeField]
    private AudioClip gameEndSound;


    public static bool gameStarted = false;
    public static int score;
    public static int arrowsFired;
    private float timer;
    private Text gameStateText;
    private Animator gameStateAnimation;


    void Start() {

        gameStateText = gameStateUI.GetComponent<Text>();
        gameStateText.text = "Hit space to play!";
        gameStateAnimation = gameStateUI.GetComponent<Animator>();
        gameStateAnimation.SetBool("show", true);
        timer = gameLengthInSeconds;

        

        UpdateScoreBoard();

    }


    void Update() {

        // If the game has not started and the player pressed the spacebar...
        if (!gameStarted && Input.GetKeyUp(KeyCode.Space)) {

			// ... then start the game
			startGame ();

        }


        // If the game has started...
        if (gameStarted) {

            // Reset the timer and update the scoreboard.
            timer -= Time.deltaTime;
            UpdateScoreBoard();
		
        }


        if (gameStarted && timer <= 0) {
			EndGame ();
        }


        if (Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }

    }


    private void UpdateScoreBoard() {

        scoreText.text = score + " Targets";
        float accuracy = Mathf.Round((float)score / (float)arrowsFired * 100f);

        if (arrowsFired == 0)
            accuracy = 0f;

        accuracyText.text = accuracy + "% Accuracy";
        timerText.text = Mathf.RoundToInt(timer) + " Seconds";

    }


	private void startGame(){
	
		score = 0;
		gameStarted = true;
		arrowsFired = 0;
		gameStateAnimation.SetBool("show", false);
		gameStateSounds.clip = gameStartSound;
		gameStateSounds.Play();

	}

	private void EndGame(){
		gameStateText.text = "Time's up!\nHit space to play again!";
		gameStateAnimation.SetBool("show", true);
		gameStarted = false;
		timer = gameLengthInSeconds;
		gameStateSounds.clip = gameEndSound;
		gameStateSounds.Play();


	}
}
