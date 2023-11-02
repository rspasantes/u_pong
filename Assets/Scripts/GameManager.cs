using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int iScoreToWin = 5;

    // Texts
    [SerializeField] private TMP_Text txtPaddle1Score;
    [SerializeField] private TMP_Text txtPaddle2Score;
    [SerializeField] private TMP_Text txtIAStateTextP1;
    [SerializeField] private TMP_Text txtIAStateTextP2;
    [SerializeField] private GameObject goEndGame;

    [SerializeField] private GameObject goStartMessage;

    // GameObjects
    [SerializeField] private GameObject goPaddle1;
    [SerializeField] private GameObject goPaddle2;
    [SerializeField] private GameObject goBall;
    [SerializeField] private GameObject goHelpScreen;

    // Scripts
    private PaddleController pcPaddle1;
    private PaddleController pcPaddle2;
    private IAScript iaPaddle1;
    private IAScript iaPaddle2;

    [SerializeField] private int paddle1Score;
    [SerializeField] private int paddle2Score;
    private bool bIsPlaying = false;
    [SerializeField] private bool isPaused = false;

    private bool bP1isIA;
    private bool bP2isIA;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private static GameManager instance;

    public static GameManager Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    private void Start()
    {
        goEndGame.SetActive(false);

        // Show message to start game
        goStartMessage.GetComponent<TextScript>().startBlink();

        pcPaddle1 = goPaddle1.GetComponent<PaddleController>();
        pcPaddle2 = goPaddle2.GetComponent<PaddleController>();
        iaPaddle1 = goPaddle1.GetComponent<IAScript>();
        iaPaddle2 = goPaddle2.GetComponent<IAScript>();

        bP1isIA = false;
        bP2isIA = true;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];
    }

    private void Update()
    {
        if (!bIsPlaying && Input.GetButton("Submit"))
        {
            startGame();
        }

        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape"))
        {
            TogglePause();
            goHelpScreen.SetActive(!goHelpScreen.activeSelf);
        }

        if (Input.GetKeyDown("1"))
        {
            pcPaddle1.enabled = !pcPaddle1.enabled;
            iaPaddle1.enabled = !iaPaddle1.enabled;

            if (iaPaddle1.enabled)
            {
                txtIAStateTextP1.text = "IA";
                bP1isIA = true;
            }
            else 
            {
                txtIAStateTextP1.text = "PLAYER1";
                bP1isIA = false;
            }
            
        }

        if (Input.GetKeyDown("2"))
        {
            pcPaddle2.enabled = !pcPaddle2.enabled;
            iaPaddle2.enabled = !iaPaddle2.enabled;

            if (iaPaddle2.enabled)
            {
                txtIAStateTextP2.GetComponent<TMP_Text>().text = "IA";
                bP2isIA = true;
            }
            else
            {
                txtIAStateTextP2.text = "PLAYER2";
                bP2isIA = false;
            }
        }

        /*if (Input.GetKey("escape"))
        {
            Exit();
        }*/
    }

    private void startGame() {

        // Reset score
        paddle1Score = 0;
        paddle2Score = 0;
        txtPaddle1Score.text = paddle1Score.ToString();
        txtPaddle2Score.text = paddle2Score.ToString();

        // Reset IA velocity
        iaPaddle1.resetSpeed();
        iaPaddle1.resetSpeed();

        bIsPlaying = true;

        // Hides all messages
        goStartMessage.GetComponent<TextScript>().stopBlink();
        goEndGame.SetActive(false);

        // We launch the ball
        StartCoroutine(relaunch(0));
    }

    private void endGame()
    {
        audioSource2.Play();

        bIsPlaying = false;
        goEndGame.GetComponent<TMP_Text>().text = paddle1Score.Equals(iScoreToWin) ? "PLAYER1 WINS!" : "PLAYER2 WINS";
        goStartMessage.GetComponent<TextScript>().startBlink();
        goEndGame.SetActive(true);
    }

    public void playerScored(string player) {

        audioSource1.Play();

        switch (player) {
            case "1":
                paddle1Score++;
                txtPaddle1Score.text = paddle1Score.ToString();
                if (bP2isIA) 
                {
                    iaPaddle2.aumSpeed();
                }
                break;
            case "2":
                paddle2Score++;
                txtPaddle2Score.text = paddle2Score.ToString();
                if (bP1isIA) 
                {
                    iaPaddle1.aumSpeed();
                }
                break;
        }

        if (paddle1Score.Equals(iScoreToWin) || paddle2Score.Equals(iScoreToWin)) {
            endGame();
        } else {
            StartCoroutine(relaunch(2));
        }
    }

    public IEnumerator relaunch(int seconds)
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(seconds);

        goBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        goBall.transform.position = new Vector2(0, 0);
        goBall.GetComponent<BallController>().Launch();
    }

    // This function toggles the pause state of the game
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pauses the game
            // Here you can also activate your UI for the pause menu
        }
        else
        {
            Time.timeScale = 1f; // Resumes the game
            // Here you can deactivate your UI for the pause menu
        }
    }

    void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}


